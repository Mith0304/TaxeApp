using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaxeApp.Model;

namespace TaxeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<string> categories = new List<string>();
            categories.Add("Nourriture");
            categories.Add("Livres");
            categories.Add("Médicaments");
            categories.Add("Autres");
            categoryCmb.ItemsSource = categories;
        }

        static ObservableCollection<Article> cart = new ObservableCollection<Article>();

        private void addCartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Check())
            {
                return;
            }

            //Parse inputs
            string cat = categoryCmb.Text;
            double priceHT = Convert.ToDouble(priceHTTB.Text, System.Globalization.CultureInfo.InvariantCulture);
            string name = articleNameTB.Text;
            bool inCategory = cat == "Autres" ? false : true;

            //Compute TTC price
            double tax = Compute.GetTax(priceHT, inCategory, importRB.IsChecked.Value);
            double priceTTC = priceHT + tax;

            //Add article to the cart
            Article art = new Article
            {
                Nom = name == "" || name == "Saisissez le nom" ? "Article " + (articleListView.Items.Count + 1) : name,
                Categorie = cat,
                PrixHT = priceHT,
                PrixTTC = priceTTC
            };

            cart.Add(art);
            articleListView.ItemsSource = cart;

            //Print taxes and total price
            totalTaxValueLabel.Content = cart.Sum(s => s.PrixTTC - s.PrixHT).ToString("0.00");
            totalValueLabel.Content = cart.Sum(s => s.PrixTTC).ToString("0.00"); 

            Reset();
        }

        private bool Check()
        {
            if (categoryCmb.Text == "--Catégories--")
            {
                MessageBox.Show("Veuillez sélectionner une catégorie pour votre article");
                return false;
            }
            if (priceHTTB.Text == "Saisissez le prix HT")
            {
                MessageBox.Show("Veuillez saisir un prix pour votre article");
                return false;
            }
            return true;
        }

        private void Reset()
        {
            articleNameTB.Text = "Saisissez le nom";
            categoryCmb.SelectedItem = "--Catégories--";
            priceHTTB.Text = "Saisissez le prix HT";
            importRB.IsChecked = false;
        }
    }
}
