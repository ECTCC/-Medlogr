using µMedlogr.core.Enums;
using µMedlogr.core.Models;

namespace µMedlogr.core.Services; 
public class EntityManager
{
    private readonly µMedlogrContext _context;

    public EntityManager(µMedlogrContext context) {
        ArgumentNullException.ThrowIfNull(context);
        _context = context; 
    }

    internal async Task<SymptomMeasurement?> CreateSymptomMeasurement(SymptomType symptom, Severity severity)
    {
        if (symptom is null || severity <(Severity) 1)
        {
            return null;
        }
        var newmesurment = new SymptomMeasurement { Symptom=symptom,SubjectiveSeverity=severity,TimeSymptomWasChecked=DateTime.Now};
        return newmesurment;
    }
    internal async Task<bool> SaveNewSymptomMeasurement(SymptomMeasurement newSymptomMeasurement)
    {
        if(newSymptomMeasurement is null) {
            return false;
        }
        //If Id is not 0 then the entity is not new
        if(newSymptomMeasurement.Id != 0) {
            return false;
        }
        _context.Add(newSymptomMeasurement);
        await _context.SaveChangesAsync();
        return true;
    }

}
