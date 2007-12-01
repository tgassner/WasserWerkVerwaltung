using System;
using System.Collections.Generic;
using System.Text;
using WasserWerkVerwaltung.DAL;
using WasserWerkVerwaltung.CommonObjects;
using WasserWerkVerwaltung.Print;
using System.Drawing;
using System.Globalization;

namespace WasserWerkVerwaltung.BL {
    public class WWVBusinessObject : IWWVBL {

        private IList<KundenData> allKunden = null;

        internal WWVBusinessObject() {
        }

        #region Kunde
        public IList<KundenData> GetAllKunden() {
            if (allKunden == null) {
                IKunde kundenDB = Database.CreateKunde();
                allKunden = kundenDB.FindAll();
            }
            return allKunden;
        }

        bool IWWVBL.UpdateKunde(KundenData kunde) {
            IKunde kundeDB = Database.CreateKunde();
            if (kundeDB.Update(kunde)) {
                this.allKunden.Remove(kunde);
                this.allKunden.Add(kunde);

                return true;
            } else {
                return false;
            }
        }

        public KundenData InsertKunde(KundenData kunde) {
            IKunde kundeDB = Database.CreateKunde();
            long kID = kundeDB.Insert(kunde);
            if (kID > 0) {
                kunde = new KundenData(kID,
                            kunde.Vorname,
                            kunde.Nachname,
                            kunde.Strasse,
                            kunde.Ort,
                            kunde.Objekt,
                            kunde.Tel,
                            kunde.Hausbesitzer,
                            kunde.BankVerbindung,
                            kunde.BekommtRechnung,
                            kunde.ZaehlerEinbauStand,
                            kunde.ZaehlerNeuStand,
                            kunde.EichDatum,
                            kunde.ZaehlerNummer,
                            kunde.EinbauDatum,
                            kunde.Erkl,
                            kunde.TauschDatum,
                            kunde.Zaehlermiete,
                            kunde.Bemerkung,
                            kunde.Zahlung,
                            kunde.Leitungskreis);
                this.allKunden.Add(kunde);
                return kunde;
            } else {
                return null;
            }

        }

        #endregion Kunde

        #region JahresDaten
        public IList<JahresDatenData> GetJahresdataByKundenID(long kundenID) {
            IJahresDaten jahresdatenDB = Database.CreateJahresDaten();
            return jahresdatenDB.FindByKundenId(kundenID);
        }

        public bool UpdateJahresDaten(JahresDatenData jahresDatum) {
            IJahresDaten jahredDataDB = Database.CreateJahresDaten();
            return jahredDataDB.Update(jahresDatum);
            //if (jahredDataDB.Update(jahresDatum)) {
            //    return true;
            //} else {
            //    return false;
            //}   
        }

        public JahresDatenData InsertJahresDaten(JahresDatenData jahresDatum) {
            IJahresDaten jahredDataDB = Database.CreateJahresDaten();
            long jdID = jahredDataDB.Insert(jahresDatum);
            if (jdID > 0) {
                jahresDatum = new JahresDatenData(jdID,
                            jahresDatum.KundenId,
                            jahresDatum.Rechnungssumme,
                            jahresDatum.ZaehlerStandAlt,
                            jahresDatum.ZaehlerStandNeu,
                            jahresDatum.Jahr,
                            jahresDatum.AbleseDatum,
                            jahresDatum.BereitsBezahlt);
                return jahresDatum;
            } else {
                return null;
            }
        }

        public bool DeleteJahresDaten(long jahresDatumID) {
            IJahresDaten jahredDataDB = Database.CreateJahresDaten();
            return jahredDataDB.Delete(jahresDatumID);
        }

        public JahresDatenData GetJahresdataByKundenIDandYear(long kundenID, long year) {
            IJahresDaten jahresdatenDB = Database.CreateJahresDaten();
            foreach (JahresDatenData jdd in jahresdatenDB.FindByKundenId(kundenID)) {
                if (jdd.Jahr == year) {
                    return jdd;
                }
            }
            return null;
        }

        #endregion JahresDaten

