using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using WebApplication1.entities;
using System.Text;
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
        public bool consultaganador(int idSorteo, string tipo)
        {
            bool fueganador = false;
            int sorteo = dbUtils.isSorteoWinner(idSorteo, tipo);
            if (sorteo > 0)
            {
                fueganador = true;
            }
            return fueganador;
        }

        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public bool prInsertaSorteo(int idSorteo, string numsTr, string numsRe, DateTime fecha, bool winnerTr, bool winnerRe )
        {
            List<sorteos> LisSor = new List<sorteos>();
            utilidades.tipos Tr = utilidades.tipos.Tr;
            bool insercionOK = false;
            string fechaS = fecha.Date.ToString();
            int sorteo = 0;
            sorteo = dbUtils.prInsertaSorteos(idSorteo, numsTr, numsRe, fechaS, winnerTr, winnerRe);
            if (sorteo >= 0)
            {
                insercionOK = true;
                sorteos draw = new sorteos();
                for (int i = 0; i <= 1; i++)
                {
                    if (Tr == utilidades.tipos.Tr && i == Convert.ToInt32(Tr))
                    {
                        draw = dbUtils.GetSorteoValues(idSorteo, "Tr");
                    }
                    else
                    {
                        draw = dbUtils.GetSorteoValues(idSorteo, "Re");
                    }
                    draw.Listas();
                    LisSor.Add(draw);
                }
                dbUtils.prInsertaCombos(LisSor);
            }
            return insercionOK;
        }


        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public sorteos consultaSorteo(int idSorteo, string tipo)
        {
            sorteos sorteo = dbUtils.GetSorteoValues(idSorteo, tipo);
            return sorteo;
        }

        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public List<sorteos.Si> contadorSingular(string tipo, string nuevo, int n1)
        {
            List<sorteos.Si> lista = new List<sorteos.Si>();
            List<sorteos.Se> sextes = new List<sorteos.Se>();
            sorteos.Se sexte = new sorteos.Se();
            sexte.Num1 = n1;
            sextes = utilidades.spCoincidentes(tipo, nuevo, sexte);
            utilidades.sextetos_singles(sextes, lista);
            return lista;
        }

        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public List<sorteos.Du> contadorDuplas(string tipo, string nuevo, int n1, int n2)
        {
            List<sorteos.Du> duplas = new List<sorteos.Du>();
            List<sorteos.Se> sextes = new List<sorteos.Se>();
            sorteos.Se sexte = new sorteos.Se();
            sexte.Num1 = n1;
            sexte.Num2 = n2;
            sextes = utilidades.spCoincidentes(tipo, nuevo, sexte);
            utilidades.sextetos_duplas(sextes,duplas);
            return duplas;
        }

        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public List<sorteos.Te> contadorTernas(string tipo, string nuevo, int n1, int n2, int n3)
        {
            List<sorteos.Te> ternas = new List<sorteos.Te>();
            List<sorteos.Se> sextes = new List<sorteos.Se>();
            sorteos.Se sexte = new sorteos.Se();

            sorteos.Du duplita = new sorteos.Du();
            sexte.Num1 = n1;
            sexte.Num2 = n2;
            sexte.Num3 = n3;
            sextes = utilidades.spCoincidentes(tipo, nuevo, sexte);
            utilidades.sextetos_ternas(sextes, ternas);
            return ternas;
        }

        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public List<sorteos.Cu> contadorCuartetos(string tipo, string nuevo, int n1, int n2, int n3, int n4)
        {
            List<sorteos.Cu> cuartetos = new List<sorteos.Cu>();
            List<sorteos.Se> sextes = new List<sorteos.Se>();
            sorteos.Se sexte = new sorteos.Se();

            sorteos.Du duplita = new sorteos.Du();
            sexte.Num1 = n1;
            sexte.Num2 = n2;
            sexte.Num3 = n3;
            sexte.Num4 = n4;
            sextes = utilidades.spCoincidentes(tipo, nuevo, sexte);
            utilidades.sextetos_cuartetos(sextes, cuartetos);
            return cuartetos;
        }

        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public List<sorteos.Qu> contadorQuintetos(string tipo, string nuevo, int n1, int n2, int n3, int n4, int n5)
        {
            List<sorteos.Qu> quintetos = new List<sorteos.Qu>();
            List<sorteos.Se> sextes = new List<sorteos.Se>();
            sorteos.Se sexte = new sorteos.Se();

            sorteos.Du duplita = new sorteos.Du();
            sexte.Num1 = n1;
            sexte.Num2 = n2;
            sexte.Num3 = n3;
            sexte.Num4 = n4;
            sexte.Num5 = n5;
            sextes = utilidades.spCoincidentes(tipo, nuevo, sexte);
            utilidades.sextetos_quintetos(sextes, quintetos);
            return quintetos;
        }

        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public List<sorteos.Se> contadorSextetos(string tipo, string nuevo, int n1, int n2, int n3, int n4, int n5, int n6)
        {
            List<sorteos.Se> sextetos = new List<sorteos.Se>();
            List<sorteos.Se> sextes = new List<sorteos.Se>();
            sorteos.Se sexte = new sorteos.Se();

            sorteos.Du duplita = new sorteos.Du();
            sexte.Num1 = n1;
            sexte.Num2 = n2;
            sexte.Num3 = n3;
            sexte.Num4 = n4;
            sexte.Num5 = n5;
            sextes = utilidades.spCoincidentes(tipo, nuevo, sexte);
            //Utils.sextetos_quintetos(sextes, quintetos);
            return sextes;
        }

        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public bool llenarCombos(int sorteoIni, int sorteoFin)
        {
            List<sorteos> lSorteos = new List<sorteos>();
            bool llenos = false;
            for (int i = sorteoIni; i<= sorteoFin; i++)
            {
                sorteos.Se s = new sorteos.Se();
                sorteos sorteo = new sorteos();
                sorteo = consultaSorteo(i, "Tr");

                lSorteos.Add(sorteo);

            }
            

            dbUtils.prInsertaCombos(lSorteos);
            return llenos;
        }

        [WebInvoke(
        Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json
        )]
        public bool poblarTablas(int sorteoIni, int sorteoFin)
        {
            List<sorteos> LisSor = new List<sorteos>();
            utilidades.tipos Tr = utilidades.tipos.Tr;
            bool insercionOK = false;
            try
            {
                for (int ii = sorteoIni; ii <= sorteoFin; ii++)
                {
                    insercionOK = false;
                    sorteos draw = new sorteos();
                    for (int i = 0; i <= 1; i++)
                    {
                        if (Tr == utilidades.tipos.Tr && i == Convert.ToInt32(Tr))
                        {
                            draw = dbUtils.GetSorteoValues(ii, "Tr");
                        }
                        else
                        {
                            draw = dbUtils.GetSorteoValues(ii, "Re");
                        }
                        draw.Listas();
                        LisSor.Add(draw);
                    }

                }
                dbUtils.prInsertaCombos(LisSor);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                insercionOK = false;
            }
            int cant = (sorteoFin - sorteoIni) * 2;
            if (LisSor.Count.Equals(cant))
            {
                insercionOK = true;
            }

            return insercionOK;
        }
    }
}
