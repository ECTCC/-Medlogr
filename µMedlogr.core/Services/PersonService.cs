﻿using µMedlogr.core.Interfaces;
using µMedlogr.core.Models;

namespace µMedlogr.core.Services;
public class PersonService : IEntityService<Person> {
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
