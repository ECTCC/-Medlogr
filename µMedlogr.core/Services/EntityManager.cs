using µMedlogr.core.Enums;
using µMedlogr.core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace µMedlogr.core.Services;
public class EntityManager {
    private readonly µMedlogrContext _context;
    private readonly UserManager<AppUser> _userManager;

    public EntityManager(µMedlogrContext context, UserManager<AppUser> userManager) {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
        _userManager = userManager;
    }
    #region Person
    internal Person? GetUserPerson(string userId) {
        if (userId == null) {
            return null;
        }
        //Cs8602 will never acrtually trigger since ThenInclude will never dereference null includes by design 
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return _context.AppUsers
            .Where(x => x.Id == userId)
            .Include(x => x.Me)
            .ThenInclude(x => x.HealthRecord)
            .Include(x => x.Me)
            .ThenInclude(x => x.CareGivers)
            .Select(x => x.Me)
            .SingleOrDefault();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
    public async Task<Person?> GetMeFromUser(AppUser appUser) {
        var userMe = await _context.Users
            .Where(x => x.Id == appUser.Id)
            .Include(x => x.Me)
            .FirstOrDefaultAsync();
        return userMe?.Me;
    }
    public async Task<Person?> GetOnePerson(int id) {
        var person = await _context.People.Where(x => x.Id == id).FirstOrDefaultAsync();
        return person;
    }
    public async Task<bool> SaveNewPerson(Person person) {
        if (person is null) {
            return false;
        }
        _context.Add(person);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> EditOnePerson(Person person, string nickname, DateOnly birthdate, float? weight, List<string> allergies) {
        person.DateOfBirth = birthdate;
        person.Allergies = allergies;
        person.NickName = nickname;
        person.WeightInKg = weight;
        var updatedPerson = await _context.SaveChangesAsync();
        if (updatedPerson >= 1) {
            return true;
        } else {
            return false;
        }
    }
    public async Task<bool> DeleteOnePerson(Person person) {
        var removePerson = _context.People.Remove(person);
        if (removePerson.Entity is not null) {
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task<List<Person>> GetJunctionData(AppUser appUser) {
        var userWithPeople = _context.Users
            .Where(x => x.Id == appUser.Id)
            .Include(x => x.PeopleInCareOf)
            .ThenInclude(x => x.CareGivers)
            .FirstOrDefault();
        if (userWithPeople is not null && userWithPeople.PeopleInCareOf is not null) {
            var caregiverForPeople = userWithPeople.PeopleInCareOf
        .Where(p => p.CareGivers.Any(x => x.Id == appUser.Id))
        .ToList();

            return caregiverForPeople;
        }
        return new List<Person>();
    }
    #endregion
    #region HealthRecord
    internal async Task<bool> SaveNewHealthRecordEntry(HealthRecordEntry recordEntry) {
        if (recordEntry is null) {
            return false;
        }
        if (recordEntry.Id != 0) {
            return false;
        }
        _context.Add(recordEntry);
        await _context.SaveChangesAsync();
        return true;
    }
    internal HealthRecord? GetHealthRecordById(string userId) {
        if (userId == null) {
            return null;
        }
        return _context.AppUsers
            .Where(x => x.Id == userId)
            .Select(x => x.Me)
            .Select(x => x.HealthRecord)
            .SingleOrDefault();
    }
    internal bool AddTemperatureData(int healthRecordId, float temperature, string? notes) {
        //Valid Health record Id?
        if (healthRecordId <= 0) {
            return false;
        }

        //Valid Temperature?
        //Note has right length?

        //Is HealthRecord In database?
        var healthRecord = _context.HealthRecords.FirstOrDefault(x => x.Id == healthRecordId);
        if (healthRecord != null) {
            var tempdata = new TemperatureData() { Measurement = temperature, Comments = notes, TimeOfMeasurement = DateTime.Now };
            healthRecord.Temperatures.Add(tempdata);

            _context.Attach(healthRecord);
            var updatedEntries = _context.SaveChanges();
            if (updatedEntries >= 1) {
                return true;
            }
        }
        return false;
    }
    internal async Task<SymptomMeasurement?> CreateSymptomMeasurement(int symptomId, Severity severity) {
        if (symptomId <= 0 || severity <= Severity.None || severity > Severity.Maximal) {
            return null;
        }
        var symptom = _context.SymptomTypes.Find(symptomId) ?? throw new NotImplementedException();

        var newMesurment = new SymptomMeasurement { Symptom = symptom, SubjectiveSeverity = severity };
        return newMesurment;
    }
    internal async Task<bool> SaveNewSymptomMeasurement(SymptomMeasurement newSymptomMeasurement) {
        if (newSymptomMeasurement is null) {
            return false;
        }
        //If Id is not 0 then the entity is not new
        if (newSymptomMeasurement.Id != 0) {
            return false;
        }
        //_context.Attach(newSymptomMeasurement.Symptom);
        _context.Add(newSymptomMeasurement);
        await _context.SaveChangesAsync();
        return true;
    }
    internal IEnumerable<TemperatureData> GetTemperatureDataByHealthRecordId(int healthRecordId) {
        return _context.HealthRecords.Where(record => record.Id == healthRecordId).SelectMany(record => record.Temperatures).AsEnumerable();
    }
    internal async Task<bool> SaveNewTemperatureData(TemperatureData data) {
        if (data == null || data.Id != 0) {
            return false;
        }
        _context.Add(data);
        var updatedEntries = await _context.SaveChangesAsync();
        if (updatedEntries > 0) {
            return true;
        }
        return false;
    }
    #endregion
    #region Generic
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

    internal async Task<bool> UpdateEntity<T>(T entity) where T : class {
        if (entity == null) {
            return false;
        }
        _context.Update<T>(entity);
        var task = _context.SaveChangesAsync();
        if (task != null) {
            return await task > 0;
        }
        return false;
    }
    internal bool SaveEntity<T>() {
        throw new NotImplementedException();
    }
    #endregion

    internal Event CreateEvent(string title, string description, DateTime time, List<int>? drugIds) {
        //Uppdatera healthrecord i context
        _context.SaveChanges();


        var drugs = new List<Drug>();
        if (!drugIds.IsNullOrEmpty()) {
            drugs = [.. _context.Drugs.Where(drug => drugIds!.Contains(drug.Id))];
        }
        return new Event() { Title = title, Description = description, NotedAt = time, AdministeredMedicines = drugs };
    }

}

