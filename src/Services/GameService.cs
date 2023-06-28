using System;
using Blackjack.Models;
using Blackjack.Enums;

namespace Blackjack.Services
{
	public class GameService
	{
		public void PlayGame()
		{
			var shoe = new Shoe(8); // Create a shoe with 8 decks
			var playerHand = new Hand(); // Player's hand
			var dealerHand = new Hand(); // Dealer's hand
			var runningCount = 0; // Running count for card counting

			while (true)
			{
				Console.WriteLine("=== New Hand ===");

				// Deal two cards to the player
				playerHand.AddCard(shoe.DrawCard());
				playerHand.AddCard(shoe.DrawCard());

				// Deal two cards to the dealer
				dealerHand.AddCard(shoe.DrawCard());
				dealerHand.AddCard(shoe.DrawCard());

				Console.WriteLine($"Player's Hand: {playerHand.PartialString()}");
				Console.WriteLine($"Dealer's Hand: {dealerHand.PartialString(true)}");

				// Provide recommendation based on the running count
				var recommendation = CardCountingHelper.GetRecommendation(runningCount, playerHand, dealerHand.Cards[0]);
				Console.WriteLine("");
				Console.WriteLine($"Recommendation: {recommendation}");
				Console.WriteLine("");

				// Check if the player or dealer has blackjack
				if (playerHand.IsBlackjack && dealerHand.IsBlackjack)
				{
					Console.WriteLine("=== Push! Both player and dealer have blackjack. ===");
				}
				else if (playerHand.IsBlackjack)
				{
					Console.WriteLine("+++ Player wins with blackjack! +++");
				}
				else if (dealerHand.IsBlackjack)
				{
					Console.WriteLine("--- Dealer wins with blackjack! ---");
				}
				else
				{
					// Player's turn
					while (true)
					{
						var playerAction = GetPlayerAction();
						if (playerAction == PlayerAction.Hit)
						{
							playerHand.AddCard(shoe.DrawCard());
							Console.WriteLine($"Player's Hand: {playerHand}");

							if (playerHand.IsBust)
							{
								Console.WriteLine("");
								Console.WriteLine("--- Player busts! Dealer wins. ---");
								Console.WriteLine("");
								break;
							}

							// Provide updated recommendation based on the new card
							recommendation = CardCountingHelper.GetRecommendation(runningCount, playerHand, dealerHand.Cards[0]);
							Console.WriteLine("");
							Console.WriteLine($"Recommendation: {recommendation}");
							Console.WriteLine("");
						}
						else if (playerAction == PlayerAction.Stand)
						{
							// Dealer's turn
							while (dealerHand.GetTotal() < 17)
							{
								dealerHand.AddCard(shoe.DrawCard());
								Console.WriteLine($"Dealer's Hand: {dealerHand}");

								if (dealerHand.IsBust)
								{
									Console.WriteLine("");
									Console.WriteLine("Dealer busts! Player wins.");
									Console.WriteLine("");
									break;
								}
							}

							Console.WriteLine("");
							if (!dealerHand.IsBust)
							{
								Console.WriteLine($"Dealer's Hand: {dealerHand}");

								if (playerHand.GetTotal() > dealerHand.GetTotal())
								{
									Console.WriteLine("+++ Player wins! +++");
								}
								else if (playerHand.GetTotal() < dealerHand.GetTotal())
								{
									Console.WriteLine("--- Dealer wins! ---");
								}
								else
								{
									Console.WriteLine("=== Push! It's a tie. ===");
								}
							}
							Console.WriteLine("");

							break;
						}
						else if (playerAction == PlayerAction.Double)
						{
							playerHand.AddCard(shoe.DrawCard());
							Console.WriteLine($"Player's Hand: {playerHand}");

							if (playerHand.IsBust)
							{
								Console.WriteLine("");
								Console.WriteLine("--- Player busts! Dealer wins. ---");
								Console.WriteLine("");
								break;
							}

							// Provide updated recommendation based on the new card
							recommendation = CardCountingHelper.GetRecommendation(runningCount, playerHand, dealerHand.Cards[0]);
							Console.WriteLine("");
							Console.WriteLine($"Recommendation: {recommendation}");
							Console.WriteLine("");

							// Double the bet
							//playerHand.Bet *= 2;

							// Dealer's turn
							while (dealerHand.GetTotal() < 17)
							{
								dealerHand.AddCard(shoe.DrawCard());
								Console.WriteLine($"Dealer's Hand: {dealerHand}");

								if (dealerHand.IsBust)
								{
									Console.WriteLine("");
									Console.WriteLine("Dealer busts! Player wins.");
									Console.WriteLine("");
									break;
								}
							}

							Console.WriteLine("");
							if (!dealerHand.IsBust)
							{
								Console.WriteLine($"Dealer's Hand: {dealerHand}");

								if (playerHand.GetTotal() > dealerHand.GetTotal())
								{
									Console.WriteLine("+++ Player wins! +++");
								}
								else if (playerHand.GetTotal() < dealerHand.GetTotal())
								{
									Console.WriteLine("--- Dealer wins! ---");
								}
								else
								{
									Console.WriteLine("=== Push! It's a tie. ===");
								}
							}
							Console.WriteLine("");

							break;
						}
					}
				}

				// Clear the hands for the next hand
				playerHand.Clear();
				dealerHand.Clear();
			}
		}

		// Helper method to get player action
		public static PlayerAction GetPlayerAction()
		{
			Console.WriteLine("Choose an action:");
			Console.WriteLine("1. Hit");
			Console.WriteLine("2. Stand");
			Console.WriteLine("3. Double");
			Console.WriteLine("===============");

			while (true)
			{
				var input = Console.ReadLine();
				if (int.TryParse(input, out var choice) && Enum.IsDefined(typeof(PlayerAction), choice - 1))
				{
					Console.WriteLine($"Player chooses {((PlayerAction)(choice - 1)).ToString().ToLower()}.");
					return (PlayerAction)(choice - 1);
				}

				Console.WriteLine("Invalid input. Please try again.");
			}
		}
	}
}
