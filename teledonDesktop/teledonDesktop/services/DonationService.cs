using System;
using System.Collections.Generic;
using teledonCS.model;
using teledonCS.repository;
using teledonCS.utils.observer;
using teledonCS.utils.observer.events;

namespace teledonDesktop.services
{
    public class DonationService:Observable<DonationEvent>
    {
        public IDonationRepository DonationRepository;
        public IDonorRepository DonorRepository;
        public ICharitableChaseRepository charitableChaseRepository;
        public List<Observer<DonationEvent>> observers = new List<Observer<DonationEvent>>();

        public DonationService(IDonationRepository donationRepository, IDonorRepository donorRepository, ICharitableChaseRepository charitableChaseRepository)
        {
            DonationRepository = donationRepository;
            DonorRepository = donorRepository;
            this.charitableChaseRepository = charitableChaseRepository;
        }

        public void addObserver(Observer<DonationEvent> e)
        {
            observers.Add(e);
        }

        public void removeObserver(Observer<DonationEvent> e)
        {
            observers.Remove(e);
        }

        public void notifyObserver(DonationEvent t)
        {
            foreach (var observer in observers)
            {
                observer.update(t);
            }
        }

        public void saveDonation(CharitableCase charitableCase, Donor donor, int amount)
        {
           
            int oldAmount = charitableCase.amountRaised;
            int newAmount = oldAmount + amount;
            CharitableCase charitableCaseUpdated = new CharitableCase(charitableCase.name, newAmount);
            charitableCaseUpdated.id = charitableCase.id;
            Donation newDonation = new Donation(donor, charitableCase, amount);
            DonationRepository.save(newDonation);
            
            charitableChaseRepository.update(charitableCaseUpdated); 
            notifyObserver(new DonationEvent(ChangeEventType.ADD,charitableCase));

            
          

        }
    }
}