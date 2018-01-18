using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Configuration;

public class clsControleAluno
{
    private string wStrServer = ConfigurationManager.AppSettings["pStrServer"].ToString();
    private string wStrDataBase = ConfigurationManager.AppSettings["pStrDataBase"].ToString();
    private string wStrUser = ConfigurationManager.AppSettings["pStrUser"].ToString();
    private string wStrpass = ConfigurationManager.AppSettings["pStrpass"].ToString();

    public int AluId { get; set; }
	public int CurId { get; set; }
	public int TurId { get; set; }
	public string AluNome { get; set; }
	public string AluEmail { get; set; }
    public string AluCPF { get; set; }
	public string AluSenha { get; set; }
	public string AluEndereco { get; set; }
	public string AluBairro { get; set; }
	public string AluCidade { get; set; }
	public string AluCEP { get; set; }
	public string AluUF { get; set; }
	public string AluFone { get; set; }
	public string AluCelular { get; set; }
	public string AluSituacao { get; set; }
	public string AluObservacao { get; set; }
	public DateTime AluCadastro { get; set; }
	public DateTime AluAlteracao { get; set; }
	public DateTime AluUltAcesso { get; set; }
	
    public clsControleAluno()
    {
        this.AluId = 0;
		this.CurId = 0;
		this.TurId = 0;
		this.AluNome = string.Empty;
		this.AluEmail = string.Empty;
		this.AluCPF = string.Empty;
		this.AluSenha = string.Empty;
		this.AluEndereco = string.Empty;
		this.AluBairro = string.Empty;
		this.AluCidade = string.Empty;
		this.AluCEP = string.Empty;
		this.AluUF = string.Empty;
		this.AluFone = string.Empty;
		this.AluCelular = string.Empty;
        this.AluSituacao = string.Empty;
		this.AluObservacao = string.Empty;
    }

    public DataTable ValidarLogin()
    {
        clsBD objBd = new clsBD(wStrServer, wStrDataBase, wStrUser, wStrpass);

        try
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM tbaluno WHERE aluEmail = @login AND aluSenha = @senha AND aluSituacao = 'S';");
            
            cmd.Parameters.AddWithValue("@login", this.AluEmail);
            cmd.Parameters.AddWithValue("@senha", this.AluSenha);

            return objBd.ExecuteDataTable(cmd);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro logar aluno " + ex.Message);
        }
    }

    public DataTable ConsultarReservas()
    {
        clsBD objBd = new clsBD(wStrServer, wStrDataBase, wStrUser, wStrpass);

        try
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbreserva where aluId = @aluId");
            
            cmd.Parameters.AddWithValue("@aluId", this.AluId);

            return objBd.ExecuteDataTable(cmd);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao consultar datatable reserva aluno: " + ex.Message);
        }
    }

    public bool CarregarDadosAlunos(DataTable objDataTable)
    {
        bool booRetono = false;

        if (objDataTable.Rows.Count > 0)
        {
            if (!String.IsNullOrEmpty(objDataTable.Rows[0]["aluId"].ToString()))
                this.AluId = Convert.ToInt32(objDataTable.Rows[0]["aluId"].ToString());

            if (!String.IsNullOrEmpty(objDataTable.Rows[0]["curId"].ToString()))
                this.CurId = Convert.ToInt32(objDataTable.Rows[0]["curId"].ToString());

            if (!String.IsNullOrEmpty(objDataTable.Rows[0]["turId"].ToString()))
                this.TurId = Convert.ToInt32(objDataTable.Rows[0]["turId"].ToString());

            if (!String.IsNullOrEmpty(objDataTable.Rows[0]["aluNome"].ToString()))
                this.AluNome = objDataTable.Rows[0]["aluNome"].ToString();

            if (!String.IsNullOrEmpty(objDataTable.Rows[0]["aluEmail"].ToString()))
                this.AluEmail = objDataTable.Rows[0]["aluEmail"].ToString();

            if (!String.IsNullOrEmpty(objDataTable.Rows[0]["aluCPF"].ToString()))
                this.AluCPF = objDataTable.Rows[0]["aluCPF"].ToString();

            if (!String.IsNullOrEmpty(objDataTable.Rows[0]["aluFone"].ToString()))
                this.AluFone = objDataTable.Rows[0]["aluFone"].ToString();

            if (!String.IsNullOrEmpty(objDataTable.Rows[0]["aluCelular"].ToString()))
                this.AluCelular = objDataTable.Rows[0]["aluCelular"].ToString();

            if (!String.IsNullOrEmpty(objDataTable.Rows[0]["aluSituacao"].ToString()))
                this.AluSituacao = objDataTable.Rows[0]["aluSituacao"].ToString();

            booRetono = true;
        }

        return booRetono;
    }
}