using System.ComponentModel.DataAnnotations.Schema;

namespace µMedlogr.core.Models;
public abstract class Entity {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}
