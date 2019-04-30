using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using System.Globalization;
using System.Web.SessionState;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace WebApplication1.Utilities
{
    public class serviceMethod
    {
        //private static CultureInfo esCO = new CultureInfo("es-CO");
        //private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //private static HttpSessionState Session
        //{
        //    get
        //    {
        //        return HttpContext.Current.Session;
        //    }
        //}

        //public static List<DataSet> ObtenerSaldosDavox(ref bool MostrarBoton, ref bool EnableTextBox)
        //{
        //    // Dim parametrosFS As New parametrosFS
        //    string Metodo = string.Empty;

        //    string user = "USER"; // Utils.GetParamValue("userWSAC");
        //    string passwd = "PASSWORD";  // Utils.GetParamValue("passWSAC");
        //    string packCode = "ASD654AS"; //  Utils.GetParamValue("packCodeWSAC");
        //    string strLRedirect;
        //    int CantCuotas = 0;
        //    int LInf = 0;
        //    int LSup = 0;
        //    string URLServicio = "<![CDATA[<WS><USER>{0}</USER><PASSWORD>{1}</PASSWORD><DATEPROCESS>{2}</DATEPROCESS><PACKCODE>{3}</PACKCODE><SEPARATOR>{4}</SEPARATOR><TYPE>{5}</TYPE><COUNT>{6}</COUNT><VALUES>{7}</VALUES></WS>]]>"; //getParamURL("URLServicesDavox_SELCuotas");
        //    DataSet Ds;
        //    XDocument xd= null;
        //    DataSet DSDt = new DataSet();
        //    DataSet Dss = new DataSet();
        //    List<DataSet> DSCol = new List<DataSet>();
        //    if (Session("Sel_Cuotas") == null)
        //        Session("Sel_Cuotas") = 0;
        //    List<string> collection = new List<string>();
        //    collection.Add(ObtenerInfoXCelSession.COD_CLIENTE + "Õ" + "1");
        //    int Conteo = 1;
        //    Metodo = ParametroMetodos("1");

        //    string fechaFormXML = Convert.ToDateTime(DateTime.Now()).ToString("yyyy-MM-dd");
        //    try
        //    {
        //        if (xd == null & Session("SaldoXD") == null)
        //        {
        //            xd = GeneraXMLSaldoDetalleSEL(user, passwd, fechaFormXML, packCode, collection, Metodo, URLServicio, Conteo);
        //            Session("SaldoXD") = xd;
        //        }
        //        else if (Session("SaldoXD") != null & Session("Sel_Cuotas") == 0)
        //            xd = Session("SaldoXD");
        //        else if (Session("SaldoXD") != null & Session("Sel_Cuotas") >= 1)
        //        {
        //            xd = null/* TODO Change to default(_) if this is not a reference type */;
        //            MostrarBoton = true;
        //            EnableTextBox = true;
        //        }
        //        if (xd != null)
        //            Ds = BringBalance(xd, Convert.ToInt16(utilidades.TipoCons.Saldo), MostrarBoton, EnableTextBox);
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Info("Se  presento error en el consumo del webservice para construir los datasets " + ex.Message.ToString());
        //        strLRedirect = "ys_error.aspx";
        //        HttpContext.Current.Response.Redirect(strLRedirect, false);
        //    }

        //    return DSCol;
        //}

//        public static List<DataSet> LogicaRepositorio(ref DataSet Ds, ref XDocument xd, string TipoConsulta, string fecha, List<string> MSIDN, int Conteo = 1, int NPagina = 1)
//        {
//            // Dim parametrosFS As New parametrosFS
//            string Metodo = string.Empty;
//            string user = "USER"; // Utils.GetParamValue("userWSAC");
//            string passwd = "PASSWORD";  // Utils.GetParamValue("passWSAC");
//            string packCode = "ASD654AS"; //  Utils.GetParamValue("packCodeWSAC");
//            string strLRedirect;
//            int CantCuotas = 0;
//            int LInf = 0;
//            int LSup = 0;
//            string URLServicio = getParamURL("URLServicesDavox_SELCuotas");

//            DataSet DSDt = new DataSet();
//            DataSet Dss = new DataSet();
//            List<DataSet> DSCol = new List<DataSet>();

//            List<string> collection = new List<string>();
//            Metodo = ParametroMetodos(TipoConsulta);

//            string fechaFormXML = Convert.ToDateTime(fecha).ToString("yyyy-MM-dd");
//            try
//            {
//                if (xd == null)
//                {
//                    xd = GeneraXMLSaldoDetalleSEL(user, passwd, fechaFormXML, packCode, MSIDN, Metodo, URLServicio, Conteo);
//                    Session("datosXD") = xd;
//                }
//                if (xd != null)
//                {
//                    Ds = BringData(xd, TipoConsulta);
//                    DetallesXPagina(Convert.ToInt16(EnumPagos.TipoCons.Detalles).ToString(), Conteo, NPagina, Metodo, user, passwd, packCode, CantCuotas, LInf, LSup, URLServicio, Ds, DSDt, Dss, DSCol, xd, collection);
//                }
//            }
//            catch (Exception ex)
//            {
//                log.Info("Se  presento error en el consumo del webservice para construir los datasets " + ex.Message.ToString());
//                strLRedirect = "ys_error.aspx";
//                HttpContext.Current.Response.Redirect(strLRedirect, false);
//            }

//            return DSCol;
//        }

//        private static void DetallesSaldo(ref string TipoConsulta, int Conteo, ref string Metodo, string user, string password, string packCode, ref int CantCuotas, ref int LInf, ref int LSup, string URLServicio, ref DataSet Ds, ref DataSet DSDt, ref DataSet Dss, ref List<DataSet> DSCol, ref XDocument xd, List<string> collection)
//        {
//            int PosI = 0;
//            int maxPosI = 0;
//            XDocument xdd = null/* TODO Change to default(_) if this is not a reference type */;

//            List<XDocument> LAllxdd = new List<XDocument>();

//            DataTable dtt = new DataTable();
//            if ((Ds != null))
//            {
//                if ((!string.IsNullOrEmpty("6")))
//                    CantCuotas = Convert.ToInt16("6");
//                if (Session("DetallesCuotas") != null)
//                    LAllxdd = Session("DetallesCuotas");
//                for (PosI = 0; PosI <= CantCuotas - 1; PosI++)
//                {
//                    if (Session("DetallesCuotas") == null)
//                    {
//                        collection.Clear();
//                        Metodo = ParametroMetodos(TipoConsulta); // "CONSULTA_DETALLECUOTAS_SEL"
//                        string fechaFormateada = Convert.ToDateTime(Ds.Tables("Cuotas").Rows(PosI)("FechaCobro")).ToString("dd/MM/yyyy");
//                        collection.Add(Ds.Tables("Cuotas").Rows(PosI)("ClienteSCL") + "Õ" + fechaFormateada);
//                        xdd = GeneraXMLSaldoDetalleSEL(user, password, fechaFormateada, packCode, collection, Metodo, URLServicio, Conteo);
//                        LAllxdd.Add(xdd);
//                    }
//                    else
//                        xdd = LAllxdd[PosI];


//                    dtt = BringData2(xdd, utilidades.TipoCons.Detalles, PosI);
//                    DSDt.Tables.Add(dtt);
//                    if (dtt != null)
//                        Ds.Tables("Cuotas").Rows(PosI)("Hijos") = true;
//                }
//                DSCol.Add(Ds);
//                DSCol.Add(DSDt);
//                Session("DetallesCuotas") = LAllxdd;

//                PaginarDataset(DSCol);
//            }
//        }

//        private static void DetallesXPagina(ref string TipoConsulta, int Conteo, int NPagina, ref string Metodo, string user, string password, string packCode, ref int CantCuotas, ref int LInf, ref int LSup, string URLServicio, ref DataSet Ds, ref DataSet DSDt, ref DataSet Dss, ref List<DataSet> DSCol, ref XDocument xd, List<string> collection)
//        {
//            int PosI = 0;
//            int maxPosI = 0;
//            XDocument xdd = null/* TODO Change to default(_) if this is not a reference type */;

//            List<XDocument> LAllxdd = new List<XDocument>();

//            DataTable dtt = new DataTable();
//            if ((Ds != null))
//            {
//                if ((!string.IsNullOrEmpty("6")))
//                    CantCuotas = Convert.ToInt16("6");
//                if (Session("DetallesCuotas") != null)
//                    LAllxdd = Session("DetallesCuotas");
//                for (PosI = 0; PosI <= CantCuotas - 1; PosI++)
//                {
//                    if (Session("DetallesCuotas") == null)
//                    {
//                        collection.Clear();
//                        Metodo = ParametroMetodos(TipoConsulta); // "CONSULTA_DETALLECUOTAS_SEL"
//                        string fechaFormateada = Convert.ToDateTime(Ds.Tables("Cuotas").Rows(PosI)("FechaCobro")).ToString("dd/MM/yyyy");
//                        collection.Add(Ds.Tables("Cuotas").Rows(PosI)("ClienteSCL") + "Õ" + fechaFormateada);
//                        xdd = GeneraXMLSaldoDetalleSEL(user, password, fechaFormateada, packCode, collection, Metodo, URLServicio, Conteo);
//                        LAllxdd.Add(xdd);
//                    }
//                    else
//                        xdd = LAllxdd[PosI];


//                    dtt = BringData2(xdd, Convert.ToInt16(utilidades.TipoCons.Detalles),ref PosI);
//                    DSDt.Tables.Add(dtt);
//                    if (dtt != null)
//                        Ds.Tables("Cuotas").Rows(PosI)("Hijos") = true;
//                }
//                DSCol.Add(Ds);
//                DSCol.Add(DSDt);
//                Session("DetallesCuotas") = LAllxdd;

//                PaginarDataset(DSCol);
//            }
//        }

//        public static XDocument GeneraXMLSaldoDetalleSEL(string USER, string PASSWORD, string fecha, string packcode, List<string> MSIDN, string MetodoUrl, string urlServicio, int Conteo = 1)
//        {
//            string content = "";
//            string paramEnvelope = "<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:int="http://ws.davox.telefonica.com.co/interfazSalesforce/"><soapenv:Header/><soapenv:Body><int:find><xml>{0}</xml></int:find></soapenv:Body></soapenv:Envelope>"; // Utils.GetParamValue("EnvelopeAC");
//            string envelope = string.Empty;

//            envelope = paramEnvelope.Replace("{0}", EnsamblarCDATA(USER, PASSWORD, fecha, packcode, MSIDN, MetodoUrl, urlServicio, Conteo));

//            XmlDocument xmlEnvelope = new XmlDocument();
//            xmlEnvelope.LoadXml(envelope);
//            log.Info("Envelope: " + envelope.ToString());
//            HttpWebRequest request = CreateWebRequest(urlServicio, MetodoUrl);
//            InsertSoapEnvelopeIntoWebRequest(ref xmlEnvelope, request);
//            IAsyncResult asyncResult = request.BeginGetResponse(null/* TODO Change to default(_) if this is not a reference type */, null/* TODO Change to default(_) if this is not a reference type */);
//            asyncResult.AsyncWaitHandle.WaitOne();
//            string soapResult;
//            using (WebResponse webResponse = request.EndGetResponse(asyncResult))
//            {
//                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream))
//                {
//                    soapResult = rd.ReadToEnd();
//                }
//                content = soapResult;
//            }
//            StringReader reader = new StringReader(content);
//            XDocument xml = XDocument.Load(reader);
//            return xml;
//        }

//        public static string EnsamblarCDATA(string USER, string PASSWORD, string fecha, string packcode, List<string> MSIDN, string MetodoUrl, string TipoOPE, int Conteo = 1)
//        {
//            //string NumeroConsulta = string.Empty;
//            string Numeros = string.Empty;
//            string paramCData = "<![CDATA[<WS><USER>{0}</USER><PASSWORD>{1}</PASSWORD><DATEPROCESS>{2}</DATEPROCESS><PACKCODE>{3}</PACKCODE><SEPARATOR>{4}</SEPARATOR><TYPE>{5}</TYPE><COUNT>{6}</COUNT><VALUES>{7}</VALUES></WS>]]>"; // Utils.GetParamValue("CDataAC");
//            string cdata = string.Empty;

//            if (Conteo > 0)
//            {
//                foreach (string NumeroConsulta in MSIDN)
//                    Numeros = Numeros + "<VALUE>" + NumeroConsulta + "</VALUE>";
//            }
//            cdata = paramCData.Replace("{0}", USER).Replace("{1}", PASSWORD).Replace("{2}", fecha).Replace("{3}", packcode).Replace("{4}", "Õ").Replace("{5}", MetodoUrl).Replace("{6}", Conteo).Replace("{7}", Numeros);
//            log.Info("Request" + cdata);
//            return cdata;
//        }

//        public static string getParamURL(string parametro)
//        {
//            string urlECare = "";
//            modGeneral MGeneral = modGeneral.Instance;
//            CodigosParametros oResponse = null/* TODO Change to default(_) if this is not a reference type */;
//            // Dim oFSResponse As parametrosFS = Nothing
//            log.Info("Se ejecuta la operacion GetValorParametrosAutogestion, Entrada : " + parametro);
//            oResponse = MGeneral.GetValorParametrosAutogestion(parametro);
//            log.Info("Se ejecuta la operacion GetValorParametrosAutogestion, Salida : " + JsonConvert.SerializeObject(oResponse));
//            urlECare = oResponse.VALOR;
//            return urlECare;
//        }

//        //public static bool ActivaProxy()
//        //{
//        //    bool retorno;
//        //    string packCode = "Proxy_Davox";  //Utils.GetParamValue("Proxy_Davox");
//        //    retorno = (packCode == "1")? true: false;
//        //    return retorno;
//        //}

//        public static HttpWebRequest CreateWebRequest(string url, string Action)
//        {
//            HttpWebRequest WebRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
//            WebRequest.Headers.Add("SOAPAction", Action);
//            WebRequest.ContentType = "text/xml;charset=\"utf-8\"";
//            WebRequest.Accept = "text/xml";
//            WebRequest.Method = "POST";

//            //if (ActivaProxy())
//            //{
//                NetworkCredential net = new NetworkCredential();
//                WebProxy proxy = new WebProxy("PROXY_HOST", Convert.ToInt32("8080"));
//                //WebProxy proxy = new WebProxy(Utils.GetParamValue("PROXY_HOST"), Convert.ToInt32(Utils.GetParamValue("PROXY_PORT")));
//                net.Domain = "PROXY_DOMAIN"; //Utils.GetParamValue("PROXY_DOMAIN");
//                // net.Password = proxy.BypassList()Utils.GetParamValue("PROXY_PASSWORD")
//                // net.UserName = Utils.GetParamValue("PROXY_USERNAME")
//                proxy.Credentials = net;
//                WebRequest.Proxy = proxy;
//            //}
//            return WebRequest;
//        }

//        public static void InsertSoapEnvelopeIntoWebRequest(ref XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
//        {
//            try
//            {
//                using (Stream Stream = webRequest.GetRequestStream)
//                {
//                    soapEnvelopeXml.Save(Stream);
//                }
//            }
//            catch (WebException wex)
//            {
//                log.Info("Error haciendo el request del XML" + wex.ToString);

//                log.Info("Respuesta : " + wex.Response.GetResponseStream.ToString());
//            }
//            catch (Exception ex)
//            {
//                log.Info("Error haciendo el request del XML" + ex.ToString());
//            }
//        }

//        private static void HeaderTableSaldo(DataTable dt)
//        {
//            dt.Columns.Add(new DataColumn("NCelular", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("ClienteSCL", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("SaldoActual", typeof(System.Single)));
//            dt.Columns.Add(new DataColumn("Transaccion", typeof(System.Single)));
//            dt.Columns.Add(new DataColumn("FechaCobro", typeof(System.DateTime)));
//            dt.Columns.Add(new DataColumn("ValorRecaudo", typeof(System.Single)));
//            dt.Columns.Add(new DataColumn("CodTransaccion", typeof(System.Single)));
//            dt.Columns.Add(new DataColumn("EstTransaccion", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("Restantes", typeof(System.Single)));
//        }

//        private static void HeaderTableCuotas(DataTable dt)
//        {
//            dt.Columns.Add(new DataColumn("NCelular", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("ClienteSCL", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("Transaccion", typeof(System.Single)));
//            dt.Columns.Add(new DataColumn("FechaCobro", typeof(System.DateTime)));
//            dt.Columns.Add(new DataColumn("ValorCuota", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("SaldoFavor", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("CodTransaccion", typeof(System.Single)));
//            dt.Columns.Add(new DataColumn("EstTransaccion", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("FId", typeof(System.Int32)));
//            dt.Columns.Add(new DataColumn("Hijos", typeof(System.Boolean)));
//            dt.Columns.Add(new DataColumn("FechaGL", typeof(System.String)));
//        }


//        private static void HeaderTableDetalles(DataTable dt)
//        {
//            dt.Columns.Add(new DataColumn("ClienteSCL", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("DescEquipo", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("IMEIEquipo", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("ValorCuota", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("NumCuota", typeof(System.String)));
//            dt.Columns.Add(new DataColumn("CodTransaccion", typeof(System.Single)));
//            dt.Columns.Add(new DataColumn("EstTransaccion", typeof(System.String)));
//        }

//        /// <summary>
//        ///     ''' Número de cliente SCL o número de celular 
//        ///     ''' Tipo de transacción 
//        ///     ''' Saldo del mes facturado
//        ///     ''' Referencia de pago
//        ///     ''' Fecha del último pago
//        ///     ''' Cuotas por facturar
//        ///     ''' Código de transacción
//        ///     ''' Mensaje de estado de transacción
//        ///     ''' 
//        ///     ''' </summary>
//        ///     ''' <param name="datos"></param>
//        private static void ProcesarTramaSaldo(ref DataTable dt, string[] datos)
//        {
//            DataRow row = dt.NewRow;
//            row("NCelular") = datos[0];
//            row("ClienteSCL") = datos[1];
//            row("SaldoActual") = datos[2];
//            row("Transaccion") = datos[3];
//            row("FechaCobro") = datos[4];
//            row("ValorRecaudo") = datos[5];
//            row("CodTransaccion") = datos[6];
//            row("EstTransaccion") = datos[7];
//            row("Restantes") = datos[8];
//            dt.Rows.Add(row);
//        }

//        /// <summary>
//        ///     ''' Número de celular 
//        ///     '''	Código del cliente SCL
//        ///     '''	Tipo Transacción (1 o 2)
//        ///     '''	Fecha de cobro de la cuota de la financiación
//        ///     '''	Valor de la cuota de la financiación
//        ///     '''	Código de transacción
//        ///     '''	Mensaje de estado de transacción
//        ///     ''' </summary>
//        ///     ''' <param name="datos"></param>

//        private static void ProcesarTramaCuotas(ref DataTable dt, string[] datos, int IdChk, bool Childs)
//        {
//            DataRow row = dt.NewRow;
//            row("NCelular") = datos[0];
//            row("ClienteSCL") = datos[1];
//            row("Transaccion") = datos[2];
//            string fecobroStr = Convert.ToString(datos[3]);
//            DateTime fecobroDat;
//            if (DateTime.TryParseExact(fecobroStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, ref fecobroDat))
//                row("FechaCobro") = Convert.ToDateTime(fecobroDat);
//            // Convert.ToDateTime(datos(3))
//            row("ValorCuota") = datos[4];
//            row("SaldoFavor") = datos[5];
//            row("CodTransaccion") = datos[6];
//            row("EstTransaccion") = datos[7];
//            row("FId") = IdChk;
//            row("Hijos") = Childs;
//            row("FechaGL") = EmbellecedordeFecha(row("FechaCobro"));
//            dt.Rows.Add(row);
//        }

//        /// <summary>
//        ///     ''' Número de cliente SCL 
//        ///     ''' Descripción Equipo
//        ///     ''' IMEI_EQUIPO
//        ///     ''' Valor de la cuota
//        ///     ''' Cuota
//        ///     ''' Código de transacción
//        ///     ''' Mensaje de estado de transacción
//        ///     ''' </summary>
//        ///     ''' <param name="datos"></param>

//        private static DataRow ProcesarTramaDetalles(DataTable dt, string[] datos)
//        {
//            DataRow row = dt.NewRow;
//            row("ClienteSCL") = datos[0];
//            row("DescEquipo") = datos[1];
//            row("IMEIEquipo") = datos[2];
//            row("ValorCuota") = datos[3];
//            row("NumCuota") = datos[4];
//            row("CodTransaccion") = datos[5];
//            row("EstTransaccion") = datos[6];
//            return row;
//        }

//        public static string EmbellecedordeFecha(string FechaFea)
//        {
//            string FechaGL = string.Empty;
//            FechaGL = Convert.ToDateTime(FechaFea).ToString("dd 'de' MMMM 'de' yyyy");
//            FechaGL = FechaGL.Replace("January", "Enero");
//            FechaGL = FechaGL.Replace("February", "Febrero");
//            FechaGL = FechaGL.Replace("March", "Marzo");
//            FechaGL = FechaGL.Replace("April", "Abril");
//            FechaGL = FechaGL.Replace("May", "Mayo");
//            FechaGL = FechaGL.Replace("June", "Junio");
//            FechaGL = FechaGL.Replace("July", "Julio");
//            FechaGL = FechaGL.Replace("August", "Agosto");
//            FechaGL = FechaGL.Replace("September", "Septiembre");
//            FechaGL = FechaGL.Replace("October", "Octubre");
//            FechaGL = FechaGL.Replace("November", "Noviembre");
//            FechaGL = FechaGL.Replace("December", "Diciembre");
//            return FechaGL;
//        }

//        public static void PaginarDataset(ref List<DataSet> DSCol)
//        {
//            // Dim CuoDataSet As DataSet = New DataSet
//            DataSet DetDataSet = new DataSet();
//            DataTable DCTemp = null/* TODO Change to default(_) if this is not a reference type */;
//            // Dim DtTemp As DataTable = Nothing
//            DateTime fecha = System.Convert.ToDateTime(DateTime.Now);
//            string CuotaDetalle = string.Empty;
//            double VCuota = 0;
//            int Pags = 0;
//            int PagsMod = 0;
//            int Regs = 0;
//            int regInicio = 0;
//            int regFinal = 0;
//            int i = 0;
//            int Npag = 1;
//            bool Parar = false;
//            int divisor = ParametroPaginacion();

//            Regs = DSCol[0].Tables("Cuotas").Rows.Count;

//            while (!Parar)
//            {
//                i = regInicio;
//                PagsMod = CalculaParametrosPaginacion(Npag, ref Pags, Regs, ref regInicio, ref regFinal, divisor);
//                CrearEstructuraTabla(ref EnumPagos.TipoCons.Cuotas, ref DCTemp, ref NPag);

//                DSCol.Add(PobladorCuotas(DSCol, DCTemp, regInicio, regFinal));

//                DSCol.Add(PobladorDetalles(DSCol, DetDataSet, regInicio, regFinal, Npag));

//                if ((Npag >= PagsMod))
//                    Parar = true;
//                else
//                    Npag += 1;
//            }
//        }

//        private static DataSet PobladorCuotas(List<DataSet> DSCol, DataTable DCTemp, int regInicio, int regFinal)
//        {
//            int Fid = 0;
//            DataSet dss = new DataSet();
//            int i;
//            for (i = regInicio; i <= regFinal; i++)
//            {
//                DataRow row = DCTemp.NewRow;
//                row("NCelular") = DSCol[0].Tables("Cuotas").Rows(i)("NCelular");
//                row("ClienteSCL") = DSCol[0].Tables("Cuotas").Rows(i)("ClienteSCL");
//                row("Transaccion") = DSCol[0].Tables("Cuotas").Rows(i)("Transaccion");
//                row("FechaCobro") = DSCol[0].Tables("Cuotas").Rows(i)("FechaCobro");
//                row("ValorCuota") = Utils.FormatNumber(Convert.ToSingle(DSCol[0].Tables("Cuotas").Rows(i)("ValorCuota")));
//                row("SaldoFavor") = Utils.FormatNumber(Convert.ToSingle(DSCol[0].Tables("Cuotas").Rows(i)("SaldoFavor")));
//                row("CodTransaccion") = DSCol[0].Tables("Cuotas").Rows(i)("CodTransaccion");
//                row("EstTransaccion") = DSCol[0].Tables("Cuotas").Rows(i)("EstTransaccion");
//                row("FId") = DSCol[0].Tables("Cuotas").Rows(i)("FId");
//                Fid = Convert.ToInt16(DSCol[0].Tables("Cuotas").Rows(i)("FId"));
//                row("Hijos") = DSCol[0].Tables("Cuotas").Rows(i)("Hijos");
//                row("FechaGL") = DSCol[0].Tables("Cuotas").Rows(i)("FechaGL");
//                DCTemp.Rows.Add(row);
//            }
//            dss.Tables.Add(DCTemp);
//            return dss;
//        }


//        private static DataSet PobladorDetalles(List<DataSet> DSCol, DataSet DetDataSet, int regInicio, int regFinal, int Fid)
//        {
//            int i = 0;
//            DataTable DtTemp = new DataTable();
//            DataSet dss = new DataSet();
//            string indPagina = string.Empty;
//            string indCuota = string.Empty;
//            int indt;
//            for (indt = regInicio; indt <= regFinal; indt++)
//            {
//                CrearEstructuraTabla(ref EnumPagos.TipoCons.DetailRes, ref DtTemp, ref indt);
//                indCuota = indt.ToString();
//                i = 0;
//                while (i >= 0 & i < DSCol[1].Tables(indCuota).Rows.Count)
//                {
//                    indPagina = Fid.ToString();
//                    DataRow row = DtTemp.NewRow;
//                    row("ClienteSCL") = DSCol[1].Tables(indCuota).Rows(i)("ClienteSCL");
//                    row("DescEquipo") = DSCol[1].Tables(indCuota).Rows(i)("DescEquipo");
//                    row("IMEIEquipo") = DSCol[1].Tables(indCuota).Rows(i)("IMEIEquipo");
//                    row("ValorCuota") = Utils.FormatNumber(Convert.ToSingle(DSCol[1].Tables(indCuota).Rows(i)("ValorCuota")));
//                    row("NumCuota") = DSCol[1].Tables(indCuota).Rows(i)("NumCuota");
//                    row("CodTransaccion") = DSCol[1].Tables(indCuota).Rows(i)("CodTransaccion");
//                    row("EstTransaccion") = DSCol[1].Tables(indCuota).Rows(i)("EstTransaccion");
//                    DtTemp.Rows.Add(row);
//                    i += 1;
//                }
//                dss.Tables.Add(DtTemp);
//            }
//            return dss;
//        }

//        private static int CalculaParametrosPaginacion(int numpagina, ref int Pags, int Regs, ref int regInicio, ref int regFinal, int divisor)
//        {
//            int PagsMod = 0;
//            int PagsR = 1;
//            int RegsT = 0;
//            if (Regs > 0 & Regs > divisor)
//            {
//                PagsMod = Regs % divisor;
//                if (PagsMod == 0)
//                {
//                    Pags = Regs / (double)divisor;
//                    if (numpagina > 0 & numpagina <= Pags)
//                    {
//                        regInicio = ((numpagina - 1) * divisor);
//                        regFinal = (numpagina * divisor) - 1;
//                    }
//                    PagsR = Pags;
//                }
//                else
//                {
//                    RegsT = Regs - PagsMod;
//                    Pags = Convert.ToInt16(RegsT / (double)divisor);
//                    if (numpagina > 0)
//                    {
//                        if (numpagina <= Pags)
//                        {
//                            regInicio = ((numpagina - 1) * divisor);
//                            regFinal = (numpagina * divisor) - 1;
//                        }
//                        else
//                        {
//                            regInicio = ((numpagina - 1) * divisor);
//                            regFinal = Regs - 1;
//                        }
//                    }
//                    PagsR = Pags + 1;
//                }
//            }
//            else if (Regs > 0 & Regs < divisor)
//            {
//                regInicio = 0;
//                regFinal = Regs - 1;
//                PagsR = 1;
//            }

//            return PagsR;
//        }

//        public static DataSet BringBalance(XDocument Xml, int TipoConsulta, ref bool MostrarBoton, ref bool EnableTextBox)
//        {
//            string[] datos;
//            DataSet ds = new DataSet("saldo");
//            DataTable dt = null/* TODO Change to default(_) if this is not a reference type */;
//            string redireccion = string.Empty;
//            string temvalue = string.Empty;
//            int IdCheckBox = 0;
//            int ValorSaldo = 0;
//            int CuotasPend = 0;
//            try
//            {
//                var xefr = Xml.Root.Descendants("findReturn");
//                if (xefr != null)
//                {
//                    if (xefr.Value != null)
//                    {
//                        XDocument textvalue = XDocument.Parse(xefr.Value.ToString());
//                        CrearEstructuraTabla(ref TipoConsulta, ref dt);
//                        ;/* Cannot convert ForEachBlockSyntax, CONVERSION ERROR: Conversion for XmlElementAccessExpression not implemented, please report this issue in 'textvalue.<WS>.<VALUES>.<VA...' at character 12657
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitXmlMemberAccessExpression(XmlMemberAccessExpressionSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.XmlMemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitXmlMemberAccessExpression(XmlMemberAccessExpressionSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.XmlMemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitForEachBlock(ForEachBlockSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ForEachBlockSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

//Input: 
//                    For Each xe As XElement In textvalue.<WS>.<VALUES>.<VALUE>
//                        datos = HttpUtility.HtmlDecode(xe.Value).Split("Õ")

//                        log.Info("respuesta despues de decodificar" & textvalue.ToString)

//                        If datos.Count > 1 Then

//                            ValorSaldo = IIf(String.IsNullOrEmpty(datos(2).ToString), 0, Convert.ToInt32(datos(2).ToString))
//                            CuotasPend = IIf(String.IsNullOrEmpty(datos(8).ToString), 0, Convert.ToInt32(datos(8).ToString))
//                            If ValorSaldo = 0 And CuotasPend = 0 Then
//                                MostrarBoton = False
//                                EnableTextBox = False
//                            ElseIf ValorSaldo > 0 And CuotasPend = 0 Then
//                                MostrarBoton = False
//                                EnableTextBox = True
//                                Session("SaldoCobrosEquipo") = datos(2).ToString
//                            ElseIf ValorSaldo > 0 And CuotasPend > 0 Then
//                                MostrarBoton = True
//                                EnableTextBox = True
//                                Session("SaldoCobrosEquipo") = datos(2).ToString
//                            ElseIf ValorSaldo = 0 And CuotasPend > 0 Then
//                                MostrarBoton = True
//                                EnableTextBox = True
//                                Session("SaldoCobrosEquipo") = "0"
//                            End If
//                            Session("Cuotas") = CuotasPend
//                            Select Case TipoConsulta
//                                Case EnumPagos.TipoCons.Saldo
//                                    ProcesarTramaSaldo(dt, datos)
//                            End Select
//                        Else
//                            log.Error("No se obtuvo un array en la respuesta del XDocument : " + xe.Value.ToString())
//                        End If
//                    Next

// */
//                        ds.Tables.Add(dt);
//                    }
//                    else
//                    {
//                        ds.Tables.Add(dt);
//                        return ds;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                log.Error("Error procesando trama respuesta en BringBalance, Entrada : " + ex.Message);
//            }
//            return ds;
//        }




//        /// <summary>
//        ///     ''' convierte en dataset la informacion del XML
//        ///     ''' </summary>
//        ///     ''' <param name="xml"></param>
//        ///     ''' <param name="tipoconsulta">1 consulta saldo, 2 consulta Cuotas, 3 consulta detalles</param>
//        ///     ''' <returns></returns>
//        public static DataSet BringData(XDocument xml, int tipoconsulta)
//        {
//            string[] datos;
//            DataSet ds = new DataSet("resultados");
//            DataTable dt = null/* TODO Change to default(_) if this is not a reference type */;
//            string redireccion = string.Empty;
//            string temvalue = string.Empty;
//            int IdCheckBox = 0;
//            try
//            {
//                var xefr = xml.Root.Descendants("findReturn");
//                if (xefr != null)
//                {
//                    if (xefr.Value != null)
//                    {
//                        XDocument textvalue = XDocument.Parse(xefr.Value.ToString());
//                        CrearEstructuraTabla(ref tipoconsulta, ref dt);
//                        ;/* Cannot convert ForEachBlockSyntax, CONVERSION ERROR: Conversion for XmlElementAccessExpression not implemented, please report this issue in 'textvalue.<WS>.<VALUES>.<VA...' at character 15938
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitXmlMemberAccessExpression(XmlMemberAccessExpressionSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.XmlMemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitXmlMemberAccessExpression(XmlMemberAccessExpressionSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.XmlMemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitForEachBlock(ForEachBlockSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ForEachBlockSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

//Input: 
//                    For Each xe As XElement In textvalue.<WS>.<VALUES>.<VALUE>
//                        datos = HttpUtility.HtmlDecode(xe.Value).Split("Õ")

//                        log.Info("respuesta despues de decodificar" & textvalue.ToString)

//                        If datos.Count > 1 Then
//                            Session("SaldoAFavorEquipo") = datos(5).ToString
//                            If datos(6).ToString() = Utils.GetParamValue("CodigoErrorSinCuotas") Then
//                                redireccion = "ys_error.aspx"
//                                transferirParametros.cadenaResultado = codigosMensajes.codErrAC
//                                'TelefonicaSession.PagRet =
//                                transferirParametros.PgBack = "ys_ind_servicio_pago_saldo.aspx"
//                                log.Error("Error pagina ys_ind_servicio_sel_cuotas.aspx. No se despliega detalles por respuesta " & datos(6).ToString() & " del servicio de detalle de cuotas. " & " Redireccion:" & redireccion & " Numero Celular :" & datos(0).ToString())
//                                HttpContext.Current.Response.Redirect(redireccion, False)
//                                Exit Function
//                            End If

//                            Select Case tipoconsulta
//                                Case EnumPagos.TipoCons.Saldo
//                                    ProcesarTramaSaldo(dt, datos)
//                                Case EnumPagos.TipoCons.Cuotas
//                                    IdCheckBox = IdCheckBox + 1
//                                    TelefonicaSession.CantCuotasXEquipo = IdCheckBox.ToString()
//                                    ProcesarTramaCuotas(dt, datos, IdCheckBox, False)
//                            End Select
//                        Else
//                            log.Error("No se obtuvo un array en la respuesta del XDocument : " + xe.Value.ToString())
//                        End If
//                    Next

// */
//                        ds.Tables.Add(dt);
//                    }
//                    else
//                    {
//                        ds.Tables.Add(dt);
//                        return ds;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                log.Error("Error procesando trama respuesta en BringData, Entrada : " + ex.Message);
//            }
//            return ds;
//        }

//        /// <summary>
//        ///     ''' convierte en dataset la informacion del XML
//        ///     ''' </summary>
//        ///     ''' <param name="xml"></param>
//        ///     ''' <param name="tipoconsulta">1 consulta saldo, 2 consulta Cuotas, 3 consulta detalles</param>
//        ///     ''' <returns></returns>
//        public static DataTable BringData2(XDocument xml, int tipoconsulta, ref int indTabla)
//        {
//            string[] datos;
//            DataRow Rou = null/* TODO Change to default(_) if this is not a reference type */;
//            DataSet ds = new DataSet("resultados");
//            DataTable dt = null/* TODO Change to default(_) if this is not a reference type */;
//            string temvalue = string.Empty;
//            int IdCheckBox = 0;
//            try
//            {
//                var xefr = xml.Root.Descendants("findReturn");
//                if (xefr != null)
//                {
//                    if (xefr.Value != null)
//                    {
//                        XDocument textvalue = XDocument.Parse(xefr.Value.ToString());
//                        CrearEstructuraTabla(ref tipoconsulta, ref dt, ref indTabla);
//                        Rou = dt.NewRow;
//                        ;/* Cannot convert ForEachBlockSyntax, CONVERSION ERROR: Conversion for XmlElementAccessExpression not implemented, please report this issue in 'textvalue.<WS>.<VALUES>.<VA...' at character 19227
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.NodesVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitXmlMemberAccessExpression(XmlMemberAccessExpressionSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.XmlMemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingNodesVisitor.DefaultVisit(SyntaxNode node)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.VisitXmlMemberAccessExpression(XmlMemberAccessExpressionSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.XmlMemberAccessExpressionSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at ICSharpCode.CodeConverter.CSharp.VisualBasicConverter.MethodBodyVisitor.VisitForEachBlock(ForEachBlockSyntax node)
//   at Microsoft.CodeAnalysis.VisualBasic.Syntax.ForEachBlockSyntax.Accept[TResult](VisualBasicSyntaxVisitor`1 visitor)
//   at Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxVisitor`1.Visit(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.ConvertWithTrivia(SyntaxNode node)
//   at ICSharpCode.CodeConverter.CSharp.CommentConvertingMethodBodyVisitor.DefaultVisit(SyntaxNode node)

//Input: 
//                    For Each xe As XElement In textvalue.<WS>.<VALUES>.<VALUE>
//                        datos = HttpUtility.HtmlDecode(xe.Value).Split("Õ")
//                        If datos.Count > 1 Then


//                            If tipoconsulta = EnumPagos.TipoCons.Detalles Then
//                                'CrearEstructuraTabla(EnumPagos.TipoCons.Detalles, dt)
//                                Rou = ProcesarTramaDetalles(dt, datos)

//                            End If
//                        Else
//                            log.Error("No se obtuvo un array en la respuesta del XDocument : " + xe.Value.ToString())
//                        End If
//                        If Rou("DescEquipo") IsNot Nothing Then
//                            dt.Rows.Add(Rou)
//                        End If
//                    Next

// */
//                        return dt;
//                    }
//                    else
//                    {
//                        dt.Rows.Add(Rou);
//                        return dt;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                log.Error("Error procesando trama respuesta en BringData2, Entrada : " + ex.Message);
//            }
//            return dt;
//        }

//        private static void CrearEstructuraTabla(ref int tipoconsulta, ref DataTable dt, ref int indTabla = 0)
//        {
//            switch (tipoconsulta)
//            {
//                case object _ when EnumPagos.TipoCons.Saldo:
//                    {
//                        dt = new DataTable("Saldo");
//                        HeaderTableSaldo(dt);
//                        break;
//                    }

//                case object _ when EnumPagos.TipoCons.Cuotas:
//                    {
//                        string NTabla = "Cuotas";
//                        if (indTabla > 0)
//                            NTabla = "CuotasPagina" + indTabla.ToString();
//                        dt = new DataTable(NTabla);
//                        HeaderTableCuotas(dt);
//                        break;
//                    }

//                case object _ when EnumPagos.TipoCons.Detalles:
//                    {
//                        string NTabla = indTabla.ToString();
//                        dt = new DataTable(NTabla);
//                        HeaderTableDetalles(dt);
//                        break;
//                    }

//                case object _ when EnumPagos.TipoCons.DetailRes:
//                    {
//                        string NTabla = string.Empty;
//                        if (indTabla >= 0)
//                            NTabla = "DetallesCuota" + indTabla.ToString();
//                        dt = new DataTable(NTabla);
//                        HeaderTableDetalles(dt);
//                        break;
//                    }
//            }
//            dt.Rows.Clear();
//        }

//        public static void formateaCamposT(ref double valor, ref TextBox Obj)
//        {
//            if (valor.ToString().Length < 3)
//                Obj.Text = string.Format("${0}", valor);
//            else
//                Obj.Text = string.Format("${0}", valor.ToString("0,0", esCO));
//        }

//        public static void formateaCamposL(ref double valor, ref Label Obj)
//        {
//            if (valor.ToString().Length < 3)
//                Obj.Text = string.Format("${0}", valor);
//            else
//                Obj.Text = string.Format("${0}", valor.ToString("0,0", esCO));
//        }


//        public static void formateaCamposLi(ref double valor, ref Literal Obj)
//        {
//            if (valor.ToString().Length < 3)
//                Obj.Text = string.Format("${0}", valor);
//            else
//                Obj.Text = string.Format("${0}", valor.ToString("0,0", esCO));
//        }


//        // Guarda incidencia al realizar clic en el botón LnkFinanciacion CDHS
//        public static void guardarIncidencia()
//        {
//            modGeneral MGeneral = modGeneral.Instance;
//            CodigosParametros oResponse = null/* TODO Change to default(_) if this is not a reference type */;
//            Array strArr;
//            // Parametros del servicio
//            var parametros = new modelCrearIncidencia();

//            oResponse = MGeneral.GetValorParametrosAutogestion("ParamCreaIncidenciaPagosEditados");

//            if ((oResponse.CODERROR == "00000" && TelefonicaSession.Cod_Abonado != null/* TODO Change to default(_) if this is not a reference type */))
//            {
//                strArr = Split(oResponse.VALOR, ";");

//                parametros.NumAtencion = strArr(0);
//                parametros.Usuario = strArr(1);
//                parametros.CodCampania = strArr(2);
//                parametros.IndHistorico = strArr(3);
//                parametros.TipIncidencia = strArr(4);
//                parametros.CodEstado = strArr(5);
//                parametros.Sector = strArr(6);

//                // El comentario tendra otros parametros que todavia no se han definido
//                parametros.Comentario = construir_msg_incidencia(); // strArr(7)

//                // Codigo Interlocutor
//                parametros.CodInterlocutor = TelefonicaSession.Cod_Abonado;


//                // Guarda incidencia 
//                MGeneral.registrarIncidencia(parametros);
//            }
//        }


//        // RN - funcion que verifica si ha tenido cambios
//        protected static bool verifica_cambio()
//        {
//            bool confirmar = false;
//            if (System.Convert.ToDecimal(TelefonicaSession.ValorAPagarPorServicio) < System.Convert.ToDecimal(TelefonicaSession.SaldoCobros) | System.Convert.ToDecimal(TelefonicaSession.ValorAPagarPorServicio) > System.Convert.ToDecimal(TelefonicaSession.SaldoCobros) | System.Convert.ToDecimal(TelefonicaSession.ValorAPagarPorEquipo) < System.Convert.ToDecimal(TelefonicaSession.SaldoCobrosEquipo) | System.Convert.ToDecimal(TelefonicaSession.ValorAPagarPorEquipo) > System.Convert.ToDecimal(TelefonicaSession.SaldoCobrosEquipo))
//                confirmar = true;
//            else if (System.Convert.ToDecimal(TelefonicaSession.ValorAPagarPorServicio) == System.Convert.ToDecimal(TelefonicaSession.SaldoCobros) | System.Convert.ToDecimal(TelefonicaSession.ValorAPagarPorEquipo) == System.Convert.ToDecimal(TelefonicaSession.SaldoCobrosEquipo))
//                confirmar = false;
//            return confirmar;
//        }

//        // RN - Este procedimiento construye la cadena de la incidencia y retorna falso en caso que la construccion de la cadena haya fallado
//        protected static string construir_msg_incidencia()
//        {
//            string cadena = "";
//            bool cambio;
//            string strSldServicio;
//            string strSldEquipo;
//            string strPagoServicio;
//            string strPagoEquipo;
//            string strFecha;
//            string strHora;

//            try
//            {
//                cambio = verifica_cambio();
//                cadena = string.Format("|{0}| ", TelefonicaSession.Nombre.ToString);
//                if (TelefonicaSession.Apellido1.ToString != "" | TelefonicaSession.Apellido2.ToString != "")
//                    cadena += string.Format("|{0} {1}| ", TelefonicaSession.Apellido1.ToString, TelefonicaSession.Apellido2.ToString);
//                cadena += string.Format(" identificado(con) |{0}| |{1}| con la línea |{2}| cuenta |{3}| ", TelefonicaSession.Tip_Ident.ToString, TelefonicaSession.idCliente.ToString, TelefonicaSession.Celular.ToString, TelefonicaSession.Cod_Abonado.ToString);
//                if (cambio)
//                {
//                    strSldServicio = "$" + Format(System.Convert.ToDouble(TelefonicaSession.SaldoCobros), "###,###,###,##0");
//                    strSldEquipo = "$" + Format(System.Convert.ToDouble(TelefonicaSession.SaldoCobrosEquipo), "###,###,###,##0");
//                    cadena += string.Format("con un valor por pagar en servicio |{0}|", strSldServicio);
//                    if ((System.Convert.ToDouble(TelefonicaSession.SaldoCobrosEquipo) > 0))
//                        cadena += string.Format(" y terminal de |{0}|. ", strSldEquipo);
//                    else
//                        cadena += string.Format(". ");
//                }
//                strPagoServicio = "$" + Format(System.Convert.ToDouble(TelefonicaSession.ValorAPagarPorServicio), "###,###,###,##0");
//                strPagoEquipo = "$" + Format(System.Convert.ToDouble(TelefonicaSession.ValorAPagarPorEquipo), "###,###,###,##0");
//                strFecha = DateTime.Now().ToShortDateString();
//                strHora = string.Format("{0:HH:mm:ss}", DateTime.Now);
//                cadena += string.Format("Realizó el pago el día  |{0}| a las |{1}| ", strFecha, strHora);
//                if ((System.Convert.ToDouble(TelefonicaSession.ValorAPagarPorServicio) > 0))
//                {
//                    cadena += string.Format(" por un valor de servicio de |{0}|", strPagoServicio);
//                    if ((System.Convert.ToDouble(TelefonicaSession.ValorAPagarPorEquipo) > 0))
//                        cadena += string.Format(" y un valor de terminal de |{0}|.", strPagoEquipo);
//                    else
//                        cadena += string.Format(".");
//                }
//                else if ((System.Convert.ToDouble(TelefonicaSession.ValorAPagarPorEquipo) > 0))
//                    cadena += string.Format(" por un valor de terminal de |{0}|.", strPagoEquipo);
//                else
//                    cadena += string.Format(".");
//            }



//            // log.Info(cadena)

//            // cadena += cadena + "Realizo el pago |el día  |dd/mm/aaaa| a las |hh24:mi:ss| por un valor de servicio de |<valor servicio pagado>| y un valor de terminal de |<valor terminal pagado>|"
//            catch (Exception ex)
//            {
//                log.Error(ex.Message + "|" + JsonConvert.SerializeObject(ex));
//                cadena = ex.Message;
//            }
//            return cadena;
//        }


//        public static int ParametroPaginacion()
//        {
//            parametrosFS parametrosFS = new parametrosFS();
//            int Cantidad;
//            string valor;
//            valor = "20"; //FSUtils.getParam("RegistrosXPagina_SELCuotas");
//            if (!string.IsNullOrEmpty(valor) & (valor.Length <= 3 & valor.Length >= 1))
//                Cantidad = Convert.ToInt16(valor);
//            else
//                Cantidad = 4;
//            return Cantidad;
//        }


//        public static string ParametroMetodos(string TipoConsulta)
//        {
//            string Metodo = string.Empty;
//            int valor = 0;
//            if ((!string.IsNullOrEmpty(TipoConsulta)))
//                valor = Convert.ToInt16(TipoConsulta);

//            switch (valor)
//            {
//                case object _ when utilidades.TipoCons.Saldo:
//                    {
//                        Metodo = FSUtils.getParam("OperacionSaldo_SELCuotas");
//                        break;
//                    }

//                case object _ when EnumPagos.TipoCons.Cuotas:
//                    {
//                        Metodo = FSUtils.getParam("OperacionCuotas_SELCuotas");
//                        break;
//                    }

//                case object _ when EnumPagos.TipoCons.Detalles:
//                    {
//                        Metodo = FSUtils.getParam("OperacionDetalles_SELCuotas");
//                        break;
//                    }
//            }
//            return Metodo;
//        }




    }
}