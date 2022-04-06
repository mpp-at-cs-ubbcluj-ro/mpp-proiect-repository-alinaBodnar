package services;

import model.CharitableCase;
import model.Donation;
import model.Donor;
import model.Volunteer;

import java.util.List;

public interface IDonationServices {
    void login(String username,String password,IDonationObserver client) throws DonationExpection;
    void logout(Volunteer volunteer,IDonationObserver client) throws DonationExpection;
    List<CharitableCase> getAllCharitableCase() throws DonationExpection;
    void saveDonation(Donation donation) throws DonationExpection;
    void saveDonor(String firstName,String lastName,String address,String phoneNr) throws DonationExpection;
    Integer getDonor(String firstName,String lastName) throws DonationExpection;
    Donor getDonorById(Integer id) throws DonationExpection;
    List<Donor> getAll();
    List<Donor> getAllDonorsByName(String name) throws DonationExpection;
    Donor getDonorByFullName(String firstName,String lastName) throws DonationExpection;
    Integer getCharitableCaseByName(String name)throws DonationExpection;

}
