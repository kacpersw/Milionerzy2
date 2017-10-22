﻿using Milionerzy.ConsoleApplication;
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

        public static void checkLifebuoy()
        {
            if (answer == 5)
            {
                gameContinue = false;
                money = game.endOfGame(false);
                RWResults.writeResults(name, money);
            }
            if (answer == 6 && !game.changeQuestion)
            {
                game.changeQuestionFunction();
            }
            if (answer == 7 && !game.fiftyFifty)
            {
                game.fiftyFiftyFunction();
                Menu.showQuestion(game.getQuestion(), game.getQuestion().Reject1, game.getQuestion().Reject2);
                answer = int.Parse(Console.ReadLine());
            }
            if (answer == 8 && !game.publicQuestion)
            {
                Menu.showPublicAnswers(game.publicQuestionFunction());
                answer = int.Parse(Console.ReadLine());
            }
            if (answer < 5)
            {
                gameContinue = game.checkAnswer(answer);
                if (!gameContinue)
                {
                    money = game.endOfGame(true);
                    RWResults.writeResults(name, money);
                }
                else
                {
                    if (game.currentLevel == 12)
                    {
                        gameContinue = false;
                        money = game.endOfGame(false);
                        Console.WriteLine("Wygrales");
                        RWResults.writeResults(name, money);
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
            ConsoleKeyInfo option;

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
                            answer = Menu.getAnswerItem(Menu.getAnswers(game.getQuestion()));
                            checkLifebuoy();
                            if (answer > 4)
                                checkLifebuoy();
                            if (answer > 4)
                                checkLifebuoy();

                        }
                        Console.WriteLine(money);
                        Console.ReadKey();
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