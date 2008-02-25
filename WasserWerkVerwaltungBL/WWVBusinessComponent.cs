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
                //Todo: asyncron einbauen f�r alle und nur da eine jetzt suchen!!
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

        /*
         * Die Semantik dieses Buttons ist, da� er f�r ein Jahr alle Kunden 
         * die noch keinen Z�hlerstandeintrag f�r das Jahr haben den 
         * Halbjahresz�hlerstand vom Jahr davor berechnet, (mit �berpr�fung, 
         * da� mindestens ein Kunde im Vorjahr einen Eintrag haben mu�.) Kunden 
         * die bereits einen Jahresz�hlerstand eintrag f�r das JAhr haben werden 
         * ignoriert um einen bestehenden Betrag nicht zu �berschreiben. 
         * (Sollte zwar eh der selbe sein, aber du hast ja immer die M�glichkeit 
         * den Betrag anzupassen.) 
         */

        public void GenerateHalbJahresBetragFuerJahr(long jahr) {
            int countKeinenAltenEintrag = 0;
            int countBereitsEinenNeuenEintrag = 0;
            int countEintraegerErstellt = 0;
            int countFehlerBeimEintragen = 0;

            PreisData pd = this.GetPreisDataByJahr(jahr);
            if (pd == null){
                MessageBox.Show("Kein Wasserpreis / m� f�rs Jahr " + jahr + " definiert -> bitte zuvor definieren.");
                return;
            }

            foreach (KundenData kunde in this.GetAllKunden()) {
                

                if (this.hasKundeJahresdataByJahr(kunde, jahr)) {
                    countBereitsEinenNeuenEintrag++;
                }

                if (!this.hasKundeJahresdataByJahr(kunde, jahr-1)) {
                    countKeinenAltenEintrag++;
                }

                if (! this.hasKundeJahresdataByJahr(kunde,jahr) &&
                    this.hasKundeJahresdataByJahr(kunde, jahr-1)){

                    // Kunde hat im Vorjahr einen Eintrag und im aktuellen nicht!
                    // Es wird eine neue JahresDatenzeile erstellt in der 
                    // also wird HalbjahresWert eingrtragen

                    JahresDatenData jdd = new JahresDatenData(0,
                                                                kunde.Id,
                                                                0,
                                                                0,
                                                                jahr,
                                                                DateTime.MinValue,
                                                                0.0,
                                                                0,
                                                                0,
                                                                "",
                                                                0.0,
                                                                ((double)Math.Round(this.calcJahresrechnungBrutto(this.GetJahresdataByKundenIDandYear(kunde.Id,jahr - 1), kunde, pd) / 2, 2)));

                    JahresDatenData jdd2 = this.InsertJahresDaten(jdd);

                    if (jdd2 != null) {
                        countEintraegerErstellt++;
                    } else {
                        countFehlerBeimEintragen++;
                    }
                }
                
            }
            MessageBox.Show(countEintraegerErstellt + " Eintr�ge erstellt f�r das Jahr " + jahr + " auf Basis vom Jahr: " + (jahr - 1) + "\r\n" + countKeinenAltenEintrag + " hatten keinen Eintrag im Jahr " + (jahr - 1) + "\r\n" + countBereitsEinenNeuenEintrag + " hatten bereits einen Eintrag im Jahr " + jahr + "\r\n" + countFehlerBeimEintragen + " Eintr�ge konnten nicht erstellt werden.");
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
                    ppd.AddPrintableObject(new PrintableTextObject("Bahnhofstra�e 27", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("3350 Haag", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel: 07434/44398", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Jahreswasserrechnung " + preis.Jahr, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand + 180, obererRand + 13 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 15 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("f�r das Objekt:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Objekt, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 17 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Z�hlerstand alt/neu =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 18 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(jdd.ZaehlerStandAlt + " m� / " + jdd.ZaehlerStandNeu + " m�", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 18 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tauschz�hlerst. Einbau/neu =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 19 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(jdd.TauschZaehlerStandAlt + " m� / " + jdd.TauschZaehlerStandNeu + " m�", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 19 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wasserverbrauch =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 20 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(this.calcVerbrauch(jdd) + " m�", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 20 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wassergeb�hr/m� =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 21 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR  " + FormatDezimal(preis.Preis) + " + Mwst", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 21 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Wasserkosten =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 23 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(Math.Round(this.calcVerbrauch(jdd) * preis.Preis, 2)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 23 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, rechterRand, obererRand + 23 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Z�hlermiete =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(kunde.Zaehlermiete), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, rechterRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Sonstige Forderungen: " + jdd.SonstigeForderungenText + " =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(jdd.SonstigeForderungenValue), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, rechterRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 26 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 26 * zeilenabstand)));
                    
                    ppd.AddPrintableObject(new PrintableTextObject("Rechnungssumme netto =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 26 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(this.calcJahresrechnungNetto(jdd, kunde, preis)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 26 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("zuz�gl 10% Mwst =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 27 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(this.calcMwSt(jdd,kunde,preis)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 27 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 28 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 28 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject("Jahressumme gesamt =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 28 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(this.calcJahresrechnungBrutto(jdd,kunde,preis)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 28 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("incl. Mwst.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, rechterRand, obererRand + 28 * zeilenabstand));

                    if (kunde.BekommtRechnung == Rechnung.Halbjahres) {
                        ppd.AddPrintableObject(new PrintableTextObject("Abz�glich Akontozahlung Halbjahr inkl. Mwst =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 29 * zeilenabstand));
                        ppd.AddPrintableObject(new PrintableTextObject("EUR -" + FormatDezimal(this.calcJahresRechnungMinusBereitsBezahlt(jdd,kunde,preis)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 29 * zeilenabstand));
                    }

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 31 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 31 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject("Einzuzahlender Betrag = ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 31 * zeilenabstand));
                    if (kunde.BekommtRechnung == Rechnung.Halbjahres) {
                        ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(this.calcJahresrechnungBrutto(jdd, kunde, preis) - this.calcJahresRechnungMinusBereitsBezahlt(jdd,kunde,preis)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 31 * zeilenabstand));
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
                    ppd.AddPrintableObject(new PrintableTextObject("Bahnhofstra�e 27", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("3350 Haag", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel: 07434/44398", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Halbjahreswasserrechnung " + preis.Jahr, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand + 180, obererRand + 13 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 15 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("f�r das Objekt:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Objekt, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 17 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Die Rechnung ist eine Akontierung und entspricht", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 18 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("ca. der H�lfte der Vorjahresrechnung.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 19 * zeilenabstand));
                    

                    ppd.AddPrintableObject(new PrintableTextObject("Nettobetrag =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 22 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(jdd.HalbJahresBetrag / 1.1) + " + " + FormatDezimal(jdd.HalbJahresBetrag * (0.1/1.1)) + " (10% Mwst) = EUR " + FormatDezimal(jdd.HalbJahresBetrag), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 22 * zeilenabstand));
                    
                    
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

        public void PrintBezahltCheckListe(IList<KundenData> kunden, PreisData preis) {
            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";
            int seitencounter = 1;
            PrintablePage ppd = new PrintablePage();
            ppd.AddPrintableObject(new PrintableTextObject("Seite " + seitencounter, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand - 30, obererRand - 30));
            ppd.AddPrintableObject(new PrintableTextObject("OFFENE RECHNUNGEN  " + preis.Jahr, new Font("Arial", 12, FontStyle.Bold),Brushes.Black,mittelinkerRand - 20,obererRand - 30));
            ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.ToShortDateString(), new Font("Arial", 9, FontStyle.Bold),Brushes.Black,rechterRand,obererRand - 30));
            ppd.AddPrintableObject(new PrintableTextObject("Name, Vorname", new Font("Arial", 9, FontStyle.Bold),Brushes.Black,linkerRand - 30,obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("Objekt", new Font("Arial", 9, FontStyle.Bold),Brushes.Black,linkerRand + 190,obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("Rechnunssum", new Font("Arial", 9, FontStyle.Bold),Brushes.Black,linkerRand + 410,obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("bereits bezahlt", new Font("Arial", 9, FontStyle.Bold),Brushes.Black,linkerRand + 510,obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("Noch ausst�ndig", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 610, obererRand + 15));

            int kleinerZeilenabstand = 15;
            int zeile = 8;
            int counter = 0;
            ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand - 30, (int)kleinerZeilenabstand * (int)(zeile), (int)linkerRand + 700, (int)kleinerZeilenabstand * (int)(zeile)));
            double sumSum = 0;
            double bereitsBezahlt = 0;
            double ausstaendig = 0;
            foreach (KundenData kunde in kunden) {
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);

                ppd.AddPrintableObject(new PrintableTextObject(kunde.Nachname + " " + kunde.Vorname, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand - 30, zeile * kleinerZeilenabstand));
                ppd.AddPrintableObject(new PrintableFillRectangleObject(Brushes.White,(int)linkerRand+188,zeile * kleinerZeilenabstand + 1,300,13));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Objekt, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 190, zeile * kleinerZeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(this.calcJahresrechnungBrutto(jdd, kunde, preis)) + " EUR", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 410, zeile * kleinerZeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(jdd.BereitsBezahlt) + " EUR", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 510, zeile * kleinerZeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(calcJahresRechnungMinusBereitsBezahlt(jdd,kunde,preis)) + " EUR", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 610, zeile * kleinerZeilenabstand));

                ppd.AddPrintableObject(new PrintableLineObject(Pens.LightGray, (int)linkerRand - 30, (int)kleinerZeilenabstand * (int)(zeile+1), (int)linkerRand + 700, (int)kleinerZeilenabstand * (int)(zeile+1)));
                sumSum += this.calcJahresrechnungBrutto(jdd, kunde, preis);
                bereitsBezahlt += jdd.BereitsBezahlt;
                ausstaendig += (this.calcJahresrechnungBrutto(jdd, kunde, preis) - jdd.BereitsBezahlt);
                zeile++;
                counter++;

                if ((counter % 65) == 0) {
                    pdd.AddPrintPage(ppd);
                    ppd = new PrintablePage();
                    zeile = 8;
                    seitencounter++;

                    ppd.AddPrintableObject(new PrintableTextObject("Seite " + seitencounter, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand - 30, obererRand - 30));
                    ppd.AddPrintableObject(new PrintableTextObject("OFFENE RECHNUNGEN  " + preis.Jahr, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, mittelinkerRand - 20, obererRand - 30));
                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.ToShortDateString(), new Font("Arial", 9, FontStyle.Bold), Brushes.Black, rechterRand, obererRand - 30));
                    ppd.AddPrintableObject(new PrintableTextObject("Name, Vorname", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand - 30, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("Objekt", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 190, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("Rechnunssum", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 410, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("bereits bezahlt", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 510, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("Noch ausst�ndig", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 610, obererRand + 15));

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand - 30, (int)kleinerZeilenabstand * (int)(zeile), (int)linkerRand + 700, (int)kleinerZeilenabstand * (int)(zeile)));
                }
            }
            
            ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand - 30, (int)kleinerZeilenabstand * (int)(zeile), (int)linkerRand + 700, (int)kleinerZeilenabstand * (int)(zeile)));

            ppd.AddPrintableObject(new PrintableTextObject("Summe", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand - 30, zeile * kleinerZeilenabstand));
            ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(sumSum) + " EUR", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 410, zeile * kleinerZeilenabstand));
            ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(bereitsBezahlt) + " EUR", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 510, zeile * kleinerZeilenabstand));
            ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(ausstaendig) + " EUR", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 610, zeile * kleinerZeilenabstand));

            if ((counter % 65) != 0) {
                pdd.AddPrintPage(ppd);
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
                ppd.AddPrintableObject(new PrintableTextObject("Z�hlerstand alt/neu m� " + jdd.ZaehlerStandAlt + "/" + jdd.ZaehlerStandNeu + "  Tz " + jdd.TauschZaehlerStandAlt + "/" + jdd.TauschZaehlerStandNeu, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Wasserverbrauch = " + this.calcVerbrauch(jdd) + "     Wassergeb�hr pro m� = " + preis.Preis.ToString(), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Wasserkosten = EUR " + Math.Round(this.calcVerbrauch(jdd) * preis.Preis,2) + "     Z�hlermiete = EUR " + kunde.Zaehlermiete + " netto", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Sonstige Forderungen: " + jdd.SonstigeForderungenText + " = " + jdd.SonstigeForderungenValue, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Rechnungssumme netto = EUR " + this.calcJahresrechnungNetto(jdd,kunde,preis) + "    zuz�gl 10% Mwst = EUR " + this.calcMwSt(jdd,kunde,preis), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Jahresrechnungssumme gesamt incl. Mwst = EUR " + this.calcJahresrechnungBrutto(jdd,kunde,preis), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject("Abz�glich 1/2 Jahresrechnung = EUR " + jdd.HalbJahresBetrag, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + zeile++ * zeilenabstand));
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

        public void PrintZaehlerstandabrechnungsFormular(IList<KundenData> selectedKundenList, PreisData preisData) {
            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";

            PrintablePage ppd;
            foreach (KundenData kunde in kunden) {
                ppd = new PrintablePage();
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);
                if (jdd != null) {

                    ppd.AddPrintableObject(new PrintableTextObject("WASSERWERK WEINBERGER", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bahnhofstra�e 27", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("3350 Haag", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel: 07434/44398", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Betrifft: Jahreswasserstand" + preis.Jahr, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 13 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 15 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sehr geehrte/r Herr/Frau " + kunde.Nachname + "!", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));

                    

                    
                    ppd.AddPrintableObject(new PrintableTextObject("Mit bestem Dank,", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 30 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Ing. Waltraud Weinberger-Hairas", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 33 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wasserwerk Weinberger", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 34 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Unsere ATU Nummer: ATU 563 88 929", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 40 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bankverbindung: Erste Bank Haag BLZ 20111 Kontonummer 267 126 769 00", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 41 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel/Fax: 07434/44398 oder 0676/620 32 38", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 42 * zeilenabstand));

                    pdd.AddPrintPage(ppd);
                }
            }

            pdd.DoPrint();
        }

        public void PrintMahnung1(IList<KundenData> kunden, PreisData preis) {

            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";

            PrintablePage ppd;
            foreach (KundenData kunde in kunden) {
                ppd = new PrintablePage();
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);
                if (jdd != null) {
                    ppd.AddPrintableObject(new PrintableTextObject("WASSERWERK WEINBERGER", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bahnhofstra�e 27", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("3350 Haag", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel: 07434/44398", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Betrifft: Zahlungserinnerung Jahreswasserrechung " + preis.Jahr, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 13 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 15 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sehr geehrte/r Herr/Frau " + kunde.Nachname + "!", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Bitte denken Sie innerhalb der n�chsten 14 Tagen an die Bezahlung unserer", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 20 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Jahreswasserrechnung " + preis.Jahr, new Font("Arial", stdFontSize, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 21 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("f�r das Objekt " + kunde.Objekt + ",", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 250, obererRand + 21 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("mit � " + this.calcJahresRechnungMinusBereitsBezahlt(jdd,kunde,preis) + " zuz�glich Mahnspesen von � 3,-.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 22 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sollte Ihre Bezahlung inzwischen schon unterwegs sein, betrachten Sie dieses ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Schreiben bitte als gegenstandslos. Andernfalls �berweisen Sie bitte den", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Betrag von � " + (this.calcJahresRechnungMinusBereitsBezahlt(jdd, kunde, preis) + 3.0).ToString() + " auf unser Konto Nummer 267 126 769 00, BLZ 20111, Erste Bank.", new Font("Arial", stdFontSize, FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 26 * zeilenabstand));                   

                    ppd.AddPrintableObject(new PrintableTextObject("Mit bestem Dank,", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 30 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Ing. Waltraud Weinberger-Hairas", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 33 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wasserwerk Weinberger", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 34 * zeilenabstand));                    

                    ppd.AddPrintableObject(new PrintableTextObject("Unsere ATU Nummer: ATU 563 88 929", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 40 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bankverbindung: Erste Bank Haag BLZ 20111 Kontonummer 267 126 769 00", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 41 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel/Fax: 07434/44398 oder 0676/620 32 38", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 42 * zeilenabstand));

                    pdd.AddPrintPage(ppd);
                }
            }

            pdd.DoPrint();
        }

        public void PrintMahnung2(IList<KundenData> kunden, PreisData preis) {

            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";

            PrintablePage ppd;
            foreach (KundenData kunde in kunden) {
                ppd = new PrintablePage();
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);
                if (jdd != null) {
                    ppd.AddPrintableObject(new PrintableTextObject("WASSERWERK WEINBERGER", new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bahnhofstra�e 27", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("3350 Haag", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel: 07434/44398", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Betrifft: 2. Zahlungserinnerung Jahreswasserrechung " + preis.Jahr, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 13 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 15 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sehr geehrte/r Herr/Frau " + kunde.Nachname + "!", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Trotz Zahlungserinnerung ist die Zahlung Ihrer Jahreswasserrechnung nicht bei uns", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 20 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("eingegangen.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 21 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Wir ersuchen Sie dringend den Rechnungsbetrag von � " + this.calcJahresRechnungMinusBereitsBezahlt(jdd,kunde,preis) + " zuz�glich", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 23 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Mahnspesen von � 5,-, das sind", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("� " + (this.calcJahresRechnungMinusBereitsBezahlt(jdd,kunde,preis) + 5) + " Mahnbetrag auf unser Konto Nummer", new Font("Arial", stdFontSize, FontStyle.Underline), Brushes.Black, linkerRand + 260, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("267 126 769 00, BLZ 20111, Erste Bank,", new Font("Arial", stdFontSize, FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("zu �berweisen.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 330, obererRand + 25 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sollte der Betrag bis zum", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 27 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.AddDays(14).Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand + 205, obererRand + 27 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("nicht bei uns eingelangt sein, wird Ihr Akt", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 300, obererRand + 27 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("automatisch an unseren Rechtsvertreter weitergeleitet, wodurch f�r Sie mit", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 28 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("erheblichen Mehrkosten zu rechnen ist.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 29 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sollte Ihre Bezahlung inzwischen schon unterwegs sein, betrachten Sie dieses ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 31 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Schreiben bitte als gegenstandslos.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 32 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Mit bestem Dank,", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 35 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Ing. Waltraud Weinberger-Hairas", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 38 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Wasserwerk Weinberger", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 39 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Unsere ATU Nummer: ATU 563 88 929", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 42 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bankverbindung: Erste Bank Haag BLZ 20111 Kontonummer 267 126 769 00", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 43 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Tel/Fax: 07434/44398 oder 0676/620 32 38", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 44 * zeilenabstand));

                    pdd.AddPrintPage(ppd);
                }
            }

            pdd.DoPrint();
        }
        #endregion Print

        #region Tools

        public string FormatDezimal(double d) {
            return String.Format("{0:F}", Math.Round(d,2));
        }

        public double calcJahresRechnungMinusBereitsBezahlt(JahresDatenData jdd, KundenData kunde, PreisData preis) {
            return Math.Round(calcJahresrechnungBrutto(jdd,kunde,preis) - jdd.BereitsBezahlt,2);
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
