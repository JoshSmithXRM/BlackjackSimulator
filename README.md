# Blackjack Simulator

Welcome to the Blackjack Simulator, an exciting project that allows you to simulate playing the popular casino game, Blackjack, from your command line! You'll be able to play games, count cards, and get insights to help you better understand the game. This simulator was created with the SOLID design principles and dependency injection to provide a clean and maintainable codebase.

## Features

* **Game Play**: Play a full game of Blackjack following the standard rules.
* **Card Counting**: The simulator includes an automatic card counting feature, which uses a simple +1, 0, -1 counting system.
* **Advice**: Get advice based on the current count, your hand, and the dealer's face-up card. This can be used to learn Blackjack strategy.
* **Object-Oriented Design**: The simulator was designed using SOLID design principles for clean and maintainable code.
* **Dependency Injection**: The application leverages .NET's built-in Dependency Injection framework for better testability and separation of concerns.

## Getting Started

To get started with the Blackjack Simulator, you'll need to clone the repository and then build and run the project.

1. Clone the repository.
    ```
    git clone https://github.com/JoshSmithXRM/BlackjackSimulator.git
    ```

2. Navigate into the cloned repository.
    ```
    cd BlackjackSimulator
    ```

3. Navigate into the src folder.
    ```
    cd src
    ```

4. Build the solution using dotnet CLI.
    ```
    dotnet build
    ```

5. Run the application.
    ```
    dotnet run
    ```

## Overview of Key Components

* **Card Counting Service**: An implementation of a card counting strategy. By default, the simulator uses a simple count where 10s and Aces are worth -1, 7-9 are worth 0, and 2-6 are worth +1. The CardCountingService provides recommendations based on this count, your hand, and the dealer's up card.
* **Game Service**: The heart of the simulator, this component manages the flow of a game of Blackjack, from dealing cards to handling player and dealer actions.
* **Shoe Service**: This is where the cards are kept. A new Shoe is created at the start of each game, containing several decks of cards. The Shoe can be shuffled and cards can be drawn from it.

We hope you enjoy using the Blackjack Simulator and find it a useful tool for understanding the game!
