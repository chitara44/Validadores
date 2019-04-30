using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Tmc.Servicios.FullStack;
using static Tmc.Servicios.FullStack.ServiceClient;

namespace encrypterAES256
{


    class EncryptingClass
    {

        private static string ConnectionString
        {
            get
            {
                return Properties.Settings.Default.cnPrepagos;
            }
        }

        #region Encryption
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iKeySize"></param>
        /// <param name="modo"></param>
        /// <param name="IVDef"></param>
        /// <param name="keyStrDef"></param>
        /// <returns></returns>
        public static string GenerateKey(int iKeySize, string modo, TextBox t2, TextBox t3 )
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
            //aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Mode = (modo.Equals("CBC")) ? CipherMode.CBC : CipherMode.ECB;
            aesEncryption.Padding = PaddingMode.PKCS7;
            
            string ivStr = string.Empty;
            if (t2.Text == string.Empty)
            {
                aesEncryption.GenerateIV();
                ivStr = Convert.ToBase64String(aesEncryption.IV);
                aesEncryption.GenerateKey();
            }
            else
            {
                aesEncryption.IV = ASCIIEncoding.UTF8.GetBytes(t2.Text);
                ivStr = t2.Text;
                aesEncryption.GenerateKey();
            }

            string keyStr = Convert.ToBase64String(aesEncryption.Key);
            string completeKey = Convert.ToBase64String(ASCIIEncoding.UTF8.GetBytes(ivStr + "," + keyStr));
            return completeKey;
        }

