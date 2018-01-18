<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrmLivroDetalhes.aspx.cs" Inherits="BiblioBookingMobile.FrmLivroDetalhes" %>

<!DOCTYPE html>
<html lang="en">
<head id="Head1" runat="server">
    <title>Bibliobooking</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="css/jquery.mobile-1.3.1.min.css" rel="stylesheet" type="text/css" />
    <script src="js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script src="js/jquery.mobile-1.3.1.min.js" type="text/javascript"></script>
</head>
<body>
    <form runat="server" id="form1">
    <div data-role="page" data-theme="a">
        <div data-role="header" data-position="fixed">
	        <h1>BiblioBooking</h1>
	        <a href="Principal.aspx" data-icon="home" data-iconpos="notext" data-direction="reverse" class="ui-btn-left jqm-home" data-ajax="false">Home</a>
            <a href="#" data-rel="back" class="ui-btn-right" data-ajax="false">Voltar</a>
        </div>
        <div data-role="content" id="content">
            <h1>Detalhes do Livros</h1>
            <div id="divDetalhesLivros" runat="server" data-role="collapsible">

            </div>
        </div>
        <div data-role="popup" id="popupDialog" data-overlay-theme="a" data-theme="c" style="max-width:400px;" class="ui-corner-all ui-popup ui-body-c ui-overlay-shadow" aria-disabled="false" data-disabled="false" data-shadow="true" data-corners="true" data-transition="none" data-position-to="origin">
			<div data-role="header" data-theme="a" class="ui-corner-top ui-header ui-bar-a" role="banner">
				<h1 class="ui-title" role="heading" aria-level="1">Reservar Item</h1>
			</div>
			<div data-role="content" data-theme="d" class="ui-corner-bottom ui-content ui-body-d" role="main">
				<h3 class="ui-title">Adicionar Item</h3>
				<a href="#" data-role="button" data-inline="true" data-rel="back" data-theme="c" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" class="ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-c"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Cancelar</span></span></a>
				<a href="#" data-role="button" data-inline="true" data-transition="flow" data-theme="b" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" class="ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-b" onclick="ProcessarInclusao()"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Inserir</span></span></a>  
                <link href="css/toastmessage/jquery.toastmessage-min.css" rel="stylesheet" type="text/css" />
                <script src="js/toastmessage/jquery.toastmessage-min.js" type="text/javascript"></script>
                <script type="text/javascript">
                    function ProcessarInclusao() {
                        $.ajax({
                            url: "handler/HandlerReserva.ashx?strAcao=ADD",
                            data: { livId: $("#livId").val() },
                            success: function (data) {
                                if (data == "0") {
                                    MostrarMensagem("Item já inserido anteriomente!", "error");
                                } else if (data == "1") {
                                    MostrarMensagem("Item inserido com sucesso!", "success");
                                    setTimeout(function () {
                                        window.location = 'FrmLivroDetalhes.aspx?id=' + $("#livId").val();
                                    }, 2000);
                                } else if (data == "2") {
                                    MostrarMensagem("Quantidade de itens de reserva excedido!", "notice");
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
                                sticky: false,
                                position: 'middle-center',
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
        <div data-role="popup" id="popupDialogDel" data-overlay-theme="a" data-theme="c" style="max-width:400px;" class="ui-corner-all ui-popup ui-body-c ui-overlay-shadow" aria-disabled="false" data-disabled="false" data-shadow="true" data-corners="true" data-transition="none" data-position-to="origin">
			<div data-role="header" data-theme="a" class="ui-corner-top ui-header ui-bar-a" role="banner">
				<h1 class="ui-title" role="heading" aria-level="1">Excluir Item</h1>
			</div>
			<div data-role="content" data-theme="d" class="ui-corner-bottom ui-content ui-body-d" role="main">
				<h3 class="ui-title">Excluir Item</h3>
				<a href="#" data-role="button" data-inline="true" data-rel="back" data-theme="c" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" class="ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-c"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Cancelar</span></span></a>
				<a href="#" data-role="button" data-inline="true" data-transition="flow" data-theme="b" data-corners="true" data-shadow="true" data-iconshadow="true" data-wrapperels="span" class="ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-b" onclick="ProcessarExclusao()"><span class="ui-btn-inner ui-btn-corner-all"><span class="ui-btn-text">Excluir</span></span></a>  
                <script type="text/javascript">
                    function ProcessarExclusao() {
                        $.ajax({
                            url: "handler/HandlerReserva.ashx?strAcao=EXC",
                            data: { livId: $("#livId").val() },
                            success: function (data) {
                                if (data == "0") {
                                    MostrarMensagem("Você não possui reserva deste item!", "error");
                                } else if (data == "1") {
                                    MostrarMensagem("Item excluído com sucesso!", "success");
                                    setTimeout(function () {
                                        window.location = 'FrmLivroDetalhes.aspx?id=' + $("#livId").val();
                                    }, 2000);
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
                                sticky: false,
                                position: 'middle-center',
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

