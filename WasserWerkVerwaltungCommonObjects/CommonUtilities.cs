using System;
using System.Collections.Generic;
using System.Text;
using WasserWerkVerwaltung.CommonObjects;

namespace WasserWerkVerwaltung.CommonUtilities {

    //public class ListViewItemComparer : System.Collections.IComparer {
    //    private int col;
    //    public ListViewItemComparer() {
    //        col = 0;
    //    }
    //    public ListViewItemComparer(int column) {
    //        col = column;
    //    }

    //    public int Compare(object x, object y) {
    //        return String.Compare(((ListViewItem)x).SubItems[col].Text,
    //                              ((ListViewItem)y).SubItems[col].Text);
    //    }
    //}

//    public static string Math.formatedRound=function(wert,stellen){
//wert=Math.round(wert*Math.pow(10,stellen))/Math.po w(10,stellen);
//var wert_str=wert.toString();
//var wert_array=wert_str.split(".");
//while(wert_array[1].length<stellen){
//wert_array[1]+="0";
//}
//wert_str=wert_array[0]+"."+wert_array[1];
//return(wert_str);
//}

    public class StaticUtilities {

        public static int Rechnung(Rechnung rechnung) {
            return ((int)rechnung);
        }

        public static Rechnung Rechnung(int rechnung) {
            return (Rechnung)rechnung;
        }

        public static IList<KundenData> SortByNachname(IList<KundenData> kunden) {
            List<KundenData> list = new List<KundenData>(kunden);
            list.Sort(CompareKundenByNachname);
            return list;
        }

        public static int CompareKundenByNachname(KundenData x, KundenData y) {
            if (x == null) {
                if (y == null) {
                    return 0;
                } else {
                    return -1;
                }
            } else {
                if (y == null) {
                    return 1;
                } else {
                    return x.Nachname.CompareTo(y.Nachname);
                }
            }
        }

    //    public static string getAgeFromBirthDate(DateTime birthDate) {
    //        DateTime dt = new DateTime(DateTime.Now.Ticks - birthDate.Ticks);
    //        int years = dt.Year - 1;
    //        if (years >= 2) {
    //            return years.ToString();
    //        } else {
    //            double months = (1.0 / 12.0) * (double)(dt.Month - 1);
    //            months = Math.Round(months, 2);
    //            if (years == 0) {
    //                return months.ToString();
    //            } else {
    //                if (years == 1) {
    //                    return (months + 1.0).ToString();
    //                } else {
    //                    throw new NotSupportedException("Programm");
    //                }
    //            }
    //        }
    //        return (dt.Year - 1).ToString();
    //    }

    //    public static DateTime getBirthDateFromAge(int age) {
    //        return new DateTime(DateTime.Today.Year - age, DateTime.Today.Month, 1);
    //    }

    //    private static int CompareOperationsByDate(OperationData x, OperationData y) {
    //        if (x == null) {
    //            if (y == null) {
    //                return 0;
    //            } else {
    //                return -1;
    //            }
    //        } else {
    //            if (y == null) {
    //                return 1;
    //            } else {
    //                return x.Date.CompareTo(y.Date);
    //            }
    //        }
    //    }

    //    public static IList<OperationData> SortOperationListByDate(IList<OperationData> operations){
    //        List<OperationData> list = new List<OperationData>(operations);
    //        list.Sort(CompareOperationsByDate);
    //        return list;
    //    }

    //    private static int CompareVisitsByDate(VisitData x, VisitData y) {
    //        if (x == null) {
    //            if (y == null) {
    //                return 0;
    //            } else {
    //                return -1;
    //            }
    //        } else {
    //            if (y == null) {
    //                return 1;
    //            } else {
    //                return x.VisitDate.CompareTo(y.VisitDate);
    //            }
    //        }
    //    }

    //    public static IList<VisitData> SortVisitsListByDate(IList<VisitData> visits) {
    //        List<VisitData> list = new List<VisitData>(visits);
    //        list.Sort(CompareVisitsByDate);
    //        return list;
    //    }
    }

}
