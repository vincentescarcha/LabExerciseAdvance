using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public class SchoolRegistration<T> : IRegistration<T> where T : Person
    {
        public List<T> RegisteredPersons { get; set; }
        public SchoolRegistration()
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
            if (!(Person is Child))
            {
                throw new Exception("Person is not a child");
            }
            if (IsPersonRegistered(Person))
            {
                throw new Exception("Person is Already Registered");
            }
            return true;
        }

        public void RegisterPerson(T Person)
        {
            IsPersonValid(Person);
            RegisteredPersons.Add(Person);
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
