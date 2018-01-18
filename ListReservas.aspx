<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListReservas.aspx.cs" Inherits="BiblioBookingMobile.ListReservas" %>
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
            <a href="ListPesquisa.aspx" class="ui-btn-right" data-ajax="false" data-icon="back">Voltar</a>
        </div>
        <div data-role="content" id="divListItensReserva" runat="server">
		   
	    </div>
        <div id="footer" data-role="footer" data-position="fixed">
            <div data-role="navbar" id="divConcluirReservar" runat="server">
              
            </div>
        </div>
        <div data-role="popup" id="popupDialog" data-overlay-theme="a" data-theme="c" style="max-width:400px;" class="ui-corner-all ui-popup ui-body-c ui-overlay-shadow" aria-disabled="false" data-disabled="false" data-shadow="true" data-corners="true" data-transition="none" data-position-to="origin">
		<div data-role="header" data-theme="a" class="ui-corner-top ui-header ui-bar-a" role="banner">
			<h1 class="ui-title" role="heading" aria-level="1">Confirmação</h1>
		</div>
		<div data-role="content" data-theme="d" class="ui-corner-bottom ui-content ui-body-d" role="main">
			<h3 class="ui-title">Confirmar Reserva</h3>
			<a href="#" data-role="button" data-inline="true" data-rel="back" data-theme="c" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" class="ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-c"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Cancelar</span></span></a>
			<a href="#" data-role="button" data-inline="true" data-transition="flow" data-theme="b" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" class="ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-b" onclick="ProcessarReserva()"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Reservar</span></span></a>  
            <link href="css/toastmessage/jquery.toastmessage-min.css" rel="stylesheet" type="text/css" />
            <script src="js/toastmessage/jquery.toastmessage-min.js" type="text/javascript"></script>
            <script type = "text/javascript">
                function ProcessarReserva() {
                    $.ajax({
                        url: "handler/HandlerReserva.ashx?strAcao=CON",
                        success: function (data) {
                            if (data == "0") {
                                MostrarMensagem("Não há mais itens pendentes para reserva no momento!", "error");
                            } else if (data != "0" && data != "") {
                                MostrarMensagem("Reserva concluída com sucesso!\nVocê tem 12 horas para retirar sua reserva.\nApós este período sua reserva será cancelada!\nAnote seu protocolo: " + data, "success");
                                setTimeout(function () {
                                    window.location = 'ListReservasAtivas.aspx';
                                }, 15000);
                            } else {
                                MostrarMensagem("Erro ao retornar dados!", "warning");
                            }
                        },
                        error: function (a, b, c) {
                            alert("Ocorreu um problema ao validar o documento. Tente novamente!");
                        }
                    });

                    function MostrarMensagem(strTexto, strTipo) {
                        $().toastmessage('showToast', {
                            text: strTexto,
                            sticky: true,
                            position: 'top-center',
                            type: strTipo,
                            closeText: '',
                            close: function () {
                                console.log("toast is closed ...");
                            }
                        });
                    }
                }
            </script>
		</div>
	</div>
    </div>
    </form>
</body>
</html>
