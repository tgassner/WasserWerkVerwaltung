using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using WasserWerkVerwaltung.CommonObjects;
using WasserWerkVerwaltung.DAL;

/*
public interface IPatient {
        ***IList<PatientData> FindAll();
        ***PatientData FindByID(long id);
        ***long Insert(PatientData patient);
        ***bool Update(PatientData patient);
        ***bool Delete(long id);

        IList<PatientData> FindDiagnose(string searchString);
        IList<PatientData> FindProcedure(string searchString);
        IList<PatientData> FindTreatment(string searchString);
        
        ***bool InsertFurtherTreatment(string ft,long pID);
        ***string GetFurtherTreatmentByPatentID(object pID);
    }

    public interface IVisit {
        IList<VisitData> FindAll();
        ***VisitData FindByID(long id);
        ***IList<VisitData> FindByPatientID(long id);
        ***long Insert(VisitData visit);
        bool Update(VisitData visit);
        ***bool Delete(long id);
    }
    
    public interface IOperation{
        IList<OperationData> FindAll();
        IList<OperationData> FindByPatientId(long patientId);
        ***OperationData FindByOperationId(long operationId);
        ***long Insert(OperationData odata);
        ***bool Delete(long operationId);
        bool Update(OperationData operation);
    }

    public interface IPhoto {
        IList<ImageData> FindAll();
        IList<ImageData> FindByPatientId(long patientId);
        ***ImageData FindByPhotoId(long photoID);
        ***long Insert(ImageData idata);
        ***bool Delete(long photoId);
        bool Update(ImageData photo);
    }
 
    ToDo Count
 */


namespace SimplePatientDocumentation.DAL.Tests {

    //[TestFixture]
    //public class VisitTest {

    //    private long pID;

    //    [SetUp]
    //    public void InitVisitTest() {
    //        IPatient patientDB = Database.CreatePatient();
    //        DateTime date = new DateTime(2006, 12, 24);
    //        PatientData patient = new PatientData(0, "first", "sure", date, Sex.male, "0123456789", 29, "address");
    //        pID = patientDB.Insert(patient);
    //    }

    //    [Test]
    //    public void InsertTest(){
    //        IVisit visitDB = Database.CreateVisit();
    //        VisitData visit = new VisitData(0,"good Cause", "lokkkalis", "extra Diagnossses", "procedurrre", "extratherapie", pID, DateTime.Now,"anest...","ultrasound","blooood","ToDo","Radiodiagnostic");
    //        long vID = visitDB.Insert(visit);
    //        VisitData visitWithID = new VisitData(vID, visit.Cause, visit.Localis, visit.ExtraDiagnosis, visit.Procedure, visit.ExtraTherapy, pID, visit.VisitDate, visit.Anesthesiology, visit.Ultrasound, visit.Blooddiagnostic,visit.Todo, visit.Radiodiagnostics);

    //        visit = visitDB.FindByID(vID);

    //        Assert.AreEqual(visit.Anesthesiology, visitWithID.Anesthesiology);
    //        Assert.AreEqual(visit.Blooddiagnostic, visitWithID.Blooddiagnostic);
    //        Assert.AreEqual(visit.Cause, visitWithID.Cause);
    //        Assert.AreEqual(visit.ExtraDiagnosis, visitWithID.ExtraDiagnosis);
    //        Assert.AreEqual(visit.ExtraTherapy, visitWithID.ExtraTherapy);
    //        Assert.AreEqual(visit.Id, vID);
    //        Assert.AreEqual(visit.Localis, visitWithID.Localis);
    //        Assert.AreEqual(visit.PatientId, pID);
    //        Assert.AreEqual(visit.Procedure, visitWithID.Procedure);
    //        Assert.AreEqual(visit.Ultrasound, visitWithID.Ultrasound);
    //        Assert.AreEqual(visit.VisitDate, visitWithID.VisitDate);
    //        Assert.AreEqual(visit.Todo, visitWithID.Todo);
    //        Assert.AreEqual(visit.Radiodiagnostics, visitWithID.Radiodiagnostics);

    //        Assert.IsTrue(visitDB.Delete(vID));

    //        Assert.IsNull(visitDB.FindByID(vID));
    //    }

