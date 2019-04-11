using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using WebApplication1.entities;

namespace WebApplication1.Utilities
{
    public class utilidades
    {
        public enum tipos
        {
            Tr,
            Re
        }

        /// <summary>
        /// funcion para obtener el valor de algun parametro desde una lista
        /// </summary>
        /// <param name="paramList">Listado de parametros</param>
        /// <param name="nom_parametro">nombre del parametro a consultar</param>
        /// <returns>el valor del parametro</returns>
        public static string GetValorParametro(List<parametros> paramList, string nom_parametro)
        {
            parametros result = paramList.Find(x => x.Nom_Parametro == nom_parametro);
            return result.Val_Parametro;
        }


        /// <summary>
        /// se encarga de traer los parametros basicos de la consultas de Evidente.
        /// </summary>
        /// <param name="paramList">Lista de parametros de la base de datos</param>
        /// <returns></returns>
        public static string[] cargarParametrosEvidente(List<parametros> paramList)
        {
            string[] parametrosEV = new string[7];
            parametrosEV[0] = GetValorParametro(paramList, "PRODUCTOEV");
            parametrosEV[1] = GetValorParametro(paramList, "TIPIDUSUEV");
            parametrosEV[2] = GetValorParametro(paramList, "USUEVIDENTE");
            parametrosEV[3] = GetValorParametro(paramList, "NITMOVISTAREV");
            parametrosEV[4] = GetValorParametro(paramList, "CANALEV");
            parametrosEV[5] = GetValorParametro(paramList, "RESPVALIDAOK");
            parametrosEV[6] = GetValorParametro(paramList, "RESPPREGUNOK");
            return parametrosEV;
        }

        public static List<parametros> ProcesarDT(DataTable dt)
        {
            //parametros param = new parametros();
            List<parametros> lParams = new List<parametros>(); 
            int conteo = dt.Rows.Count;

            if (conteo > 0)
            {
                int i = 0;
                while (i < dt.Rows.Count)
                {
                    parametros param = new parametros();
                    param.Canal = Convert.ToString(dt.Rows[i]["Id_Canal"]);
                    param.Nom_Parametro = Convert.ToString(dt.Rows[i]["Nom_Parametro"]);
                    param.Val_Parametro = Convert.ToString(dt.Rows[i]["Val_Parametro"]);
                    param.Des_Parametro = Convert.ToString(dt.Rows[i]["Des_Parametro"]);
                    lParams.Add(param);
                    i++;
                }
            }
            else
            {
                Console.WriteLine("No logramos obtener parametros");
            }
            return lParams;
        }

        public static void CreateRequestEvidente()
        {
            StringBuilder soapBaseAns = new StringBuilder("<?xml version='1.0' encoding='UTF - 8'?>< Parametrizaciones >< Parametrizacion codigo = '2477' nombre = 'EVIDN-MOVIST-VENTA-INICL-PRUEBAS-FEB2012' />< Parametrizacion codigo = '2736' nombre = 'MOVISTAR CENTROS EXPERIENCIA' /></ Parametrizaciones >");

            StringBuilder soapBaseReq = new StringBuilder("<soapenv:Envelope xmlns:soapenv=http://schemas.xmlsoap.org/soap/envelope/ xmlns:int=http://ws.davox.telefonica.com.co/interfazSalesforce/>", 5000);
            soapBaseReq.Append("<soapenv:Header/><soapenv:Body><int:find><xml></xml></int:find></soapenv:Body></soapenv:Envelope>");

            StringBuilder consultarParametrizaciones = new StringBuilder("<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:ws='http://ws.id.dc.com'>< soapenv:Header />< soapenv:Body >");
            consultarParametrizaciones.Append("< ws:consultarParametrizaciones soapenv:encodingStyle = 'http://schemas.xmlsoap.org/soap/encoding/' >< producto xsi: type = 'soapenc:string' xmlns: soapenc = 'http://schemas.xmlsoap.org/soap/encoding/' >'{0}'</ producto >");
            consultarParametrizaciones.Append("< tipoIdentificacion xsi: type = 'soapenc:string' xmlns: soapenc = 'http://schemas.xmlsoap.org/soap/encoding/' > '{1}'</ tipoIdentificacion >");
            consultarParametrizaciones.Append("< entidad xsi: type = 'soapenc:string' xmlns: soapenc = 'http://schemas.xmlsoap.org/soap/encoding/' >'{2}'</ entidad >");
            consultarParametrizaciones.Append("</ ws:consultarParametrizaciones ></ soapenv:Body ></ soapenv:Envelope > ");
        }


