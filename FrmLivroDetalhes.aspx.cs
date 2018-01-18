using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using BiblioBookingMobile.classe;
using System.Web.Services;

namespace BiblioBookingMobile
{
    public partial class FrmLivroDetalhes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ALUNO"] == null)
            {
                Session["ALUNO"] = null;
                Response.Redirect("~/Login.aspx");
            }

            // Cria uma reserva vazio na sessão se ele não exitir
            if (Session["RESERVA"] == null)
            {
                Session["RESERVA"] = new Reserva();
            }

            // Cria uma reserva vazio na sessão se ele não exitir
            if (Session["PENDENTES"] == null)
            {
                Session["PENDENTES"] = new Reserva();
            }

            int livId = 0;
            string livTitulo = "";
            string autNome = "";
            string ediNome = "";
            string catDescricao = "";
            string livAno = "";
            string livEdicao = "";
            string livStatus = "";
            
            if(!Page.IsPostBack)
            {
                StringBuilder stbListView = new StringBuilder();
                DataTable objDataTable = new DataTable();

                clsControleLivro objLivro = new clsControleLivro();

                objDataTable = objLivro.ConsultarDataTable(Request.QueryString["id"],"LIVID", "");

                foreach (DataRow row in objDataTable.Rows)
                {
                    livId = Convert.ToInt32(row["livId"]);
                    livTitulo = row["livTitulo"].ToString();
                    autNome = row["autNome"].ToString();
                    ediNome = row["ediNome"].ToString();
                    catDescricao = row["catdescricao"].ToString();
                    livAno = row["livAno"].ToString();
                    livEdicao = row["livEdicao"].ToString();
                    livStatus = row["livStatus"].ToString();
                }

                stbListView.AppendLine(String.Format("<h2>Título: {0}</h2>", livTitulo.ToString()));
                stbListView.AppendLine(String.Format("<p>Autor: {0}</p>", autNome.ToString()));
                stbListView.AppendLine(String.Format("<p>Editora: {0}</p>", ediNome.ToString()));
                stbListView.AppendLine(String.Format("<p>Categoria: {0}</p>", catDescricao.ToString()));
                stbListView.AppendLine(String.Format("<p>Ano: {0}</p>", livAno.ToString()));
                stbListView.AppendLine(String.Format("<p>Edição: {0}</p>", livEdicao.ToString()));
                stbListView.AppendLine(String.Format("<input type=\"hidden\" id=\"livId\" name=\"livId\" value=\"{0}\">", livId));
                if (livStatus.ToUpper().Trim().Equals("S") && ItemReservado(livId.ToString()) == false)
                {
                    stbListView.AppendLine("<a href=\"#popupDialog\" data-rel=\"popup\" data-position-to=\"window\" data-role=\"button\" data-inline=\"true\" data-transition=\"pop\" data-corners=\"true\" data-shadow=\"true\" data-iconshadow=\"true\" data-wrapperels=\"span\" data-theme=\"c\" aria-haspopup=\"true\" aria-owns=\"#popupDialog\" class=\"ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-c\"><span class=\"ui-btn-inner ui-btn-corner-all\"><span class=\"ui-btn-text\">Incluir Item</span></span></a>");
                }
                if (ItemReservado(livId.ToString()) == true)
                {
                    stbListView.AppendLine("<a href=\"#popupDialogDel\" data-rel=\"popup\" data-position-to=\"window\" data-role=\"button\" data-inline=\"true\" data-transition=\"pop\" data-corners=\"true\" data-shadow=\"true\" data-iconshadow=\"true\" data-wrapperels=\"span\" data-theme=\"c\" aria-haspopup=\"true\" aria-owns=\"#popupDialogDel\" class=\"ui-btn ui-shadow ui-btn-corner-all ui-btn-inline ui-btn-up-c\"><span class=\"ui-btn-inner ui-btn-corner-all\"><span class=\"ui-btn-text\">Excluir Item</span></span></a>");
                }
                divDetalhesLivros.InnerHtml = stbListView.ToString();
            }
        }

        private bool ItemReservado(string livId)
        {
            bool booExiste = false;
            
            if (Session["RESERVA"] != null)
            {
                // Pega a reserva atual da Sessão
                Reserva reserva = (Reserva)Session["RESERVA"];
                clsControleAluno aluno = (clsControleAluno)Session["ALUNO"];

                if (reserva.Itens.Count > 0)
                {
                    for (int i = 0; i < reserva.Itens.Count; i++)
                    {
                        if (reserva.Itens[i].LivId.Equals(livId))
                        {
                            booExiste = true;
                        }
                    }
                }
            }

            return booExiste;
        }
    }
}