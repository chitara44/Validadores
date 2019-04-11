using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Utilities;

namespace WebApplication1.entities
{
    public class parametros
    {
        private string canal = null;
        public string Canal
        {
            get { return canal; }
            set { canal = value; }
        }

        private string nom_parametro = null;
        public string Nom_Parametro
        {
            get { return nom_parametro; }
            set { nom_parametro = value; }
        }

        private string val_parametro = null;
        public string Val_Parametro
        {
            get { return val_parametro; }
            set { val_parametro = value; }
        }

        private string des_parametro = null;
        public string Des_Parametro
        {
            get { return des_parametro; }
            set { des_parametro = value; }
        }


        public List<string> CreaListaParametros()
        {
            List<string> lP = new List<string>();
            lP.Add(canal.ToString() + '|' + nom_parametro.ToString() + '|' + val_parametro.ToString() + '|' + des_parametro.ToString() );
            return lP;
        }
    }

    
}