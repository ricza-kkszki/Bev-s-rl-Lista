using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace bevasarlolista
{
    /// <summary>
    /// Interaction logic for Hozzaadas.xaml
    /// </summary>
    public partial class Hozzaadas : Window
    {
        public ItemModel ujtermek;
        public Hozzaadas()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            int mennyiseg_ = 0;
            int ar_ = 0;
            if(nev.Text=="" || !int.TryParse(mennyiseg.Text,out mennyiseg_) | !int.TryParse(mennyiseg.Text, out ar_) || kategoria.SelectedItem == null)
            {
                MessageBox.Show("Nem megfelelő adatok a beviteli mezőkben!","Hiba",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            {
                ujtermek = new ItemModel(nev.Text, int.Parse(mennyiseg.Text), int.Parse(ar.Text), kategoria.Text);
                MessageBox.Show("Termék sikeresen hozzáadva!", "Infó", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
        }
    }
}
