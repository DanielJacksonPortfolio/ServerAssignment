using PacketData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Drawing;

namespace Server
{
    public enum Message_Origins { SERVER, CLIENT };

    public class Server_Server : IDisposable
    {
        TcpListener server = null;
        List<Server_Client> clients = new List<Server_Client>();
        List<RockPaperScissorsGame> rpsGames = new List<RockPaperScissorsGame>();
        string serverID = "Server";
        Thread connectorThread;
        ServerWindow serverWindow;
        IPAddress ip;
        public Color errorColor = Color.DarkRed;
        public Color announceColor = Color.RoyalBlue;
        public Color messageColor = Color.SaddleBrown;
        public Color logColor = Color.Yellow;
        int port;
        bool connected = false;
        bool disposed = false;
        MemoryStream memoryStream = new MemoryStream();
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        public Server_Server(ServerWindow serverWindow)
        {
            this.serverWindow = serverWindow;
            this.serverWindow.InitializeServer(this);
        }

        public bool IsConnected()
        {
            return this.connected;
        }

        void ValidateID(Server_Client client, string receivedMessage)
        {
            string potentialID = receivedMessage;
            int potenialIDNum = 0;
            bool nameValid;
            while (true)
            {
                nameValid = true;
                foreach (Server_Client recClient in clients)
                {
                    if (recClient != client)
                    {
                        if (potentialID == recClient.ID)
                        {
                            potentialID = receivedMessage + "(" + ++potenialIDNum + ")";
                            nameValid = false;
                            break;
                        }
                    }
                }
                if (nameValid)
                    break;
            }
            client.ID = potentialID;
        }
        void SetClientID(Server_Client client, string receivedMessage)
        {
            ValidateID(client, receivedMessage);
            Log("Log: Connection Made - " + client.ColorID() + " connected", this.logColor);
            MessageClient("------------------------------------------------------------------------------------------------------------------------------------------------------------", client, this.announceColor);
            MessageClient("This is " + serverID + " server. For more information visit: https://danieljacksonportfolio.co.uk/", client, this.announceColor);
            MessageClient("Welcome " + client.ColorID(), client, this.announceColor);

            foreach (Server_Client recClient in clients)
            {
                if (recClient != client)
                {
                    MessageClient(client.ColorID() + " - Connected", recClient, this.announceColor);
                }
            }
        }
        void SetClientColor(Server_Client client, Color chatColor, bool newUser = false)
        {
            client.color = chatColor;
            if (!newUser)
            {
                Announce(client.ColorID() + " is now color: <color (" + chatColor.R + "," + chatColor.G + "," + chatColor.B + ")>(" + chatColor.R + ", " + chatColor.G + ", " + chatColor.B + ")</color>.");
            }
        }

        //void Send(Packet data, Server_Client client)
        //{
        //    try
        //    {
        //        memoryStream.SetLength(0);
        //        binaryFormatter.Serialize(memoryStream, data);
        //        memoryStream.Flush();
        //        byte[] buffer = memoryStream.GetBuffer();
        //        memoryStream.SetLength(0);

        //        client.bwriter.Write(buffer.Length);
        //        client.bwriter.Write(buffer);
        //        client.bwriter.Flush();
        //    }
        //    catch (IOException e)
        //    {
        //        Console.WriteLine("Send Failed - " + e.Message);
        //    }
        //}

        public Packet CreateChatPacket(string message, Color color)
        {
            return new ChatMessagePacket(message, color);
        }
        public Packet CreateInitPacket(string message, Color color)
        {
            return new InitMessagePacket(message, color);
        }
        public Packet CreateDisconnectPacket(string message, DisconnectPacket.DisconnectType dType)
        {
            return new DisconnectPacket(message, dType);
        }

