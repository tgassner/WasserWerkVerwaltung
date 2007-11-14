using System;
using System.Collections.Generic;
using System.Text;
using WasserWerkVerwaltung.CommonObjects;

namespace WasserWerkVerwaltung.BL {
    public interface IWWVBL {

        #region Kunde
            IList<KundenData> GetAllKunden();
            bool UpdateKunde(KundenData kunde);
            KundenData InsertKunde(KundenData kunde);
        #endregion Kunde

        #region JahresDaten
            IList<JahresDatenData> GetJahresdataByKundenID(long kundenID);
        bool UpdateJahresDaten(JahresDatenData jahresDatum);
            JahresDatenData InsertJahresDaten(JahresDatenData jahresDatum);
        #endregion JahresDaten

        #region Preis
            PreisData GetPreisDataByJahr(long jahr);
            PreisData InsertPreis(PreisData preis);
            bool UpdatePreis(PreisData jahresDatum);
        #endregion Preis
    }
}
