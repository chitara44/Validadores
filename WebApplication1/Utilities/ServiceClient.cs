
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Tmc.Servicios.FullStack
{
    public class ServiceClient
    {
        /// <summary>
        /// Funciones comunes de consumo SOAP.
        /// </summary>
        public class Common
        {
            /// <summary>
            /// Crea una petición específica para procesar SOAP, basado en sus parámetros.
            /// </summary>
            /// <param name="url">URL del método SOAP a ejecutar.</param>
            /// <param name="action">(Opcional) método a ejecutar.</param>
            /// <returns></returns>
            /// <remarks>En caso de que no se especifique un método SOAP a ejecutar, el método será la misma URL.</remarks>
            public static HttpWebRequest CreateWebRequest(string url, string action = "")
            {
                HttpWebRequest WebRequest;
                try
                {
                    if (action.Trim() == "") action = url;
                    WebRequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
                    WebRequest.Headers.Add("SOAPAction", action);
                    WebRequest.ContentType = "text/xml;charset=\"utf-8\"";
                    WebRequest.Accept = "text/xml";
                    WebRequest.Method = "POST";
                }
                catch (Exception ex)
                {
                    //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.CreateWebRequest", Detalle = ex.ToString() });
                    WebRequest = null;
                }
                return WebRequest;
            }

            /// <summary>
            /// Crea un envelope SOAP con base al contenido indicado.
            /// </summary>
            /// <param name="content">Contenido a cargar.</param>
            /// <returns></returns>
            public static XmlDocument MakeEnvelope(string content)
            {
                XmlDocument @out = null;
                try
                {
                    @out = new XmlDocument();
                    @out.LoadXml(content);
                }
                catch (Exception ex)
                {
                    //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.MakeEnvelope", Detalle = ex.ToString() });
                }
                return @out;
            }

            /// <summary>
            /// Inserta el contenido de la petición en el webrequest.
            /// </summary>
            /// <param name="soapEnvelopeXml">Contenido de la petición.</param>
            /// <param name="webRequest">Petición específica SOAP.</param>
            public static void InsertSoapEnvelopeIntoWebRequest(ref XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
            {
                using (Stream stream = webRequest.GetRequestStream())
                {
                    soapEnvelopeXml.Save(stream);
                }
            }

            /// <summary>
            /// Obtiene el valor específico de un elemento a consultar en el XML usando XPath.
            /// </summary>
            /// <param name="xml">Documento Xml a consultar.</param>
            /// <param name="Key">Nombre del elemento a consultar.</param>
            /// <returns></returns>
            /// <remarks>El método sólamente obtiene el primer valor coincidente con el nombre del elemento a consultar.</remarks>
            public static string GetValueFromXPath(XDocument xml, string Key)
            {
                string @out = "";
                try
                {
                    XElement myXml = XElement.Parse(xml.ToString());
                    Console.WriteLine(new Types.Tablas.Trace() {
                        AppId = 1,
                        Fecha = DateTime.Now,
                        Accion = "Elementos encontrados: " + myXml.XPathSelectElements(Key).Count().ToString(),
                        P1 = "ServiceClient.Common.GetValueFromXPath"
                    });
                    @out = myXml.XPathSelectElements(Key).FirstOrDefault().Value;
                }
                catch (Exception ex)
                {
                    // Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetValueFromXPath", Detalle = ex.ToString() });
                    @out = Utils.NothingValue;
                }
                return @out;
            }

            /// <summary>
            /// Obtiene el valor específico de un elemento a consultar en el XML.
            /// </summary>
            /// <param name="xml">Documento Xml a consultar.</param>
            /// <param name="Key">Nombre del elemento a consultar.</param>
            /// <returns></returns>
            /// <remarks>El método sólamente obtiene el primer valor coincidente con el nombre del elemento a consultar.</remarks>
            public static string GetValueOf(XDocument xml, string Key)
            {
                string @out = "";
                try
                {
                    @out = xml.Descendants().FirstOrDefault(p => p.Name.LocalName == Key).Value;
                }
                catch (Exception ex)
                {
                    // Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetValueOf", Detalle = ex.ToString() });
                    @out = Utils.NothingValue;
                }
                return @out;
            }

            /// <summary>
            /// Obtiene el valor específico de un elemento a consultar en el XML.
            /// </summary>
            /// <param name="xml">Documento Xml a consultar.</param>
            /// <param name="Key">Nombre del elemento a consultar.</param>
            /// <param name="searchFor">Texto a buscar en la respuesta (para campos con mismo nombre)</param>
            /// <returns></returns>
            /// <remarks>El método sólamente obtiene el primer valor coincidente con el nombre del elemento a consultar.</remarks>
            public static string GetValueOf(XDocument xml, string Key, string searchFor)
            {
                string @out = "";
                try
                {
                    @out = xml.Descendants().FirstOrDefault(p => p.Name.LocalName == Key && p.Value.Contains(searchFor)).Value;
                }
                catch (Exception ex)
                {
                    // Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetValueOf", Detalle = ex.ToString() });
                    @out = Utils.NothingValue;
                }
                return @out;
            }

            /// <summary>
            /// Obtiene el valor específico de un elemento a consultar en el elemento XML.
            /// </summary>
            /// <param name="xml">Elemento Xml a consultar.</param>
            /// <param name="Key">Nombre del elemento a consultar.</param>
            /// <returns></returns>
            /// <remarks>El método sólamente obtiene el primer valor coincidente con el nombre del elemento a consultar.</remarks>
            public static string GetValueOf(XElement xml, string Key)
            {
                string @out = "";
                try
                {
                    @out = xml.Descendants().FirstOrDefault(p => p.Name.LocalName == Key).Value;
                }
                catch (Exception ex)
                {
                    // Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetValueOf", Detalle = ex.ToString() });
                    @out = Utils.NothingValue;
                }
                return @out;
            }

            /// <summary>
            /// Obtiene el valor específico de un elemento a consultar en el elemento XML.
            /// </summary>
            /// <param name="xml">Elemento Xml a consultar.</param>
            /// <param name="Key">Nombre del elemento a consultar.</param>
            /// <param name="searchFor">Texto a buscar en la respuesta (para campos con mismo nombre)</param>
            /// <returns></returns>
            /// <remarks>El método sólamente obtiene el primer valor coincidente con el nombre del elemento a consultar.</remarks>
            public static string GetValueOf(XElement xml, string Key, string searchFor)
            {
                string @out = "";
                try
                {
                    @out = xml.Descendants().FirstOrDefault(p => p.Name.LocalName == Key).Value;
                }
                catch (Exception ex)
                {
                    // Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetValueOf", Detalle = ex.ToString() });
                    @out = Utils.NothingValue;
                }
                return @out;
            }

            /// <summary>
            /// Ejecuta un método SOAP y devuelve el string bruto de la respuesta obtenida.
            /// </summary>
            /// <param name="request">Petición SOAP a ejecutar.</param>
            /// <returns></returns>
            public static string GetSOAPResultString(HttpWebRequest request)
            {
                string @out = "";
                try
                {
                    IAsyncResult asyncResult = request.BeginGetResponse(null, null);
                    asyncResult.AsyncWaitHandle.WaitOne();
                    using (WebResponse webResponse = request.EndGetResponse(asyncResult))
                    {
                        try
                        {
                            using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                            {
                                @out = rd.ReadToEnd();
                            }
                        }
                        catch (WebException wex)
                        {
                            using (StreamReader rd = new StreamReader(wex.Response.GetResponseStream()))
                            {
                                @out = rd.ReadToEnd();
                                //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetSOAPResult", Detalle = request.Address + " ha respondido con error HTTP 500: " + @out });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetSOAPResult", Detalle = ex.ToString() });
                }
                return @out;
            }

            /// <summary>
            /// Ejecuta un método SOAP y devuelve el Xml de la respuesta obtenida.
            /// </summary>
            /// <param name="request">Petición SOAP a ejecutar.</param>
            /// <returns></returns>
            public static XDocument GetSOAPResult(HttpWebRequest request)
            {
                XDocument @value = null;
                string @out = "";
                try
                {
                    Console.WriteLine(new Types.Tablas.Trace("GetSOAPResult - Inicio de la secuencia") { P1 = request.RequestUri.AbsoluteUri });
                    Console.WriteLine(new Types.Tablas.Trace("Iniciando la petición"));
                    IAsyncResult asyncResult = request.BeginGetResponse(null, null);
                    asyncResult.AsyncWaitHandle.WaitOne();
                    using (WebResponse webResponse = request.EndGetResponse(asyncResult))
                    {
                        Console.WriteLine(new Types.Tablas.Trace("Intentando obtener la respuesta del servidor"));
                        try
                        {
                            using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                            {
                                @out = rd.ReadToEnd();
                            }
                            Console.WriteLine(new Types.Tablas.Trace("Se ha obtenido el resultado de la SOA.") { P1 = @out });
                        }
                        catch (WebException wex)
                        {
                            Console.WriteLine(new Types.Tablas.Trace("La SOA respondió con código 500. Obteniendo el valor del error. Encuéntrelo en el registro de erores."));
                            using (StreamReader rd = new StreamReader(wex.Response.GetResponseStream()))
                            {
                                @out = rd.ReadToEnd();
                                //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetSOAPResult", Detalle = request.Address + " ha respondido con error HTTP 500: " + @out });
                            }
                        }

                    }
                    Console.WriteLine(new Types.Tablas.Trace("GetSOAPResult: Resultado: " + @out));
                    @value = XDocument.Parse(@out);
                    Console.WriteLine(new Types.Tablas.Trace("GetSOAPResult: El XDocument fue cargado correctamente."));
                }
                catch (WebException wex)
                {
                    Console.WriteLine(new Types.Tablas.Trace("La SOA respondió con código 500. Obteniendo el valor del error. Encuéntrelo en el registro de erores."));
                    using (StreamReader rd = new StreamReader(wex.Response.GetResponseStream()))
                    {
                        @out = rd.ReadToEnd();
                        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetSOAPResult", Detalle = request.Address + " ha respondido con error HTTP 500: " + @out });
                        Console.WriteLine(new Types.Tablas.Trace("GetSOAPResult: Resultado: " + @out));
                        @value = XDocument.Parse(@out);
                        Console.WriteLine(new Types.Tablas.Trace("GetSOAPResult: El XDocument fue cargado correctamente."));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(new Types.Tablas.Trace("GETSOAPResult: Ha ocurrido un error. Consulte la tabla de errores para mayor información."));
                    //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetSOAPResult", Detalle = ex.ToString() + ". Resultado: " + @out });
                    @value = new XDocument();
                }
                return @value;
            }
        }

        /// <summary>
        /// Contiene las funciones de consumo de métodos SOAP de FullStack
        /// </summary>
        public class FullStack
        {
            #region WI11667 - Coliving Masivo
            /// <summary>
            /// Verifica si un número telefónico ha sido migrado a FullStack
            /// </summary>
            /// <param name="msisdn">Número telefónico a consultar</param>
            /// <returns></returns>
            //public static XDocument QueryMigrationByNumber_v1(string msisdn)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método QueryMigrationByNumber_v1", P1 = msisdn });
            //    XDocument @out = null;
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QuerymigrationByNumber");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        XmlDocument envelope = Common.MakeEnvelope(parametros.Values("QueryMigrationByNumberRequestTemplate").Replace("?", msisdn));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("QueryMigrationByNumberUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.QueryMigrationByNumber_v1", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}
            #endregion

            #region RS14694 - Coliving Corporativo
            /// <summary>
            /// Obtiene el mensaje a guardar en la incidencia FullStack
            /// </summary>
            /// <param name="input">Parámetros personalizados</param>
            /// <param name="parametros">Parámetros almacenados en la base de datos</param>
            /// <returns></returns>
            private static string GetInteractionMessage(Types.FullStack.CreateInteractionInput input, Types.Services.GetParametroCollectionResult parametros)
            {
                string @out = parametros.Values("CreateInteractionDefaultDescriptionBusinessInteraction" + input.Accion);
                @out = @out.Replace("::0::", input.NumeroCelular);
                @out = @out.Replace("::1::", input.Nombre);
                @out = @out.Replace("::2::", input.Apellido);
                @out = @out.Replace("::3::", input.TipoDocumento);
                @out = @out.Replace("::4::", input.NumeroDocumento);
                @out = @out.Replace("::5::", input.Usuario);
                @out = @out.Replace("::6::", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                @out = @out.Replace("::7::", input.Correo);
                @out = @out.Replace("::8::", input.NombreProducto);
                return @out;
            }



            /// <summary>
            /// Obtiene la información detallada de una cuenta de cliente.
            /// </summary>
            /// <param name="IDCustomerAccount">Identificador único del cliente a consultar.</param>
            /// <returns></returns>
            //public static XDocument GetAccountDetail_v1(string IDCustomerAccount)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetAccountDetail_v1", P1 = IDCustomerAccount });
            //    XDocument @out = null;
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetAccountDetail");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("GetAccountDetailRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("GetAccountDetailHeaderOperation")));
            //        string body = parametros.Values("GetAccountDetailRequestTemplateBody");
            //        body = body.Replace("::0::", IDCustomerAccount);
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.InnerXml.ToString(), P1 = envelope.InnerXml });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("GetAccountDetailUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.GetAccountDetail_v1", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Obtiene la información detallada de un cliente.
            /// </summary>
            /// <param name="NumeroIdentidad">Número de identificación.</param>
            /// <param name="TipoIdentidad">Tipo de documento de identidad (NIT)</param>
            /// <returns></returns>
            //public static XDocument GetCustomerDetail_v1(string NumeroIdentidad, string TipoIdentidad)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetCustomerDetail_v1", P1 = TipoIdentidad, P2 = NumeroIdentidad });
            //    XDocument @out = null;
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetCustomerDetail");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("GetCustomerDetailRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("GetCustomerDetailHeaderOperation")));
            //        string body = parametros.Values("GetCustomerDetailRequestTemplateBody");
            //        body = body.Replace("::0::", TipoIdentidad);
            //        body = body.Replace("::1::", NumeroIdentidad);
            //        template = template.Replace("::1::", body);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado el Envelope de la petición", P1 = template });
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("GetCustomerDetailUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.GetCustomerDetail_v1", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Obtiene la lista de productos de un cliente.
            /// </summary>
            /// <param name="IDCustomer">Identificador del cliente.</param>
            /// <returns></returns>
            //public static XDocument GetSuscriberList_v1(string IDCustomer)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetSuscriberList_v1", P1 = IDCustomer });
            //    XDocument @out = null;
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetSuscriberList");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("GetSuscriberListRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("GetSuscriberListHeaderOperation")));
            //        string body = parametros.Values("GetSuscriberListRequestTemplateBody");
            //        body = body.Replace("::0::", IDCustomer);
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("GetSuscriberListUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.GetSuscriberList_v1", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Obtiene la lista de productos de una cuenta.
            /// </summary>
            /// <param name="AcctCode">Identificador de la cuenta.</param>
            /// <returns></returns>
            //public static XDocument GetSuscriberList_v1_byAcctCode(string AcctCode)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetSuscriberList_v1_byAcctCode", P1 = AcctCode });
            //    XDocument @out = null;
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetSuscriberList");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("GetSuscriberListRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("GetSuscriberListHeaderOperation")));
            //        string body = parametros.Values("GetSuscriberListRequestTemplateBodyAcctCode");
            //        body = body.Replace("::0::", AcctCode);
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("GetSuscriberListUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.GetSuscriberList_v1_byAcctCode", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Obtiene la lista de bancos disponibles para pagos PSE.
            /// </summary>
            /// <returns></returns>
            //public static XDocument GetBankListOp_v1()
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetBankListOp_v1" });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetBankListOp");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("GetBankListOpRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("GetBankListOpHeaderOperation")));
            //        string body = parametros.Values("GetBankListOpRequestTemplateBody");
            //        body = body.Replace("::0::", parametros.Values("GetBankListOpPsePublicKey"));
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("GetBankListOpUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.GetBankListOp_v1", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Verifica si una cuenta de cliente ha sido migrada a FullStack
            /// </summary>
            /// <param name="CustomerId">Identificador del cliente.</param>
            /// <returns></returns>
            //public static XDocument QueryMigrationByAccount_v1(string CustomerId)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método QueryMigrationByAccount_v1", P1 = CustomerId });
            //    XDocument @out = null;
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryMigrationByAccount");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("QueryMigrationByAccountRequestTemplate");
            //        string body = parametros.Values("QueryMigrationByAccountRequestTemplateBody");
            //        body = body.Replace("::0::", CustomerId);
            //        template = template.Replace("::0::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("QueryMigrationByAccountUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.QueryMigrationByAccount_v1", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Obtiene la información de facturación de un número especificado
            /// </summary>
            /// <param name="primaryTelephoneNumber">Número telefónico a consultar.</param>
            /// <returns></returns>
            //public static XDocument QueryInvoice(string primaryTelephoneNumber)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método QueryInvoice", P1 = primaryTelephoneNumber });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryInvoice");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("QueryInvoiceRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("QueryInvoiceHeaderOperation")));
            //        string body = parametros.Values("QueryInvoiceRequestTemplateBody");
            //        body = body.Replace("::0::", primaryTelephoneNumber);
            //        body = body.Replace("::1::", parametros.Values("QueryInvoiceDefaultValuesSecondReqFlag"));
            //        body = body.Replace("::2::", parametros.Values("QueryInvoiceDefaultValuesBeginRowNum"));
            //        body = body.Replace("::3::", parametros.Values("QueryInvoiceDefaultValuesFetchRowNum"));
            //        body = body.Replace("::4::", parametros.Values("QueryInvoiceDefaultValuesTotalRowNum"));
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.InnerXml.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("QueryInvoiceUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.QueryInvoice", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Obtiene la información de facturación de una cuenta especificado
            /// </summary>
            /// <param name="acctCode">Número de cuenta a consultar.</param>
            /// <returns></returns>
            //public static XDocument QueryInvoiceByAcctCode(string acctCode)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método QueryInvoiceByAcctCode", P1 = acctCode });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryInvoice");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("QueryInvoiceRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("QueryInvoiceHeaderOperation")));
            //        string body = parametros.Values("QueryInvoiceRequestTemplateBodyAcctCode");
            //        body = body.Replace("::0::", acctCode);
            //        body = body.Replace("::1::", parametros.Values("QueryInvoiceDefaultValuesSecondReqFlag"));
            //        body = body.Replace("::2::", parametros.Values("QueryInvoiceDefaultValuesBeginRowNum"));
            //        body = body.Replace("::3::", parametros.Values("QueryInvoiceDefaultValuesFetchRowNum"));
            //        body = body.Replace("::4::", parametros.Values("QueryInvoiceDefaultValuesTotalRowNum"));
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.InnerXml.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("QueryInvoiceUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.QueryInvoiceByAcctCode", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Pago de facturas.
            /// </summary>
            /// <param name="input">Parámetros de entrada.</param>
            /// <returns></returns>
            //public static XDocument PayOperation_Pagar(Types.BusinessLogic.GeneraOrdenInput input)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método ServiceClient.FullStack.PayOperation en modo Pagar", P1 = JsonConvert.SerializeObject(input) });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("PayOperation");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("PayOperationRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("PayOperationHeaderOperation")));
            //        string body = parametros.Values("PayOperationRequestTemplateBody");
            //        body = body.Replace("::0::", parametros.Values("PayOperationDefaultPersonType"));
            //        body = body.Replace("::1::", input.BancoCodigo);
            //        body = body.Replace("::2::", input.BancoNombre);
            //        body = body.Replace("::3::", input.Direccion);
            //        body = body.Replace("::4::", input.Ciudad);
            //        body = body.Replace("::5::", input.Departamento);
            //        body = body.Replace("::6::", input.CodigoPostal);
            //        body = body.Replace("::7::", input.IdCustomer);
            //        body = body.Replace("::8::", parametros.Values("PayOperationDefaultTypeNationalCardIdentification"));
            //        body = body.Replace("::9::", input.NumeroNit);
            //        body = body.Replace("::10::", input.RazonSocial);
            //        body = body.Replace("::11::", input.ContactoTelefono);
            //        body = body.Replace("::12::", input.ContactoCorreo);
            //        body = body.Replace("::13::", input.Monto.ToString("0"));
            //        body = body.Replace("::14::", parametros.Values("PayOperationDefaultCurrencyParameter"));
            //        body = body.Replace("::15::", parametros.Values("PayOperationDefaultPaymentMethod"));
            //        body = body.Replace("::16::", parametros.Values("PayOperationDefaultTransactionCode"));
            //        body = body.Replace("::17::", input.UrlRetorno);
            //        body = body.Replace("::18::", input.IdServiceOrder);
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("PayOperationUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.PayOperation", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Pago de facturas.
            /// </summary>
            /// <param name="input">Parámetros de entrada.</param>
            /// <returns></returns>
            //public static XDocument PayOperation(Types.FullStack.Pagos.InputPago input)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método ServiceClient.FullStack.PayOperation", P1 = JsonConvert.SerializeObject(input) });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("PayOperation");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("PayOperationRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("PayOperationHeaderOperation")));
            //        string body = parametros.Values("PayOperationRequestTemplateBody");
            //        body = body.Replace("::0::", parametros.Values("PayOperationDefaultPaymentMethod"));
            //        body = body.Replace("::1::", parametros.Values("PayOperationDefaultPartyIdOrganization"));
            //        if (input.Direccion.Trim() != "")
            //        {
            //            body = body.Replace("::2::", input.Direccion);
            //            body = body.Replace("::3::", input.Ciudad);
            //            body = body.Replace("::4::", input.Departamento);
            //            body = body.Replace("::5::", input.CodGeografico);
            //            body = body.Replace("::6::", input.CodPostal);
            //        }
            //        body = body.Replace("::7::", input.CodCliente);
            //        body = body.Replace("::8::", parametros.Values("PayOperationDefaultPersonType"));
            //        body = body.Replace("::9::", input.Segmento);
            //        body = body.Replace("::10::", parametros.Values("PayOperationDefaultTypeNationalCardIdentification"));
            //        /* ***** WI49544 - Inicio ***** */
            //        body = body.Replace("::11::", input.NumeroDocumento);
            //        /* ***** WI49544 - Final  ***** */
            //        body = body.Replace("::12::", input.NombreRazonSocial);
            //        body = body.Replace("::13::", input.Numero);
            //        if (input.Correo.Trim() != "") body = body.Replace("::14::", input.Correo);
            //        body = body.Replace("::15::", input.Monto.ToString("0"));
            //        body = body.Replace("::16::", parametros.Values("PayOperationDefaultTaxAmount"));
            //        body = body.Replace("::17::", parametros.Values("PayOperationDefaultCurrencyParameter"));
            //        if (input.TipoPago == Types.FullStack.Pagos.TiposPago.PagoFactura)
            //        {
            //            string factura = parametros.Values("PayOperationDefaultInvoiceContainer");
            //            factura = factura.Replace("::0::", input.Factura);
            //            body = body.Replace("::18::", factura);
            //        }
            //        else
            //        {
            //            body = body.Replace("::18::", "");
            //        }
            //        body = body.Replace("::19::", parametros.Values("PayOperationDefaultFeeTypeBusiness"));
            //        body = body.Replace("::20::", input.Monto.ToString("0"));
            //        body = body.Replace("::21::", input.IdBanco);
            //        body = body.Replace("::22::", input.Banco);
            //        body = body.Replace("::23::", parametros.Values("PayOperationDefaultDescriptionPaymentMethod"));
            //        body = body.Replace("::24::", parametros.Values("PayOperationDefaultAccessChannel"));
            //        body = body.Replace("::25::", input.CodCliente);
            //        if (input.TipoPago == Types.FullStack.Pagos.TiposPago.PagoFactura)
            //        {
            //            body = body.Replace("::26::", parametros.Values("PayOperationDefaultPagoIdTransactionCode"));
            //        }
            //        else
            //        {
            //            body = body.Replace("::26::", parametros.Values("PayOperationDefaultRecargaIdTransactionCode"));
            //        }
            //        body = body.Replace("::27::", input.SessionID);
            //        body = body.Replace("::28::", parametros.Values("PayOperationDefaultCookie"));
            //        body = body.Replace("::29::", input.IPAddress);
            //        body = body.Replace("::30::", parametros.Values("PayOperationDefaultCUSPaymentOp"));
            //        body = body.Replace("::31::", input.UrlRetorno);
            //        /* ***** WI49544 - Inicio ***** */
            //        body = body.Replace("::32::", input.Cli_Hijo);
            //        /* ***** WI49544 - Final  ***** */
            //        template = template.Replace("::1::", body);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Request creado", P1 = template });
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("PayOperationUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.PayOperation", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Verifica el estado de procesos de pago.
            /// </summary>
            /// <param name="input">Parámetros de entrada.</param>
            /// <returns></returns>
            //public static XDocument PayOperationVerify(Types.FullStack.Pagos.InputVerifyPago input)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método ServiceClient.FullStack.PayOperationVerify", P1 = JsonConvert.SerializeObject(input) });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("PayOperation");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("PayOperationRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("PayOperationHeaderOperation")));
            //        string body = parametros.Values("PayOperationRequestTemplateBodyVerify");
            //        body = body.Replace("::0::", parametros.Values("PayOperationDefaultVerifyTypeNationalIdentity"));
            //        body = body.Replace("::1::", parametros.Values("PayOperationDefaultVerifyNrPassportIdentification"));
            //        body = body.Replace("::2::", parametros.Values("PayOperationDefaultVerifyLegalName"));
            //        body = body.Replace("::3::", parametros.Values("PayOperationDefaultVerifyEmail"));
            //        body = body.Replace("::4::", parametros.Values("PayOperationDefaultVerifyAmount"));
            //        body = body.Replace("::5::", parametros.Values("PayOperationDefaultVerifyCurrencyParameter"));
            //        body = body.Replace("::6::", parametros.Values("PayOperationDefaultVerifyDescriptionPaymentMethod"));
            //        body = body.Replace("::7::", parametros.Values("PayOperationDefaultVerifyIdTransactionCode"));
            //        body = body.Replace("::8::", input.IdTicket);
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("PayOperationUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.PayOperationVerify", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Obtiene la URL de un documento electrónico.
            /// </summary>
            /// <param name="number">Número a consultar</param>
            /// <param name="typeDocument">Tipo de documento a consultar</param>
            /// <returns></returns>
            //public static XDocument QueryDocumentURL(string number, string typeDocument)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método QueryDocumentURL", P1 = number, P2 = typeDocument });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryDocument");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("QueryDocumentRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("QueryDocumentHeaderOperation")));
            //        string body = parametros.Values("QueryDocumentRequestTemplateBody");
            //        body = body.Replace("::0::", number);
            //        body = body.Replace("::1::", parametros.Values("QueryDocumentDefaultTypeDocument" + typeDocument));
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("QueryDocumentUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.QuerDocumentURL", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            ///// <summary>
            ///// Obtiene el histórico de pagos de un cliente.
            ///// </summary>
            ///// <param name="AccountId">Id de cliente a consultar</param>
            ///// <returns></returns>
            //public static XDocument QueryPaymentLog(Types.FullStack.Pagos.InputHistorico input)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método QueryPaymentLog", P1 = JsonConvert.SerializeObject(input) });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryPaymentLog");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("QueryPaymentLogRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("QueryPaymentLogHeaderOperation")));
            //        string body = parametros.Values("QueryPaymentLogRequestTemplateBody");
            //        body = body.Replace("::0::", input.Cuenta);
            //        body = body.Replace("::1::", input.FechaInicio);
            //        body = body.Replace("::2::", input.FechaFinal);
            //        body = body.Replace("::3::", parametros.Values("QueryPaymentLogDefaultTotalRowNum"));
            //        body = body.Replace("::4::", parametros.Values("QueryPaymentLogDefaultBeginRowNum"));
            //        body = body.Replace("::5::", parametros.Values("QueryPaymentLogDefaultFetchRowNum"));
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("QueryPaymentLogUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.QueryPaymentLog", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            /// <summary>
            /// Obtiene el listado de categorías de SVAs a ofrecer.
            /// </summary>
            /// <returns></returns>
            //public static XDocument QueryOfferingCategory()
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método QueryOfferingCategory" });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryOfferingCategory");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("QueryOfferingCategoryRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("QueryOfferingCategoryHeaderOperation")));
            //        string body = parametros.Values("QueryOfferingCategoryRequestTemplateBody");
            //        body = body.Replace("::0::", parametros.Values("QueryOfferingCategoryDefaultValuesCategory"));
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.Value });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("QueryOfferingCategoryUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out, P1 = @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.QueryOfferingCategory", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            //public static XDocument QuerySuscriberAvailableOffering(string number, string category)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace("QuerySuscriberAvailableOffering - Inicio de la secuencia") { P1 = number, P2 = category });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QuerySuscriberAvailableOffering");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "QuerySuscriberAvailableOffering - Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("QuerySuscriberAvailableOfferingRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("QuerySuscriberAvailableOfferingHeaderOperation")));
            //        string body = parametros.Values("QuerySuscriberAvailableOfferingRequestTemplateBody");
            //        body = body.Replace("::0::", number);
            //        body = body.Replace("::1::", category);
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "QuerySuscriberAvailableOffering - Envelope creado: " + template, P1 = template });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "QuerySuscriberAvailableOffering - Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("QuerySuscriberAvailableOfferingUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "QuerySuscriberAvailableOffering - Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "QuerySuscriberAvailableOffering - Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "QuerySuscriberAvailableOffering - Resultado de la petición: " + @out, P1 = @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace("QuerySuscriberAvailableOffering - Ha ocurrido un error: " + ex.Message));
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.QuerySuscriberAvailableOffering", Detalle = ex.ToString() });
            //        @out = new XDocument();
            //    }
            //    return @out;
            //}

            //public static XDocument ChangeSuplementaryOffering(string number, string idOffer)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace("ChangeSuplementaryOffering - Inicio de la secuencia") { P1 = number, P2 = idOffer });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("ChangeSuplementaryOffering");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "ChangeSuplementaryOffering - Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("ChangeSuplementaryOfferingRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("ChangeSuplementaryOfferingHeaderOperation")));
            //        string body = parametros.Values("ChangeSuplementaryOfferingRequestTemplateBody");
            //        body = body.Replace("::0::", number);
            //        body = body.Replace("::1::", idOffer);
            //        body = body.Replace("::2::", parametros.Values("ChangeSuplementaryOfferingRequestDefaultValuesIDBusinessInteraction"));
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "ChangeSuplementaryOffering - Envelope creado: " + template, P1 = template });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "ChangeSuplementaryOffering - Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("ChangeSuplementaryOfferingUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "ChangeSuplementaryOffering - Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "ChangeSuplementaryOffering - Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "ChangeSuplementaryOffering - Resultado de la petición: " + @out, P1 = @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace("ChangeSuplementaryOffering - Ha ocurrido un error: " + ex.Message));
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.ChangeSuplementaryOffering", Detalle = ex.ToString() });
            //        @out = new XDocument();
            //    }
            //    return @out;
            //}

            ///// <summary>
            ///// Activa la factura electrónica
            ///// </summary>
            ///// <param name="IDCustomerAccount">Código de cuenta</param>
            ///// <param name="Email">Correo electrónico para apuntar la factura electrónica</param>
            ///// <returns></returns>
            //public static XDocument ModifyBillMedium(string IDCustomerAccount, string Email)
            //{
            //    Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método ModifyBillMedium", P1 = IDCustomerAccount, P2 = Email });
            //    XDocument @out = new XDocument();
            //    try
            //    {
            //        // Obtenemos los parámetros desde la tabla.
            //        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("ModifyBillMedium");
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Registros: " + parametros.Items.Count.ToString() });
            //        string template = parametros.Values("ModifyBillMediumRequestTemplate");
            //        template = template.Replace("::0::", Data.GetSoapHeader(parametros.Values("ModifyBillMediumOperation")));
            //        string body = parametros.Values("ModifyBillMediumRequestTemplateBody");
            //        body = body.Replace("::0::", IDCustomerAccount);
            //        body = body.Replace("::1::", parametros.Values("ModifyBillMediumDefaultValuesValueParameter"));
            //        body = body.Replace("::2::", parametros.Values("ModifyBillMediumDefaultValuesContentModeBillMedium"));
            //        body = body.Replace("::3::", parametros.Values("ModifyBillMediumDefaultValuesContactTypePartyAccountContact"));
            //        body = body.Replace("::4::", Email);
            //        template = template.Replace("::1::", body);
            //        XmlDocument envelope = Common.MakeEnvelope(template);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.InnerXml.ToString() });
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
            //        HttpWebRequest request = Common.CreateWebRequest(parametros.Values("ModifyBillMediumUrl"));
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
            //        Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
            //        @out = Common.GetSOAPResult(request);
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            //        //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.ModifyBillMedium", Detalle = ex.ToString() });
            //    }
            //    return @out;
            //}

            #endregion
        }
    }
}
