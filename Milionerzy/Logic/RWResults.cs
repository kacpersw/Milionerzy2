using Milionerzy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milionerzy.Logic
{
    public static class RWResults
    {
        public static List<Result> results = new List<Result>();

        public static void writeResults(string name, int money)
        {
            readResults();

            results.Add(new Result(
                name,
                money
                ));

            if(results.Count>=20)
            {
                results = results.OrderBy(x => -x.Points).Take(20).ToList();
            }
            else
            {
                results = results.OrderBy(x => x.Points).ToList();
            }

            using (StreamWriter sw = new StreamWriter("wyniki.txt"))
            {
                for (int i = 0; i < results.Count ; i++)
                {
                    sw.WriteLine(results[i].Name);
                    sw.WriteLine(results[i].Points);
                }
            }
        }

        public static List<Result> readResults()
        {
            results.Clear();

            using (StreamReader sr = new StreamReader("wyniki.txt"))
            {
                do
                {
                    results.Add(new Result(
                    sr.ReadLine(),
                    int.Parse(sr.ReadLine())
                    ));
                } while (sr.EndOfStream == false);
            }
            return results;
        }
    }
}
