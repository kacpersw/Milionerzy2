using Milionerzy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milionerzy.Logic
{
    public class Game
    {
        public int[] levels { get; private set; }
        public int currentLevel { get; private set; }
        List<Question> questions;
        private int range;

        public bool changeQuestion { get; private set; }
        public bool publicQuestion { get; private set; }
        public bool fiftyFifty { get; private set; }

        public Game(List<Question> questions)
        {
            levels = new int[] { 500, 1000, 2000, 5000, 10000, 25000, 40000, 75000, 125000, 250000, 500000, 1000000 };
            currentLevel = 0;
            this.questions = questions;
            range = 0;
            changeQuestion = false;
            publicQuestion = false;
            fiftyFifty = false;
        }

        public void fiftyFiftyFunction()
        {
            if (!fiftyFifty)
            {
                questions[currentLevel].FiftyFifty = true;
                fiftyFifty = true;
            }
        }

        public void changeQuestionFunction()
        {
            if (!changeQuestion)
            {
                changeQuestion = true;
                questions.RemoveAt(currentLevel);
            }
        }

        public int[] publicQuestionFunction()
        {
            if (!publicQuestion)
            {
                publicQuestion = true;

                if (!questions[currentLevel].FiftyFifty)
                {
                    return random4Answers();
                }
                else
                {
                    return random2Answers();
                }
            }
            return null;
        }

        public int[] random2Answers()
        {
            Random rand = new Random();
            int[] randomAnswers = new int[4];
            int correctAnswerRandom = rand.Next(45, 100);

            for (int i = 0; i<4; i++)
            {
                if (questions[currentLevel].Correct == i)
                    randomAnswers[i] = correctAnswerRandom;
                
                else if(questions[currentLevel].Reject1 != i && questions[currentLevel].Reject2 != i)
                    randomAnswers[i] = 100 - correctAnswerRandom;
            }
           
            return randomAnswers;
        }

        public int[] random4Answers()
        {
            Random rand = new Random();
            int[] randomAnswers = new int[4];
            int correctAnswerRandom = rand.Next(30, 70);
            int incorrectAnswerRandom1 = 70 - correctAnswerRandom;
            int incorrectAnswerRandom2 = rand.Next(30);
            int incorrectAnswerRandom3 = 30 - incorrectAnswerRandom2;

            for (int i = 0; i < 4; i++)
            {
                if (questions[currentLevel].Reject1 == i)
                    randomAnswers[i] = incorrectAnswerRandom2;

                else if (questions[currentLevel].Reject2 == i)
                    randomAnswers[i] = incorrectAnswerRandom3;

                else if (questions[currentLevel].Correct == i)
                    randomAnswers[i] = correctAnswerRandom;

                else
                    randomAnswers[i] = incorrectAnswerRandom1;
            }

            return randomAnswers;
        }

        public Question getQuestion()
        {
            return questions[currentLevel];
        }

        public void checkRange()
        {
            if (currentLevel == 2)
                range = 1000;

            if (currentLevel == 8)
                range = 40000;
        }

        public bool checkAnswer(int answer)
        {
            if (answer == questions[currentLevel].Correct)
            {
                currentLevel++;
                checkRange();

                return true;
            }

            return false;
        }

        public int endOfGame(bool lose)
        {
            if (lose)
            {
                return range;
            }
            else
            {
                if(currentLevel>0)
                    return levels[currentLevel-1];
                else
                    return 0;
            }
        }
    }
}
