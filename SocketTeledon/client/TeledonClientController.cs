using System;
using System.Collections.Generic;
using services;
using teledonCS.model;

namespace clientCs
{
    public class TeledonClientController:ITeledonObserver
    {
        public TeledonClientController(ITeledonServices server)
        {
            this.server = server;
            currentVolunteer = null;
        }

        public event EventHandler<TeledonUserEventArgs> updateEvent;
        private readonly ITeledonServices server;
        private Volunteer currentVolunteer;

        public void login(String username, String password)
        {
            Volunteer volunteer = new Volunteer(username, password);
            server.login(username,password,this);
            Console.WriteLine("Login succeded...");
            currentVolunteer = volunteer;
            Console.WriteLine("Current volunteer {0}",volunteer);
        }

        public void logout()
        {
            server.logout(currentVolunteer,this);
            Console.WriteLine("Logout succeded...");
            currentVolunteer = null;
        }
        public void donationSaved(Donation donation)
        {
            TeledonUserEventArgs userEventArgs = new TeledonUserEventArgs(TeledonUserEvent.NEW_DONATION, donation);
            Console.WriteLine("Donation saved");
            OnUserEvent(userEventArgs);
            
            // server.saveDonation(donation);
        }

        public void saveDonation(Donation donation)
        {
            server.saveDonation(donation);
        }
        
        
        
        protected virtual void OnUserEvent(TeledonUserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }

        public List<CharitableCase> getCharitableCases()
        {
            return server.getAllCharitableCase();
        }

        public List<Donor> getDonorsByName(string nameDonor)
        {
            return server.getAllDonorsByName(nameDonor);
        }

        public Donor getDonorByFullName(string firstName, string lastName)
        {
            return server.getDonorByFullName(firstName, lastName);
        }

        public void saveDonor(string firstName, string lastName, string address, string phoneNr)
        {
            server.saveDonor(firstName,lastName,address,phoneNr);
        }

        public int getDonor(string firstName, string lastName)
        {
            return server.getDonor(firstName, lastName);
        }

        public Donor getDonorById(int idNewDonor)
        {
            return server.getDonorById(idNewDonor);
        }

        public int getCharitableCaseByName(string charitableCaseName)
        {
            return server.getCharitableCaseByName(charitableCaseName);
        }
    }
}