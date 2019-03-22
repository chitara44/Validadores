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



        public static int isSorteoWinner(int idsorteo, string tipo)
        {
            int gano = 0;
            try
            {
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                {
                    cn.Open();
                    SqlCommand tsql = cn.CreateCommand();
                    tsql.CommandText = "spIsSorteoWinner";
                    tsql.CommandType = System.Data.CommandType.StoredProcedure;
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", idsorteo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", tipo));
                    using (SqlDataReader dr = tsql.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr.HasRows)
                            {
                                gano = Convert.ToInt32(dr["conteo"]);
                            }
                        }
                    }

                    cn.Close();
                }
            }
            catch
            {
                gano = 0;
            }
            return gano;
        }

        public static int prInsertaSorteos(int idsorteo,  string numsTr, string numsRe, string fechaSor, bool winnerTr, bool winnerRe )
        {
            int gano = 0;
            string trad = "Tr";
    
            string reva = "Re";
            string nuevo = "SI";
            string ganador = "SI";
            string noGanador = "NO";
            string wuinnerTr = "NO";
            string wuinnerRe = "NO";
            try
            {
                
                string[] lNumsTr = null;
                string[] lNumsRe = null;
                if (numsTr != String.Empty && numsRe != String.Empty)
                {
                    lNumsTr = numsTr.Split(',');
                    lNumsRe = numsRe.Split(',');
                    wuinnerTr = (winnerTr) ? ganador : noGanador;
                    wuinnerRe = (winnerRe) ? ganador : noGanador;
                    using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                    {
                        cn.Open();
                        SqlCommand tsql = cn.CreateCommand();
                        tsql.CommandText = "spInsertarSorteo";
                        tsql.CommandType = System.Data.CommandType.StoredProcedure;
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", idsorteo));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n1", lNumsTr[0].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n2", lNumsTr[1].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n3", lNumsTr[2].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n4", lNumsTr[3].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n5", lNumsTr[4].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("sb", lNumsTr[5].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("fechaSor", Convert.ToDateTime(fechaSor)));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", trad));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("winner", wuinnerTr.ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("nuevo", nuevo));
                        tsql.ExecuteNonQuery();
                        cn.Close();
                    }
                    using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                    {
                        cn.Open();
                        SqlCommand tsql = cn.CreateCommand();
                        tsql.CommandText = "spInsertarSorteo";
                        tsql.CommandType = System.Data.CommandType.StoredProcedure;
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", idsorteo));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n1", lNumsRe[0].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n2", lNumsRe[1].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n3", lNumsRe[2].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n4", lNumsRe[3].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n5", lNumsRe[4].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("sb", lNumsRe[5].ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("fechaSor", Convert.ToDateTime(fechaSor)));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", reva));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("winner", wuinnerRe.ToString()));
                        tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("nuevo", nuevo));
                        tsql.ExecuteNonQuery();
                        cn.Close();
                    }
                    gano = 1;
                }
            }
            catch
            {
                gano = 0;
            }
            return gano;
        }

        public static bool prInsertaCombos(List<sorteos> lSe)
        {
            bool exito = false;
            string[] lNums = null;

            try
            {
                foreach (sorteos.Se sor in lSe)
                {
                    var indi = new sorteos.Si();
                    var dupl = new sorteos.Du();
                    var terc = new sorteos.Te();
                    var cuar = new sorteos.Cu();
                    var quin = new sorteos.Qu();


                    foreach (string cad in indi.Indi)
                    {
                        lNums = cad.Split(',');
                        InsertarIndis(sor, lNums);
                    }
                    foreach (string cad in dupl.Dupl)
                    {
                        lNums = cad.Split(',');
                        InsertarDuplas(sor, lNums);
                    }
                    foreach (string cad in terc.Terc)
                    {
                        lNums = cad.Split(',');
                        InsertarTernas(sor, lNums);
                    }
                    foreach (string cad in cuar.Cuar)
                    {
                        lNums = cad.Split(',');
                        InsertarCuartetos(sor, lNums);
                    }
                    foreach (string cad in quin.Quin)
                    {
                        lNums = cad.Split(',');
                        InsertarQuintetos(sor, lNums);
                    }
                }
                exito = true;
            }
            catch
            {
                exito = false;
            }
            return exito;
        }

        private static bool InsertarIndis(sorteos se, string[] lNums)
        {
            bool inserto = false;
            try {
                var indi = new sorteos.Si();
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                {
                    cn.Open();
                    SqlCommand tsql = cn.CreateCommand();
                    tsql.CommandText = "spInsertarIndi";
                    tsql.CommandType = System.Data.CommandType.StoredProcedure;
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", indi.IdLastSorteo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n1", lNums[0].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("fechaSor", Convert.ToDateTime(se.FechaSorteo)));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", se.Tipo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("winner", se.Winner.ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("nuevo", utilidades.sorteoNuevo(indi.IdLastSorteo)));
                    tsql.ExecuteNonQuery();
                    cn.Close();
                    inserto = true;
                }
            }
            catch
            {
                inserto = false;
            }
            return inserto;
        }

        private static bool InsertarDuplas(sorteos se, string[] lNums)
        {
            bool inserto = false;
            try
            {
                var dupl = new sorteos.Du();
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                {
                    cn.Open();
                    SqlCommand tsql = cn.CreateCommand();
                    tsql.CommandText = "spInsertarDupla";
                    tsql.CommandType = System.Data.CommandType.StoredProcedure;
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", dupl.IdLastSorteo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n1", lNums[0].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n2", lNums[1].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("fechaSor", Convert.ToDateTime(se.FechaSorteo)));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", se.Tipo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("winner", se.Winner.ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("nuevo", utilidades.sorteoNuevo(dupl.IdLastSorteo)));
                    tsql.ExecuteNonQuery();
                    cn.Close();
                    inserto = true;
                }
            }
            catch
            {
                inserto = false;
            }
            return inserto;
        }

        private static bool InsertarTernas(sorteos se, string[] lNums)
        {
            bool inserto = false;
            try
            {
                var terc = new sorteos.Te();
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                {
                    cn.Open();
                    SqlCommand tsql = cn.CreateCommand();
                    tsql.CommandText = "spInsertarTerna";
                    tsql.CommandType = System.Data.CommandType.StoredProcedure;
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", terc.IdLastSorteo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n1", lNums[0].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n2", lNums[1].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n3", lNums[2].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("fechaSor", Convert.ToDateTime(se.FechaSorteo)));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", se.Tipo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("winner", se.Winner.ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("nuevo", utilidades.sorteoNuevo(terc.IdLastSorteo)));
                    tsql.ExecuteNonQuery();
                    cn.Close();
                    inserto = true;
                }
            }
            catch
            {
                inserto = false;
            }
            return inserto;
        }

        private static bool InsertarCuartetos(sorteos se, string[] lNums)
        {
            bool inserto = false;
            try
            {
                var cuar = new sorteos.Cu();
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                {
                    cn.Open();
                    SqlCommand tsql = cn.CreateCommand();
                    tsql.CommandText = "spInsertarCuarteto";
                    tsql.CommandType = System.Data.CommandType.StoredProcedure;
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", cuar.IdLastSorteo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n1", lNums[0].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n2", lNums[1].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n3", lNums[2].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n4", lNums[3].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("fechaSor", se.FechaSorteo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", se.Tipo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("winner", se.Winner));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("nuevo", utilidades.sorteoNuevo(cuar.IdLastSorteo)));
                    tsql.ExecuteNonQuery();
                    cn.Close();
                    inserto = true;
                }
            }
            catch
            {
                inserto = false;
            }
            return inserto;
        }

        private static bool InsertarQuintetos(sorteos se, string[] lNums)
        {
            bool inserto = false;
            try
            {
                var quin = new sorteos.Qu();
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                {
                    cn.Open();
                    SqlCommand tsql = cn.CreateCommand();
                    tsql.CommandText = "spInsertarQuinteto";
                    tsql.CommandType = System.Data.CommandType.StoredProcedure;
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", quin.IdLastSorteo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n1", lNums[0].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n2", lNums[1].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n3", lNums[2].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n4", lNums[3].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n5", lNums[4].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("fechaSor", Convert.ToDateTime(se.FechaSorteo)));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", se.Tipo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("winner", se.Winner.ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("nuevo", utilidades.sorteoNuevo(quin.IdLastSorteo)));
                    tsql.ExecuteNonQuery();
                    cn.Close();
                    inserto = true;
                }
            }
            catch
            {
                inserto = false;
            }
            return inserto;
        }

        private static bool InsertarSextetos(sorteos se, string[] lNums)
        {
            bool inserto = false;
            try
            {
                var sext = new sorteos.Se();
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                {
                    cn.Open();
                    SqlCommand tsql = cn.CreateCommand();
                    tsql.CommandText = "spInsertarSexteto";
                    tsql.CommandType = System.Data.CommandType.StoredProcedure;
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", sext.IdLastSorteo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n1", lNums[0].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n2", lNums[1].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n3", lNums[2].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n4", lNums[3].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n5", lNums[4].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("n6", lNums[5].ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("fechaSor", Convert.ToDateTime(se.FechaSorteo)));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", se.Tipo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("winner", se.Winner.ToString()));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("nuevo", utilidades.sorteoNuevo(sext.IdLastSorteo)));
                    tsql.ExecuteNonQuery();
                    cn.Close();
                    inserto = true;
                }
            }
            catch
            {
                inserto = false;
            }
            return inserto;
        }


        public static sorteos GetSorteoValues(int idsorteo, string tipo)
        {
            sorteos sorteo = new sorteos();
            try
            {
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                {
                    cn.Open();
                    SqlCommand tsql = cn.CreateCommand();
                    tsql.CommandText = "spGetSorteoValues";
                    tsql.CommandType = System.Data.CommandType.StoredProcedure;
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("idSorteo", idsorteo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", tipo));
                    using (SqlDataReader dr = tsql.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            sorteo.IdSorteo = Convert.ToInt32(dr["idSorteo"]);
                            sorteo.Num1 = Convert.ToInt32(dr["n1"]);
                            sorteo.Num2 = Convert.ToInt32(dr["n2"]);
                            sorteo.Num3 = Convert.ToInt32(dr["n3"]);
                            sorteo.Num4 = Convert.ToInt32(dr["n4"]);
                            sorteo.Num5 = Convert.ToInt32(dr["n5"]);
                            sorteo.Sb = Convert.ToInt32(dr["sb"]);
                            sorteo.Fecha = Convert.ToDateTime(dr["fecha"]);
                            sorteo.Tipo = Convert.ToString(dr["tipo"]);
                            sorteo.Winner = Convert.ToString(dr["ganador"]);
                            sorteo.Nuevo = Convert.ToString(dr["nuevo"]);
                        }
                    }

                    cn.Close();
                }
            }
            catch
            {
                sorteo = null;
            }
            return sorteo;
        }

        public static List<sorteos.Si> spRecorreNumerosCaidos(string tipo, string clase, List<sorteos.Si> listica)
        {
            sorteos.Si numero = new sorteos.Si();
            try
            {
                using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
                {
                    cn.Open();
                    SqlCommand tsql = cn.CreateCommand();
                    tsql.CommandText = "spRecorreNumerosCaidos";
                    tsql.CommandType = System.Data.CommandType.StoredProcedure;
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("tipo", tipo));
                    tsql.Parameters.Add(new System.Data.SqlClient.SqlParameter("clase", clase));
                    using (SqlDataReader dr = tsql.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (dr.HasRows)
                            {
                                numero.Num1 = Convert.ToInt32(dr["numero"]);
                                numero.CantCaidos = Convert.ToInt32(dr["veces"]);

                                listica.Add(numero);
                            }
                        }
                    }

                    cn.Close();
                }
            }
            catch
            {
                listica = null;
            }
            return listica;
        }

        public static DataTable consultaCoincidentes(string tipo, string nuevo, string busca)
        {
            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(Properties.Settings.Default.con))
            {
                StringBuilder sb = new StringBuilder("SELECT * FROM pruebitasionic.dbo.f_Coincidentes(", 500);
                sb.AppendFormat("N'{0}',N'{1}', N'{2}')", tipo, nuevo, busca);
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