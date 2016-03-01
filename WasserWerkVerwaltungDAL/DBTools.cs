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
                if (File.Exists(DbUtil.GetDatabaseFullFilename()))
                {
                    DialogResult dr = MessageBox.Show("Soll die bestehende Datenbank überschrieben werden?", "Achtung", MessageBoxButtons.OKCancel);
                    if (!(dr == DialogResult.OK))
                    {
                        return false;
                    }
                }
                File.Copy(filename, DbUtil.GetDatabaseFullFilename(), true);
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
