using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WasserWerkVerwaltung.CommonObjects;
using WasserWerkVerwaltung.DAL;


namespace SimplePatientDocumentation.DAL.Tests {

    [TestFixture]
    public class KundeTest {

        private long kID;
        private KundenData kunde;

        [SetUp]
        public void InitVisitTest() {
            IKunde kundeDB = Database.CreateKunde();
            kunde = new KundenData(0, "vorname", "Nachname", "Strasse", "ort", "objekt", "tel", "hausbesetzer", "Datenbank", Rechnung.Keine, DateTime.Now, "zNr", DateTime.Now, "erkl", DateTime.Now, 234.1, "Bemerkung", "asdf",11);
            kID = kundeDB.Insert(kunde);
        }

        [Test]
        public void KundeFindAllTest() {
            IKunde kundeDB = Database.CreateKunde();
            IList<KundenData> kundenList = kundeDB.FindAll();
            foreach (KundenData kundefoeach in kundenList) {
                if (kundefoeach.Id == this.kID) {
                    Assert.AreEqual(kundefoeach.BankVerbindung, this.kunde.BankVerbindung);
                    Assert.AreEqual(kundefoeach.BekommtRechnung, this.kunde.BekommtRechnung);
                    Assert.AreEqual(kundefoeach.Bemerkung, this.kunde.Bemerkung);
                    Assert.AreEqual(kundefoeach.EichDatum.Date, this.kunde.EichDatum.Date);
                    Assert.AreEqual(kundefoeach.EinbauDatum.Date, this.kunde.EinbauDatum.Date);
                    Assert.AreEqual(kundefoeach.Erkl, this.kunde.Erkl);
                    Assert.AreEqual(kundefoeach.Hausbesitzer, this.kunde.Hausbesitzer);
                    Assert.AreEqual(kundefoeach.Nachname, this.kunde.Nachname);
                    Assert.AreEqual(kundefoeach.Ort, this.kunde.Ort);
                    Assert.AreEqual(kundefoeach.Objekt, this.kunde.Objekt);
                    Assert.AreEqual(kundefoeach.Strasse, this.kunde.Strasse);
                    Assert.AreEqual(kundefoeach.TauschDatum.Date, this.kunde.TauschDatum.Date);
                    Assert.AreEqual(kundefoeach.Tel, this.kunde.Tel);
                    Assert.AreEqual(kundefoeach.Vorname, this.kunde.Vorname);
                    Assert.AreEqual(kundefoeach.Zaehlermiete, this.kunde.Zaehlermiete);
                    Assert.AreEqual(kundefoeach.ZaehlerNummer, this.kunde.ZaehlerNummer);
                    Assert.AreEqual(kundefoeach.Zahlung, this.kunde.Zahlung);
                    Assert.AreEqual(kundefoeach.Leitungskreis, this.kunde.Leitungskreis);
                }
            }
        }

        [Test]
        public void KundeFindByIdTest() {
            IKunde kundeDB = Database.CreateKunde();
            KundenData kunde2 = kundeDB.FindByID(this.kID);
            Assert.AreEqual(kunde2.Id, this.kID);
            Assert.AreEqual(kunde2.BankVerbindung, this.kunde.BankVerbindung);
            Assert.AreEqual(kunde2.BekommtRechnung, this.kunde.BekommtRechnung);
            Assert.AreEqual(kunde2.Bemerkung, this.kunde.Bemerkung);
            Assert.AreEqual(kunde2.EichDatum.Date, this.kunde.EichDatum.Date);
            Assert.AreEqual(kunde2.EinbauDatum.Date, this.kunde.EinbauDatum.Date);
            Assert.AreEqual(kunde2.Erkl, this.kunde.Erkl);
            Assert.AreEqual(kunde2.Hausbesitzer, this.kunde.Hausbesitzer);
            Assert.AreEqual(kunde2.Nachname, this.kunde.Nachname);
            Assert.AreEqual(kunde2.Ort, this.kunde.Ort);
            Assert.AreEqual(kunde2.Objekt, this.kunde.Objekt);
            Assert.AreEqual(kunde2.Strasse, this.kunde.Strasse);
            Assert.AreEqual(kunde2.TauschDatum.Date, this.kunde.TauschDatum.Date);
            Assert.AreEqual(kunde2.Tel, this.kunde.Tel);
            Assert.AreEqual(kunde2.Vorname, this.kunde.Vorname);
            Assert.AreEqual(kunde2.Zaehlermiete, this.kunde.Zaehlermiete);
            Assert.AreEqual(kunde2.ZaehlerNummer, this.kunde.ZaehlerNummer);
            Assert.AreEqual(kunde2.Zahlung, this.kunde.Zahlung);
            Assert.AreEqual(kunde2.Leitungskreis, this.kunde.Leitungskreis);
        }

