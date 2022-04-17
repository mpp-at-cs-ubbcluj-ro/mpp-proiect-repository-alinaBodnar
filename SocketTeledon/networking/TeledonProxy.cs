using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using services;
using teledonCS.model;

namespace networking
{
    public class TeledonServerProxy:ITeledonServices
    {
        private string host;
        private int port;

        private ITeledonObserver client;

        private NetworkStream _stream;

        private IFormatter _formatter;
        private TcpClient connection;

        private Queue<Response> _responses;
        private volatile bool finished;
        private EventWaitHandle _waitHandle;

        public TeledonServerProxy(string host, int port)
        {
            this.host = host;
            this.port = port;
            _responses = new Queue<Response>();
        }

        public virtual void login(string username, string password, ITeledonObserver client)
        {
            initializeConnection();
            Volunteer volunteer = new Volunteer(username, password);
            sendRequest(new LoginRequest(volunteer));
            Response response = readResponse();
            if (response is OkResponse)
            {
                this.client = client;
                return;
            }

            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse)response;
                closeConnection();
                throw new TeledonException(err.Message);
            }

        }
        
     

        private void closeConnection()
        {
            finished = true;
            try
            {
                _stream.Close();

                connection.Close();
                _waitHandle.Close();
                client = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private Response readResponse()
        {
            Response response = null;
            try
            {
                _waitHandle.WaitOne();
                lock (_responses)
                {
                    response = _responses.Dequeue();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return response;
        }

        private void sendRequest(Request request)
        {
            try
            {
                _formatter.Serialize(_stream, request);
                _stream.Flush();
            }
            catch (Exception e)
            {
                throw new TeledonException("error sending object " + e);
            }
        }

        private void initializeConnection()
        {
            try
            {
                connection = new TcpClient(host, port);
                _stream = connection.GetStream();
                _formatter = new BinaryFormatter();
                finished = false;
                _waitHandle = new AutoResetEvent(false);
                startReader();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void startReader()
        {
            Thread tw = new Thread(run);
            tw.Start();
        }

        public virtual void run()
        {
            while (!finished)
            {
                try
                {
                    object response = _formatter.Deserialize(_stream);
                    Console.WriteLine("response received " + response);
                    if (response is UpdateResponse)
                    {
                        handleUpdate((UpdateResponse) response);
                    }
                    else
                    {
                        lock (_responses)
                        {
                            _responses.Enqueue((Response) response);
                        }

                        _waitHandle.Set();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Reading error " + e);
                }
            }
        }

        private void handleUpdate(UpdateResponse response)
        {
            if (response is NewDonationResponse)
            {
                NewDonationResponse resp = (NewDonationResponse) response;
                try
                {
                   
                    Task.Run(() =>  client.donationSaved(resp.GetDonation));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }
        


        public virtual void logout(Volunteer volunteer, ITeledonObserver client)
        {
            sendRequest(new LogoutRequest(volunteer));
            Response response = readResponse();
            closeConnection();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                throw new TeledonException(err.Message);
            }
        }

        public virtual List<CharitableCase> getAllCharitableCase()
        {
            sendRequest(new GetCharitableCasesRquest());
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                throw new TeledonException(err.Message);
            }

            GetCharitableCasesResponse casesResponse = (GetCharitableCasesResponse) response;
            List<CharitableCase> cases = casesResponse.GetCases;
            return cases;

        }

        public virtual void saveDonation(Donation donation)
        {
            sendRequest(new SaveDonationRequest(donation));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                throw new TeledonException(err.Message);
            }
        }

        public virtual void saveDonor(string firstName, string lastName, string address, string phoneNr)
        {
            string info = firstName + " " + lastName + " " + address + " " + phoneNr;
            sendRequest(new SaveDonorRequest(info));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                throw new TeledonException(err.Message);
            }

        }

        public virtual int getDonor(string firstName, string lastName)
        {
            String fullName = firstName + " " + lastName;
            sendRequest(new GetDonorIdRequest(fullName));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                throw new TeledonException(err.Message);
            }

            GetDonorIdResponse donorIdResponse = (GetDonorIdResponse) response;
            int id = donorIdResponse.getId;
            return id;
        }

        public virtual Donor getDonorById(int id)
        {
            sendRequest(new GetDonorRequest(id));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                throw new TeledonException(err.Message);
            }

            GetDonorResponse donorResponse = (GetDonorResponse) response;
            Donor donor = donorResponse.GetDonor;
            return donor;
        }
        

        public virtual List<Donor> getAllDonorsByName(string name)
        {
            sendRequest(new GetDonorsByNameRequest(name));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                throw new TeledonException(err.Message);
            }

            GetDonorsByNameResponse donorByFullNameResponse = (GetDonorsByNameResponse) response;
            List<Donor> donors = donorByFullNameResponse.Donors;
            return donors;
        }

        public virtual Donor getDonorByFullName(string firstName, string lastName)
        {
            string fullName = firstName + " " + lastName;
            sendRequest(new GetDonorByFullNameRequest(fullName));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                throw new TeledonException(err.Message);
            }

            GetDonorByFullNameResponse donorByFullNameResponse = (GetDonorByFullNameResponse) response;
            Donor donor = donorByFullNameResponse.GetDonor;
            return donor;
        }

        public virtual int getCharitableCaseByName(string name)
        {
            sendRequest(new GetCharitableCaseRequest(name));
            Response response = readResponse();
            if (response is ErrorResponse)
            {
                ErrorResponse err = (ErrorResponse) response;
                throw new TeledonException(err.Message);
            }

            GetCharitableCaseResponse charitableCaseResponse = (GetCharitableCaseResponse) response;
            int id = charitableCaseResponse.getCase;
            return id;


        }
    }
}