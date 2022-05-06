using System;
using System.Collections.Generic;
using teledonCS.model;
using teledonCS.repository;

namespace teledonDesktop.services
{
    public class DonorService
    {
        public IDonorRepository DonorRepository;

        public DonorService(IDonorRepository donorRepository)
        {
            DonorRepository = donorRepository;
        }

        public void saveDonor(String firstName, String lastName, String address, String phoneNr)
        {
            Donor donor = new Donor(firstName, lastName, address, phoneNr);
            DonorRepository.save(donor);
        }

        public int getDonor(String firstName, String lastName)
        {
            return (int) DonorRepository.getDonorByName(firstName, lastName);
        }

        public Donor getDonorById(int id)
        {
            Donor donor = DonorRepository.findOne(id);
            return donor;
        }

        public List<Donor> getAll()
        {
            return DonorRepository.findAll();
        }

        public List<Donor> getAllDonorsByName(String name)
        {
            List<Donor> possibleDonors = getAll();
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

        public Donor getDonorByFullName(String firstName, String lastName)
        {
            return DonorRepository.getDonorByFullName(firstName, lastName);
        }
    }
}