        [Test]
        public void KundeUpdateTest() {
            IKunde kundeDB = Database.CreateKunde();
            KundenData kunde2 = kundeDB.FindByID(this.kID);
            kunde2.BankVerbindung = kunde.BankVerbindung = "bank2";
            kunde2.BekommtRechnung = kunde.BekommtRechnung = Rechnung.Jahres;
            kunde2.Bemerkung = kunde.Bemerkung = "Bemerkung2";
            kunde2.EichDatum = kunde.EichDatum = DateTime.Now;
            kunde2.EinbauDatum = kunde.EinbauDatum = DateTime.Now;
            kunde2.Erkl = kunde.Erkl = "Erkl2";
            kunde2.Hausbesitzer = kunde.Hausbesitzer = "Hausbesitzer2";
            kunde2.Nachname = kunde.Nachname = "Nachnane2";
            kunde2.Ort = kunde.Ort = "Ort2";
            kunde2.Objekt = kunde.Objekt = "Objekt2";
            kunde2.Strasse = kunde.Strasse = "Strasse2";
            kunde2.TauschDatum = kunde.TauschDatum = DateTime.Now;
            kunde2.Tel = kunde.Tel = "Tel2";
            kunde2.Vorname = kunde.Vorname = "Vorname2";
            kunde2.Zaehlermiete = kunde.Zaehlermiete = 2.09876543;
            kunde2.ZaehlerNummer = kunde.ZaehlerNummer = "ZNo2";
            kunde2.Zahlung = kunde.Zahlung = "Zahlung2";
            kunde2.Leitungskreis = kunde.Leitungskreis = 22;

            Assert.IsTrue(kundeDB.Update(kunde2));
            KundenData kunde3 = kundeDB.FindByID(kID);

            Assert.AreEqual(kunde3.Id, this.kID);
            Assert.AreEqual(kunde3.BankVerbindung, this.kunde.BankVerbindung);
            Assert.AreEqual(kunde3.BekommtRechnung, this.kunde.BekommtRechnung);
            Assert.AreEqual(kunde3.Bemerkung, this.kunde.Bemerkung);
            Assert.AreEqual(kunde3.EichDatum.Date, this.kunde.EichDatum.Date);
            Assert.AreEqual(kunde3.EinbauDatum.Date, this.kunde.EinbauDatum.Date);
            Assert.AreEqual(kunde3.Erkl, this.kunde.Erkl);
            Assert.AreEqual(kunde3.Hausbesitzer, this.kunde.Hausbesitzer);
            Assert.AreEqual(kunde3.Nachname, this.kunde.Nachname);
            Assert.AreEqual(kunde3.Ort, this.kunde.Ort);
            Assert.AreEqual(kunde3.Objekt, this.kunde.Objekt);
            Assert.AreEqual(kunde3.Strasse, this.kunde.Strasse);
            Assert.AreEqual(kunde3.TauschDatum.Date, this.kunde.TauschDatum.Date);
            Assert.AreEqual(kunde3.Tel, this.kunde.Tel);
            Assert.AreEqual(kunde3.Vorname, this.kunde.Vorname);
            Assert.AreEqual(kunde3.Zaehlermiete, this.kunde.Zaehlermiete);
            Assert.AreEqual(kunde3.ZaehlerNummer, this.kunde.ZaehlerNummer);
            Assert.AreEqual(kunde3.Zahlung, this.kunde.Zahlung);
            Assert.AreEqual(kunde3.Leitungskreis, this.kunde.Leitungskreis);
        }

        [Test]
        public void KundeDeleteTest() {
            IKunde kundeDB = Database.CreateKunde();
            Assert.IsNotNull(kundeDB.FindByID(this.kID));
            Assert.IsTrue(kundeDB.Delete(this.kID));
            Assert.IsNull(kundeDB.FindByID(this.kID));
        }
    }

