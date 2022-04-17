using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Threading;
using services;
using teledonCS.model;

namespace networking
{
    public class TeledonWorker:ITeledonObserver
    {
        private ITeledonServices server;
        private TcpClient connection;

        private NetworkStream stream;
        private IFormatter _formatter;
        private volatile bool connected;

        public TeledonWorker(ITeledonServices server, TcpClient connection)
        {
            this.server = server;
            this.connection = connection;
            try
            {
                stream = connection.GetStream();
                _formatter = new BinaryFormatter();
                connected = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        public virtual void run()
        {
            while(connected)
            {
                try
                {
                    object request = _formatter.Deserialize(stream);
                    object response =handleRequest((Request)request);
                    if (response!=null)
                    {
                        sendResponse((Response) response);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
				
                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
            try
            {
                stream.Close();
                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error "+e);
            }
        }

        private void sendResponse(Response response)
        {
            Console.WriteLine("sending response " + response);
            lock (stream)
            {
                _formatter.Serialize(stream,response);
                stream.Flush();
            }
        }

        private object handleRequest(Request request)
        {
            Response response = null;
            if (request is LoginRequest)
            {
                Console.WriteLine("Login request...");
                LoginRequest loginRequest = (LoginRequest) request;
                try
                {
                    lock (server)
                    {
                        server.login(loginRequest.Volunteer.username, loginRequest.Volunteer.password, this);
                    }

                    return new OkResponse();
                }catch (TeledonException e)
                {
                    connected=false;
                    return new ErrorResponse(e.Message);
                }
            }
            if (request is LogoutRequest)
            {
                Console.WriteLine("Logout request...");
                LogoutRequest logoutRequest = (LogoutRequest) request;
                try
                {
                    lock (server)
                    {
                        server.logout(logoutRequest.Volunteer, this);
                    }

                    connected = false;
                    return new OkResponse();
                }catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetCharitableCasesRquest)
            {
                Console.WriteLine("get charitable cases request..");
                GetCharitableCasesRquest charitableCasesRquest = (GetCharitableCasesRquest) request;
                try
                {
                    List<CharitableCase> cases = null;
                    lock (server)
                    {
                        cases = server.getAllCharitableCase();
                    }

                    return new GetCharitableCasesResponse(cases);
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is SaveDonationRequest)
            {
                Console.WriteLine("saving donation...");
                SaveDonationRequest saveDonationRequest = (SaveDonationRequest) request;
                Donation donation = saveDonationRequest.GetDonation;
                try
                {
                    lock (server)
                    {
                        server.saveDonation(donation);
                    }

                    return new OkResponse();
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetDonorsByNameRequest)
            {
                GetDonorsByNameRequest donorsByNameRequest = (GetDonorsByNameRequest) request;
                String name = donorsByNameRequest.Name;
                try
                {
                    List<Donor> donors = null;
                    lock (server)
                    {
                        donors = server.getAllDonorsByName(name);
                    }

                    return new GetDonorsByNameResponse(donors);
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetDonorByFullNameRequest)
            {
                GetDonorByFullNameRequest donorByFullNameRequest = (GetDonorByFullNameRequest) request;
                String name = donorByFullNameRequest.GetFullName;
                String[] names = name.Split(' ');
                String firstName = names[0];
                String lastName = names[1];
                try
                {
                    Donor donor = null;
                    lock (server)
                    {
                        donor  = server.getDonorByFullName(firstName, lastName);
                    }

                    return new GetDonorByFullNameResponse(donor);
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is SaveDonorRequest)
            {
                SaveDonorRequest donorRequest = (SaveDonorRequest) request;
                String info = donorRequest.Info;
                String[] cols = info.Split(' ');
                String firstName = cols[0];
                String lastName = cols[1];
                String address = cols[2];
                String phoneNr = cols[3];
                try
                {
                    lock (server)
                    {
                        server.saveDonor(firstName, lastName, address, phoneNr);
                    }

                    return new OkResponse();
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
                
            }

            if (request is GetDonorIdRequest)
            {
                GetDonorIdRequest donorIdRequest = (GetDonorIdRequest) request;
                String fullname = donorIdRequest.FullName;
                String[] names = fullname.Split(' ');
                String firstName = names[0];
                String lastName = names[1];
                try
                {
                    int id;
                    lock (server)
                    {
                        id = server.getDonor(firstName, lastName);
                    }

                    return new GetDonorIdResponse(id);
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            if (request is GetDonorRequest)
            {
                GetDonorRequest donorRequest = (GetDonorRequest) request;
                int id = donorRequest.GetId;
                try
                {
                    Donor donor = null;
                    lock (server)
                    {
                        donor = server.getDonorById(id);
                    }

                    return new GetDonorResponse(donor);
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
                
            }

            if (request is GetCharitableCaseRequest)
            {
                GetCharitableCaseRequest charitableCaseRequest = (GetCharitableCaseRequest) request;
                String name = charitableCaseRequest.getName;
                try
                {
                    int id;
                    lock (server)
                    {
                        id = server.getCharitableCaseByName(name);
                    }

                    return new GetCharitableCaseResponse(id);
                }
                catch (TeledonException e)
                {
                    return new ErrorResponse(e.Message);
                }
            }

            return response;
        }

        public void donationSaved(Donation donation)
        {
            Console.WriteLine("Donation saved " + donation);
            try
            {
                sendResponse(new NewDonationResponse(donation));
            }
            catch (Exception e)
            {
                throw new TeledonException("Sendind error " + e);
            }
        }
    }
}