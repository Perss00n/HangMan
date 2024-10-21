# Hangman Game - School Project

This project is part of my ongoing journey to become a backend developer. The purpose of this project is to build a fully functional Hangman game in C# that runs in the console. It supports both single-player and two-player modes, and it's built with fundamental C# programming concepts.

## About the Project

This project is a **school assignment** that simulates the classic **Hangman game** using C#. It serves as a practical exercise to apply object-oriented programming (OOP) principles, such as classes, properties, methods, and encapsulation. My goal in this project is to enhance my understanding of backend development through coding practices that closely mimic real-world scenarios.

The game allows a single player to guess a randomly chosen word or two players to challenge each other by taking turns setting secret words and guessing them.

## How It Works

### Single-Player Mode:
- A random word is selected from a predefined list or a `words.txt` file.
- The player has 8 attempts to guess the letters in the secret word.
- After each guess, the game updates the display to show correctly guessed letters or increment the wrong guesses count if the guess is incorrect.
- The game ends when the player either guesses all the letters or reaches the 8-wrong-guesses limit.

### Two-Player Mode:
- Player 1 sets a secret word for Player 2 to guess.
- Player 2 guesses the word, and then the roles switch.
- Each player gets 8 attempts to guess the word correctly.

## Features

- **Single-player mode**: Play against the computer and try to guess a randomly selected word.
- **Two-player mode**: Take turns with another player. One sets the word, and the other guesses.
- **Console-based UI**: All interactions happen through a simple and effective console interface.
- **Error handling**: Input validation ensures players can only enter valid single-letter guesses.
- **Game status updates**: A visual representation of the hangman is shown as incorrect guesses are made.

### Key Files:

- **Program.cs**: The entry point where the game menu is displayed, and the game modes are initialized.
- **Game.cs**: Contains the main game logic, including both single-player and two-player modes, as well as helper methods for guessing, checking for wins, and setting up users.
- **User.cs**: A class representing each player, responsible for tracking guesses, wrong answers, and scores.
- **Display.cs**: Manages visual output to the console, such as the hangman drawing and messages.

- ## Technologies Used

- **C#**: The programming language used to build the project.
- **.NET Core/Framework**: The platform that provides libraries, runtime, and compiler for running C# applications.
- **Object-Oriented Programming (OOP)**: The project is designed around core OOP principles like classes, methods, properties, and encapsulation.

## Why This Project?

This project represents a key milestone in my path toward becoming a backend developer. It was created as part of a **school project** that pushes me to apply my knowledge of C# and backend programming concepts in a real-world scenario.

Key learning objectives:
- Implementing OOP concepts in C#.
- Handling user input and validation.
- Managing game state and alternating turns between players.
- Creating a console application that simulates a user-friendly experience.
