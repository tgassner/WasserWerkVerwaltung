using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using WasserWerkVerwaltung.CommonObjects;
using System.Globalization;
using SimplePatientDocumentation.DAL;
using NUnit.Framework;

namespace WasserWerkVerwaltung.DAL {
    class Kunde : IKunde {

        //const string SQL_FIND_BY_ID = "SELECT * FROM kunde WHERE kundenID = ?";
        //const string SQL_FIND_ALL = "SELECT * FROM patient";
        //const string SQL_LAST_INSERTED_ROW = "SELECT @@Identity";
        //const string SQL_GET_FURTHER_TREATMENT_BY_ID = "SELECT furthertreatment from patient WHERE patientID = ?";
        //readonly string SQL_UPDATE_BY_ID = "UPDATE patient SET firstname = ?, surname = ?, birthdate = ?, sex = ?, phone = ?, weight = ?, address = ? WHERE patientID = ?";
        //readonly string SQL_INSERT_BY_ID = "INSERT INTO patient (firstname, surname, birthdate, sex, phone, furthertreatment, weight, address) VALUES(?,?,?,?,?,?,?,?)";
        //readonly string SQL_DELETE_BY_ID = "DELETE FROM patient WHERE patientID = ?";
        //static IDbCommand findByIdCmd;
        //static IDbCommand findAllCmd;
        //static IDbCommand updateByIdCmd;
        //static IDbCommand insertByIdCmd;
        //static IDbCommand deleteByIdCmd;
        //static IDbCommand lastInsertedRowCmd;
        
        public IList<KundenData> FindAll() {
        //    try {
        //        DbUtil.OpenConnection();

        //        if (findAllCmd == null) {
        //            findAllCmd = DbUtil.CreateCommand(SQL_FIND_ALL, DbUtil.CurrentConnection);
        //        }

                
                
        //        using (IDataReader rdr = findAllCmd.ExecuteReader()) {
        //            IList<KundenData> kundenList = new List<KundenData>();
        //            while (rdr.Read()) {
        //                Sex sex = Sex.male;
        //                if (((string)rdr["sex"]).Equals(Sex.male.ToString()))
        //                    sex = Sex.male;
        //                if (((string)rdr["sex"]).Equals(Sex.female.ToString()))
        //                    sex = Sex.female;
        //                long l = (long)(int)rdr["patientid"];

        //                kundenList.Add(new PatientData((long)(int)rdr["patientid"], (string)rdr["firstname"],
        //                                       (string)rdr["surname"], DateTime.Parse((string)rdr["birthdate"],
        //                                       DateTimeFormatInfo.InvariantInfo), sex, /*(string)rdr["city"], (string)rdr["street"],*/
        //                                       (string)rdr["phone"], (int)(Int16)rdr["weight"], (string)rdr["address"]));
        //            }
        //            return kundenList;
        //        }
        //    } finally {
        //        DbUtil.CloseConnection();
        //    }
            throw new NotImplementedException();
        }

        public KundenData FindByID(long id) {
        //    try {
        //        DbUtil.OpenConnection();

        //        if (findByIdCmd == null) {
        //            findByIdCmd = DbUtil.CreateCommand(SQL_FIND_BY_ID, DbUtil.CurrentConnection);
        //            findByIdCmd.Parameters.Add(DbUtil.CreateParameter("@patientId", DbType.Int64));
        //        }

        //        ((IDataParameter)findByIdCmd.Parameters["@patientId"]).Value = id;
                
        //        using (IDataReader rdr = findByIdCmd.ExecuteReader()) {
        //            IList<PatientData> patientList = new List<PatientData>();
        //            if (rdr.Read()) {
        //                Sex sex = Sex.male;
        //                if (((string)rdr["sex"]).Equals(Sex.male.ToString()))
        //                    sex = Sex.male;
        //                if (((string)rdr["sex"]).Equals(Sex.female.ToString()))
        //                    sex = Sex.female;

        //                return new PatientData((long)(int)rdr["patientid"], (string)rdr["firstname"],
        //                                       (string)rdr["surname"], DateTime.Parse((string)rdr["birthdate"],
        //                                       DateTimeFormatInfo.InvariantInfo), sex, (string)rdr["phone"],
        //                                       (int)(Int16)rdr["weight"], (string)rdr["address"]);
        //            }
        //        }
        //    } finally {
        //        DbUtil.CloseConnection();
        //    }
        //    return null;
            throw new NotImplementedException();
        }

