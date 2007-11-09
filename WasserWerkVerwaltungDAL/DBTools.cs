using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WasserWerkVerwaltung.DAL {

    public class DBTools {
        //public static string GetDatabaseFile() {
        //    return WasserWerkVerwaltung.DAL.DBSettings.Default.DatabaseFile;
        //    //return "patient.mdb";
        //}

        //public static string GetDatabaseFullFilename() {
        //    return new FileInfo(Environment.GetCommandLineArgs()[0]).DirectoryName + "\\" + GetDatabaseFile();
        //    //return Environment.CurrentDirectory + "\\" + GetDatabaseFile();
        //}

        //private static string getDataBaseFileExtension() {
        //    return new FileInfo(GetDatabaseFile()).Extension;
        //}

        //private static string getDataBaseFilenameWithoutExtension() {
        //    return GetDatabaseFile().Substring(0, GetDatabaseFile().Length - getDataBaseFileExtension().Length);
        //}

        //private static string generateBackupFilename() {
        //    return getDataBaseFilenameWithoutExtension() + ".001." + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + getDataBaseFileExtension();
        //}

        //private static string incrementBackupFilename(string backupFilename) {
        //    string[] backupFileparts = backupFilename.Split('.');
        //    int number = Int32.Parse(backupFileparts[1]) + 1;
        //    backupFileparts[1] = number.ToString("000");
        //    return backupFileparts[0] + "." + backupFileparts[1] + "." + backupFileparts[2] + "." + backupFileparts[3];
        //}

        //public static string Backup(string backupPath) {
        //    WasserWerkVerwaltung.DAL.DBSettings.Default.BackupPath = backupPath;
        //    WasserWerkVerwaltung.DAL.DBSettings.Default.Save();
        //    string log = "";
        //    string test = generateBackupFilename();
        //    string[] backuppedFiles = Directory.GetFiles(GetBackupPath(), getDataBaseFilenameWithoutExtension() + ".*.*" + getDataBaseFileExtension());

        //    Array.Sort(backuppedFiles);

        //    try {
        //        for (int i = backuppedFiles.Length - 1; i >= 0; i--) {
        //            if (i >= 9) { //one has to be deleted
        //                File.Delete(GetBackupPath() + "\\" + backuppedFiles[i]);
        //                log = log + backuppedFiles[i] + " deleted\r\n";
        //            } else {
        //                File.Move(backuppedFiles[i], incrementBackupFilename(backuppedFiles[i]));
        //                log = log + backuppedFiles[i] + " to " + incrementBackupFilename(backuppedFiles[i]) + " renamed\r\n";
        //            }

        //        }

        //        File.Copy(GetDatabaseFullFilename(), GetBackupPath() + "\\" + generateBackupFilename());
        //        log = log + GetDatabaseFullFilename() + " to " + GetBackupPath() + "\\" + generateBackupFilename() + " copied\r\n";
        //    } catch (Exception e) {
        //        log = "Error: " + e.ToString() + log;
        //    }

        //    return log;
        //}

        //public static string GetBackupPath() {
        //    return WasserWerkVerwaltung.DAL.DBSettings.Default.BackupPath;
        //}

    }
}