    [TestFixture]
    public class JahresdatenTest {

        private long jdID;
        private JahresDatenData jahresdaten;


        [SetUp]
        public void InitVisitTest() {
            IJahresDaten jahresDatenDB = Database.CreateJahresDaten();
            jahresdaten = new JahresDatenData(0, 1, 23, 234, 2007, DateTime.Now, 87.0,5,8,"asdf",456.7,2345.9);
            this.jdID = jahresDatenDB.Insert(jahresdaten);
        }   

        [Test]
        public void JahresdatenFindAllTest() {
            IJahresDaten jahredDatenDB = Database.CreateJahresDaten();
            IList<JahresDatenData> jahresdataList = jahredDatenDB.FindAll();
            foreach (JahresDatenData jahrfoeach in jahresdataList) {
                if (jahrfoeach.Id == this.jdID) {
                    Assert.AreEqual(jahrfoeach.AbleseDatum.Date, this.jahresdaten.AbleseDatum.Date);
                    Assert.AreEqual(jahrfoeach.BereitsBezahlt, this.jahresdaten.BereitsBezahlt);
                    Assert.AreEqual(jahrfoeach.Jahr, this.jahresdaten.Jahr);
                    Assert.AreEqual(jahrfoeach.KundenId, this.jahresdaten.KundenId);
                    Assert.AreEqual(jahrfoeach.ZaehlerStandAlt, this.jahresdaten.ZaehlerStandAlt);
                    Assert.AreEqual(jahrfoeach.ZaehlerStandNeu, this.jahresdaten.ZaehlerStandNeu);
                    Assert.AreEqual(jahrfoeach.TauschZaehlerStandAlt, this.jahresdaten.TauschZaehlerStandAlt);
                    Assert.AreEqual(jahrfoeach.TauschZaehlerStandNeu, this.jahresdaten.TauschZaehlerStandNeu);
                    Assert.AreEqual(jahrfoeach.SonstigeForderungenText, this.jahresdaten.SonstigeForderungenText);
                    Assert.AreEqual(jahrfoeach.SonstigeForderungenValue, this.jahresdaten.SonstigeForderungenValue);
                    Assert.AreEqual(jahrfoeach.HalbjahresZahlung, this.jahresdaten.HalbjahresZahlung);
                }
            }
        }

        [Test]
        public void JahresdatenFindByIdTest() {
            IJahresDaten jahredDatenDB = Database.CreateJahresDaten();
            JahresDatenData jahredData2 = jahredDatenDB.FindByID(this.jdID);
            Assert.AreEqual(jahredData2.Id, this.jdID);
            Assert.AreEqual(jahredData2.AbleseDatum.Date, this.jahresdaten.AbleseDatum.Date);
            Assert.AreEqual(jahredData2.BereitsBezahlt, this.jahresdaten.BereitsBezahlt);
            Assert.AreEqual(jahredData2.Jahr, this.jahresdaten.Jahr);
            Assert.AreEqual(jahredData2.KundenId, this.jahresdaten.KundenId);
            Assert.AreEqual(jahredData2.ZaehlerStandAlt, this.jahresdaten.ZaehlerStandAlt);
            Assert.AreEqual(jahredData2.ZaehlerStandNeu, this.jahresdaten.ZaehlerStandNeu);
            Assert.AreEqual(jahredData2.TauschZaehlerStandAlt, this.jahresdaten.TauschZaehlerStandAlt);
            Assert.AreEqual(jahredData2.TauschZaehlerStandNeu, this.jahresdaten.TauschZaehlerStandNeu);
            Assert.AreEqual(jahredData2.SonstigeForderungenText, this.jahresdaten.SonstigeForderungenText);
            Assert.AreEqual(jahredData2.SonstigeForderungenValue, this.jahresdaten.SonstigeForderungenValue);
            Assert.AreEqual(jahredData2.HalbjahresZahlung, this.jahresdaten.HalbjahresZahlung);
        }