    //    [Test]
    //    public void FindByPatientTest() {
    //        IVisit visitDB = Database.CreateVisit();
    //        VisitData visit1 = new VisitData(0, "good Cause", "lokkkalis", "extra Diagnossses", "procedurrre", "extratherapie", pID, DateTime.Now, "anest...", "ultrasound", "blooood","Todo","Radiodiagnasdn");
    //        long vID1 = visitDB.Insert(visit1);
    //        VisitData visitWithID1 = new VisitData(vID1, visit1.Cause, visit1.Localis, visit1.ExtraDiagnosis, visit1.Procedure, visit1.ExtraTherapy, pID, visit1.VisitDate, visit1.Anesthesiology, visit1.Ultrasound, visit1.Blooddiagnostic, visit1.Todo, visit1.Radiodiagnostics);
    //        VisitData visit2 = new VisitData(0, "asdfg", "nkjbjhbhj", "ejhij", "aölsdfjöasj", "laksdjalksd", pID, new DateTime(2007,12,01), "ikouhz...", "döner", "kljhg","asdas","asdasfd");
    //        long vID2 = visitDB.Insert(visit2);
    //        VisitData visitWithID2 = new VisitData(vID2, visit2.Cause, visit2.Localis, visit2.ExtraDiagnosis, visit2.Procedure, visit2.ExtraTherapy, pID, visit2.VisitDate, visit2.Anesthesiology, visit2.Ultrasound, visit2.Blooddiagnostic,visit2.Todo, visit2.Radiodiagnostics);
    //        VisitData visit3 = new VisitData(0, "öloiu", "kjhsbdklsw", "üüpüpü", "asüdkpasüd", "+*a", pID, new DateTime(2007, 12, 02), "pooip", "saddsf", "bloooodüüü","todoooo","Radioooo");
    //        long vID3 = visitDB.Insert(visit3);
    //        VisitData visitWithID3 = new VisitData(vID3, visit3.Cause, visit3.Localis, visit3.ExtraDiagnosis, visit3.Procedure, visit3.ExtraTherapy, pID, visit3.VisitDate, visit3.Anesthesiology, visit3.Ultrasound, visit3.Blooddiagnostic, visit3.Todo, visit3.Radiodiagnostics);

    //        IList<VisitData> visits = visitDB.FindByPatientID(pID);
    //        Assert.AreEqual(3, visits.Count);

    //        foreach (VisitData visit in visits) {
    //            Assert.IsTrue(visitDB.Delete(visit.Id));
    //        }

    //        visits = visitDB.FindByPatientID(pID);
    //        Assert.AreEqual(0,visits.Count);
    //    }

    //    [TearDown]
    //    public void TearDownOperationTest() {
    //        IPatient patientDB = Database.CreatePatient();
    //        patientDB.Delete(pID);
    //    }
    //}

    //[TestFixture]
    //public class OperationTest {

    //    private long pID;

    //    [SetUp]
    //    public void InitOperationTest() {
    //        IPatient patientDB = Database.CreatePatient();
    //        DateTime date = new DateTime(2006, 12, 24);
    //        PatientData patient = new PatientData(0, "first", "sure", date, Sex.male, "0123456789", 29, "address");
    //        pID = patientDB.Insert(patient);
    //    }

    //    [Test]
    //    public void InsertTest(){
    //        IOperation operationDB = Database.CreateOperation();
    //        OperationData operation = new OperationData(0, DateTime.Now,"Tolles Team","Toller Proccess","diagnosissss","alles performed",pID);
    //        long oID = operationDB.Insert(operation);
    //        OperationData operationWithID = new OperationData(oID,operation.Date,operation.Team,operation.Process,operation.Diagnoses,operation.Performed,pID);

    //        operation = operationDB.FindByOperationId(oID);

    //        Assert.AreEqual(operation.Date, operationWithID.Date);
    //        Assert.AreEqual(operation.Diagnoses, operationWithID.Diagnoses);
    //        Assert.AreEqual(operation.OperationId, operationWithID.OperationId);
    //        Assert.AreEqual(operation.PatientId, operationWithID.PatientId);
    //        Assert.AreEqual(operation.Performed, operationWithID.Performed);
    //        Assert.AreEqual(operation.Process, operationWithID.Process);
    //        Assert.AreEqual(operation.Team, operationWithID.Team);

    //        Assert.IsTrue(operationDB.Delete(oID));

    //        Assert.IsNull(operationDB.FindByOperationId(oID));
    //    }

    //    [TearDown]
    //    public void TearDownOperationTest() {
    //        IPatient patientDB = Database.CreatePatient();
    //        patientDB.Delete(pID);
    //    }
    //}

    //[TestFixture]
    //public class PhotoTest {

    //    private long pID;

