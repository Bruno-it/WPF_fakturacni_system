using Microsoft.Data.Sqlite;
using System;

namespace FakturaApp
{
    public static class Database
    {
        private const string connectionString = "Data Source=Faktury.db";

        // Vytvoří tabulku, pokud ještě neexistuje
        public static void Init()
        {
            using var con = new SqliteConnection(connectionString);
            con.Open();

            string sql = @"CREATE TABLE IF NOT EXISTS faktury (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            cislo_faktury TEXT,
                            odberatel TEXT,
                            adresa TEXT,
                            ico TEXT,
                            sluzba TEXT,
                            hodinova_sazba REAL,
                            pocet_hodin INTEGER,
                            castka REAL,
                            datum_vystaveni TEXT,
                            datum_splatnosti TEXT
                          );";

            using var cmd = new SqliteCommand(sql, con);
            cmd.ExecuteNonQuery();
        }

        // Spočítá počet faktur
        public static int ZiskejPocetFaktur()
        {
            using var con = new SqliteConnection(connectionString);
            con.Open();

            string sql = "SELECT COUNT(*) FROM faktury";
            using var cmd = new SqliteCommand(sql, con);
            long pocet = (long)cmd.ExecuteScalar();
            return (int)pocet;
        }

        // Uloží novou fakturu do databáze
        public static void UlozFakturu(Faktura f)
        {
            using var con = new SqliteConnection(connectionString);
            con.Open();

            string sql = @"INSERT INTO faktury 
                           (cislo_faktury, odberatel, adresa, ico, sluzba, hodinova_sazba, pocet_hodin, castka, datum_vystaveni, datum_splatnosti)
                           VALUES (@cislo, @odberatel, @adresa, @ico, @sluzba, @sazba, @hodiny, @castka, @vystaveni, @splatnost)";

            using var cmd = new SqliteCommand(sql, con);
            cmd.Parameters.AddWithValue("@cislo", f.CisloFaktury);
            cmd.Parameters.AddWithValue("@odberatel", f.Odberatel);
            cmd.Parameters.AddWithValue("@adresa", f.Adresa);
            cmd.Parameters.AddWithValue("@ico", f.ICO);
            cmd.Parameters.AddWithValue("@sluzba", f.Sluzba);
            cmd.Parameters.AddWithValue("@sazba", f.HodinovaSazba);
            cmd.Parameters.AddWithValue("@hodiny", f.PocetHodin);
            cmd.Parameters.AddWithValue("@castka", f.Castka);
            cmd.Parameters.AddWithValue("@vystaveni", f.DatumVystaveni);
            cmd.Parameters.AddWithValue("@splatnost", f.DatumSplatnosti);

            cmd.ExecuteNonQuery();
        }
    }
}
