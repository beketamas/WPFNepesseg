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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace WPFNepesseg
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			List<Telepules> telepulesek;

			InitializeComponent();
			telepulesek = AdatokBetoltese("Adatok\\kozerdeku_lakossag_2022.csv");
			dgTelepulesek.ItemsSource = telepulesek;

			cbMegyek.ItemsSource = telepulesek.Select(x => x.Megye).Distinct().ToList();
		}

		private List<Telepules> AdatokBetoltese(string fajlNev)
		{
			//throw new NotImplementedException();
			List<Telepules> ujLista = new List<Telepules>();
			string[] tomb = File.ReadAllLines(fajlNev);

			foreach (var item in tomb.Skip(1))
			{
				string[] splitTomb = item.Split(";");
				Telepules uj_telepules = new Telepules(splitTomb[2], splitTomb[3], splitTomb[4], int.Parse(splitTomb[5].Replace(" ", "")),
					int.Parse(splitTomb[6].Replace(" ", "")));
				ujLista.Add(uj_telepules);
			}
			return ujLista;
		}

		private void cbMegyek_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			if (cbMegyek.SelectedItem != null)
			{
				string kivalasztottMegye = cbMegyek.SelectedItem.ToString();
				List<Telepules> telepulesek;
				telepulesek = AdatokBetoltese("Adatok\\kozerdeku_lakossag_2022.csv");

				var valam = telepulesek.Where(x => x.Megye == kivalasztottMegye);
				dgTelepulesek.ItemsSource = valam;
			}
		}

		private void btnReset_Click(object sender, RoutedEventArgs e)
		{
			List<Telepules> telepulesek;
			telepulesek = AdatokBetoltese("Adatok\\kozerdeku_lakossag_2022.csv");
			dgTelepulesek.ItemsSource = telepulesek;
			cbMegyek.Text = string.Empty;
		}

		private void btnNovekvoFerfi_Click(object sender, RoutedEventArgs e)
		{
			List<Telepules> telepulesek;
			telepulesek = AdatokBetoltese("Adatok\\kozerdeku_lakossag_2022.csv");

			if (cbMegyek.SelectedItem != null)
			{
				string kivalasztottMegye = cbMegyek.SelectedItem.ToString();
				var NovekvoRendezettLista = telepulesek.Where(x => x.Megye == kivalasztottMegye);
				var x = NovekvoRendezettLista.OrderByDescending(x => x.FerfiLakosokSzama);
				dgTelepulesek.ItemsSource = x;
			}

			else
			{
				var rendezettLista = telepulesek.OrderByDescending(x => x.FerfiLakosokSzama);

				dgTelepulesek.ItemsSource = rendezettLista;
			}


		}

		private void btnNovekvoNoi_Click(object sender, RoutedEventArgs e)
		{
			List<Telepules> telepulesek;
			telepulesek = AdatokBetoltese("Adatok\\kozerdeku_lakossag_2022.csv");

			if (cbMegyek.SelectedItem != null)
			{
				string kivalasztottMegye = cbMegyek.SelectedItem.ToString();
				var valam = telepulesek.Where(x => x.Megye == kivalasztottMegye);
				var x = valam.OrderByDescending(x => x.NoiLakosokSzama);
				dgTelepulesek.ItemsSource = x;
			}

			else
			{
				var rendezettLista = telepulesek.OrderByDescending(x => x.NoiLakosokSzama);

				dgTelepulesek.ItemsSource = rendezettLista;
			}

		}
	}
}