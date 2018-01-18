using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Configuration;

public class clsControleLivro
{
    private string wStrServer = ConfigurationManager.AppSettings["pStrServer"].ToString();
    private string wStrDataBase = ConfigurationManager.AppSettings["pStrDataBase"].ToString();
    private string wStrUser = ConfigurationManager.AppSettings["pStrUser"].ToString();
    private string wStrpass = ConfigurationManager.AppSettings["pStrpass"].ToString();

    public int LivId { get; set; }
	public int AutId { get; set; }
	public int EdiId { get; set; }
	public int CatId { get; set; }
	public int TipId { get; set; }
    public string LivISBN { get; set; }
	public string LivTitulo { get; set; }
	public int LivAno { get; set; }
	public string LivEdicao { get; set; }
	public string LivLocalizacao { get; set; }
	public string LivCapa { get; set; }
	public string LivObservacao { get; set; }
	public int LivPaginas { get; set; }
	public DateTime LivDataCadastro { get; set; }
	public int LivStatus { get; set; }
	
    public clsControleLivro()
    {
        this.AutId = 0;
		this.EdiId = 0;
		this.CatId = 0;
		this.TipId = 0;
		this.LivISBN = string.Empty;
		this.LivTitulo = string.Empty;
		this.LivAno = 0;
		this.LivEdicao = string.Empty;
		this.LivLocalizacao = string.Empty;
		this.LivCapa = string.Empty;
		this.LivObservacao = string.Empty;
		this.LivPaginas = 0;
		this.LivStatus = 0;
    }
    
    public DataTable ConsultarDataTable(string strTexto, string strFiltro, string intStatus)
    {
        clsBD objBd = new clsBD(wStrServer, wStrDataBase, wStrUser, wStrpass);

        try
        {
            string sql;

            sql = "select * from tblivro L ";
            sql = sql + "left outer join tbautor A on (A.autId = L.autId) ";
            sql = sql + "left outer join tbeditora E on (E.ediId = L.ediId) ";
            sql = sql + "left outer join tbcategoria C on (C.catId = L.catId) ";
            sql = sql + "left outer join tbtipolivro T on (T.tipId = L.tipId) ";
            
            if (strFiltro.Trim().ToUpper().Equals("TITULO"))
            {
                sql = sql + "where L.livTitulo like '%" + strTexto.Trim() + "%'";
            }
            else if (strFiltro.Trim().ToUpper().Equals("AUTOR"))
            {
                sql = sql + "where E.ediNome like '%" + strTexto.Trim() + "%'";
            }
            else if (strFiltro.Trim().ToUpper().Equals("EDITORA"))
            {
                sql = sql + "where E.ediNome like '%" + strTexto.Trim() + "%'";
            }
            else if (strFiltro.Trim().ToUpper().Equals("CATEGORIA"))
            {
                sql = sql + "where C.catDescricao like '%" + strTexto.Trim() + "%'";
            }
            else if (strFiltro.Trim().ToUpper().Equals("TIPOLIVRO"))
            {
                sql = sql + "where T.tipDescricao like '%" + strTexto.Trim() + "%'";
            }
            else if (strFiltro.Trim().ToUpper().Equals("LIVID"))
            {
                sql = sql + "where L.livId = " + Convert.ToInt32(strTexto.Trim());
            }
            else
            {
                sql = sql + "where L.livTitulo like '%" + strTexto.Trim() + "%'";
            }
            
            if (intStatus != "")
            {
                sql = sql + " and L.livStatus = '" + intStatus + "'";
            }
            
            sql = sql + " order by L.livTitulo;";

            MySqlCommand cmd = new MySqlCommand(sql);
            cmd.CommandType = CommandType.Text;

            return objBd.ExecuteDataTable(cmd);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao consultar datatable livro" + ex.Message);
        }
    }

    public DataTable ConsultarDataTableIn(string strTexto)
    {
        clsBD objBd = new clsBD(wStrServer, wStrDataBase, wStrUser, wStrpass);

        try
        {
            if (!strTexto.Trim().Equals(""))
            {
                string sql;
                sql = "select * from tblivro ";
                sql = sql + "where livId in (" + strTexto.Trim() + ")";
                sql = sql + " order by livTitulo;";
                MySqlCommand cmd = new MySqlCommand(sql);
                cmd.CommandType = CommandType.Text;
                return objBd.ExecuteDataTable(cmd);
            }

            return new DataTable();
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao consultar datatable livro" + ex.Message);
        }
    }
}