    //    Public Shared Function GeneraXMLSaldoDetalleSEL(ByVal USER As String, ByVal PASSWORD As String, ByVal fecha As String, ByVal packcode As String, ByVal MSIDN As List(Of String), ByVal MetodoUrl As String, ByVal urlServicio As String, Optional ByVal Conteo As Integer = 1) As XDocument
    //    Dim content As String = ""
    //    Dim envelope As String =
    //        "<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:int=""http://ws.davox.telefonica.com.co/interfazSalesforce/"">" &
    //        "<soapenv:Header/>" &
    //        "<soapenv:Body>" &
    //        "<int:find>" &
    //        "<xml>" & EnsamblarCDATA(USER, PASSWORD, fecha, packcode, MSIDN, MetodoUrl, urlServicio, Conteo) &
    //        "</xml>" &
    //        "</int:find>" &
    //        "</soapenv:Body>" &
    //        "</soapenv:Envelope>"


    //    Dim xmlEnvelope As New XmlDocument()
    //    xmlEnvelope.LoadXml(envelope)
    //    Dim request As HttpWebRequest = CreateWebRequest(urlServicio, MetodoUrl)
    //    InsertSoapEnvelopeIntoWebRequest(xmlEnvelope, request)
    //    Dim asyncResult As IAsyncResult = request.BeginGetResponse(Nothing, Nothing)
    //    asyncResult.AsyncWaitHandle.WaitOne()
    //    Dim soapResult As String
    //    Using webResponse As WebResponse = request.EndGetResponse(asyncResult)
    //        Using rd As New StreamReader(webResponse.GetResponseStream)
    //            soapResult = rd.ReadToEnd
    //        End Using
    //        content = soapResult
    //    End Using
    //    Dim reader As New StringReader(content)
    //    Dim xml As XDocument = XDocument.Load(reader)
    //    Return xml
    //End Function


    //Public Shared Function EnsamblarCDATA(ByVal USER As String, ByVal PASSWORD As String, ByVal fecha As String, ByVal packcode As String, ByVal MSIDN As List(Of String), ByVal MetodoUrl As String, ByVal TipoOPE As String, Optional ByVal Conteo As Integer = 1) As String
    //    Dim NumeroConsulta As String = String.Empty
    //    Dim Numeros As String = String.Empty
    //    Dim envelope As String = String.Empty
    //    'Try
    //    envelope =
    //        "<![CDATA[<WS><USER>" & USER & "</USER>" &
    //        "<PASSWORD>" & PASSWORD & "</PASSWORD>" &
    //        "<DATEPROCESS>" & fecha & "</DATEPROCESS>" &
    //        "<PACKCODE>" & packcode & "</PACKCODE>" &
    //        "<SEPARATOR>Õ</SEPARATOR>" &
    //        "<TYPE>" & MetodoUrl & "</TYPE>" &
    //        "<COUNT>" & Conteo & "</COUNT>" &
    //        "<VALUES>"

    //        If Conteo > 0 Then
    //            For Each NumeroConsulta In MSIDN
    //                Numeros = Numeros & "<VALUE>" & NumeroConsulta & "</VALUE>"
    //            Next
    //        End If
    //        envelope = envelope & Numeros & "</VALUES></WS>]]>"
    //        Return envelope
    //    ' Catch ex As Exception
    //    'log.Info("Error ensamblando el CDATA")
    //    'End Try
    //End Function




