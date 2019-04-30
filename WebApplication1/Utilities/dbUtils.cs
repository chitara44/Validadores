using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.entities;
using System.Data;
using System.Text;


namespace WebApplication1.Utilities
{
    public class dbUtils
    {

        //public static int consultarParametros(int canal, string tipo)
        //{
        //    int gano = 0;
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
        //        {
        //            cn.Open();
        //            SqlCommand tsql = cn.CreateCommand();
        //            tsql.CommandText = "f_obtenerParametrosXcanal";
        //            tsql.CommandType = System.Data.CommandType.StoredProcedure;
        //            tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("canal", canal));
        //            using (SqlDataReader dr = tsql.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    if (dr.HasRows)
        //                    {
        //                        gano = Convert.ToInt32(dr["conteo"]);
        //                    }
        //                }
        //            }

        //            cn.Close();
        //        }
        //    }
        //    catch
        //    {
        //        gano = 0;
        //    }
        //    return gano;
        //}



        //public static int prInsertaSorteos(int idsorteo,  string numsTr, string numsRe, string fechaSor, bool winnerTr, bool winnerRe )
        //{
        //    int gano = 0;
        //    string trad = "Tr";

        //    string reva = "Re";
        //    string nuevo = "SI";
        //    string ganador = "SI";
        //    string noGanador = "NO";
        //    string wuinnerTr = "NO";
        //    string wuinnerRe = "NO";
        //    try
        //    {

        //        string[] lNumsTr = null;
        //        string[] lNumsRe = null;
        //        if (numsTr != String.Empty && numsRe != String.Empty)
        //        {
        //            lNumsTr = numsTr.Split(',');
        //            lNumsRe = numsRe.Split(',');
        //            wuinnerTr = (winnerTr) ? ganador : noGanador;
        //            wuinnerRe = (winnerRe) ? ganador : noGanador;
        //            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
        //            {
        //                cn.Open();
        //                SqlCommand tsql = cn.CreateCommand();
        //                tsql.CommandText = "spInsertarSorteo";
        //                tsql.CommandType = System.Data.CommandType.StoredProcedure;
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", idsorteo));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n1", lNumsTr[0].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n2", lNumsTr[1].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n3", lNumsTr[2].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n4", lNumsTr[3].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n5", lNumsTr[4].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("sb", lNumsTr[5].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("fechaSor", Convert.ToDateTime(fechaSor)));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", trad));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("winner", wuinnerTr.ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("nuevo", nuevo));
        //                tsql.ExecuteNonQuery();
        //                cn.Close();
        //            }
        //            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
        //            {
        //                cn.Open();
        //                SqlCommand tsql = cn.CreateCommand();
        //                tsql.CommandText = "spInsertarSorteo";
        //                tsql.CommandType = System.Data.CommandType.StoredProcedure;
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", idsorteo));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n1", lNumsRe[0].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n2", lNumsRe[1].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n3", lNumsRe[2].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n4", lNumsRe[3].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n5", lNumsRe[4].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("sb", lNumsRe[5].ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("fechaSor", Convert.ToDateTime(fechaSor)));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", reva));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("winner", wuinnerRe.ToString()));
        //                tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("nuevo", nuevo));
        //                tsql.ExecuteNonQuery();
        //                cn.Close();
        //            }
        //            gano = 1;
        //        }
        //    }
        //    catch
        //    {
        //        gano = 0;
        //    }
        //    return gano;
        //}

        //public static bool prInsertaCombos(List<sorteos> lSe)
        //{
        //    bool exito = false;
        //    string[] lNums = null;

        //    try
        //    {
        //        foreach (sorteos raffle in lSe)
        //        {
        //            foreach (string cad in raffle.Indi)
        //            {
        //                lNums = cad.Split(',');
        //                InsertarIndis(raffle, lNums);
        //            }
        //            foreach (string cad in raffle.Dupl)
        //            {
        //                lNums = cad.Split(',');
        //                InsertarDuplas(raffle, lNums);
        //            }
        //            foreach (string cad in raffle.Terc)
        //            {
        //                lNums = cad.Split(',');
        //                InsertarTernas(raffle, lNums);
        //            }
        //            foreach (string cad in raffle.Cuar)
        //            {
        //                lNums = cad.Split(',');
        //                InsertarCuartetos(raffle, lNums);
        //            }
        //            foreach (string cad in raffle.Quin)
        //            {
        //                lNums = cad.Split(',');
        //                InsertarQuintetos(raffle, lNums);
        //            }
        //            foreach (string cad in raffle.Sext)
        //            {
        //                lNums = cad.Split(',');
        //                InsertarSextetos(raffle, lNums);
        //            }
        //        }
        //        exito = true;
        //    }
        //    catch
        //    {
        //        exito = false;
        //    }
        //    return exito;
        //}

        public static int isValidUser(string usuario, string password)
        {
            int perfil = 0;
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                {
                    StringBuilder sb = new StringBuilder("SELECT AppVentasMovistar.dbo.f_isValidLogin(", 500);
                    sb.AppendFormat("N'{0}',N'{1}') as perfil", usuario, password);
                    string sqlSelect = sb.ToString();
                    SqlDataAdapter da = new SqlDataAdapter(sqlSelect, cn);
                    da.Fill(dt);
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName == "perfil")
                        {
                            perfil = Convert.ToInt16(dt.Rows[0][col].ToString());
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: ", ex.Message);
                perfil = 0;
            }
            return perfil;
        }

        public static DataTable consultaParams( string canal)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
            {
                StringBuilder sb = new StringBuilder("SELECT * FROM AppVentasMovistar.dbo.f_obtenerParametrosXcanal(", 500);
                sb.AppendFormat("N'{0}')", canal);
                string sqlSelect = sb.ToString();
                SqlDataAdapter da = new SqlDataAdapter(sqlSelect, cn);
                da.Fill(dt);
            }
            return dt;
        }

        public static int masRecienteSorteo(DataTable dt, int col)
        {
            int masReciente = 0;


            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                if (Convert.ToInt16(dt.Rows[i][col].ToString()) > masReciente)
                {
                    masReciente = Convert.ToInt16(dt.Rows[i][col].ToString());
                }
            }
            return masReciente;
        }
    }
}