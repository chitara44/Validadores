using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using WebApplication1.entities;
using System.Text;
using System.Data;
using System.ServiceModel.Web;
using WebApplication1.Utilities;

namespace WebApplication1
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "LuckyService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione LuckyService.svc o LuckyService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class LuckyService : ILuckyService
    {

        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public List<parametros> consultarparametros(string canal)
        {
            DataTable dt = new DataTable();
            List<parametros> parametros = new List<parametros>();
            
            dbUtils.consultaParams(canal);
            parametros = utilidades.ProcesarDT(dt);
            return parametros;
        }

        



        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public resValidaciones consultaEvidente(persona tipoPer)
        {
            string canal = "APPVENTAS";
            resValidaciones resVal = new resValidaciones();
            string[] paramsEV = null;
            DataTable dt = dbUtils.consultaParams(canal);
            List<parametros> lParams = new List<parametros>();
            //dbUtils.consultaParams(canal);
            lParams = utilidades.ProcesarDT(dt);
            paramsEV = utilidades.cargarParametrosEvidente(lParams);
            evidente 

            if (paramsEV != null)
            {
                resVal.Resultado = true;
            }
            Console.WriteLine(paramsEV);

            return resVal;
        }

        //[WebInvoke(
        //Method = "POST",
        //RequestFormat = WebMessageFormat.Json,
        //ResponseFormat = WebMessageFormat.Json
        //)]
        //public bool prInsertaSorteo(int idSorteo, string numsTr, string numsRe, DateTime fecha, bool winnerTr, bool winnerRe )
        //{
        //    List<sorteos> LisSor = new List<sorteos>();
        //    utilidades.tipos Tr = utilidades.tipos.Tr;
        //    bool insercionOK = false;
        //    string fechaS = fecha.Date.ToString();
        //    int sorteo = 0;
        //    sorteo = dbUtils.prInsertaSorteos(idSorteo, numsTr, numsRe, fechaS, winnerTr, winnerRe);
        //    if (sorteo >= 0)
        //    {
        //        insercionOK = true;
        //        sorteos draw = new sorteos();
        //        for (int i = 0; i <= 1; i++)
        //        {
        //            if (Tr == utilidades.tipos.Tr && i == Convert.ToInt32(Tr))
        //            {
        //                draw = dbUtils.GetSorteoValues(idSorteo, "Tr");
        //            }
        //            else
        //            {
        //                draw = dbUtils.GetSorteoValues(idSorteo, "Re");
        //            }
        //            draw.Listas();
        //            LisSor.Add(draw);
        //        }
        //        dbUtils.prInsertaCombos(LisSor);
        //    }
        //    return insercionOK;
        //}


       

       
    }
}