        /// <summary>
        /// funcion que se encarga de separar una cadena de caracteres y retornarla en el objeto de entrada
        /// </summary>
        /// <param name="numeros">cadena que contiene los numeros del sorteo</param>
        /// <param name="s">objeto sexteto sobre el cual se devolvera la informacion separada</param>
        /// <returns>el objeto sexteto con los numeros separados y con la cantidad de numeros usados</returns>
        //public static sorteos separaNumeros( string numeros, int id, string fechaSor, string tipo, Boolean winner, sorteos s)
        //{
        //    string[] nums = null;
        //    if (numeros.Length > 0)
        //    {
        //        nums = numeros.Split('|');
        //        s.EnUso = nums.Length;
        //        switch (nums.Length)
        //        {
        //            case 1:
        //                s.Num1 = Convert.ToInt16(nums[0].ToString());
        //                break;
        //            case 2:
        //                s.Num1 = Convert.ToInt16(nums[0].ToString());
        //                s.Num2 = Convert.ToInt16(nums[1].ToString());
        //                break;
        //            case 3:
        //                s.Num1 = Convert.ToInt16(nums[0].ToString());
        //                s.Num2 = Convert.ToInt16(nums[1].ToString());
        //                s.Num3 = Convert.ToInt16(nums[2].ToString());
        //                break;
        //            case 4:
        //                s.Num1 = Convert.ToInt16(nums[0].ToString());
        //                s.Num2 = Convert.ToInt16(nums[1].ToString());
        //                s.Num3 = Convert.ToInt16(nums[2].ToString());
        //                s.Num4 = Convert.ToInt16(nums[3].ToString());
        //                break;
        //            case 5:
        //                s.Num1 = Convert.ToInt16(nums[0].ToString());
        //                s.Num2 = Convert.ToInt16(nums[1].ToString());
        //                s.Num3 = Convert.ToInt16(nums[2].ToString());
        //                s.Num4 = Convert.ToInt16(nums[3].ToString());
        //                s.Num5 = Convert.ToInt16(nums[4].ToString());
        //                break;
        //            case 6:
        //                s.Num1 = Convert.ToInt16(nums[0].ToString());
        //                s.Num2 = Convert.ToInt16(nums[1].ToString());
        //                s.Num3 = Convert.ToInt16(nums[2].ToString());
        //                s.Num4 = Convert.ToInt16(nums[3].ToString());
        //                s.Num5 = Convert.ToInt16(nums[4].ToString());
        //                s.Sb = Convert.ToInt16(nums[5].ToString());
        //                break;
        //        }
        //    }
        //    return s;
        //}

        /// <summary>
        /// pasa datos de un objeto sexteto a un objeto singles
        /// </summary>
        /// <param name="s">objeto sexteto</param>
        /// <param name="d">objeto singles</param>
        //protected static void numeros(sorteos s, sorteos.Si i)
        //{
        //    i.Num1 = s.Num1;
        //}

        /// <summary>
        /// pasa datos de un objeto sexteto a un objeto dupla
        /// </summary>
        /// <param name="s">objeto sexteto</param>
        /// <param name="d">objeto duplas</param>
        //protected static void numeros2(sorteos s, sorteos.Du d)
        //{
        //    d.Num1 = s.Num1;
        //    d.Num2 = s.Num2;
        //}

        /// <summary>
        /// pasa datos de un objeto sexteto a un objeto ternas
        /// </summary>
        /// <param name="s">objeto sexteto</param>
        /// <param name="d">objeto ternas</param>
        //protected static void numeros3(sorteos s, sorteos.Te t)
        //{
        //    t.Num1 = s.Num1;
        //    t.Num2 = s.Num2;
        //    t.Num3 = s.Num3;
        //}

        /// <summary>
        /// pasa datos de un objeto sexteto a un objeto cuartetos
        /// </summary>
        /// <param name="s">objeto sexteto</param>
        /// <param name="d">objeto cuartetos</param>
        //protected static void numeros4(sorteos s, sorteos.Cu c)
        //{
        //    c.Num1 = s.Num1;
        //    c.Num2 = s.Num2;
        //    c.Num3 = s.Num3;
        //    c.Num4 = s.Num4;
        //}

