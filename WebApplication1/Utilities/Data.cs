using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Tmc.Servicios.FullStack
{
    public class Data
    {
        /// <summary>
        /// Cadena de conexión a la base de datos
        /// </summary>
        /// <remarks>
        /// Está registrada en el archivo Web.config
        /// </remarks>
        //private static string ConnectionString
        //{
        //    get
        //    {
        //        return Properties.Settings.Default.cnPrepagos;
        //    }
        //}

        /// <summary>
        /// Obtiene un valor de configuración.
        /// </summary>
        /// <param name="Clave">Clave a consultar.</param>
        /// <returns></returns>
        //public static Types.Common.StringResult GetConfigValue(string Clave)
        //{
        //    Types.Common.StringResult @out = new Types.Common.StringResult();
        //    try
        //    {
        //        using (SqlConnection cn = new SqlConnection(ConnectionString))
        //        {
        //            cn.Open();
        //            SqlCommand tsql = cn.CreateCommand();
        //            tsql.CommandText = "spGetFsConfigValue";
        //            tsql.CommandType = System.Data.CommandType.StoredProcedure;
        //            tsql.Parameters.Add(new SqlParameter("Clave", Clave));
        //            @out.Resultado = tsql.ExecuteScalar().ToString();
        //            @out.Operacion.AffectedRows += 1;
        //            cn.Close();
        //        }
        //        @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
        //    }
        //    catch (Exception ex)
        //    {
        //        @out.Operacion = new Types.Common.ResultadoOperacion(ex);
        //        AddException(new Types.Tablas.Exception() { Modulo = "GetConfigValue", Detalle = ex.ToString() });
        //    }
        //    return @out;
        //}

        //public static Types.Services.GetParametroCollectionResult GetConfigValues(string Clave)
        //{
        //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Inicio de la secuencia", P1 = Clave });
        //    Types.Services.GetParametroCollectionResult @out = new Types.Services.GetParametroCollectionResult();
        //    @out.Items = new Types.Tablas.ParametroCollection();
        //    try
        //    {
        //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Ingresando al Try" });

        //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Definiendo conexión con la BD" });
        //        using (SqlConnection cn = new SqlConnection(ConnectionString))
        //        {
        //            Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Abriendo conexión con la BD" });
        //            cn.Open();
        //            Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Definiendo comando TSQL" });
        //            SqlCommand tsql = cn.CreateCommand();
        //            tsql.CommandText = "spGetFsConfigValues";
        //            tsql.CommandType = System.Data.CommandType.StoredProcedure;
        //            tsql.Parameters.Add(new SqlParameter("Clave", Clave));
        //            Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Ejecutando comando TSQL" });
        //            using (SqlDataReader rs = tsql.ExecuteReader())
        //            {
        //                Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Obteniendo datos" });
        //                while (rs.Read())
        //                {
        //                    Types.Tablas.Parametro item = new Types.Tablas.Parametro();
        //                    item.Clave = rs["Clave"].ToString();
        //                    item.Valor = rs["Valor"].ToString();
        //                    @out.Operacion.AffectedRows += 1;
        //                    @out.Items.Add(item);
        //                    Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: " + item.Clave, P1 = item.Valor });

        //                }
        //            }
        //            Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Cerando conexión con la BD" });
        //            cn.Close();
        //        }
        //        @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Ocurrió un error" });
        //        AddException(new Types.Tablas.Exception() { Modulo = "Data.GetConfigValues", Detalle = ex.ToString() });
        //        @out.Operacion = new Types.Common.ResultadoOperacion(ex);
        //        AddException(new Types.Tablas.Exception() { Modulo = "GetConfigValues", Detalle = ex.ToString() });
        //    }
        //    return @out;
        //}

        //public static Types.Common.ServiceResult AddException(Types.Tablas.Exception input)
        //{
        //    Types.Common.ServiceResult @out = new Types.Common.ServiceResult();
        //    try
        //    {
        //        if (input.AppId == 0) input.AppId = Utils.AppId;
        //        input.Fecha = DateTime.Now;
        //        using (SqlConnection cn = new SqlConnection(ConnectionString))
        //        {
        //            cn.Open();
        //            SqlCommand tsql = cn.CreateCommand();
        //            tsql.CommandText = "spAddException";
        //            tsql.CommandType = System.Data.CommandType.StoredProcedure;
        //            tsql.Parameters.Add(new SqlParameter("AppId", input.AppId));
        //            tsql.Parameters.Add(new SqlParameter("Modulo", input.Modulo));
        //            tsql.Parameters.Add(new SqlParameter("Detalle", input.Detalle));
        //            @out.Operacion.AffectedRows = tsql.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //        @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
        //    }
        //    catch (Exception ex)
        //    {
        //        @out.Operacion = new Types.Common.ResultadoOperacion(ex);
        //        // TODO: Registro de erroes por archivo log y Visor de sucesos de Windows para contingencias.
        //    }
        //    return @out;
        //}

        //public static Types.Common.ServiceResult AddTrace(Types.Tablas.Trace input)
        //{
        //    Types.Common.ServiceResult @out = new Types.Common.ServiceResult();
        //    try
        //    {
        //        // Si no lo especificamos, el AppId es el del servicio.
        //        if (input.AppId == 0) input.AppId = Utils.AppId;
        //        // Ponemos la fecha actual al registro.
        //        input.Fecha = DateTime.Now;
        //        using (SqlConnection cn = new SqlConnection(ConnectionString))
        //        {
        //            cn.Open();
        //            SqlCommand tsql = cn.CreateCommand();
        //            tsql.CommandText = "spAddTrace";
        //            tsql.CommandType = System.Data.CommandType.StoredProcedure;
        //            tsql.Parameters.Add(new SqlParameter("AppId", input.AppId));
        //            tsql.Parameters.Add(new SqlParameter("Accion", input.Accion));
        //            tsql.Parameters.Add(new SqlParameter("P1", input.P1));
        //            tsql.Parameters.Add(new SqlParameter("P2", input.P2));
        //            tsql.Parameters.Add(new SqlParameter("P3", input.P3));
        //            tsql.Parameters.Add(new SqlParameter("P4", input.P4));
        //            tsql.Parameters.Add(new SqlParameter("P5", input.P5));
        //            tsql.Parameters.Add(new SqlParameter("P6", input.P6));
        //            tsql.Parameters.Add(new SqlParameter("P7", input.P7));
        //            tsql.Parameters.Add(new SqlParameter("P8", input.P8));
        //            @out.Operacion.AffectedRows = tsql.ExecuteNonQuery();
        //            cn.Close();
        //        }
        //        @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
        //    }
        //    catch (Exception ex)
        //    {
        //        @out.Operacion = new Types.Common.ResultadoOperacion(ex);
        //    }
        //    return @out;
        //}

        private static string GetExecId
        {
            get
            {
                string @out = "";
                @out += DateTime.Now.ToString("yyyy");
                @out += "a";
                @out += DateTime.Now.ToString("MM");
                @out += "b-c";
                @out += DateTime.Now.ToString("dd");
                @out += "d-e";
                @out += DateTime.Now.ToString("HH");
                @out += "f-a";
                @out += DateTime.Now.ToString("mm");
                @out += "b-";
                @out += DateTime.Now.ToString("yyyyMM");
                @out += DateTime.Now.ToString("HHmmss");
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado un nuevo ExecId", P1 = @out });
                return @out;
            }
        }

        private static string GetTimestamp
        {
            get
            {
                string @out = "";
                @out += DateTime.Now.ToString("yyyy-MM-dd");
                @out += "T";
                @out += DateTime.Now.ToString("HH:mm:ss");
                @out += ".000+05:00";
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado un nuevo Timestamp", P1 = @out });
                return @out;
            }
        }

        //public static string GetSoapHeader(string operation)
        //{
        //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando el encabezado de la petición SOA para el método " + operation });
        //    string @out = "";
        //    try
        //    {
        //        Types.Services.GetParametroCollectionResult parametros = GetConfigValues("SoaHeader");
        //        @out = parametros.Values("SoaHeaderTemplate");
        //        @out = @out.Replace("::0::", parametros.Values("SoaHeaderCountry"));
        //        @out = @out.Replace("::1::", parametros.Values("SoaHeaderLang"));
        //        @out = @out.Replace("::2::", parametros.Values("SoaHeaderEntity"));
        //        @out = @out.Replace("::3::", parametros.Values("SoaHeaderSystem"));
        //        @out = @out.Replace("::4::", parametros.Values("SoaHeaderSubsystem"));
        //        @out = @out.Replace("::5::", parametros.Values("SoaHeaderUserId"));
        //        @out = @out.Replace("::6::", operation);
        //        @out = @out.Replace("::7::", GetExecId);
        //        @out = @out.Replace("::8::", GetTimestamp);
        //        @out = @out.Replace("::9::", parametros.Values("SoaHeaderArgUser"));
        //        @out = @out.Replace("::10::", parametros.Values("SoaHeaderArgPassword"));
        //        @out = @out.Replace("::11::", parametros.Values("SoaHeaderArgChannel"));
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error al crear el encabezado de la petición SOA para el método " + operation + ". Consulte la tabla de errores para mayor información." });
        //        @out = "<!-- " + ex.Message + "-->";
        //        //AddException(new Types.Tablas.Exception() { Modulo = "Data.GetSoapHeader", Detalle = ex.ToString() });
        //    }
        //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado el encabezado de la petición SOA para el método " + operation, P1 = @out });
        //    return @out;
        //}
    }
}