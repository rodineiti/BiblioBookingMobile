<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="BiblioBookingMobile.Principal" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <title>BiblioBooking</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="css/jquery.mobile-1.3.1.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mobile-1.3.1.min.js" type="text/javascript"></script>
    <style type="text/css">
		img.main-image{margin-top:-15px;width:100%;display:block; }
		img.dining-main-image{width:100%;display:block; }
	</style>
</head>
<body>
    <form id="form1" runat="server" data-ajax="false">
    <div data-role="page" data-theme="a">
        <div data-role="header" data-position="fixed">
	        <h1>BiblioBooking</h1>
	        <a href="Principal.aspx" data-icon="home" data-iconpos="notext" data-direction="reverse" class="ui-btn-left jqm-home" data-ajax="false">Home</a>
            <asp:Button ID="Button1" runat="server" Text="Logoff" class="ui-btn-right" 
                onclick="Button1_Click" />
        </div>
        <img class="main-image" src="img/logo.fw.png" />
        <div data-role="content">
		    <ul data-role="listview">
                <li data-role="list-divider">MENU</li> 
			    <li><a href="ListPesquisa.aspx" data-ajax="false" data-transition="flow">Pesquisar</a></li>
                <li><a href="ListReservas.aspx" data-ajax="false" data-transition="flow">Reservas Pendentes</a></li>
                <li><a href="ListReservasAtivas.aspx" data-ajax="false" data-transition="flow">Reservas Ativas</a></li>
                <% if (HttpContext.Current.Session["PENDENTES"] != null)
                   {
                       // Pega a pendencia atual da Sessão
                       BiblioBookingMobile.classe.Reserva pendencia = (BiblioBookingMobile.classe.Reserva)HttpContext.Current.Session["PENDENTES"];

                       if (pendencia.Itens.Count > 0)
                       {
                     %>
                     <li><a href="ListReservasNaoConcluidas.aspx" class="ui-link-inherit" data-ajax="false" data-transition="flow">Itens Não Reservado</a></li>
                     <%
                       }
                   }
                     %>
		    </ul>
	    </div>
        <div id="footer" data-role="footer" data-position="fixed">
		    <h3>&copy BiblioBooking - Todos os direitos reservados</h3>
	    </div>
    </div>
    </form>
</body>
</html>
