<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="Empath1.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
         <meta charset="utf-8"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
  <title></title>
  <meta name="description" content=""/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
  <link rel="apple-touch-icon" href="apple-touch-icon.png"/>
  <link href="https://fonts.googleapis.com/css?family=Raleway:300,400,700,900" rel="stylesheet"/> 
  <link rel="stylesheet" href="edde-master/css/bootstrap.min.css"/>
  <style>
      body {
          height: 100%;
      }
  </style>
  <link rel="stylesheet" href="edde-master/css/bootstrap-theme.min.css"/>
  <link rel="stylesheet" href="edde-master/css/main.css"/>

  <script src="edde-master/js/vendor/modernizr-2.8.3-respond-1.4.2.min.js"></script>

    <asp:PlaceHolder runat="server">     
          <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>  
    <webopt:BundleReference runat="server" Path="~/Content/css" /> 
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width" />

      
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
 


</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" EnablePageMethods="true"></asp:ScriptManager>
     
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
      <div class="side-logo"><img src="edde-master/img/mc-logo.png" /></div>
      <div class="side-name">
        <h3>Macomb County</h3>
        <p>ECONOMIC DEVELOPMENT</p>
      </div>
    </div>
 <div class="side-nav">
      <a class="side-nav-item" href="main.aspx">
        <div class="side-nav-text active">
         Home
        </div>
      </a>
      <a class="side-nav-item" href="contacts.aspx">
        <div class="side-nav-text">
          Contact
        </div>
        <div class="side-nav-img"><img src="edde-master/img/contact-avatar.png" id="mainContactImage" runat="server"/></div>
      </a>
    </div>
	  <div class="welcome-message" id="sideWelcomeMessage" runat="server" ><span id="welcome-header">Welcome to the Fori Automotive Economic Development Dashboard</span>
		Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate.
    </div>
    <div class="side-user" id="sideSignedInAs" runat="server" >
		Signed in as  <b>Fori Automotive</b> | <a href="#" >sign out</a>
    </div>
      
      <div class="side-project">

      <!-- Split button -->
   <div class="dropdown2">
       <asp:DropDownList ID="DropDownList1" runat="server" class="btn btn-primary dropdown-toggle" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>
 
  </div>
    </div>

      <!--JA START  -->
 
   <!--JA END -->
      
  </div> 
 
        <div class="document-container">
  <div class="overlay"></div>
    <div class="row" id="MainDocContainerRow" runat="server">        
  
      <div class="document-item document-item-blue col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse . . .
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-bg1 col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse . . .
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-bg2 col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
           Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse . . .
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-white col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse . . .
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-red col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
           Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse . . .
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-bg1 col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse . . .
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-bg2 col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
          This is the description of the document.  Brief.  Not the exec summary.  This one doesn’t have a thumbnail.
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-white col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
          This is the description of the document.  Brief.  Not the exec summary.  This one doesn’t have a thumbnail.
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-blue col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
          This is the description of the document.  Brief.  Not the exec summary.  This one doesn’t have a thumbnail.
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-bg1 col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
          This is the description of the document.  Brief.  Not the exec summary.  This one doesn’t have a thumbnail.
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-bg2 col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
          This is the description of the document.  Brief.  Not the exec summary.  This one doesn’t have a thumbnail.
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
      <div class="document-item document-item-white col-md-6 boxlink" data-url="pdf.html">
        <div class="document-time">
          12/12/2017
        </div>
        <div class="document-title">Document name XYZ</div>
        <div class="document-summary">
          This is the description of the document.  Brief.  Not the exec summary.  This one doesn’t have a thumbnail.
        </div>
        <div class="document-nav">
          <a href="doc/example.pdf" class="document-nav-square">
            <span class="glyphicon glyphicon-menu-right"></span>
          </a>
        </div>
      </div>
    </div>
    </div>
 

      <div class="note-container">
    <div class="note-header">
		<span class="glyphicon glyphicon-menu-left"></span>
      <div class="note-header-title">Notes</div>
    </div>

    <div class="note-content" id ="NoteContentContainer" runat="server">
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
         Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco 
        </div>
      </div>
    </div>
  </div>
  <div class="mobile-footer">
    <div class="mobile-footer-nav home-nav"><a href="#home">home</a></div>
    <div class="mobile-footer-nav note-nav"><a href="#note">notes</a></div>
    <div class="mobile-footer-nav"><a href="contact.html">contact</a></div>
  </div>         
     
  <script src="edde-master/js/vendor/jquery-1.11.2.min.js"></script>
  <script>window.jQuery || document.write('<script src="edde-master/js/vendor/jquery-1.11.2.min.js"><\/script>')</script>
  <script src="edde-master/js/vendor/bootstrap.min.js"></script>
  <script src="edde-master/js/main.js"></script>

 
<script>
$(document).ready(function(){
    $(".dropdown2").on("hidden.bs.dropdown", function (event) {
        // Change the text of the project to the selected project and reload page
        var x = $(event.relatedTarget).text(); // Get the button text
        alert("Running script...: " + x);
        PageMethods.GetData();
    });
});
</script>
     
 <script type="text/javascript">
        function CallingServerSideFunction() {
            PageMethods.GetData();
        }
    </script>



    </form>
</body>
</html>
