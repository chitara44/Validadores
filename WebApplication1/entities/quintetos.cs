using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.entities
{
    public class quintetos
    {
        private int idSorteo = 0;
        public int IdSorteo
        {
            get { return idSorteo; }
            set { idSorteo = value; }
        }
        private int idLastSorteo = 0;
        public int IdLastSorteo
        {
            get { return idLastSorteo; }
            set { idLastSorteo = value; }
        }
        private int num1 = 0;
        public int Num1
        {
            get { return num1; }
            set { num1 = value; }
        }
        private int num2 = 0;
        public int Num2
        {
            get { return num2; }
            set { num2 = value; }
        }
        private int num3 = 0;
        public int Num3
        {
            get { return num3; }
            set { num3 = value; }
        }
        private int num4 = 0;
        public int Num4
        {
            get { return num4; }
            set { num4 = value; }
        }
        private int num5 = 0;
        public int Num5
        {
            get { return num5; }
            set { num5 = value; }
        }
        private int cantSinCaer = 0;
        public int CantSinCaer
        {
            get { return cantSinCaer; }
            set { cantSinCaer = value; }
        }
        private int cantCaidos = 0;
        public int CantCaidos
        {
            get { return cantCaidos; }
            set { cantCaidos = value; }
        }
        private string tipo = string.Empty;
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        private string winner = string.Empty;
        public string Winner
        {
            get { return winner; }
            set { winner = value; }
        }
        private string nuevo = string.Empty;
        public string Nuevo
        {
            get { return nuevo; }
            set { nuevo = value; }
        }
    }
}