        bool Receive(Server_Client client)
        {
            try
            {
                Packet rawPacket = client.TCPRead();
                if (rawPacket != null)
                {
                    switch (rawPacket.type)
                    {
                        case PacketType.CHAT_MESSAGE:
                            {
                                ChatMessagePacket packet = (ChatMessagePacket)rawPacket;
                                ProcessClientMessage(packet.message, clients.IndexOf(client));
                                break;
                            }
                        case PacketType.INIT_MESSAGE:
                            {
                                InitMessagePacket packet = (InitMessagePacket)rawPacket;
                                SetClientColor(client, packet.chatColor, true);
                                SetClientID(client, packet.message);
                                break;
                            }
                        case PacketType.COLOR:
                            {
                                ColorPacket packet = (ColorPacket)rawPacket;
                                SetClientColor(client, packet.color);
                                break;
                            }
                    }
                }
                return true;
            }
            catch (IOException e)
            {
                Console.WriteLine("Receive Failed - " + e.Message);
                return false;
            }

        }


        void DisconnectClient(DisconnectPacket.DisconnectType dType, string clientID)
        {
            DisconnectClient(dType, GetClientFromID(clientID));
        }
        void DisconnectClient(DisconnectPacket.DisconnectType dType, Server_Client client, bool all = false)
        {
            string dMessage = "Disconnected from Server - Reason: N/A";
            switch (dType)
            {
                case DisconnectPacket.DisconnectType.CLEAN:
                    dMessage = "Disconnected from Server - Reason: Manual Disconnection";
                    break;
                case DisconnectPacket.DisconnectType.SERVER_DEAD:
                    dMessage = "Disconnected from Server - Reason: Server Died";
                    break;
                case DisconnectPacket.DisconnectType.SERVER_KILL:
                    dMessage = "Disconnected from Server - Reason: Killed by Server";
                    break;
                case DisconnectPacket.DisconnectType.USER_KILL:
                    dMessage = "Disconnected from Server - Reason: Killed by User";
                    break;
            }


            client.TCPSend(CreateDisconnectPacket(dMessage, dType));
            Announce(client.ID + " Disconnected");
            client.Close();
            if (!all)
                clients.Remove(client);
        }

        void ClientMethod(object clientObj)
        {
            Server_Client client = (Server_Client)clientObj;
            if (client != null)
            {
                while(true)
                    Receive(client);
            }
        }

        void CheckForConnections()
        {
            Log("------------------------------------------------------------------------------------------------------------------------------------------------------------\nLog: Server Start", this.logColor);
            Log("Log: Connected to IP: " + ip + ", on Port: " + port, this.logColor);
            Log("Log: Server Name: " + serverID, this.logColor);
            while (true)
            {
                try
                {
                    Socket socket = server.AcceptSocket();
                    Server_Client client = new Server_Client();
                    client.TCPConnect(socket);
                    clients.Add(client);
                    client.ID = (clients.IndexOf(client) + 1).ToString();
                    Thread t = new Thread(new ParameterizedThreadStart(ClientMethod));
                    t.Name = "Client " + client.ID;
                    t.Start(client);
                }
                catch (SocketException e)
                {
                    Log("Log: Server Quit - " + e.Message, Color.DarkRed);
                    //Log("Log: Server Quit");
                    break;
                }
            }
        }

        public void Connect(object args)
        {
            Array argsArray;
            argsArray = (Array)args;
            string ipAddress = (string)argsArray.GetValue(0);
            int port = (int)argsArray.GetValue(1);
            string ID = (string)argsArray.GetValue(2);
            try
            {
                this.ip = IPAddress.Parse(ipAddress);
                this.port = port;
                server = new TcpListener(this.ip, this.port);
                connectorThread = new Thread(CheckForConnections);
                serverID = ID;
                connected = true;
                server.Start();
                serverWindow.StartConnection();
            }
            catch (FormatException e)
            {
                Console.WriteLine("Exception: Connection error - " + e.Message);
                connected = false;
            }
        }

        public void Run()
        {
            connectorThread.Start();
        }
        public void CloseServer()
        {
            foreach (Server_Client client in clients)
            {
                DisconnectClient(DisconnectPacket.DisconnectType.SERVER_DEAD, client, true);
            }
            clients.Clear();
            if (server != null) server.Stop();
            connected = false;
            //Log("Server Disconnected");
        }


