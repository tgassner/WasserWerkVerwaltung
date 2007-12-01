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
            bool DeleteJahresDaten(long jahresDatumID);
        #endregion JahresDaten

        #region Preis
            IList<PreisData> GetAllPreise();
            PreisData GetPreisDataByJahr(long jahr);
            PreisData InsertPreis(PreisData preis);
            bool UpdatePreis(PreisData jahresDatum);
        #endregion Preis

        #region Print
            void PrintJahresRechnungen(IList<KundenData> kunden, PreisData preis);
        #endregion Print

        #region Tools
        double calcJahresrechnungNetto(JahresDatenData jdd, KundenData kunde, PreisData preis);
        double calcMwSt();
        double calcJahresrechnungBrutto(JahresDatenData jdd, KundenData kunde, PreisData preis);
        int calcVerbrauch(JahresDatenData jdd);
        #endregion Tools
        }
}
