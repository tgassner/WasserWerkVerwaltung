using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Data;

namespace WasserWerkVerwaltung.DAL {
    public class Database {
        public static IKunde CreateKunde() {
            return new WasserWerkVerwaltung.DAL.Kunde();
        }

        public static IJahresDaten CreateJahresDaten() {
            return new WasserWerkVerwaltung.DAL.JahresDaten();
        }

        public static IPreis CreatePreis() {
            return new WasserWerkVerwaltung.DAL.Preis();
        }

    }
}
