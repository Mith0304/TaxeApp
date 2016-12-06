using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxeApp
{
    class Article
    {
        public string Nom { get; set; }
        public string Categorie { get; set; }
        public double PrixHT { get; set; }
        public double PrixTTC { get; set; }

        public override string ToString()
        {
            return this.Nom;
        }
    }
}
