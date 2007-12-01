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
                            jahresDatum.ZaehlerStandAlt,
                            jahresDatum.ZaehlerStandNeu,
                            jahresDatum.Jahr,
                            jahresDatum.AbleseDatum,
                            jahresDatum.BereitsBezahlt,
                            jahresDatum.TauschZaehlerStandAlt,
                            jahresDatum.TauschZaehlerStandNeu,
                            jahresDatum.SonstigeForderungenText,
                            jahresDatum.SonstigeForderungenValue,
                            jahresDatum.HalbjahresZahlung);
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

        const float linkerRand = 75;
        const float mittlererRand = linkerRand + 500;
        const float obererRand = 100;
        const float zeilenabstand = 21;
        const float stdFontSize = 13;

        public void PrintJahresRechnungen(IList<KundenData> kunden, PreisData preis) {
            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";

            PrintablePage ppd;
            foreach(KundenData kunde in kunden){
                ppd = new PrintablePage();
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);
                if (jdd != null) {
                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Jahreswasserrechnung " + preis.Jahr, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand + 180, obererRand + 6 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 8 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("für das Objekt: " + kunde.Objekt, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 10 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Zählerstand alt/neu = " + jdd.ZaehlerStandAlt + " m³ / " + jdd.ZaehlerStandNeu + " m³", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 11 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tauschzählerstand Einbau/neu = " + jdd.TauschZaehlerStandAlt + " m³ / " + jdd.TauschZaehlerStandNeu + " m³", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 12 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wasserverbrauch = " + this.calcVerbrauch(jdd) + " m³", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 13 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wassergebühr/m³ = EUR  " + preis.Preis + " + Mwst", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 14 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Wasserkosten =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 16 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + Math.Round(this.calcVerbrauch(jdd) * preis.Preis, 2) + " netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 16 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Zählermiete =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + kunde.Zaehlermiete + " netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 17 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Sonstige Forderungen: " + jdd.SonstigeForderungenText + " =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 18 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + jdd.SonstigeForderungenValue + " netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 18 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 19 * zeilenabstand), (int)linkerRand + 650, (int)(obererRand + 19 * zeilenabstand)));
                    
                    ppd.AddPrintableObject(new PrintableTextObject("Rechnungssumme netto =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 19 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + this.calcJahresrechnungNetto(jdd, kunde, preis), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 19 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("zuzügl 10% Mwst =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 20 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + Math.Round((this.calcJahresrechnungNetto(jdd, kunde, preis) * 0.1), 2), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 20 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 21 * zeilenabstand), (int)linkerRand + 650, (int)(obererRand + 21 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject("Jahressumme gesamt =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 21 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + this.calcJahresrechnungBrutto(jdd,kunde,preis) + " incl. Mwst.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 21 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Abzüglich Akontozahlung Halbjahr inkl. Mwst =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 22 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR -" + jdd.HalbjahresZahlung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 22 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 24 * zeilenabstand), (int)linkerRand + 650, (int)(obererRand + 24 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject("Einzuzahlender Betrag = ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + (this.calcJahresrechnungBrutto(jdd,kunde,preis) - jdd.HalbjahresZahlung), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 25 * zeilenabstand), (int)linkerRand + 650, (int)(obererRand + 25 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Zahlung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 26 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Unsere ATU Nummer: ATU 563 88 929", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 30 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bankverbindung: Erste Bank Haag BLZ 20111 Kontonummer 267 12 769 00", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 31 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel/Fax: 07434/44398 oder 0676/620 32 38", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 32 * zeilenabstand));
                    
                    pdd.AddPrintPage(ppd);
                }
            }

            pdd.DoPrint();
        }

        #endregion Print

        #region Tools
        public double calcJahresrechnungNetto(JahresDatenData jdd, KundenData kunde, PreisData preis) {
            return Math.Round((((double)this.calcVerbrauch(jdd)) * preis.Preis) + kunde.Zaehlermiete + jdd.SonstigeForderungenValue, 2);
        }

        public double calcMwSt(JahresDatenData jdd, KundenData kunde, PreisData preis) {
            return Math.Round(calcJahresrechnungNetto(jdd, kunde, preis) * 0.1,2);
        }

        public double calcJahresrechnungBrutto(JahresDatenData jdd, KundenData kunde, PreisData preis) {
            return Math.Round(this.calcJahresrechnungNetto(jdd, kunde, preis) * 1.1, 2);
        }

        public int calcVerbrauch(JahresDatenData jdd) {
            return (int)(jdd.ZaehlerStandNeu - jdd.ZaehlerStandAlt) + (int)(jdd.TauschZaehlerStandAlt - jdd.TauschZaehlerStandNeu);
        }

        #endregion Tools
    }
}
