using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.entities
{
    public class persona
    {
        private string numdoc = null;
        public string NumDoc
        {
            get { return numdoc; }
            set { numdoc = value; }
        }

        private string tipdoc = null;
        public string TipDoc
        {
            get { return tipdoc; }
            set { tipdoc = value; }
        }

        private string fecha_exp_doc = null;
        public string Fecha_Exp_Doc
        {
            get { return fecha_exp_doc; }
            set { fecha_exp_doc = value; }
        }

        private string nombres = null;
        public string Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }

        private string apellidos = null;
        public string Apellidos
        {
            get { return apellidos; }
            set { apellidos = value; }
        }
    }
}