using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using BiblioBookingMobile.classe;

namespace BiblioBookingMobile
{
    public partial class ListReservasAtivas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ALUNO"] == null)
            {
                Session["ALUNO"] = null;
                Response.Redirect("~/Login.aspx");
            }

            MontarListView();
        }

        private void MontarListView()
        {
            clsRotinas objRotinas = new clsRotinas();
            StringBuilder stbListView = new StringBuilder();
            DataTable objDataTable = new DataTable();
            clsControleAluno objReserva = (clsControleAluno)Session["ALUNO"];
            DataTable objDataTableItem;
            Reserva ObjReservaItem;

            objReserva.AluId = objReserva.AluId;
            objDataTable = objReserva.ConsultarReservas();

            foreach (DataRow row in objDataTable.Rows)
            {
                stbListView.AppendLine("<div data-role=\"collapsible\">");
                stbListView.AppendLine(String.Format("<h3>Data: {0} - Protocolo: {1}</h3>", row["resDataReserva"].ToString(), row["resId"].ToString()));
                ObjReservaItem = new Reserva();
                objDataTableItem = new DataTable();
                objDataTableItem = ObjReservaItem.ConsultarItem(Convert.ToInt32(row["resId"]));
                foreach (DataRow row2 in objDataTableItem.Rows)
                {
                    stbListView.AppendLine(String.Format("<p>Livro: {0}</p>", row2["livTitulo"].ToString()));
                }
                stbListView.AppendLine("</div>");
            }
            content.InnerHtml = stbListView.ToString();
        }
    }
}