        [Test]
        public void JahresdatenFindByKundenIdTest() {
            IJahresDaten jahredDatenDB = Database.CreateJahresDaten();
            JahresDatenData jd1 = new JahresDatenData(0, 1, 234, 345, 2007, DateTime.Now, 234.9,324,567,"asdfg",2345.7,345.7);
            JahresDatenData jd2 = new JahresDatenData(0, 1, 234, 3545, 2007, DateTime.Now, 233.9,76,987,"sadf",324.7,987.65);
            long jdid1 = jahredDatenDB.Insert(jd1);
            long jdid2 = jahredDatenDB.Insert(jd2);
            IList<JahresDatenData> jahresdataList = jahredDatenDB.FindByKundenId(1);
            Assert.IsTrue(jahresdataList.Count >= 2);
            bool jd1exists = false;
            bool jd2exists = false;

            foreach (JahresDatenData jd in jahresdataList) {
                if (jd.Id == jdid1) {
                    jd1exists = !jd1exists;
                    Assert.AreEqual(jd.AbleseDatum.Date, jd1.AbleseDatum.Date);
                    Assert.AreEqual(jd.BereitsBezahlt, jd1.BereitsBezahlt);
                    Assert.AreEqual(jd.Jahr, jd1.Jahr);
                    Assert.AreEqual(jd.KundenId, jd1.KundenId);
                    Assert.AreEqual(jd.ZaehlerStandAlt, jd1.ZaehlerStandAlt);
                    Assert.AreEqual(jd.ZaehlerStandNeu, jd1.ZaehlerStandNeu);
                    Assert.AreEqual(jd.TauschZaehlerStandAlt, jd1.TauschZaehlerStandAlt);
                    Assert.AreEqual(jd.TauschZaehlerStandNeu, jd1.TauschZaehlerStandNeu);
                    Assert.AreEqual(jd.SonstigeForderungenText, jd1.SonstigeForderungenText);
                    Assert.AreEqual(jd.SonstigeForderungenValue, jd1.SonstigeForderungenValue);
                    Assert.AreEqual(jd.HalbjahresZahlung, jd1.HalbjahresZahlung);
                }
                if (jd.Id == jdid2) {
                    jd2exists = !jd2exists;
                    Assert.AreEqual(jd.AbleseDatum.Date, jd2.AbleseDatum.Date);
                    Assert.AreEqual(jd.BereitsBezahlt, jd2.BereitsBezahlt);
                    Assert.AreEqual(jd.Jahr, jd2.Jahr);
                    Assert.AreEqual(jd.KundenId, jd2.KundenId);
                    Assert.AreEqual(jd.ZaehlerStandAlt, jd2.ZaehlerStandAlt);
                    Assert.AreEqual(jd.ZaehlerStandNeu, jd2.ZaehlerStandNeu);
                    Assert.AreEqual(jd.TauschZaehlerStandAlt, jd2.TauschZaehlerStandAlt);
                    Assert.AreEqual(jd.TauschZaehlerStandNeu, jd2.TauschZaehlerStandNeu);
                    Assert.AreEqual(jd.SonstigeForderungenText, jd2.SonstigeForderungenText);
                    Assert.AreEqual(jd.SonstigeForderungenValue, jd2.SonstigeForderungenValue);
                    Assert.AreEqual(jd.HalbjahresZahlung, jd2.HalbjahresZahlung);
                }
            }
            Assert.IsTrue(jd1exists && jd2exists);
        }

