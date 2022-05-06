using System;
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
            Console.WriteLine("Message received");
            OnUserEvent(userEventArgs);
        }
        
        protected virtual void OnUserEvent(TeledonUserEventArgs e)
        {
            if (updateEvent == null) return;
            updateEvent(this, e);
            Console.WriteLine("Update Event called");
        }
    }
}