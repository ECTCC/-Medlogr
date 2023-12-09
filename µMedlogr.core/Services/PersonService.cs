using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core.Services;
public class PersonService(µMedlogrContext context) : IEntityService<Person>, IPersonService {
    private readonly µMedlogrContext _context = context;
    #region EntityService
    public Task<bool> Delete(Person entity) {
        throw new NotImplementedException();
    }

    public Task<Person?> Find(int key) {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Person>> GetAll() {
        throw new NotImplementedException();
    }

    public Task<bool> SaveAll(IEnumerable<Person> values) {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Person entity) {
        if (entity is null || entity.Id <= 0) {
            return Task.FromResult(false);
        }
        _context.Update<Person>(entity);
        return Task.Run(() => _context.SaveChanges() > 0);
    }
    #endregion
    #region PersonService
    public async Task<bool> DeletePerson(Person person) {
        _context.People.Remove(person);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Person?> FindPerson(int personId) {
        if(personId <= 0) {
            return null;
        }
        return _context.People
            .Where(x => x.Id == personId)
            .Include(x => x.CareGivers)
            .FirstOrDefault();
    }

    public async Task<Person?> GetAppUsersPersonById(string userId) {
        if(userId is null) {
            return null;
        }
        IQueryable<AppUser> appUser = _context.AppUsers
            .Include(x => x.Me)
            .ThenInclude(x => x.CareGivers)
            .Where(x => x.Id == userId);
        if (!appUser.Any()) {
            return null;
        }
        return await appUser.Select(x => x.Me).FirstOrDefaultAsync();
    }

    public async Task<bool> SavePerson(Person person) {
        if (person is not null && HasValidData(person) && person.Id == 0) {
            _context.People.Add(person);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<bool> UpdatePerson(Person person) {
        if (person is not null && HasValidData(person) && ExistsInDatabase<Person>(person.Id)) {
            _context.People.Update(person);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }
    #endregion

    private static bool HasValidData(Person person) {
        bool hasReasonableAge = person.DateOfBirth > new DateOnly(1920, 1, 1) && person.DateOfBirth < DateOnly.FromDateTime(DateTime.Now);
        bool hasReasonableWeight = person.WeightInKg > 2 && person.WeightInKg < 200;
        return hasReasonableAge && hasReasonableWeight;
    }
    private bool ExistsInDatabase<T>(int entityId) where T: Entity {
        return _context.Find<T>(entityId) is not null;
    }
}
