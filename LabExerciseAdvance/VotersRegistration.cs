﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public class VotersRegistration<T> : IRegistration<T> where T : Person
    {
        public List<T> RegisteredPersons { get; set; }
        public VotersRegistration()
        {
            RegisteredPersons = new List<T>();
        }

        public List<T> GetRegisteredPersons()
        {
            return RegisteredPersons.OrderBy(p => p.ID).ToList();
        }

        public bool IsPersonRegistered(T Person)
        {
            return RegisteredPersons.Any(p => p.ID == Person.ID);
        }

        public bool IsPersonValid(T Person)
        {
            if (!(Person is Adult))
            {
                throw new Exception("Person is not an Adult");
            }
            if (IsPersonRegistered(Person))
            {
                throw new Exception("Person is Already Registered");
            }
            if (Person.Age <= 16)
            {
                throw new Exception("Person Age is Minor to Register");
            }
            return true;
        }

        public void RegisterPerson(T Person) 
        {
            IsPersonValid(Person);
            RegisteredPersons.Add(Person);
        }

        public List<PersonView> SearchRegisteredPersons(string searchKey, string searchField)
        {
            return GetRegisteredPersons().ToPersonView().Search(searchKey, searchField).ToList();
        }

        public void UnregisterPerson(int personId)
        {
            T person = GetRegisteredPersons().SingleOrDefault(p => p.ID == personId);
            if (person == null || !IsPersonRegistered(person))
            {
                throw new Exception("Person is not Registered");
            }
            
            RegisteredPersons.Remove(person);
        }
    }
}
