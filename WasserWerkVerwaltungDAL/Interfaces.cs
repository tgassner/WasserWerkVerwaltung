using System;
using System.Collections.Generic;
using System.Text;
using WasserWerkVerwaltung.CommonObjects;

namespace WasserWerkVerwaltung.DAL {
    
    public interface IKunde {
        IList<KundenData> FindAll();
        KundenData FindByID(long id);
        long Insert(KundenData kunde);
        bool Update(KundenData kunde);
        bool Delete(long id);
    }

    public interface IJahresDaten {
        IList<JahresDatenData> FindAll();
        JahresDatenData FindByID(long id);
        long Insert(JahresDatenData kunde);
        bool Update(JahresDatenData kunde);
        bool Delete(long id);
    }

    public interface IPreis {
        IList<PreisData> FindAll();
        PreisData FindByJahr(long jahr);
        bool Insert(PreisData kunde);
        bool Update(PreisData kunde);
        bool Delete(long jahr);
    }
}
