using System.Collections.Generic;

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

            server.MessageClients("Choose your Move - Rock, Paper or Scissors: ", new List<string>() { player1ID, player2ID }, server.messageColor);
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
            string gameString = "Server: " + server.GetClientFromID(player1ID).ColorID() + " played " + player1Move + ". " + server.GetClientFromID(player2ID).ColorID() + " played " + player2Move + ".";

            server.MessageClient(gameString, player1ID, server.messageColor);
            server.MessageClient(gameString, player2ID, server.messageColor);

            string winString = "";
            bool replay = false;
            if (player1Move == "Rock" && player2Move == "Scissors")
                winString = "Server: " + server.GetClientFromID(player1ID).ColorID() + " Wins!";
            else if (player1Move == "Rock" && player2Move == "Paper")
                winString = "Server: " + server.GetClientFromID(player2ID).ColorID() + " Wins!";
            else if (player1Move == "Scissors" && player2Move == "Paper")
                winString = "Server: " + server.GetClientFromID(player1ID).ColorID() + " Wins!";
            else if (player1Move == "Scissors" && player2Move == "Rock")
                winString = "Server: " + server.GetClientFromID(player2ID).ColorID() + " Wins!";
            else if (player1Move == "Paper" && player2Move == "Rock")
                winString = "Server: " + server.GetClientFromID(player1ID).ColorID() + " Wins!";
            else if (player1Move == "Paper" && player2Move == "Scissors")
                winString = "Server: " + server.GetClientFromID(player2ID).ColorID() + " Wins!";
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
