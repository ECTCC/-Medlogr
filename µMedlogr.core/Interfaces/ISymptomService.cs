using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace µMedlogr.core.Interfaces;
public interface ISymptomService {

    public Task<IEnumerable<SymptomType>> GetAllSymptoms();
    public Task<SymptomType?> FindSymptom(int id);
}
