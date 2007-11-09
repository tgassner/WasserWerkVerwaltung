using System;
using System.Collections.Generic;
using System.Text;

namespace WasserWerkVerwaltung.CommonObjects {

    [Serializable]
    public class KundenData {
        private long id;
        private string vorname;
        private string nachname;
        private string strasse;
        private string ort;
        private string tel;
        private string hausbesitzer;
        private string bankVerbindung;
        private bool bekommtRechnung;
        private long zaehlerEinbauStand;
        private long zaehlerNeuStand;
        private DateTime eichDatum;
        private string zaehlerNummer;
        private DateTime einbauDatum;
        private string erkl;
        private DateTime tauschDatum;
        private double zaehlermiete;
        private string bemerkung;

        public KundenData(long id, string vorname, string nachname, string strasse,
                    string ort, string tel, string hausbesitzer, string bankVerbindung, 
                    bool bekommtRechnung, long zaehlerEinbauStand, long zaehlerNeuStand, 
                    DateTime eichDatum, string zaehlerNummer, DateTime einbauDatum, 
                    string erkl, DateTime tauschDatum, double zaehlermiete, string bemerkung){

            this.id = id;
            this.vorname = vorname;
            this.nachname = nachname;
            this.strasse = strasse;
            this.ort = ort;
            this.tel = tel;
            this.hausbesitzer = hausbesitzer;
            this.bankVerbindung = bankVerbindung;
            this.bekommtRechnung = bekommtRechnung;
            this.zaehlerEinbauStand = zaehlerEinbauStand;
            this.zaehlerNeuStand = zaehlerNeuStand;
            this.eichDatum = eichDatum;
            this.zaehlerNummer = zaehlerNummer;
            this.einbauDatum = einbauDatum;
            this.erkl = erkl;
            this.tauschDatum = tauschDatum;
            this.zaehlermiete = zaehlermiete;
            this.bemerkung = bemerkung;
            
        }

        public long Id {
            get {
                return id;
            }
        }

        public string Vorname {
            get {
                return this.vorname;
            }
            set {
                this.vorname = value;
            }
        }

        public string Nachmame {
            get {
                return this.nachname;
            }
            set {
                this.nachname = value;
            }
        }

        public string Strasse {
            get {
                return this.strasse;
            }
            set {
                this.strasse = value;
            }
        }

        public string Ort {
            get {
                return this.ort;
            }
            set {
                this.ort = value;
            }
        }

        public string Tel {
            get {
                return this.tel;
            }
            set {
                this.tel = value;
            }
        }

        public string Hausbesitzer {
            get {
                return this.hausbesitzer;
            }
            set {
                this.hausbesitzer = value;
            }
        }

        public string BankVerbindung {
            get {
                return this.bankVerbindung;
            }
            set {
                this.bankVerbindung = value;
            }
        }

        public bool BekommtRechnung {
            get {
                return this.bekommtRechnung;
            }
            set {
                this.bekommtRechnung = value;
            }
        }

        public long ZaehlerEinbauStand {
            get {
                return this.zaehlerEinbauStand;
            }
            set {
                this.zaehlerEinbauStand = value;
            }
        }

        public long ZaehlerNeuStand {
            get {
                return this.zaehlerNeuStand;
            }
            set {
                this.zaehlerNeuStand = value;
            }
        }

        public DateTime EichDatum {
            get {
                return this.eichDatum;
            }
            set {
                this.eichDatum = value;
            }
        }

        public string ZaehlerNummer {
            get {
                return this.zaehlerNummer;
            }
            set {
                this.zaehlerNummer = value;
            }
        }

        public DateTime EinbauDatum {
            get {
                return this.einbauDatum;
            }
            set {
                this.einbauDatum = value;
            }
        }

        public string Erkl {
            get {
                return this.erkl;
            }
            set {
                this.erkl = value;
            }
        }

        public DateTime TauschDatum {
            get {
                return this.tauschDatum;
            }
            set {
                this.tauschDatum = value;
            }
        }

