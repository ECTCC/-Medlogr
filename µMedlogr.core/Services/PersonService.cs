using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using Microsoft.AspNetCore.Identity;
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
        throw new NotImplementedException();
    }
    #endregion
    #region PersonService
    public async Task<bool> DeletePerson(Person person) {
        _context.People.Remove(person);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Person?> FindPerson(int personId) {
        return _context.People
            .Where(x => x.Id == personId)
            .Include(x => x.CareGivers)
            .FirstOrDefault();
    }

    public async Task<Person?> GetAppUsersPersonById(string userId) {
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
        if (HasValidData(person)) {
            _context.People.Add(person);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<bool> UpdatePerson(Person person) {
        if (HasValidData(person)) {
            _context.People.Update(person);
            return await _context.SaveChangesAsync() > 0;
        }
        return false;
    }
    #endregion

    private bool HasValidData(Person person) {
        return true;
    }
}
