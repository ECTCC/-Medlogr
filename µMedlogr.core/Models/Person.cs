using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.core.Models;
internal class Person {
    public int Id { get; set; }
    public string NickName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public Nullable<float> WeightInKg { get; set; }
}
