using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;
using System.Threading;
using networking;
using services;
using teledonCS.repository;

namespace server
{
    public class StartServer
    {
        static void Main(string[] args)
        {
            IDictionary<String, string> serverProps = new SortedList<string, string>();
            serverProps.Add("ConnectionString",GetConnectionStringByName("teledonTV"));

            IVolunteerRepository volunteerRepository = new VolunteerRepository(serverProps);
            ICharitableChaseRepository charitableCaseRepository = new CharitableCaseRepository(serverProps);
            IDonorRepository donorRepository = new DonorRepository(serverProps);
            IDonationRepository donationRepository = new DonationRepository(serverProps);
            TeledonServerImpl donationServer = new TeledonServerImpl(volunteerRepository,charitableCaseRepository,donorRepository,donationRepository);

            SerialTeledonServer server = new SerialTeledonServer("127.0.0.1", 55556, donationServer);
            server.Start();
            Console.WriteLine("Server started...");
            Console.ReadLine();

        }
        static  string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
   
    public class SerialTeledonServer:ServerUtils.ConcurrentServer
    {
        private ITeledonServices server;
        private TeledonWorker worker;
        public SerialTeledonServer(string host, int port,ITeledonServices server) : base(host, port)
        {
            this.server = server;
            Console.WriteLine("SerialTeledonServer...");
        }

        protected override Thread createWorker(TcpClient client)
        {
            worker = new TeledonWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }
}