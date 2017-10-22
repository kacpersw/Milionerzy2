using Milionerzy.Logic;
using Milionerzy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milionerzy.ConsoleApplication
{
    public static class Menu
    {
        public static string[] getLifebuoys(Game game)
        {
            string[] lifebuoys = new string[3];

            if (!game.fiftyFifty)
            {
                lifebuoys[0] = "Pół na pół";
            }
            else
            {
                lifebuoys[0] = string.Empty;
            }

            if (!game.publicQuestion)
            {
                lifebuoys[1] = "Pytanie do publiczności";
            }
            else
            {
                lifebuoys[1] = string.Empty;
            }

            if (!game.changeQuestion)
            {
                lifebuoys[2] = "Zmiana pytania";
            }
            else
            {
                lifebuoys[2] = string.Empty;
            }
            return lifebuoys;
        }

        public static int getAnswerItem(string[] answers, string[] lifebuoys, List<string> publicAnswers)
        {
            int count = 0;
            for(int i=0; i<lifebuoys.Length;i++)
            {
                if (lifebuoys[i] != string.Empty)
                    count++;
            }
            string[] inArray = new string[answers.Length + count];
            answers.CopyTo(inArray, 0);
            count = 0;
            for (int i = 0; i < lifebuoys.Length; i++)
            {
                if (lifebuoys[i] != string.Empty)
                {
                    inArray[answers.Length + count] = lifebuoys[i];
                    count++;
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop);

            bool loopComplete = false;
            int topOffset = Console.CursorTop;
            int bottomOffset = 0;
            int selectedItem = 0;
            int a = topOffset;
            ConsoleKeyInfo kb;
            string[] abcd = { "| A) ", "| B) ", "| C) ", "| D) ", "| ", "| ", "| ", "| " };

            Console.CursorVisible = false;

            //this will resise the console if the amount of elements in the list are too big
            if ((inArray.Length) > Console.WindowHeight)
            {
                throw new Exception("Too many items in the array to display");
            }

            /**
             * Drawing phase
             * */
            while (!loopComplete)
            {//This for loop prints the array out
                for (int i = 0; i < inArray.Length; i++)
                {

                    if (i == selectedItem && inArray[i] != string.Empty)
                    {//This section is what highlights the selected item

                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write(abcd[i] + inArray[i]);
                        writeSentence(abcd[i] + inArray[i]);
                        Console.ResetColor();
                        topOffset = Console.CursorTop;
                        Console.SetCursorPosition(0, topOffset);
                    }
                    else if (inArray[i] != string.Empty)
                    {//this section is what prints unselected items

                        Console.Write(abcd[i] + inArray[i]);
                        writeSentence(abcd[i] + inArray[i]);
                        topOffset = Console.CursorTop;
                        Console.SetCursorPosition(0, topOffset);
                    }
                    if (i == 3)
                    {
                        drawLine();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("| Koła ratunkowe:");
                        writeSentence("| Koła ratunkowe:");
                        Console.ResetColor();
                    }
                }
                drawLine();
                Console.WriteLine();
                if(publicAnswers != null)
                {
                    foreach (var publicAnswer in publicAnswers)
                    {
                        Console.Write(publicAnswer);
                        writeSentence(publicAnswer);
                    }
                    drawLine();
                }

                bottomOffset = Console.CursorTop;

                /*
                 * User input phase
                 * */

                kb = Console.ReadKey(true); //read the keyboard

                switch (kb.Key)
                { //react to input
                    case ConsoleKey.UpArrow:
                        if (selectedItem > 0)
                        {
                            selectedItem--;
                        }
                        else
                        {
                            selectedItem = (inArray.Length - 1);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedItem < (inArray.Length - 1))
                        {
                            selectedItem++;
                        }
                        else
                        {
                            selectedItem = 0;
                        }
                        break;

                    case ConsoleKey.Enter:
                        loopComplete = true;
                        break;
                }
                //Reset the cursor to the top of the screen
                Console.SetCursorPosition(0, a);
            }
            //set the cursor just after the menu so that the program can continue after the menu
            Console.SetCursorPosition(40, bottomOffset);

            Console.CursorVisible = true;
            if (selectedItem == 4)
            {
                if (lifebuoys[0] == string.Empty)
                {
                    if (lifebuoys[1] != string.Empty)
                        selectedItem = 5;
                    else
                        selectedItem = 6;
                }
            }
            else if (selectedItem == 5)
            {
                if (lifebuoys[0] == string.Empty)
                {
                    selectedItem = 6;
                }
                if (lifebuoys[1] == string.Empty)
                    selectedItem = 6;
            }


            return selectedItem;
        }


        public static int getMenuItems(string[] inArray)
        {
            Console.Clear();
            Console.SetCursorPosition(40, 7);

            bool loopComplete = false;
            int topOffset = Console.CursorTop;
            int bottomOffset = 0;
            int selectedItem = 0;
            ConsoleKeyInfo kb;


            Console.CursorVisible = false;

            //this will resise the console if the amount of elements in the list are too big
            if ((inArray.Length) > Console.WindowHeight)
            {
                throw new Exception("Too many items in the array to display");
            }

            /**
             * Drawing phase
             * */
            while (!loopComplete)
            {//This for loop prints the array out
                for (int i = 0; i < inArray.Length; i++)
                {

                    if (i == selectedItem)
                    {//This section is what highlights the selected item

                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(inArray[i]);
                        Console.ResetColor();
                        topOffset = Console.CursorTop;
                        Console.SetCursorPosition(40, topOffset);
                    }
                    else
                    {//this section is what prints unselected items

                        Console.WriteLine(inArray[i]);
                        topOffset = Console.CursorTop;
                        Console.SetCursorPosition(40, topOffset);
                    }
                }

                bottomOffset = Console.CursorTop;

                /*
                 * User input phase
                 * */

                kb = Console.ReadKey(true); //read the keyboard

                switch (kb.Key)
                { //react to input
                    case ConsoleKey.UpArrow:
                        if (selectedItem > 0)
                        {
                            selectedItem--;
                        }
                        else
                        {
                            selectedItem = (inArray.Length - 1);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (selectedItem < (inArray.Length - 1))
                        {
                            selectedItem++;
                        }
                        else
                        {
                            selectedItem = 0;
                        }
                        break;

                    case ConsoleKey.Enter:
                        loopComplete = true;
                        break;
                }
                //Reset the cursor to the top of the screen
                Console.SetCursorPosition(40, 7);
            }
            //set the cursor just after the menu so that the program can continue after the menu
            Console.SetCursorPosition(40, bottomOffset);

            Console.CursorVisible = true;
            return selectedItem;
        }

        public static void drawLine()
        {
            for (int i = 0; i < Console.WindowWidth - 1; i++)
            {
                Console.Write("-");
            }
        }
        public static void showQuestion(Question question)
        {
            drawLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("| " + question.Content);
            for (int i = question.Content.Length; i < Console.WindowWidth - 3; i++)
            {
                if (i < Console.WindowWidth - 4)
                {
                    Console.Write(" ");
                }
                else
                {
                    Console.Write("|");
                }
            }

            Console.ResetColor();
            Console.WriteLine();
            //Console.Write("| a) " + question.Answers[0]);
            //writeSentence(question.Answers[0]);
            //Console.Write("| b) " + question.Answers[1]);
            //writeSentence(question.Answers[1]);
            //Console.Write("| c) " + question.Answers[2]);
            //writeSentence(question.Answers[2]);
            //Console.Write("| d) " + question.Answers[3]);
            //writeSentence(question.Answers[3]);

            //drawLine();
            //Console.WriteLine();
        }

        public static void showLifebuoy(Game game)
        {
            Console.Write("| ");
        }

        public static void writeSentence(string sentence)
        {
            string s = string.Empty;
            for (int i = sentence.Length; i < Console.WindowWidth - 1; i++)
            {
                if (i < Console.WindowWidth - 2)
                {
                    s += " ";
                }
                else
                {
                    s += "|";
                }
            }
            Console.Write(s);
            Console.WriteLine();
        }

        public static string[] getAnswers(Question question)
        {
            return new string[] { question.Answers[0],
                question.Answers[1],
                question.Answers[2],
                question.Answers[3] };
        }

        public static void showMoney(int[] levels, int level)
        {
            Console.Clear();
            drawLine();
            Console.WriteLine();
            Console.Write("|");
            for (int i = 0; i < levels.Length; i++)
            {
                if (level == i)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(levels[i]);
                    Console.ResetColor();
                }
                else
                {
                    if (i == 1 || i == 6)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(levels[i]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(levels[i]);
                    }
                }
                if (i != levels.Length - 1)
                {
                    Console.Write("    ");
                }
                else
                {
                    Console.Write("|");
                }
            }
            Console.WriteLine();
        }

        public static void showQuestion(Question question, int firstAnswer, int secondAnswer)
        {
            Console.WriteLine(question.Content);
            if (firstAnswer != 0 && secondAnswer != 0)
            {
                Console.WriteLine("a) " + question.Answers[0]);
            }
            if (firstAnswer != 1 && secondAnswer != 1)
            {
                Console.WriteLine("b) " + question.Answers[1]);
            }
            if (firstAnswer != 2 && secondAnswer != 2)
            {
                Console.WriteLine("c) " + question.Answers[2]);
            }
            if (firstAnswer != 3 && secondAnswer != 3)
            {
                Console.WriteLine("d) " + question.Answers[3]);
            }
        }



        public static List<string> showPublicAnswers(int[] publicAnswers)
        {
            List<string> answers = new List<string>();

            if (publicAnswers[0] > 0)
                answers.Add("| Odpowiedź A: " + publicAnswers[0] + "%");

            if (publicAnswers[1] > 0)
                answers.Add("| Odpowiedź B: " + publicAnswers[1] + "%");

            if (publicAnswers[2] > 0)
                answers.Add("| Odpowiedź C: " + publicAnswers[2] + "%");

            if (publicAnswers[3] > 0)
                answers.Add("| Odpowiedź D: " + publicAnswers[3] + "%");

            return answers;
        }

        public static string[] getMenuItems()
        {
            return new string[] { "| Rozpocznij grę          |",
                "| Zobacz najlepsze wyniki |",
                "| Zmień imię              |",
                "| Zakończ                 |"};
        }

        public static string getName()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 7);
            Console.WriteLine("Podaj swoje imię");
            Console.SetCursorPosition(40, 9);
            return Console.ReadLine();
        }

        public static void showResults(List<Result> results)
        {
            Console.Clear();
            Console.WriteLine("Wyniki");
            for (int i = 0; i < results.Count; i++)
            {
                Console.WriteLine(i + 1 + "." + results[i].Name + "   " + results[i].Points);
            }
            Console.ReadKey();
        }
    }
}
