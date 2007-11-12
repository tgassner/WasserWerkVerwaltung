using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WasserWerkVerwaltung.CommonObjects;
using System.Globalization;
using SimplePatientDocumentation.DAL;
using NUnit.Framework;

namespace WasserWerkVerwaltung.DAL {
    class Preis : IPreis {

        const string SQL_FIND_BY_ID = "SELECT * FROM Preis WHERE Jahr = ?";
        const string SQL_FIND_ALL = "SELECT * FROM Preis";
        //const string SQL_LAST_INSERTED_ROW = "SELECT @@Identity";
        readonly string SQL_UPDATE_BY_ID = "UPDATE Preis SET Preis = ? WHERE Jahr = ?";
        readonly string SQL_INSERT_BY_ID = "INSERT INTO Preis (Jahr, Preis) VALUES(?,?)";
        readonly string SQL_DELETE_BY_ID = "DELETE FROM Preis WHERE Jahr = ?";
        static IDbCommand findByIdCmd;
        static IDbCommand findAllCmd;
        static IDbCommand updateByIdCmd;
        static IDbCommand insertByIdCmd;
        static IDbCommand deleteByIdCmd;
        //static IDbCommand lastInsertedRowCmd;


        //KundeID, Vorname, Nachname, Strasse, Ort, Tel, Hausbesitzer, Bankverbindung, bekommtRechnung, ZaehlerEinbauStand, ZaehlerNeuStand, Eichdatum, ZaehlerNr, Einbaudatum, Erkl, Tauschdatum, Zaehlermiete, Bemerkung


        public IList<PreisData> FindAll() {
            try {
                DbUtil.OpenConnection();

                if (findAllCmd == null) {
                    findAllCmd = DbUtil.CreateCommand(SQL_FIND_ALL, DbUtil.CurrentConnection);
                }

                using (IDataReader rdr = findAllCmd.ExecuteReader()) {
                    IList<PreisData> preisList = new List<PreisData>();
                    while (rdr.Read()) {
                        preisList.Add(new PreisData((long)(int)rdr["Jahr"], (double)rdr["Preis"]));
                    }
                    return preisList;
                }
            } finally {
                DbUtil.CloseConnection();
            }
        }

        public PreisData FindByJahr(long jahr) {
            try {
                DbUtil.OpenConnection();

                if (findByIdCmd == null) {
                    findByIdCmd = DbUtil.CreateCommand(SQL_FIND_BY_ID, DbUtil.CurrentConnection);
                    findByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Jahr", DbType.Int64));
                }

                ((IDataParameter)findByIdCmd.Parameters["@Jahr"]).Value = jahr;

                using (IDataReader rdr = findByIdCmd.ExecuteReader()) {
                    IList<PreisData> kundenList = new List<PreisData>();
                    if (rdr.Read()) {
                        return new PreisData((long)(int)rdr["Jahr"], (double)rdr["Preis"]);
                    }
                }
            } finally {
                DbUtil.CloseConnection();
            }
            return null;
        }

        public bool Insert(PreisData preis) {
            try {
                DbUtil.OpenConnection();

                if (insertByIdCmd == null) {
                    insertByIdCmd = DbUtil.CreateCommand(SQL_INSERT_BY_ID, DbUtil.CurrentConnection);
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Jahr", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Preis", DbType.Double));
                }

                ((IDataParameter)insertByIdCmd.Parameters["@Preis"]).Value = preis.Preis;
                ((IDataParameter)insertByIdCmd.Parameters["@Jahr"]).Value = preis.Jahr;

                if (insertByIdCmd.ExecuteNonQuery() != 1)
                    return false;
                else {
                    return true;
                }
            } finally {
                DbUtil.CloseConnection();
            }
        }

        public bool Update(PreisData preis) {
            try {
                DbUtil.OpenConnection();

                if (updateByIdCmd == null) {
                    updateByIdCmd = DbUtil.CreateCommand(SQL_UPDATE_BY_ID, DbUtil.CurrentConnection);

                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Preis", DbType.Double));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Jahr", DbType.Int64));
                }

                ((IDataParameter)updateByIdCmd.Parameters["@Preis"]).Value = preis.Preis;
                ((IDataParameter)updateByIdCmd.Parameters["@Jahr"]).Value = preis.Jahr;

                return updateByIdCmd.ExecuteNonQuery() == 1;
            } finally {
                DbUtil.CloseConnection();
            }
        }

        public bool Delete(long jahr) {
            try {
                DbUtil.OpenConnection();

                if (deleteByIdCmd == null) {
                    deleteByIdCmd = DbUtil.CreateCommand(SQL_DELETE_BY_ID, DbUtil.CurrentConnection);
                    deleteByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Jahr", DbType.Int64));
                }

                ((IDataParameter)deleteByIdCmd.Parameters["@Jahr"]).Value = jahr;

                return deleteByIdCmd.ExecuteNonQuery() == 1;
            } finally {
                DbUtil.CloseConnection();
            }
        }

    }
}
