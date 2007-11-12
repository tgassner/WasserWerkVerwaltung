using System;
using System.Collections.Generic;
using System.Text;
using WasserWerkVerwaltung.DAL;
using WasserWerkVerwaltung.CommonObjects;

namespace WasserWerkVerwaltung.BL {
    public class WWVBusinessObject : IWWVBL{

        private IList<KundenData> allKunden = null;

        internal WWVBusinessObject() {
        }

        public IList<KundenData> GetAllKunden() {
            if (allKunden == null) {
                IKunde kundenDB = Database.CreateKunde();
                allKunden = kundenDB.FindAll();
            }
            return allKunden;
        }

    }
}
