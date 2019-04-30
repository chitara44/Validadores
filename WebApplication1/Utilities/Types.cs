using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Tmc.Servicios.FullStack
{
    public class Types
    {
        public class Tablas
        {
            public class Parametro
            {
                public int Id { get; set; }
                public string Clave { get; set; }
                public string Valor { get; set; }
            }

            public class ParametroCollection : List<Parametro>
            {
            }

            public class AppId
            {
                public int Id { get; set; }
                public string Aplicacion { get; set; }
                public string Responsable { get; set; }
                public string ResponsableCorreo { get; set; }
                public int Status { get; set; }
            }

            public class AppIdCollection : List<AppId>
            {
            }

            public class Exception
            {
                public int Id { get; set; }
                public int AppId { get; set; }
                public DateTime Fecha { get; set; }
                public string Modulo { get; set; }
                public string Detalle { get; set; }
                public bool Resuelto { get; set; }
            }

            public class ExceptionCollection : List<Exception>
            {
            }

            public class Trace
            {
                public int Id { get; set; }
                public int AppId { get; set; }
                public DateTime Fecha { get; set; }
                public string Accion { get; set; }
                public string P1 { get; set; }
                public string P2 { get; set; }
                public string P3 { get; set; }
                public string P4 { get; set; }
                public string P5 { get; set; }
                public string P6 { get; set; }
                public string P7 { get; set; }
                public string P8 { get; set; }

                public Trace()
                {
                }

                public Trace(string accion)
                {
                    Accion = accion;
                }
            }

            public class TraceCollection : List<Trace>
            {
            }
        }

        public class Enums
        {
            public enum ResultadosOperacion
            {
                NoEjecuto,
                ProcesoOk,
                ErrorInterno
            }

            public enum TraceTypes
            {
                Information,
                Warning,
                InternalError
            }
        }

        public class Common
        {
            public class ResultadoOperacion
            {
                string currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                public Enums.ResultadosOperacion Codigo { get; set; }
                public int NewRowId { get; set; }
                public int AffectedRows { get; set; }
                public string Mensaje { get; set; }
                public string Detalle { get; set; }
                public SoaResult SoaResult { get; set; }
                public string ServiceVersion { get { return currentVersion; } }

                public ResultadoOperacion()
                {
                }

                public ResultadoOperacion(Exception ex)
                {
                    Codigo = Enums.ResultadosOperacion.ErrorInterno;
                    AffectedRows = 0;
                    Mensaje = ex.Message;
                    Detalle = ex.ToString();
                    SoaResult = new SoaResult();
                    currentVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                }
            }

            public class ServiceResult
            {
                public ResultadoOperacion Operacion { get; set; }

                public ServiceResult()
                {
                    Operacion = new ResultadoOperacion();
                }
            }

            public class StringResult : ServiceResult
            {
                public string Resultado { get; set; }
            }

            public class IntegerResult : ServiceResult
            {
                public int Resultado { get; set; }
            }

            public class LongResult : ServiceResult
            {
                public Int64 Resultado { get; set; }
            }

            public class BooleanResult : ServiceResult
            {
                public bool Resultado { get; set; }
            }

            public class SoaResult
            {
                public string SoaCode { get; set; }
                public string SoaMessage { get; set; }
            }

            public class ComboItem
            {
                public string Texto { get; set; }
                public string Valor { get; set; }

                public ComboItem()
                {
                    Texto = "";
                    Valor = "";
                }
            }

            public class ComboItemCollection : List<ComboItem>
            {
            }

            public class ComboItemCollectionResult : ServiceResult
            {
                public ComboItemCollection Items { get; set; }

                public ComboItemCollectionResult()
                {
                    Items = new ComboItemCollection();
                }
            }
        }

        public class FullStack
        {
            public class Banco
            {
                public string Nombre { get; set; }
                public string Codigo { get; set; }
            }

            public class BancoCollection : List<Banco>
            {
            }

            public class Direccion
            {
                public string CodigoPostal { get; set; }
                public string Ciudad { get; set; }
                public string Departamento { get; set; }
                public string DireccionExacta { get; set; }
            }

            public class DireccionCollection : List<Direccion>
            {
            }

            public class InformacionContacto
            {
                public string Telefono { get; set; }
                public string Correo { get; set; }
            }

            public class InformacionContactoCollection : List<InformacionContacto>
            {
            }

            public class InformacionPago
            {
                public string NumeroPago { get; set; }
                public string NumeroTransaccion { get; set; }
                public DateTime FechaPrimerPago { get; set; }
                public Single MontoOriginal { get; set; }
                public string MetodoPago { get; set; }
            }

            public class InformacionPagoCollection : List<InformacionPago>
            {
            }

            #region RS14694 - Coliving corporativo
            public class CreateInteractionInput
            {
                public string PrimaryTelephoneNumber { get; set; }
                //public string ModalityDesc { get { return Data.GetConfigValue("CreateInteractionDefaultModalityDesc").Resultado; } }
                //public string IDGeographicAddress { get { return Data.GetConfigValue("CreateInteractionDefaultIDGeographicAddress").Resultado; } }
                //public string ContactTypePartyAccountContact { get { return Data.GetConfigValue("CreateInteractionDefaultContactTypePartyAccountContact").Resultado; } }
                public string NumeroCelular { get; set; }
                public string Nombre { get; set; }
                public string Apellido { get; set; }
                public string TipoDocumento { get; set; }
                public string NumeroDocumento { get; set; }
                public string Usuario { get; set; }
                public string Correo { get; set; }
                /// <summary>
                /// Acción a reportar: { Registro | Agregar | NotifNewProd | Eliminar | FacturaAgregar | FacturaModificar | SetPrivado | UnsetPrivado | NombreNumeroAgregar | NombreNumeroModificar }
                /// </summary>
                public string Accion { get; set; }
                public string NombreProducto { get; set; }
                //public string DescriptionBusinessInteraction
                //{
                //    get
                //    {
                //        string @out = Data.GetConfigValue("CreateInteractionDefaultDescriptionBusinessInteraction" + Accion).Resultado;
                //        @out = @out.Replace("::0::", NumeroCelular);
                //        @out = @out.Replace("::1::", Nombre);
                //        @out = @out.Replace("::2::", Apellido);
                //        @out = @out.Replace("::3::", TipoDocumento);
                //        @out = @out.Replace("::4::", NumeroDocumento);
                //        @out = @out.Replace("::5::", Usuario);
                //        @out = @out.Replace("::6::", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                //        @out = @out.Replace("::7::", Correo);
                //        @out = @out.Replace("::8::", NombreProducto);
                //        return @out;
                //    }
                //}
                public string Mensaje { get; set; }

                public CreateInteractionInput()
                {
                    PrimaryTelephoneNumber = "";
                    NumeroCelular = "";
                    Nombre = "";
                    Apellido = "";
                    TipoDocumento = "";
                    NumeroDocumento = "";
                    Usuario = "";
                    Correo = "";
                    Accion = "N/A";
                    NombreProducto = "";
                    Mensaje = "";
                }
            }

            public class GetCustomerDetailOutput : Common.SoaResult
            {
                public string IdCustomer { get; set; }
                public string OrganizationName { get; set; }
                public string FirstNameCustomer { get; set; }
                public string LastNameCustomer { get; set; }
                public string CustomerSegment { get; set; }
                public string CustomerSubSegment { get; set; }
                public string SeluSubSegment { get; set; }
                public string CustomerMail { get; set; }
                public bool EsEmpresa { get; set; }
                public string NameCustomer { get; set; }

                public GetCustomerDetailOutput()
                {
                    IdCustomer = "";
                    OrganizationName = "";
                    FirstNameCustomer = "";
                    LastNameCustomer = "";
                    CustomerSegment = "";
                    CustomerSubSegment = "";
                    SeluSubSegment = "";
                    CustomerMail = "";
                    EsEmpresa = false;
                    NameCustomer = "";
                }
            }

            public class CuentaPorNit
            {
                public int Id { get; set; }
                public string Nit { get; set; }
                public string Cuenta { get; set; }
                public bool DuplicadoEnScl { get; set; }

                public CuentaPorNit()
                {
                    this.Id = 0;
                    this.Nit = "";
                    this.Cuenta = "";
                    this.DuplicadoEnScl = false;
                }
            }

            public class CuentaPorNitCollection : List<CuentaPorNit>
            {
            }

            public class CelularPorCuenta
            {
                public string Cuenta { get; set; }
                public string NumeroCelular { get; set; }
                public Single SaldoPendiente { get; set; }
                public DateTime FechaLimitePago { get; set; }
                public string NombreUsuario { get; set; }
                public string NombrePlan { get; set; }
                public DateTime FechaActivacion { get; set; }
                public string Estado { get; set; }
                public string ModeloEquipo { get; set; }

                public CelularPorCuenta()
                {
                    Cuenta = "";
                    NumeroCelular = "";
                    SaldoPendiente = 0;
                    FechaLimitePago = DateTime.Now;
                    NombreUsuario = "";
                    NombrePlan = "";
                    FechaActivacion = DateTime.Now;
                    Estado = "";
                    ModeloEquipo = "";
                }
            }

            public class CelularPorCuentaCollection : List<CelularPorCuenta>
            {
            }

            #region CU-012
            /// <summary>
            /// Contiene la informacion de una categoria SVA
            /// </summary>
            public class CategoriaSVA
            {
                public string Id { get; set; }
                public string Descripcion { get; set; }

                public CategoriaSVA()
                {
                    Id = "";
                    Descripcion = "";
                }
            }

            /// <summary>
            /// Contiene un grupo de categorias SVA
            /// </summary>
            public class CategoriaSVACollection : List<CategoriaSVA>
            {
            }

            /// <summary>
            /// Contiene la informacion de una oferta SVA
            /// </summary>
            public class OfertaSVA
            {
                public string Id { get; set; }
                public string Descripcion { get; set; }
                public string Valor { get; set; }

                public OfertaSVA()
                {
                    Id = "";
                    Descripcion = "";
                    Valor = "";
                }
            }

            /// <summary>
            /// Contiene un grupo de ofertas SVA
            /// </summary>
            public class OfertaSVACollection : List<OfertaSVA>
            {
            }

            public class ContratacionSVA
            {
                public string OrdenServicioId { get; set; }

                public ContratacionSVA()
                {
                    OrdenServicioId = "";
                }
            }
            #endregion

            public class Pagos
            {
                public enum TiposPago
                {
                    PagoFactura,
                    Recarga
                }

                public class InputPago
                {
                    public TiposPago TipoPago { get; set; }
                    public string Direccion { get; set; }
                    public string Ciudad { get; set; }
                    public string Departamento { get; set; }
                    public string CodGeografico { get; set; }
                    public string CodPostal { get; set; }
                    public string CodCliente { get; set; }
                    public string Cli_Hijo { get; set; }
                    public string Segmento { get; set; }
                    public string TipoDocumento { get; set; }
                    public string NumeroDocumento { get; set; }
                    public string NombreRazonSocial { get; set; }
                    public string Numero { get; set; }
                    public string Correo { get; set; }
                    public Single Monto { get; set; }
                    public string Factura { get; set; }
                    public string IdBanco { get; set; }
                    public string Banco { get; set; }
                    public string Cuenta { get; set; }
                    public string SessionID { get; set; }
                    public string IPAddress { get; set; }
                    public string UrlRetorno { get; set; }

                    //public InputPago()
                    //{
                    //    Services.GetParametroCollectionResult parametros = Data.GetConfigValues("PayOperation");
                    //    TipoPago = TiposPago.PagoFactura;
                    //    Direccion = parametros.Values("PayOperationDefaultFormattedRespAddress");
                    //    Ciudad = parametros.Values("PayOperationDefaultCintyName");
                    //    Departamento = parametros.Values("PayOperationDefaultStateOrProvince");
                    //    CodGeografico = parametros.Values("PayOperationDefaultIdCod");
                    //    CodPostal = parametros.Values("PayOperationDefaultPostalCode");
                    //    TipoDocumento = parametros.Values("PayOperationDefaultTypeNationalCardIdentification");
                    //    Correo = parametros.Values("PayOperationDefaultEmailAddress");
                    //    SessionID = parametros.Values("PayOperationDefaultDeviceSessionId");
                    //    UrlRetorno = "";
                    //    CodCliente = "";
                    //    Cli_Hijo = "";
                    //    Segmento = "";
                    //    NumeroDocumento = "";
                    //    NombreRazonSocial = "";
                    //    Numero = "";
                    //    Monto = 0;
                    //    Factura = "";
                    //    IdBanco = "";
                    //    Banco = "";
                    //    Cuenta = "";
                    //    IPAddress = "";
                    //}
                }

                public class OutputPago
                {
                    public string CodResultado { get; set; }
                    public string Resultado { get; set; }
                    public string UrlPasarela { get; set; }
                    public string IdTiket { get; set; }

                    public OutputPago()
                    {
                        CodResultado = "";
                        Resultado = "";
                        UrlPasarela = "";
                        IdTiket = "";
                    }
                }

                public class InputVerifyPago
                {
                    public string TipoDocumento { get; set; }
                    public string NumeroDocumento { get; set; }
                    public string NombreRazonSocial { get; set; }
                    public string Correo { get; set; }
                    public Single Monto { get; set; }
                    public string IdTicket { get; set; }

                    //public InputVerifyPago()
                    //{
                    //    Services.GetParametroCollectionResult parametros = Data.GetConfigValues("PayOperation");
                    //    TipoDocumento = parametros.Values("PayOperationDefaultTypeNationalCardIdentification");
                    //    NumeroDocumento = "";
                    //    NombreRazonSocial = "";
                    //    Correo = parametros.Values("PayOperationDefaultEmailAddress");
                    //    Monto = 0;
                    //    IdTicket = "";
                    //}
                }

                public class OutputVerifyPago
                {
                    public string Status { get; set; }
                    public string SelStatus { get; set; }
                    public string Descripcion { get; set; }
                    public string Response { get; set; }
                    public string StatusResponse { get; set; }

                    public OutputVerifyPago()
                    {
                        Status = "";
                        Descripcion = "";
                        Response = "";
                        StatusResponse = "";
                        SelStatus = "";
                    }
                }

                public class InputHistorico
                {
                    public string Cuenta { get; set; }
                    public string FechaInicio { get; set; }
                    public string FechaFinal { get; set; }

                    public InputHistorico()
                    {
                        Cuenta = "";
                        FechaInicio = "";
                        FechaFinal = "";
                    }
                }

                public class HistoricoElement
                {
                    public string IdPago { get; set; }
                    public DateTime FechaPago { get; set; }
                    public Single Monto { get; set; }
                    public string Descripcion { get; set; }
                    public string IdTrx { get; set; }
                    public string Cuenta { get; set; }

                    public HistoricoElement()
                    {
                        IdPago = "";
                        FechaPago = DateTime.Now;
                        Monto = 0;
                        Descripcion = "";
                        IdTrx = "";
                        Cuenta = "";
                    }
                }

                public class HistoricoElementCollection : List<HistoricoElement>
                {
                }

                public class OutputHistorico
                {
                    public HistoricoElementCollection Items { get; set; }

                    public OutputHistorico()
                    {
                        Items = new HistoricoElementCollection();
                    }
                }
            }

            #endregion
        }

        public class BusinessLogic
        {
            public class EsClienteMigradoResult : Common.ServiceResult
            {
                public string Codigo { get; set; }
                public string Descripcion { get; set; }
                public string Detalle { get; set; }
                public string Msisdn { get; set; }
                public string Source { get; set; }
                public string FullStackSuscriberId { get; set; }
                public bool EsMigrado { get; set; }

                public EsClienteMigradoResult()
                {
                    Codigo = "";
                    Descripcion = "";
                    Detalle = "";
                    Msisdn = "";
                    Source = "";
                    FullStackSuscriberId = "";
                    EsMigrado = false;
                }
            }

            /// <summary>
            /// Contiene la información de un celular corporativo en FullStack.
            /// </summary>
            public class InfoCelularPorCuentaFS
            {
                public string Cuenta { get; set; }
                public string Numero { get; set; }
                public Single Saldo { get; set; }
                public DateTime LimitePago { get; set; }
                public string NombreUsuario { get; set; }
                public string CodigoPlan { get; set; }
                public string Plan { get; set; }
                public DateTime Activacion { get; set; }
                public string Estado { get; set; }
                public string Modelo { get; set; }
                public string Marca { get; set; }

                public InfoCelularPorCuentaFS()
                {
                    Cuenta = "";
                    Numero = "";
                    Saldo = 0;
                    LimitePago = new DateTime(1901, 1, 1);
                    NombreUsuario = "";
                    CodigoPlan = "";
                    Plan = "";
                    Activacion = new DateTime(1901, 1, 1);
                    Estado = "";
                    Modelo = "";
                    Marca = "";
                }
            }

            /// <summary>
            /// Contiene la información de un grupo de celulares corporativos en FullStack.
            /// </summary>
            public class InfoCelularPorCuentaFSCollection : List<InfoCelularPorCuentaFS>
            {
                /// <summary>
                /// Indica si un número está registrado en el listado de líneas de la cuenta.
                /// </summary>
                /// <param name="numero">Número telefónico a consultar.</param>
                /// <returns></returns>
                public bool Contiene(string numero)
                {
                    bool @out = false;
                    foreach (InfoCelularPorCuentaFS item in this)
                    {
                        if (item.Numero == numero)
                        {
                            @out = true;
                            break;
                        }
                    }
                    return @out;
                }
            }

            /// <summary>
            /// Contiene la información de una cuenta corporativa en FullStack.
            /// </summary>
            public class InfoCuentaPorNitFS
            {
                public string IdFs { get; set; }
                public string Nit { get; set; }
                public string Cuenta { get; set; }
                public bool DuplicadoEnScl { get; set; }
                public string NumeroFactura { get; set; }
                public InfoCelularPorCuentaFSCollection Lineas { get; set; }

                public InfoCuentaPorNitFS()
                {
                    IdFs = "";
                    Nit = "";
                    Cuenta = "";
                    DuplicadoEnScl = false;
                    NumeroFactura = "";
                    Lineas = new InfoCelularPorCuentaFSCollection();
                }
            }

            /// <summary>
            /// Contiene la información de un grupo de cuentas corporativas en FullStack.
            /// </summary>
            public class InfoCuentaPornitFSCollection : List<InfoCuentaPorNitFS>
            {
                /// <summary>
                /// Verifica si ya se ha registrado un número de cuenta en el listado.
                /// </summary>
                /// <param name="cuenta">Número de cuenta a verificar</param>
                /// <returns></returns>
                public bool Contiene(string cuenta)
                {
                    bool @out = false;
                    foreach (InfoCuentaPorNitFS item in this)
                    {
                        if (item.Cuenta.Trim() == cuenta)
                        {
                            @out = true;
                            break;
                        }
                    }
                    return @out;
                }

                /// <summary>
                /// Indica el total de líneas registradas para la totalidad de las cuentas registradas.
                /// </summary>
                public int TotalLineas
                {
                    get
                    {
                        int @out = 0;
                        foreach (InfoCuentaPorNitFS item in this)
                        {
                            @out += item.Lineas.Count;
                        }
                        return @out;
                    }
                }
            }

            public class AccountInfo
            {
                public string NameCustomerAccount { get; set; }
                public string AccountCode { get; set; }

                public AccountInfo()
                {
                    NameCustomerAccount = "";
                    AccountCode = "";
                }
            }

            public class AccountInfoCollection : List<AccountInfo>
            {
            }

            public class InfoCuenta
            {
                public string Cuenta { get; set; }
                public string RazonSocial { get; set; }
                public string Nombres { get; set; }
                public string Apellidos { get; set; }
                public bool FacturaElectronicaEnabled { get; set; }
                public string Email { get; set; }
                public string ContactTypePartyAccountContact { get; set; }
                public AccountInfoCollection Cuentas { get; set; }

                public InfoCuenta()
                {
                    Cuenta = "";
                    RazonSocial = "";
                    Nombres = "";
                    Apellidos = "";
                    FacturaElectronicaEnabled = false;
                    Email = "";
                    ContactTypePartyAccountContact = "";
                    Cuentas = new AccountInfoCollection();
                }
            }

            public class InfoFactura
            {
                public Single SaldoPendiente { get; set; }
                public DateTime FechaLimitePago { get; set; }
                public string NumeroFactura { get; set; }

                public InfoFactura()
                {
                    SaldoPendiente = 0;
                    FechaLimitePago = DateTime.Now;
                    NumeroFactura = "";
                }
            }

            public class GeneraOrdenInput
            {
                /// <summary>
                /// Corresponde al paramName del banco que seleccionó el usuario de la lista desplegable que es llenada por el proxy GetBankList.
                /// </summary>
                public string BancoCodigo { get; set; }
                /// <summary>
                /// Corresponde al paramValue del banco que seleccionó el usuario que es llenada por el proxy GetBankList.
                /// </summary>
                public string BancoNombre { get; set; }
                /// <summary>
                /// Si el parámetro de salida  formattedRespAddress del WS GetCustomerDetail tiene el valor “Structured” se debe concatenar la información del campo complejo formatedAddressItemc separando los valores con un espacio. Si el parámetro de salida  formattedRespAddress del WS GetCustomerDetail tiene el valor “Unstructured” se envia el campo formattedRespSplitAddress.
                /// </summary>
                public string Direccion { get; set; }
                /// <summary>
                /// Corresponde al parámetro townIdGeographicAddress de salida del WS GetCustomerDetail.
                /// </summary>
                public string Ciudad { get; set; }
                /// <summary>
                /// Corresponde al parámetro de salida stateOrProvinceGeographicAddress  del WS GetCustomerDetail.
                /// </summary>
                public string Departamento { get; set; }
                /// <summary>
                /// Corresponde al parámetro de salida postalCodeAddress del WS GetCustomerDetail.
                /// </summary>
                public string CodigoPostal { get; set; }
                /// <summary>
                /// Corresponde al campo idEnFS que se encuentra en la tabla CuentasPorNitFS.
                /// </summary>
                public string IdCustomer { get; set; }
                /// <summary>
                /// Corresponde al número de documento del usuario que se autenticó.
                /// </summary>
                public string NumeroNit { get; set; }
                /// <summary>
                /// Corresponde a la concatenación de los parámetros de salida firstNameCustomer y lastNameCustomer  del WS GetAccountDetail.
                /// </summary>
                public string RazonSocial { get; set; }
                /// <summary>
                /// Si el parámetro de salida contactTypePartyAccountContact del WS GetAccountDetail es igual a 1, se toma el  dato del parámetro ValueParameter del tipo complejo ContactInformationItem. Si no viene se debe tomar el primer número celular de la columna NumeroCelular de la tabla CelularesPorCuentaFS.
                /// </summary>
                public string ContactoTelefono { get; set; }
                /// <summary>
                /// Si el parámetro de salida contactTypePartyAccountContact del WS GetAccountDetail es igual a 0, se toma el  dato del parámetro ValueParameter del tipo complejo ContactInformationItem. Si no viene se envía el valor “notengo@email.com”.
                /// </summary>
                public string ContactoCorreo { get; set; }
                /// <summary>
                /// Corresponde al valor a pagar que aparece en la página.
                /// </summary>
                public Single Monto { get; set; }
                /// <summary>
                /// Corresponde a la url de servicio en línea corporativo donde debe retornar cuando el usuario finalice el pago en la página del banco.
                /// </summary>
                public string UrlRetorno { get; set; }
                /// <summary>
                /// Corresponde al valor IdEnFS de la table CuentasPorNitFS de la cuenta seleccionada para realizar el pago.
                /// </summary>
                public string IdServiceOrder { get; set; }

                public GeneraOrdenInput()
                {
                    BancoCodigo = "";
                    BancoNombre = "";
                    Direccion = "";
                    Ciudad = "";
                    Departamento = "";
                    CodigoPostal = "";
                    IdCustomer = "";
                    NumeroNit = "";
                    RazonSocial = "";
                    ContactoTelefono = "";
                    ContactoCorreo = "notengo@email.com";
                    Monto = 0;
                    UrlRetorno = "";
                    IdServiceOrder = "";
                }
            }

            public class GeneraOrdenOutput
            {
                /// <summary>
                /// Resultado de la operación.
                /// </summary>
                public string Result { get; set; }
                /// <summary>
                /// No importa el valor enviado.
                /// </summary>
                public string ResultCode { get; set; }
                /// <summary>
                /// Mensaje de respuesta del servicio.
                /// </summary>
                public string ResponseMessage { get; set; }
                /// <summary>
                /// Corresponde a la url a la que debe redireccionar servicio en línea para que lleve al usuario a la página del banco que seleccionó.
                /// </summary>
                public string HostUrl { get; set; }
                /// <summary>
                /// Id de la operación.
                /// </summary>
                public string OperationIdPaymentOp { get; set; }

                public GeneraOrdenOutput()
                {
                    Result = "";
                    ResultCode = "";
                    ResponseMessage = "";
                    HostUrl = "";
                    OperationIdPaymentOp = "";
                }
            }

            public class InfoPago
            {
                public string SerialNumber { get; set; }
                public string TradeTimer { get; set; }
                public Single OriginalAmmount { get; set; }
                public string PaymentMethod { get; set; }
                public string ExternalSerialNumber { get; set; }
                public string IDCustomerAccount { get; set; }

                public InfoPago()
                {
                    SerialNumber = "";
                    TradeTimer = "";
                    OriginalAmmount = 0;
                    PaymentMethod = "";
                    ExternalSerialNumber = "";
                    IDCustomerAccount = "";
                }
            }

            public class InfoPagoCollection : List<InfoPago>
            {
            }
        }

        public class Services
        {
            #region Comunes
            public class GetParametroResult : Common.ServiceResult
            {
                public Tablas.Parametro Resultado { get; set; }
            }

            public class GetParametroCollectionResult : Common.ServiceResult
            {
                public Tablas.ParametroCollection Items { get; set; }

                public string Values(string Key)
                {
                    string @out = "";
                    foreach (Tablas.Parametro item in Items)
                    {
                        if (item.Clave.Trim().ToLower() == Key.Trim().ToLower())
                        {
                            @out = item.Valor;
                            break;
                        }
                    }
                    return @out;
                }

                public GetParametroCollectionResult()
                {
                    Items = new Tablas.ParametroCollection();
                }
            }

            public class GetAppIdResult : Common.ServiceResult
            {
                public Tablas.AppId Resultado { get; set; }
            }

            public class GetAppIdCollectionResult : Common.ServiceResult
            {
                public Tablas.AppIdCollection Items { get; set; }

                public GetAppIdCollectionResult()
                {
                    Items = new Tablas.AppIdCollection();
                }
            }

            public class GetExceptionResult : Common.ServiceResult
            {
                public Tablas.Exception Resultado { get; set; }
            }

            public class GetExceptionCollectionResult : Common.ServiceResult
            {
                public Tablas.ExceptionCollection Items { get; set; }

                public GetExceptionCollectionResult()
                {
                    Items = new Tablas.ExceptionCollection();
                }
            }

            public class GetTraceResult : Common.ServiceResult
            {
                public Tablas.Trace Resultado { get; set; }
            }

            public class GetTraceCollectionResult : Common.ServiceResult
            {
                public Tablas.TraceCollection Items { get; set; }

                public GetTraceCollectionResult()
                {
                    Items = new Tablas.TraceCollection();
                }
            }
            #endregion

            #region RS14694 - Coliving corporativo
            public class GetInfoCuentaResult : Common.ServiceResult
            {
                public BusinessLogic.InfoCuenta Resultado { get; set; }

                public GetInfoCuentaResult()
                {
                    Resultado = new BusinessLogic.InfoCuenta();
                }
            }

            public class GetCusstomerDetailResult : Common.ServiceResult
            {
                public FullStack.GetCustomerDetailOutput Resultado { get; set; }

                public GetCusstomerDetailResult()
                {
                    Resultado = new FullStack.GetCustomerDetailOutput();
                }
            }

            public class GetCuentasPorNitResult : Common.ServiceResult
            {
                public FullStack.CuentaPorNitCollection Items { get; set; }

                public GetCuentasPorNitResult()
                {
                    Items = new FullStack.CuentaPorNitCollection();
                }
            }

            public class GetCelularesPorCuentaResult : Common.ServiceResult
            {
                public FullStack.CelularPorCuentaCollection Items { get; set; }

                public GetCelularesPorCuentaResult()
                {
                    Items = new FullStack.CelularPorCuentaCollection();
                }
            }

            public class GetProductosClienteResult : Common.ServiceResult
            {
                public BusinessLogic.InfoCuentaPornitFSCollection Items { get; set; }

                public GetProductosClienteResult()
                {
                    Items = new BusinessLogic.InfoCuentaPornitFSCollection();
                }
            }

            public class GeneraOrdenResult : Common.ServiceResult
            {
                public BusinessLogic.GeneraOrdenOutput Resultado { get; set; }

                public GeneraOrdenResult()
                {
                    Resultado = new BusinessLogic.GeneraOrdenOutput();
                }
            }

            public class GetPagoResult : Common.ServiceResult
            {
                public FullStack.Pagos.OutputPago Resultado { get; set; }

                public GetPagoResult()
                {
                    Resultado = new FullStack.Pagos.OutputPago();
                }
            }

            public class VerifyPagoResult : Common.ServiceResult
            {
                public FullStack.Pagos.OutputVerifyPago Resultado { get; set; }

                public VerifyPagoResult()
                {
                    Resultado = new FullStack.Pagos.OutputVerifyPago();
                }
            }

            public class GetPagoHistoricoResult : Common.ServiceResult
            {
                public FullStack.Pagos.HistoricoElementCollection Items { get; set; }

                public GetPagoHistoricoResult()
                {
                    Items = new FullStack.Pagos.HistoricoElementCollection();
                }
            }

            #region CU-012
            public class GetCategoriaSVAResult : Common.ServiceResult
            {
                public FullStack.CategoriaSVA Resultado { get; set; }

                public GetCategoriaSVAResult()
                {
                    Resultado = new FullStack.CategoriaSVA();
                }
            }

            public class GetCategoriaSVACollectionResult : Common.ServiceResult
            {
                public FullStack.CategoriaSVACollection Items { get; set; }

                public GetCategoriaSVACollectionResult()
                {
                    Items = new FullStack.CategoriaSVACollection();
                }
            }

            public class GetOfertaSVAResult : Common.ServiceResult
            {
                public FullStack.OfertaSVA Resultado { get; set; }

                public GetOfertaSVAResult()
                {
                    Resultado = new FullStack.OfertaSVA();
                }
            }

            public class GetOfertaSVACollectionResult : Common.ServiceResult
            {
                public FullStack.OfertaSVACollection Items { get; set; }

                public GetOfertaSVACollectionResult()
                {
                    Items = new FullStack.OfertaSVACollection();
                }
            }

            public class GetContratacionSVAResult : Common.ServiceResult
            {
                public FullStack.ContratacionSVA Resultado { get; set; }

                public GetContratacionSVAResult()
                {
                    Resultado = new FullStack.ContratacionSVA();
                }
            }
            #endregion

            #endregion
        }
    }
}