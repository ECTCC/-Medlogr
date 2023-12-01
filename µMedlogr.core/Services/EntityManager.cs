using µMedlogr.core.Enums;
using µMedlogr.core.Models;

namespace µMedlogr.core.Services;
public class EntityManager
{
    private readonly µMedlogrContext _context;

    public EntityManager(µMedlogrContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    internal async Task<SymptomMeasurement?> CreateSymptomMeasurement(int symptomId, Severity severity)
    {
        if (symptomId <= 0 || severity <= Severity.None || severity > Severity.Maximal)
        {
            return null;
        }
        var symptom = _context.SymptomTypes.Find(symptomId) ?? throw new NotImplementedException();

        var newmesurment = new SymptomMeasurement { Symptom = symptom, SubjectiveSeverity = severity, TimeSymptomWasChecked = DateTime.Now };
        return newmesurment;
    }

    internal IEnumerable<TemperatureData> GetTemperatureDataByHealthRecordId(int healthRecordId) {
        return _context.HealthRecords.Where(record => record.Id == healthRecordId).SelectMany(record => record.Temperatures).AsEnumerable();
    }

    internal bool AddTemperatureData(int healthRecordId, float temperature, string? notes) {
        //Valid Health record Id?
        if(healthRecordId <= 0) {
            return false;
        }

        //Valid Temperature?
        //Note has right length?

        //Is HealthRecord In database?
        var healthRecord = _context.HealthRecords.FirstOrDefault(x => x.Id == healthRecordId);
        if(healthRecord != null) {
            var tempdata = new TemperatureData() { Measurement = temperature, Comments=notes, TimeOfMeasurement=DateTime.Now};
            healthRecord.Temperatures.Add(tempdata);

            _context.Attach(healthRecord);
            var updatedEntries = _context.SaveChanges();
            if(updatedEntries >= 1) {
                return true;
            }
        }
        return false;
    }

    internal async Task<bool> SaveNewTemperatureData(TemperatureData data) {
        if (data == null || data.Id != 0) {
            return false;
        }
        _context.Add(data);
        var updatedEntries = await _context.SaveChangesAsync();
        if(updatedEntries > 0) {
            return true;
        }
        return false;
    }

    //internal bool Delete<T>(object id) where T : class {
    //    if (id == null || id == default ) {
    //        return false;
    //    }
    //    var entity = _context.Find<T>(id);
    //    if (entity == null) {
    //        return false;
    //    }
    //    _context.Remove(entity);
    //    return true;
    //}


    internal async Task<bool> SaveNewSymptomMeasurement(SymptomMeasurement newSymptomMeasurement)
    {
        if (newSymptomMeasurement is null)
        {
            return false;
        }
        //If Id is not 0 then the entity is not new
        if (newSymptomMeasurement.Id != 0)
        {
            return false;
        }
        //_context.Attach(newSymptomMeasurement.Symptom);
        _context.Add(newSymptomMeasurement);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> SaveNewPerson(Person person)
    {
        if (person is null)
        {
            return false;
        }
        _context.Add(person);
        await _context.SaveChangesAsync();
        return true;
    }
}
