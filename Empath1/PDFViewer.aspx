<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PDFViewer.aspx.cs" Inherits="Empath1.PDFViewer" %>

<!DOCTYPE html>
<!--[if lt IE 7]>      <html class="no-js lt-ie9 lt-ie8 lt-ie7" lang=""> <![endif]-->
<!--[if IE 7]>         <html class="no-js lt-ie9 lt-ie8" lang=""> <![endif]-->
<!--[if IE 8]>         <html class="no-js lt-ie9" lang=""> <![endif]-->
<!--[if gt IE 8]><!--> <html class="no-js" lang=""> <!--<![endif]-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
  <title></title>
  <meta name="description" content="">
  <!-- <meta name="viewport" content="width=device-width, initial-scale=1"> -->
  <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
  <link rel="apple-touch-icon" href="edde-master/apple-touch-icon.png">
<!--   <link href="https://fonts.googleapis.com/css?family=Roboto:300,400,700" rel="stylesheet">
 -->  <link rel="stylesheet" href="edde-master/css/bootstrap.min.css">
  <style>
      html, body {
          padding: 0;
          height: 100%;
      }
      embed{
        position: absolute;
        top: 40px;
        left: 0;
        right: 0;
        bottom: 0;
      }
      .pdf-header{
        position: fixed;
        height: 40px;
        padding: 10px 20px;
        line-height: 20px;
        font-size: 14px;
        color: black;
        top: 0;
        left: 0;
        right: 0;
      }
      .pdf-header a{
        color: black;
        text-decoration: none;
      }
      .pdf-reader a:hover{
        text-decoration: none;
        color: #333;
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
  <!-- Desktop View -->
  <div class="pdf-header container-fluid">
    <a href="index.html"><span class="glyphicon glyphicon-chevron-left" style="font-size: 12px;"></span>&nbsp;BACK</a>
    <div class="pull-right">PDF Title</div>
  </div>
  <embed src="edde-master/doc/example.pdf" width="100%" height="100%" type='application/pdf'>
    </form>
</body>
</html>
