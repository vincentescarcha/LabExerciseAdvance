using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public class Common
    {
        public static int CalculateAge(DateTime dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(dob).Ticks).Year - 1;
            return Years;
        }
        public static DateTime ParseDate(string dateOfBirth)
        {
            if (!DateTime.TryParseExact(dateOfBirth, "yyyyMMdd",
                          CultureInfo.InvariantCulture,
                          DateTimeStyles.None, out DateTime dob))
            {
                throw new Exception("Cant Convert String to Datetime");
            }
            return dob;
        }
        public static string CheckCurrentDirectory(string fileName)
        {
            string fileUrl = AppDomain.CurrentDomain.BaseDirectory + fileName;
            if (!File.Exists(fileUrl))
            {
                throw new Exception(fileName + " File Does not Exist");
            }
            else
            {
                return fileUrl;
            }
        }
    }
}
