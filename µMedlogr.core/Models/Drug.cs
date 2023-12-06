using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using µMedlogr.core.Enums;

namespace µMedlogr.core.Models;
[PrimaryKey(nameof(Id))]
public class Drug {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string ActiveSubstance { get; set; }
    public Form Form { get; set; }
    public IEnumerable<Effect> Effects { get; set; } = Enumerable.Empty<Effect>();
}
