using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BiblioBookingMobile
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogar_Click(object sender, EventArgs e)
        {
            if (!txtLogin.Text.Equals("") && !txtSenha.Text.Equals(""))
            {
                clsControleAluno objAluno = new clsControleAluno();
                objAluno.AluEmail = txtLogin.Text.Trim();
                objAluno.AluSenha = txtSenha.Text.Trim();
                DataTable objTableLogin = objAluno.ValidarLogin();

                if (objAluno.CarregarDadosAlunos(objTableLogin) == true)
                {
                    Session["ALUNO"] = objAluno;

                    Response.Redirect("Principal.aspx");
                }
                else
                {
                    lblResultado.Text = "Usuário não encontrado!";
                }
            }
            else
            {
                lblResultado.Text = "Informe todos os dados!";
            }
        }
    }
}