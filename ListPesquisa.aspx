<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListPesquisa.aspx.cs" Inherits="BiblioBookingMobile.ListPesquisa" %>
<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <title>Bibliobooking</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="css/jquery.mobile-1.3.1.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mobile-1.3.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        /* stack all grids below 40em (640px) */
        @media all and (max-width: 35em) {
	        .my-breakpoint .ui-block-a,
	        .my-breakpoint .ui-block-b,
	        .my-breakpoint .ui-block-c,
	        .my-breakpoint .ui-block-d,
	        .my-breakpoint .ui-block-e {
		        width: 100%;
		        float: none;
	        }
        }
    </style>
</head>
<body>
    <div data-role="page" data-theme="a">
        <div data-role="header" data-position="fixed">
	        <h1>BiblioBooking</h1>
	        <a href="Principal.aspx" data-icon="home" data-iconpos="notext" data-direction="reverse" class="ui-btn-left jqm-home" data-ajax="false">Home</a>
            <a href="#" data-rel="back" class="ui-btn-right" data-ajax="false">Voltar</a>
        </div>
        <form id="form1" runat="server">
        <div data-role="content" id="content">
            <label for="txtTexto">Pesquisar:</label>
            <asp:TextBox ID="txtTexto" runat="server"></asp:TextBox>
            <fieldset data-role="controlgroup" data-type="horizontal">
                <legend>Horizontal:</legend>
                <input type="radio" name="radio-choice-h-2" id="radio-choice-h-2a" value="Titulo" checked="checked">
                <label for="radio-choice-h-2a">Título</label>
                <input type="radio" name="radio-choice-h-2" id="radio-choice-h-2b" value="Autor">
                <label for="radio-choice-h-2b">Autor</label>
                <input type="radio" name="radio-choice-h-2" id="radio-choice-h-2c" value="Editora">
                <label for="radio-choice-h-2c">Editora</label>
                <input type="radio" name="radio-choice-h-2" id="radio-choice-h-2d" value="Categoria">
                <label for="radio-choice-h-2d">Categoria</label>
                <input type="radio" name="radio-choice-h-2" id="radio-choice-h-2e" value="TipoLivro">
                <label for="radio-choice-h-2e">Tipo de Livro</label>
            </fieldset>
            <asp:CheckBox ID="ckbStatus" runat="server" class="custom" />
            <label for="ckbStatus">Disponível</label>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" onclick="btnBuscar_Click" />
            <p>Lista de Livros</p>
            <div id="divListaLivros" runat="server">
            </div>
        </div>
        </form>
        <div id="footer" data-role="footer" data-position="fixed">
		    <h3>&copy BiblioBooking - Todos os direitos reservados</h3>
	    </div>
    </div>
</body>
</html>
