using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WebApplication1.entities;

namespace WebApplication1
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ILuckyService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ILuckyService
    {
        [OperationContract]
        bool consultaganador(int idSorteo, string tipo);
        [OperationContract]
        sorteos consultaSorteo(int idSorteo, string tipo);
        [OperationContract]
        bool prInsertaSorteo(int idSorteo, string numsTr, string numsRe, DateTime fecha, bool winnerTr, bool winnerRe);
        [OperationContract]
        List<sorteos.Si> contadorSingular(string tipo, string clase, int n1);
        [OperationContract]
        List<sorteos.Du> contadorDuplas(string tipo, string nuevo, int n1, int n2);
        [OperationContract]
        List<sorteos.Te> contadorTernas(string tipo, string nuevo, int n1, int n2, int n3);
        [OperationContract]
        List<sorteos.Cu> contadorCuartetos(string tipo, string nuevo, int n1, int n2, int n3, int n4);
        [OperationContract]
        List<sorteos.Qu> contadorQuintetos(string tipo, string nuevo, int n1, int n2, int n3, int n4, int n5);
        [OperationContract]
        List<sorteos.Se> contadorSextetos(string tipo, string nuevo, int n1, int n2, int n3, int n4, int n5, int n6);
        [OperationContract]
        bool llenarCombos(int sorteoIni, int sorteoFin);
    }
}
