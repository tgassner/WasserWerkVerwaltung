using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WasserWerkVerwaltung.DAL
{
    public class DbTools
    {
        public static bool DoDbBackup(string backupFileName)
        {
            try
            {
                File.Copy(DbUtil.GetDatabaseFullFilename(), backupFileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }

        public static bool DoDbImport(string filename) {
            try
            {
                File.Copy(filename, DbUtil.GetDatabaseFullFilename());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }
    }
}
