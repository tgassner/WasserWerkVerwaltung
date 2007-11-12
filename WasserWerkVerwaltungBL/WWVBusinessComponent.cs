using System;
using System.Collections.Generic;
using System.Text;
using WasserWerkVerwaltung.DAL;

namespace WasserWerkVerwaltung.BL {
    public class WWVBusinessComponent {

        public void Trallala() {
            IKunde kundeDB = Database.CreateKunde();
            kundeDB.FindAll();
        }
    }
}
