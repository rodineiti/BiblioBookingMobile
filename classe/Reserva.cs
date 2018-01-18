using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace BiblioBookingMobile.classe
{
    public class Reserva
    {
        private string wStrServer = ConfigurationManager.AppSettings["pStrServer"].ToString();
        private string wStrDataBase = ConfigurationManager.AppSettings["pStrDataBase"].ToString();
        private string wStrUser = ConfigurationManager.AppSettings["pStrUser"].ToString();
        private string wStrpass = ConfigurationManager.AppSettings["pStrpass"].ToString();

        private List<ItemReserva> _Itens = new List<ItemReserva>();
        
        public List<ItemReserva> Itens { get { return _Itens; } }

        public void AdicionarItem(string livId, string aluId)
        {
            _Itens.Add(new ItemReserva { LivId = livId, AluId = aluId });
        }
        public void Limpar()
        {
            _Itens.Clear();
        }
        public void RemoverItem(int item)
        {
           _Itens.RemoveAt(item);
        }

        public int InserirReserva(int aluId)
        {
            clsBD objBd = new clsBD(wStrServer, wStrDataBase, wStrUser, wStrpass);

            int resId = 0;

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into tbreserva (aluId, resDataReserva) values (@aluId, NOW());select last_insert_id();");
                
                cmd.Parameters.AddWithValue("@aluId", aluId);
                
                resId = Convert.ToInt32(objBd.ExecuteObject(cmd));
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir reserva: " + ex.Message);
            }

            return resId;
        }

        public void InserirItensReserva(int livId, int resId)
        {
            clsBD objBd = new clsBD(wStrServer, wStrDataBase, wStrUser, wStrpass);

            try
            {
                MySqlCommand cmd = new MySqlCommand("insert into tbitemreserva (livId, resId, iteData) values (@livId, @resId, NOW());");

                cmd.Parameters.AddWithValue("@livId", livId);
                cmd.Parameters.AddWithValue("@resId", resId);

                objBd.ExecuteQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir item de reserva: " + ex.Message);
            }
        }

        public void AtualizaStatusLivro(string livId, string livStatus)
        {
            clsBD objBd = new clsBD(wStrServer, wStrDataBase, wStrUser, wStrpass);

            try
            {
                MySqlCommand cmd = new MySqlCommand("update tblivro set livStatus = @livStatus where livId = @livId;");

                cmd.Parameters.AddWithValue("@livId", Convert.ToInt32(livId));
                cmd.Parameters.AddWithValue("@livStatus", livStatus.ToUpper().Trim());

                objBd.ExecuteQuery(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar status do livro: " + ex.Message);
            }
        }

        public bool ConsultarDisponibilidadeLivro(int livId)
        {
            clsBD objBd = new clsBD(wStrServer, wStrDataBase, wStrUser, wStrpass);
            
            bool booRetorno = true;
            
            try
            {
                MySqlCommand cmd = new MySqlCommand("select count(livId) from tblivro where livId = @livId and livStatus in ('N');");

                cmd.Parameters.AddWithValue("@livId", livId);

                int resposta = Convert.ToInt32(objBd.ExecuteObject(cmd));
                
                if (resposta > 0)
                {
                    booRetorno = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar disponibilidade do livro: " + ex.Message);
            }

            return booRetorno;
        }

        public DataTable ConsultarItem(int resId)
        {
            clsBD objBd = new clsBD(wStrServer, wStrDataBase, wStrUser, wStrpass);

            try
            {
                MySqlCommand cmd = new MySqlCommand("select * from tbitemreserva i inner join tblivro l on (l.livId = i.livId) where i.resId = @resId;");
                
                cmd.Parameters.AddWithValue("@resId", Convert.ToInt32(resId));

                return objBd.ExecuteDataTable(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao consultar datatable item reserva: " + ex.Message);
            }
        }
    }
}