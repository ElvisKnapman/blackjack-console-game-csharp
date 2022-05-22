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
            // Deal hand to player
            GameLogic.DealHand(player, gameDeck);

            // Display Dealer Hand
            Console.WriteLine("Dealer Hand");
            Console.WriteLine("===========");
            DisplayDealerHand(dealer, gameDeck);
            Console.WriteLine();

            // Check first if Dealer has blackjack
            bool dealerHasBlackjack = GameLogic.CheckForBlackjack(dealer);

            // Check if player has blackjack
            bool playerHasBlackjack = GameLogic.CheckForBlackjack(player);

            // if dealer has blackjack and player also does, it's a push
            if (dealerHasBlackjack && playerHasBlackjack)
            {
                Console.WriteLine("Both Dealer and Player hav blackjack! It's a push.");
            }

            // if dealer has blackjack and player doesn't, dealer wins
            if (dealerHasBlackjack)
            {
                Console.WriteLine("Dealer has blackjack and wins the round.");
            }

            // if player has blackjack and dealer doesn't, player wins
            if (playerHasBlackjack)
            {
                Console.WriteLine("Player has blackjack and wins the round.");
            }


            bool isHitting = true;
            while (isHitting)
            {
                // Display Player Hand
                
                DisplayPlayerHand(player, gameDeck);


                // ask  player to hit or stand
                Console.WriteLine();



                Console.Write("What would you like to do? (hit/stand) ");
                string userInput = Console.ReadLine();
                Console.WriteLine();

                if (userInput.Trim().ToLower() == "hit")
                {
                    GameLogic.RequestCard(player);

                    // if hand is over 21, player loses
                    if (GameLogic.CheckIfHandValid(player) == false)
                    {
                        DisplayPlayerHand(player, gameDeck);
                        Console.WriteLine("Player Busts! Dealer wins the game.");
                        Console.ReadLine();
                        return;
                    }
                }
                else
                {
                    isHitting = false;
                    Console.WriteLine($"Player has stood with a hand of {GameLogic.CalculateHandValue(player)}");
                }


                // **** End of player flow ****


                // **** Start of dealer hand flow ****



                // dealer has to stand or hit

                // dealer will then bust or stand

                // check who wins between player and dealer hand values

            }

            Console.ReadLine();
        }


        private static void DisplayPlayerHand(PlayerModel player, List<DeckCardModel> deck)
        {
            Console.WriteLine($"{player.PlayerName}'s Hand");
            Console.WriteLine("============");
            // display hand to player
            foreach (var card in player.playerHand)
            {
                Console.WriteLine($"{card.Value} of {card.Suit}");
            }
            int handValue = GameLogic.CalculateHandValue(player);

            Console.WriteLine();
            Console.WriteLine($"Hand total: {handValue}");
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
