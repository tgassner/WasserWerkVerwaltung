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

        public JahresDatenData GetJahresdataByJahresDataId(long jahresDatenId)
        {
            if (allJahresData == null)
            {
                this.GetAllJahresdata();
            }

            foreach (JahresDatenData jdd in this.allJahresData)
            {
                if (jdd.Id == jahresDatenId)
                {
                    return jdd;
                }
            }
            return null;
        }

        public bool UpdateJahresDaten(JahresDatenData jahresDatum) {
            IJahresDaten jahresDataDB = Database.CreateJahresDaten();

            if (jahresDataDB.Update(jahresDatum)) {
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
                            jahresDatum.HalbJahresBetrag,
                            jahresDatum.RechnungsDatumHalbjahr,
                            jahresDatum.RechnungsDatumJahr,
                            jahresDatum.RechnungsNummerHalbjahr,
                            jahresDatum.RechnungsNummerJahr);
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
         * Die Semantik dieses Buttons ist, daß er für ein Jahr alle Kunden 
         * die noch keinen Zählerstandeintrag für das Jahr haben den 
         * Halbjahreszählerstand vom Jahr davor berechnet, (mit Überprüfung, 
         * daß mindestens ein Kunde im Vorjahr einen Eintrag haben muß.) Kunden 
         * die bereits einen Jahreszählerstand eintrag für das JAhr haben werden 
         * ignoriert um einen bestehenden Betrag nicht zu überschreiben. 
         * (Sollte zwar eh der selbe sein, aber du hast ja immer die Möglichkeit 
         * den Betrag anzupassen.) 
         */

        public void GenerateHalbJahresBetragFuerJahr(long jahr) {
            int countKeinenAltenEintrag = 0;
            int countBereitsEinenNeuenEintrag = 0;
            int countEintraegerErstellt = 0;
            int countFehlerBeimEintragen = 0;

            PreisData pd = this.GetPreisDataByJahr(jahr - 1);
            if (pd == null){
                MessageBox.Show("Kein Wasserpreis / m³ fürs Jahr " + (jahr - 1) + " definiert -> bitte zuvor definieren.");
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

                    double halbjahresbetrag = this.calcJahresrechnungBrutto(this.GetJahresdataByKundenIDandYear(kunde.Id, jahr - 1), kunde, pd);
                    halbjahresbetrag = halbjahresbetrag / 2;
                    halbjahresbetrag = Math.Round(halbjahresbetrag, 2);

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
                                                                halbjahresbetrag,
                                                                DateTime.MinValue,
                                                                DateTime.MinValue,
                                                                null,
                                                                null);

                    JahresDatenData jdd2 = this.InsertJahresDaten(jdd);

                    if (jdd2 != null) {
                        countEintraegerErstellt++;
                    } else {
                        countFehlerBeimEintragen++;
                    }
                }
                
            }
            MessageBox.Show(countEintraegerErstellt + " Einträge erstellt für das Jahr " + jahr + " auf Basis vom Jahr: " + (jahr - 1) + "\r\n" + countKeinenAltenEintrag + " hatten keinen Eintrag im Jahr " + (jahr - 1) + "\r\n" + countBereitsEinenNeuenEintrag + " hatten bereits einen Eintrag im Jahr " + jahr + "\r\n" + countFehlerBeimEintragen + " Einträge konnten nicht erstellt werden.");
        }

        public bool SetRechnungsDatumHalbjahr(KundenData kunde, long jahr, DateTime datum){
            JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, jahr);
            jdd.RechnungsDatumHalbjahr = datum;
            return this.UpdateJahresDaten(jdd);
        }

        public bool SetRechnungsDatumJahr(KundenData kunde, long jahr, DateTime datum) {
            JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, jahr);
            jdd.RechnungsDatumJahr = datum;
            return this.UpdateJahresDaten(jdd);
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

        const string BANK_VERBINDUNG = "Bankverbindung: Sparkasse OÖ, AT36 2032 0261 0000 3132, BIC: ASPKAT2L";
        const string ATU_NUMMER = "Unsere ATU Nummer: ATU 563 88 929";
        const string TEL = "Tel: 07434/44398";
        const string TEL_FAX_MOBIL = "Tel/Fax: 07434/44398 oder 0676/620 32 38";
        const string WASSERWERK_WEINBERGER = "WASSERWERK WEINBERGER";
        const string BAHNHOFSTRASSE_27 = "Bahnhofstraße 27";
        const string _3350_STADT_HAAG = "3350 Haag";
        
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
                    if (jdd.RechnungsNummerJahr == null)
                    {
                        jdd.RechnungsNummerJahr = setGanzJahresRechnungsNummer(jdd.Id);
                    }
                    ppd.AddPrintableObject(new PrintableTextObject(WASSERWERK_WEINBERGER, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(BAHNHOFSTRASSE_27, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(_3350_STADT_HAAG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(TEL, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("RechnungsNr: " + jdd.getFullRechnungsNummerGanzJahr(), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 437, obererRand + 9.1F * zeilenabstand));

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
                        ppd.AddPrintableObject(new PrintableTextObject("EUR -" + FormatDezimal(jdd.BereitsBezahlt), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 29 * zeilenabstand));
                    }

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 31 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 31 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject("Einzuzahlender Betrag = ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 31 * zeilenabstand));
                    if (kunde.BekommtRechnung == Rechnung.Halbjahres) {
                        ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(this.calcJahresRechnungMinusBereitsBezahlt(jdd,kunde,preis)), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 31 * zeilenabstand));
                    } 
                    if (kunde.BekommtRechnung == Rechnung.Jahres) {
                        ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal((this.calcJahresrechnungBrutto(jdd, kunde, preis))), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittlererRand, obererRand + 31 * zeilenabstand));
                    }

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 32 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 32 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Zahlung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 33 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(ATU_NUMMER, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 37 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(BANK_VERBINDUNG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 38 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(TEL_FAX_MOBIL, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 39 * zeilenabstand));
                    
                    pdd.AddPrintPage(ppd);
                }
                this.SetRechnungsDatumJahr(kunde, preis.Jahr, DateTime.Now);
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
                    if (jdd.RechnungsNummerHalbjahr == null)
                    {
                        jdd.RechnungsNummerHalbjahr = setHalbJahresRechnungsNummer(jdd.Id);
                    }
                    ppd.AddPrintableObject(new PrintableTextObject(WASSERWERK_WEINBERGER, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(BAHNHOFSTRASSE_27, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(_3350_STADT_HAAG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(TEL, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("RechnungsNr: " + jdd.getFullRechnungsNummerHalbJahr(), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 423, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Halbjahreswasserrechnung " + preis.Jahr, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand + 180, obererRand + 13 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 15 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("für das Objekt:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Objekt, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 17 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Die Rechnung ist eine Akontierung und entspricht", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 18 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("ca. der Hälfte der Vorjahresrechnung.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 19 * zeilenabstand));
                    

                    ppd.AddPrintableObject(new PrintableTextObject("Nettobetrag =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 22 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(jdd.HalbJahresBetrag / 1.1) + " + " + FormatDezimal(jdd.HalbJahresBetrag * (0.1/1.1)) + " (10% Mwst) = EUR " + FormatDezimal(jdd.HalbJahresBetrag), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 22 * zeilenabstand));
                    
                    
                    ppd.AddPrintableObject(new PrintableTextObject("Einzuzahlender Betrag =", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("EUR " + FormatDezimal(jdd.HalbJahresBetrag), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 26 * zeilenabstand), (int)linkerRand + 680, (int)(obererRand + 26 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Zahlung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 29 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(ATU_NUMMER, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 35 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(BANK_VERBINDUNG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 36 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(TEL_FAX_MOBIL, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 37 * zeilenabstand));

                    pdd.AddPrintPage(ppd);
                }
                this.SetRechnungsDatumHalbjahr(kunde, preis.Jahr, DateTime.Now);
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
            ppd.AddPrintableObject(new PrintableTextObject("Noch ausständig", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 610, obererRand + 15));

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
                    ppd.AddPrintableObject(new PrintableTextObject("Noch ausständig", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 610, obererRand + 15));

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

        public void PrintZaehlerstandabrechnungsFormular(IList<KundenData> selectedKundenList, PreisData preis, string textZaehlerstandformular, DateTime von, DateTime bis) {
            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";

            PrintablePage ppd;
            foreach (KundenData kunde in selectedKundenList) {
                ppd = new PrintablePage();
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);
                if (jdd != null) {

                    ppd.AddPrintableObject(new PrintableTextObject(WASSERWERK_WEINBERGER, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand - 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(BAHNHOFSTRASSE_27, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand - 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(_3350_STADT_HAAG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(TEL, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 4 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 5 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Ablesung des Zählerstandes " + preis.Jahr, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 11 * zeilenabstand), (int)(linkerRand + 650), (int)(obererRand + 11 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject("Sehr geehrte/r Herr/Frau " + kunde.Nachname + "!", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 12 * zeilenabstand));

                    string[] lines = textZaehlerstandformular.Split(new string[] {"\r\n"}, StringSplitOptions.None);
                    for(int i = 0; i < lines.Length; i++) {
                        ppd.AddPrintableObject(new PrintableTextObject(lines[i], new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + (14 + i) * zeilenabstand));
                    }

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + 23 * zeilenabstand), (int)(linkerRand + 650), (int)(obererRand + 23 * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableTextObject("An das", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(WASSERWERK_WEINBERGER, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(BAHNHOFSTRASSE_27, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 26 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(_3350_STADT_HAAG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 27 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Abschnitt in den Briefschlitz", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, linkerRand + 400, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Bahnhofstraße 27 werfen, oder", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, linkerRand + 400, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Postgebühr beim Empfänger einheben.", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, linkerRand + 400, obererRand + 26 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Ablesezeitraum: " + von.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo) + " bis " + bis.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo) + "!!!!", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 30 * zeilenabstand));


                    ppd.AddPrintableObject(new PrintableTextObject("Name:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 31 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 160, obererRand + 31 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Straße:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 32.5F * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 160, obererRand + 32.5F * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("PLZ, Ort:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 34 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 160, obererRand + 34 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Objekt:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 35.5F * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Objekt, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 160, obererRand + 35.5F * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Telefon/Handy*:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 37 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Tel, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 160, obererRand + 37 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Personen wohnhaft im", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 38.5F * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Objekt (inkl. Kinder):", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 39.2F * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.PersonenImObjekt.ToString(), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 160, obererRand + 38.5F * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Zähler-Nummer.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 41.5F * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.ZaehlerNummer, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 160, obererRand + 41.5F * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Zähler-Stand:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 43 * zeilenabstand));
                    
                    ppd.AddPrintableObject(new PrintableTextObject("Ablesedatum:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 44.5F * zeilenabstand));
                
                    ppd.AddPrintableObject(new PrintableTextObject("Unterschrift:", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 350, obererRand + 44.5F * zeilenabstand));
                    
                    ppd.AddPrintableObject(new PrintableTextObject("* Bitte Telefon- oder Handy-Nummer für den Fall eines Leitungsgebrechens angeben.", new Font("Arial", 10, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 46 * zeilenabstand));

                    for (int i = 0; i < 11; i++) {
                        ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand, (int)(obererRand + (31 + i * 1.5) * zeilenabstand), (int)(linkerRand + 650), (int)(obererRand + (31 + i * 1.5) * zeilenabstand)));
                    }


                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand), (int)(obererRand + (31) * zeilenabstand), (int)(linkerRand), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 158), (int)(obererRand + (31) * zeilenabstand), (int)(linkerRand + 158), (int)(obererRand + (31 + 6 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 403), (int)(obererRand + (31) * zeilenabstand), (int)(linkerRand + 403), (int)(obererRand + (31 + 6 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 650), (int)(obererRand + (31) * zeilenabstand), (int)(linkerRand + 650), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 158), (int)(obererRand + (31 + 7 * 1.5) * zeilenabstand), (int)(linkerRand + 158), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 403), (int)(obererRand + (31 + 7 * 1.5) * zeilenabstand), (int)(linkerRand + 403), (int)(obererRand + (31 + 8 * 1.5) * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 348), (int)(obererRand + (31 + 9 * 1.5) * zeilenabstand), (int)(linkerRand + 348), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 452), (int)(obererRand + (31 + 9 * 1.5) * zeilenabstand), (int)(linkerRand + 452), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));

                    //Fetter
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 349), (int)(obererRand + (31 + 9 * 1.5) * zeilenabstand), (int)(linkerRand + 349), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 453), (int)(obererRand + (31 + 9 * 1.5) * zeilenabstand), (int)(linkerRand + 453), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 347), (int)(obererRand + (31 + 9 * 1.5) * zeilenabstand), (int)(linkerRand + 347), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 451), (int)(obererRand + (31 + 9 * 1.5) * zeilenabstand), (int)(linkerRand + 451), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 651), (int)(obererRand + (31 + 8 * 1.5) * zeilenabstand), (int)(linkerRand + 651), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 157), (int)(obererRand + (31 + 8 * 1.5) * zeilenabstand), (int)(linkerRand + 157), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 649), (int)(obererRand + (31 + 8 * 1.5) * zeilenabstand), (int)(linkerRand + 649), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 159), (int)(obererRand + (31 + 8 * 1.5) * zeilenabstand), (int)(linkerRand + 159), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand)));


                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 158), (int)(obererRand + (31 + 8 * 1.5) * zeilenabstand) - 1, (int)(linkerRand + 650), (int)(obererRand + (31 + 8 * 1.5) * zeilenabstand) - 1));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 158), (int)(obererRand + (31 + 9 * 1.5) * zeilenabstand) - 1, (int)(linkerRand + 650), (int)(obererRand + (31 + 9 * 1.5) * zeilenabstand) - 1));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 158), (int)(obererRand + (31 + 8 * 1.5) * zeilenabstand) + 1, (int)(linkerRand + 650), (int)(obererRand + (31 + 8 * 1.5) * zeilenabstand) + 1));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 158), (int)(obererRand + (31 + 9 * 1.5) * zeilenabstand) + 1, (int)(linkerRand + 650), (int)(obererRand + (31 + 9 * 1.5) * zeilenabstand) + 1));

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 158), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand) - 1, (int)(linkerRand + 350), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand) - 1));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 452), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand) - 1, (int)(linkerRand + 650), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand) - 1));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 158), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand) + 1, (int)(linkerRand + 350), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand) + 1));
                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)(linkerRand + 452), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand) + 1, (int)(linkerRand + 650), (int)(obererRand + (31 + 10 * 1.5) * zeilenabstand) + 1));

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
                    ppd.AddPrintableObject(new PrintableTextObject(WASSERWERK_WEINBERGER, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(BAHNHOFSTRASSE_27, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(_3350_STADT_HAAG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(TEL, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Betrifft: Zahlungserinnerung Jahreswasserrechung " + preis.Jahr, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 13 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 15 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sehr geehrte/r Herr/Frau " + kunde.Nachname + "!", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Bitte denken Sie innerhalb der nächsten 14 Tagen an die Bezahlung unserer", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 20 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Jahreswasserrechnung " + preis.Jahr, new Font("Arial", stdFontSize, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 21 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("für das Objekt " + kunde.Objekt + ",", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 250, obererRand + 21 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("mit € " + this.calcJahresRechnungMinusBereitsBezahlt(jdd,kunde,preis) + " zuzüglich Mahnspesen von € 3,-.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 22 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sollte Ihre Bezahlung inzwischen schon unterwegs sein, betrachten Sie dieses ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Schreiben bitte als gegenstandslos. Andernfalls überweisen Sie bitte den", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Betrag von € " + (this.calcJahresRechnungMinusBereitsBezahlt(jdd, kunde, preis) + 3.0).ToString() + " auf unser Konto Nummer 267 126 769 00, BLZ 20111, Erste Bank.", new Font("Arial", stdFontSize, FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 26 * zeilenabstand));                   

                    ppd.AddPrintableObject(new PrintableTextObject("Mit bestem Dank,", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 30 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Ing. Waltraud Weinberger-Hairas", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 33 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(WASSERWERK_WEINBERGER, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 34 * zeilenabstand));                    

                    ppd.AddPrintableObject(new PrintableTextObject(ATU_NUMMER, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 40 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(BANK_VERBINDUNG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 41 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(TEL_FAX_MOBIL, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 42 * zeilenabstand));

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
                    ppd.AddPrintableObject(new PrintableTextObject(WASSERWERK_WEINBERGER, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(BAHNHOFSTRASSE_27, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(_3350_STADT_HAAG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(TEL, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Herr/Frau/Fa", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname + " " + kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 7 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Betrifft: 2. Zahlungserinnerung Jahreswasserrechung " + preis.Jahr, new Font("Arial", 16, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 13 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 15 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sehr geehrte/r Herr/Frau " + kunde.Nachname + "!", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Trotz Zahlungserinnerung ist die Zahlung Ihrer Jahreswasserrechnung nicht bei uns", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 20 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("eingegangen.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 21 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Wir ersuchen Sie dringend den Rechnungsbetrag von € " + this.calcJahresRechnungMinusBereitsBezahlt(jdd,kunde,preis) + " zuzüglich", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 23 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Mahnspesen von € 5,-, das sind", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("€ " + (this.calcJahresRechnungMinusBereitsBezahlt(jdd,kunde,preis) + 5) + " Mahnbetrag auf unser Konto Nummer", new Font("Arial", stdFontSize, FontStyle.Underline), Brushes.Black, linkerRand + 260, obererRand + 24 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("267 126 769 00, BLZ 20111, Erste Bank,", new Font("Arial", stdFontSize, FontStyle.Underline), Brushes.Black, linkerRand, obererRand + 25 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("zu überweisen.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 330, obererRand + 25 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sollte der Betrag bis zum", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 27 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.AddDays(14).Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Bold | FontStyle.Underline), Brushes.Black, linkerRand + 205, obererRand + 27 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("nicht bei uns eingelangt sein, wird Ihr Akt", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 300, obererRand + 27 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("automatisch an unseren Rechtsvertreter weitergeleitet, wodurch für Sie mit", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 28 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("erheblichen Mehrkosten zu rechnen ist.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 29 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Sollte Ihre Bezahlung inzwischen schon unterwegs sein, betrachten Sie dieses ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 31 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Schreiben bitte als gegenstandslos.", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 32 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject("Mit bestem Dank,", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 35 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject("Ing. Waltraud Weinberger-Hairas", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 38 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(WASSERWERK_WEINBERGER, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 39 * zeilenabstand));

                    ppd.AddPrintableObject(new PrintableTextObject(ATU_NUMMER, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 42 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(BANK_VERBINDUNG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 43 * zeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(TEL_FAX_MOBIL, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 44 * zeilenabstand));

                    pdd.AddPrintPage(ppd);
                }
            }

            pdd.DoPrint();
        }

        public void PrintJahresrechnungsdatumUebersicht(IList<KundenData> kunden, PreisData preis) {
            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung";
            int seitencounter = 1;
            PrintablePage ppd = new PrintablePage();
            ppd.AddPrintableObject(new PrintableTextObject("Seite " + seitencounter, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand - 30, obererRand - 30));
            ppd.AddPrintableObject(new PrintableTextObject("Rechnungsdaten  " + preis.Jahr, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, mittelinkerRand - 20, obererRand - 30));
            ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.ToShortDateString(), new Font("Arial", 9, FontStyle.Bold), Brushes.Black, rechterRand, obererRand - 30));
            ppd.AddPrintableObject(new PrintableTextObject("Name", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand - 30, obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("Vorname", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 60, obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("Objekt", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 160, obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("Re-datum", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 300, obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("AKonto 1/2", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 380, obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("ReSum net.", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 460, obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("10 % Mwst", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 540, obererRand + 15));
            ppd.AddPrintableObject(new PrintableTextObject("ReSum brut.", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 620, obererRand + 15));


            int kleinerZeilenabstand = 15;
            int zeile = 8;
            int counter = 0;
            ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand - 30, (int)kleinerZeilenabstand * (int)(zeile), (int)linkerRand + 700, (int)kleinerZeilenabstand * (int)(zeile)));
            double sumRechnungsSummeNetto = 0;
            double sum10ProzMwst = 0;
            double sumReSumBrutto = 0;
            double sumAKonto = 0;

            foreach (KundenData kunde in kunden) {
                JahresDatenData jdd = this.GetJahresdataByKundenIDandYear(kunde.Id, preis.Jahr);

                bool rechnung;
                if (jdd.RechnungsDatumJahr.Day == 1 && jdd.RechnungsDatumJahr.Month == 1 && jdd.RechnungsDatumJahr.Year == 1901) {
                    rechnung = false;
                } else {
                    rechnung = true;
                }

                if (!rechnung) {
                    ppd.AddPrintableObject(new PrintableFillRectangleObject(Brushes.LightGray, (int)linkerRand - 30, zeile * kleinerZeilenabstand + 1, 329, 13));
                }

                ppd.AddPrintableObject(new PrintableTextObject(kunde.Nachname, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand - 30, zeile * kleinerZeilenabstand));
                 ppd.AddPrintableObject(new PrintableFillRectangleObject(Brushes.White,(int)linkerRand + 59,zeile * kleinerZeilenabstand + 1,300,13));
                
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 60, zeile * kleinerZeilenabstand));
                ppd.AddPrintableObject(new PrintableFillRectangleObject(Brushes.White, (int)linkerRand + 159, zeile * kleinerZeilenabstand + 1, 300, 13));

                ppd.AddPrintableObject(new PrintableTextObject(kunde.Objekt, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 160, zeile * kleinerZeilenabstand));
                ppd.AddPrintableObject(new PrintableFillRectangleObject(Brushes.White, (int)linkerRand + 299, zeile * kleinerZeilenabstand + 1, 300, 13));

                if (rechnung) {
                    ppd.AddPrintableObject(new PrintableTextObject(jdd.RechnungsDatumJahr.ToShortDateString(), new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 300, zeile * kleinerZeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(jdd.HalbJahresBetrag) + "EUR", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 380, zeile * kleinerZeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(this.calcJahresrechnungNetto(jdd, kunde, preis)) + "EUR", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 460, zeile * kleinerZeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(this.calcMwSt(jdd, kunde, preis)) + "EUR", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 540, zeile * kleinerZeilenabstand));
                    ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(this.calcJahresrechnungBrutto(jdd, kunde, preis)) + "EUR", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 620, zeile * kleinerZeilenabstand));
                }
                
                ppd.AddPrintableObject(new PrintableLineObject(Pens.LightGray, (int)linkerRand - 30, (int)kleinerZeilenabstand * (int)(zeile + 1), (int)linkerRand + 700, (int)kleinerZeilenabstand * (int)(zeile + 1)));
                if (rechnung) {
                    sumRechnungsSummeNetto += this.calcJahresrechnungNetto(jdd, kunde, preis);
                    sum10ProzMwst += this.calcMwSt(jdd, kunde, preis);
                    sumReSumBrutto += this.calcJahresrechnungBrutto(jdd, kunde, preis);
                    sumAKonto += jdd.HalbJahresBetrag;
                }

                zeile++;
                counter++;

                if ((counter % 65) == 0) {
                    pdd.AddPrintPage(ppd);
                    ppd = new PrintablePage();
                    zeile = 8;
                    seitencounter++;

                    ppd.AddPrintableObject(new PrintableTextObject("Seite " + seitencounter, new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand - 30, obererRand - 30));
                    ppd.AddPrintableObject(new PrintableTextObject("Rechnungsdaten  " + preis.Jahr, new Font("Arial", 12, FontStyle.Bold), Brushes.Black, mittelinkerRand - 20, obererRand - 30));
                    ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.ToShortDateString(), new Font("Arial", 9, FontStyle.Bold), Brushes.Black, rechterRand, obererRand - 30));
                    ppd.AddPrintableObject(new PrintableTextObject("Name", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand - 30, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("Vorname", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 60, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("Objekt", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 160, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("Re-datum", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 300, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("AKonto 1/2", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 380, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("ReSum net.", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 460, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("10 % Mwst", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 540, obererRand + 15));
                    ppd.AddPrintableObject(new PrintableTextObject("ReSum brut.", new Font("Arial", 9, FontStyle.Bold), Brushes.Black, linkerRand + 620, obererRand + 15));
                    

                    ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand - 30, (int)kleinerZeilenabstand * (int)(zeile), (int)linkerRand + 700, (int)kleinerZeilenabstand * (int)(zeile)));
                }
            }

            ppd.AddPrintableObject(new PrintableLineObject(Pens.Black, (int)linkerRand - 30, (int)kleinerZeilenabstand * (int)(zeile), (int)linkerRand + 700, (int)kleinerZeilenabstand * (int)(zeile)));

            ppd.AddPrintableObject(new PrintableTextObject("Summe", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand - 30, zeile * kleinerZeilenabstand));
            ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(sumAKonto) + "EUR", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 380, zeile * kleinerZeilenabstand));
            ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(sumRechnungsSummeNetto) + "EUR", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 460, zeile * kleinerZeilenabstand));
            ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(sum10ProzMwst)+"EUR", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 540, zeile * kleinerZeilenabstand));
            ppd.AddPrintableObject(new PrintableTextObject(this.FormatDezimal(sumReSumBrutto)+"EUR", new Font("Arial", 8, FontStyle.Bold), Brushes.Black, linkerRand + 620, zeile * kleinerZeilenabstand));
            

            if ((counter % 65) != 0) {
                pdd.AddPrintPage(ppd);
            }
            pdd.DoPrint();
        }

        public void PrintHalbjahresrechnungsdatumUebersicht(IList<KundenData> kunden, PreisData preis) {
            MessageBox.Show("Noch nicht implementiert!");
        }

        public void PrintStammdaten(IList<KundenData> kunden) {
            PrintableDocument pdd = new PrintableDocument();
            pdd.DocumentName = "Wasser Werk Verwaltung Stammdaten";

            PrintablePage ppd;
            foreach (KundenData kunde in kunden) {
                ppd = new PrintablePage();
                
                ppd.AddPrintableObject(new PrintableTextObject(WASSERWERK_WEINBERGER, new Font("Arial", 15, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 0 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(BAHNHOFSTRASSE_27, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 1 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(_3350_STADT_HAAG, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 2 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(TEL, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 3 * zeilenabstand));

                
                ppd.AddPrintableObject(new PrintableTextObject(DateTime.Now.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand + 575, obererRand + 0 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Kundenstammdatenblatt ", new Font("Arial", stdFontSize + 1, FontStyle.Bold), Brushes.Black, linkerRand, obererRand + 6 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Kundennummer: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 8 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Id.ToString(), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 8 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Vorname: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 9 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Vorname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 9 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Nachname: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 10 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Nachname, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 10 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Strasse: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 11 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Strasse, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 11 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Ort: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 12 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Ort, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 12 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Objekt: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 13 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Objekt, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 13 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Tel: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 14 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Tel, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 14 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Hausbesitzer: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 15 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Hausbesitzer, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 15 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Bankverbindung: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 16 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.BankVerbindung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 16 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Eichdatum: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 17 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.EichDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 17 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Zählernummer: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 18 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.ZaehlerNummer, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 18 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Einbaudatum: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 19 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.EinbauDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 19 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Erklährung: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 20 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Erkl, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 20 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Tauschdatum: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 21 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.TauschDatum.Date.ToString("dd.MM.yyyy", DateTimeFormatInfo.InvariantInfo), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 21 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Zählermiete: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 22 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Zaehlermiete.ToString(), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 22 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Bemerkung: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 23 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Bemerkung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 23 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Zahlung: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 24 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Zahlung, new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 24 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Leitungskreis: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 25 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.Leitungskreis.ToString(), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 25 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Personen im Objekt: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 26 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.PersonenImObjekt.ToString(), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 26 * zeilenabstand));

                ppd.AddPrintableObject(new PrintableTextObject("Rechnung: ", new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, linkerRand, obererRand + 27 * zeilenabstand));
                ppd.AddPrintableObject(new PrintableTextObject(kunde.BekommtRechnung.ToString(), new Font("Arial", stdFontSize, FontStyle.Regular), Brushes.Black, mittelinkerRand, obererRand + 27 * zeilenabstand));


                pdd.AddPrintPage(ppd);
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

        public bool doDbBackup(string backupFileName)
        {
            return DbTools.DoDbBackup(backupFileName);
        }

        public bool doDbImport(string fileName)
        {
            return DbTools.DoDbImport(fileName);
        }

        public long setHalbJahresRechnungsNummer(long jahresDatenId)
        {
            JahresDatenData jdd = GetJahresdataByJahresDataId(jahresDatenId);
            if (jdd.RechnungsNummerHalbjahr == null) {
                long rechnungsHalbJahresNummer = getNewHalbJahresRechnungsNummer(jdd.Jahr);
                jdd.RechnungsNummerHalbjahr = rechnungsHalbJahresNummer;
                UpdateJahresDaten(jdd);
                return rechnungsHalbJahresNummer;
            } else {
                return (long)jdd.RechnungsNummerHalbjahr;
            }
        }

        public long setGanzJahresRechnungsNummer(long jahresDatenId)
        {
            JahresDatenData jdd = GetJahresdataByJahresDataId(jahresDatenId);
            if (jdd.RechnungsNummerJahr == null)
            {
                long rechnungsGanzJahresNummer = getNewGanzJahresRechnungsNummer(jdd.Jahr);
                jdd.RechnungsNummerJahr = rechnungsGanzJahresNummer;
                UpdateJahresDaten(jdd);
                return rechnungsGanzJahresNummer;
            }
            else {
                return (long)jdd.RechnungsNummerJahr;
            }
        }

        private long getNewHalbJahresRechnungsNummer(long jahr) {
            long max = 0;
            foreach (JahresDatenData jdd in GetAllJahresdata()) {
                if (jdd.Jahr == jahr && jdd.RechnungsNummerHalbjahr != null) {
                    max = Math.Max(max, (long)jdd.RechnungsNummerHalbjahr);
                }
            }
            return max + 1;
        }

        private long getNewGanzJahresRechnungsNummer(long jahr)
        {
            long max = 0;
            foreach (JahresDatenData jdd in GetAllJahresdata())
            {
                if (jdd.Jahr == jahr && jdd.RechnungsNummerJahr != null)
                {
                    max = Math.Max(max, (long)jdd.RechnungsNummerJahr);
                }
            }
            return max + 1;
        }

        #endregion Tools
    }
}
