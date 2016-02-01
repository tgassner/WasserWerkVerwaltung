using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using System.Reflection;

namespace WasserWerkVerwaltung.DAL {
    internal class DbUtil {
        private static IDbConnection conn = null;
        private static int nestedOpens = 0;
        private static string connString = "";
        const string mdbFile = "wasser.mdb";

        private static IDbConnection GetCachedConnection() {
            if (conn == null) {
                if (String.IsNullOrEmpty(connString)) {
                    if (!File.Exists(GetDatabaseFullFilename()))
                    {
                        MessageBox.Show("Databasefile:" + Environment.NewLine + GetDatabaseFullFilename() + Environment.NewLine + "does not exist!");
                        //throw new Exception(null, "Databasefile:" + Environment.NewLine + mdbFile + Environment.NewLine + "does not exist!", null);
                    }
                    FileAttributes fileAttributesMdb = File.GetAttributes(GetDatabaseFullFilename());
                    if ((fileAttributesMdb & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        MessageBox.Show("Databasefile:" + Environment.NewLine + GetDatabaseFullFilename() + Environment.NewLine + "is Readonly!");
                        //throw new Exception(null, "Databasefile:" + Environment.NewLine + mdbFile + Environment.NewLine + "is Readonly!", null);
                    }
                    connString = String.Format("Data Source={0};Provider=Microsoft.Jet.OLEDB.4.0;", GetDatabaseFullFilename());
                }
                conn = new OleDbConnection(connString);
            }

            return conn;
        }

        public static string GetDatabaseFullFilename()
        {
            string path = getSPDLocalChangePath() + Path.DirectorySeparatorChar + mdbFile;

            if (!File.Exists(path))
            {
                if (File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + mdbFile))
                {
                    File.Copy(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + mdbFile, path);
                }
            }

            return path;
        }

        public static string getSPDLocalChangePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + Path.DirectorySeparatorChar +
                "wwv";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path;
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
