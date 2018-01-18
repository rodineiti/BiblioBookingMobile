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
    public partial class ListReservasNaoConcluidas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ALUNO"] == null)
            {
                Session["ALUNO"] = null;
                Response.Redirect("~/Login.aspx");
            }

            if (Session["PENDENTES"] != null)
            {
                string itens = fcPegarItens();
                MontarListView(itens);
            }
            else
            {
                MontarListView(null);
            }
        }

        private string fcPegarItens()
        {
            string livIds = null;

            if (Session["PENDENTES"] != null)
            {
                // Pega a reserva atual da Sessão
                Reserva reserva = (Reserva)Session["PENDENTES"];

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
            }
            else
            {
                stbListView = new StringBuilder("Nenhum livro encontrado como não concluido!");
            }

            divListItensReservaNaoConcluida.InnerHtml = stbListView.ToString();
        }
    }
}