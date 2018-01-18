using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BiblioBookingMobile.classe;

namespace BiblioBookingMobile.handler
{
    /// <summary>
    /// Summary description for HandlerReserva
    /// </summary>
    public class HandlerReserva : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.QueryString["strAcao"].ToString() == "ADD")
            {
                String livId = context.Request.Params["livId"];
                context.Response.Write(ReservarItem(livId));
            }
            else if (context.Request.QueryString["strAcao"].ToString() == "CON")
            {
                string strRetorno = ProcessarReserva();
                context.Response.Write(strRetorno);
            }
            else if (context.Request.QueryString["strAcao"].ToString() == "EXC")
            {
                String livId = context.Request.Params["livId"];
                
                if (fcRemoverItem(livId) == true)
                {
                    context.Response.Write("1");
                }
                else
                {
                    context.Response.Write("0");
                }
            }
            else
            {
                context.Response.Write("Parametros inválidos!");
            }
        }

        public string ProcessarReserva()
        {
            string booRetorno = "0";

            if (HttpContext.Current.Session["RESERVA"] != null)
            {
                // Pega a reserva atual da Sessão
                Reserva reserva = (Reserva)HttpContext.Current.Session["RESERVA"];
                clsControleAluno aluno = (clsControleAluno)HttpContext.Current.Session["ALUNO"];

                int resId = reserva.InserirReserva(Convert.ToInt32(aluno.AluId));

                if (resId != 0)
                {
                    for (int i = 0; i < reserva.Itens.Count; i++)
                    {
                        if (reserva.ConsultarDisponibilidadeLivro(Convert.ToInt32(reserva.Itens[i].LivId)) == true)
                        {
                            reserva.InserirItensReserva(Convert.ToInt32(reserva.Itens[i].LivId), resId);
                            reserva.AtualizaStatusLivro(reserva.Itens[i].LivId, "N");
                        }
                        else
                        {
                            fcAdicionarItemPendencia(reserva.Itens[i].LivId, aluno.AluId.ToString());
                        }
                    }

                    reserva.Limpar();
                    HttpContext.Current.Session["RESERVA"] = null;
                    booRetorno = resId.ToString(); ;
                }
            }
            return booRetorno;
        }

        public string ReservarItem(string livId)
        {
            clsControleAluno objAluno = (clsControleAluno)HttpContext.Current.Session["ALUNO"];
            string strRetorno = "";

            if (!livId.Equals(""))
            {
                int intResposta = fcAdicionarItem(livId, objAluno.AluId.ToString());

                if (intResposta.Equals(0))
                {
                    //strRetorno = "Item já inserido anteriomente!";
                    strRetorno = "0";
                }
                else if (intResposta.Equals(1))
                {
                    //strRetorno = "Item inserido com sucesso!";
                    strRetorno = "1";
                }
                else if (intResposta.Equals(2))
                {
                    //strRetorno = "Quantidade de itens de reserva excedido!";
                    strRetorno = "2";
                }
            }
            return strRetorno;
        }

        private int fcAdicionarItem(string livId, string aluId)
        {
            bool booExiste = false;
            // define erro, já existe o item
            int intRetorno = 0;

            if (HttpContext.Current.Session["RESERVA"] != null)
            {
                // Pega a reserva atual da Sessão
                Reserva reserva = (Reserva)HttpContext.Current.Session["RESERVA"];

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
                
                if (!booExiste)
                {
                    if (reserva.Itens.Count < 3)
                    {
                        reserva.AdicionarItem(livId, aluId);
                        // retorno de sucesso
                        intRetorno = 1;
                    }
                    else
                    {
                        // retorno de erro, quantidade excedida
                        intRetorno = 2;
                    }
                }
            }

            return intRetorno;
        }

        private void fcAdicionarItemPendencia(string livId, string aluId)
        {
            bool booExiste = false;
            
            if (HttpContext.Current.Session["PENDENTES"] != null)
            {
                // Pega a pendencia atual da Sessão
                Reserva pendencia = (Reserva)HttpContext.Current.Session["PENDENTES"];

                if (pendencia.Itens.Count > 0)
                {
                    for (int i = 0; i < pendencia.Itens.Count; i++)
                    {
                        if (pendencia.Itens[i].LivId.Equals(livId))
                        {
                            booExiste = true;
                        }
                    }
                }

                if (!booExiste)
                {
                    pendencia.AdicionarItem(livId, aluId);
                }
            }
        }

        public bool fcRemoverItem(string livId)
        {
            bool booRetorno = false;

            if (HttpContext.Current.Session["RESERVA"] != null)
            {
                // Pega a reserva atual da Sessão
                Reserva reserva = (Reserva)HttpContext.Current.Session["RESERVA"];

                for (int i = 0; i < reserva.Itens.Count; i++)
                {
                    if (reserva.Itens[i].LivId.Equals(livId))
                    {
                        reserva.Itens.RemoveAt(i);
                        booRetorno = true;
                    }
                }
                HttpContext.Current.Session["RESERVA"] = reserva;
            }
            return booRetorno;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}