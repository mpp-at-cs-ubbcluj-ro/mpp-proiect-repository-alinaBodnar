package com.example.teledonjfx.repository;

import com.example.teledonjfx.model.CharitableCase;
import com.example.teledonjfx.model.Donation;
import com.example.teledonjfx.model.Donor;

public interface IDonationRepository extends ICrudRepository<Donation,Integer> {
    public void deleteDonation(Integer donor,Integer charitableCase);
    public Donor findByDonor(Integer donor);
    public CharitableCase findByCharitableCase(Integer charitableC);

}
