using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabExerciseAdvance
{
    public class Adult : Person
    {
        public Adult()
        {

        }
        public bool Employed { get; set; }
        public string JobTitle { get; set; }
    }
}
