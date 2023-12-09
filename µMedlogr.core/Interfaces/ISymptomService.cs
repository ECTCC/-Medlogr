using µMedlogr.core.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace µMedlogr.core.Interfaces;
internal interface ISymptomService {

    public Task<IEnumerable<SymptomType>> GetAllSymptoms();
}
