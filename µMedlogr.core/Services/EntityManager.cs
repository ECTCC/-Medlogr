﻿using µMedlogr.core.Enums;
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
