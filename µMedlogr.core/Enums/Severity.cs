using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.core.Enums;
public enum Severity {
    //[Display(Description = "Severity0", ResourceType = typeof(System.Globalization.Severity))]
    None = 0,
    //[Display(Description = "Severity1", ResourceType = typeof(System.Globalization.Severity))]
    Mild = 1,
    //[Display(Description = "Severity2", ResourceType = typeof(System.Globalization.Severity))]
    Moderate = 2,
    //[Display(Description = "Severity3", ResourceType = typeof(System.Globalization.Severity))]
    Severe = 3,
    //[Display(Description = "Severity4", ResourceType = typeof(System.Globalization.Severity))]
    Intense = 4,
    //[Display(Description = "Severity5", ResourceType = typeof(System.Globalization.Severity))]
    Unbearable = 5,
    //[Display(Description = "Severity6", ResourceType = typeof(System.Globalization.Severity))]
    Maximal = 6
}