        #region Preis
        public PreisData GetPreisDataByJahr(long jahr) {
            IPreis preisDB = Database.CreatePreis();
            return preisDB.FindByJahr(jahr);
        }

        public PreisData InsertPreis(PreisData preis) {
            IPreis preisDB = Database.CreatePreis();
            bool b = preisDB.Insert(preis);
            if (b) {
                preis = new PreisData(preis.Jahr,
                            preis.Preis);
                return preis;
            } else {
                return null;
            }
        }

        public bool UpdatePreis(PreisData jahresDatum) {
            throw new Exception("The method or operation is not implemented.");
        }

        public IList<PreisData> GetAllPreise() {
            IPreis preisDB = Database.CreatePreis();
            return preisDB.FindAll();
        }
        #endregion Preis



        #region Print

        const float linkerRand = 25;
        const float obererRand = 50;
        const float zeilenabstand = 20;
        const float stdFontSize = 13;

        public void PrintJahresRechnungen(IList<KundenData> kunden, PreisData preis) {
            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";

            PrintablePage ppd;
            foreach(KundenData kunde in kunden){
                ppd = new PrintablePage();
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);
                if (jdd != null) {
                    int verbrauch = (int)(jdd.ZaehlerStandNeu - jdd.ZaehlerStandAlt);
                    double rechnungssummeNetto = Math.Round((((double)verbrauch)*preis.Preis) + kunde.Zaehlermiete,2);
                    double rechnungssummeBrutto = Math.Round(rechnungssummeNetto * 1.1,2);
                    ppd.AddPrintableObject(new PrintableTextObject("WASSERWERK WEINBERGER - 3350 Stadt Haag Bahnhofstr. 27", new Font("Arial", 18, FontStyle.Underline), Brushes.Black, linkerRand, obererRand));
                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 10 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 11 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Jahreswasserrechnung " + preis.Jahr, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand + 250, obererRand + 19 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 675, obererRand + 22 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("für das Objekt: " + kunde.Objekt, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Zählerstand alt/neu = " + jdd.ZaehlerStandAlt + " m³ / " + jdd.ZaehlerStandNeu + " m³", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 26 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tauschzählerstand Einbau/neu = " + kunde.ZaehlerEinbauStand.ToString() + " m³ / " + kunde.ZaehlerNeuStand + " m³", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 28 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wasserverbrauch = " + verbrauch + " m³", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 30 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Zählermiete = EUR " + kunde.Zaehlermiete + " netto, Wassergebühr/m³ = EUR " + preis.Preis + " + Mwst", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 32 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wasserkosten = EUR " + Math.Round(verbrauch * preis.Preis, 2) + " netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 34 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Rechnungssumme netto = EUR " + rechnungssummeNetto + " zuzügl 10% Mwst = EUR " + Math.Round((rechnungssummeNetto * 0.1), 2), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 36 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Jahressumme gesamt", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 38 * zeilenabstand));
                        ppd.AddPrintableObject(new PrintableTextObject("= EUR " + rechnungssummeBrutto + " incl. Mwst.", new Font("Arial", stdFontSize, FontStyle.Underline), Brushes.Black, linkerRand + 350, obererRand + 38 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Sonstige Forderungen", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 40 * zeilenabstand));
                        ppd.AddPrintableObject(new PrintableTextObject("= EUR " + Math.Round(jdd.Rechnungssumme - rechnungssummeBrutto, 2), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 350, obererRand + 40 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Einzuzahlender Betrag", new Font("Arial", stdFontSize, FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 42 * zeilenabstand));
                        ppd.AddPrintableObject(new PrintableTextObject("= EUR " + jdd.Rechnungssumme, new Font("Arial", stdFontSize, FontStyle.Underline), Brushes.Black, linkerRand + 350, obererRand + 42 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Zahlung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 44 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Unsere ATU Nummer: ATU 563 88 929", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 50 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bankverbindung: Erste Bank Haag BLZ 20111 Kontonummer 267 12 769 00", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 51 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel/Fax: 07434/44398 oder 0676/620 32 38", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 52 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Red, 100, 100, 200, 200));
                    pdd.AddPrintPage(ppd);
                }
            }

            pdd.DoPrint();
        }

        #endregion Print
    }
}
