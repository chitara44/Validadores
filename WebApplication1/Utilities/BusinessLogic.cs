using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace Tmc.Servicios.FullStack
{
    public class BusinessLogic
    {
        #region WI11667 - Coliving masivo
        /// <summary>
        /// Verifica si un número indicado ha sido migrado a FullStack.
        /// </summary>
        /// <param name="msisdn">Número celular a consultar.</param>
        /// <returns></returns>
        public static Types.BusinessLogic.EsClienteMigradoResult EsNumeroMigrado(string msisdn)
        {
            Data.AddTrace(new Types.Tablas.Trace() { Accion = "Se ha ejecutado la función EsClienteMigrado", P1 = msisdn });
            Data.AddTrace(new Types.Tablas.Trace() { Accion = "Cargando los parámetros" });
            Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryMigrationByNumber");
            Data.AddTrace(new Types.Tablas.Trace() { Accion = "Parámetros cargados. Conteo: " + parametros.Items.Count.ToString() });
            Types.BusinessLogic.EsClienteMigradoResult @out = new Types.BusinessLogic.EsClienteMigradoResult();
            try
            {
                XDocument value = ServiceClient.FullStack.QueryMigrationByNumber_v1(msisdn);
                if (value != null)
                {
                    Data.AddTrace(new Types.Tablas.Trace() { Accion = "Extrayendo valores del Xml" });
                    @out.Codigo = ServiceClient.Common.GetValueOf(value, parametros.Values("QueryMigrationByNumberCodeField"));
                    @out.Descripcion = ServiceClient.Common.GetValueOf(value, parametros.Values("QueryMigrationByNumberDescriptionField"));
                    @out.Source = ServiceClient.Common.GetValueOf(value, parametros.Values("QueryMigrationByNumberSourceField"));
                    Data.AddTrace(new Types.Tablas.Trace() { Accion = "Codigo: " + @out.Codigo });
                    Data.AddTrace(new Types.Tablas.Trace() { Accion = "Descripcion: " + @out.Descripcion });
                    Data.AddTrace(new Types.Tablas.Trace() { Accion = "Source: " + @out.Source });
                    string migratedCode = parametros.Values("QueryMigrationByNumberMigratedCode");
                    string migratedSource = parametros.Values("QueryMigrationByNumberMigratedSource");
                    Data.AddTrace(new Types.Tablas.Trace() { Accion = "migratedCode: " + migratedCode });
                    Data.AddTrace(new Types.Tablas.Trace() { Accion = "migratedSource: " + migratedSource });
                    if ((@out.Codigo == migratedCode) && (@out.Source == migratedSource))
                    {
                        Data.AddTrace(new Types.Tablas.Trace() { Accion = "El número existe en FullStack" });
                        @out.EsMigrado = true;
                    }
                    else
                    {
                        Data.AddTrace(new Types.Tablas.Trace() { Accion = "El número NO existe en FullStack" });
                        @out.EsMigrado = false;
                    }
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                }
                else
                {
                    Data.AddTrace(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error, consulte la tabla de errores para mayor información." });
                    Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.EsNumeroMigrado", Detalle = "No se ha obtenido información al consultar la SOA y el XML se retorna vacío." });
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ErrorInterno;
                    @out.Operacion.Mensaje = "No se ha obtenido información al consultar la SOA.";
                    @out.Operacion.Detalle = "No se ha obtenido información al consultar la SOA y el XML se retorna vacío.";
                }
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error, consulte la tabla de errores para mayor información." });
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.EsNumeroMigrado", Detalle = ex.ToString() });
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ErrorInterno;
                @out.Operacion.Mensaje = ex.Message;
                @out.Operacion.Detalle = ex.ToString();
            }
            return @out;
        }

        /// <summary>
        /// Verifica si un número está migrado a FullStack, para definir la redirección a seguir.
        /// </summary>
        /// <param name="msisdn">Número a consultar.</param>
        /// <param name="url">URL a redirigir si el número no es migrado.</param>
        /// <returns></returns>
        public static Types.Common.StringResult LogicaRedireccion(string msisdn, string url)
        {
            Data.AddTrace(new Types.Tablas.Trace("Se ha convocado al méodo BusinessLogic.LogicaRedireccion") { P1 = msisdn, P2 = url });
            Types.Common.StringResult @out = new Types.Common.StringResult();
            try
            {
                if (EsNumeroMigrado(msisdn).EsMigrado)
                {
                    Data.AddTrace(new Types.Tablas.Trace("El número ha sido migrado a FullStack, devolviendo la URL de eCare."));
                    @out.Resultado = Data.GetConfigValue("FsECareLoginUrl").Resultado;
                }
                else
                {
                    Data.AddTrace(new Types.Tablas.Trace("El número no ha sido migrado a FullStack, devolviendo la URL del parámetro de entrada."));
                    @out.Resultado = url;
                }
                @out.Operacion.AffectedRows = 1;
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk; 
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error, consulte la tabla de errores para mayor información."));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetListaBancos", Detalle = ex.ToString() });
                @out = new Types.Common.StringResult();
                @out.Operacion = new Types.Common.ResultadoOperacion(ex);
            }
            return @out;
        }
        #endregion

        #region RS14694 - Coliving Corporativo
        /// <summary>
        /// Registra una incidencia en FullStack.
        /// </summary>
        /// <param name="input">Parámetros de la incidencia.</param>
        /// <returns></returns>
        public static Types.Common.ServiceResult CreaIncidencia(Types.FullStack.CreateInteractionInput input)
        {
            Data.AddTrace(new Types.Tablas.Trace() { Accion = "Se ha convocado el método BusinessLogic.CreaIncidencia", P1 = input.Usuario, P2 = input.NumeroCelular });
            Types.Common.ServiceResult @out = new Types.Common.ServiceResult();
            try
            {
                XDocument valor = ServiceClient.FullStack.CreateInteraction_v1(input);
                string requestId = ServiceClient.Common.GetValueOf(valor, Data.GetConfigValue("CreateInteractionSucessfulField").Resultado);
                if (requestId.Trim() != "")
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                else
                {
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ErrorInterno;
                    @out.Operacion.Mensaje = ServiceClient.Common.GetValueOf(valor, "exceptionAppCode");
                    @out.Operacion.Detalle = ServiceClient.Common.GetValueOf(valor, "exceptionAppMessage");
                }
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores para mayor información." });
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.CreaIncidencia", Detalle = ex.ToString() });
                @out.Operacion = new Types.Common.ResultadoOperacion(ex);
            }
            return @out;
        }

        /// <summary>
        /// Verifica si una cuenta indicada ha sido migrada a FullStack.
        /// </summary>
        /// <param name="CustomerId">Número de cuenta a validar</param>
        /// <returns></returns>
        public static Types.BusinessLogic.EsClienteMigradoResult EsCuentaMigrada(string CustomerId)
        {
            Data.AddTrace(new Types.Tablas.Trace() { Accion = "Se ha convocado el método BusinessLogic.EsCuentaMigrada", P1 = CustomerId });
            Types.BusinessLogic.EsClienteMigradoResult @out = new Types.BusinessLogic.EsClienteMigradoResult();
            try
            {
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QyeryMigrationByAccountResponseElements");
                Data.AddTrace(new Types.Tablas.Trace() { Accion = "Obtenido los parámetros de lectura del resultado SOA.", P1 = "QueryMigrationBtAccount", P2 = parametros.Items.Count.ToString() });
                XDocument valor = ServiceClient.FullStack.QueryMigrationByAccount_v1(CustomerId);
                string code = ServiceClient.Common.GetValueOf(valor, parametros.Values("QyeryMigrationByAccountResponseElementsCode"));
                string source = ServiceClient.Common.GetValueOf(valor, parametros.Values("QyeryMigrationByAccountResponseElementsSource"));
                @out.Codigo = code;
                @out.Source = source;
                @out.Descripcion = ServiceClient.Common.GetValueOf(valor, parametros.Values("QueryMigrationByAccountResponseElementsDescription"));
                if ((code == parametros.Values("QyeryMigrationByAccountResponseElementsMigratedCode")) && (source == parametros.Values("QyeryMigrationByAccountResponseElementsMigratedSource")))
                    @out.EsMigrado = true;
                else
                    @out.EsMigrado = false;
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                @out.Operacion.AffectedRows += 1;
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace() { Accion = "Ha ocurrido un error. Verifique la tabla de errores para mayor información." });
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.EsCuentaMigrada", Detalle = ex.ToString() });
                @out.Operacion = new Types.Common.ResultadoOperacion(ex);
                throw;
            }
            return @out;
        }

        public static Types.Services.GetCusstomerDetailResult GetCustomerDetail(string NumeroIdentidad, string TipoIdentidad)
        {
            Data.AddTrace(new Types.Tablas.Trace("Se ha convocado el método BusinessLogic.GetCustomerDetail") { P1 = TipoIdentidad, P2 = NumeroIdentidad });
            Types.Services.GetCusstomerDetailResult @out = new Types.Services.GetCusstomerDetailResult();
            XDocument resultado = new XDocument();
            try
            {
                /// WI49544: Separamos el digito verificador del documento de identidad, si viene ==
                //NumeroIdentidad = Utils.QuitarDigitoVerificador(NumeroIdentidad);
                /// WI49544: FINAL =================================================================
                string tipoIdentidad = Utils.GetFsTipIdent(TipoIdentidad);
                Data.AddTrace(new Types.Tablas.Trace("Se reemplaza el tipo de documento para poder enviarlo a FullStack") { P1 = TipoIdentidad, P2 = tipoIdentidad });
                TipoIdentidad = tipoIdentidad;
                resultado = ServiceClient.FullStack.GetCustomerDetail_v1(NumeroIdentidad, TipoIdentidad);
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados"));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetCustomerDetailFields");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                @out.Resultado.IdCustomer = ServiceClient.Common.GetValueOf(resultado, parametros.Values("GetCustomerDetailFieldsIDCustomer"));
                @out.Resultado.OrganizationName = ServiceClient.Common.GetValueOf(resultado, parametros.Values("GetCustomerDetailFieldsOrganizationName"));
                @out.Resultado.FirstNameCustomer = ServiceClient.Common.GetValueOf(resultado, parametros.Values("GetCustomerDetailFieldsFirstNameCustomer"));
                @out.Resultado.LastNameCustomer = ServiceClient.Common.GetValueOf(resultado, parametros.Values("GetCustomerDetailFieldsLastNameCustomer"));
                @out.Resultado.CustomerSegment = ServiceClient.Common.GetValueOf(resultado, parametros.Values("GetCustomerDetailFieldsSegment"));
                @out.Resultado.CustomerSubSegment = ServiceClient.Common.GetValueOf(resultado, parametros.Values("GetCustomerDetailFieldsSubSegment"));
                @out.Resultado.CustomerMail = ServiceClient.Common.GetValueOf(resultado, parametros.Values("GetCustomerDetailFieldsMail"), "@");
                @out.Resultado.NameCustomer = ServiceClient.Common.GetValueOf(resultado, parametros.Values("GetCustomerDetailFieldsNameCustomer"));
                if (@out.Resultado.NameCustomer != parametros.Values("GetCustomerDetailFieldsNameCustomerFilter")) @out.Resultado.EsEmpresa = true;
                @out.Resultado.SeluSubSegment = Utils.GetSeluSegment(@out.Resultado.CustomerSubSegment);
                if (@out.Resultado.IdCustomer != Utils.NothingValue)
                {
                    Data.AddTrace(new Types.Tablas.Trace("Se encontraron resultados en el Xml devuelto por FullStack"));
                    @out.Operacion.AffectedRows += 1;
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                }
                else
                {
                    Data.AddTrace(new Types.Tablas.Trace("No se encontraron resultados en el Xml devuelto por FullStack"));
                    parametros = Data.GetConfigValues("SoaExceptionFields");
                    @out.Resultado.SoaCode = ServiceClient.Common.GetValueOf(resultado, parametros.Values("SoaExceptionFieldsAppCode"));
                    @out.Resultado.SoaMessage = ServiceClient.Common.GetValueOf(resultado, parametros.Values("SoaExceptionFieldsAppMessage"));
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ErrorInterno;
                }
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de manejo de errores"));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("SoaExceptionFields");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                string codigoError = ServiceClient.Common.GetValueOf(resultado, parametros.Values("SoaExceptionFieldsAppMessage"));
                string mensajeError = ServiceClient.Common.GetValueOf(resultado, parametros.Values("SoaExceptionFieldsAppMessage"));
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error: " + ex.Message));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetCustomerDetail", Detalle = ex.ToString() });
                @out.Resultado = new Types.FullStack.GetCustomerDetailOutput();
                @out.Operacion.AffectedRows = 0;
                @out.Operacion.Detalle = "Ha ocurrido un error en la SOA: " + codigoError + " - " + mensajeError;
                @out.Operacion.Mensaje = mensajeError;
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ErrorInterno;
                if (codigoError != "#N/A")
                {
                    @out.Operacion.SoaResult = new Types.Common.SoaResult();
                    @out.Operacion.SoaResult.SoaCode = codigoError;
                    @out.Operacion.SoaResult.SoaMessage = mensajeError;
                }
            }
            return @out;
        }

        public static Types.Services.GetInfoCuentaResult GetInfoCuenta(string NumeroCuenta)
        {
            Data.AddTrace(new Types.Tablas.Trace("Se ha convocado el método BusinessLogic.GetInfoCuenta") { P1 = NumeroCuenta });
            Types.Services.GetInfoCuentaResult @out = new Types.Services.GetInfoCuentaResult();
            try
            {
                Data.AddTrace(new Types.Tablas.Trace("Ejecutando GetAccountDetail_v1") { P1 = NumeroCuenta });
                XDocument resultado = ServiceClient.FullStack.GetAccountDetail_v1(NumeroCuenta);
                Data.AddTrace(new Types.Tablas.Trace("Valor obtenido: " + resultado.Root.InnerXML()) { P1 = resultado.Root.InnerXML() });
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados"));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetAccountDetailResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                @out.Resultado.Cuenta = NumeroCuenta;
                Data.AddTrace(new Types.Tablas.Trace("Obteniendo el valor para " + parametros.Values("GetAccountDetailResponseElementsContactTypePartyAccountContact")));
                @out.Resultado.ContactTypePartyAccountContact = ServiceClient.Common.GetValueOf(resultado, parametros.Values("GetAccountDetailResponseElementsContactTypePartyAccountContact"));

                // Buscamos las cuentas de la cuenta
                Data.AddTrace(new Types.Tablas.Trace("Buscamos las cuentas de la cuenta"));
                @out.Resultado.Cuentas = new Types.BusinessLogic.AccountInfoCollection();
                foreach (XNode node in resultado.DescendantNodes())
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        XElement element = (XElement)node;
                        // Data.AddTrace(new Types.Tablas.Trace("Validando elemento " + element.Name.LocalName + " <=> " + parametros.Values("GetAccountDetailResponseElementsCuentasContainer")));
                        if (element.Name.LocalName == parametros.Values("GetAccountDetailResponseElementsCuentasContainer"))
                        {
                            Types.BusinessLogic.AccountInfo cuenta = new Types.BusinessLogic.AccountInfo();
                            cuenta.NameCustomerAccount = ServiceClient.Common.GetValueOf(element, parametros.Values("GetAccountDetailResponseElementsCuentasName"));

                            foreach (XNode subnode in element.DescendantNodes())
                            {
                                if (subnode.NodeType == XmlNodeType.Element)
                                {
                                    XElement subelement = (XElement)subnode;
                                    if (subelement.Name.LocalName == parametros.Values("GetAccountDetailResponseElementsCuentasAcctContainer"))
                                    {
                                        string filter = ServiceClient.Common.GetValueOf(subelement, parametros.Values("GetAccountDetailResponseElementsCuentasAcctKey"));
                                        if (filter == parametros.Values("GetAccountDetailResponseElementsCuentasAcctFilter"))
                                        {
                                            cuenta.AccountCode = ServiceClient.Common.GetValueOf(subelement, parametros.Values("GetAccountDetailResponseElementsCuentasAcctValue"));
                                            break;
                                        }
                                    }
                                }
                            }
                            if (cuenta.AccountCode.Trim() != "")
                            {
                                // Solo agregamos la cuenta si tiene código de cuenta.
                                @out.Resultado.Cuentas.Add(cuenta);
                            }
                        }
                    }
                }
                Data.AddTrace(new Types.Tablas.Trace("Se encontraron " + @out.Resultado.Cuentas.Count().ToString() + " códigos de cuenta."));

                // Buscamos el indicador de factura electrónica activa
                Data.AddTrace(new Types.Tablas.Trace("Buscamos el indicador de factura electrónica activa"));
                foreach (XNode node in resultado.DescendantNodes())
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        XElement element = (XElement)node;
                        if (element.Name.LocalName == parametros.Values("GetAccountDetailResponseElementsFEEnabledContainer"))
                        {
                            string valueParameter = ServiceClient.Common.GetValueOf(element, parametros.Values("GetAccountDetailResponseElementsFEEnabledContainerElement"));
                            if (valueParameter == parametros.Values("GetAccountDetailResponseElementsFEEnabledFilter"))
                            {
                                @out.Resultado.FacturaElectronicaEnabled = true;
                                break;
                            }
                        }
                    }
                }

                // Buscamos la dirección de correo de la factura electrónica, si está activa
                if (@out.Resultado.FacturaElectronicaEnabled)
                {
                    Data.AddTrace(new Types.Tablas.Trace("Buscamos la dirección de correo de envío de la factura electrónica"));
                    foreach (XNode node in resultado.DescendantNodes())
                    {
                        if (node.NodeType == XmlNodeType.Element)
                        {
                            XElement element = (XElement)node;
                            if (element.Name.LocalName == parametros.Values("GetAccountDetailResponseElementsFEMailContainer"))
                            {
                                string nameParameter = ServiceClient.Common.GetValueOf(element, parametros.Values("GetAccountDetailResponseElementsFEMailContainerId"));
                                string valueParameter = ServiceClient.Common.GetValueOf(element, parametros.Values("GetAccountDetailResponseElementsFEMailContainerElement"));

                                if (nameParameter == parametros.Values("GetAccountDetailResponseElementsFEMailContainerIdFilter"))
                                {
                                    @out.Resultado.Email = valueParameter;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (@out.Resultado.Cuenta.Trim() == "")
                {
                    Data.AddTrace(new Types.Tablas.Trace("No se encontraron resultados en el Xml devuelto por FullStack"));
                    parametros = Data.GetConfigValues("SoaExceptionFields");
                    @out.Resultado = new Types.BusinessLogic.InfoCuenta();
                    @out.Operacion.Mensaje = ServiceClient.Common.GetValueOf(resultado, parametros.Values("SoaExceptionFieldsAppCode"));
                    @out.Operacion.Detalle = ServiceClient.Common.GetValueOf(resultado, parametros.Values("SoaExceptionFieldsAppMessage"));
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ErrorInterno;
                }
                else
                {
                    @out.Operacion.AffectedRows = 1;
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                }
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error, verifique la tabla de excepciones para mayor información."));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetInfocuenta", Detalle = ex.ToString() });
                @out = new Types.Services.GetInfoCuentaResult();
                @out.Operacion = new Types.Common.ResultadoOperacion(ex);
            }
            return @out;
        }

        /// <summary>
        /// Obtiene la lista de bancos disponibles para pagar por PSE.
        /// </summary>
        /// <returns></returns>
        public static Types.Common.ComboItemCollectionResult GetListaBancos()
        {
            Data.AddTrace(new Types.Tablas.Trace() { Accion = "Se ha convocado el método BusinessLogic.GetListaBancos" });
            Types.Common.ComboItemCollectionResult @out = new Types.Common.ComboItemCollectionResult();
            try
            {
                XDocument resultado = ServiceClient.FullStack.GetBankListOp_v1();
                /* Solo para fines de depuración local */
                // resultado = XDocument.Parse("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\"><soapenv:Header><ns1:HeaderOut xmlns:ns1=\"http://telefonica.com/globalIntegration/header\"><ns1:originator>co:TEF:MiMovistarCorp:MiMovistarCorp</ns1:originator><ns1:destination>co:Es:TEF:MiMovistarCorp:MiMovistarCorp</ns1:destination><ns1:execId>550e8400-e29b-41d4-a716-446655440001</ns1:execId><ns1:msgId>3f0dd0d3-1693-4f6b-9eae-a51b3913acd6</ns1:msgId><ns1:timestamp>2017-11-09T11:44:58.776-05:00</ns1:timestamp><ns1:msgType>RESPONSE</ns1:msgType></ns1:HeaderOut></soapenv:Header><soap:Body xmlns:wsa=\"http://schemas.xmlsoap.org/ws/2004/08/addressing\" xmlns:wsu=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:wsse=\"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><ns2:getbanklistopResponse xmlns:ns2=\"http://telefonica.com/globalIntegration/services/Getbanklistop/v1\"><ns2:bankOpListItem><ns2:partyIdOrganization>1040</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO AGRARIO</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1081</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO AGRARIO DESARROLLO</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1080</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO AGRARIO QA DEFECTOS</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>10322</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO CAJA SOCIAL</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1032</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO CAJA SOCIAL DESARROLLO</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1019</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO COLPATRIA DESARROLLO</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1078</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO COLPATRIA UAT</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1052</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO COMERCIAL AVVILLAS S.A.</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1061</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO COOMEVA S.A. - BANCOOMEVA</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1016</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO COOPERATIVO COOPCENTRAL</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1006</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO CORPBANCA (Migracion)</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1051</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO DAVIVIENDA</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>10512</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO DAVIVIENDA Desarrollo</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1001</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO DE BOGOTA DESARROLLO 2013</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1023</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO DE OCCIDENTE</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1062</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO FALABELLA</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1010</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO GNB COLOMBIA (ANTES HSBC)</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1012</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO GNB SUDAMERIS</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1060</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO PICHINCHA S.A.</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1002</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO POPULAR</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1058</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO PROCREDIT COLOMBIA</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1101</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>Banco PSE</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1065</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO SANTANDER COLOMBIA</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1035</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO TEQUENDAMA</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1022</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCO UNION COLOMBIANO</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1055</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>Banco Web Service ACH</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1055</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>Banco Web Service ACH WSE 3.0</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>10072</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCOLOMBIA DATAPOWER</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>10071</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCOLOMBIA DESARROLLO</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1007</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BANCOLOMBIA QA</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1013</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>BBVA COLOMBIA S.A.</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1009</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>CITIBANK COLOMBIA S.A.</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1014</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>HELM BANK S.A. WSE 3.0</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1098</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>HELM BANK S.A...</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>1508</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>NEQUI CERTIFICACION</ns2:nameTypeOrganizationName></ns2:bankOpListItem><ns2:bankOpListItem><ns2:partyIdOrganization>121212</ns2:partyIdOrganization><ns2:nameTypeOrganizationName>Prueba Steve</ns2:nameTypeOrganizationName></ns2:bankOpListItem></ns2:getbanklistopResponse></soap:Body></soapenv:Envelope>");
                Data.AddTrace(new Types.Tablas.Trace("BusinessLogic.GetListaBancos: Resultado:") { P1 = resultado.ToString() });
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetBankListOpFields");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                Data.AddTrace(new Types.Tablas.Trace("Obteniendo el listado de bancos"));
                foreach (XNode element in resultado.Root.DescendantNodes())
                {
                    if (element.NodeType == XmlNodeType.Element)
                    {
                        XElement xmlElement = (XElement)element;
                        try
                        {
                            if (xmlElement.Name.LocalName == parametros.Values("GetBankListOpFieldsContainer"))
                            {
                                Types.Common.ComboItem item = new Types.Common.ComboItem();
                                int van = 0;
                                foreach (XNode subelement in xmlElement.DescendantNodes())
                                {
                                    if (subelement.NodeType == XmlNodeType.Element)
                                    {
                                        XElement xmlSubelement = (XElement)subelement;
                                        if (xmlSubelement.Name.LocalName == parametros.Values("GetBankListOpFieldsName"))
                                        {
                                            item.Texto = xmlSubelement.Value;
                                            van += 1;
                                        }
                                        if (xmlSubelement.Name.LocalName == parametros.Values("GetBankListOpFieldsId"))
                                        {
                                            item.Valor = xmlSubelement.Value;
                                            van += 1;
                                        }
                                        if (van == 2)
                                        {
                                                @out.Items.Add(item);
                                                item = new Types.Common.ComboItem();
                                                van = 0;
                                                @out.Operacion.AffectedRows += 1;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex1)
                        {
                            Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error, consulte la tabla de errores para mayor información."));
                            Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetListaBancos", Detalle = ex1.ToString() });
                        }
                    }
                }
                Data.AddTrace(new Types.Tablas.Trace("Total elementos obtenidos: " + @out.Operacion.AffectedRows.ToString()));
                if (@out.Operacion.AffectedRows == 0)
                {
                    Data.AddTrace(new Types.Tablas.Trace("No se encontraron resultados en el Xml devuelto por FullStack") { P1 = resultado.ToString() });
                    try
                    {
                        parametros = Data.GetConfigValues("SoaExceptionFields");
                        string codigoError = ServiceClient.Common.GetValueOf(resultado, parametros.Values("SoaExceptionFieldsAppCode"));
                        string mensajeError = ServiceClient.Common.GetValueOf(resultado, parametros.Values("SoaExceptionFieldsAppMessage"));
                        Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error en la SOA: " + codigoError));
                        Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ActivarFacturaElectronica", Detalle = "Ha ocurrido un error en la SOA: " + codigoError + " - " + mensajeError });
                        @out.Items.Clear();
                        @out.Operacion.AffectedRows = 0;
                        @out.Operacion.Detalle = "Ha ocurrido un error en la SOA: " + codigoError + " - " + mensajeError;
                        @out.Operacion.Mensaje = mensajeError;
                        @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ErrorInterno;
                        @out.Operacion.SoaResult = new Types.Common.SoaResult();
                        @out.Operacion.SoaResult.SoaCode = codigoError;
                        @out.Operacion.SoaResult.SoaMessage = mensajeError;
                    }
                    catch { }
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ErrorInterno;
                }
                else
                {
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                }
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error, consulte la tabla de errores para mayor información."));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetListaBancos", Detalle = ex.ToString() });
                @out.Items.Clear();
                @out.Operacion = new Types.Common.ResultadoOperacion(ex);
            }
            return @out;
        }

        public static Types.Services.GetProductosClienteResult GetProductosCliente(string idCliente, string nit, string fullName)
        {
            Data.AddTrace(new Types.Tablas.Trace() { Accion = "Se ha convocado el método BusinessLogic.GetProductosCliente", P1 = idCliente });
            Types.Services.GetProductosClienteResult @out = new Types.Services.GetProductosClienteResult();
            try
            {
                XDocument resultado = ServiceClient.FullStack.GetSuscriberList_v1(idCliente);
                Data.AddTrace(new Types.Tablas.Trace("ServiceClient.FullStack.GetSuscriberList_v1: Resultado:") { P1 = resultado.ToString() });

                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetSuscriberListResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));

                Data.AddTrace(new Types.Tablas.Trace("Obteniendo el listado de cuentas del cliente"));
                Data.AddTrace(new Types.Tablas.Trace("Se buscará " + parametros.Values("GetSuscriberListResponseElementsIDCustomer") + " en cada " + parametros.Values("GetSuscriberListResponseElementsContainer")));
                foreach (XNode node in resultado.Root.DescendantNodes())
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        XElement element = (XElement)node;
                        try
                        {
                            if (element.Name.LocalName == parametros.Values("GetSuscriberListResponseElementsContainer"))
                            {
                                foreach (XNode subnode in element.DescendantNodes())
                                {
                                    if (subnode.NodeType == XmlNodeType.Element)
                                    {
                                        XElement subelement = (XElement)subnode;
                                        if (subelement.Name.LocalName == parametros.Values("GetSuscriberListResponseElementsIDCustomer"))
                                        {
                                            Data.AddTrace(new Types.Tablas.Trace("Agregando " + subelement.Value));
                                            Types.BusinessLogic.InfoCuentaPorNitFS item = new Types.BusinessLogic.InfoCuentaPorNitFS();
                                            Types.BusinessLogic.InfoCelularPorCuentaFS linea = new Types.BusinessLogic.InfoCelularPorCuentaFS();
                                            item.Cuenta = subelement.Value;             // CU-002 RS V1.1.19

                                            // Solo procesamos si la cuenta no ha sido cargada.
                                            if (!@out.Items.Contiene(item.Cuenta))
                                            {
                                                item.IdFs = item.Cuenta;                      // CU-002 RS V1.1.19
                                                item.Nit = nit;                             // CU-002 RS V1.1.19
                                                item.DuplicadoEnScl = false;                // CU-002 RS V1.1.19

                                                Data.AddTrace(new Types.Tablas.Trace("Buscando líneas de la cuenta " + item.Cuenta));
                                                item.Lineas = ExtraerLineasCuenta(resultado, item, "");

                                                @out.Items.Add(item);
                                            }
                                        }
                                    }
                                }
                                Data.AddTrace(new Types.Tablas.Trace(@out.Items.Count().ToString() + " elementos agregados"));
                            }
                        }
                        catch (Exception ex)
                        {
                            Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error. Verifique la tabla de errores para mayor información."));
                            Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetProductosCliente", Detalle = ex.ToString() });
                        }
                    }
                }
                Data.AddTrace(new Types.Tablas.Trace("Total cuentas obtenidas: " + @out.Items.Count.ToString()));
                Data.AddTrace(new Types.Tablas.Trace("Total líneas obtenidas: " + @out.Items.TotalLineas.ToString()));
                @out.Operacion.AffectedRows = @out.Items.Count();
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;

            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error, consulte la tabla de errores para mayor información."));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ProductosCliente", Detalle = ex.ToString() });
                @out.Items.Clear();
                @out.Operacion = new Types.Common.ResultadoOperacion(ex);
            }
            return @out;
        }

        public static Types.Services.GetProductosClienteResult GetProductosClienteByAcctCode(string acctCode, string nit, string fullName)
        {
            Data.AddTrace(new Types.Tablas.Trace() { Accion = "Se ha convocado el método BusinessLogic.GetProductosClienteByAcctCode", P1 = acctCode, P2 = nit, P3 = fullName });
            Types.Services.GetProductosClienteResult @out = new Types.Services.GetProductosClienteResult();
            try
            {
                XDocument resultado = ServiceClient.FullStack.GetSuscriberList_v1_byAcctCode(acctCode);
                Data.AddTrace(new Types.Tablas.Trace("ServiceClient.FullStack.GetSuscriberList_v1_byAcctCode: Resultado:") { P1 = resultado.ToString() });

                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetSuscriberListResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));

                Data.AddTrace(new Types.Tablas.Trace("Obteniendo el listado de cuentas del cliente"));
                Data.AddTrace(new Types.Tablas.Trace("Se buscará " + parametros.Values("GetSuscriberListResponseElementsIDCustomer") + " en cada " + parametros.Values("GetSuscriberListResponseElementsContainer")));
                foreach (XNode node in resultado.Root.DescendantNodes())
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        XElement element = (XElement)node;
                        try
                        {
                            if (element.Name.LocalName == parametros.Values("GetSuscriberListResponseElementsContainer"))
                            {
                                foreach (XNode subnode in element.DescendantNodes())
                                {
                                    if (subnode.NodeType == XmlNodeType.Element)
                                    {
                                        XElement subelement = (XElement)subnode;
                                        if (subelement.Name.LocalName == parametros.Values("GetSuscriberListResponseElementsIDCustomer"))
                                        {
                                            Data.AddTrace(new Types.Tablas.Trace("Agregando " + subelement.Value));
                                            Types.BusinessLogic.InfoCuentaPorNitFS item = new Types.BusinessLogic.InfoCuentaPorNitFS();
                                            Types.BusinessLogic.InfoCelularPorCuentaFS linea = new Types.BusinessLogic.InfoCelularPorCuentaFS();
                                            item.Cuenta = acctCode;             // CU-002 RS V1.1.29

                                            // Solo procesamos si la cuenta no ha sido cargada.
                                            if (!@out.Items.Contiene(item.Cuenta))
                                            {
                                                item.IdFs = item.Cuenta;                      // CU-002 RS V1.1.29
                                                item.Nit = nit;                             // CU-002 RS V1.1.19
                                                item.DuplicadoEnScl = false;                // CU-002 RS V1.1.19

                                                Data.AddTrace(new Types.Tablas.Trace("Buscando líneas de la cuenta " + item.Cuenta));
                                                item.Lineas = ExtraerLineasCuenta(resultado, item, "");

                                                @out.Items.Add(item);
                                            }
                                        }
                                    }
                                }
                                Data.AddTrace(new Types.Tablas.Trace(@out.Items.Count().ToString() + " elementos agregados"));
                            }
                        }
                        catch (Exception ex)
                        {
                            Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error. Verifique la tabla de errores para mayor información."));
                            Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetProductosCliente", Detalle = ex.ToString() });
                        }
                    }
                }
                Data.AddTrace(new Types.Tablas.Trace("Total cuentas obtenidas: " + @out.Items.Count.ToString()));
                Data.AddTrace(new Types.Tablas.Trace("Total líneas obtenidas: " + @out.Items.TotalLineas.ToString()));
                @out.Operacion.AffectedRows = @out.Items.Count();
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;

            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error, consulte la tabla de errores para mayor información."));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ProductosCliente", Detalle = ex.ToString() });
                @out.Items.Clear();
                @out.Operacion = new Types.Common.ResultadoOperacion(ex);
            }
            return @out;
        }

        /// <summary>
        /// Extrae las cuentas de la respuesta de la SOA.
        /// </summary>
        /// <param name="input">Respuesta a leer.</param>
        /// <returns></returns>
        private static Types.BusinessLogic.InfoCuentaPornitFSCollection ExtraerCuentasProductos(XDocument input, string idCliente, string nit, string fullName)
        {
            Data.AddTrace(new Types.Tablas.Trace("Se ha convocado el método BusinessLogic.ExtraerCuentasProductos") {P1 = input.ToString() });
            Types.BusinessLogic.InfoCuentaPornitFSCollection @out = new Types.BusinessLogic.InfoCuentaPornitFSCollection();
            try
            {

                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetSuscriberListResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));

                foreach (XNode node in input.Root.DescendantNodes())
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        XElement element = (XElement)node;
                        try
                        {
                            if (element.Name.LocalName == parametros.Values("GetSuscriberListResponseElementsContainer"))
                            {
                                foreach (XNode subnode in element.DescendantNodes())
                                {
                                    if (subnode.NodeType == XmlNodeType.Element)
                                    {
                                        XElement subelement = (XElement)subnode;
                                        if (subelement.Name.LocalName == parametros.Values("GetSuscriberListResponseElementsIDCustomer"))
                                        {
                                            Types.BusinessLogic.InfoCuentaPorNitFS item = new Types.BusinessLogic.InfoCuentaPorNitFS();
                                            item.Cuenta = subelement.Value;                 // CU-002 RS V1.1.19

                                            // Solo procesamos si la cuenta no ha sido cargada.
                                            if (!@out.Contiene(item.Cuenta))
                                            {
                                                item.IdFs = idCliente;                      // CU-002 RS V1.1.19
                                                item.Nit = nit;                             // CU-002 RS V1.1.19
                                                item.DuplicadoEnScl = false;                // CU-002 RS V1.1.19

                                                // Obtenemos las líneas de esta cuenta
                                                item.Lineas = ExtraerLineasCuenta(input, item, fullName);

                                                // Agregamos a los resultados
                                                @out.Add(item);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error. Verifique la tabla de errores para mayor información."));
                            Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ExtraerCuentasProductos", Detalle = ex.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                @out = new Types.BusinessLogic.InfoCuentaPornitFSCollection();
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error. Consulte la tabla de errores para mayor información."));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ExtraerCuentasProductos", Detalle = ex.ToString() });

            }
            return @out;
        }

        /// <summary>
        /// Extrae las líneas asociadas a una cuenta desde la respuesta de la SOA.
        /// </summary>
        /// <param name="input">Respuesta a leer</param>
        /// <param name="cuenta">Cuenta a buscar</param>
        /// <param name="fullName">Nombre completo del cliente</param>
        /// <returns></returns>
        private static Types.BusinessLogic.InfoCelularPorCuentaFSCollection ExtraerLineasCuenta(XDocument input, Types.BusinessLogic.InfoCuentaPorNitFS cuenta, string fullName)
        {
            Data.AddTrace(new Types.Tablas.Trace("Se ha convocado el método BusinsessLogic.ExtraerLineasCuenta") { P1 = input.ToString(), P2 = cuenta.Cuenta.ToString(), P3 = fullName });
            Types.BusinessLogic.InfoCelularPorCuentaFSCollection @out = new Types.BusinessLogic.InfoCelularPorCuentaFSCollection();
            try
            {
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("GetSuscriberListResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));

                Types.BusinessLogic.InfoCelularPorCuentaFS item = new Types.BusinessLogic.InfoCelularPorCuentaFS();

                foreach (XNode node in input.Root.DescendantNodes())
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        XElement element = (XElement)node;
                        try
                        {
                            if (element.Name.LocalName == parametros.Values("GetSuscriberListResponseElementsProductContainer"))
                            {
                                Data.AddTrace(new Types.Tablas.Trace("Es " + element.Name.LocalName));
                                item = new Types.BusinessLogic.InfoCelularPorCuentaFS();
                                int pasos = 0;
                                bool encontrado = false;

                                foreach (XNode subnode in element.DescendantNodes())
                                {
                                    if (subnode.NodeType == XmlNodeType.Element)
                                    {
                                        XElement subelement = (XElement)subnode;
                                        try
                                        {
                                            if (subelement.Name.LocalName == parametros.Values("GetSuscriberListResponseElementsContainer"))
                                            {
                                                Data.AddTrace(new Types.Tablas.Trace("Obteniendo información general del producto"));
                                                string ncuenta = ServiceClient.Common.GetValueOf(subelement, parametros.Values("GetSuscriberListResponseElementsIDCustomer"));
                                                //if (ncuenta == cuenta.Cuenta)
                                                //{
                                                    encontrado = true;

                                                    Data.AddTrace(new Types.Tablas.Trace("El producto pertenece a la cuenta"));
                                                    Data.AddTrace(new Types.Tablas.Trace("Obteniendo propiedades"));
                                                    item.Cuenta = ncuenta;
                                                    item.Numero = ServiceClient.Common.GetValueOf(subelement, parametros.Values("GetSuscriberListResponseElementsNumber"));
                                                    item.Estado = ServiceClient.Common.GetValueOf(subelement, parametros.Values("GetSuscriberListResponseElementsStatus"));
                                                    string activacion = ServiceClient.Common.GetValueOf(subelement, parametros.Values("GetSuscriberListResponseElementsActivation"));
                                                    int year = Convert.ToInt32(activacion.Substring(0, 4));
                                                    int month = Convert.ToInt32(activacion.Substring(4, 2));
                                                    int day = Convert.ToInt32(activacion.Substring(6, 2));
                                                    item.Activacion = new DateTime(year, month, day);
                                                    Data.AddTrace(new Types.Tablas.Trace("Obteniendo parámetros"));
                                                    foreach (XNode nodeparam in subelement.DescendantNodes())
                                                    {
                                                        if (nodeparam.NodeType == XmlNodeType.Element)
                                                        {
                                                            XElement xeParam = (XElement)nodeparam;
                                                            if (xeParam.Name.LocalName == parametros.Values("GetSuscriberListResponseElementsParameterContainer"))
                                                            {
                                                                string paramName = ServiceClient.Common.GetValueOf(xeParam, parametros.Values("GetSuscriberListResponseElementsIDParameter"));
                                                                string paramValue = ServiceClient.Common.GetValueOf(xeParam, parametros.Values("GetSuscriberListResponseElementsValueParameter"));

                                                                if (paramName == parametros.Values("GetSuscriberListResponseElementsBrandKey"))
                                                                    item.Marca = paramValue;
                                                                else if (paramName == parametros.Values("GetSuscriberListResponseElementsModelKey"))
                                                                    item.Modelo = paramValue;
                                                            }
                                                        }
                                                    }

                                                    pasos += 1;
                                                // }
                                            }
                                            else if (subelement.Name.LocalName == parametros.Values("GetSuscriberListResponseElementsOfferingContainer"))
                                            {
                                                Data.AddTrace(new Types.Tablas.Trace("Obteniendo información del plan del producto"));
                                                item.Plan = ServiceClient.Common.GetValueOf(subelement, parametros.Values("GetSuscriberListResponseElementsNameProductOffering"));
                                                item.CodigoPlan = ServiceClient.Common.GetValueOf(subelement, parametros.Values("GetSuscriberListResponseElementsIdProductOffering"));
                                                pasos += 1;
                                            }
                                        }
                                        catch (Exception ex3)
                                        {
                                            Data.AddTrace(new Types.Tablas.Trace("EX3: Ha ocurrido un error. Verifique la tabla de errores para mayor información."));
                                            Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ExtraerLineasCuentas", Detalle = ex3.ToString() });
                                        }
                                    }
                                }

                                if (encontrado) @out.Add(item);
                            }
                        }
                        catch (Exception ex2)
                        {
                            Data.AddTrace(new Types.Tablas.Trace("EX2: Ha ocurrido un error. Verifique la tabla de errores para mayor información."));
                            Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ExtraerLineasCuentas", Detalle = ex2.ToString() });
                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                Data.AddTrace(new Types.Tablas.Trace("EX1: Ha ocurrido un error. Consulte la tabla de errores para mayor información."));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ExtraerLineasCuenta", Detalle = ex1.ToString() });
            }
            return @out;
        }

        /// <summary>
        /// Obtiene la información de facturación de una línea, para poder obtener su sal
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static Types.BusinessLogic.InfoFactura GetInfoFactura(string numero)
        {
            Data.AddTrace(new Types.Tablas.Trace("Se ejecuta la función BusinessLogic.GetInfoFactura") { P1 = numero });
            Types.BusinessLogic.InfoFactura @out = new Types.BusinessLogic.InfoFactura();
            try
            {
                XDocument resultado = ServiceClient.FullStack.QueryInvoice(numero);
                Data.AddTrace(new Types.Tablas.Trace("Respuesta obtenida desde el bus FS: " + resultado.ToString()) { P1 = resultado.ToString() });
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryInvoiceResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));

                // Código anterior a los distintos valores obtenidos
                // @out.SaldoPendiente = Convert.ToSingle(ServiceClient.Common.GetValueOf(resultado, parametros.Values("QueryInvoiceResponseElementsAmountQuentity")));
                // string fecha = ServiceClient.Common.GetValueOf(resultado, parametros.Values("QueryInvoiceResponseElementsEndDateTimePeriod"));
                // @out.FechaLimitePago = new DateTime(Convert.ToInt32(fecha.Substring(0, 4)), Convert.ToInt32(fecha.Substring(4, 2)), Convert.ToInt32(fecha.Substring(6, 2)));

                // Código nuevo, barriendo todos los valores posibles
                @out.SaldoPendiente = 0;
                @out.FechaLimitePago = new DateTime(1901, 1, 1);
                Data.AddTrace(new Types.Tablas.Trace("Obteniendo saldos de la línea " + numero));
                foreach (XNode node in resultado.DescendantNodes())
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        XElement element = (XElement)node;
                        if (element.Name.LocalName == parametros.Values("QueryInvoiceResponseElementsInvoiceContainer"))
                        {
                            @out.SaldoPendiente += Convert.ToSingle(ServiceClient.Common.GetValueOf(element, parametros.Values("QueryInvoiceResponseElementsAmountQuentity")));
                            string fecha = ServiceClient.Common.GetValueOf(resultado, parametros.Values("QueryInvoiceResponseElementsEndDateTimePeriod"));
                            DateTime nuevafecha = new DateTime(Convert.ToInt32(fecha.Substring(0, 4)), Convert.ToInt32(fecha.Substring(4, 2)), Convert.ToInt32(fecha.Substring(6, 2)));
                            if (nuevafecha > @out.FechaLimitePago) @out.FechaLimitePago = nuevafecha;
                        }
                    }
                }
                Data.AddTrace(new Types.Tablas.Trace("Saldo obtenido exitosamente") { P1 = @out.SaldoPendiente.ToString() });

                Data.AddTrace(new Types.Tablas.Trace("Saldo pendiente: " + @out.SaldoPendiente.ToString()));
                Data.AddTrace(new Types.Tablas.Trace("Fecha límite de pago: " + @out.FechaLimitePago.ToString()));
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error. Verifique el registro de errores para mayor información."));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetInfoFactura", Detalle = ex.ToString() });
                @out = new Types.BusinessLogic.InfoFactura();
            }
            return @out;
        }

        /// <summary>
        /// Obtiene la información de facturación de una cuenta FS, para poder obtener su número de factura
        /// </summary>
        /// <param name="acctCode"></param>
        /// <returns></returns>
        public static Types.BusinessLogic.InfoFactura GetInfoFacturaByAcctCode(string acctCode)
        {
            Data.AddTrace(new Types.Tablas.Trace("Se ejecuta la función BusinessLogic.GetInfoFacturaByAcctCode") { P1 = acctCode });
            Types.BusinessLogic.InfoFactura @out = new Types.BusinessLogic.InfoFactura();
            try
            {
                XDocument resultado = ServiceClient.FullStack.QueryInvoiceByAcctCode(acctCode);
                Data.AddTrace(new Types.Tablas.Trace("Respuesta obtenida desde el bus FS: " + resultado.ToString()) { P1 = resultado.ToString() });
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryInvoiceResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                foreach (XNode node in resultado.DescendantNodes())
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        XElement element = (XElement)node;
                        if (element.Name.LocalName == parametros.Values("QueryInvoiceResponseElementsInvoiceContainer"))
                        {
                            @out.NumeroFactura = ServiceClient.Common.GetValueOf(element, parametros.Values("QueryInvoiceResponseElementsInvoiceField"));
                            Data.AddTrace(new Types.Tablas.Trace("Número de factura para " + acctCode + ": " + @out.NumeroFactura));
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error. Verifique el registro de errores para mayor información."));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetInfoFactura", Detalle = ex.ToString() });
                @out = new Types.BusinessLogic.InfoFactura();
            }
            return @out;
        }

        /// <summary>
        /// Genera una orden de pago antes de redirigir a la pasarela de PSE.
        /// </summary>
        /// <param name="input">Parámetros de entrada.</param>
        /// <returns></returns>
        public static Types.Services.GeneraOrdenResult GeneraOrden(Types.BusinessLogic.GeneraOrdenInput input)
        {
            Data.AddTrace(new Types.Tablas.Trace("Se ha convocado el método BusinessLogic.GeneraOrden") { P1 = JsonConvert.SerializeObject(input) });
            Types.Services.GeneraOrdenResult @out = new Types.Services.GeneraOrdenResult();
            try
            {
                XDocument resultado = ServiceClient.FullStack.PayOperation_Pagar(input);
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("PayOperationResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                @out.Resultado.Result = ServiceClient.Common.GetValueOf(resultado, parametros.Values("PayOperationResponseElementsResult"));
                @out.Resultado.ResultCode = ServiceClient.Common.GetValueOf(resultado, parametros.Values("PayOperationResponseElementsResultCode"));
                @out.Resultado.ResponseMessage = ServiceClient.Common.GetValueOf(resultado, parametros.Values("PayOperationResponseElementsResponseMessage"));
                @out.Resultado.HostUrl = ServiceClient.Common.GetValueOf(resultado, parametros.Values("PayOperationResponseElementsHostUrl"));
                @out.Resultado.OperationIdPaymentOp = ServiceClient.Common.GetValueOf(resultado, parametros.Values("PayOperationResponseElementsOperationId"));
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                Data.AddTrace(new Types.Tablas.Trace("Resultado obtenido exitosamente.") { P1 = JsonConvert.SerializeObject(@out.Resultado) });
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error. Verifique el registro de errores para mayor información."));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GeneraOrden", Detalle = ex.ToString() });
                @out = new Types.Services.GeneraOrdenResult() { Operacion = new Types.Common.ResultadoOperacion(ex) };
            }
            return @out;
        }

        /// <summary>
        /// Obtiene la URL a redireccionar para un método o caso de uso.
        /// </summary>
        /// <param name="operacion">Operación a consultar.</param>
        /// <returns></returns>
        public static Types.Common.StringResult GetECareUrlFor(string operacion)
        {
            return Data.GetConfigValue("ECareUrlFor" + operacion);
        }

        /// <summary>
        /// Verifica si un número pertenece a un cliente
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="Productos"></param>
        /// <returns></returns>
        public static bool EsCelularDeCuentaFS(string numero, Types.Services.GetProductosClienteResult Productos)
        {
            bool @out = false;
            foreach (Types.BusinessLogic.InfoCuentaPorNitFS cuenta in Productos.Items)
            {
                foreach (Types.BusinessLogic.InfoCelularPorCuentaFS linea in cuenta.Lineas)
                {
                    if (linea.Numero == numero)
                    {
                        @out = true;
                        break;
                    }
                }
            }
            return @out;
        }

        /// <summary>
        /// Traduce un estado de línea FS a SCL.
        /// </summary>
        /// <param name="CodUsoFs">valor a traducir.</param>
        /// <returns></returns>
        public static string GetCodSituacionSclFor(string CodUsoFs)
        {
            string @out = "BAA"; // Ponemos de baja, por si acaso
            try
            {
                Types.Services.GetParametroCollectionResult valoresFs = Data.GetConfigValues("FsCodSituacionFs");
                Types.Services.GetParametroCollectionResult valoresScl = Data.GetConfigValues("FsCodSituacionScl");

                foreach (Types.Tablas.Parametro itemFs in valoresFs.Items)
                {
                    if (itemFs.Valor == CodUsoFs)
                    {
                        string terminal = itemFs.Clave.Substring(itemFs.Clave.Length - 3);
                        foreach (Types.Tablas.Parametro itemScl in valoresScl.Items)
                        {
                            if (itemScl.Clave.EndsWith(terminal))
                            {
                                @out = itemScl.Valor;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetCodSituacionSclFor", Detalle = ex.ToString() });
                @out = "ERR";
            }
            return @out;
        }

        /// <summary>
        /// Traduce un estado de línea SCL a FS.
        /// </summary>
        /// <param name="CodUsoScl">valor a traducir.</param>
        /// <returns></returns>
        public static string GetCodSituacionFsFor(string CodUsoScl)
        {
            string @out = "BAA"; // Ponemos de baja, por si acaso
            try
            {
                Types.Services.GetParametroCollectionResult valoresFs = Data.GetConfigValues("FsCodSituacionFs");
                Types.Services.GetParametroCollectionResult valoresScl = Data.GetConfigValues("FsCodSituacionScl");

                foreach (Types.Tablas.Parametro itemScl in valoresScl.Items)
                {
                    if (itemScl.Valor == CodUsoScl)
                    {
                        string terminal = itemScl.Clave.Substring(itemScl.Clave.Length - 3);
                        foreach (Types.Tablas.Parametro itemFs in valoresFs.Items)
                        {
                            if (itemFs.Clave.EndsWith(terminal))
                            {
                                @out = itemFs.Valor;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetCodSituacionFsFor", Detalle = ex.ToString() });
                @out = "ERR";
            }
            return @out;
        }

        /// <summary>
        /// Traduce un tipo de línea FS a SCL.
        /// </summary>
        /// <param name="CodUsoFs">valor a traducir.</param>
        /// <returns></returns>
        public static string GetCodUsoSclFor(string CodUsoFs)
        {
            string @out = "2"; // Ponemos Prepago, por si acaso
            try
            {
                Types.Services.GetParametroCollectionResult valoresFs = Data.GetConfigValues("FsCodUsoFs");
                Types.Services.GetParametroCollectionResult valoresScl = Data.GetConfigValues("FsCodUsoScl");

                foreach (Types.Tablas.Parametro itemFs in valoresFs.Items)
                {
                    if (itemFs.Valor == CodUsoFs)
                    {
                        string terminal = itemFs.Clave.Substring(itemFs.Clave.Length - 3);
                        foreach (Types.Tablas.Parametro itemScl in valoresScl.Items)
                        {
                            if (itemScl.Clave.EndsWith(terminal))
                            {
                                @out = itemScl.Valor;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetCodUsoSclFor", Detalle = ex.ToString() });
                @out = "0";
            }
            return @out;
        }

        /// <summary>
        /// Traduce un tipo de línea SCL a FS.
        /// </summary>
        /// <param name="CodUsoScl">valor a traducir.</param>
        /// <returns></returns>
        public static string GetCodUsoFsFor(string CodUsoScl)
        {
            string @out = "2"; // Ponemos prepago, por si acaso
            try
            {
                Types.Services.GetParametroCollectionResult valoresFs = Data.GetConfigValues("FsCodUsoFs");
                Types.Services.GetParametroCollectionResult valoresScl = Data.GetConfigValues("FsCodUsoScl");

                foreach (Types.Tablas.Parametro itemScl in valoresScl.Items)
                {
                    if (itemScl.Valor == CodUsoScl)
                    {
                        string terminal = itemScl.Clave.Substring(itemScl.Clave.Length - 3);
                        foreach (Types.Tablas.Parametro itemFs in valoresFs.Items)
                        {
                            if (itemFs.Clave.EndsWith(terminal))
                            {
                                @out = itemFs.Valor;
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetCodUsoFsFor", Detalle = ex.ToString() });
                @out = "ERR";
            }
            return @out;
        }

        /// <summary>
        /// Obtiene la URL de un documento electrónico para un número en específico
        /// </summary>
        /// <param name="numero">Número a consultar</param>
        /// <param name="tipoDocumento">Tipo de documento a consultar</param>
        /// <returns></returns>
        public static Types.Common.StringResult GetUrlDocumento(string numero, string tipoDocumento)
        {
            Types.Common.StringResult @out = new Types.Common.StringResult();
            try
            {
                string tipoIdentidad = Utils.GetFsTipIdent(tipoDocumento);
                Data.AddTrace(new Types.Tablas.Trace("Se reemplaza el tipo de documento para poder enviarlo a FullStack") { P1 = tipoDocumento, P2 = tipoIdentidad });
                tipoDocumento = tipoIdentidad;
                XDocument resultado = ServiceClient.FullStack.QueryDocumentURL(numero, tipoDocumento);
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryDocumentResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                @out.Resultado = ServiceClient.Common.GetValueOf(resultado, parametros.Values("QueryDocumentResponseElementsUrl"));
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                @out.Operacion.AffectedRows = 1;
            }
            catch (Exception ex)
            {
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.GetUrlDocumento", Detalle = ex.ToString() });
                @out = new Types.Common.StringResult() { Operacion = new Types.Common.ResultadoOperacion(ex) };
            }
            return @out;
        }

        public static Types.Services.GetPagoHistoricoResult ConsultaPagos(Types.FullStack.Pagos.InputHistorico data)
        {
            Types.Services.GetPagoHistoricoResult @out = new Types.Services.GetPagoHistoricoResult();
            try
            {
                XDocument resultado = ServiceClient.FullStack.QueryPaymentLog(data);
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryPaymentLogResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));

                foreach (XNode node in resultado.DescendantNodes())
                {
                    if (node.NodeType == XmlNodeType.Element)
                    {
                        XElement element = (XElement)node;
                        if (element.Name.LocalName == parametros.Values("QueryPaymentLogResponseElementsPaymentContainer"))
                        {
                            Types.FullStack.Pagos.HistoricoElement item = new Types.FullStack.Pagos.HistoricoElement();

                            item.Cuenta = data.Cuenta;
                            item.Descripcion = ServiceClient.Common.GetValueOf(element, parametros.Values("QueryPaymentLogResponseElementsDescription"));
                            item.FechaPago = DateTime.ParseExact(ServiceClient.Common.GetValueOf(element, parametros.Values("QueryPaymentLogResponseElementsTransactionDate")), parametros.Values("QueryPaymentLogResponseElementsDateFormat"), System.Globalization.CultureInfo.InvariantCulture);
                            item.IdPago = ServiceClient.Common.GetValueOf(element, parametros.Values("QueryPaymentLogResponseElementsExternalSerialNumber"));
                            item.IdTrx = ServiceClient.Common.GetValueOf(element, parametros.Values("QueryPaymentLogResponseElementsIdTrxUnique"));
                            item.Monto = Convert.ToSingle(ServiceClient.Common.GetValueOf(element, parametros.Values("QueryPaymentLogResponseElementsAmmount")));

                            foreach (XNode subnode in element.DescendantNodes())
                            {
                                if (subnode.NodeType == XmlNodeType.Element)
                                {
                                    XElement subelement = (XElement)subnode;
                                    if (subelement.Name.LocalName == parametros.Values("QueryPaymentLogResponseElementsAccountContainer"))
                                    {
                                        string nameParameter = ServiceClient.Common.GetValueOf(subelement, parametros.Values("QueryPaymentLogResponseElementsAccountContainerFilter"));
                                        if (nameParameter == parametros.Values("QueryPaymentLogResponseElementsAccountContainerFilterValue"))
                                        {
                                            item.Cuenta = ServiceClient.Common.GetValueOf(subelement, parametros.Values("QueryPaymentLogResponseElementsAccountContainerValue"));
                                            break;
                                        }
                                    }
                                }
                            }

                            @out.Items.Add(item);
                            @out.Operacion.AffectedRows += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ConsultaPagos", Detalle = ex.ToString() });
                @out = new Types.Services.GetPagoHistoricoResult() { Operacion = new Types.Common.ResultadoOperacion(ex) };
            }
            return @out;
        }

        public static Types.Common.BooleanResult ActivarFacturaElectronica(string accountId, string email)
        {
            Data.AddTrace(new Types.Tablas.Trace("ActivarFacturaElectronica - Inicio de la secuencia") { P1 = accountId, P2 = email });
            Types.Common.BooleanResult @out = new Types.Common.BooleanResult();
            try
            {
                Data.AddTrace(new Types.Tablas.Trace("Ingresando al try"));
                Data.AddTrace(new Types.Tablas.Trace("Ejecutando ModifyBillMedium_v1") { P1 = accountId, P2 = email });
                XDocument resultado = ServiceClient.FullStack.ModifyBillMedium(accountId, email);
                Data.AddTrace(new Types.Tablas.Trace("Resultado obtenido: " + resultado.Root.InnerXML()) { P1 = resultado.Root.InnerXML() });
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de errores."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("SoaExceptionFields");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                string codigoError = ServiceClient.Common.GetValueOf(resultado, parametros.Values("SoaExceptionFieldsAppCode"));
                if (codigoError == "#N/A")
                {
                    Data.AddTrace(new Types.Tablas.Trace("La factura electrónica se ha activado exitosamente para " + accountId + ", al correo " + email));
                    @out.Resultado = true;
                    @out.Operacion.AffectedRows = 1;
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                }
                else
                {
                    string mensajeError = ServiceClient.Common.GetValueOf(resultado, parametros.Values("SoaExceptionFieldsAppMessage"));
                    Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error en la SOA: " + codigoError));
                    Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ActivarFacturaElectronica", Detalle = "Ha ocurrido un error en la SOA: " + codigoError + " - " + mensajeError });
                    @out.Resultado = false;
                    @out.Operacion.AffectedRows = 0;
                    @out.Operacion.Detalle = "Ha ocurrido un error en la SOA: " + codigoError + " - " + mensajeError;
                    @out.Operacion.Mensaje = mensajeError;
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ErrorInterno;
                    @out.Operacion.SoaResult = new Types.Common.SoaResult();
                    @out.Operacion.SoaResult.SoaCode = codigoError;
                    @out.Operacion.SoaResult.SoaMessage = mensajeError;
                }
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error: " + ex.Message));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ActivarFacturaElectronica", Detalle = ex.ToString() });
                @out = new Types.Common.BooleanResult() { Operacion = new Types.Common.ResultadoOperacion(ex) };
            }
            return @out;
        }

        public static Types.Services.GetPagoResult RealizarPago(Types.FullStack.Pagos.InputPago data)
        {
            Data.AddTrace(new Types.Tablas.Trace("BusinessLogic.RealizarPago - Inicio de la secuencia") { P1 = JsonConvert.SerializeObject(data) });
            Types.Services.GetPagoResult @out = new Types.Services.GetPagoResult();
            try
            {
                /// WIXXXXX: Separamos el digito verificador del documento de identidad, si viene ==
                data.NumeroDocumento = data.NumeroDocumento; //Se envia con guion en rs no especifica quitarlo - Utils.QuitarDigitoVerificador(data.NumeroDocumento )
                /// WIXXXXX: FINAL =================================================================
                Data.AddTrace(new Types.Tablas.Trace("Ejecutando PayOperation") { P1 = JsonConvert.SerializeObject(data) });
                XDocument resultado = ServiceClient.FullStack.PayOperation(data);
                Data.AddTrace(new Types.Tablas.Trace("Resultado obtenido: " + resultado.Root.InnerXML()) { P1 = resultado.Root.InnerXML() });
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de errores."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("PayOperationResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                @out.Resultado.CodResultado = ServiceClient.Common.GetValueOf(resultado, parametros.Values("PayOperationResponseElementsStatus"));
                @out.Resultado.IdTiket = ServiceClient.Common.GetValueOf(resultado, parametros.Values("PayOperationResponseElementsIdTicket"));
                @out.Resultado.Resultado = ServiceClient.Common.GetValueOf(resultado, parametros.Values("PayOperationResponseElementsDescription"));
                @out.Resultado.UrlPasarela = ServiceClient.Common.GetValueOf(resultado, parametros.Values("PayOperationResponseElementsHostUrl"));
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error: " + ex.Message));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.RealizarPago", Detalle = ex.ToString() });
                @out = new Types.Services.GetPagoResult() { Operacion = new Types.Common.ResultadoOperacion(ex) };
            }
            return @out;
        }

        public static Types.Services.VerifyPagoResult VerificarPago(Types.FullStack.Pagos.InputVerifyPago data)
        {
            Data.AddTrace(new Types.Tablas.Trace("BusinessLogic.VerificarPago - Inicio de la secuencia") { P1 = JsonConvert.SerializeObject(data) });
            Types.Services.VerifyPagoResult @out = new Types.Services.VerifyPagoResult();
            try
            {
                Data.AddTrace(new Types.Tablas.Trace("Ejecutando PayOperation") { P1 = JsonConvert.SerializeObject(data) });
                XDocument resultado = ServiceClient.FullStack.PayOperationVerify(data);
                Data.AddTrace(new Types.Tablas.Trace("Resultado obtenido: " + resultado.Root.InnerXML()) { P1 = resultado.Root.InnerXML() });
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de errores."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("PayOperationResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                @out.Resultado.Status = ServiceClient.Common.GetValueOf(resultado, parametros.Values("PayOperationResponseElementsVerifyCode"));
                @out.Resultado.SelStatus = Utils.GetSelStatus(@out.Resultado.Status);
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("Ha ocurrido un error: " + ex.Message));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.VerificarPago", Detalle = ex.ToString() });
                @out = new Types.Services.VerifyPagoResult() { Operacion = new Types.Common.ResultadoOperacion(ex) };
            }
            return @out;
        }

        #region CU-012
        public static Types.Services.GetCategoriaSVACollectionResult GetCategoriasSVA()
        {
            Data.AddTrace(new Types.Tablas.Trace() { Accion = "GetCategoriasSVA - Inicio de la secuencia" });
            Types.Services.GetCategoriaSVACollectionResult @out = new Types.Services.GetCategoriaSVACollectionResult();
            try
            {
                Data.AddTrace(new Types.Tablas.Trace("GetCategoriasSVA - Consumiendo al bus FS"));
                XDocument xmlCategorias = ServiceClient.FullStack.QueryOfferingCategory();
                Data.AddTrace(new Types.Tablas.Trace("Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QueryOfferingCategoryResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                Data.AddTrace(new Types.Tablas.Trace("Buscando elementos con nombre " + parametros.Values("QueryOfferingCategoryResponseElementsFilter")));
                string[] nodos = parametros.Values("QueryOfferingCategoryResponseElementsFilter").Split('|');
                string[] ids = parametros.Values("QueryOfferingCategoryResponseElementsCategoryId").Split('|');
                string[] names = parametros.Values("QueryOfferingCategoryResponseElementsCategoryName").Split('|');
                foreach (XElement item in xmlCategorias.Descendants())
                {
                    if (Utils.FindStringInArray(item.Name.LocalName, nodos))
                    {
                        bool tieneHijos = false;
                        foreach (XElement subitem in item.Descendants())
                        {
                            if (Utils.FindStringInArray(subitem.Name.LocalName, nodos))
                            {
                                tieneHijos = true;
                                break;
                            }
                        }

                        if (!tieneHijos)
                        {
                            Data.AddTrace(new Types.Tablas.Trace("Encontrado " + item.Name.LocalName) { P1 = item.ToString() });
                            Types.FullStack.CategoriaSVA categoria = new Types.FullStack.CategoriaSVA();
                            string id = "id";
                            string desc = "descripcion";
                            string clave = "";
                            string valor = "";
                            foreach (XElement subitem in item.Descendants())
                            {
                                clave = subitem.Name.LocalName;
                                valor = subitem.Value;
                                Data.AddTrace(new Types.Tablas.Trace(item.Name.LocalName + " > " + clave + ": " + valor));
                                if (Utils.FindStringInArray(clave, ids))
                                {
                                    id = valor;
                                }
                                if (Utils.FindStringInArray(clave, names))
                                {
                                    desc = valor;
                                }
                            }
                            Data.AddTrace(new Types.Tablas.Trace(desc + " => " + id));
                            categoria.Id = id;
                            categoria.Descripcion = desc;
                            @out.Items.Add(categoria);
                            @out.Operacion.AffectedRows += 1;
                        }
                    }
                }
                @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
            }
            catch (Exception ex)
            {
                @out = new Types.Services.GetCategoriaSVACollectionResult() { Operacion = new Types.Common.ResultadoOperacion(ex) };
            }
            return @out;
        }

        public static Types.Services.GetOfertaSVACollectionResult GetOfertasSVA(string number)
        {
            Data.AddTrace(new Types.Tablas.Trace() { Accion = "GetOfertasSVA - Inicio de la secuencia" });
            Types.Services.GetOfertaSVACollectionResult @out = new Types.Services.GetOfertaSVACollectionResult();
            try
            {
                Data.AddTrace(new Types.Tablas.Trace("GetOfertasSVA - Obteniendo Categorias"));
                Types.Services.GetCategoriaSVACollectionResult categorias = GetCategoriasSVA();
                if (categorias.Operacion.Codigo == Types.Enums.ResultadosOperacion.ProcesoOk)
                {
                    foreach (Types.FullStack.CategoriaSVA item in categorias.Items)
                    {
                        Data.AddTrace(new Types.Tablas.Trace("GetOfertasSVA - Obteniendo ofertas para " + item.Id + ": " + item.Descripcion));
                        XDocument xmlOfertas = ServiceClient.FullStack.QuerySuscriberAvailableOffering(number, item.Id);
                        Data.AddTrace(new Types.Tablas.Trace("GetOfertasSVA - Se cargan los parámetros de lectura de resultados."));
                        Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("QuerySuscriberAvailableOfferingResponseElements");
                        Data.AddTrace(new Types.Tablas.Trace("GetOfertasSVA - Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                        Data.AddTrace(new Types.Tablas.Trace("GetOfertasSVA - Buscando elementos con nombre " + parametros.Values("QuerySuscriberAvailableOfferingResponseElementsContainer")));
                        foreach (XElement element in xmlOfertas.Descendants())
                        {
                            if (element.Name.LocalName == parametros.Values("QuerySuscriberAvailableOfferingResponseElementsContainer"))
                            {
                                Types.FullStack.OfertaSVA oferta = new Types.FullStack.OfertaSVA();
                                oferta.Id = ServiceClient.Common.GetValueOf(element, parametros.Values("QuerySuscriberAvailableOfferingResponseElementsOfferId"));
                                oferta.Descripcion = ServiceClient.Common.GetValueOf(element, parametros.Values("QuerySuscriberAvailableOfferingResponseElementsName"));
                                foreach (XNode subnode in element.DescendantNodes())
                                {
                                    if (subnode.NodeType == XmlNodeType.Element)
                                    {
                                        XElement subelement = (XElement)subnode;
                                        if (subelement.Name.LocalName == parametros.Values("QuerySuscriberAvailableOfferingResponseElementsValueContainer"))
                                        {
                                            string filtro = ServiceClient.Common.GetValueOf(subelement, parametros.Values("QuerySuscriberAvailableOfferingResponseElementsValueFilter"));
                                            if (filtro == parametros.Values("QuerySuscriberAvailableOfferingResponseElementsValueFilterValue"))
                                            {
                                                oferta.Descripcion = ServiceClient.Common.GetValueOf(subelement, parametros.Values("QuerySuscriberAvailableOfferingResponseElementsValueElement")) + "<|>" + oferta.Descripcion;
                                            }
                                        }
                                    }
                                }
                                string[] partes = oferta.Descripcion.Split('$');
                                partes = partes[1].Split(' ');
                                oferta.Valor = partes[0].Replace(".", "");
                                // oferta.Valor = ServiceClient.Common.GetValueOf(element, parametros.Values("QuerySuscriberAvailableOfferingResponseElementsValue"));
                                Data.AddTrace(new Types.Tablas.Trace("GetOfertasSVA - " + oferta.Id + ": " + oferta.Descripcion + ", $ " + oferta.Valor));
                                @out.Items.Add(oferta);
                                @out.Operacion.AffectedRows += 1;
                            }
                        }
                    }
                    Data.AddTrace(new Types.Tablas.Trace("GetOfertasSVA - " + @out.Operacion.AffectedRows.ToString() + " registros procesados"));
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                }
                else
                {
                    Data.AddTrace(new Types.Tablas.Trace("GetOfertasSVA - Ha ocurrido un error: " + categorias.Operacion.Mensaje));
                    @out = new Types.Services.GetOfertaSVACollectionResult() { Operacion = categorias.Operacion };
                }
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("GetOfertasSVA - Ha ocurrido un error: " + ex.Message));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "FullStack.BusinessLogic.GetOfertasSVA", Detalle = ex.ToString() });
                @out = new Types.Services.GetOfertaSVACollectionResult() { Operacion = new Types.Common.ResultadoOperacion(ex) };
            }
            return @out;
        }

        public static Types.Services.GetContratacionSVAResult ContratarOfertaSVA(string number, string idOffering)
        {
            Data.AddTrace(new Types.Tablas.Trace("ContratarOfertaSVA - Inicio de la secuencia") { P1 = number, P2 = idOffering });
            Types.Services.GetContratacionSVAResult @out = new Types.Services.GetContratacionSVAResult();
            try
            {
                Data.AddTrace(new Types.Tablas.Trace("ContratarOfertaSVA - Ingresando al Try"));
                Data.AddTrace(new Types.Tablas.Trace("ContratarOfertaSVA - Se cargan los parámetros de lectura de resultados."));
                Types.Services.GetParametroCollectionResult parametros = Data.GetConfigValues("ChangeSuplementaryOfferingResponseElements");
                Data.AddTrace(new Types.Tablas.Trace("ContratarOfertaSVA - Total parámetros obtenidos: " + parametros.Items.Count.ToString()));
                Data.AddTrace(new Types.Tablas.Trace("ContratarOfertaSVA - Ejecutando método en el bus FS"));
                XDocument resultado = ServiceClient.FullStack.ChangeSuplementaryOffering(number, idOffering);
                Data.AddTrace(new Types.Tablas.Trace("ContratarOfertaSVA - Obteniendo resultado de la operación"));
                @out.Resultado.OrdenServicioId = ServiceClient.Common.GetValueOf(resultado, parametros.Values("ChangeSuplementaryOfferingResponseElementsIdServiceOrder"));
                try
                {
                    @out.Operacion.SoaResult = new Types.Common.SoaResult();
                    @out.Operacion.SoaResult.SoaCode = ServiceClient.Common.GetValueOf(resultado, "exceptionCode");
                    @out.Operacion.SoaResult.SoaMessage = ServiceClient.Common.GetValueOf(resultado, "exceptionMessage") + ": " + ServiceClient.Common.GetValueOf(resultado, "exceptionAppMessage");
                }
                catch { }
                @out.Operacion.SoaResult.SoaCode = @out.Operacion.SoaResult.SoaCode.Trim();
                if ((@out.Operacion.SoaResult.SoaCode != "") && (@out.Operacion.SoaResult.SoaCode != "0") && (@out.Operacion.SoaResult.SoaCode != "#N/A"))
                {
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ErrorInterno;
                    @out.Operacion.Mensaje = ServiceClient.Common.GetValueOf(resultado, "exceptionAppMessage");
                    @out.Operacion.Detalle = @out.Operacion.SoaResult.SoaCode + ": " + @out.Operacion.SoaResult.SoaMessage;
                    Data.AddTrace(new Types.Tablas.Trace("ContratarOfertaSVA - Se recibió un error desde el busFS: " + @out.Operacion.Detalle));
                    Data.AddException(new Types.Tablas.Exception() { });
                }
                else
                {
                    Data.AddTrace(new Types.Tablas.Trace("ContratarOfertaSVA - Resultado de la operación: " + @out.Resultado.OrdenServicioId));
                    @out.Operacion.AffectedRows = 1;
                    @out.Operacion.Codigo = Types.Enums.ResultadosOperacion.ProcesoOk;
                }
            }
            catch (Exception ex)
            {
                Data.AddTrace(new Types.Tablas.Trace("ContratarOfertaSVA - Ha ocurrido un error: " + ex.Message));
                Data.AddException(new Types.Tablas.Exception() { Modulo = "BusinessLogic.ContratarOfertaSVA", Detalle = ex.ToString() });
                @out = new Types.Services.GetContratacionSVAResult() { Operacion = new Types.Common.ResultadoOperacion(ex) };
            }
            return @out;
        }
        #endregion

        #endregion
    }

    public static class XElementExtension
    {
        public static string InnerXML(this XElement el)
        {
            var reader = el.CreateReader();
            reader.MoveToContent();
            return reader.ReadInnerXml();
        }
    }
}