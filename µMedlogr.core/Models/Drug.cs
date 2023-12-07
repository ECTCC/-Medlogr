using µMedlogr.core.Enums;
using Microsoft.EntityFrameworkCore;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class Drug : Entity {
    public required string Name { get; set; }
    public required string ActiveSubstance { get; set; }
    public Form Form { get; set; }
    public IEnumerable<Effect> Effects { get; set; } = Enumerable.Empty<Effect>();
}
