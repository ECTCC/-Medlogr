using µMedlogr.core.Enums;
using µMedlogr.core.Models;

namespace µMedlogr.core.Services; 
public class EntityManager(µMedlogrContext context)
{
    private readonly µMedlogrContext _context = context;

    internal async Task<SymptomMeasurement> CreateSymptomMeasurement(SymptomType symptom, Severity severity)
    {
        //if (symptom != null && severity > 0)
        //{
        //    if
        //}
        return null;
    }

}
