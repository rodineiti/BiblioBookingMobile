using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

namespace BiblioBookingMobile.classe
{
    public class clsRotinas
    {
        public StringBuilder GetGenericListView(DataTable objDataTable, string strTituloColuna, string[] arrConteudoColunas, string strPaginaRedirect, string strColunaPK, string strTipoLink)
        {
            StringBuilder stbLista = new StringBuilder();

            try
            {
                stbLista.AppendLine("<ul data-role=\"listview\" data-filter=\"true\">");

                if (strTipoLink.Trim().ToUpper().Equals("DELETE"))
                {
                    for (int i = 0; i < objDataTable.Rows.Count; i++)
                    {
                        string strDelimitador = "#";
                        if (!String.IsNullOrEmpty(strPaginaRedirect))
                        {
                            // PK
                            for (int x = 0; x < objDataTable.Columns.Count; x++)
                            {
                                if (objDataTable.Columns[x].ColumnName.ToUpper().Equals(strColunaPK))
                                {
                                    strDelimitador = String.Format("{0}?id={1}", strPaginaRedirect, objDataTable.Rows[i].ItemArray[x].ToString());
                                }
                            }
                        }

                        stbLista.AppendLine(String.Format("<li data-icon=\"delete\"><a href=\"{0}\" role=\"button\" class=\"ui-btn ui-shadow ui-corner-all\" data-rel=\"dialog\" data-transition=\"pop\">", strDelimitador));
                        stbLista.AppendLine("<table>");
                        stbLista.AppendLine("<tr>");
                        stbLista.AppendLine("<td style=\"text-align: left; padding-left: 5px;\">");

                        // Title
                        for (int x = 0; x < objDataTable.Columns.Count; x++)
                        {
                            if (objDataTable.Columns[x].ColumnName.ToUpper().Equals(strTituloColuna))
                            {
                                stbLista.AppendLine(String.Format("<h3>{0}</h3>", objDataTable.Rows[i].ItemArray[x].ToString()));
                            }
                        }

                        // Content
                        for (int x = 0; x < objDataTable.Columns.Count; x++)
                        {
                            foreach (string pStrColumn in arrConteudoColunas)
                            {
                                if (objDataTable.Columns[x].ColumnName.ToUpper().Equals(pStrColumn))
                                {
                                    stbLista.AppendLine(String.Format("<p>{0}</p>", objDataTable.Rows[i].ItemArray[x].ToString()));
                                }
                            }
                        }

                        stbLista.AppendLine("</td>");
                        stbLista.AppendLine("</tr>");
                        stbLista.AppendLine("</table>");
                        stbLista.AppendLine("</a></li>");
                    }
                }
                else
                {
                    for (int i = 0; i < objDataTable.Rows.Count; i++)
                    {
                        string strDelimitador = "#";
                        if (!String.IsNullOrEmpty(strPaginaRedirect))
                        {
                            // PK
                            for (int x = 0; x < objDataTable.Columns.Count; x++)
                            {
                                if (objDataTable.Columns[x].ColumnName.ToUpper().Equals(strColunaPK))
                                {
                                    strDelimitador = String.Format("{0}?id={1}", strPaginaRedirect, objDataTable.Rows[i].ItemArray[x].ToString());
                                }
                            }
                        }

                        stbLista.AppendLine(String.Format("<li><a href=\"{0}\">", strDelimitador));
                        stbLista.AppendLine("<table>");
                        stbLista.AppendLine("<tr>");
                        stbLista.AppendLine("<td style=\"text-align: center; width: 60px;\">");
                        stbLista.AppendLine("<img src=\"/img/0001.png\" style=\"width: 60px;\" alt=\"Copérnico\">");
                        stbLista.AppendLine("</td>");
                        stbLista.AppendLine("<td style=\"text-align: left; padding-left: 5px;\">");

                        // Title
                        for (int x = 0; x < objDataTable.Columns.Count; x++)
                        {
                            if (objDataTable.Columns[x].ColumnName.ToUpper().Equals(strTituloColuna))
                            {
                                stbLista.AppendLine(String.Format("<h3>{0}</h3>", objDataTable.Rows[i].ItemArray[x].ToString()));
                            }
                        }

                        // Content
                        for (int x = 0; x < objDataTable.Columns.Count; x++)
                        {
                            foreach (string pStrColumn in arrConteudoColunas)
                            {
                                if (objDataTable.Columns[x].ColumnName.ToUpper().Equals(pStrColumn))
                                {
                                    stbLista.AppendLine(String.Format("<p>{0}</p>", objDataTable.Rows[i].ItemArray[x].ToString()));
                                }
                            }
                        }

                        stbLista.AppendLine("</td>");
                        stbLista.AppendLine("</tr>");
                        stbLista.AppendLine("</table>");
                        stbLista.AppendLine("</a></li>");
                    }
                }

                stbLista.AppendLine("</ul>");
            }
            catch (Exception ex)
            {
                stbLista = new StringBuilder("Nenhum livro adicionado para reserva.");
            }

            return stbLista;
        }
    }
}