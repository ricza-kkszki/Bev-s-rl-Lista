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
            termekek.Add(new ItemModel("Kenyer2", 1, 450, "B"));
            termekek.Add(new ItemModel("Tej", 12, 400, "A"));
            termekek.Add(new ItemModel("Sajt", 5, 1500, "D"));
            priceProgressBar.Minimum = termekek.Min(t => t.Ar);
            priceProgressBar.Maximum = termekek.Max(t => t.Ar);

        }

        private void LoadBtn(object sender, RoutedEventArgs e)
        {
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
                priceProgressBar.Minimum = termekek.Min(t => t.Ar);
                priceProgressBar.Maximum = termekek.Max(t => t.Ar);
            }
        }

        private void RemoveBtn(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is ItemModel)
            {
                termekek.Remove((ItemModel)dataGrid.SelectedItem);
                dataGrid.Items.Refresh();
            }
        }

        private void Top3ABtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Kategoria == "A").OrderByDescending(x=>x.Ar).Take(3);
        }

        private void Top5TotalBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderByDescending(x => x.Osszesen).Take(5);
        }

        private void MoreThan1Btn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Mennyiseg > 1).Select(k => new { Nev = k.Nev, Osszesen = k.Osszesen });
        }

        private void DescendingPriceBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderByDescending(t => t.Ar);
        }

        private void dOver500Btn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Kategoria == "D" && t.Ar > 500);
        }

        private void nameAndTotalBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderBy(x => x.Nev).Select(g => new { Nev = g.Nev, Osszesen = g.Osszesen });
        }

        private void typeQuantityAndTotalBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderBy(x => x.Nev).GroupBy(g => g.Kategoria).Select(g => new { Tipus = g.Key, Darab = g.Sum(t => t.Mennyiseg), Osszesen = g.Sum(t => t.Osszesen) });
        }

        private void typeAvgBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(t => t.Kategoria).Select(g => new
            {
                Kategoria = g.Key,
                Atlagar = Math.Round(g.Average(t => t.Ar),2)
            });
        }

        private void highestTotalPerCategoryBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(t => t.Kategoria).Select(g => new
            {
                Kategoria = g.Key,
                Osszertek = g.Max(x => x.Osszesen)
            });
        }

        private void bcUnder1000Btn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => (t.Kategoria == "B" || t.Kategoria == "C") && t.Ar < 1000);
        }

        private void above500Btn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Ar > 500).GroupBy(x => x.Kategoria).Select(y => new
            {
                Kategoria = y.Key,
                TermekekSzama = y.Count()
            });
        }

        private void moreThan10CheaperThan1000Btn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Mennyiseg > 10 && t.Ar < 1000).OrderBy(t=>t.Ar);
        }

        private void totalAbove2000abcBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Osszesen > 2000).OrderBy(t => t.Nev);
        }

        private void groupByNameAndTypeBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(t => new
            {
                Nev = t.Nev,
                Kategoria = t.Kategoria
            }).Select(x => new
            {
                TermekNev = x.Key.Nev,
                Kategoria = x.Key.Kategoria,
                Darab = x.Count()
            });
        }

        private void mostExpensivePerTypeBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(item => item.Kategoria).Select(item => new
            {
                Kategoria = item.Key,
                Ar = item.Max(x => x.Ar)
            });
        }

        private void totalCountPerTypeBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(f => f.Kategoria).Select(g => new
            {
                Kategoria = g.Key,
                Darab = g.Sum(h => h.Mennyiseg)
            }); 
        }

        private void zeroFtBtn(object sender, RoutedEventArgs e)
        {
            var nullaFtosTermek = termekek.Any(x => x.Ar == 0);

            if (nullaFtosTermek == false)
            {
                MessageBox.Show("Nincs nulla ft-os termék");
            }
            else
            {
                MessageBox.Show("Van nulla ft-os termék");
            }
        }

        private void onlyBreadBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(x => x.Nev.ToLower().Contains("kenyer")).OrderByDescending(x=>x.Ar);
        }

        private void matchingPriceBtn(object sender, RoutedEventArgs e)
        {
            var egyformak = termekek
                .GroupBy(x => x.Ar)
                .Select(g => new
                {
                    Darab = g.Count()
                })
                .Where(z => z.Darab > 1);

            dataGrid.ItemsSource = egyformak;

            if (egyformak.Any())
            {
                MessageBox.Show("Van eggyező");
            }
            else
            {
                MessageBox.Show("Nincs eggyező");
            }
        }
        private void Valtozas(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Nev.ToLower().Contains(textBox.Text.ToLower())).ToList();
        }

        private void matchingProductBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.GroupBy(t => t.Nev).Where(t => t.Count() > 1).SelectMany(g => g);
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem is ItemModel selectedItem)
            {
                priceProgressBar.Value = selectedItem.Ar;
            }
        }

        private void notCBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.Where(t => t.Kategoria != "C");
        }

        private void nameLengthBtn(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = termekek.OrderBy(t => t.Nev);
        }
    }
}