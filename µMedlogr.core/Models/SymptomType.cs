using µMedlogr.core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.core.Models;
internal class SymptomType {
    public int Id { get; set; }
    public required string Name { get; set; }
    public Severity SubjectiveSeverity { get; set; }

}
