using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public class CityRepository
    {
        private readonly List<City> _city;
        private int IdCount = 1;

        public CityRepository()
        {
            _city = new List<City>();
            LoadFromCSV("Cities.csv");
        }
        public List<City> GetList
        {
            get
            {
                return _city;
            }
        }
        public void LoadFromCSV(string fileName)
        {
            string fileUrl = Common.CheckCurrentDirectory(fileName);

            using (var reader = new StreamReader(fileUrl))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split('|');

                    Add(values);
                }
            }
        }
        public void Add(string[] values)
        {
            City city = new City
            {
                ID = IdCount,
                Name = values[0],
                Province = values[1],
                Region = values[2]
            };

            _city.Add(city);
            IdCount++;
        }
    }
}
