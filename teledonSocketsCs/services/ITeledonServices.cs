using System;
using System.Collections.Generic;
using teledonCS.model;

namespace services
{
    public interface ITeledonServices
    {
        void login(String username,String password,ITeledonObserver client);
        void logout(Volunteer volunteer,ITeledonObserver client);
        List<CharitableCase> getAllCharitableCase();
        void saveDonation(Donation donation);
        void saveDonor(String firstName,String lastName,String address,String phoneNr);
        int getDonor(String firstName,String lastName);
        Donor getDonorById(int id);
        List<Donor> getAllDonorsByName(String name);
        Donor getDonorByFullName(String firstName,String lastName);
        int getCharitableCaseByName(String name);
    }
    
}