    //    [SetUp]
    //    public void InitPhotoTest() {
    //        IPatient patientDB = Database.CreatePatient();
    //        DateTime date = new DateTime(2006, 12, 24);
    //        PatientData patient = new PatientData(0, "first", "sure", date, Sex.male, "0123456789", 29, "address");
    //        pID = patientDB.Insert(patient);
    //    }

    //    [Test]
    //    public void InsertTest() {
    //        IPhoto photoDB = Database.CreatePhoto();
    //        ImageData photo = new ImageData(0, pID, "c:\\image.jpg", "Gutes Foto");
    //        long iID = photoDB.Insert(photo);
    //        ImageData photoWithID = new ImageData(iID, photo.PatientID, photo.Link, photo.Kommentar);

    //        photo = photoDB.FindByPhotoId(iID);

    //        Assert.AreEqual(photo.Link, photoWithID.Link);
    //        Assert.AreEqual(photo.Kommentar, photoWithID.Kommentar);
    //        Assert.AreEqual(photo.PhotoID, photoWithID.PhotoID);
    //        Assert.AreEqual(photo.Link, photoWithID.Link);

    //        Assert.IsTrue(photoDB.Delete(iID));

    //        Assert.IsNull(photoDB.FindByPhotoId(iID));
    //    }

    //    [TearDown]
    //    public void TearDownPhotoTest() {
    //        IPatient patientDB = Database.CreatePatient();
    //        patientDB.Delete(pID);
    //    }
    //}

    //[TestFixture]
    //public class PatientTest {
        
    //    [Test]
    //    public void InsertTest(){
    //        IPatient patientDB = Database.CreatePatient();
    //        DateTime date = new DateTime(2006,12, 24);
    //        PatientData patient = new PatientData(0, "first", "sure", date, Sex.male, "0123456789",29,"address");
    //        long pID = patientDB.Insert(patient);
    //        PatientData pdataWithID = new PatientData(pID,
    //            patient.FirstName,patient.SurName,patient.DateOfBirth,
    //            patient.Sex, patient.Phone,patient.Weight, patient.Address);

    //        patient = patientDB.FindByID(pdataWithID.Id);
            
    //        Assert.AreEqual(patient.Id,pdataWithID.Id);
    //        Assert.AreEqual(patient.FirstName, pdataWithID.FirstName);
    //        Assert.AreEqual(patient.SurName, pdataWithID.SurName);
    //        Assert.AreEqual(patient.Phone, pdataWithID.Phone);
    //        Assert.AreEqual(patient.DateOfBirth, pdataWithID.DateOfBirth);
    //        Assert.AreEqual(patient.Sex, pdataWithID.Sex);
    //        Assert.AreEqual(patient.Weight, pdataWithID.Weight);
    //        Assert.AreEqual(patient.Address, pdataWithID.Address);
            
    //        Assert.IsTrue(patientDB.Delete(pID));
    //        Assert.IsNull(patientDB.FindByID(pID));
    //    }

    //    [Test]
    //    public void FurtherTreatmentTest() {
    //        IPatient patientDB = Database.CreatePatient();
    //        DateTime date = new DateTime(2006, 12, 24);
    //        PatientData patient = new PatientData(0, "first", "sure", date, Sex.male, "0123456789", 29, "address");
    //        long pID = patientDB.Insert(patient);
    //        PatientData pdataWithID = new PatientData(pID,
    //            patient.FirstName, patient.SurName, patient.DateOfBirth,
    //            patient.Sex, patient.Phone, patient.Weight, patient.Address);

    //        patientDB.InsertFurtherTreatment("Trallala Further Treatment", pID);
    //        Assert.AreEqual("Trallala Further Treatment",patientDB.GetFurtherTreatmentByPatentID(pID));

    //        patientDB.InsertFurtherTreatment("Döner", pID);
    //        Assert.AreEqual("Döner", patientDB.GetFurtherTreatmentByPatentID(pID));

    //        Assert.IsTrue(patientDB.Delete(pID));

    //        Assert.IsNull(patientDB.FindByID(pID));
    //    }

    //    [Test]
    //    public void UpdateTest() {
    //        IPatient patientDB = Database.CreatePatient();
    //        DateTime date = new DateTime(2006, 12, 24);
    //        PatientData patient = new PatientData(0, "first", "sure", date, Sex.male, "0123456789", 29, "address");
    //        long pID = patientDB.Insert(patient);
    //        PatientData pdataWithID = new PatientData(pID,
    //            patient.FirstName, patient.SurName, patient.DateOfBirth,
    //            patient.Sex, patient.Phone, patient.Weight, patient.Address);

