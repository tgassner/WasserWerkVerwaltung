using System;
using System.Collections.Generic;
using System.Text;
using WasserWerkVerwaltung.DAL;
using WasserWerkVerwaltung.CommonObjects;
using WasserWerkVerwaltung.Print;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Threading;

namespace WasserWerkVerwaltung.BL {
    public class WWVBusinessObject : IWWVBL {

        private IList<KundenData> allKunden = null;
        private IList<JahresDatenData> allJahresData = null;

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
                            kunde.Leitungskreis,
                            kunde.PersonenImObjekt);
                this.allKunden.Add(kunde);
                return kunde;
            } else {
                return null;
            }

        }

        #endregion Kunde

        #region JahresDaten

        public IList<JahresDatenData> GetAllJahresdata() {
            if (allJahresData == null) {
                IJahresDaten jahresdatenDB = Database.CreateJahresDaten();
                allJahresData = jahresdatenDB.FindAll();
            }
            return allJahresData;
        }

        public IList<JahresDatenData> GetJahresdataByKundenID(long kundenID) {
            if (allJahresData == null) {
                //Todo: asyncron einbauen für alle und nur da eine jetzt suchen!!
                //IJahresDaten jahresdatenDB = Database.CreateJahresDaten();
                //return jahresdatenDB.FindByKundenId(kundenID);
                this.GetAllJahresdata();
            }
 
            IList<JahresDatenData> jahresDaten = new List<JahresDatenData>();
            foreach(JahresDatenData jdd in this.allJahresData){
                if (jdd.KundenId == kundenID) {
                    jahresDaten.Add(jdd);
                }
            }
            return jahresDaten;
        }

        public bool UpdateJahresDaten(JahresDatenData jahresDatum) {
            IJahresDaten jahredDataDB = Database.CreateJahresDaten();

            if (jahredDataDB.Update(jahresDatum)) {
                if (this.allJahresData != null) {
                    this.allJahresData.Remove(jahresDatum);
                    this.allJahresData.Add(jahresDatum);
                }
                return true;
            } else {
                return false;
            }  
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
                            jahresDatum.HalbJahresBetrag);
                if (this.allJahresData != null){
                    this.allJahresData.Add(jahresDatum);
                }
                return jahresDatum;
            } else {
                return null;
            }
        }

        public bool DeleteJahresDaten(long jahresDatumID) {
            IJahresDaten jahredDataDB = Database.CreateJahresDaten();
            bool ok = jahredDataDB.Delete(jahresDatumID);
            JahresDatenData jdd = null;
            if (ok) {
                foreach (JahresDatenData jddforeach in this.allJahresData) {
                    if (jddforeach.Id == jahresDatumID) {
                        jdd = jddforeach;
                    }
                }
                if (jdd != null) {
                    this.allJahresData.Remove(jdd);
                }
            }
            return ok;
        }

        public JahresDatenData GetJahresdataByKundenIDandYear(long kundenID, long year) {
            //IJahresDaten jahresdatenDB = Database.CreateJahresDaten();
            //foreach (JahresDatenData jdd in jahresdatenDB.FindByKundenId(kundenID)) {
            foreach (JahresDatenData jdd in this.GetJahresdataByKundenID(kundenID)) {
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
            IPreis preisDB = Database.CreatePreis();
            return preisDB.Update(jahresDatum);
        }

        public IList<PreisData> GetAllPreise() {
            IPreis preisDB = Database.CreatePreis();
            return preisDB.FindAll();
        }
        #endregion Preis

        #region Join

        public bool hasKundeJahresdataByPreis(KundenData kunde, PreisData preis) {
            JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);
            return (jdd == null) ? false : true;
        }

        public bool hasKundeJahresdataByJahr(KundenData kunde, long jahr) {
            JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, jahr);
            return (jdd == null) ? false : true;
        }

        #endregion Join

        #region Print

        const float linkerRand = 75;
        const float mittlererRand = linkerRand + 500;
        const float rechterRand = mittlererRand + 105;
        const float mittelinkerRand = linkerRand + 250;
        const float obererRand = 90;
        const float zeilenabstand = 21;
        const float stdFontSize = 13;

        public void PrintJahresRechnungen(IList<KundenData> kunden, PreisData preis) {
            foreach (KundenData kunde in kunden) {
                if (kunde.BekommtRechnung == Rechnung.Keine) {
                    MessageBox.Show("Mindestens ein Kunde bekommt keine Rechnung!\r\nVorgang abgebrochen!");
                    return;
                }
            }
            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";

            PrintablePage ppd;
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            NumberFormatInfo nfi = ci.NumberFormat;
            foreach(KundenData kunde in kunden){
                ppd = new PrintablePage();
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);
                if (jdd != null) {
                    ppd.AddPrintableObject(new PrintableTextObject("WASSERWERK WEINBERGER", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bahnhofstraße 27", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("3350 Haag", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel: 07434/44398", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Jahreswasserrechnung " + preis.Jahr, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand + 180, obererRand + 13 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 15 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("für das Objekt:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Objekt, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 17 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Zählerstand alt/neu =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 18 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(jdd.ZaehlerStandAlt + " m³ / " + jdd.ZaehlerStandNeu + " m³", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 18 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tauschzählerst. Einbau/neu =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 19 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(jdd.TauschZaehlerStandAlt + " m³ / " + jdd.TauschZaehlerStandNeu + " m³", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 19 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wasserverbrauch =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 20 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(this.calcVerbrauch(jdd) + " m³", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 20 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wassergebühr/m³ =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 21 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR  " + FormatDezimal(preis.Preis) + " + Mwst", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 21 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Wasserkosten =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 23 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(Math.Round(this.calcVerbrauch(jdd) * preis.Preis, 2)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 23 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, rechterRand, obererRand + 23 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Zählermiete =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(kunde.Zaehlermiete), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, rechterRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Sonstige Forderungen: " + jdd.SonstigeForderungenText + " =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(jdd.SonstigeForderungenValue), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, rechterRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 26 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 26 * zeilenabstand)));
                    
                    ppd.AddPrintableObject(new PrintableTextObject("Rechnungssumme netto =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 26 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(this.calcJahresrechnungNetto(jdd, kunde, preis)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 26 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("zuzügl 10% Mwst =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 27 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(this.calcMwSt(jdd,kunde,preis)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 27 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 28 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 28 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject("Jahressumme gesamt =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 28 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(this.calcJahresrechnungBrutto(jdd,kunde,preis)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 28 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("incl. Mwst.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, rechterRand, obererRand + 28 * zeilenabstand));

                    if (kunde.BekommtRechnung == Rechnung.Halbjahres) {
                        ppd.AddPrintableObject(new PrintableTextObject("Abzüglich Akontozahlung Halbjahr inkl. Mwst =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 29 * zeilenabstand));
                        ppd.AddPrintableObject(new PrintableTextObject("EUR -" + FormatDezimal(jdd.HalbJahresBetrag), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 29 * zeilenabstand));
                    }

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 31 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 31 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject("Einzuzahlender Betrag = ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 31 * zeilenabstand));
                    if (kunde.BekommtRechnung == Rechnung.Halbjahres) {
                        ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(Math.Round((this.calcJahresrechnungBrutto(jdd, kunde, preis) - jdd.HalbJahresBetrag),2)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 31 * zeilenabstand));
                    } 
                    if (kunde.BekommtRechnung == Rechnung.Jahres) {
                        ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal((this.calcJahresrechnungBrutto(jdd, kunde, preis))), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 31 * zeilenabstand));
                    }

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 32 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 32 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Zahlung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 33 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Unsere ATU Nummer: ATU 563 88 929", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 37 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bankverbindung: Erste Bank Haag BLZ 20111 Kontonummer 267 126 769 00", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 38 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel/Fax: 07434/44398 oder 0676/620 32 38", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 39 * zeilenabstand));
                    
                    pdd.AddPrintPage(ppd);
                }
            }

            pdd.DoPrint();
        }

        public void PrintHalbJahresRechnungen(IList<KundenData> kunden, PreisData preis) {
            foreach (KundenData kunde in kunden) {
                if (kunde.BekommtRechnung != Rechnung.Halbjahres){
                    MessageBox.Show("Mindestens ein Kunde bekommt keine Halbjahresrechnung!\r\nVorgang abgebrochen!");
                    return;
                }
            }
            
            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";

            PrintablePage ppd;
            foreach (KundenData kunde in kunden) {
                ppd = new PrintablePage();
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);
                if (jdd != null) {
                    ppd.AddPrintableObject(new PrintableTextObject("WASSERWERK WEINBERGER", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bahnhofstraße 27", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("3350 Haag", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel: 07434/44398", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Halbjahreswasserrechnung " + preis.Jahr, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand + 180, obererRand + 13 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 15 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("für das Objekt:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Objekt, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 17 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Die Rechnung ist eine Akontierung und entspricht", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 18 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("ca. der Hälfte der Vorjahresrechnung.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 19 * zeilenabstand));
                    

                    ppd.AddPrintableObject(new PrintableTextObject("Nettobetrag =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 22 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(Math.Round(jdd.HalbJahresBetrag/1.2,2)) + " + 10% Mwst = EUR " + FormatDezimal(jdd.HalbJahresBetrag), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 22 * zeilenabstand));
                    
                    
                    ppd.AddPrintableObject(new PrintableTextObject("Einzuzahlender Betrag =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(jdd.HalbJahresBetrag), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 26 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 26 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Zahlung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 29 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Unsere ATU Nummer: ATU 563 88 929", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 35 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bankverbindung: Erste Bank Haag BLZ 20111 Kontonummer 267 126 769 00", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 36 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel/Fax: 07434/44398 oder 0676/620 32 38", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 37 * zeilenabstand));

                    pdd.AddPrintPage(ppd);
                }
            }

            pdd.DoPrint();
        }

        public void PrintKontrollZettel(IList<KundenData> kunden, PreisData preis) {
            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";

            PrintablePage ppd = new PrintablePage();
            int counter = 0;
            int zeile = 0;
            foreach (KundenData kunde in kunden) {
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);
                counter++;
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Nachname + " " + kunde.Vorname + "  Objekt: " + kunde.Objekt + "  Jahreswasserre. " + DateTime.Now.Year.ToString(), new Font("Arial", stdFontSize, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Zählerstand alt/neu m³ " + jdd.ZaehlerStandAlt + "/" + jdd.ZaehlerStandNeu + "  Tz " + jdd.TauschZaehlerStandAlt + "/" + jdd.TauschZaehlerStandNeu, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Wasserverbrauch = " + this.calcVerbrauch(jdd) + "     Wassergebühr pro m³ = " + preis.Preis.ToString(), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Wasserkosten = EUR " + Math.Round(this.calcVerbrauch(jdd) * preis.Preis,2) + "     Zählermiete = EUR " + kunde.Zaehlermiete + " netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Sonstige Forderungen: " + jdd.SonstigeForderungenText + " = " + jdd.SonstigeForderungenValue, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Rechnungssumme netto = EUR " + this.calcJahresrechnungNetto(jdd,kunde,preis) + "    zuzügl 10% Mwst = EUR " + this.calcMwSt(jdd,kunde,preis), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Jahresrechnungssumme gesamt incl. Mwst = EUR " + this.calcJahresrechnungBrutto(jdd,kunde,preis), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Abzüglich 1/2 Jahresrechnung = EUR " + jdd.HalbJahresBetrag, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Zahlbetrag = " + (this.calcJahresrechnungBrutto(jdd,kunde,preis) - jdd.HalbJahresBetrag) + "   Bankverbindung: " + kunde.BankVerbindung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Bekommt Rechnung: " + kunde.BekommtRechnung.ToString() + "   Bemerkung: " + kunde.Bemerkung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                
                if ((counter % 5) == 0) {
                    pdd.AddPrintPage(ppd);
                    ppd = new PrintablePage();
                    zeile = 0;
                } else {
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + zeile * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + zeile * zeilenabstand)));
                }
            }
            if ((counter % 5) != 0) {
                pdd.AddPrintPage(ppd);
            }
            pdd.DoPrint();
        }

        #endregion Print

        #region Tools

        public string FormatDezimal(double d) {
            return String.Format("{0:F}", d);
        }

        public double calcJahresrechnungNetto(JahresDatenData jdd, KundenData kunde, PreisData preis) {
            return Math.Round((((double)this.calcVerbrauch(jdd)) * preis.Preis) + kunde.Zaehlermiete + jdd.SonstigeForderungenValue, 2);
        }

        public double calcMwSt(JahresDatenData jdd, KundenData kunde, PreisData preis) {
            return Math.Round(calcJahresrechnungNetto(jdd, kunde, preis) * 0.1,2);
        }

        public double calcJahresrechnungBrutto(JahresDatenData jdd, KundenData kunde, PreisData preis) {
            return Math.Round(this.calcJahresrechnungNetto(jdd, kunde, preis) + this.calcMwSt(jdd,kunde,preis), 2);
        }

        public int calcVerbrauch(JahresDatenData jdd) {
            return (int)(jdd.ZaehlerStandNeu - jdd.ZaehlerStandAlt) + (int)(jdd.TauschZaehlerStandNeu - jdd.TauschZaehlerStandAlt);
        }

        #endregion Tools
    }
}
