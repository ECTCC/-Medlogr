using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;

[PrimaryKey(nameof(Id))]
public class Event {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime NotedAt { get; set; }
    public ICollection<Drug> AdministeredMedicines { get; set; } = [];
}