        public double Zaehlermiete {
            get {
                return this.zaehlermiete;
            }
            set {
                this.zaehlermiete = value;
            }
        }

        public string Bemerkung {
            get {
                return this.bemerkung;
            }
            set {
                this.bemerkung = value;
            }
        }

        public override string ToString() {
            return this.vorname + " " + this.nachname;
        }

        public override bool Equals(object obj) {
            KundenData cd = obj as KundenData;
            if (cd == null)
                return false;
            return cd.Id == this.id;
        }

        public override int GetHashCode() {
            return this.id.GetHashCode() + this.vorname.GetHashCode() + this.nachname.GetHashCode();
        }
    }

    [Serializable]
    public class AblesungData {
        private long id;
        private long kundenId;
        private double rechnungssumme;
        private string zahlung;
        private long zaehlerStandAlt;
        private long zaehlerStandNeu;
        private long jahr;
        private DateTime ableseDatum;
        private double bereitsBezahlt;

        public AblesungData(long id, long kundenId, double rechnungssumme, string zahlung, 
                    long zaehlerStandAlt, long zaehlerStandNeu, long jahr, 
                    DateTime ableseDatum, double bereitsBezahlt) {
            this.id = id;
            this.kundenId = kundenId;
            this.rechnungssumme = rechnungssumme;
            this.zahlung = zahlung;
            this.zaehlerStandAlt = zaehlerStandAlt;
            this.zaehlerStandNeu = zaehlerStandNeu;
            this.jahr = jahr;
            this.ableseDatum = ableseDatum;
            this.bereitsBezahlt = bereitsBezahlt;
        }

        public long Id {
            get {
                return this.id;
            }
            set {
                this.id = value;
            }
        }

        public long KundenId {
            get {
                return this.kundenId;
            }
            set {
                this.kundenId = value;
            }
        }

        public double Rechnungssumme{
            get {
                return this.rechnungssumme;
            }
            set {
                this.rechnungssumme = value;
            }
        }

        public string Zahlung {
            get {
                return this.zahlung;
            }
            set {
                this.zahlung = value;
            }
        }

        public long ZaehlerStandAlt {
            get {
                return this.zaehlerStandAlt;
            }
            set {
                this.zaehlerStandAlt = value;
            }
        }

        public long ZaehlerStandNeu {
            get {
                return this.zaehlerStandNeu;
            }
            set {
                this.zaehlerStandNeu = value;
            }
        }

                public long Jahr {
            get {
                return this.jahr;
            }
            set {
                this.jahr = value;
            }
        }

        public DateTime AbleseDatum {
            get {
                return this.ableseDatum;
            }
            set {
                this.ableseDatum = value;
            }
        }

        public double BereitsBezahlt {
            get {
                return this.bereitsBezahlt;
            }
            set {
                this.bereitsBezahlt = value;
            }
        }

        public override string ToString() {
            return this.id + this.jahr.ToString() + " " + this.zaehlerStandNeu.ToString();
        }

        public override bool Equals(object obj) {
            AblesungData abl = obj as AblesungData;
            if (abl == null)
                return false;
            return abl.Id == this.id;
        }

        public override int GetHashCode() {
            return this.id.GetHashCode() + this.jahr.GetHashCode() + this.zaehlerStandNeu.GetHashCode();
        }
    }

    [Serializable]
    public class PreisData {
        private long jahr;
        private long preis;

        public PreisData(long jahr, long preis) {
            this.jahr = jahr;
            this.preis = preis;
        }

        public long Jahr {
            get {
                return this.jahr;
            }
            set {
                this.jahr = value;
            }
        }

        public long Preis {
            get {
                return this.preis;
            }
            set {
                this.preis = value;
            }
        }       

        public override string ToString() {
            return this.jahr + this.jahr.ToString() + " " + this.preis.ToString();
        }

        public override bool Equals(object obj) {
            AblesungData abl = obj as AblesungData;
            if (abl == null)
                return false;
            return abl.Jahr == this.jahr;
        }

        public override int GetHashCode() {
            return this.jahr.GetHashCode() + this.preis.GetHashCode();
        }
    }
}