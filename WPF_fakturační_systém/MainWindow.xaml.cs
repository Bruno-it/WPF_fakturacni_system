using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FakturaApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Database.Init();
        }

        private void btnVystavit_Click(object sender, RoutedEventArgs e)
        {
           
         // Získání nového čísla faktury
         int pocet = Database.ZiskejPocetFaktur() + 1;
         string cisloFaktury = $"F-{pocet:D3}";

         // Vytvoření nové faktury
         var faktura = new Faktura
         {
           CisloFaktury = cisloFaktury,
           Odberatel = txtOdberatel.Text,
           Adresa = txtAdresa.Text,
           ICO = txtICO.Text,
           Sluzba = (cmbSluzba.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "",
           HodinovaSazba = double.Parse(txtSazba.Text),
           PocetHodin = int.Parse(txtHodiny.Text)
         };

         // Uložení do databáze
         Database.UlozFakturu(faktura);

         // Uložení faktury do stejné složky jako aplikace
         string fileName = $"{faktura.CisloFaktury}.txt";

         using (StreamWriter sw = new StreamWriter(fileName))
         {
           sw.WriteLine($"FAKTURA č. {faktura.CisloFaktury}");
           sw.WriteLine($"Datum vystavení: {faktura.DatumVystaveni}");
           sw.WriteLine($"Datum splatnosti: {faktura.DatumSplatnosti}");
           sw.WriteLine();
           sw.WriteLine($"Odběratel: {faktura.Odberatel}");
           sw.WriteLine($"Adresa: {faktura.Adresa}");
           sw.WriteLine($"IČO: {faktura.ICO}");
           sw.WriteLine();
           sw.WriteLine($"Služba: {faktura.Sluzba}");
           sw.WriteLine($"Počet hodin: {faktura.PocetHodin}");
           sw.WriteLine($"Hodinová sazba: {faktura.HodinovaSazba} Kč");
           sw.WriteLine($"Celkem: {faktura.Castka} Kč");
         }
           MessageBox.Show($"Faktura {faktura.CisloFaktury} byla uložena do složky.","Hotovo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
