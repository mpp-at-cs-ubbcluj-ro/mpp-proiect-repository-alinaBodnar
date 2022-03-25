package com.example.teledonjfx.service;

import com.example.teledonjfx.model.Donor;
import com.example.teledonjfx.repository.DonorRepository;
import com.example.teledonjfx.repository.IDonorRepository;

import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

public class DonorService {
    private IDonorRepository donorRepository;

    public DonorService(DonorRepository donorRepository) {
        this.donorRepository = donorRepository;
    }

    public void saveDonor(String firstName,String lastName,String address,String phoneNr){
        Donor donor = new Donor(firstName,lastName,address,phoneNr);
        donorRepository.save(donor);
    }
    public Integer getDonor(String firstName,String lastName){
        return donorRepository.getDonorByName(firstName,lastName);
    }
    public Donor getDonorById(Integer id){
        return donorRepository.findById(id);
    }

    public List<Donor> getAll(){
        Iterable<Donor> donors = donorRepository.findAll();
        List<Donor> allDonors = new ArrayList<>();
        donors.forEach(x -> allDonors.add(x));
        return allDonors;
    }

    public List<Donor> getAllDonorsByName(String name) {
        List<Donor> possibleDonors = getAll();
        List<Donor> finaleDonors = new ArrayList<>();
        String nameLower = name.toLowerCase();
        for(Donor donor:possibleDonors){
            if(donor.getFirstName().toLowerCase().startsWith(nameLower) || donor.getLastName().toLowerCase().startsWith(nameLower))
                finaleDonors.add(donor);
        }
        return finaleDonors;
    }

    public Donor getDonorByFullName(String firstName, String lastName) {
        return donorRepository.getDonorByFullName(firstName,lastName);
    }
}
