using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BiblioBookingMobile.classe;
using System.Text;
using System.Data;

namespace BiblioBookingMobile
{
    public partial class ListReservas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ALUNO"] == null)
            {
                Session["ALUNO"] = null;
                Response.Redirect("~/Login.aspx");
            }

            if (Session["RESERVA"] != null)
            {
                string itens = fcPegarItens();
                MontarListView(itens);
            }
            else 
            {
                MontarListView(null);
            }
        }

        private void MontarListView(string itens)
        {
            clsRotinas objRotinas = new clsRotinas();
            StringBuilder stbListView = new StringBuilder();
            DataTable objDataTable = new DataTable();

            clsControleLivro objLivro = new clsControleLivro();

            if (itens != null)
            {
                objDataTable = objLivro.ConsultarDataTableIn(itens);
                stbListView = objRotinas.GetGenericListView(objDataTable, "LIVTITULO", new string[] { "LIVTITULO" }, "FrmLivroDetalhes.aspx", "LIVID", "");
                ProcessarReserva();
            }
            else
            {
                stbListView = new StringBuilder("Nenhum livro adicionado para reserva.");
            }

            divListItensReserva.InnerHtml = stbListView.ToString();
        }

        private string fcPegarItens()
        {
            string livIds = null;

            if (Session["RESERVA"] != null)
            {
                // Pega a reserva atual da Sessão
                Reserva reserva = (Reserva)Session["RESERVA"];

                if (reserva.Itens.Count > 0)
                {
                    int index = 1;

                    foreach (var item in reserva.Itens)
                    {
                        livIds += item.LivId;

                        if (index < reserva.Itens.Count)
                        {
                            livIds += ",";
                        }
                        index += 1;
                    }
                }
            }
            return livIds;
        }

        private void ProcessarReserva()
        {
            StringBuilder stbListOpcao = new StringBuilder();
            string strPathConcluirReserva = "";

            strPathConcluirReserva = String.Format("{0}", "#popupDialog");
            stbListOpcao.AppendLine("<ul>");
            stbListOpcao.AppendLine("<li>");
            stbListOpcao.AppendLine(String.Format("<a href=\"{0}\" data-rel=\"popup\" data-theme=\"c\" data-position-to=\"window\" data-role=\"button\" data-inline=\"false\" data-transition=\"pop\" data-corners=\"true\" data-shadow=\"true\" data-iconshadow=\"true\" data-wrapperels=\"span\" data-theme=\"c\" aria-haspopup=\"true\" aria-owns=\"#popupDialog\" class=\"ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-c\"><span class=\"ui-btn-inner ui-btn-corner-all\"><span class=\"ui-btn-text\">Concluir Reserva</span></span></a>", strPathConcluirReserva));
            stbListOpcao.AppendLine("</li>");
            stbListOpcao.AppendLine("</ul>");
            divConcluirReservar.InnerHtml = stbListOpcao.ToString();
        }
    }
}