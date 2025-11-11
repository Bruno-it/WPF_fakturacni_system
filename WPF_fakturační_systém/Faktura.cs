using System;

namespace FakturaApp
{
    public class Faktura
    {
        public string CisloFaktury { get; set; }
        public string Odberatel { get; set; }
        public string Adresa { get; set; }
        public string ICO { get; set; }
        public string Sluzba { get; set; }
        public double HodinovaSazba { get; set; }
        public int PocetHodin { get; set; }
        public double Castka => HodinovaSazba * PocetHodin;
        public string DatumVystaveni { get; set; } = DateTime.Now.ToShortDateString();
        public string DatumSplatnosti { get; set; } = DateTime.Now.AddDays(14).ToShortDateString();
    }
}
