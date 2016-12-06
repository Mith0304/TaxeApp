using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxeApp.Model
{
    public class Compute
    {
        public static double GetTax(double priceHT, bool inCategory, bool imported)
        {
            /*
            Une taxe sur la valeur ajoutée de 10 % est appliquée sur chaque produit, à l'exception des livres, de
            la nourriture et des médicaments, qui en sont exemptés. Une taxe additionnelle de 5 % sur les 
            produits importés, sans exception.

            Le montant de chacune des taxes est arrondi aux 5 cents supérieurs.
                
            Pttc = Pht + somme(arrondi(Pht * t / 100))
            */

            double tax = inCategory ? 0 : ((double)(long)(0.1 * priceHT * 20 + 0.5)) / 20;
            tax = imported ? tax + ((double)(long)(0.05 * priceHT * 20 + 0.5)) / 20 : tax;

            return tax;
        }
    }
}
