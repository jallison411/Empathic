//-----------------------------------------------------------------------
// <copyright file="Secure.cs" company="Empathic">
//     Copyright (c) Empathic All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Empath1.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Security;
    using System.Security.Cryptography;
    using System.Web;
    using System.Web.Configuration;

    /// <summary>
    /// This class contains logic for encrypting the password connecting to O365
    /// In the web config file if the reset flag is set to yes or true a new password will be requested. This will be a pop up box on the login page. Initial setup only.
    /// </summary>
    public class Secure
    {
        /// <summary>
        /// This field represents the salt characters used for encryption.
        /// </summary>
        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("Salt Is Not A Password");

        /// <summary>
        /// This method will encrypt the provided string using salt
        /// </summary>
        /// <param name="input">string to encrypt</param>
        /// <returns>returns encrypted string</returns>
        public static string EncryptString(System.Security.SecureString input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)), entropy, System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        /// <summary>
        /// This method will decrypt the provided string using salt
        /// </summary>
        /// <param name="encryptedData">requires the encrypted string input</param>
        /// <returns>returns the decrypted string</returns>
        public static System.Security.SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(Convert.FromBase64String(encryptedData), entropy, System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        /// <summary>
        /// Converts a string to a secure string object
        /// </summary>
        /// <param name="input">requires string input</param>
        /// <returns>returns a secure string</returns>
        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }

            secure.MakeReadOnly();
            return secure;
        }

        /// <summary>
        /// This method reverses a secure string
        /// </summary>
        /// <param name="input">requires a secure string object</param>
        /// <returns>returns a string</returns>
        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }

            return returnValue;
        }

        /// <summary>
        /// This method checks the web config for a reset password flag element. If missing or the element is set to yes or true then the return is true for a reset condition
        /// </summary>
        /// <returns>returns a boolean true or false</returns>
        public static bool ResetPasswordFlag()
        {
            bool result = true;
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");

                if (config.AppSettings.Settings["ResetPassword"] == null)
                {
                    // Reset password key is missing from config -- add it in AND reset the password.
                    config.AppSettings.Settings.Remove("ResetPassword");
                    config.AppSettings.Settings.Add("ResetPassword", "YES");
                }
                else
                {
                    // setting exists -- check value
                    if (config.AppSettings.Settings["ResetPassword"].Value.ToString().ToUpper().Trim() == "YES" || config.AppSettings.Settings["ResetPassword"].Value.ToString().ToUpper().Trim() == "TRUE")
                    {
                        result = true;
                    }
                    else
                    {
                        // Flag set to not reset -- but first check if the setting even exists
                        if (config.AppSettings.Settings["O365LoginPassword"] != null)
                        {
                            // all good no need to reset the password.
                            result = false;
                        }
                    }
                }
            }
            catch (Exception ignore)
            {
                string errorMessage = ignore.Message;
            }

            return result;
        }

        /// <summary>
        /// This method will store a new secure string password in the web config.
        /// </summary>
        /// <param name="newPassword">requires a secure string object</param>
        public static void ResetPassword(SecureString newPassword)
        {
            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");

                // Set the password in the config file
                config.AppSettings.Settings.Remove("O365LoginPassword");
                config.AppSettings.Settings.Add("O365LoginPassword", Secure.EncryptString(newPassword));
                config.AppSettings.Settings.Remove("ResetPassword");
                config.AppSettings.Settings.Add("ResetPassword", "False");
                config.Save();
            }
            catch (Exception error)
            {
                string myError = error.Message;
            }
        }

        /// <summary>
        /// This method will retrieve the stored secure string password from the web config
        /// </summary>
        /// <returns>returns a secure string</returns>
        public static SecureString GetO365Password()
        {
            SecureString result = null;

            try
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");
                string encryptedStringPassword = config.AppSettings.Settings["O365LoginPassword"].Value;
                result = Secure.DecryptString(encryptedStringPassword);
            }
            catch (Exception es)
            {
                string error = es.Message;
            }

            return result;
        }
    }
}