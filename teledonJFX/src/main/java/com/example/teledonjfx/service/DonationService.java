package com.example.teledonjfx.service;

import com.example.teledonjfx.model.CharitableCase;
import com.example.teledonjfx.model.Donation;
import com.example.teledonjfx.model.Donor;
import com.example.teledonjfx.repository.*;
import com.example.teledonjfx.utils.Observable;
import com.example.teledonjfx.utils.Observer;
import com.example.teledonjfx.utils.events.DonationEvent;

import java.util.ArrayList;
import java.util.List;

public class DonationService implements Observable<DonationEvent> {
    private IDonationRepository donationRepository;
    private IDonorRepository donorRepository;
    private ICharitableCaseRepository charitableCaseRepository;

    public DonationService(DonationRepository donationRepository, DonorRepository donorRepository, CharitableCaseRepository charitableCaseRepository) {
        this.donationRepository = donationRepository;
        this.donorRepository = donorRepository;
        this.charitableCaseRepository = charitableCaseRepository;
    }

    private List<Observer<DonationEvent>> observerList = new ArrayList<>();

    @Override
    public void addObserver(Observer<DonationEvent> e) {
        observerList.add(e);
    }

    @Override
    public void removeObserver(Observer<DonationEvent> e) {
        observerList.remove(e);

    }

    @Override
    public void notifyObservers(DonationEvent t) {
        observerList.forEach(x -> x.update(t));

    }
    public void saveDonation(CharitableCase charitableCase, Donor donor,Integer amount){
        Donation newDonation = new Donation(donor,charitableCase,amount);
        donationRepository.save(newDonation);
    }
}