        public long Insert(KundenData patient) {
        //    try {
        //        DbUtil.OpenConnection();

        //        if (insertByIdCmd == null) {
        //            insertByIdCmd = DbUtil.CreateCommand(SQL_INSERT_BY_ID, DbUtil.CurrentConnection);
        //            insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@fistname", DbType.String));
        //            insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@surname", DbType.String));
        //            insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@birthdate", DbType.String));
        //            insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@sex", DbType.String));
        //            insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@phone", DbType.String));
        //            insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@furthertreatment", DbType.String));
        //            insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@weight", DbType.Int64));
        //            insertByIdCmd.Parameters.Add(DbUtil.CreateParameter("@address", DbType.String));
        //        }

        //        ((IDataParameter)insertByIdCmd.Parameters["@fistname"]).Value = patient.FirstName;
        //        ((IDataParameter)insertByIdCmd.Parameters["@surname"]).Value = patient.SurName;
        //        ((IDataParameter)insertByIdCmd.Parameters["@birthdate"]).Value = patient.DateOfBirth.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
        //        ((IDataParameter)insertByIdCmd.Parameters["@sex"]).Value = patient.Sex.ToString();
        //        ((IDataParameter)insertByIdCmd.Parameters["@phone"]).Value = patient.Phone;
        //        ((IDataParameter)insertByIdCmd.Parameters["@furthertreatment"]).Value = "";
        //        ((IDataParameter)insertByIdCmd.Parameters["@weight"]).Value = patient.Weight;
        //        ((IDataParameter)insertByIdCmd.Parameters["@address"]).Value = patient.Address;


        //        if (insertByIdCmd.ExecuteNonQuery() != 1)
        //            return 0;
        //        else
        //        {
        //            if (lastInsertedRowCmd == null) {
        //                lastInsertedRowCmd = DbUtil.CreateCommand(SQL_LAST_INSERTED_ROW, DbUtil.CurrentConnection);
        //            }
        //            return (long)(int)lastInsertedRowCmd.ExecuteScalar();
        //        }
        //    } finally {
        //        DbUtil.CloseConnection();
        //    }
            throw new NotImplementedException();
        }

        public bool Update(KundenData patient) {
        //    try {
        //        DbUtil.OpenConnection();

        //        if (updateByIdCmd == null) {
        //            updateByIdCmd = DbUtil.CreateCommand(SQL_UPDATE_BY_ID, DbUtil.CurrentConnection);

        //            updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@firstname", DbType.String));
        //            updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@surname", DbType.String));
        //            updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@birthdate", DbType.String));
        //            updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@sex", DbType.String));
        //            updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@phone", DbType.String));
        //            updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@weight", DbType.Int64));
        //            updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@address", DbType.String));
        //            updateByIdCmd.Parameters.Add(DbUtil.CreateParameter("@patientID", DbType.Int64));
        //        }

        //        ((IDataParameter)updateByIdCmd.Parameters["@firstname"]).Value = patient.FirstName;
        //        ((IDataParameter)updateByIdCmd.Parameters["@surname"]).Value = patient.SurName;
        //        ((IDataParameter)updateByIdCmd.Parameters["@birthdate"]).Value = patient.DateOfBirth.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
        //        ((IDataParameter)updateByIdCmd.Parameters["@sex"]).Value = patient.Sex.ToString();
        //        ((IDataParameter)updateByIdCmd.Parameters["@phone"]).Value = patient.Phone;
        //        ((IDataParameter)updateByIdCmd.Parameters["@weight"]).Value = patient.Weight;
        //        ((IDataParameter)updateByIdCmd.Parameters["@address"]).Value = patient.Address;
        //        ((IDataParameter)updateByIdCmd.Parameters["@patientID"]).Value = patient.Id;

        //        return updateByIdCmd.ExecuteNonQuery() == 1;
        //    } finally {
        //        DbUtil.CloseConnection();
        //    }
            throw new NotImplementedException();
        }

        public bool Delete(long id) {
        //    try {
        //        DbUtil.OpenConnection();

        //        if (deleteByIdCmd == null) {
        //            deleteByIdCmd = DbUtil.CreateCommand(SQL_DELETE_BY_ID, DbUtil.CurrentConnection);
        //            deleteByIdCmd.Parameters.Add(DbUtil.CreateParameter("@patientID", DbType.Int64));
        //        }

        //        ////Don't delete if there are references to it
        //        //ISongsImAlbum songsImAlbum = Database.CreateSongsImAlbum();
        //        //IPreis preis = Database.CreatePreis();
        //        //IBilder bilder = Database.CreateBilder();
        //        //IRezension rezension = Database.CreateRezension();
        //        //IBesitzt besitzt = Database.CreateBesitzt();
        //        //IWiederverkauf wiederverkauf = Database.CreateWiederverkauf();
        //        //IList<SongsImAlbumData> songsimalbumList = songsImAlbum.FindBySongId(songId);
        //        //PreisData preisData = preis.FindNewestSongPrice(songId);
        //        //IList<BilderData> bilderList = bilder.FindBySongId(songId);
        //        //IList<RezensionData> rezensionList = rezension.FindBySong(songId);
        //        //IList<BesitztData> besitztList = besitzt.FindBySongId(songId);
        //        //IList<WiederverkaufData> wiederverkaufList = wiederverkauf.FindBySong(songId);
        //        //if ((songsimalbumList.Count > 0) || (preisData != null) ||
        //        //    (bilderList.Count > 0) || (rezensionList.Count > 0) ||
        //        //    (besitztList.Count > 0) || (wiederverkaufList.Count > 0))
        //        //    return false;

        //        ((IDataParameter)deleteByIdCmd.Parameters["@patientID"]).Value = id;

        //        return deleteByIdCmd.ExecuteNonQuery() == 1;
        //    } finally {
        //        DbUtil.CloseConnection();
        //    }
            throw new NotImplementedException();
        }

    }
}
