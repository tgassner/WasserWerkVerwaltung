using System;
using System.Collections.Generic;
using System.Text;
using WasserWerkVerwaltung.CommonObjects;

namespace WasserWerkVerwaltung.BL {
    public interface IWWVBL {
        IList<KundenData> GetAllKunden();
    }
}
