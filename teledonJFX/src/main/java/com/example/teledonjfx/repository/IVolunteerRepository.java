package com.example.teledonjfx.repository;

import com.example.teledonjfx.model.Volunteer;

public interface IVolunteerRepository extends ICrudRepository<Volunteer,Integer>{
    Volunteer getByUsernameAndPassword(String username,String password);
}
