package com.example.teledonjfx.repository;

import com.example.teledonjfx.model.Donor;

public interface IDonorRepository extends ICrudRepository<Donor,Integer>{
    Integer getDonorByName(String firstName,String lastName);
}
