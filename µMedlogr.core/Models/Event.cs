using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;

[PrimaryKey(nameof(Id))]
public class Event : Entity {
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime NotedAt { get; set; }
    public ICollection<Drug> AdministeredMedicines { get; set; } = [];
}
