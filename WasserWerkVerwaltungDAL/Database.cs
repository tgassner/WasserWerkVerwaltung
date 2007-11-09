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

    
  }
}
