<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Landing.aspx.cs" Inherits="Empath1.Landing" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>EMPATHETIC</h2>
            </hgroup>
            <p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>.
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET.
                If you have any questions about ASP.NET visit
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
   <!--[if lt IE 8]>
      <p class="browserupgrade">You are using an <strong>outdated</strong> browser. Please <a href="http://browsehappy.com/">upgrade your browser</a> to improve your experience.</p>
  <![endif]-->
  <!-- Mobile Header -->
  <div class="mobile-header" id="Jeff">
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
      <a class="side-nav-item" href="index.html">
        <div class="side-nav-text active">
         Home
        </div>
      </a>
      <a class="side-nav-item" href="contact.html">
        <div class="side-nav-text">
          Contact
        </div>
        <div class="side-nav-img"></div>
      </a>
    </div>
    <div class="side-user">
      Signed in as:  <b>Fori Automotive</b>
    </div>
    <div class="side-project">
      <!-- Split button -->
      <div class="btn-group dropdown-btn">
        <button type="button" class="btn btn-blue btn-text">Incentive Project</button>
        <button type="button" class="btn btn-blue dropdown-toggle" >
          <span class="caret"></span>
          <span class="sr-only">Toggle Dropdown</span>
        </button>
        <ul class="dropdown-menu">
          <li><a href="#">Incentive Project </a></li>
          <li><a href="#">Ford Project</a></li>
        </ul>
      </div>
      <button type="button" class="btn btn-view">View</button>
    </div>
  </div> 
  <div class="document-container">
    <div class="row">
      <div class="document-item document-item-blue col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-bg1 col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-bg2 col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-white col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-blue col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-bg1 col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-bg2 col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-white col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-blue col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-bg1 col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-bg2 col-md-6" data-url="pdf.html">
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
      <div class="document-item document-item-white col-md-6" data-url="pdf.html">
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
      <div class="note-header-title">Note</div>
    </div>
    <div class="note-content">
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here. 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here. 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here. 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here. 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here. 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here. 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here. 
        </div>
      </div>
      <div class="note-item">
        <div class="note-time">12/12/2017</div>
        <div class="note-title">Descriptive title of note goes here.</div>
        <div class="note-detail">
          Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here. Detailed notes here.  Detailed notes here.  Detailed notes here. 
        </div>
      </div>
    </div>
  </div>
  <div class="mobile-footer">
    <div class="mobile-footer-nav home-nav"><a href="#home">home</a></div>
    <div class="mobile-footer-nav note-nav"><a href="#note">notes</a></div>
    <div class="mobile-footer-nav"><a href="contact.html">contact</a></div>
  </div>
  <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
  <script>window.jQuery || document.write('<script src="edde-master/js/vendor/jquery-1.11.2.min.js"><\/script>')</script>
  <script src="edde-master/js/vendor/bootstrap.min.js"></script>
  <script src="edde-master/js/main.js"></script>
</asp:Content>