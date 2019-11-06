﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class RockPaperScissorsGame
    {
        string player1ID = "";
        string player2ID = "";
        string player1Move = "";
        string player2Move = "";
        bool gameComplete = false;
        Server_Server server;

        public RockPaperScissorsGame(Server_Server server, string p1ID, string p2ID)
        {
            player1ID = p1ID;
            player2ID = p2ID;
            this.server = server;

            server.MessageClient("Choose your Move - Rock, Paper or Scissors: ", player1ID, server.messageColor);
            server.MessageClient("Choose your Move - Rock, Paper or Scissors: ", player2ID, server.messageColor);
        }

        public bool IsComplete()
        {
            return this.gameComplete;
        }
        public bool PlayerInGame(string ID)
        {
            if (player1ID == ID || player2ID == ID)
                return true;
            else
                return false;
        }
        public bool PlayerMadeMove(string ID)
        {
            if ((player1ID == ID && player1Move != "") || (player2ID == ID && player2Move != ""))
                return true;
            else
                return false;
        }
        public bool MakeMove(string playerID, string move)
        {
            move = move.ToLower();
            switch (move)
            {
                case "r":
                case "rock":
                    {
                        if (playerID == player1ID)
                            player1Move = "Rock";
                        else
                            player2Move = "Rock";
                        break;
                    }
                case "p":
                case "paper":
                    {
                        if (playerID == player1ID)
                            player1Move = "Paper";
                        else
                            player2Move = "Paper";
                        break;
                    }
                case "s":
                case "scissors":
                    {
                        if (playerID == player1ID)
                            player1Move = "Scissors";
                        else
                            player2Move = "Scissors";
                        break;
                    }
                default:
                    return false;
            }
            if (player1Move != "" && player2Move != "")
            {
                gameComplete = true;
            }
            return true;
        }

        public void FinishGame()
        {
            string gameString = "Server: " + player1ID + " played " + player1Move + ". " + player2ID + " played " + player2Move + ".";

            server.MessageClient(gameString, player1ID, server.messageColor);
            server.MessageClient(gameString, player2ID, server.messageColor);

            string winString = "";
            bool replay = false;
            if (player1Move == "Rock" && player2Move == "Scissors")
                winString = "Server: " + player1ID + " Wins!";
            else if (player1Move == "Rock" && player2Move == "Paper")
                winString = "Server: " + player2ID + " Wins!";
            else if (player1Move == "Scissors" && player2Move == "Paper")
                winString = "Server: " + player1ID + " Wins!";
            else if (player1Move == "Scissors" && player2Move == "Rock")
                winString = "Server: " + player2ID + " Wins!";
            else if (player1Move == "Paper" && player2Move == "Rock")
                winString = "Server: " + player1ID + " Wins!";
            else if (player1Move == "Paper" && player2Move == "Scissors")
                winString = "Server: " + player2ID + " Wins!";
            else if (player1Move == player2Move)
            {
                winString = "Server: Tie, play another round";
                replay = true;
            }

            server.MessageClient(winString, player1ID, server.messageColor);
            server.MessageClient(winString, player2ID, server.messageColor);

            if (replay)
            {
                player1Move = "";
                player2Move = "";
                gameComplete = false;
                server.MessageClient("Choose your Move - Rock, Paper or Scissors: ", player1ID, server.messageColor);
                server.MessageClient("Choose your Move - Rock, Paper or Scissors: ", player2ID, server.messageColor);
            }
        }
    }
}