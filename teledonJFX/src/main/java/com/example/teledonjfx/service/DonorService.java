package com.example.teledonjfx.service;

import com.example.teledonjfx.model.Donor;
import com.example.teledonjfx.repository.DonorRepository;
import com.example.teledonjfx.repository.IDonorRepository;

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
}
