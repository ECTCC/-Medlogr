using µMedlogr.core.Enums;
using µMedlogr.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.core.Services
{
    public class EntityManager(µMedlogrContext context)
    {
        private readonly µMedlogrContext _context = context;

        internal async Task<bool> CreateSymptomMeasurement(SymptomType symptom, Severity severity)
        {
            //if (symptom != null && severity > 0)
            //{
            //    if
            //}
            return true;
        }

    }
}
