
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Tmc.Servicios.FullStack
{
    public class Utils
    {
        /// <summary>
        /// Valor predeterminado para resultados nulos.
        /// </summary>
        public static string NothingValue { get { return "#N/A"; } }
        /// <summary>
        /// Obtiene el identificador de la aplicación.
        /// </summary>
        //public static int AppId { get { return Convert.ToInt32(ConfigurationManager.AppSettings["AppId"]); } }
        /// <summary>
        /// Indica si el servicio se encuentra en modo Fake o en modo de consumo de datos.
        /// </summary>
        //public static bool IsDevMode { get { return (Data.GetConfigValue("FsDevMode").Resultado == "1") ? true : false; } }

        /// <summary>
        /// Obtiene el tipo de documento de indentidad para enviar a FullStack
        /// </summary>
        /// <param name="tipident">Tipo de documento de identidad recibido</param>
        /// <returns></returns>
        public static string GetFsTipIdent(string tipident)
        {
            Console.WriteLine(new Types.Tablas.Trace("Obteniendo TipIdent FS para " + tipident));
            string @out = "";
            bool encontrado = false;
            
            /* Primero, validamos si es Cédula de ciudadanía */
            //string[] sclTipIdent = Data.GetConfigValue("SclTipIdentCC").Resultado.Split(',');
            //foreach (string tipo in sclTipIdent)
            //{
            //    if (tipident == tipo)
            //    {
            //        @out = Data.GetConfigValue("FsTipIdentCC").Resultado;
            //        encontrado = true;
            //        break;
            //    }
            //}

            /* Segundo, validamos si es cédula de extranjería */
            //if (!encontrado)
            //{
            //    sclTipIdent = Data.GetConfigValue("SclTipIdentCE").Resultado.Split(',');
            //    foreach (string tipo in sclTipIdent)
            //    {
            //        if (tipident == tipo)
            //        {
            //            @out = Data.GetConfigValue("FsTipIdentCE").Resultado;
            //            encontrado = true;
            //            break;
            //        }
            //    }
            //}

            /* Tercero, validamos si es pasaporte */
            //if (!encontrado)
            //{
            //    sclTipIdent = Data.GetConfigValue("SclTipIdentPA").Resultado.Split(',');
            //    foreach (string tipo in sclTipIdent)
            //    {
            //        if (tipident == tipo)
            //        {
            //            @out = Data.GetConfigValue("FsTipIdentPA").Resultado;
            //            encontrado = true;
            //            break;
            //        }
            //    }
            //}

            ///* Cuarto, validamos si es NIT */
            //if (!encontrado)
            //{
            //    sclTipIdent = Data.GetConfigValue("SclTipIdentNIT").Resultado.Split(',');
            //    foreach (string tipo in sclTipIdent)
            //    {
            //        if (tipident == tipo)
            //        {
            //            @out = Data.GetConfigValue("FsTipIdentNIT").Resultado;
            //            encontrado = true;
            //            break;
            //        }
            //    }
            //}

            ///* Finalmente, si todo falla, indicamos que es un tipo desconocido */
            //if (!encontrado)
            //{
            //    @out = Data.GetConfigValue("FsTipIdentUnknow").Resultado;
            //}

            Console.WriteLine(new Types.Tablas.Trace(tipident + " => " + @out));
            return @out;
        }

        /// <summary>
        /// Traduce el segmento obtenido en FullStack al término utilizado por SELU
        /// </summary>
        /// <param name="FsSegment">Nombre del segmento obtenido en FullStack</param>
        /// <returns></returns>
        //public static string GetSeluSegment(string FsSegment)
        //{
        //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Buscando el segmento SELU para " + FsSegment });
            //string @out = Data.GetConfigValue("SclSegmentosTradicional").Resultado;
            //bool encontrado = false;
            //Types.Services.GetParametroCollectionResult FsKeys = Data.GetConfigValues("FsSegmentos");
            //foreach (Types.Tablas.Parametro key in FsKeys.Items)
            //{
            //    if (!encontrado)
            //    {
            //        string[] parts = key.Valor.Split('|');
            //        foreach (string part in parts)
            //        {
            //            if (FsSegment.Trim().ToLower().Contains(part.Trim().ToLower()))
            //            {
            //                @out = Data.GetConfigValue(key.Clave.Replace("Fs", "Scl")).Resultado;
            //                Console.WriteLine(new Types.Tablas.Trace(FsSegment + " => " + @out));
            //                encontrado = true;
            //                break;
            //            }
            //        }
            //    }
            //    else
            //        break;
            //}
            //return @out;
        //}

        /// <summary>
        /// Traduce el status de pago obtenido en FullStack al término utilizado por SEL
        /// </summary>
        /// <param name="FsStatus">Nombre del status obtenido en FullStack</param>
        /// <returns></returns>
        //public static string GetSelStatus(string FsStatus)
        //{
        //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Buscando el status SEL para " + FsStatus });
        //    string @out = Data.GetConfigValue("SelPaymentStatusUnknown").Resultado;
        //    bool encontrado = false;
        //    Types.Services.GetParametroCollectionResult FsKeys = Data.GetConfigValues("FsPaymentStatus");
        //    foreach (Types.Tablas.Parametro key in FsKeys.Items)
        //    {
        //        if (!encontrado)
        //        {
        //            string[] parts = key.Valor.Split('|');
        //            foreach (string part in parts)
        //            {
        //                if (FsStatus.Trim().ToLower().Contains(part.Trim().ToLower()))
        //                {
        //                    @out = Data.GetConfigValue(key.Clave.Replace("Fs", "Sel")).Resultado;
        //                    Console.WriteLine(new Types.Tablas.Trace(FsStatus + " => " + @out));
        //                    encontrado = true;
        //                    break;
        //                }
        //            }
        //        }
        //        else
        //            break;
        //    }
        //    return @out;
        //}

        /// <summary>
        /// Busca un texto en un array
        /// </summary>
        /// <param name="filter">Texto a buscar</param>
        /// <param name="array">Array donde buscar</param>
        /// <returns></returns>
        public static bool FindStringInArray(string filter, string[] array)
        {
            bool @out = false;
            foreach (string value in array)
            {
                if (filter == value)
                {
                    @out = true;
                    break;
                }
            }
            return @out;
        }

        /// <summary>
        /// WI49544 - Remover el digito verificador del NIT para enviar a FS
        /// </summary>
        /// <param name="numeroIdentidad"></param>
        /// <returns></returns>
        public static string QuitarDigitoVerificador(string numeroIdentidad)
        {
            try
            {
                string[] partes = numeroIdentidad.Split('-');
                return partes[0];
            }
            catch (Exception)
            {
                return numeroIdentidad;
                throw;
            }
        }
    }
}