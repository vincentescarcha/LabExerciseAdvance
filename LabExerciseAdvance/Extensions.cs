using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public static class Extensions
    {
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

        public static dynamic GroupPersons<T>(List<T> registeredPersons, string groupBy) where T : Person
        {
            if (groupBy == "City")
            {
                return registeredPersons.Join(Program.CityRepo.GetList, p => p.CityId, c => c.ID,
                    (p, c) => new
                    {
                        p.ID,
                        p.FirstName,
                        p.LastName,
                        DateOfBirth = p.DateOfBirth.ToString("MMM dd, yyyy"),
                        p.Age,
                        Gender = p.Gender.ToString(),
                        Status = p.Status.ToString(),
                        PersonType = p.GetType().Name,
                        City = c.Name,
                        c.Province,
                        c.Region
                    }
                    ).GroupBy(x => x.City);
            }
            else if (groupBy == "Province")
            {
                return registeredPersons.Join(Program.CityRepo.GetList, p => p.CityId, c => c.ID,
                    (p, c) => new
                    {
                        p.ID,
                        p.FirstName,
                        p.LastName,
                        DateOfBirth = p.DateOfBirth.ToString("MMM dd, yyyy"),
                        p.Age,
                        Gender = p.Gender.ToString(),
                        Status = p.Status.ToString(),
                        PersonType = p.GetType().Name,
                        City = c.Name,
                        c.Province,
                        c.Region
                    }
                    ).GroupBy(x => x.Province);
            }
            else if (groupBy == "Region")
            {
                return registeredPersons.Join(Program.CityRepo.GetList, p => p.CityId, c => c.ID,
                    (p, c) => new
                    {
                        p.ID,
                        p.FirstName,
                        p.LastName,
                        DateOfBirth = p.DateOfBirth.ToString("MMM dd, yyyy"),
                        p.Age,
                        Gender = p.Gender.ToString(),
                        Status = p.Status.ToString(),
                        PersonType = p.GetType().Name,
                        City = c.Name,
                        c.Province,
                        c.Region
                    }
                    ).GroupBy(x => x.Region);
            }
            else
            {
                throw new Exception("Invalid Group by field");
            }
        }
    }
}