        /// <summary>
        /// pasa datos de un objeto sexteto a un objeto quintetos
        /// </summary>
        /// <param name="s">objeto sexteto</param>
        ///// <param name="d">objeto quintetos</param>
        //protected static void numeros5(sorteos s, sorteos.Qu q)
        //{
        //    q.Num1 = s.Num1;
        //    q.Num2 = s.Num2;
        //    q.Num3 = s.Num3;
        //    q.Num4 = s.Num4;
        //    q.Num5 = s.Num5;
        //}

        /// <summary>
        /// pasa datos de un objeto sexteto a un objeto quintetos
        /// </summary>
        /// <param name="s">objeto sexteto</param>
        /// <param name="d">objeto quintetos</param>
        //protected static void numeros6(sorteos s, sorteos so)
        //{
        //    s.Num1 = so.Num1;
        //    s.Num2 = so.Num2;
        //    s.Num3 = so.Num3;
        //    s.Num4 = so.Num4;
        //    s.Num5 = so.Num5;
        //    s.Sb = so.Sb;
        //}

        /// <summary>
        /// funcion para llenar las diferentes tablas de segmentacion de numeros 
        /// </summary>
        /// <param name="numeros"></param>
        //public static void generaCombos(int sorteoI, int sorteoF)
        //{
        //    sorteos s = new sorteos();
        //    string tipoTr = "Tr";
        //    string tipoRe = "Re";
        //    sorteos.Se se = new sorteos.Se();
        //    List<sorteos> lSor = new List<sorteos>();
        //    //s = separaNumeros(numeros,se);
        //    for (int i = sorteoI; sorteoI <= sorteoF; sorteoI++)
        //    {
        //        s = dbUtils.GetSorteoValues(i, tipoTr);
        //        numeros6(se, s);
        //        s = construirListaBusca(s);
        //        lSor.Add(se);
        //        //dbUtils.prInsertaCombos(se);
        //        s = dbUtils.GetSorteoValues(i, tipoRe);
        //        numeros6(se, s);
        //        s = construirListaBusca(s);
        //        lSor.Add(se);
        //        //dbUtils.prInsertaCombos(se);
        //    }
        //    dbUtils.prInsertaCombos(lSor);
        //}



        //public static List<sorteos.Se> spCoincidentes(string tipo, string nuevo, sorteos.Se sext)
        //{
        //    sorteos.Se numero = new sorteos.Se();
        //    List<sorteos.Se> listica = new List<sorteos.Se>();
        //    DataSet ds1 = new DataSet();
        //    string busca = busca = sext.Num1.ToString() + '|' + sext.Num2.ToString();
        //    construirBusca(sext);
        //    try
        //    {
        //        listica = llenaObjetoS(tipo, nuevo, busca, listica);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error" + ex.Message);
        //        listica = null;
        //    }
        //    return listica;
        //}

        //private static string construirBusca(sorteos s)
        //{
        //    string busca = string.Empty;

        //    switch (s.EnUso)
        //    {
        //        case 1:
        //            busca = s.Num1.ToString();
        //            break;
        //        case 2:
        //            busca = s.Num1.ToString() + '|' + s.Num2.ToString();
        //            break;
        //        case 3:
        //            busca = s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString();
        //            break;
        //        case 4:
        //            busca = s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString();
        //            break;
        //        case 5:
        //            busca = s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString();
        //            break;
        //        case 6:
        //            busca = s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString();
        //            break;
        //    }

        //    return busca;
        //}

        //private static sorteos construirListaBusca(sorteos s)
        //{
        //    string busca = string.Empty;
        //    List<string> lista = new List<string>();
        //    var i = new sorteos.Si();
        //    var d = new sorteos.Du();
        //    var t = new sorteos.Te();
        //    var c = new sorteos.Cu();
        //    var q = new sorteos.Qu();
        //    var se = new sorteos.Se();


