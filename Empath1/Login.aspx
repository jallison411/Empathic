<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Empath1.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
  <title></title>
  <meta name="description" content=""/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="apple-touch-icon" href="edde-master/apple-touch-icon.png"/>
<!--   <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,700" rel="stylesheet">
 -->  <link rel="stylesheet" href="edde-master/css/bootstrap.min.css"/>
  <style>
      body {
          padding-top: 50px;
          padding-bottom: 20px;
      }
  </style>
  <link rel="stylesheet" href="edde-master/css/bootstrap-theme.min.css"/>
  <link rel="stylesheet" href="edde-master/css/main.css"/>

  <script src="edde-master/js/vendor/modernizr-2.8.3-respond-1.4.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"></asp:ScriptManager>
  <!--[if lt IE 8]>
      <p class="browserupgrade">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> to improve your experience.</p>
  <![endif]-->
  <!-- Desktop View -->
  <div class="full-container login-screen">
    <div class="login-container text-center">
      <div class="login-header">
          <div class="glyphicon-new-window" id ="NewPasswordDiv" runat="server" visible="false">
              <asp:Label ID="Label1" runat="server" Text="Label" ForeColor="Red">Please enter a password for Office 365</asp:Label>
              <br />
              <asp:TextBox ID="NewPasswordTextBox" runat="server" TextMode="Password"></asp:TextBox>
              <asp:Button ID="NewPasswordButtonOK" runat="server" Enabled="false" Text="OK" OnClick="NewPasswordButtonOK_Click" />
          </div>
        <div class="header-logo"><img src="edde-master/img/header-logo.png" /></div>
      </div>
      <div class="login-body">
        <div class="input-group">
          <input type="text" class="form-control" id="userNameTextBox" runat="server" placeholder="*username">
        </div>
        <div class="input-group">
          <input type="password" class="form-control" id="passwordTextBox" runat="server" placeholder="password">
        </div>
        <div class="input-group login-button">
          
      <button class="btn btn-login" id="loginButton" runat="server" >Login</button>
          <button class="btn btn-create" id="newAccount" runat="server">Create Account</button>
        </div>
        <div class="login-footer">
          * use your Microsoft Account ID (XBOX, Office 365, etc)  Or simply create a new one.         
        </div>
           <div class="login-error" id="loginErrorDiv" runat="server" style="visibility:hidden">
          Invalid Credentials. Please try again.
        </div>
      </div>
    </div>
  </div> <!-- /container -->        
  <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
  <script>window.jQuery || document.write('<script src="edde-master/js/vendor/jquery-1.11.2.min.js"><\/script>')</script>
  <script src="edde-master/js/vendor/bootstrap.min.js"></script>
  <script src="edde-master/js/main.js"></script>
  </form>

</body>
</html>
