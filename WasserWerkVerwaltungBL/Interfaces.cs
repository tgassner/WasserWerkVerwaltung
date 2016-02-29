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
            void GenerateHalbJahresBetragFuerJahr(long jahr);
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
            void PrintBezahltCheckListe(IList<KundenData> kunden, PreisData preis);
            void PrintKontrollZettel(IList<KundenData> kunden, PreisData preis);
            void PrintZaehlerstandabrechnungsFormular(IList<KundenData> selectedKundenList, PreisData preisData, string textZaehlerstandformular, DateTime von, DateTime bis);
            void PrintMahnung1(IList<KundenData> kunden, PreisData preis);
            void PrintMahnung2(IList<KundenData> kunden, PreisData preis);
            void PrintJahresrechnungsdatumUebersicht(IList<KundenData> kunden, PreisData preis);
            void PrintHalbjahresrechnungsdatumUebersicht(IList<KundenData> kunden, PreisData preis);
            void PrintStammdaten(IList<KundenData> kunden);
        #endregion Print

        #region Tools
            string FormatDezimal(double d);
            double calcJahresRechnungMinusBereitsBezahlt(JahresDatenData jdd, KundenData kunde, PreisData preis);
            double calcJahresrechnungNetto(JahresDatenData jdd, KundenData kunde, PreisData preis);
            double calcMwSt(JahresDatenData jdd, KundenData kunde, PreisData preis);
            double calcJahresrechnungBrutto(JahresDatenData jdd, KundenData kunde, PreisData preis);
            int calcVerbrauch(JahresDatenData jdd);
            bool doDbBackup(string backupFileName);
            bool doDbImport(string fileName);
        #endregion Tools
    }
}
