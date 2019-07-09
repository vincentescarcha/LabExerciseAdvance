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
    }
}