        public Server_Client GetClientFromID(string ID)
        {
            foreach (Server_Client client in clients)
            {
                if (clients[clients.IndexOf(client)].ID == ID)
                {
                    return clients[clients.IndexOf(client)];
                }
            }
            return null;
        }

        void MessageCommand(Message_Origins origin, List<string> commandData)
        {
            if (origin == Message_Origins.SERVER)
            {
                if (commandData.Count >= 1)
                {
                    if (commandData[0] != "")
                    {
                        if (commandData.Count >= 2)
                        {
                            string msg = "";
                            for (int i = 1; i < commandData.Count; ++i)
                            {
                                msg += commandData[i];
                                msg += " ";
                            }
                            msg = msg.Substring(0, msg.Length - 1);
                            List<string> unValidatedReceivers = new List<string>(commandData[0].Split(','));
                            List<string> validatedReceivers = new List<string>();
                            foreach (string rec in unValidatedReceivers)
                            {
                                if (GetClientFromID(rec) != null)
                                {
                                    validatedReceivers.Add(rec);
                                    Log("Log: Private Message to " + GetClientFromID(rec).ColorID() + ": " + msg, this.logColor);
                                }
                                else
                                {
                                    Log("Error: Private Message Partial Failure - Invalid User: " + rec, this.errorColor);
                                }
                            }
                            MessageClients("Server whispered: " + msg, validatedReceivers, this.messageColor);

                        }
                        else
                        {
                            Log("Error: Private Message Failed - No Message Given", this.errorColor);
                        }
                    }
                    else
                    {
                        Log("Error: Private Message Failed - No User Given", this.errorColor);
                    }
                }
                else
                {
                    Log("Error: Private Message Failed - No User Given", this.errorColor);
                }
            }
            else
            {
                if (commandData[1] != "")
                {

                    if (commandData.Count >= 3)
                    {
                        string msg = "";
                        for (int i = 2; i < commandData.Count; ++i)
                        {
                            msg += commandData[i];
                            msg += " ";
                        }

                        List<string> unValidatedReceivers = new List<string>(commandData[1].Split(','));
                        List<string> validatedReceivers = new List<string>();
                        foreach (string rec in unValidatedReceivers)
                        {
                            if (GetClientFromID(rec) != null)
                            {
                                if (commandData[0] != rec)
                                {
                                    validatedReceivers.Add(rec);
                                    Log("Private Message to " + GetClientFromID(rec).ColorID() + ": " + msg, this.logColor);
                                }
                                else
                                {
                                    MessageClient("Private Message Partial Failure - Cannot Message Yourself", commandData[0], this.errorColor);
                                    Log("Error: Private Message by " + GetClientFromID(commandData[0]).ColorID() + " Partial Failure - Cannot Message Yourself", this.errorColor);
                                }
                            }
                            else
                            {
                                MessageClient("Private Message Failed - Invalid User: " + rec, commandData[0], this.errorColor);
                                Log("Error: Private Message Partial Failure - Invalid User: " + rec, this.errorColor);
                            }
                        }
                        if (validatedReceivers.Count > 0)
                        {
                            MessageClient("You whispered to " + commandData[1] + ": " + msg.Substring(0, msg.Length - 1), commandData[0], this.messageColor);
                            MessageClients(GetClientFromID(commandData[0]).ColorID() + " whispered to you: " + msg, validatedReceivers, GetClientFromID(commandData[0]).color);
                        }
                    }
                    else
                    {
                        MessageClient("Private Message Failed - No Message Given", commandData[0], this.errorColor);
                        Log("Error: Private Message by " + GetClientFromID(commandData[0]).ColorID() + " Failed - No Message Given", this.errorColor);
                    }
                }
                else
                {
                    MessageClient("Private Message Failed - No User Given", commandData[0], this.errorColor);
                    Log("Error: Private Message by " + GetClientFromID(commandData[0]).ColorID() + " Failed - No User Given", this.errorColor);
                }
            }
        }


