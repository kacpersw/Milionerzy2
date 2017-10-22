using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milionerzy.Models
{
    public class Result
    {
        public string Name { get; private set; }
        public int Points { get; private set;}


        public Result(string name, int points)
        {
            Name = name;
            Points = points;
        }

    }
}
