﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HelloDungeon
{

    class Game
    {
        string characterName = "";
        float currentArea = 1;
        bool gameOver = false;
        float health = 20;
        bool playerIsAlive = true;
        private int i;

        /// <summary>
        /// The starting room where the player gives their name, and has their first encounter.
        /// </summary>
        void Room1()
        {
            //Get the name from the player
            Console.Write("Please enter your name.");
            characterName = Console.ReadLine();
            Console.WriteLine("Hello, " + characterName);
            Console.ReadKey(true);
            Console.Clear();

            //Display text for the first encounter, and store the players decision
            string input = GetInput("You've been approached by a traveler!! " +
                "\n They offer you a potion. Do you accept?","Yes ", "No" );
           
            //If the player drinks the potion...
            if (input == "1" || input == "Yes" || input == "1.Yes")
            {
                //...kill the player
                Console.WriteLine("It was posion!! Ya dead shuuuunnnnn");
                playerIsAlive = false;
                gameOver = true;
            }
            //Otherwise if they do not...
            else if (input == "2" || input == "No" || input == "1.No")
            {
                //...display text to let the player know that they survived the first room
                Console.WriteLine("You decide to follow your gut and decline. You move on to the next area.");
                Console.ReadKey(true);
            }


        }

        /// <summary>
        /// The second room where the player is given a riddle to solve.
        /// </summary>
        void Room2()
        {
            int numberOfAttempts = 4;
            string input = "";

            //Loop until the player gets the riddle right or they run out of tries
            for (int i = 0; i < numberOfAttempts; i--)
            {
                Console.Clear();


                //Draws monkey character 
                Console.WriteLine("     __\n" +
                                   "w  c(..)o   (\n" +
                                   " \\__(-)   __)\n" +
                                   "    /|   (\n" +
                                   "   /(_)___)\n" +
                                   "  w /|\n" +
                                   "   \\  \n" +
                                   "    m m");

                //Prints a description of the situation for context
                Console.WriteLine("A very old man with a monkey on his back approaches you." +
                "\n The monkey offers you immortality if you can solve a riddle in " + numberOfAttempts + " attempts.");
                Console.WriteLine("What has to be broken before you can use it?");

                //Store the amount of attempts the player has remaining
                int attemptsRemaining = numberOfAttempts + i;

                //Displays the remaining number of attempts
                Console.WriteLine("Attempts Remaining: " + attemptsRemaining);

                //Get input for the players guess
                Console.Write("> ");
                input = Console.ReadLine();

                //If the player answered correctly...
                if (input.ToLower() == "egg")
                {
                    //...print text for feedback and break the loop
                    Console.WriteLine("Congrats! You've gained immortality!");
                    Console.ReadKey();
                    return;
                }
                
                //If the player has died after guessing
                else if (health <= 0)
                {
                    //...update the player state and print player feedback to the screen
                    Console.WriteLine("You died...");
                    Console.ReadKey();
                    Console.Clear();
                    playerIsAlive = false;
                    gameOver = true;
                    return;
                }

                //If the player doesn't answer correctly deal damage to them
                else
                {
                    Console.WriteLine("Incorrect! The monkey laughs at you! It hurts..." +
                    "you take 5 points of damage.");
                    Console.ReadKey();
                    health -= 5;
                }
                
            }
        }

        /// <summary>
        /// Prints the menu for restarting or exiting the game
        /// </summary>
        void DisplayMainMenu()
        {
            //Display question and store input
            string input = GetInput("Would you like to play again?", "Yes", "No");

            //If the player decides to restart...
            if (input == "1" || input == "Yes" || input == "1.Yes")
            {
                //...set their current area to be the start and update the player state to be alive
                currentArea = 1;
                gameOver = false;
                playerIsAlive = true;
                health = 20;
            }
            //Otherwise if the player wants to quit...
            else if (input == "2" || input == "No" || input == "1.No")
            {
                //...set game over to be true
                gameOver = true;
            }
        }

        /// <summary>
        /// Prints the text for the test room
        /// </summary>
        void Room3()
        {
            Console.Clear();
            Console.WriteLine("You've reached the end of your journey!");
            Console.ReadKey(true);
        }


        /// <summary>
        /// Gets an input from the player based on some given decision
        /// </summary>
        /// <param name="description">The context for the input</param>
        /// <param name="option1">The first option the player can choose</param>
        /// <param name="option2">The second option the player can choose</param>
        /// <returns></returns>
        string GetInput(string description, string option1, string option2)
        {
            string input = "";
            string inputReceived = "";

            //While input is not 1 or 2 display the options
            while (inputReceived != "1" && inputReceived != "2")
            {
                //Print options
                Console.Write(description);
                Console.Write("1." + option1);
                Console.Write("2." + option2);
                Console.Write("> ");

                //Get input from player
                input = Console.ReadLine();

                //If player selected the first option...
                if (input == "1" || input.ToLower() == "yes")
                {
                    //Set input received to be the first option
                    inputReceived = "1";
                }
                //Otherwise if the player selected the second option...
                else if (input == "2" || input.ToLower() == "no")
                {
                    //Set input received to be the second option
                    inputReceived = "2";
                }
                //If neither are true...
                else
                {
                    //...display error message
                    Console.WriteLine("Invalid Input");
                    Console.ReadKey();
                }

                Console.Clear();
            }

            return inputReceived;
        }

        public void Run()
        {
            //Loop while game isn't over
            while (gameOver == false)
            {
                //Print the current room to the screen
                if (currentArea == 1)
                {
                    Room1();
                    if (gameOver == true)
                    {
                        return;
                    }
                    currentArea++;
                }
                if (currentArea == 2)
                {
                    Room2();
                    if (gameOver == true)
                    {
                        return;
                    }
                    currentArea++;
                }
                if (currentArea == 3)
                {
                    Room3();
                }
            
                //If the player lost or beat the game...
                if (playerIsAlive == false || currentArea == 3)
                {
                    //...print main menu
                    DisplayMainMenu();
                }
                //Otherwise if the player is alive and hasn't finished...
                else
                {
                    //...increment the current area
                    currentArea++;
                }
            }
        }
    }
}