        public void ProcessCommand(Message_Origins origin, string command)
        {
            if (!command.StartsWith("/"))
            {
                if (command.Length != 0 && !command.StartsWith("\n"))
                {
                    Log("Log: " + command, this.logColor);
                }
            }
            else
            {
                string commandType;
                string commandDataString = "";
                List<string> commandData = new List<string>();
                try
                {
                    commandType = command.Substring(0, command.IndexOf(' '));
                    commandDataString = command.Substring(command.IndexOf(' ') + 1);
                    commandData = new List<string>(commandDataString.Split(' '));
                }
                catch
                {
                    commandType = command;
                }

                switch (commandType.ToLower())
                {
                    case "/kill_server":
                    case "/stop_server":
                        {
                            CloseServer();
                            break;
                        }
                    default:
                        if (origin == Message_Origins.SERVER)
                        {
                            switch (commandType.ToLower())
                            {
                                case "/log":
                                    {
                                        Log(commandDataString, this.logColor);
                                        break;
                                    }
                                case "/new_user":
                                    {
                                        Process.Start("D:\\OneDrive - Staffordshire University\\Desktop\\University\\Repos\\C# Projects\\NewServer\\Client\\Bin\\Debug\\NewServer.exe");
                                        break;
                                    }
                                case "/pm":
                                case "/message":
                                case "/whisper":
                                case "/dm":
                                    {
                                        MessageCommand(origin, commandData);
                                        break;
                                    }
                                case "/announce":
                                    {
                                        Announce(commandDataString);
                                        break;
                                    }
                                case "/help":
                                    {
                                        Log("\nServer Commands:\n\n/rename [ID] [NewID]\n/announce [Message]\n/kill_user [ID]\n/op [ID]\n/deop [ID]\n/kill_server\n/stop_server\n/quit [ID]\n/exit [ID]\n/kill [ID]\n/new_user\n", this.logColor);
                                        break;
                                    }
                                case "/kill_user":
                                case "/kick":
                                case "/disconnect_user":
                                    {
                                        if (commandData[0] == "")
                                        {
                                            Log("Error:  Kill_User Failed - No User Selected", this.errorColor);
                                        }
                                        else
                                        {
                                            if (ClientExists(commandData[0]))
                                            {
                                                DisconnectClient(DisconnectPacket.DisconnectType.SERVER_KILL, commandData[0]);
                                                Log("Killed User - " + GetClientFromID(commandData[0]).ColorID(), this.logColor);
                                            }
                                            else
                                            {
                                                Log("Error:  Kill_User Failed - Invalid User Selected", this.errorColor);
                                            }
                                        }
                                        break;
                                    }
                                case "/rename_server":
                                case "/rename":
                                    {
                                        if (commandData[0] == "")
                                        {
                                            Log("Error:  Server Rename Failed - No Data Given", this.errorColor);
                                        }
                                        else
                                        {
                                            this.serverID = commandDataString;
                                            Announce("Server renamed to: " + this.serverID);
                                        }
                                        break;
                                    }
                                case "/op":
                                    {
                                        if (commandData[0] == "")
                                        {
                                            Log("Error:  OP Failed - No User Selected", this.errorColor);
                                        }
                                        else
                                        {
                                            if (ClientExists(commandData[0]))
                                            {
                                                if (!GetClientFromID(commandData[0]).GetOP())
                                                {
                                                    GetClientFromID(commandData[0]).SetOP(true);
                                                    MessageClient("You are have been opped", commandData[0], this.messageColor);
                                                    Log("Log: Opped " + GetClientFromID(commandData[0]).ColorID(), this.logColor);
                                                }
                                            }
                                            else
                                            {
                                                Log("Error:  OP Failed - Invalid User Selected: " + commandData[0], this.errorColor);
                                            }
                                        }
                                        break;
                                    }
                                case "/deop":
                                    {
                                        if (commandData[0] == "")
                                        {
                                            Log("Error:  OP Failed - No User Selected", this.errorColor);
                                        }
                                        else
                                        {
                                            if (ClientExists(commandData[0]))
                                            {
                                                if (GetClientFromID(commandData[0]).GetOP())
                                                {
                                                    GetClientFromID(commandData[0]).SetOP(false);
                                                    MessageClient("You have been deoppped", commandData[0], this.messageColor);
                                                    Log("Log: Deopped " + GetClientFromID(commandData[0]).ColorID(), this.logColor);

                                                }
                                            }
                                            else
                                            {
                                                Log("Error:  OP Failed - Invalid User Selected: " + commandData[0], this.errorColor);
                                            }
                                        }
                                        break;
                                    }
                            }
                        }
                        else if (origin == Message_Origins.CLIENT)
                        {
                            string sendingClient = commandData[0];
                            string receivingClient = "NULL CLIENT";
                            if (commandData.Count >= 2)
                            {
                                receivingClient = commandData[1];
                            }
                            switch (commandType.ToLower())
                            {
                                case "/pm":
                                case "/message":
                                case "/whisper":
                                case "/dm":
                                    {
                                        MessageCommand(origin, commandData);
                                        break;
                                    }
                                case "/play_rps":
                                case "/playrps":
                                case "/rps":
                                case "/rockpaperscissors":
                                case "/rock_paper_scissors":
                                    {
                                        if (receivingClient != "")
                                        {
                                            if (GetClientFromID(receivingClient) != null)
                                            {
                                                if (sendingClient != receivingClient)
                                                {
                                                    if (PlayingRPS(sendingClient, false) != null)
                                                    {
                                                        MessageClient("Rock Paper Scissors Failed - You are already playing a game of rock paper scissors", sendingClient, this.errorColor);
                                                        Log("Error: Rock Paper Scissors by " + GetClientFromID(sendingClient).ColorID() + " Failed - You are already playing a game of rock paper scissors", this.errorColor);
                                                    }
                                                    else if (PlayingRPS(receivingClient, false) != null)
                                                    {
                                                        MessageClient("Rock Paper Scissors Failed - " + GetClientFromID(receivingClient).ColorID() + " is already playing a game of rock paper scissors", sendingClient, this.errorColor);
                                                        Log("Error: Rock Paper Scissors by " + GetClientFromID(sendingClient).ColorID() + " Failed - " + GetClientFromID(receivingClient).ColorID() + " is already playing a game of rock paper scissors", this.errorColor);

                                                    }
                                                    else
                                                    {
                                                        MessageClient("You have been challenged to a game of Rock-Paper-Scissors by " + GetClientFromID(sendingClient).ColorID(), receivingClient, this.announceColor);
                                                        rpsGames.Add(new RockPaperScissorsGame(this, sendingClient, receivingClient));
                                                        Log("Rock Paper Scissors - " + GetClientFromID(sendingClient).ColorID() + " vs " + GetClientFromID(sendingClient).ColorID(), this.logColor);
                                                    }
                                                }
                                                else
                                                {
                                                    MessageClient("Rock Paper Scissors Failed - You Cannot Play Yourself", sendingClient, this.errorColor);
                                                    Log("Error: Rock Paper Scissors by " + GetClientFromID(sendingClient).ColorID() + " Failed - Cannot Play Self", this.errorColor);
                                                }
                                            }
                                            else
                                            {
                                                MessageClient("Rock Paper Scissors Failed - Invalid Opponent: " + receivingClient, sendingClient, this.errorColor);
                                                Log("Error: Rock Paper Scissors by " + GetClientFromID(sendingClient).ColorID() + " Failed - Invalid Opponent: " + receivingClient, this.errorColor);
                                            }
                                        }
                                        else
                                        {
                                            MessageClient("Rock Paper Scissors Failed - No Opponent Given", sendingClient, this.errorColor);
                                            Log("Error: Rock Paper Scissors by " + GetClientFromID(sendingClient).ColorID() + " Failed - No Opponent Given", this.errorColor);
                                        }
                                        break;
                                    }
                                case "/rename":
                                    {
                                        if (commandData[1] != "")
                                        {
                                            string oldID = GetClientFromID(sendingClient).ID;
                                            if (oldID != commandData[1])
                                            {
                                                oldID = GetClientFromID(sendingClient).ColorID();
                                                int index = clients.IndexOf(GetClientFromID(sendingClient));
                                                ValidateID(GetClientFromID(sendingClient), commandData[1]);
                                                Announce(oldID + " is now called " + clients[index].ColorID());
                                            }
                                            else
                                            {
                                                MessageClient("Error: Rename Failed - You are already called " + GetClientFromID(oldID).ColorID(), sendingClient, this.errorColor);
                                                Log("Error: Rename by " + GetClientFromID(sendingClient).ColorID() + " Failed - Already called " + oldID, this.errorColor);
                                            }
                                        }
                                        else
                                        {
                                            MessageClient("Error: Rename Failed - New name not given", sendingClient, this.errorColor);
                                            Log("Error: Rename by " + GetClientFromID(sendingClient).ColorID() + " Failed - New name not given", this.errorColor);
                                        }

                                        break;
                                    }
                                case "/help":
                                    {
                                        MessageClient("\nUser Commands:\n\n/rename[NewID]\n/kill_user[ID] *\n/op[ID] *\n/deop[ID] *\n/kill_server *\n/stop_server *\n/playrps [OpponentID]\n/quit\n/exit\n/kill\n/rename_server [NewID] *\n\n* = Administrator Only\n", sendingClient, this.messageColor);
                                        break;
                                    }
                                case "/kill_user":
                                case "/kick":
                                case "/disconnect_user":
                                    {
                                        if (receivingClient != "")
                                        {
                                            if (ClientExists(receivingClient))
                                            {
                                                Log("User " + GetClientFromID(receivingClient).ColorID() + " Killed by " + GetClientFromID(sendingClient).ColorID(), this.logColor);
                                                DisconnectClient(DisconnectPacket.DisconnectType.USER_KILL, receivingClient);
                                            }
                                            else
                                            {
                                                MessageClient("Kill_User Failed - Invalid User Selected: " + receivingClient, sendingClient, this.errorColor);
                                                Log("Error: Kill_User by " + GetClientFromID(sendingClient).ColorID() + " Failed - Invalid User Selected: " + receivingClient, this.errorColor);
                                            }
                                        }
                                        else
                                        {
                                            MessageClient("Kill_User Failed - No User Selected", sendingClient, this.errorColor);
                                            Log("Error:  Kill_User by " + GetClientFromID(sendingClient).ColorID() + " Failed - No User Selected", this.errorColor);
                                        }
                                        break;
                                    }
                                case "/rename_server":
                                    {
                                        if (commandData[1] == "")
                                        {
                                            MessageClient("Server Rename Failed - No Data Given", sendingClient, this.errorColor);
                                            Log("Error:  Server Rename by " + GetClientFromID(sendingClient).ColorID() + " Failed - No Data Given", this.errorColor);
                                        }
                                        else
                                        {
                                            string newID = "";
                                            for (int i = 1; i < commandData.Count; ++i)
                                            {
                                                newID += commandData[i];
                                                newID += " ";
                                            }
                                            this.serverID = newID.Substring(0, newID.Length - 1);
                                            Announce(GetClientFromID(sendingClient).ColorID() + " renamed the server to: " + this.serverID);
                                        }
                                        break;
                                    }
                                case "/op":
                                    {
                                        if (receivingClient != "")
                                        {
                                            if (ClientExists(receivingClient))
                                            {
                                                if (!GetClientFromID(receivingClient).GetOP())
                                                {
                                                    GetClientFromID(receivingClient).SetOP(true);
                                                    MessageClient("You are have been opped", receivingClient, this.messageColor);
                                                    Log("Log: Opped " + GetClientFromID(receivingClient).ColorID(), this.logColor);
                                                }
                                            }
                                            else
                                            {
                                                MessageClient("OP Failed - Invalid User Selected", sendingClient, this.errorColor);
                                                Log("Error:  OP by " + GetClientFromID(sendingClient).ColorID() + " Failed - Invalid User Selected", this.errorColor);
                                            }
                                        }
                                        else
                                        {
                                            MessageClient("OP Failed - No User Selected", sendingClient, this.errorColor);
                                            Log("Error:  OP by " + GetClientFromID(sendingClient).ColorID() + " Failed - No User Selected", this.errorColor);
                                        }
                                        break;
                                    }
                                case "/deop":
                                    {
                                        if (receivingClient != "")
                                        {
                                            if (ClientExists(receivingClient))
                                            {
                                                if (GetClientFromID(receivingClient).GetOP())
                                                {
                                                    GetClientFromID(receivingClient).SetOP(false);
                                                    MessageClient("You have been deopped", receivingClient, this.messageColor);
                                                    Log("Log: " + GetClientFromID(receivingClient).ColorID() + " Deopped by " + GetClientFromID(sendingClient).ColorID(), this.logColor);

                                                }
                                            }
                                            else
                                            {
                                                MessageClient("DEOP Failed - Invalid User Selected", sendingClient, this.errorColor);
                                                Log("Error:  DEOP by " + GetClientFromID(sendingClient).ColorID() + " Failed - Invalid User Selected", this.errorColor);
                                            }
                                        }
                                        else
                                        {
                                            MessageClient("DEOP Failed - No User Selected", sendingClient, this.errorColor);
                                            Log("Error:  DEOP by " + GetClientFromID(sendingClient).ColorID() + " Failed - No User Selected", this.errorColor);
                                        }
                                        break;
                                    }
                                case "/quit":
                                case "/kill":
                                case "/exit":
                                case "/disconnect":
                                    {
                                        DisconnectClient(DisconnectPacket.DisconnectType.CLEAN, sendingClient);
                                        break;
                                    }
                                default:
                                    {
                                        Log("Error: Invalid Command: " + command, this.errorColor);
                                        break;
                                    }
                            }
                        }
                        break;
                }
            }
        }
        void ProcessClientMessage(string command, int clientIndex)
        {
            if (command.StartsWith("/"))
            {
                string commandType;
                string commandData = "";
                try
                {
                    commandType = command.Substring(0, command.IndexOf(' '));
                    commandData = command.Substring(command.IndexOf(' ') + 1);
                }
                catch
                {
                    commandType = command;
                }
                switch (commandType.ToLower())
                {
                    /// Admin Commands (Pass ID + Data) ///
                    case "/kill_user":
                    case "/disconnect_user":
                    case "/rename_server":
                    case "/op":
                    case "/deop":
                        {
                            if (clients[clientIndex].GetOP())
                            {
                                if (commandData != "")
                                {
                                    ProcessCommand(Message_Origins.CLIENT, commandType + " " + clients[clientIndex].ID + " " + commandData);
                                }
                                else
                                {
                                    MessageClient("Error: Invalid Command Parameters: " + command, clientIndex, this.errorColor);
                                }
                            }
                            else
                            {
                                MessageClient("Error: You must be an op to use: " + command, clientIndex, this.errorColor);
                            }
                            break;
                        }
                    /// Admin Commands (Pass ID) ///
                    case "/kill_server":
                    case "/stop_server":
                        {
                            if (clients[clientIndex].GetOP())
                            {
                                ProcessCommand(Message_Origins.CLIENT, commandType + " " + clients[clientIndex].ID);
                            }
                            else
                            {
                                MessageClient("Error: You must be an op to use: " + command, clientIndex, this.errorColor);
                            }
                            break;
                        }
                    /// Non-Admin Commands (Pass ID + Data) ///
                    case "/play_rps":
                    case "/playrps":
                    case "/rps":
                    case "/rockpaperscissors":
                    case "/rock_paper_scissors":
                    case "/rename":
                    case "/pm":
                    case "/message":
                    case "/whisper":
                    case "/dm":
                        {
                            if (commandData != "")
                            {
                                ProcessCommand(Message_Origins.CLIENT, commandType + " " + clients[clientIndex].ID + " " + commandData);
                            }
                            else
                            {
                                MessageClient("Error: Invalid Command Parameters: " + command, clientIndex, this.errorColor);
                            }
                            break;
                        }
                    /// Non-Admin Commands (Pass ID) ///
                    case "/help":
                        {
                            ProcessCommand(Message_Origins.CLIENT, commandType + " " + clients[clientIndex].ID);
                            break;
                        }
                    /// Quit Commands ///
                    case "/quit":
                    case "/kill":
                    case "/disconnect":
                    case "/exit":
                        {
                            ProcessCommand(Message_Origins.CLIENT, commandType + " " + clients[clientIndex].ID);
                            break;
                        }
                    default:
                        {
                            MessageClient("Error: No Such Command: " + command, clientIndex, this.errorColor);
                            break;
                        }
                }
            }
            else //Not Command
            {
                RockPaperScissorsGame game;
                if ((game = PlayingRPS(clients[clientIndex].ID)) != null)
                {
                    if (game.MakeMove(clients[clientIndex].ID, command))
                    {
                        MessageClient("You played: " + command, clientIndex, this.messageColor);
                        if (game.IsComplete())
                        {
                            game.FinishGame();
                            if (game.IsComplete())
                            {
                                rpsGames.Remove(game);
                            }
                        }
                    }
                    else
                    {
                        MessageClient("Invalid Move: " + command, clientIndex, this.errorColor);
                        MessageClient("Choose your Move - Rock, Paper or Scissors: ", clientIndex, this.messageColor);
                    }
                }
                else
                {
                    MessageAllClients(clients[clientIndex].ID + ": " + command, clients[clientIndex].color);
                }
            }
        }
        RockPaperScissorsGame PlayingRPS(string clientID, bool checkMove = true)
        {
            foreach (RockPaperScissorsGame game in rpsGames)
            {
                if (game.PlayerInGame(clientID))
                {
                    if (!checkMove || !game.PlayerMadeMove(clientID))
                    {
                        return game;
                    }
                }

            }
            return null;
        }

