using Microsoft.AspNetCore.Identity;
using µMedlogr.core.Enums;
using µMedlogr.core.Models;
using Microsoft.IdentityModel.Tokens;
using System;

namespace µMedlogr.core.Services;
public class EntityManager
{
    private readonly µMedlogrContext _context;
    private readonly UserManager<AppUser> _userManager;

    public EntityManager(µMedlogrContext context, UserManager<AppUser> userManager)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
        _userManager = userManager;
    }

    internal async Task<SymptomMeasurement?> CreateSymptomMeasurement(int symptomId, Severity severity)
    {
        if (symptomId <= 0 || severity <= Severity.None || severity > Severity.Maximal)
        {
            return null;
        }
        var symptom = _context.SymptomTypes.Find(symptomId) ?? throw new NotImplementedException();

        var newMesurment = new SymptomMeasurement { Symptom = symptom, SubjectiveSeverity = severity };
        return newMesurment;
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

    internal async Task<bool> SaveNewHealthRecordEntry(HealthRecordEntry recordEntry)
    {
        if (recordEntry is null)
        {
            return false;
        }
        if (recordEntry.Id != 0)
        {
            return false;
        }
        _context.Add(recordEntry);
        await _context.SaveChangesAsync();
        return true;
    }
}
