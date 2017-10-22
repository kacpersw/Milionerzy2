using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milionerzy.Models
{
    public class Question
    {
        public string Content { get; private set; }
        public List<string> Answers { get; private set; }
        public int Correct { get; private set; }
        public int Reject1 { get; set; }
        public int Reject2 { get; set; }
        public bool FiftyFifty { get; set; }

        public Question(string content, string A, string B, string C, string D, int correct, int reject1, int reject2)
        {
            this.Content = content;
            this.Answers = new List<string>
            {
                A,B,C,D
            };
            this.Correct = correct;
            this.Reject1 = reject1;
            this.Reject2 = reject2;
            this.FiftyFifty = false;
        }
    }
}