        //    i.Indi.Add(s.Num1.ToString());
        //    i.Indi.Add(s.Num2.ToString());
        //    i.Indi.Add(s.Num3.ToString());
        //    i.Indi.Add(s.Num4.ToString());
        //    i.Indi.Add(s.Num5.ToString());
        //    i.Indi.Add(s.Sb.ToString());

        //    d.Dupl.Add(s.Num1.ToString() + '|' + s.Num2.ToString());
        //    d.Dupl.Add(s.Num1.ToString() + '|' + s.Num3.ToString());
        //    d.Dupl.Add(s.Num1.ToString() + '|' + s.Num4.ToString());
        //    d.Dupl.Add(s.Num1.ToString() + '|' + s.Num5.ToString());
        //    d.Dupl.Add(s.Num1.ToString() + '|' + s.Sb.ToString());
        //    d.Dupl.Add(s.Num2.ToString() + '|' + s.Num3.ToString());
        //    d.Dupl.Add(s.Num2.ToString() + '|' + s.Num4.ToString());
        //    d.Dupl.Add(s.Num2.ToString() + '|' + s.Num5.ToString());
        //    d.Dupl.Add(s.Num2.ToString() + '|' + s.Sb.ToString());
        //    d.Dupl.Add(s.Num3.ToString() + '|' + s.Num4.ToString());
        //    d.Dupl.Add(s.Num3.ToString() + '|' + s.Num5.ToString());
        //    d.Dupl.Add(s.Num3.ToString() + '|' + s.Sb.ToString());
        //    d.Dupl.Add(s.Num4.ToString() + '|' + s.Num5.ToString());
        //    d.Dupl.Add(s.Num4.ToString() + '|' + s.Sb.ToString());
        //    d.Dupl.Add(s.Num5.ToString() + '|' + s.Sb.ToString());



        //    t.Terc.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString());
        //    t.Terc.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num4.ToString());
        //    t.Terc.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num5.ToString());
        //    t.Terc.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Sb.ToString());
        //    t.Terc.Add(s.Num1.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString());
        //    t.Terc.Add(s.Num1.ToString() + '|' + s.Num3.ToString() + '|' + s.Num5.ToString());
        //    t.Terc.Add(s.Num1.ToString() + '|' + s.Num3.ToString() + '|' + s.Sb.ToString());
        //    t.Terc.Add(s.Num1.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString());
        //    t.Terc.Add(s.Num1.ToString() + '|' + s.Num4.ToString() + '|' + s.Sb.ToString());
        //    t.Terc.Add(s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString());
        //    t.Terc.Add(s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num5.ToString());
        //    t.Terc.Add(s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Sb.ToString());
        //    t.Terc.Add(s.Num2.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString());
        //    t.Terc.Add(s.Num2.ToString() + '|' + s.Num4.ToString() + '|' + s.Sb.ToString());
        //    t.Terc.Add(s.Num2.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());
        //    t.Terc.Add(s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString());
        //    t.Terc.Add(s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Sb.ToString());
        //    t.Terc.Add(s.Num3.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());
        //    t.Terc.Add(s.Num4.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());


        //    c.Cuar.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString());
        //    c.Cuar.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num5.ToString());
        //    c.Cuar.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Sb.ToString());
        //    c.Cuar.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString());
        //    c.Cuar.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num4.ToString() + '|' + s.Sb.ToString());
        //    c.Cuar.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());
        //    c.Cuar.Add(s.Num1.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString());
        //    c.Cuar.Add(s.Num1.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Sb.ToString());
        //    c.Cuar.Add(s.Num1.ToString() + '|' + s.Num3.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());
        //    c.Cuar.Add(s.Num1.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());
        //    c.Cuar.Add(s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString());
        //    c.Cuar.Add(s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Sb.ToString());
        //    c.Cuar.Add(s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());
        //    c.Cuar.Add(s.Num2.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());
        //    c.Cuar.Add(s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());

