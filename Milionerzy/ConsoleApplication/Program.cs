using Milionerzy.ConsoleApplication;
using Milionerzy.Logic;
using Milionerzy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Milionerzy
{
    class Program
    {
        static string name;
        static Game game;
        static int answer;
        static int money;
        public static bool gameContinue;
        public static List<string> publicAnswers;
        public static List<int> rejects;

        public static void checkLifebuoy()
        {
            if (answer == 7)
            {
                gameContinue = false;
                money = game.endOfGame(false);
                Console.Clear();
                RWResults.writeResults(name, money);
            }
            if (answer == 6 && !game.changeQuestion)
            {
                game.changeQuestionFunction();
                Console.Clear();
            }
            if (answer == 4 && !game.fiftyFifty)
            {
                game.fiftyFiftyFunction();
                rejects = new List<int> { game.getQuestion().Reject1, game.getQuestion().Reject2 };
            }
            if (answer == 5 && !game.publicQuestion)
            {
                Console.WriteLine();
                publicAnswers = Menu.showPublicAnswers(game.publicQuestionFunction());
                if (game.getQuestion().FiftyFifty == true)
                    rejects = new List<int> { game.getQuestion().Reject1, game.getQuestion().Reject2 };

            }
            if (answer < 4)
            {
                gameContinue = game.checkAnswer(answer);
                if (!gameContinue)
                {
                    money = game.endOfGame(true);
                    Console.WriteLine();
                    Console.Write("| Niepoprawna odpowiedz!!! Twoja wygrana wynosi: " + money);
                    Menu.writeSentence("| Niepoprawna odpowiedz!!! Twoja wygrana wynosi: " + money);
                    Menu.drawLine();
                    Thread.Sleep(5000);
                    RWResults.writeResults(name, money);
                    RWResults.writeResults(name, money);
                }
                else
                {
                    if (game.currentLevel == 12)
                    {
                        gameContinue = false;
                        money = game.endOfGame(false);
                        Console.Write("| GRATULACJE!!! Twoja wygrana wynosi: " + money);
                        Menu.writeSentence("| GRATULACJE!!! Twoja wygrana wynosi: " + money);
                        Menu.drawLine();
                        Thread.Sleep(5000);
                        RWResults.writeResults(name, money);
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WindowWidth = 107;
            gameContinue = true;
            Console.SetCursorPosition(40, 7);
            Console.WriteLine("Podaj swoje imię");
            Console.SetCursorPosition(40, 9);
            name = Console.ReadLine();
            Console.Clear();

            int selectedItem = 0;
            while (true)
            {
                selectedItem = Menu.getMenuItems(Menu.getMenuItems());

                switch (selectedItem)
                {
                    case 0:
                        List<Question> q = RWQuestions.loadQuestions();
                        gameContinue = true;
                        game = new Game(q);
                        while (gameContinue)
                        {
                            Menu.showMoney(game.levels, game.currentLevel);
                            Menu.showQuestion(game.getQuestion());
                            answer = Menu.getAnswerItem(Menu.getAnswers(game.getQuestion()), Menu.getLifebuoys(game), publicAnswers, rejects, game);
                            publicAnswers = null;
                            rejects = null;
                            checkLifebuoy();
                            if (answer > 4 && answer!=7)
                                checkLifebuoy();
                            if (answer > 4 && answer!=7)
                                checkLifebuoy();

                        }
                        break;

                    case 1:
                        Menu.showResults(RWResults.readResults());
                        break;

                    case 2:
                        name = Menu.getName();
                        Console.Clear();
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }


            }
        }
    }
}
