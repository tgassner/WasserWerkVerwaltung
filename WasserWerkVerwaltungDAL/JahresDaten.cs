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

        readonly string SQL_UPDATE_BY_ID = "UPDATE JahresDaten SET KundeID = ?, ZaehlerStandAlt = ?, ZaehlerStandNeu = ?, Ablesedatum = ?, Jahr = ?, BereitsBezahlt = ?, TauschZaehlerStandAlt = ?, TauschZaehlerStandNeu = ?, SonstigeForderungenText = ?, SonstigeForderungenValue = ?, HalbJahresBetrag = ?, RechnungsDatumHalbjahr = ?, RechnungsDatumJahr = ?, RechnungsNummerHalbjahr = ?, RechnungsNummerJahr = ? WHERE JahresDatenID = ?";
        readonly string SQL_INSERT_BY_ID = "INSERT INTO JahresDaten (KundeID, ZaehlerStandAlt, ZaehlerStandNeu, Ablesedatum, Jahr, BereitsBezahlt, TauschZaehlerStandAlt, TauschZaehlerStandNeu, SonstigeForderungenText, SonstigeForderungenValue, HalbJahresBetrag, RechnungsDatumHalbjahr, RechnungsDatumJahr, RechnungsNummerHalbjahr, RechnungsNummerJahr) VALUES(?,?,?,?,?,?,?,?,?,?,?,?,?, ?, ?)";
        readonly string SQL_DELETE_BY_ID = "DELETE FROM JahresDaten WHERE JahresDatenID = ?";
        static IDbCommand findByIdCmd;
        static IDbCommand findByKundeIdCmd;
        static IDbCommand findAllCmd;
        static IDbCommand updateByIdCmd;
        static IDbCommand insertByIdCmd;
        static IDbCommand deleteByIdCmd;
        static IDbCommand lastInsertedRowCmd;

        //JahresDatenID, KundeID, Rechnungssumme, ZaehlerStandAlt, ZaehlerStandNeu, Ablesedatum, Jahr, BereitsBezahlt
        // TauschZaehlerStandAlt, TauschZaehlerStandNeu, SonstigeForderungenText, SonstigeForderungenValue, HalbJahresBetrag
        //RechnungsDatumHalbjahr, RechnungsDatumJahr
        public IList<JahresDatenData> FindAll() {
            try {
                DbUtil.OpenConnection();

                if (findAllCmd == null) {
                    findAllCmd = DbUtil.CreateCommand(SQL_FIND_ALL, DbUtil.CurrentConnection);
                }

                using (IDataReader rdr = findAllCmd.ExecuteReader()) {
                    IList<JahresDatenData> jahresDatenList = new List<JahresDatenData>();
                    while (rdr.Read())
                    {
                        jahresDatenList.Add(fillJahresDaten(rdr));
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
                        return fillJahresDaten(rdr);
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
                        jahresDatenList.Add(fillJahresDaten(rdr));
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
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@ZaehlerStandAlt", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@ZaehlerStandNeu", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Ablesedatum", DbType.DateTime));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Jahr", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@BereitsBezahlt", DbType.Double));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@TauschZaehlerStandAlt", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@TauschZaehlerStandNeu", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@SonstigeForderungenText", DbType.String));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@SonstigeForderungenValue", DbType.Double));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@HalbJahresBetrag", DbType.Double));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@RechnungsDatumHalbjahr", DbType.DateTime));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@RechnungsDatumJahr", DbType.DateTime));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@RechnungsNummerHalbjahr", DbType.Int64));
                    insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@RechnungsNummerJahr", DbType.Int64));

                }

                ((IDataParameter)insertByIdCmd.Parameters["@KundeID"]).Value = jahresDatenData.KundenId;
                ((IDataParameter)insertByIdCmd.Parameters["@ZaehlerStandAlt"]).Value = jahresDatenData.ZaehlerStandAlt;
                ((IDataParameter)insertByIdCmd.Parameters["@ZaehlerStandNeu"]).Value = jahresDatenData.ZaehlerStandNeu;
                ((IDataParameter)insertByIdCmd.Parameters["@Ablesedatum"]).Value = jahresDatenData.AbleseDatum.Date;
                ((IDataParameter)insertByIdCmd.Parameters["@Jahr"]).Value = jahresDatenData.Jahr;
                ((IDataParameter)insertByIdCmd.Parameters["@BereitsBezahlt"]).Value = jahresDatenData.BereitsBezahlt;
                ((IDataParameter)insertByIdCmd.Parameters["@TauschZaehlerStandAlt"]).Value = jahresDatenData.TauschZaehlerStandAlt;
                ((IDataParameter)insertByIdCmd.Parameters["@TauschZaehlerStandNeu"]).Value = jahresDatenData.TauschZaehlerStandNeu;
                ((IDataParameter)insertByIdCmd.Parameters["@SonstigeForderungenText"]).Value = jahresDatenData.SonstigeForderungenText;
                ((IDataParameter)insertByIdCmd.Parameters["@SonstigeForderungenValue"]).Value = jahresDatenData.SonstigeForderungenValue;
                ((IDataParameter)insertByIdCmd.Parameters["@HalbJahresBetrag"]).Value = jahresDatenData.HalbJahresBetrag;

                if (jahresDatenData.RechnungsDatumHalbjahr.HasValue) {
                    ((IDataParameter)insertByIdCmd.Parameters["@RechnungsDatumHalbjahr"]).Value = jahresDatenData.RechnungsDatumHalbjahr.Value;
                } else {
                    ((IDataParameter)insertByIdCmd.Parameters["@RechnungsDatumHalbjahr"]).Value = DBNull.Value;
                }

                if (jahresDatenData.RechnungsDatumJahr.HasValue) {
                    ((IDataParameter)insertByIdCmd.Parameters["@RechnungsDatumJahr"]).Value = jahresDatenData.RechnungsDatumJahr.Value;
                } else {
                    ((IDataParameter)insertByIdCmd.Parameters["@RechnungsDatumJahr"]).Value = DBNull.Value;
                }

                if (jahresDatenData.RechnungsNummerHalbjahr == null) {
                    ((IDataParameter)insertByIdCmd.Parameters["@RechnungsNummerHalbjahr"]).Value = DBNull.Value;
                } else {
                    ((IDataParameter)insertByIdCmd.Parameters["@RechnungsNummerHalbjahr"]).Value = jahresDatenData.RechnungsNummerHalbjahr;
                }

                if (jahresDatenData.RechnungsNummerJahr == null) {
                    ((IDataParameter)insertByIdCmd.Parameters["@RechnungsNummerJahr"]).Value = DBNull.Value;
                } else {
                    ((IDataParameter)insertByIdCmd.Parameters["@RechnungsNummerJahr"]).Value = jahresDatenData.RechnungsNummerJahr;
                }
                
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
                Console.WriteLine("Update: " + jahresDatenData.ToString());
                if (updateByIdCmd == null) {
                    updateByIdCmd = DbUtil.CreateCommand(SQL_UPDATE_BY_ID, DbUtil.CurrentConnection);

                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@KundeID", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@ZaehlerStandAlt", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@ZaehlerStandNeu", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Ablesedatum", DbType.DateTime));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@Jahr", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@BereitsBezahlt", DbType.Double));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@TauschZaehlerStandAlt", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@TauschZaehlerStandNeu", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@SonstigeForderungenText", DbType.String));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@SonstigeForderungenValue", DbType.Double));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@HalbJahresBetrag", DbType.Double));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@RechnungsDatumHalbjahr", DbType.DateTime));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@RechnungsDatumJahr", DbType.DateTime));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@RechnungsNummerHalbjahr", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@RechnungsNummerJahr", DbType.Int64));
                    updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@JahresDatenID", DbType.Int64));
                }

                ((IDataParameter)updateByIdCmd.Parameters["@KundeID"]).Value = jahresDatenData.KundenId;
                ((IDataParameter)updateByIdCmd.Parameters["@ZaehlerStandAlt"]).Value = jahresDatenData.ZaehlerStandAlt;
                ((IDataParameter)updateByIdCmd.Parameters["@ZaehlerStandNeu"]).Value = jahresDatenData.ZaehlerStandNeu;
                ((IDataParameter)updateByIdCmd.Parameters["@Ablesedatum"]).Value = jahresDatenData.AbleseDatum.Date;
                ((IDataParameter)updateByIdCmd.Parameters["@Jahr"]).Value = jahresDatenData.Jahr;
                ((IDataParameter)updateByIdCmd.Parameters["@BereitsBezahlt"]).Value = jahresDatenData.BereitsBezahlt;
                ((IDataParameter)updateByIdCmd.Parameters["@TauschZaehlerStandAlt"]).Value = jahresDatenData.TauschZaehlerStandAlt;
                ((IDataParameter)updateByIdCmd.Parameters["@TauschZaehlerStandNeu"]).Value = jahresDatenData.TauschZaehlerStandNeu;
                ((IDataParameter)updateByIdCmd.Parameters["@SonstigeForderungenText"]).Value = jahresDatenData.SonstigeForderungenText;
                ((IDataParameter)updateByIdCmd.Parameters["@SonstigeForderungenValue"]).Value = jahresDatenData.SonstigeForderungenValue;
                ((IDataParameter)updateByIdCmd.Parameters["@HalbJahresBetrag"]).Value = jahresDatenData.HalbJahresBetrag;

                if (jahresDatenData.RechnungsDatumHalbjahr.HasValue) {
                    ((IDataParameter)updateByIdCmd.Parameters["@RechnungsDatumHalbjahr"]).Value = jahresDatenData.RechnungsDatumHalbjahr.Value.Date;
                } else {
                    ((IDataParameter)updateByIdCmd.Parameters["@RechnungsDatumHalbjahr"]).Value = DBNull.Value;
                }

                if (jahresDatenData.RechnungsDatumJahr.HasValue) {
                    ((IDataParameter)updateByIdCmd.Parameters["@RechnungsDatumJahr"]).Value = jahresDatenData.RechnungsDatumJahr.Value.Date;
                } else {
                    ((IDataParameter)updateByIdCmd.Parameters["@RechnungsDatumJahr"]).Value = DBNull.Value;
                }

                if (jahresDatenData.RechnungsNummerHalbjahr == null) {
                    ((IDataParameter)updateByIdCmd.Parameters["@RechnungsNummerHalbjahr"]).Value = DBNull.Value;
                } else {
                    ((IDataParameter)updateByIdCmd.Parameters["@RechnungsNummerHalbjahr"]).Value = jahresDatenData.RechnungsNummerHalbjahr;
                }

                if (jahresDatenData.RechnungsNummerJahr == null)
                {
                    ((IDataParameter)updateByIdCmd.Parameters["@RechnungsNummerJahr"]).Value = DBNull.Value;
                }
                else {
                    ((IDataParameter)updateByIdCmd.Parameters["@RechnungsNummerJahr"]).Value = jahresDatenData.RechnungsNummerJahr;
                }
                
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

        private static JahresDatenData fillJahresDaten(IDataReader rdr)
        {
            JahresDatenData jahresDatenData = new JahresDatenData();
            jahresDatenData.Id = Convert.ToInt64(rdr["JahresDatenID"]);
            jahresDatenData.KundenId = Convert.ToInt64(rdr["KundeID"]);
            jahresDatenData.ZaehlerStandAlt = Convert.ToInt64(rdr["ZaehlerStandAlt"]);
            jahresDatenData.ZaehlerStandNeu = Convert.ToInt64(rdr["ZaehlerStandNeu"]);
            jahresDatenData.Jahr = Convert.ToInt64(rdr["Jahr"]);
            jahresDatenData.AbleseDatum = (DateTime)rdr["Ablesedatum"];
            jahresDatenData.BereitsBezahlt = (double)rdr["BereitsBezahlt"];
            jahresDatenData.TauschZaehlerStandAlt = Convert.ToInt64(rdr["TauschZaehlerStandAlt"]);
            jahresDatenData.TauschZaehlerStandNeu = Convert.ToInt64(rdr["TauschZaehlerStandNeu"]);
            jahresDatenData.SonstigeForderungenText = Convert.ToString(rdr["SonstigeForderungenText"]);
            jahresDatenData.SonstigeForderungenValue = (double)rdr["SonstigeForderungenValue"];
            jahresDatenData.HalbJahresBetrag = (double)rdr["HalbJahresBetrag"];
            jahresDatenData.RechnungsDatumHalbjahr = DBNull.Value.Equals(rdr["RechnungsDatumHalbjahr"]) ? (DateTime?)null : (DateTime)rdr["RechnungsDatumHalbjahr"];
            jahresDatenData.RechnungsDatumJahr = DBNull.Value.Equals(rdr["RechnungsDatumJahr"]) ? (DateTime?)null : (DateTime)rdr["RechnungsDatumJahr"];
            jahresDatenData.RechnungsNummerHalbjahr = DBNull.Value.Equals(rdr["RechnungsNummerHalbjahr"]) ? (long?)null : Convert.ToInt64(rdr["RechnungsNummerHalbjahr"]);
            jahresDatenData.RechnungsNummerJahr = DBNull.Value.Equals(rdr["RechnungsNummerJahr"]) ? (long?)null : Convert.ToInt64(rdr["RechnungsNummerJahr"]);
            return jahresDatenData;
        }
    }
}
