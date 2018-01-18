<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListReservasAtivas.aspx.cs" Inherits="BiblioBookingMobile.ListReservasAtivas" %>
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <title>BiblioBooking</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="css/jquery.mobile-1.3.1.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mobile-1.3.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="page" data-theme="a">
        <div data-role="header" data-position="fixed">
	        <h1>BiblioBooking</h1>
	        <a href="Principal.aspx" data-icon="home" data-iconpos="notext" data-direction="reverse" class="ui-btn-left jqm-home" data-ajax="false">Home</a>
            <a href="#" data-rel="back" class="ui-btn-right" data-ajax="false">Voltar</a>
        </div>
        <div id="content" runat="server">
		   
	    </div>
    </div>
    </form>
</body>
</html>