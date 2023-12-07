using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace µMedlogr.core.Services;
internal class PersonService : IEntityService<Person> {
    public bool Delete(Person entity) {
        throw new NotImplementedException();
    }

    public Person? Find(int key) {
        throw new NotImplementedException();
    }

    public IEnumerable<Person> GetAll() {
        throw new NotImplementedException();
    }

    public bool SaveAll(IEnumerable<Person> people) {
        throw new NotImplementedException();
    }

    public bool Update(Person entity) {
        throw new NotImplementedException();
    }
}
