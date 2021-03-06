using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WasserWerkVerwaltung.CommonObjects;
using System.Globalization;
using WasserWerkVerwaltung.CommonUtilities;
using NUnit.Framework;

namespace WasserWerkVerwaltung.DAL {
    class Zahlung /*: IZahlung */ {

        const string SQL_FIND_BY_ID = "SELECT * FROM Kunde WHERE KundeID = ?";
        const string SQL_FIND_ALL = "SELECT * FROM Kunde";
        const string SQL_LAST_INSERTED_ROW = "SELECT @@Identity";
        readonly string SQL_UPDATE_BY_ID = "UPDATE Kunde SET Vorname = ?, Nachname = ?, Strasse = ?, Ort = ?, Objekt = ?, Tel = ?, Hausbesitzer = ?, Bankverbindung = ?, bekommtRechnung = ?, Eichdatum = ?, ZaehlerNr = ?, Einbaudatum = ?, Erkl = ?, Tauschdatum = ?, Zaehlermiete = ?, Bemerkung = ?, Zahlung = ?, Leitungskreis = ?, PersonenImObjekt = ? WHERE KundeID = ?";
        readonly string SQL_INSERT_BY_ID = "INSERT INTO Kunde (Vorname, Nachname, Strasse, Ort, Objekt, Tel, Hausbesitzer, Bankverbindung, bekommtRechnung, Eichdatum, ZaehlerNr, Einbaudatum, Erkl, Tauschdatum, Zaehlermiete, Bemerkung, Zahlung, Leitungskreis, PersonenImObjekt) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
        readonly string SQL_DELETE_BY_ID = "DELETE FROM Kunde WHERE KundeID = ?";
        static IDbCommand findByIdCmd;
        static IDbCommand findAllCmd;
        static IDbCommand updateByIdCmd;
        static IDbCommand insertByIdCmd;
        static IDbCommand deleteByIdCmd;
        static IDbCommand lastInsertedRowCmd;


        //KundeID, Vorname, Nachname, Strasse, Ort, Tel, Hausbesitzer, Bankverbindung, 
        //bekommtRechnung, ZaehlerEinbauStand, ZaehlerNeuStand, Eichdatum, ZaehlerNr, 
        //Einbaudatum, Erkl, Tauschdatum, Zaehlermiete, Bemerkung, Zahlung, PersonenImObjekt


        public IList<KundenData> FindAll() {
            try {
                DbUtil.OpenConnection();

                if (findAllCmd == null) {
                    findAllCmd = DbUtil.CreateCommand(SQL_FIND_ALL, DbUtil.CurrentConnection);
                }

                using (IDataReader rdr = findAllCmd.ExecuteReader()) {
                    IList<KundenData> kundenList = new List<KundenData>();
                    while (rdr.Read()) {
                        kundenList.Add(new KundenData( (long)(int)rdr["KundeID"],
                                    (string)rdr["Vorname"],
                                    (string)rdr["Nachname"], 
                                    (string)rdr["Strasse"],
                                    (string)rdr["Ort"],
                                    (string)rdr["Objekt"],
                                    (string)rdr["Tel"],  
                                    (string)rdr["Hausbesitzer"],
                                    (string)rdr["Bankverbindung"],
                                    StaticUtilities.Rechnung((int)rdr["bekommtRechnung"]),
                                    (DateTime)rdr["Eichdatum"],
                                    (string)rdr["ZaehlerNr"],
                                    (DateTime)rdr["Einbaudatum"],
                                    (string)rdr["Erkl"],
                                    (DateTime)rdr["Tauschdatum"],
                                    (double)rdr["Zaehlermiete"],
                                    (string)rdr["Bemerkung"],
                                    (string) rdr["Zahlung"],
                                    (long)(int)rdr["Leitungskreis"],
                                    (long)(int)rdr["PersonenImObjekt"]));
                    }
                    return kundenList;
                }
            } finally {
                DbUtil.CloseConnection();
            }
        }

