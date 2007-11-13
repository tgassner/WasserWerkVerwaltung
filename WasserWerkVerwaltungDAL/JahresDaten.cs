using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WasserWerkVerwaltung.CommonObjects;
using System.Globalization;
using SimplePatientDocumentation.DAL;
using NUnit.Framework;

namespace WasserWerkVerwaltung.DAL {
    class JahresDaten : IJahresDaten {

        const string SQL_FIND_BY_ID = "SELECT * FROM JahresDaten WHERE JahresDatenID = ?";
        const string SQL_FIND_BY_KUNDEN_ID = "SELECT * FROM JahresDaten WHERE KundeID = ?";
        const string SQL_FIND_ALL = "SELECT * FROM JahresDaten";
        const string SQL_LAST_INSERTED_ROW = "SELECT @@Identity";
        readonly string SQL_UPDATE_BY_ID = "UPDATE JahresDaten SET KundeID = ?, Rechnungssumme = ?, ZaehlerStandAlt = ?, ZaehlerStandNeu = ?, Ablesedatum = ?, Jahr = ?, BereitsBezahlt = ? WHERE JahresDatenID = ?";
        readonly string SQL_INSERT_BY_ID = "INSERT INTO JahresDaten (KundeID, Rechnungssumme, ZaehlerStandAlt, ZaehlerStandNeu, Ablesedatum, Jahr, BereitsBezahlt) VALUES(?,?,?,?,?,?,?)";
        readonly string SQL_DELETE_BY_ID = "DELETE FROM JahresDaten WHERE JahresDatenID = ?";
        static IDbCommand findByIdCmd;
        static IDbCommand findByKundeIdCmd;
        static IDbCommand findAllCmd;
        static IDbCommand updateByIdCmd;
        static IDbCommand insertByIdCmd;
        static IDbCommand deleteByIdCmd;
        static IDbCommand lastInsertedRowCmd;
       
        //JahresDatenID, KundeID, Rechnungssumme, ZaehlerStandAlt, ZaehlerStandNeu, Ablesedatum, Jahr, BereitsBezahlt

        public IList<JahresDatenData> FindAll() {
            try {
                DbUtil.OpenConnection();

                if (findAllCmd == null) {
                    findAllCmd = DbUtil.CreateCommand(SQL_FIND_ALL, DbUtil.CurrentConnection);
                }

                using (IDataReader rdr = findAllCmd.ExecuteReader()) {
                    IList<JahresDatenData> jahresDatenList = new List<JahresDatenData>();
                    while (rdr.Read()) {

                        jahresDatenList.Add(new JahresDatenData(
                                     (long)(int)rdr["JahresDatenID"], 
                                     (long)(int)rdr["KundeID"],
                                     (double) rdr["Rechnungssumme"],
                                     (long) (int) rdr["ZaehlerStandAlt"],
                                     (long) (int) rdr["ZaehlerStandNeu"],
                                     (long) (int) rdr["Jahr"],
                                     (DateTime)rdr["Ablesedatum"],
                                     (double) rdr["BereitsBezahlt"]
                                    ));
                    }
                    return jahresDatenList;
                }
            } finally {
                DbUtil.CloseConnection();
            }
        }

        public JahresDatenData FindByID(long id) {
            try {
                DbUtil.OpenConnection();

                if (findByIdCmd == null) {
                    findByIdCmd = DbUtil.CreateCommand(SQL_FIND_BY_ID, DbUtil.CurrentConnection);
                    findByIdCmd.Parameters.Add(DbUtil.CreateParameter("@JahresDatenID", DbType.Int64));
                }

                ((IDataParameter)findByIdCmd.Parameters["@JahresDatenID"]).Value = id;

                using (IDataReader rdr = findByIdCmd.ExecuteReader()) {
                    if (rdr.Read()) {

                        return new JahresDatenData(
                                     (long)(int)rdr["JahresDatenID"],
                                     (long)(int)rdr["KundeID"],
                                     (double)rdr["Rechnungssumme"],
                                     (long)(int)rdr["ZaehlerStandAlt"],
                                     (long)(int)rdr["ZaehlerStandNeu"],
                                     (long)(int)rdr["Jahr"],
                                     (DateTime)rdr["Ablesedatum"],
                                     (double)rdr["BereitsBezahlt"]
                                    );
                    }
                }
            } finally {
                DbUtil.CloseConnection();
            }
            return null;
        }