    //        pdataWithID.Address = "Trallala";
    //        pdataWithID.DateOfBirth = new DateTime(2007, 05, 06);
    //        pdataWithID.FirstName = "asdf";
    //        pdataWithID.Phone = "0815";
    //        pdataWithID.Sex = Sex.female;
    //        pdataWithID.SurName = "asdasfd";
    //        pdataWithID.Weight = 35;

    //        patientDB.Update(pdataWithID);

    //        patient = patientDB.FindByID(pID);

    //        Assert.AreEqual(patient.Id, pdataWithID.Id);
    //        Assert.AreEqual(patient.FirstName, pdataWithID.FirstName);
    //        Assert.AreEqual(patient.Phone, pdataWithID.Phone);
    //        Assert.AreEqual(patient.SurName, pdataWithID.SurName);
    //        Assert.AreEqual(patient.DateOfBirth, pdataWithID.DateOfBirth);
    //        Assert.AreEqual(patient.Sex, pdataWithID.Sex);
    //        Assert.AreEqual(patient.Weight, pdataWithID.Weight);
    //        Assert.AreEqual(patient.Address, pdataWithID.Address);

    //        Assert.IsTrue(patientDB.Delete(pID));
    //        Assert.IsNull(patientDB.FindByID(pID));
    //    }

    //    [Test]
    //    public void FindAllTest() {
    //        IPatient patientDB = Database.CreatePatient();
    //        IList<PatientData> patients;
    //        patients = patientDB.FindAll();
    //        int originalCountOfPatients = patients.Count;
    //        DateTime date = new DateTime(2006, 12, 24);
    //        PatientData patient = new PatientData(0, "first", "sure", date, Sex.male, "0123456789", 29, "address");
    //        long pID = patientDB.Insert(patient);
    //        PatientData pdataWithID = new PatientData(pID,
    //            patient.FirstName, patient.SurName, patient.DateOfBirth,
    //            patient.Sex, patient.Phone, patient.Weight, patient.Address);

    //        patients = patientDB.FindAll();

    //        Assert.AreEqual(originalCountOfPatients + 1, patients.Count);

    //        bool exists = false;
    //        long maxPID = 0;
    //        int newCountOfPatients = patients.Count;

    //        foreach (PatientData patientlocal1 in patients) {
    //            if (patientlocal1.Id == pID) {
    //                exists = true;
    //            }
    //            if (patientlocal1.Id > maxPID) {
    //                maxPID = patientlocal1.Id;
    //            }
    //        }
    //        Assert.IsTrue(exists);

    //        IList<PatientData> patients2 = new List<PatientData>();
    //        for (int i = 0; i < (maxPID+1); i++) {
    //            PatientData patientlocal2 = patientDB.FindByID(i);
    //            if (patientlocal2 != null) {
    //                patients2.Add(patientlocal2);
    //            }
    //        }

    //        Assert.AreEqual(patients2.Count, patients.Count);

    //        Assert.IsTrue(patientDB.Delete(pID));
    //        Assert.IsNull(patientDB.FindByID(pID));

    //        patients = patientDB.FindAll();

    //        Assert.AreEqual(originalCountOfPatients, patients.Count);
    //        Assert.AreEqual(patients.Count + 1, newCountOfPatients);
            
    //    }

    //    private string getRandomString() {
    //        StringBuilder sb = new StringBuilder();
    //        Random rand = new Random();
    //        int count = rand.Next() % 20 + 1;
    //        for (int i = 0; i < count; i++) {
    //            char ch = (char)((rand.Next() % ('z' - 'a')) + 'a');
    //            sb.Append(ch);
    //        }
    //        return sb.ToString();
    //    }

    //    [Test]
    //    [Ignore]
    //    public void fillPatient() {
    //        Random rand = new Random();
    //        IPatient patientDB = Database.CreatePatient();
    //        Sex sex;
    //        for (int i = 0; i<5000;i++){
    //            if ((rand.Next() % 2) == 0){
    //                sex = Sex.female;
    //            }else{
    //                sex = Sex.male;
    //            }
    //            patientDB.Insert(new PatientData(0,getRandomString(),getRandomString(),new DateTime(rand.Next() % 10 + 1995,rand.Next() % 12 + 1,rand.Next() % 28 + 1),sex,getRandomString(),rand.Next() % 99,getRandomString()));
    //            if (i%50==0) Console.WriteLine(i.ToString());
    //        }
    //    }
    //}
}
