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

   
}
