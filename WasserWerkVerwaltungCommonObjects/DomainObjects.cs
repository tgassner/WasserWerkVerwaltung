using System;
using System.Collections.Generic;
using System.Text;

namespace WasserWerkVerwaltung.CommonObjects {

    [Serializable]
    public class KundenData {
        private long id;
        private string vorname;
        private string name;
        private string strasse;
        private string objekt;
        private string ort;
        private string tel;
        private string bankVerbindung;
        private long tzEinbau;
        private long tzNeu;
        private DateTime eichDatum;
        private string zaehlerNummer;
        private DateTime einbauDatum;
        private string erkl;
        private string hausbesitzer;
        private DateTime tauschDatum;
        private long zaehlermiete;
        private string zahlung;
        private bool bekommtRechnung;

        public KundenData(long id, string vorname, string name, string strasse,
                string objekt, string ort, string tel, string bankVerbindung,
                long tzEinbau, long tzNeu, DateTime eichDatum, string zaehlerNummer,
                DateTime einbauDatum, string erkl, string hausbesitzer, DateTime tauschDatum,
                long zaehlermiete, string zahlung, bool bekommtRechnung) {

            this.id = id;
            this.vorname = vorname;
            this.name = name;
            this.strasse = strasse;
            this.objekt = objekt;
            this.ort = ort;
            this.tel = tel;
            this.bankVerbindung = bankVerbindung;
            this.tzEinbau = tzEinbau;
            this.tzNeu = tzNeu;
            this.eichDatum = eichDatum;
            this.zaehlerNummer = zaehlerNummer;
            this.einbauDatum = einbauDatum;
            this.erkl = erkl;
            this.hausbesitzer = hausbesitzer;
            this.tauschDatum = tauschDatum;
            this.zaehlermiete = zaehlermiete;
            this.zahlung = zahlung;
            this.bekommtRechnung = bekommtRechnung;

        }

        public long Id {
            get {
                return id;
            }
        }

        public string Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
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

        public string Strasse {
            get {
                return this.strasse;
            }
            set {
                this.strasse = value;
            }
        }

        public string Objekt {
            get {
                return this.objekt;
            }
            set {
                this.objekt = value;
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

        public string BankVerbindung {
            get {
                return this.bankVerbindung;
            }
            set {
                this.bankVerbindung = value;
            }
        }

        public long TzEinbau {
            get {
                return this.tzEinbau;
            }
            set {
                this.tzEinbau = value;
            }
        }

        public long TzNeu {
            get {
                return this.tzNeu;
            }
            set {
                this.tzNeu = value;
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

        public string Hausbesitzer {
            get {
                return this.hausbesitzer;
            }
            set {
                this.hausbesitzer = value;
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

        public long Zaehlermiete {
            get {
                return this.zaehlermiete;
            }
            set {
                this.zaehlermiete = value;
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

        public bool BekommtRechnung{
            get {
                return this.bekommtRechnung;
            }
            set {
                this.bekommtRechnung = value;
            }
        }

        public override string ToString() {
            return this.name + " " + this.vorname;
        }

        public override bool Equals(object obj) {
            KundenData cd = obj as KundenData;
            if (cd == null)
                return false;
            return cd.Id == this.id;
        }

        public override int GetHashCode() {
            return this.id.GetHashCode() + this.vorname.GetHashCode() + this.name.GetHashCode();
        }
    }

    [Serializable]
    public class AblesungData {
        private long id;
        private long kundenId;
        private long jahr;
        private DateTime ableseDatum;
        private long zaehlerstand;
        private long zaehlerstand2;

        public AblesungData(long id, long kundenId, long jahr, DateTime ableseDatum, long zaehlerstand, long zaehlerstand2) {
            this.id = id;
            this.kundenId = kundenId;
            this.jahr = jahr;
            this.ableseDatum = ableseDatum;
            this.zaehlerstand = zaehlerstand;
            this.zaehlerstand2 = zaehlerstand2;
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
        public long Zaehlerstand {
            get {
                return this.zaehlerstand;
            }
            set {
                this.zaehlerstand = value;
            }
        }

        public long Zaehlerstand2 {
            get {
                return this.zaehlerstand2;
            }
            set {
                this.zaehlerstand2 = value;
            }
        }

        public override string ToString() {
            return this.jahr.ToString() + " " + this.zaehlerstand.ToString();
        }

        public override bool Equals(object obj) {
            AblesungData abl = obj as AblesungData;
            if (abl == null)
                return false;
            return abl.Id == this.id;
        }

        public override int GetHashCode() {
            return this.id.GetHashCode() + this.jahr.GetHashCode() + this.zaehlerstand.GetHashCode();
        }
    }
}
