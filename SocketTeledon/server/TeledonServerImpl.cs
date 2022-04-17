using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using services;
using teledonCS.model;
using teledonCS.repository;

namespace server
{
    public class TeledonServerImpl:ITeledonServices
    {
        
        private IVolunteerRepository volunteerRepository;
        private ICharitableChaseRepository charitableCaseRepository;
        private IDonorRepository donorRepository;
        private IDonationRepository donationRepository;
        private readonly IDictionary<String,ITeledonObserver> loggedVolunteers;
        public TeledonServerImpl(IVolunteerRepository volunteerRepository, ICharitableChaseRepository charitableCaseRepository, IDonorRepository donorRepository, IDonationRepository donationRepository) {
            this.volunteerRepository = volunteerRepository;
            this.charitableCaseRepository = charitableCaseRepository;
            this.donorRepository = donorRepository;
            this.donationRepository = donationRepository;
            loggedVolunteers = new Dictionary<string, ITeledonObserver>();
        }

        public void login(string username, string password, ITeledonObserver client)
        {
            Volunteer volunteer = volunteerRepository.getByUsernameAndPassword(username, password);
            if (volunteer != null)
            {
                if (loggedVolunteers.ContainsKey(volunteer.username))
                {
                    throw new TeledonException("Volunteer already logged in...");
                }
                else
                {
                    loggedVolunteers[volunteer.username] = client;
                }
            }
            else
            {
                throw new TeledonException("Authentification failed...");
            }
        }

        public void logout(Volunteer volunteer, ITeledonObserver client)
        {
            ITeledonObserver localClient = loggedVolunteers[volunteer.username];
            if (localClient == null)
            {
                throw new TeledonException("Volunteer" + volunteer.id + " is not logged in..");
            }

            loggedVolunteers.Remove(volunteer.username);
        }

        public List<CharitableCase> getAllCharitableCase()
        {
            List<CharitableCase> cases = charitableCaseRepository.findAll();
            return cases;
        }

        public void saveDonation(Donation donation)
        {
            int oldAmount = donation.charitableCase.amountRaised;
            donationRepository.save(donation);

            int newAmount = oldAmount + donation.amountDonated;

            donation.charitableCase.amountRaised = newAmount;
            
            charitableCaseRepository.update(donation.charitableCase);

            notifyDonationSaved(donation);
        }

        private void notifyDonationSaved(Donation donation)
        {
            List<Volunteer> allVolunteers = volunteerRepository.findAll();
            foreach (var vol in allVolunteers)
            {
                if (loggedVolunteers.ContainsKey(vol.username))
                {
                    ITeledonObserver client = loggedVolunteers[vol.username];
                    Task.Run(() => client.donationSaved(donation));
                }
            }
        }

        public void saveDonor(string firstName, string lastName, string address, string phoneNr)
        {
            Donor donor = new Donor(firstName, lastName, address, phoneNr);
            donorRepository.save(donor);
        }

        public int getDonor(string firstName, string lastName)
        {
            int donorId = (int) donorRepository.getDonorByName(firstName, lastName);
            return donorId;
        }

        public Donor getDonorById(int id)
        {
            Donor donor = donorRepository.findOne(id);
            return donor;
        }

        public List<Donor> getAllDonorsByName(string name)
        {
            List<Donor> possibleDonors = donorRepository.findAll();
            List<Donor> finaleDonors = new List<Donor>();
            String nameLower = name.ToLower();
            foreach(Donor donor in possibleDonors)
            {
                if (donor.firstName.ToLower().StartsWith(nameLower) || donor.lastName.ToLower().StartsWith(nameLower))
                {
                    finaleDonors.Add(donor);
                }
            }

            return finaleDonors;
        }

        public Donor getDonorByFullName(string firstName, string lastName)
        {
            return donorRepository.getDonorByFullNameDonor(firstName, lastName);
        }

        public int getCharitableCaseByName(string name)
        {
            int id = (int) charitableCaseRepository.getCharitableCaseByName(name);
            return id;
        }
    }
}