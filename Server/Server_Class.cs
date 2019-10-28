using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace Server
{
    public enum Message_Origins { SERVER, CLIENT };
    class Server_Client : IDisposable
    {
        Socket socket;
        NetworkStream stream;
        bool op = false;
        bool disposed = false;
        public string ID { get; set; }
        public StreamReader reader { get; private set; }
        public StreamWriter writer { get; private set; }

        public Server_Client(Socket socket)
        {
            this.socket = socket;
            ID = "";
            stream = new NetworkStream(this.socket, true);
            reader = new StreamReader(stream, Encoding.UTF8);
            writer = new StreamWriter(stream, Encoding.UTF8);
        }

        public void Close()
        {
            socket.Close();
        }

        public void SetOP(bool val)
        {
            this.op = val;
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
                    stream.Dispose();
                }
                disposed = true;
            }
        }

        public bool GetOP()
        {
            return op;
        }
    }
    public class Server_Server : IDisposable
    {
        TcpListener server = null;
        List<Server_Client> clients = new List<Server_Client>();
        string serverID = "Server";
        Thread connectorThread;
        ServerWindow serverWindow;
        string ip;
        string port;
        bool disposed = false;
        public Server_Server()
        {
            connectorThread = new Thread(CheckForConnections);
            serverWindow = new ServerWindow(this);
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
            Log("Log: Connection Made - " + client.ID + " connected");
            MessageClient("This is " + serverID + " server. For more information visit: https://danieljacksonportfolio.co.uk/ \nWelcome " + client.ID, client);

            foreach (Server_Client recClient in clients)
            {
                if (recClient != client)
                {
                    MessageClient(client.ID + " - Connected", recClient);
                }
            }
        }
        void ClientMethod(object clientObj)
        {
            Server_Client client = (Server_Client)clientObj;
            if (client != null)
            {
                string receivedMessage;
                while ((receivedMessage = client.reader.ReadLine()) != null) // Wait for name to be given
                {
                    SetClientID(client, receivedMessage);
                    break;
                }
                try
                {
                    while ((receivedMessage = client.reader.ReadLine()) != null) // Main Loop
                    {
                        string returnCommand = ProcessClientMessage(receivedMessage, clients.IndexOf(client));
                        if (returnCommand == "CODE::KILL" || returnCommand == "CODE::SERVER_DEAD")
                        {
                            break;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Server Force Closed");
                }
            }
            client.Close();
            clients.Remove(client);
        }

        void CheckForConnections()
        {
            Log("Log: Server Start");
            Log("Log: Connected to IP: "+ip+", on Port: "+port);
            Log("Log: Server Name: "+serverID);
            while (true)
            {
                try
                {
                    //Log("Log: Waiting for connection...");
                    Socket socket = server.AcceptSocket();
                    Server_Client client = new Server_Client(socket);
                    clients.Add(client);
                    client.ID = (clients.IndexOf(client) + 1).ToString();
                    Thread t = new Thread(new ParameterizedThreadStart(ClientMethod));
                    t.Start(client);
                }
                catch
                {
                    Log("Log: Server Quit");
                    break;
                }
            }
        }

        public bool Connect(string ip, int port, string ID)
        {
            try
            {
                IPAddress localIP = IPAddress.Parse(ip);
                server = new TcpListener(localIP, port);
                serverID = ID;
                this.ip = ip;
                this.port = port.ToString();

                server.Start();
                Application.Run(serverWindow);
            }
            catch
            {
                Console.WriteLine("Exception: Connection error");
                return false;
            }
            return true;
        }

        public void Run()
        {
            try
            {
                connectorThread.Start();
            }
            catch
            {
                Console.WriteLine("Server Closed");
            }
                
        }

        public void CloseServer()
        {
            MessageAllClients("CODE::SERVER_DEAD");
            foreach (Server_Client client in clients)
            {
                client.Close();
            }
            clients.Clear();
            server.Stop();
            Console.WriteLine("Server Disconnected");
        }
        Server_Client GetClientFromID(string ID)
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

        public void ProcessServerCommand(Message_Origins origin, string command)
        {
            if (!command.StartsWith("/"))
            {
                Log("Log: " + command);
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
                    commandData.Add(commandDataString);
                }

                switch (commandType.ToLower())
                {
                    case "/new_user":
                        {
                            Process.Start("D:\\OneDrive - Staffordshire University\\Desktop\\University\\Repos\\C# Projects\\NewServer\\Client\\Bin\\Debug\\NewServer.exe");
                            break;
                        }
                    case "/play_rps":
                    case "/playrps":
                    case "/rps":
                    case "/rockpaperscissors":
                    case "/rock_paper_scissors":
                        {
                            if (commandData[0] != "")
                            {
                                if (GetClientFromID(commandData[0]) != null)
                                {
                                    if (commandData.Count >= 2)
                                    {
                                        if (GetClientFromID(commandData[1]) != null)
                                        {
                                            if (commandData[0] != commandData[1])
                                            {
                                                Log("Rock Paper Scissors - " + commandData[0] + " vs " + commandData[1]);
                                            }
                                            else
                                            {
                                                MessageClient("Rock Paper Scissors Failed - Cannot Play Yourself", commandData[0]);
                                                Log("Error: Rock Paper Scissors Failed - Cannot Play Yourself");
                                            }
                                        }
                                        else
                                        {
                                            MessageClient("Rock Paper Scissors Failed - Invalid User: " + commandData[1], commandData[0]);
                                            Log("Error: Rock Paper Scissors Failed - Invalid User: " + commandData[1]);
                                        }
                                    }
                                    else
                                    {
                                        if (origin != Message_Origins.SERVER)
                                            MessageClient("Rock Paper Scissors Failed - No User 2 Given", commandData[0]);
                                        Log("Error: Rock Paper Scissors Failed - No User 2 Given");
                                    }
                                }
                                else
                                {
                                    Log("Error: Rock Paper Scissors Failed - Invalid User: " + commandData[0]);
                                }
                            }
                            else
                            {
                                Log("Error: Rock Paper Scissors Failed - No User 1 Given");
                            }
                            break;
                        }
                    case "/rename":
                        {
                            if (commandData[0] != "")
                            {
                                if (commandData.Count == 2)
                                {
                                    if (ClientExists(commandData[0]))
                                    {
                                        string oldId = GetClientFromID(commandData[0]).ID;
                                        int index = clients.IndexOf(GetClientFromID(commandData[0]));
                                        ValidateID(GetClientFromID(commandData[0]), commandData[1]);
                                        Announce(oldId + " is now called " + clients[index].ID);
                                    }
                                    else
                                    {
                                        Log("Error: Rename Failed - Client doesn't exist");
                                    }
                                }
                                else
                                {
                                    if (origin != Message_Origins.SERVER)
                                        MessageClient("Error: Rename Failed - New name not given", commandData[0]);
                                    Log("Error: Rename Failed - New name not given");
                                }
                            }
                            else
                            {
                                Log("Error: Rename Failed - No Client Given");
                            }

                            break;
                        }
                    case "/announce":
                        {
                            if (commandDataString == "")
                            {
                                commandDataString = "PING";
                            }
                            Announce(commandDataString);
                            break;
                        }
                    case "/help":
                        {
                            if (origin == Message_Origins.SERVER)
                            {
                                Log("\nServer Commands:\n\n/rename [ID] [NewID]\n/announce [Message]\n/kill_user [ID]\n/op [ID]\n/deop [ID]\n/kill_server\n/stop_server\n/quit [ID]\n/exit [ID]\n/kill [ID]\n/new_user\n");
                            }
                            else
                            {
                                MessageClient("\nUser Commands:\n\n/rename[NewID]\n/kill_user[ID] *\n/op[ID] *\n/deop[ID] *\n/kill_server *\n/stop_server *\n/playrps [OpponentID]\n/quit\n/exit\n/kill\n/rename_server [NewID] *\n\n* = Administrator Only\n", commandData[0]);
                            }
                            break;
                        }
                    case "/kill_user":
                        {
                            if (commandData[0] == "")
                            {
                                Log("Error:  Kill_User Failed - No User Selected");
                            }
                            else
                            {
                                if (ClientExists(commandData[0]))
                                {
                                    MessageClient("CODE::KILL", commandData[0]);
                                }
                                else
                                {
                                    Log("Error:  Kill_User Failed - Invalid User Selected");
                                }
                            }
                            break;
                        }
                    case "/rename_server":
                        {
                            if (commandData[0] == "")
                            {
                                Log("Error:  Server Rename Failed - No Data Given");
                            }
                            else
                            {
                                if (commandData.Count >= 2)
                                {
                                    if (ClientExists(commandData[0]))
                                    {
                                        string newID = "";
                                        for (int i = 1; i < commandData.Count; ++i)
                                        {
                                            newID += commandData[i];
                                            newID += " ";
                                        }
                                        this.serverID = newID.Substring(0,newID.Length-1);
                                        Announce(commandData[0] + " renamed the server to: " + this.serverID);
                                    }
                                    else
                                    {
                                        Log("Error:  Rename Server Failed - Invalid User Selected");
                                    }
                                }
                                else
                                {
                                    this.serverID = commandData[0];
                                    Announce("Server renamed to: " + this.serverID);
                                }
                            }
                            break;
                        }
                    case "/op":
                        {
                            if (commandData[0] == "")
                            {
                                Log("Error:  OP Failed - No User Selected");
                            }
                            else
                            {
                                if (ClientExists(commandData[0]))
                                {
                                    if (!GetClientFromID(commandData[0]).GetOP())
                                    {
                                        GetClientFromID(commandData[0]).SetOP(true);
                                        MessageClient("You are have been opped", commandData[0]);
                                        Log("Log: Opped " + GetClientFromID(commandData[0]).ID);
                                    }
                                }
                                else
                                {
                                    Log("Error:  OP Failed - Invalid User Selected");
                                }
                            }
                            break;
                        }
                    case "/deop":
                        {
                            if (commandData[0] == "")
                            {
                                Log("Error:  OP Failed - No User Selected");
                            }
                            else
                            {
                                if (ClientExists(commandData[0]))
                                {
                                    if (GetClientFromID(commandData[0]).GetOP())
                                    {
                                        GetClientFromID(commandData[0]).SetOP(false);
                                        MessageClient("You have been deoppped", commandData[0]);
                                        Log("Log: Deopped " + GetClientFromID(commandData[0]).ID);

                                    }
                                }
                                else
                                {
                                    Log("Error:  OP Failed - Invalid User Selected");
                                }
                            }
                            break;
                        }
                    case "/kill_server":
                    case "/stop_server":
                        {
                            serverWindow.Close();
                            break;
                        }
                    case "/quit":
                    case "/kill":
                    case "/exit":
                        {
                            if (commandData[0] == "")
                            {
                                serverWindow.Close();
                                break;
                            }
                            else
                            {
                                MessageClient("CODE::KILL", commandData[0]);
                            }
                            break;
                        }
                    default:
                        {
                            Log("Error: Invalid Command: " + command);
                            break;
                        }
                }
            }
        }
        string ProcessClientMessage(string command, int clientID)
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
                    /// Admin Commands (Pass ID) ///
                    case "/kill_server":
                    case "/stop_server":
                    case "/kill_user":
                    case "/rename_server":
                        {
                            if (clients[clientID].GetOP())
                            {
                                if (commandData != "")
                                {
                                    ProcessServerCommand(Message_Origins.CLIENT,commandType + " " + clients[clientID].ID + " " + commandData);
                                }
                                else
                                {
                                    MessageClient("Error - Invalid Command Parameters: " + command, clientID);
                                }
                            }
                            else
                            {
                                MessageClient("Error - You must be an op to use: " + command, clientID);
                            }
                            break;
                        }
                    /// Admin Commands (Pass Data) ///
                    case "/op":
                    case "/deop":
                        {
                            if (clients[clientID].GetOP())
                            {
                                if (commandData != "")
                                {
                                    ProcessServerCommand(Message_Origins.CLIENT,commandType + " " + commandData);
                                }
                                else
                                {
                                    MessageClient("Error - Invalid Command Parameters: " + command, clientID);
                                }
                            }
                            else
                            {
                                MessageClient("Error - You must be an op to use: " + command, clientID);
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
                        {
                            ProcessServerCommand(Message_Origins.CLIENT, commandType + " " + clients[clientID].ID + " " + commandData);
                            break;
                        }
                    /// Non-Admin Commands (Pass ID) ///
                    case "/help":
                        {
                            ProcessServerCommand(Message_Origins.CLIENT, commandType + " " + clients[clientID].ID);
                            break;
                        }
                    /// Quit Commands ///
                    case "/quit":
                    case "/kill":
                    case "/exit":
                        {
                            ProcessServerCommand(Message_Origins.CLIENT, commandType + " " + clients[clientID].ID);
                            return "CODE::KILL";
                        }
                    default:
                        {
                            MessageClient("Error - No Such Command: " + command, clientID);
                            break;
                        }
                }
            }
            else
            {
                MessageAllClients(clients[clientID].ID + ": " + command);
            }
            return "";
        }
        void Log(string text)
        {
            serverWindow.UpdateServerLog(text);
        }
        void Announce(string text)
        {
            Log("Server: " + text);
            MessageAllClients("Server: " + text);
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
        void MessageClient(string message, int clientIndex)
        {
            if (clientIndex >= 0 && clientIndex < clients.Count)
            {
                clients[clientIndex].writer.WriteLine(message);
                clients[clientIndex].writer.Flush();
            }
            else
            {
                Log("Message Sending Failed - Invalid Index - Message: " + message);
            }
        }
        void MessageClient(string message, Server_Client client)
        {
            client.writer.WriteLine(message);
            client.writer.Flush();
        }
        void MessageClient(string message, string clientID)
        {
            if (ClientExists(clientID))
            {
                GetClientFromID(clientID).writer.WriteLine(message);
                GetClientFromID(clientID).writer.Flush();
            }
            else
            {
                Log("Message Sending Failed - Invalid ID - Message: " + message);
            }
        }
        void MessageAllClients(string message)
        {
            foreach (Server_Client recClient in clients)
            {
                MessageClient(message, recClient);
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
