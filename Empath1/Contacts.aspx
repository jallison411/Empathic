<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="Empath1.Contacts" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7" lang=""> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8" lang=""> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9" lang=""> <![endif]-->
<!--[if gt IE 8]><!--> 
<html class="no-js" lang=""> <!--<![endif]-->
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  <title></title>
  <meta name="description" content="">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="apple-touch-icon" href="edde-master/apple-touch-icon.png">
<link href="https://fonts.googleapis.com/css?family=Raleway:300,400,700,900" rel="stylesheet">   
 <link rel="stylesheet" href="edde-master/css/bootstrap.min.css">
  <style>
      body {
          padding:0;
      }
  </style>
  <link rel="stylesheet" href="edde-master/css/bootstrap-theme.min.css">
  <link rel="stylesheet" href="edde-master/css/main.css">

  <script src="edde-master/js/vendor/modernizr-2.8.3-respond-1.4.2.min.js"></script>



</head>
<body>
    <form id="form1" runat="server">
        <!--[if lt IE 8]>
      <p class="browserupgrade">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> to improve your experience.</p>
  <![endif]-->
  <!-- Mobile Header -->
  <div class="mobile-header">
    <div class="mobile-back"><span class="glyphicon glyphicon-chevron-left" style="font-size: 12px;"></span>&nbsp;back</div>
    <div class="mobile-title">Project ABC</div>
  </div>
  <!-- Desktop View -->
  <div class="side-bar">
    <div class="side-header">
      <div class="side-logo"><img src="edde-master/img/mc-logo2.png" /></div>
      <div class="side-name">
        <h3>Macomb County</h3>
        <p>ECONOMIC DEVELOPMENT</p>
      </div>
    </div>
    <div class="side-nav">
      <a class="side-nav-item" href="main.aspx">
        <div class="side-nav-text">
         Home
        </div>
      </a>
      <a class="side-nav-item" href="Contacts.aspx">
        <div class="side-nav-text active">
          Contact
        </div>
      </a>
    </div>
    <div class="side-user">
      Signed in as:  <b>Fori Automotive</b>
    </div>
 
      
  

       <div class="side-project">
      <!-- Split button -->
   <div class="dropdown2">
       <asp:DropDownList ID="DropDownList1" runat="server" class="btn btn-primary dropdown-toggle" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
 
  </div>
    </div>



  </div> 


  <div class="contact-block">
    <div class="contact-container">
       <div class="contact-header">
        Our Team</div>
      <div class="row"> 
        <div class="col-sm-12" id="clientContacts" runat="server">
       
          <div class="contact-item">
            <div class="contact-avatar">
              <img src="edde-master/img/contact.png">
            </div>
            <div class="contact-info">
              <div class="contact-name"><span>John Smithterson</span></div>
              <div class="contact-title"><span>Job Title Goes Here</span></div>
              <div class="contact-department"><span>Macomb County Economic Development</span></div>
              <div class="contact-email"><span><a href="#">john.smith@macombcounty.gov</a></span></div>
              <div class="contact-phone"><span>234-567-4567</span></div>
            </div>
          </div>
          <div class="contact-item grey2">
            <div class="contact-avatar">
              <img src="edde-master/img/contact.png">
            </div>
            <div class="contact-info">
              <div class="contact-name"><span>John Smithterson</span></div>
              <div class="contact-title"><span>Job Title Goes Here</span></div>
              <div class="contact-department"><span>Macomb County Economic Development</span></div>
              <div class="contact-email"><span><a href="#">john.smith@macombcounty.gov</a></span></div>
              <div class="contact-phone"><span>234-567-4567</span></div>
            </div>
          </div>
          <div class="contact-item">
            <div class="contact-avatar">
              <img src="img/contact.png">
            </div>
            <div class="contact-info grey">
              <div class="contact-name"><span>John Smithterson</span></div>
              <div class="contact-title"><span>Job Title Goes Here</span></div>
              <div class="contact-department"><span>Macomb County Economic Development</span></div>
              <div class="contact-email"><span><a href="#">john.smith@macombcounty.gov</a></span></div>
              <div class="contact-phone"><span>234-567-4567</span></div>
            </div>
          </div>
          <div class="contact-item grey2">
            <div class="contact-avatar">
              <img src="img/contact.png">
            </div>
            <div class="contact-info">
              <div class="contact-name"><span>John Smithterson</span></div>
              <div class="contact-title"><span>Job Title Goes Here</span></div>
              <div class="contact-department"><span>Macomb County Economic Development</span></div>
              <div class="contact-email"><span><a href="#">john.smith@macombcounty.gov</a></span></div>
              <div class="contact-phone"><span>234-567-4567</span></div>
            </div>
          </div>
          <div class="contact-item grey">
            <div class="contact-avatar">
              <img src="img/contact.png">
            </div>
            <div class="contact-info">
              <div class="contact-name"><span>John Smithterson</span></div>
              <div class="contact-title"><span>Job Title Goes Here</span></div>
              <div class="contact-department"><span>Macomb County Economic Development</span></div>
				<div class="contact-email"><span><a href="#">john.smith@macombcounty.gov</a></span></div>
              <div class="contact-phone"><span>234-567-4567</span></div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="partner-container">
      <div class="partner-header">
        Our Partners
      </div>
      <div class="partner-content" id="partnerBlock" runat="server">
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
				<p><a href="#">sally.jones.smith@companyname.org</a></p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
              <p>sally.jones.smith@companyname.org</p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
              <p>sally.jones.smith@companyname.org</p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
              <p>sally.jones.smith@companyname.org</p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
              <p>sally.jones.smith@companyname.org</p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
              <p>sally.jones.smith@companyname.org</p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
              <p>sally.jones.smith@companyname.org</p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
              <p>sally.jones.smith@companyname.org</p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
              <p>sally.jones.smith@companyname.org</p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
              <p>sally.jones.smith@companyname.org</p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        <div class="partner-item">
          <div class="partner-left">
            <div class="partner-name">Sally Jones Smith</div>
            <div class="partner-title">Job Title Goes Here</div>
            <div class="partner-company">Company Name Here</div>
          </div>
          <div class="partner-right">
            <div class="partner-info">
              <p>sally.jones.smith@companyname.org</p>
              <p>234.567.8976</p>
            </div>
          </div>
        </div>
        
      </div>
    </div>
  </div>
  <div class="mobile-footer">
    <div class="mobile-footer-nav"><a href="index.html#home">home</a></div>
    <div class="mobile-footer-nav"><a href="index.html#note">notes</a></div>
    <div class="mobile-footer-nav active"><a href="contact.html">contact</a></div>
  </div>
  <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
  <script>window.jQuery || document.write('<script src="edde-master/js/vendor/jquery-1.11.2.min.js"><\/script>')</script>
  <script src="edde-master/js/vendor/bootstrap.min.js"></script>
  <script src="edde-master/js/contact.js"></script>

    </form>
</body>
</html>