        /// <summary>
        /// funcion para encriptar en AES256
        /// </summary>
        /// <param name="iPlainStr"></param>
        /// <param name="iCompleteEncodedKey"></param>
        /// <param name="iKeySize"></param>
        /// <param name="modo"></param>
        /// <returns></returns>
        public static string Encrypt(string iPlainStr, string iCompleteEncodedKey, int iKeySize, string modo, TextBox tIV, TextBox tKey)
        {
            byte[] uno;
            byte[] dos;
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
           
            aesEncryption.Mode = ( modo.Equals("CBC")) ?  CipherMode.CBC: CipherMode.ECB;
            aesEncryption.Padding = PaddingMode.PKCS7;
            string[] completeKey = ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(iCompleteEncodedKey)).Split(',');
            uno = Encoding.UTF8.GetBytes(completeKey[0].ToString());
            aesEncryption.IV = uno; // Convert.FromBase64String(completeKey[0].ToString());
            tIV.Text = completeKey[0].ToString();
            //Convert.ToBase64String(d);
            //tIV.Text = ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(completeKey[0]));
            dos = Encoding.UTF8.GetBytes(completeKey[1].ToString());
            //aesEncryption.Key = Convert.ToBase64String(completeKey[1].ToString());
            aesEncryption.Key = dos;
            tKey.Text = completeKey[1].ToString();
            byte[] plainText = ASCIIEncoding.UTF8.GetBytes(iPlainStr);
            ICryptoTransform crypto = aesEncryption.CreateEncryptor();
            byte[] cipherText = crypto.TransformFinalBlock(plainText, 0, plainText.Length);
            return Convert.ToBase64String(cipherText);
        }

        /// <summary>
        /// funcion para desencriptar de AES256
        /// </summary>
        /// <param name="iEncryptedText"></param>
        /// <param name="iCompleteEncodedKey"></param>
        /// <param name="iKeySize"></param>
        /// <returns></returns>
        public static string Decrypt(string iEncryptedText, string iCompleteEncodedKey, int iKeySize)
        {
            RijndaelManaged aesEncryption = new RijndaelManaged();
            aesEncryption.KeySize = iKeySize;
            aesEncryption.BlockSize = 128;
            aesEncryption.Mode = CipherMode.CBC;
            aesEncryption.Padding = PaddingMode.PKCS7;
            string[] completeKey = ASCIIEncoding.UTF8.GetString(Convert.FromBase64String(iCompleteEncodedKey)).Split(',');
            aesEncryption.IV = Convert.FromBase64String(completeKey[0]);
            aesEncryption.Key = Convert.FromBase64String(completeKey[1]);
            ICryptoTransform decrypto = aesEncryption.CreateDecryptor();
            byte[] encryptedBytes = Convert.FromBase64CharArray(iEncryptedText.ToCharArray(), 0, iEncryptedText.Length);
            return ASCIIEncoding.UTF8.GetString(decrypto.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length));
        }


        public static string GetIPAddress()
        {
            String address = ""; 
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            //WebRequest request = WebRequest.Create("https://f4.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/index.php/ack");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }

        public static XDocument GetSOAPResultCOCorp(HttpWebRequest request)
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
                            Console.WriteLine(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetSOAPResult", Detalle = request.Address + " ha respondido con error HTTP 500: " + @out });
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
                    Console.WriteLine(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetSOAPResult", Detalle = request.Address + " ha respondido con error HTTP 500: " + @out });
                    Console.WriteLine(new Types.Tablas.Trace("GetSOAPResult: Resultado: " + @out));
                    @value = XDocument.Parse(@out);
                    Console.WriteLine(new Types.Tablas.Trace("GetSOAPResult: El XDocument fue cargado correctamente."));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace("GETSOAPResult: Ha ocurrido un error. Consulte la tabla de errores para mayor información."));
                Console.WriteLine(new Types.Tablas.Exception() { Modulo = "ServiceClient.Common.GetSOAPResult", Detalle = ex.ToString() + ". Resultado: " + @out });
                @value = new XDocument();
            }
            return @value;
        }


        public static Types.Services.GetCusstomerDetailResult GetCustomerDetail2(string IpOri, string ClaveCod)
        {
            //Console.WriteLine(new Types.Tablas.Trace("Se ha convocado el método BusinessLogic.GetCustomerDetail") { P1 = TipoIdentidad, P2 = NumeroIdentidad });
            Types.Services.GetCusstomerDetailResult @out = new Types.Services.GetCusstomerDetailResult();
            XDocument resultado = new XDocument();
            XDocument resultad2 = new XDocument();
            XDocument resultad3 = new XDocument();
            XDocument resultad4 = new XDocument();
            XDocument resultad5 = new XDocument();
            try
            {
                string constante = "1";
                string usuario = "Usr_Ws_L_WEB_002";
                string passwd = "YVcyKkE5Kl9SdFk5cUoqNDI2";
                resultado = EncryptingClass.GetACKOperation(IpOri, ClaveCod,  constante, usuario,  passwd);
                Console.WriteLine(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados"));
                string respuestaACK = Common.GetValueOf(resultado, "return");
                resultad2 = EncryptingClass.Generar_URL_Segura_Facturas("1001777992", IpOri, "1", System.DateTime.Now.ToShortDateString());
                resultad2 = EncryptingClass.Generar_WB_Factura(respuestaACK, "2");

            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace("Se cargan los parámetros de manejo de errores"));
            }
            return @out;
        }

        public static XDocument GetACKOperation(string iporigen, string clavecod, string constante, string usuario, string passwd )
        {
            XDocument @out = null;
            try
            {
                // Obtenemos los parámetros desde la tabla.
                string template = "<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:wsdl='https://f3.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl'> ::0:: ::1::  </soapenv:Envelope>";
                template = template.Replace("::0::", "<soapenv:Header />");
                string body = string.Empty;
                body = "<soapenv:Body>      <wsdl:ack soapenv:encodingStyle = 'http://schemas.xmlsoap.org/soap/encoding/'>          <i1 xsi:type = 'xsd:string' >::0::</i1>                <c2 xsi:type = 'xsd:string' >::1::</c2>                    <t3 xsi:type = 'xsd:string' >::2::</t3>                         <u1 xsi:type = 'xsd:string' >::3::</u1>                              <p1 xsi:type = 'xsd:string' >::4::</p1>                                </wsdl:ack>                              </soapenv:Body> "; //parametros.Values("GetCustomerDetailRequestTemplateBody");
                body = body.Replace("::0::", iporigen);
                body = body.Replace("::1::", clavecod);
                body = body.Replace("::2::", constante);
                body = body.Replace("::3::", usuario);
                body = body.Replace("::4::", passwd);
                template = template.Replace("::1::", body);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado el Envelope de la petición", P1 = template });
                XmlDocument envelope = Common.MakeEnvelope(template);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
                HttpWebRequest request = Common.CreateWebRequest("https://f4.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl");
                Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
                Console.WriteLine("Ejecutando la petición web");
                @out = Common.GetSOAPResult(request);
                Console.WriteLine("Resultado de la petición: " + @out.ToString() );
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            }
            return @out;
        }

        public static XDocument Generar_Fact_Pdf(string ackResult, string fechacorte, string prod, string tipodoc)
        {
            //Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetCustomerDetail_v1", P1 = TipoIdentidad, P2 = NumeroIdentidad });
            XDocument @out = null;
            try
            {
                // Obtenemos los parámetros desde la tabla.
                string template = "<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:wsdl='https://f3.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl'> ::0:: ::1::  </soapenv:Envelope>";
                template = template.Replace("::0::", "<soapenv:Header />");
                string body = string.Empty;
                body = "<soapenv:Body>  <wsdl:generar_fact_pdf soapenv:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>	 <ack xsi:type='xsd:string'>::0::</ack>	 <fecha_corte xsi:type='xsd:string'>::1::</fecha_corte>	 <prod xsi:type='xsd:string'>::2::</prod>	 <tipo_documento xsi:type='xsd:string'>::3::</tipo_documento>  </wsdl:generar_fact_pdf></soapenv:Body>";
                body = body.Replace("::0::", ackResult);
                body = body.Replace("::1::", fechacorte);
                body = body.Replace("::2::", prod);
                body = body.Replace("::3::", tipodoc);

                template = template.Replace("::1::", body);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado el Envelope de la petición", P1 = template });
                XmlDocument envelope = Common.MakeEnvelope(template);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
                HttpWebRequest request = Common.CreateWebRequest("https://f4.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl");
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
                Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
                @out = Common.GetSOAPResult(request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
                //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.GetCustomerDetail_v1", Detalle = ex.ToString() });
            }
            return @out;
        }

        public static XDocument Generar_Fact_Pdf_Str(string ackResult, string fechacorte, string prod, string tipodoc)
        {
            //Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetCustomerDetail_v1", P1 = TipoIdentidad, P2 = NumeroIdentidad });
            XDocument @out = null;
            try
            {
                // Obtenemos los parámetros desde la tabla.
                string template = "<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:wsdl='https://f3.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl'> ::0:: ::1::  </soapenv:Envelope>";
                template = template.Replace("::0::", "<soapenv:Header />");
                string body = string.Empty;
                body = "<soapenv:Body>  <wsdl:generar_fact_pdf_in_str soapenv:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>	 <ack xsi:type='xsd:string'>::0::</ack>	 <fecha_corte xsi:type='xsd:string'>::1::</fecha_corte>	 <prod xsi:type='xsd:string'>::2::</prod>	 <tipo_documento xsi:type='xsd:string'>::3::</tipo_documento>  </wsdl:generar_fact_pdf_in_str></soapenv:Body>";
                body = body.Replace("::0::", ackResult);
                body = body.Replace("::1::", fechacorte);
                body = body.Replace("::2::", prod);
                body = body.Replace("::3::", tipodoc);

                template = template.Replace("::1::", body);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado el Envelope de la petición", P1 = template });
                XmlDocument envelope = Common.MakeEnvelope(template);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
                HttpWebRequest request = Common.CreateWebRequest("https://f4.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl");
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
                Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
                @out = Common.GetSOAPResult(request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            }
            return @out;
        }

        public static XDocument Generar_Lista_Disponible(string ackResult, string prod)
        {
            XDocument @out = null;
            try
            {
                // Obtenemos los parámetros desde la tabla.
                string template = "<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:wsdl='https://f3.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl'> ::0:: ::1::  </soapenv:Envelope>";
                template = template.Replace("::0::", "<soapenv:Header />");
                string body = string.Empty;
                body = "<soapenv:Body>  <wsdl:generar_lista_disponible soapenv:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>	 <ack xsi:type='xsd:string'>::0::</ack>	 <prod xsi:type='xsd:string'>::1::</prod>  </wsdl:generar_lista_disponible></soapenv:Body>";
                body = body.Replace("::0::", ackResult);
                body = body.Replace("::1::", prod);
                template = template.Replace("::1::", body);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado el Envelope de la petición", P1 = template });
                XmlDocument envelope = Common.MakeEnvelope(template);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
                HttpWebRequest request = Common.CreateWebRequest("https://f4.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl");
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
                Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
                @out = Common.GetSOAPResult(request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
            }
            return @out;
        }

        public static XDocument Generar_URL_Lista_Facturas(string ackResult, string prod)
        {
            //Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetCustomerDetail_v1", P1 = TipoIdentidad, P2 = NumeroIdentidad });
            XDocument @out = null;
            try
            {
                // Obtenemos los parámetros desde la tabla.
                string template = "<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:wsdl='https://f3.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl'> ::0:: ::1::  </soapenv:Envelope>";
                template = template.Replace("::0::", "<soapenv:Header />");
                string body = string.Empty;
                body = "<soapenv:Body>  <wsdl:generar_url_lista_facturas soapenv:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>	 <ack xsi:type='xsd:string'>::0::</ack>	 <prod xsi:type='xsd:string'>::1::</prod>  </wsdl:generar_url_lista_facturas></soapenv:Body>";
                body = body.Replace("::0::", ackResult);
                body = body.Replace("::1::", prod);
                template = template.Replace("::1::", body);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado el Envelope de la petición", P1 = template });
                XmlDocument envelope = Common.MakeEnvelope(template);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
                HttpWebRequest request = Common.CreateWebRequest("https://f4.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl");
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
                Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
                @out = Common.GetSOAPResult(request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
                //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.GetCustomerDetail_v1", Detalle = ex.ToString() });
            }
            return @out;
        }

        public static XDocument Generar_URL_Segura_Facturas(string nit, string ipCli, string prod, string dateAct)
        {
            //Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetCustomerDetail_v1", P1 = TipoIdentidad, P2 = NumeroIdentidad });
            XDocument @out = null;
            try
            {
                // Obtenemos los parámetros desde la tabla.
                string template = "<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:wsdl='https://f3.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl'> ::0:: ::1::  </soapenv:Envelope>";
                template = template.Replace("::0::", "<soapenv:Header />");
                string body = string.Empty;
                body = "<soapenv:Body>  <wsdl:generar_url_segura_facturas soapenv:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>	 <EntConCliente xsi:type='wsdl:Datos_Consulta'>		<!--You may enter the following 4 items in any order-->		<cuenta xsi:type='xsd:string'>::0::</cuenta>		<ip_usr xsi:type='xsd:string'>::1::</ip_usr>		<prod xsi:type='xsd:string'>::2::</prod>		<hora_actual xsi:type='xsd:string'>::3::</hora_actual>	 </EntConCliente>  </wsdl:generar_url_segura_facturas></soapenv:Body>";
                body = body.Replace("::0::", nit);
                body = body.Replace("::1::", ipCli);
                body = body.Replace("::2::", prod);
                body = body.Replace("::3::", dateAct);
                template = template.Replace("::1::", body);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado el Envelope de la petición", P1 = template });
                XmlDocument envelope = Common.MakeEnvelope(template);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
                HttpWebRequest request = Common.CreateWebRequest("https://f4.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl");
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
                Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
                @out = Common.GetSOAPResult(request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
                //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.GetCustomerDetail_v1", Detalle = ex.ToString() });
            }
            return @out;
        }

        public static XDocument Generar_WB_Factura(string ackResult, string prod)
        {
            //Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetCustomerDetail_v1", P1 = TipoIdentidad, P2 = NumeroIdentidad });
            XDocument @out = null;
            try
            {
                // Obtenemos los parámetros desde la tabla.
                string template = "<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:wsdl='https://f3.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl'> ::0:: ::1::  </soapenv:Envelope>";
                template = template.Replace("::0::", "<soapenv:Header />");
                string body = string.Empty;
                body = "<soapenv:Body>  <wsdl:generar_WB_Factura soapenv:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>	 <ack xsi:type='xsd:string'>::0::</ack>	 <prod xsi:type='xsd:string'>::1::</prod>  </wsdl:generar_WB_Factura></soapenv:Body>";
                body = body.Replace("::0::", ackResult);
                body = body.Replace("::1::", prod);
                template = template.Replace("::1::", body);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado el Envelope de la petición", P1 = template });
                XmlDocument envelope = Common.MakeEnvelope(template);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
                HttpWebRequest request = Common.CreateWebRequest("https://f4.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl");
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
                Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
                @out = Common.GetSOAPResult(request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
                //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.GetCustomerDetail_v1", Detalle = ex.ToString() });
            }
            return @out;
        }

        public static XDocument Registrar_Factura_Electronica_Email(string ackResult, string prod)
        {
            //Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha convocado el método GetCustomerDetail_v1", P1 = TipoIdentidad, P2 = NumeroIdentidad });
            XDocument @out = null;
            try
            {
                // Obtenemos los parámetros desde la tabla.
                string template = "<soapenv:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soapenv='http://schemas.xmlsoap.org/soap/envelope/' xmlns:wsdl='https://f3.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl'> ::0:: ::1::  </soapenv:Envelope>";
                template = template.Replace("::0::", "<soapenv:Header />");
                string body = string.Empty;
                body = "<soapenv:Body>  <wsdl:reg_fac_electro_email soapenv:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>	 <ack xsi:type='xsd:string'>::0::</ack>	 <fecha_corte xsi:type='xsd:string'>::1::</fecha_corte>	 <prod xsi:type='xsd:string'>::2::</prod>	 <email xsi:type='xsd:string'>::3::</email>	 <tipo_documento xsi:type='xsd:string'>::4::</tipo_documento>  </wsdl:reg_fac_electro_email></soapenv:Body>";
                body = body.Replace("::0::", ackResult);
                body = body.Replace("::1::", prod);
                template = template.Replace("::1::", body);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Se ha creado el Envelope de la petición", P1 = template });
                XmlDocument envelope = Common.MakeEnvelope(template);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Envelope creado: " + envelope.ToString() });
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Creando la petición web" });
                HttpWebRequest request = Common.CreateWebRequest("https://f4.fcert.co/fsend2/www_service/wservermain/NOV3DESOHFMOV01APPY/?wsdl");
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Cargando el envelope en la petición web" });
                Common.InsertSoapEnvelopeIntoWebRequest(ref envelope, request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ejecutando la petición web" });
                @out = Common.GetSOAPResult(request);
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Resultado de la petición: " + @out.ToString() });
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores." });
                //Data.AddException(new Types.Tablas.Exception() { Modulo = "ServiceClient.FullStack.GetCustomerDetail_v1", Detalle = ex.ToString() });
            }
            return @out;
        }

        public static Types.Services.GetParametroCollectionResult GetConfigValues(string Clave)
        {
            Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Inicio de la secuencia", P1 = Clave });
            Types.Services.GetParametroCollectionResult @out = new Types.Services.GetParametroCollectionResult();
            @out.Items = new Types.Tablas.ParametroCollection();
            try
            {
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Ingresando al Try" });

                Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Definiendo conexión con la BD" });
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Abriendo conexión con la BD" });
                    cn.Open();
                    Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Definiendo comando TSQL" });
                    SqlCommand tsql = cn.CreateCommand();
                    tsql.CommandText = "spGetFsConfigValues";
                    tsql.CommandType = System.Data.CommandType.StoredProcedure;
                    tsql.Parameters.Add(new SqlParameter("Clave", Clave));
                    Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Ejecutando comando TSQL" });
                    using (SqlDataReader rs = tsql.ExecuteReader())
                    {
                        Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Obteniendo datos" });
                        while (rs.Read())
                        {
                            Types.Tablas.Parametro item = new Types.Tablas.Parametro();
                            item.Clave = rs["Clave"].ToString();
                            item.Valor = rs["Valor"].ToString();
                            @out.Operacion.AffectedRows += 1;
                            @out.Items.Add(item);
                            Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: " + item.Clave, P1 = item.Valor });

                        }
                    }
                    Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Cerando conexión con la BD" });
                    cn.Close();
                }
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
            }
            catch (Exception ex)
            {
                Console.WriteLine(new Types.Tablas.Trace() { Accion = "GetConfigValues: Ocurrió un error" });
                //AddException(new Types.Tablas.Exception() { Modulo = "Data.GetConfigValues", Detalle = ex.ToString() });
                @out.Operacion = new Types.Common.ResultadoOperacion(ex);
                //AddException(new Types.Tablas.Exception() { Modulo = "GetConfigValues", Detalle = ex.ToString() });
            }
            return @out;
        }
        #endregion
    }





}
