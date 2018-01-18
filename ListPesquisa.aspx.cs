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
    public partial class ListPesquisa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ALUNO"] == null)
            {
                Session["ALUNO"] = null;
                Response.Redirect("~/Login.aspx");
            }
        }

        private void MontarListView(string strTexto, string strFiltro, string intStatus)
        {
            clsRotinas objRotinas = new clsRotinas();
            StringBuilder stbListView = new StringBuilder();
            DataTable objDataTable = new DataTable();

            clsControleLivro objLivro = new clsControleLivro();

            objDataTable = objLivro.ConsultarDataTable(strTexto, strFiltro, intStatus);
            stbListView = objRotinas.GetGenericListView(objDataTable, "LIVTITULO", new string[] { "LIVTITULO" }, "FrmLivroDetalhes.aspx", "LIVID", "");

            divListaLivros.InnerHtml = stbListView.ToString();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string strTexto = txtTexto.Text.Trim();
            string strFiltro = "";
            string strStatus = "";

            strFiltro = Request.Form["radio-choice-h-2"].ToString();

            if (ckbStatus.Checked.Equals(true))
            {
                strStatus = "S";
            }

            MontarListView(strTexto, strFiltro, strStatus);
        }
    }
}