using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.entities
{
    public class sorteos
    {
        private const int _IdSorteo = 0;
        private int idSorteo = 0;
        public int IdSorteo
        {
            get  { return idSorteo; }
            set  { idSorteo = value; }
        }
        private DateTime fechaSorteo;
        public DateTime FechaSorteo
        {
            get { return fechaSorteo; }
            set { fechaSorteo = value; }
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
        private int sb = 0;
        public int Sb
        {
            get { return sb; }
            set { sb = value; }
        }
        private DateTime fecha;
        public DateTime  Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        private string winner = string.Empty;
        public string Winner
        {
            get { return winner; }
            set { winner = value; }
        }
        private string tipo = string.Empty;
        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        private string nuevo = string.Empty;
        public string Nuevo
        {
            get { return nuevo; }
            set { nuevo = value; }
        }
        private int enUso = 0;
        public int EnUso
        {
            get { return enUso; }
            set { enUso = value; }
        }

        private List<string> indi = null;
        public List<string> Indi
        {
            get { return indi; }
            set { indi = value; }
        }

        private List<string> dupl = null;
        public List<string> Dupl
        {
            get { return dupl; }
            set { dupl = value; }
        }
        private List<string> terc = null;
        public List<string> Terc
        {
            get { return terc; }
            set { terc = CreaListaTernas(); }
        }
        private List<string> cuar = null;
        public List<string> Cuar
        {
            get { return cuar; }
            set { cuar = CreaListaCuartetos(); }
        }
        private List<string> quin = null;
        public List<string> Quin
        {
            get { return quin; }
            set { quin = CreaListaQuintetos(); }
        }
        private List<string> sext = null;
        public List<string> Sext
        {
            get { return sext; }
            set { sext = CreaListaSextetos(); }
        }

        public class Si : sorteos
        {
            private int idLastSorteo = 0;
            public int IdLastSorteo
            {
                get { return idLastSorteo; }
                set { idLastSorteo = value; }
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
        }

        public class Du : sorteos
        {
            private int idLastSorteo = 0;
            public int IdLastSorteo
            {
                get { return idLastSorteo; }
                set { idLastSorteo = value; }
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
        }

        public class Te:sorteos
        {
            private int idLastSorteo = 0;
            public int IdLastSorteo
            {
                get { return idLastSorteo; }
                set { idLastSorteo = value; }
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
        }

        public class Cu : sorteos
        {
            private int idLastSorteo = 0;
            public int IdLastSorteo
            {
                get { return idLastSorteo; }
                set { idLastSorteo = value; }
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
        }

        public class Qu:sorteos
        {
            private int idLastSorteo = 0;
            public int IdLastSorteo
            {
                get { return idLastSorteo; }
                set { idLastSorteo = value; }
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
        }


        public class Se : sorteos
        {
            private int idLastSorteo = 0;
            public int IdLastSorteo
            {
                get { return idLastSorteo; }
                set { idLastSorteo = value; }
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
            private int behaviour = 0;
            public int Behaviour
            {
                get { return behaviour; }
                set { behaviour = value; }
            }



        }

        public sorteos()
        {
                var i = new sorteos.Si();
                var d = new sorteos.Du();
                var t = new sorteos.Te();
                var c = new sorteos.Cu();
                var q = new sorteos.Qu();
                var se = new sorteos.Se();
          
            


                this.Indi.Add(this.Num1.ToString());
                this.Indi.Add(this.Num2.ToString());
                this.Indi.Add(this.Num3.ToString());
                this.Indi.Add(this.Num4.ToString());
                this.Indi.Add(this.Num5.ToString());
                this.Indi.Add(this.Sb.ToString());

                this.Dupl.Add(this.Num1.ToString() + '|' + this.Num2.ToString());
                this.Dupl.Add(this.Num1.ToString() + '|' + this.Num3.ToString());
                this.Dupl.Add(this.Num1.ToString() + '|' + this.Num4.ToString());
                this.Dupl.Add(this.Num1.ToString() + '|' + this.Num5.ToString());
                this.Dupl.Add(this.Num1.ToString() + '|' + this.Sb.ToString());
                this.Dupl.Add(this.Num2.ToString() + '|' + this.Num3.ToString());
                this.Dupl.Add(this.Num2.ToString() + '|' + this.Num4.ToString());
                this.Dupl.Add(this.Num2.ToString() + '|' + this.Num5.ToString());
                this.Dupl.Add(this.Num2.ToString() + '|' + this.Sb.ToString());
                this.Dupl.Add(this.Num3.ToString() + '|' + this.Num4.ToString());
                this.Dupl.Add(this.Num3.ToString() + '|' + this.Num5.ToString());
                this.Dupl.Add(this.Num3.ToString() + '|' + this.Sb.ToString());
                this.Dupl.Add(this.Num4.ToString() + '|' + this.Num5.ToString());
                this.Dupl.Add(this.Num4.ToString() + '|' + this.Sb.ToString());
                this.Dupl.Add(this.Num5.ToString() + '|' + this.Sb.ToString());



                this.Terc.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num3.ToString());
                this.Terc.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num4.ToString());
                this.Terc.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num5.ToString());
                this.Terc.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Sb.ToString());
                this.Terc.Add(this.Num1.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString());
                this.Terc.Add(this.Num1.ToString() + '|' + this.Num3.ToString() + '|' + this.Num5.ToString());
                this.Terc.Add(this.Num1.ToString() + '|' + this.Num3.ToString() + '|' + this.Sb.ToString());
                this.Terc.Add(this.Num1.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString());
                this.Terc.Add(this.Num1.ToString() + '|' + this.Num4.ToString() + '|' + this.Sb.ToString());
                this.Terc.Add(this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString());
                this.Terc.Add(this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num5.ToString());
                this.Terc.Add(this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Sb.ToString());
                this.Terc.Add(this.Num2.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString());
                this.Terc.Add(this.Num2.ToString() + '|' + this.Num4.ToString() + '|' + this.Sb.ToString());
                this.Terc.Add(this.Num2.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());
                this.Terc.Add(this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString());
                this.Terc.Add(this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Sb.ToString());
                this.Terc.Add(this.Num3.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());
                this.Terc.Add(this.Num4.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());


                this.Cuar.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString());
                this.Cuar.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num5.ToString());
                this.Cuar.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Sb.ToString());
                this.Cuar.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString());
                this.Cuar.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num4.ToString() + '|' + this.Sb.ToString());
                this.Cuar.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());
                this.Cuar.Add(this.Num1.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString());
                this.Cuar.Add(this.Num1.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Sb.ToString());
                this.Cuar.Add(this.Num1.ToString() + '|' + this.Num3.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());
                this.Cuar.Add(this.Num1.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());
                this.Cuar.Add(this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString());
                this.Cuar.Add(this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Sb.ToString());
                this.Cuar.Add(this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());
                this.Cuar.Add(this.Num2.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());
                this.Cuar.Add(this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());

                this.Quin.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString());
                this.Quin.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Sb.ToString());
                this.Quin.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());
                this.Quin.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());
                this.Quin.Add(this.Num1.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());
                this.Quin.Add(this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());


                this.Sext.Add(this.Num1.ToString() + '|' + this.Num2.ToString() + '|' + this.Num3.ToString() + '|' + this.Num4.ToString() + '|' + this.Num5.ToString() + '|' + this.Sb.ToString());


                
          
        }

        List<string> CreaListaIndis()
        {
            List<string> lI = null;
            lI.Add(num1.ToString());
            lI.Add(num2.ToString());
            lI.Add(num3.ToString());
            lI.Add(num4.ToString());
            lI.Add(num5.ToString());
            lI.Add(sb.ToString());
            return lI;
        }

        List<string> CreaListaDuplas()
        {
            List<string> lD = null;
            lD.Add(num1.ToString() + num2.ToString());
            lD.Add(num1.ToString() + num3.ToString());
            lD.Add(num1.ToString() + num4.ToString());
            lD.Add(num1.ToString() + num5.ToString());
            lD.Add(num1.ToString() + sb.ToString());
            lD.Add(num2.ToString() + num3.ToString());
            lD.Add(num2.ToString() + num4.ToString());
            lD.Add(num2.ToString() + num5.ToString());
            lD.Add(num2.ToString() + sb.ToString());
            lD.Add(num3.ToString() + num4.ToString());
            lD.Add(num3.ToString() + num5.ToString());
            lD.Add(num3.ToString() + sb.ToString());
            lD.Add(num4.ToString() + num5.ToString());
            lD.Add(num4.ToString() + sb.ToString());
            lD.Add(num5.ToString() + sb.ToString());
            return lD;
        }

        List<string> CreaListaTernas()
        {
            List<string> lT = null;
            lT.Add(num1.ToString() + num2.ToString() + num3.ToString());
            lT.Add(num1.ToString() + num2.ToString() + num4.ToString());
            lT.Add(num1.ToString() + num2.ToString() + num5.ToString());
            lT.Add(num1.ToString() + num2.ToString() + sb.ToString());
            lT.Add(num1.ToString() + num3.ToString() + num4.ToString());
            lT.Add(num1.ToString() + num3.ToString() + num5.ToString());
            lT.Add(num1.ToString() + num3.ToString() + sb.ToString());
            lT.Add(num1.ToString() + num4.ToString() + num5.ToString());
            lT.Add(num1.ToString() + num4.ToString() + sb.ToString());
            lT.Add(num2.ToString() + num3.ToString() + num4.ToString());
            lT.Add(num2.ToString() + num3.ToString() + num5.ToString());
            lT.Add(num2.ToString() + num3.ToString() + sb.ToString());
            lT.Add(num3.ToString() + num4.ToString() + num5.ToString());
            lT.Add(num3.ToString() + num4.ToString() + sb.ToString());
            lT.Add(num4.ToString() + num5.ToString() + sb.ToString());
            return lT;
        }

        List<string> CreaListaCuartetos()
        {
            List<string> lC = null;
            lC.Add(num1.ToString() + num2.ToString() + num3.ToString() + num4.ToString());
            lC.Add(num1.ToString() + num2.ToString() + num3.ToString() + num5.ToString());
            lC.Add(num1.ToString() + num2.ToString() + num3.ToString() + sb.ToString());
            lC.Add(num1.ToString() + num2.ToString() + num4.ToString() + num5.ToString());
            lC.Add(num1.ToString() + num2.ToString() + num4.ToString() + sb.ToString());
            lC.Add(num1.ToString() + num2.ToString() + num5.ToString() + sb.ToString());
            lC.Add(num1.ToString() + num3.ToString() + num4.ToString() + num5.ToString());
            lC.Add(num1.ToString() + num3.ToString() + num4.ToString() + sb.ToString());
            lC.Add(num1.ToString() + num3.ToString() + num5.ToString() + sb.ToString());
            lC.Add(num1.ToString() + num4.ToString() + num5.ToString() + sb.ToString());
            lC.Add(num2.ToString() + num3.ToString() + num4.ToString() + num5.ToString());
            lC.Add(num2.ToString() + num3.ToString() + num4.ToString() + sb.ToString());
            lC.Add(num2.ToString() + num4.ToString() + num5.ToString() + sb.ToString());
            lC.Add(num3.ToString() + num4.ToString() + num5.ToString() + sb.ToString());
            return lC;
        }

        List<string> CreaListaQuintetos()
        {
            List<string> lQ = null;
            lQ.Add(num1.ToString() + num2.ToString() + num3.ToString() + num4.ToString() + num5.ToString());
            lQ.Add(num1.ToString() + num2.ToString() + num3.ToString() + num4.ToString() + sb.ToString());
            lQ.Add(num1.ToString() + num2.ToString() + num3.ToString() + num5.ToString() + sb.ToString());
            lQ.Add(num1.ToString() + num2.ToString() + num4.ToString() + num5.ToString() + sb.ToString());
            lQ.Add(num1.ToString() + num3.ToString() + num4.ToString() + num5.ToString() + sb.ToString());
            lQ.Add(num2.ToString() + num3.ToString() + num4.ToString() + num5.ToString() + sb.ToString());
            return lQ;
        }

        List<string> CreaListaSextetos()
        {
            List<string> lS = null;
            lS.Add(num1.ToString() + num2.ToString() + num3.ToString() + num4.ToString() + num5.ToString() + sb.ToString());
            return lS;
        }
    }
}