        public KundenData FindByID(long id) {
            try {
                DbUtil.OpenConnection();

                if (findByIdCmd == null) {
                    findByIdCmd = DbUtil.CreateCommand(SQL_FIND_BY_ID, DbUtil.CurrentConnection);
                    findByIdCmd.Parameters.Add(DbUtil.CreateParameter("@KundeID", DbType.Int64));
                }

                ((IDataParameter)findByIdCmd.Parameters["@KundeID"]).Value = id;

                using (IDataReader rdr = findByIdCmd.ExecuteReader()) {
                    IList<KundenData> kundenList = new List<KundenData>();
                    if (rdr.Read()) {
                        return new KundenData((long)(int)rdr["KundeID"], 
                                    (string)rdr["Vorname"], 
                                    (string)rdr["Nachname"],
                                    (string)rdr["Strasse"], 
                                    (string)rdr["Ort"], 
                                    (string)rdr["Objekt"],
                                    (string)rdr["Tel"],
                                    (string)rdr["Hausbesitzer"], 
                                    (string)rdr["Bankverbindung"],
                                    StaticUtilities.Rechnung((int)rdr["bekommtRechnung"]), 
                                    (DateTime)rdr["Eichdatum"],
                                    (string)rdr["ZaehlerNr"],
                                    (DateTime)rdr["Einbaudatum"],
                                    (string)rdr["Erkl"],
                                    (DateTime)rdr["Tauschdatum"],
                                    (double)rdr["Zaehlermiete"], 
                                    (string)rdr["Bemerkung"],
                                    (string) rdr["Zahlung"],
                                    (long)(int)rdr["Leitungskreis"],
                                    (long)(int)rdr["PersonenImObjekt"]);
                    }
                }
            } finally {
                DbUtil.CloseConnection();
            }
            return null;
        }