        void Log(string text, Color color)
        {
            if (text.Length != 0 && !text.StartsWith("\n"))
            {
                if (!IsConnected())
                {
                    text = "(Not Listening) " + text;
                }
                serverWindow.UpdateServerLog(text, color);
            }
        }
        void Announce(string text)
        {
            if (text == "")
            {
                text = "Ping";
            }
            if (!text.StartsWith("\n"))
            {
                Log("Announcement: " + text, this.logColor);
                MessageAllClients("Server Announcement: " + text, this.announceColor);
            }
        }
        bool ClientExists(string clientID)
        {
            foreach (Server_Client recClient in clients)
            {
                if (clientID == recClient.ID)
                {
                    return true;
                }
            }
            return false;
        }
        public void MessageClient(string message, int clientIndex, Color senderColor)
        {
            if (clientIndex >= 0 && clientIndex < clients.Count)
            {
                MessageClient(message, clients[clientIndex], senderColor);
            }
            else
            {
                Log("Error: Message Sending Failed - Invalid Index - Message: " + message, this.errorColor);
            }
        }
        void MessageClient(string message, Server_Client client, Color senderColor)
        {
            //Send(CreatePacket(message, PacketType.CHAT_MESSAGE), client);
            client.TCPSend(CreateChatPacket(message, senderColor));
        }
        public void MessageClient(string message, string clientID, Color senderColor)
        {
            if (ClientExists(clientID))
            {
                MessageClient(message, GetClientFromID(clientID), senderColor);
            }
            else
            {
                Log("Error: Message Sending Failed - Invalid ID - Message: " + message, this.errorColor);
            }
        }
        void MessageAllClients(string message, Color senderColor)
        {
            MessageClients(message, clients, senderColor);
        }
        void MessageClients(string message, List<Server_Client> inClients, Color senderColor)
        {
            foreach (Server_Client recClient in clients)
            {
                if (inClients.Contains(recClient))
                {
                    MessageClient(message, recClient, senderColor);
                }
            }
        }
        void MessageClients(string message, List<string> inClientIDs, Color senderColor)
        {
            foreach (Server_Client recClient in clients)
            {
                if (inClientIDs.Contains(recClient.ID))
                {
                    MessageClient(message, recClient, senderColor);
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    serverWindow.Dispose();
                }
                disposed = true;
            }
        }

    }
}
