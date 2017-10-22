using Milionerzy.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milionerzy.Logic
{
    public static class RWQuestions
    {
        private static List<Question> questionList = new List<Question>();

        public static List<Question> loadQuestions()
        {
            questionList.Clear();
            List<Question> question = new List<Question>();
            Random rand = new Random();
            int index;

            for (int i = 1; i <= 4; i++)
            {
                question.Clear();

                using (StreamReader sr = new StreamReader("pytania" + i + ".txt"))
                {
                    do
                    {
                        question.Add(new Question(
                        sr.ReadLine(),
                        sr.ReadLine(),
                        sr.ReadLine(),
                        sr.ReadLine(),
                        sr.ReadLine(),
                        int.Parse(sr.ReadLine()),
                        int.Parse(sr.ReadLine()),
                        int.Parse(sr.ReadLine())
                        ));
                        sr.ReadLine();
                    } while (sr.EndOfStream == false);

                    for (int j = 0; j < 3; j++)
                    {
                        index = rand.Next(question.Count - 1);
                        questionList.Add(question[index]);
                        question.RemoveAt(index);
                    }
                }
            }
            index = rand.Next(question.Count - 1);
            questionList.Add(question[index]);
            question.RemoveAt(index);

            return questionList;
        }
    }
}