        public long Insert(KundenData kunde) {
            try {
                DbUtil.OpenConnection();

                if (insertByIdCmd == null) {
                    insertByIdCmd = DbUtil.CreateCommand(SQL_INSERT_BY_ID, DbUtil.CurrentConnection);
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Vorname", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Nachname", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Strasse", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Ort", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Objekt", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Tel", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Hausbesitzer", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Bankverbindung", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@bekommtRechnung", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Eichdatum", DbType.DateTime));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@ZaehlerNr", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Einbaudatum", DbType.DateTime));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Erkl", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Tauschdatum", DbType.DateTime));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Zaehlermiete", DbType.Double));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Bemerkung", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Zahlung", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Leitungskreis", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@PersonenImObjekt", DbType.Int64));
                }

                ((IDataParameter)insertByIdCmd.Parameters["@Vorname"]).Value = kunde.Vorname;
                ((IDataParameter)insertByIdCmd.Parameters["@Nachname"]).Value = kunde.Nachname;
                ((IDataParameter)insertByIdCmd.Parameters["@Strasse"]).Value = kunde.Strasse;
                ((IDataParameter)insertByIdCmd.Parameters["@Ort"]).Value = kunde.Ort;
                ((IDataParameter)insertByIdCmd.Parameters["@Objekt"]).Value = kunde.Objekt;
                ((IDataParameter)insertByIdCmd.Parameters["@Tel"]).Value = kunde.Tel;
                ((IDataParameter)insertByIdCmd.Parameters["@Hausbesitzer"]).Value = kunde.Hausbesitzer;
                ((IDataParameter)insertByIdCmd.Parameters["@Bankverbindung"]).Value = kunde.BankVerbindung;
                ((IDataParameter)insertByIdCmd.Parameters["@bekommtRechnung"]).Value = StaticUtilities.Rechnung(kunde.BekommtRechnung);
                ((IDataParameter)insertByIdCmd.Parameters["@Eichdatum"]).Value = kunde.EichDatum.Date;
                ((IDataParameter)insertByIdCmd.Parameters["@ZaehlerNr"]).Value = kunde.ZaehlerNummer;
                ((IDataParameter)insertByIdCmd.Parameters["@Einbaudatum"]).Value = kunde.EinbauDatum.Date;
                ((IDataParameter)insertByIdCmd.Parameters["@Erkl"]).Value = kunde.Erkl;
                ((IDataParameter)insertByIdCmd.Parameters["@Tauschdatum"]).Value = kunde.TauschDatum.Date;
                ((IDataParameter)insertByIdCmd.Parameters["@Zaehlermiete"]).Value = kunde.Zaehlermiete;
                ((IDataParameter)insertByIdCmd.Parameters["@Bemerkung"]).Value = kunde.Bemerkung;
                ((IDataParameter)insertByIdCmd.Parameters["@Zahlung"]).Value = kunde.Zahlung;
                ((IDataParameter)insertByIdCmd.Parameters["@Leitungskreis"]).Value = kunde.Leitungskreis;
                ((IDataParameter)insertByIdCmd.Parameters["@PersonenImObjekt"]).Value = kunde.PersonenImObjekt;

                if (insertByIdCmd.ExecuteNonQuery() != 1)
                    return 0;
                else {
                    if (lastInsertedRowCmd == null) {
                        
                        lastInsertedRowCmd = DbUtil.CreateCommand(SQL_LAST_INSERTED_ROW, DbUtil.CurrentConnection);
                    }
                    return (long)(int)lastInsertedRowCmd.ExecuteScalar();
                }
            } finally {
                DbUtil.CloseConnection();
            }
        }

        public bool Update(KundenData kunde) {
            try {
                DbUtil.OpenConnection();

                if (updateByIdCmd == null) {
                    updateByIdCmd = DbUtil.CreateCommand(SQL_UPDATE_BY_ID, DbUtil.CurrentConnection);

                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Vorname", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Nachname", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Strasse", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Ort", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Objekt", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Tel", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Hausbesitzer", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Bankverbindung", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@bekommtRechnung", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Eichdatum", DbType.DateTime));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@ZaehlerNr", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Einbaudatum", DbType.DateTime));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Erkl", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Tauschdatum", DbType.DateTime));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Zaehlermiete", DbType.Double));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Bemerkung", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Zahlung", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Leitungskreis", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@PersonenImObjekt", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@KundeID", DbType.Int64));
                }

                ((IDataParameter)updateByIdCmd.Parameters["@Vorname"]).Value = kunde.Vorname;
                ((IDataParameter)updateByIdCmd.Parameters["@Nachname"]).Value = kunde.Nachname;
                ((IDataParameter)updateByIdCmd.Parameters["@Strasse"]).Value = kunde.Strasse;
                ((IDataParameter)updateByIdCmd.Parameters["@Ort"]).Value = kunde.Ort;
                ((IDataParameter)updateByIdCmd.Parameters["@Objekt"]).Value = kunde.Objekt;
                ((IDataParameter)updateByIdCmd.Parameters["@Tel"]).Value = kunde.Tel;
                ((IDataParameter)updateByIdCmd.Parameters["@Hausbesitzer"]).Value = kunde.Hausbesitzer;
                ((IDataParameter)updateByIdCmd.Parameters["@Bankverbindung"]).Value = kunde.BankVerbindung;
                ((IDataParameter)updateByIdCmd.Parameters["@bekommtRechnung"]).Value = StaticUtilities.Rechnung(kunde.BekommtRechnung);;
                ((IDataParameter)updateByIdCmd.Parameters["@Eichdatum"]).Value = kunde.EichDatum.Date;
                ((IDataParameter)updateByIdCmd.Parameters["@ZaehlerNr"]).Value = kunde.ZaehlerNummer;
                ((IDataParameter)updateByIdCmd.Parameters["@Einbaudatum"]).Value = kunde.EinbauDatum.Date;
                ((IDataParameter)updateByIdCmd.Parameters["@Erkl"]).Value = kunde.Erkl;
                ((IDataParameter)updateByIdCmd.Parameters["@Tauschdatum"]).Value = kunde.TauschDatum.Date;
                ((IDataParameter)updateByIdCmd.Parameters["@Zaehlermiete"]).Value = kunde.Zaehlermiete;
                ((IDataParameter)updateByIdCmd.Parameters["@Bemerkung"]).Value = kunde.Bemerkung;
                ((IDataParameter)updateByIdCmd.Parameters["@Zahlung"]).Value = kunde.Zahlung;
                ((IDataParameter)updateByIdCmd.Parameters["@Leitungskreis"]).Value = kunde.Leitungskreis;
                ((IDataParameter)updateByIdCmd.Parameters["@PersonenImObjekt"]).Value = kunde.PersonenImObjekt;
                ((IDataParameter)updateByIdCmd.Parameters["@KundeID"]).Value = kunde.Id;

                return updateByIdCmd.ExecuteNonQuery() == 1;
            } finally {
                DbUtil.CloseConnection();
            }
        }

        public bool Delete(long id) {
            try {
                DbUtil.OpenConnection();

                if (deleteByIdCmd == null) {
                    deleteByIdCmd = DbUtil.CreateCommand(SQL_DELETE_BY_ID, DbUtil.CurrentConnection);
                    deleteByIdCmd.Parameters.Add(DbUtil.CreateParameter("@KundeID", DbType.Int64));
                }

                ((IDataParameter)deleteByIdCmd.Parameters["@KundeID"]).Value = id;

                return deleteByIdCmd.ExecuteNonQuery() == 1;
            } finally {
                DbUtil.CloseConnection();
            }
        }

    }
}
