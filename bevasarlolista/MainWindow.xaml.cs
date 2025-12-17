using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bevasarlolista
{
    public class ItemModel
    {
        public ItemModel(string nev, int mennyiseg, int ar, string kategoria)
        {
            Nev = nev;
            Mennyiseg = mennyiseg;
            Ar = ar;
            Kategoria = kategoria;
            Osszesen = Mennyiseg * Ar;
        }

        public string Nev { get; set; }
        public int Mennyiseg { get; set; }
        public int Ar { get; set; }
        public string Kategoria { get; set; }
        public int Osszesen { get; set; }
    }

    public partial class MainWindow : Window
    {
        List<ItemModel> termekek = new List<ItemModel>();

        public MainWindow()
        {
            InitializeComponent();

            termekek.Add(new ItemModel("Tej", 5, 450, "A"));
            termekek.Add(new ItemModel("Kenyer", 10, 350, "B"));
            termekek.Add(new ItemModel("Sajt", 2, 1200, "A"));
            termekek.Add(new ItemModel("Alma", 20, 200, "C"));
            termekek.Add(new ItemModel("Narancs", 15, 300, "C"));
            termekek.Add(new ItemModel("Hús", 3, 2500, "D"));
            termekek.Add(new ItemModel("Csokoládé", 7, 900, "B"));
            termekek.Add(new ItemModel("Kenyér", 1, 450, "B"));
            termekek.Add(new ItemModel("Tej", 12, 400, "A"));
            termekek.Add(new ItemModel("Sajt", 5, 1500, "D"));

            dataGrid.ItemsSource = termekek;
        }

        private void AddBtn(object sender, RoutedEventArgs e)
        {
            var ujtermek = new Hozzaadas();
            if (ujtermek.ShowDialog() == true)
            {
                termekek.Add(ujtermek.ujtermek);
                dataGrid.ItemsSource = termekek;
                dataGrid.Items.Refresh();
            }
        }

        private void RemoveBtn(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                termekek.Remove((ItemModel)dataGrid.SelectedItem);
                dataGrid.Items.Refresh();
            }
        }

        private void Top3ABtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Kategoria == "A").OrderByDescending(x=>x.Ar).Take(3);
        }
    }
}