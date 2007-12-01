using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;


namespace WasserWerkVerwaltung.DAL {
    internal class DbUtil {
        private static IDbConnection conn = null;
        private static int nestedOpens = 0;
        private static string connString = "";

        private static IDbConnection GetCachedConnection() {
            if (conn == null) {
                if (connString.Equals("")) {
                    //if (!File.Exists(DBTools.GetDatabaseFullFilename())) {
                    //    MessageBox.Show("Databasefile: " + DBTools.GetDatabaseFullFilename() + "does not exist!");
                    //    Environment.Exit(1);
                    //}
                    if (!File.Exists("wasser.mdb")) {
                        MessageBox.Show("Databasefile: wasser.mdb does not exist!");
                        Environment.Exit(1);
                    }
                    connString = "Data Source=" + "wasser.mdb" + ";Provider=Microsoft.Jet.OLEDB.4.0;";
                }
                conn = new OleDbConnection(connString);
            }

            return conn;
        }

        public static IDbConnection OpenConnection() {
            IDbConnection conn = GetCachedConnection();
            if (conn.State != ConnectionState.Open) {
                conn.Open();
                nestedOpens = 0;
            }

            nestedOpens++;
            return conn;
        }

        public static IDbConnection CurrentConnection {
            get {
                return conn;
            }
        }

        public static void CloseConnection() {
            if (--nestedOpens == 0)
                conn.Close();
        }

        public static IDbCommand CreateCommand(string sqlStr, IDbConnection conn) {
            IDbCommand cmd = new OleDbCommand(sqlStr);
            cmd.Connection = conn;
            cmd.CommandText = sqlStr;
            return cmd;
        }

        public static IDbDataParameter CreateParameter(string pName, DbType dbType) {
            IDbDataParameter param = new OleDbParameter(pName, dbType);
            return param;
        }

        public static DbDataAdapter CreateAdapter(string selStr,
                                                  IDbConnection conn) {
            DbDataAdapter adpt = new OleDbDataAdapter();
            adpt.SelectCommand = (DbCommand)CreateCommand(selStr, conn);
            return adpt;
        }

        public static DbCommandBuilder CreateCommandBuilder(
                                         DbDataAdapter adpt) {
            DbCommandBuilder cmdBuilder = new OleDbCommandBuilder();
            cmdBuilder.DataAdapter = adpt;
            return cmdBuilder;
        }
    }
}