        //    q.Quin.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString());
        //    q.Quin.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Sb.ToString());
        //    q.Quin.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());
        //    q.Quin.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());
        //    q.Quin.Add(s.Num1.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());
        //    q.Quin.Add(s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());


        //    se.Sext.Add(s.Num1.ToString() + '|' + s.Num2.ToString() + '|' + s.Num3.ToString() + '|' + s.Num4.ToString() + '|' + s.Num5.ToString() + '|' + s.Sb.ToString());


        //    return s;
        //}

        //private static List<sorteos.Se> llenaObjetoS(string tipo, string nuevo, string busca, List<sorteos.Se> listica)
        //{

        //    sorteos.Se numero = new sorteos.Se();
        //    llenador(tipo, nuevo, busca, numero);
        //    listica.Add(numero);
        //    return listica;
        //}

        //private static void llenador(string tipo, string nuevo, string busca, sorteos.Se numero)
        //{
        //    DataTable dt = dbUtils.consultaCoincidentes(tipo, nuevo, busca);
        //    foreach (DataColumn col in dt.Columns)
        //    {
        //        if (col.ColumnName == "idSorteo")
        //        {
        //            numero.IdLastSorteo = dbUtils.masRecienteSorteo(dt, col.Ordinal);
        //        }
        //        if (col.ColumnName == "veces")
        //        {
        //            numero.CantCaidos = Convert.ToInt16(dt.Rows[1][col.Ordinal].ToString());
        //        }
        //    }
        //}

        //public static void sexteto_sorteo(sorteos.Se s, sorteos so)
        //{
        //    s.IdLastSorteo = so.IdSorteo;
        //    s.Tipo = so.Tipo;
        //    s.Winner = so.Winner;
        //    numeros6(s, so);
        //}


        //public static void sextetos_singles(List<sorteos.Se> sext, List<sorteos.Si> indi)
        //{
        //    foreach (sorteos.Se s in sext)
        //    {
        //        sorteos.Si d = new sorteos.Si();
        //        d.IdLastSorteo = s.IdLastSorteo;
        //        d.CantCaidos = s.CantCaidos;
        //        numeros(s, d);
        //        indi.Add(d);
        //    }
        //}

        //public static void sextetos_duplas(List<sorteos.Se> sext, List<sorteos.Du> dupl)
        //{
        //    foreach (sorteos.Se s in sext)
        //    {
        //        sorteos.Du d = new sorteos.Du();
        //        d.IdLastSorteo = s.IdLastSorteo;
        //        d.CantCaidos = s.CantCaidos;
        //        numeros2(s, d);
        //        dupl.Add(d);
        //    }
        //}

        //public static void sextetos_ternas(List<sorteos.Se> sext, List<sorteos.Te> tern)
        //{
        //    foreach (sorteos.Se s in sext)
        //    {
        //        sorteos.Te d = new sorteos.Te();
        //        d.IdLastSorteo = s.IdLastSorteo;
        //        d.CantCaidos = s.CantCaidos;
        //        numeros3(s, d);
        //        tern.Add(d);
        //    }
        //}

        //public static void sextetos_cuartetos(List<sorteos.Se> sext, List<sorteos.Cu> cuar)
        //{
        //    foreach (sorteos.Se s in sext)
        //    {
        //        sorteos.Cu d = new sorteos.Cu();
        //        d.IdLastSorteo = s.IdLastSorteo;
        //        d.CantCaidos = s.CantCaidos;
        //        numeros4(s, d);
        //        cuar.Add(d);
        //    }
        //}
        //public static void sextetos_quintetos(List<sorteos.Se> sext, List<sorteos.Qu> quin)
        //{
        //    foreach (sorteos.Se s in sext)
        //    {
        //        sorteos.Qu d = new sorteos.Qu();
        //        d.IdLastSorteo = s.IdLastSorteo;
        //        d.CantCaidos = s.CantCaidos;
        //        numeros5(s, d);
        //        quin.Add(d);
        //    }
        //}
    }

}