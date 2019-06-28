using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public static class Extesions
    {
        public static CityRepository cities = new CityRepository();

        public static bool TryParseTo<T>(this object value, Type conversionType, out T output)
        {
            output = default(T);
            try
            {
                output = (T)Convert.ChangeType(value, conversionType);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IEnumerable<PersonView> ToPersonView(this IEnumerable<Person> registeredPersons) 
        {
            return registeredPersons.Join(cities.GetList, p => p.CityId, c => c.ID,
                (p, c) => new PersonView
                    {
                        ID = p.ID,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        DateOfBirth = p.DateOfBirth.ToString("MMM dd, yyyy"),
                        Age = p.Age,
                        Gender = p.Gender,
                        Status = p.Status,
                        PersonType = p.GetType().Name,
                        City = c.Name,
                        Province = c.Province,
                        Region = c.Region
                    }
                );
        }

        public static IEnumerable<PersonView> Search (this IEnumerable<PersonView> persons, string searchKey, string searchField)
        {
            if (searchField == "Age Range")
            {
                var range = searchKey.Split('-');
                return persons.Where(x => Convert.ToInt32(range[0]) <= x.Age &&
                            x.Age <= Convert.ToInt32(range[1])).ToList();
            }
            else if (searchField != "")
            {
                var propertyInfo = typeof(PersonView).GetProperty(searchField);
                return persons.Where(x => propertyInfo.GetValue(x, null).ToString().IndexOf(searchKey,
                        StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            else
            {
                return persons.Where(
                            p =>
                                p.FirstName.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                p.LastName.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                p.DateOfBirth.IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                p.Gender.ToString().IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0 ||
                                p.Status.ToString().IndexOf(searchKey, StringComparison.OrdinalIgnoreCase) >= 0
                            ).ToList();
            }
        }
        public static IEnumerable<IGrouping<string,PersonView>> Group(this IEnumerable<PersonView> persons, string groupingField)
        {
            var propertyInfo = typeof(PersonView).GetProperty(groupingField);
            return persons.GroupBy(x => propertyInfo.GetValue(x, null).ToString(),
                                        StringComparer.InvariantCultureIgnoreCase);
        }
    }
}
