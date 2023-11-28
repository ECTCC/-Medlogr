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
    internal async Task<bool> SaveSymptomMeasurement(SymptomMeasurement symptomMeasurement)
    {
        throw new NotImplementedException();
        //_context.Add(symptomMeasurement);
        //await _context.SaveChangesAsync();
        //return true;
    }

}
