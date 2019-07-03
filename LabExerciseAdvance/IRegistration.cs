using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public interface IRegistration<T> where T : Person
    {
        List<T> RegisteredPersons { get; set; }
        void RegisterPerson(T Person);
        void UnregisterPerson(int personId);
        List<T> GetRegisteredPersons();
        List<PersonView> SearchRegisteredPersons(string FirstName, string LastName, string Gender,
                string Status, string City, string Province, string Region);
        bool IsPersonRegistered(T Person);
        bool IsPersonValid(T Person);
    }
}