        public IList<JahresDatenData> FindByKundenId(long kundenId) {
            try {
                DbUtil.OpenConnection();

                if (findByKundeIdCmd == null) {
                    findByKundeIdCmd = DbUtil.CreateCommand(SQL_FIND_BY_KUNDEN_ID, DbUtil.CurrentConnection);
                    findByKundeIdCmd.Parameters.Add(DbUtil.CreateParameter("@KundeID", DbType.Int64));
                }

                ((IDataParameter)findByKundeIdCmd.Parameters["@KundeID"]).Value = kundenId;

                using (IDataReader rdr = findByKundeIdCmd.ExecuteReader()) {
                    IList<JahresDatenData> jahresDatenList = new List<JahresDatenData>();
                    while (rdr.Read()) {

                        jahresDatenList.Add( new JahresDatenData(
                                     (long)(int)rdr["JahresDatenID"],
                                     (long)(int)rdr["KundeID"],
                                     (double)rdr["Rechnungssumme"],
                                     (long)(int)rdr["ZaehlerStandAlt"],
                                     (long)(int)rdr["ZaehlerStandNeu"],
                                     (long)(int)rdr["Jahr"],
                                     (DateTime)rdr["Ablesedatum"],
                                     (double)rdr["BereitsBezahlt"]
                                    ));
                    }
                    return jahresDatenList;
                }
            } finally {
                DbUtil.CloseConnection();
            }
        }

        public long Insert(JahresDatenData jahresDatenData) {
            try {
                DbUtil.OpenConnection();

                if (insertByIdCmd == null) {

                    insertByIdCmd = DbUtil.CreateCommand(SQL_INSERT_BY_ID, DbUtil.CurrentConnection);
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@KundeID", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Rechnungssumme", DbType.Double));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@ZaehlerStandAlt", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@ZaehlerStandNeu", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Ablesedatum", DbType.DateTime));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Jahr", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@BereitsBezahlt", DbType.Double));
                }

                ((IDataParameter)insertByIdCmd.Parameters["@KundeID"]).Value = jahresDatenData.KundenId;
                ((IDataParameter)insertByIdCmd.Parameters["@Rechnungssumme"]).Value = jahresDatenData.Rechnungssumme;
                ((IDataParameter)insertByIdCmd.Parameters["@ZaehlerStandAlt"]).Value = jahresDatenData.ZaehlerStandAlt;
                ((IDataParameter)insertByIdCmd.Parameters["@ZaehlerStandNeu"]).Value = jahresDatenData.ZaehlerStandNeu;
                ((IDataParameter)insertByIdCmd.Parameters["@Ablesedatum"]).Value = jahresDatenData.AbleseDatum.Date;
                ((IDataParameter)insertByIdCmd.Parameters["@Jahr"]).Value = jahresDatenData.Jahr;
                ((IDataParameter)insertByIdCmd.Parameters["@BereitsBezahlt"]).Value = jahresDatenData.BereitsBezahlt;

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

        public bool Update(JahresDatenData jahresDatenData) {
            try {
                DbUtil.OpenConnection();

                if (updateByIdCmd == null) {
                    updateByIdCmd = DbUtil.CreateCommand(SQL_UPDATE_BY_ID, DbUtil.CurrentConnection);

                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@KundeID", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Rechnungssumme", DbType.Double));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@ZaehlerStandAlt", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@ZaehlerStandNeu", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Ablesedatum", DbType.DateTime));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Jahr", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@BereitsBezahlt", DbType.Double));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@JahresDatenID", DbType.Int64));
                }

                ((IDataParameter)updateByIdCmd.Parameters["@KundeID"]).Value = jahresDatenData.KundenId;
                ((IDataParameter)updateByIdCmd.Parameters["@Rechnungssumme"]).Value = jahresDatenData.Rechnungssumme;
                ((IDataParameter)updateByIdCmd.Parameters["@ZaehlerStandAlt"]).Value = jahresDatenData.ZaehlerStandAlt;
                ((IDataParameter)updateByIdCmd.Parameters["@ZaehlerStandNeu"]).Value = jahresDatenData.ZaehlerStandNeu;
                ((IDataParameter)updateByIdCmd.Parameters["@Ablesedatum"]).Value = jahresDatenData.AbleseDatum.Date;
                ((IDataParameter)updateByIdCmd.Parameters["@Jahr"]).Value = jahresDatenData.Jahr;
                ((IDataParameter)updateByIdCmd.Parameters["@BereitsBezahlt"]).Value = jahresDatenData.BereitsBezahlt;
                ((IDataParameter)updateByIdCmd.Parameters["@JahresDatenID"]).Value = jahresDatenData.Id;

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
                    deleteByIdCmd.Parameters.Add(DbUtil.CreateParameter("@JahresDatenID", DbType.Int64));
                }

                ((IDataParameter)deleteByIdCmd.Parameters["@JahresDatenID"]).Value = id;

                return deleteByIdCmd.ExecuteNonQuery() == 1;
            } finally {
                DbUtil.CloseConnection();
            }
        }
    }
}
