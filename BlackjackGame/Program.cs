using BlackjackGameLibrary;
using BlackjackGameLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WelcomeMessage();
            string playerName = GetPlayerName();
            PlayerModel player = new PlayerModel(playerName);
            PlayerModel dealer = new PlayerModel("Dealer", true);

            // create and shuffle a new deck for the game
            BlackjackDeckModel.InitializeDeck();
            var gameDeck = BlackjackDeckModel.shuffledDeck;

            // Deal hand to dealer
            GameLogic.DealHand(dealer, gameDeck);
            // Display Dealer Hand
            Console.WriteLine("Dealer Hand");
            Console.WriteLine("===========");
            DisplayDealerHand(dealer, gameDeck);
            Console.WriteLine();
            // Deal hand to player
            GameLogic.DealHand(player, gameDeck);
            // Display Player Hand
            Console.WriteLine($"{player.PlayerName}'s Hand");
            Console.WriteLine("============");
            DisplayPlayerHand(player, gameDeck);

            // ask to hit or stand
            Console.WriteLine();

            bool isHitting = true;

            while (isHitting)
            {

                Console.Write("What would you like to do? (hit/stand)");
                string userInput = Console.ReadLine();

                if (userInput.Trim().ToLower() == "hit")
                {
                    GameLogic.RequestCard(player);
                    DisplayPlayerHand(player, gameDeck);
                }
                else
                {
                    isHitting = false;
                }
            }


            Console.ReadLine();
        }


        private static void DisplayPlayerHand(PlayerModel player, List<DeckCardModel> deck)
        {
            // display hand to player
            foreach (var card in player.playerHand)
            {
                Console.WriteLine($"{card.Value} of {card.Suit}");
            }
        }

        private static void DisplayDealerHand(PlayerModel dealer, List<DeckCardModel> deck)
        {
            // display hand to dealer
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                {
                    Console.WriteLine("**Unknown**");
                }
                else
                {
                    Console.WriteLine($"{dealer.playerHand[i].Value} of {dealer.playerHand[i].Suit}");
                }
            }
        }

        private static string GetPlayerName()
        {

            bool isValidName = false;
            string userInput = "";

            while (isValidName == false)
            {
                Console.Write("What is your first name? ");
                userInput = Console.ReadLine();

                if (userInput.Trim().Length > 0)
                {
                    isValidName = true;
                }
            }
            Console.Clear();
            return userInput;

        }

        private static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to Blackjack!");
            Console.WriteLine("=====================");
            Console.WriteLine();
            Console.WriteLine("Created by Matt Knapman");

            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
            Console.Clear();
        }


    }
}
