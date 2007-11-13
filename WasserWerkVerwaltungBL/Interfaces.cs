using System;
using System.Collections.Generic;
using System.Text;
using WasserWerkVerwaltung.CommonObjects;

namespace WasserWerkVerwaltung.BL {
    public interface IWWVBL {
        IList<KundenData> GetAllKunden();
        bool UpdateKunde(KundenData kunde);
        KundenData InsertKunde(KundenData kunde);
        IList<JahresDatenData> GetJahresdataByKundenID(long kundenID);
    }
}
