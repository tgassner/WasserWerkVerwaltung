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
            IList<JahresDatenData> GetAllJahresdata();
            IList<JahresDatenData> GetJahresdataByKundenID(long kundenID);
            bool UpdateJahresDaten(JahresDatenData jahresDatum);
            JahresDatenData InsertJahresDaten(JahresDatenData jahresDatum);
            bool DeleteJahresDaten(long jahresDatumID);
            JahresDatenData GetJahresdataByKundenIDandYear(long kundenID, long year);
        #endregion JahresDaten

        #region Preis
            IList<PreisData> GetAllPreise();
            PreisData GetPreisDataByJahr(long jahr);
            PreisData InsertPreis(PreisData preis);
            bool UpdatePreis(PreisData jahresDatum);
        #endregion Preis

        #region Join
        bool hasKundeJahresdataByPreis(KundenData kunde, PreisData preis);
        bool hasKundeJahresdataByJahr(KundenData kunde, long jahr);
        #endregion Join

        #region Print
            void PrintJahresRechnungen(IList<KundenData> kunden, PreisData preis);
            void PrintHalbJahresRechnungen(IList<KundenData> kunden, PreisData preis);
            void PrintKontrollZettel(IList<KundenData> kunden, PreisData preis);
        #endregion Print

        #region Tools
        double calcJahresrechnungNetto(JahresDatenData jdd, KundenData kunde, PreisData preis);
        double calcMwSt(JahresDatenData jdd, KundenData kunde, PreisData preis);
        double calcJahresrechnungBrutto(JahresDatenData jdd, KundenData kunde, PreisData preis);
        int calcVerbrauch(JahresDatenData jdd);
        #endregion Tools
        }
}