        [Test]
        public void KundeUpdateTest() {
            IJahresDaten jahredDatenDB = Database.CreateJahresDaten();
            JahresDatenData jahredData2 = jahredDatenDB.FindByID(this.jdID);
            jahredData2.AbleseDatum = jahresdaten.AbleseDatum = DateTime.Now;
            jahredData2.BereitsBezahlt = jahresdaten.BereitsBezahlt = 88.8;
            jahredData2.Jahr = jahresdaten.Jahr = 2007;
            jahredData2.KundenId = jahresdaten.KundenId = 5;
            jahredData2.ZaehlerStandAlt = jahresdaten.ZaehlerStandAlt = 8765;
            jahredData2.ZaehlerStandNeu = jahresdaten.ZaehlerStandNeu = 234;
            jahredData2.TauschZaehlerStandAlt = jahresdaten.TauschZaehlerStandAlt = 2345;
            jahredData2.TauschZaehlerStandNeu = jahresdaten.TauschZaehlerStandNeu = 987;
            jahredData2.SonstigeForderungenText = jahresdaten.SonstigeForderungenText = "assdfﬂ";
            jahredData2.SonstigeForderungenValue = jahresdaten.SonstigeForderungenValue = 98.9;
            jahredData2.HalbjahresZahlung = jahresdaten.HalbjahresZahlung = 234.09;


            Assert.IsTrue(jahredDatenDB.Update(jahredData2));
            JahresDatenData jahres3 = jahredDatenDB.FindByID(jdID);


            Assert.AreEqual(jahres3.Id, this.jdID);
            Assert.AreEqual(jahres3.AbleseDatum.Date, this.jahresdaten.AbleseDatum.Date);
            Assert.AreEqual(jahres3.BereitsBezahlt, this.jahresdaten.BereitsBezahlt);
            Assert.AreEqual(jahres3.Jahr, this.jahresdaten.Jahr);
            Assert.AreEqual(jahres3.KundenId, this.jahresdaten.KundenId);
            Assert.AreEqual(jahres3.ZaehlerStandAlt, this.jahresdaten.ZaehlerStandAlt);
            Assert.AreEqual(jahres3.ZaehlerStandNeu, this.jahresdaten.ZaehlerStandNeu);
            Assert.AreEqual(jahres3.TauschZaehlerStandAlt, this.jahresdaten.TauschZaehlerStandAlt);
            Assert.AreEqual(jahres3.TauschZaehlerStandNeu, this.jahresdaten.TauschZaehlerStandNeu);
            Assert.AreEqual(jahres3.SonstigeForderungenText, this.jahresdaten.SonstigeForderungenText);
            Assert.AreEqual(jahres3.SonstigeForderungenValue, this.jahresdaten.SonstigeForderungenValue);
            Assert.AreEqual(jahres3.HalbjahresZahlung, this.jahresdaten.HalbjahresZahlung);
        }

        [Test]
        public void JahresDataDeleteTest() {
            IJahresDaten jahresDataDB = Database.CreateJahresDaten();
            Assert.IsNotNull(jahresDataDB.FindByID(this.jdID));
            Assert.IsTrue(jahresDataDB.Delete(this.jdID));
            Assert.IsNull(jahresDataDB.FindByID(this.jdID));
        }
    }


    [TestFixture]
    public class PreisTest {

        private PreisData preis;
        private long jahr;
        private Random rand = new Random();

        [SetUp]
        public void InitVisitTest() {
            IPreis preisDB = Database.CreatePreis();
            jahr = (long)rand.Next();
            preis = new PreisData(jahr, 23.8);
            Assert.IsTrue(preisDB.Insert(preis));
        }

        [Test]
        public void JahresdatenFindAllTest() {
            IPreis preisDB = Database.CreatePreis();
            IList<PreisData> preisList = preisDB.FindAll();
            foreach (PreisData preisfoeach in preisList) {
                if (preisfoeach.Preis == this.jahr) {
                    Assert.AreEqual(preisfoeach.Jahr, this.preis.Jahr);
                    Assert.AreEqual(preisfoeach.Preis, this.preis.Preis);
                }
            }
        }

        [Test]
        public void JahresdatenFindByIdTest() {
            IPreis preisDB = Database.CreatePreis();
            PreisData  preis2 = preisDB.FindByJahr(this.jahr);
            Assert.AreEqual(preis2.Jahr, this.jahr);
            Assert.AreEqual(preis2.Preis, this.preis.Preis);
            Assert.AreEqual(preis2.Jahr, this.preis.Jahr);
        }

        [Test]
        public void KundeUpdateTest() {
            IPreis preisDB = Database.CreatePreis();
            PreisData preis2 = preisDB.FindByJahr(this.jahr);

            preis2.Preis = preis.Preis = 34.7;

            Assert.IsTrue(preisDB.Update(preis2));
            PreisData preis3 = preisDB.FindByJahr(this.jahr);


            Assert.AreEqual(preis3.Jahr, this.jahr);
            Assert.AreEqual(preis3.Preis, this.preis.Preis);
            Assert.AreEqual(preis3.Jahr, this.preis.Jahr);
            
        }

        [Test]
        public void JahresDataDeleteTest() {
            IPreis preisDB = Database.CreatePreis();
            Assert.IsNotNull(preisDB.FindByJahr(this.jahr));
            Assert.IsTrue(preisDB.Delete(this.jahr));
            Assert.IsNull(preisDB.FindByJahr(this.jahr));
        }
    }
}
    