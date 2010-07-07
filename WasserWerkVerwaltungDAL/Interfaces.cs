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
        IList<JahresDatenData> FindByKundenId(long kundenId);
        long Insert(JahresDatenData kunde);
        bool Update(JahresDatenData kunde);
        bool Delete(long id);
    }

    public interface IPreis {
        IList<PreisData> FindAll();
        PreisData FindByJahr(long jahr);
        bool Insert(PreisData preis);
        bool Update(PreisData preis);
        bool Delete(long preisId);
    }

    public interface IZahlung {
        IList<ZahlungData> FindAll();
        ZahlungData FindById(long id);
        ZahlungData FindByJahresDaten(long jahresDatenId);
        bool Insert(ZahlungData zahlung);
        bool Update(ZahlungData zahlung);
        bool Delete(long zahlungId);
    }
}
