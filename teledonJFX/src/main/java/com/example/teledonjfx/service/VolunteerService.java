package com.example.teledonjfx.service;

import com.example.teledonjfx.model.Volunteer;
import com.example.teledonjfx.repository.IVolunteerRepository;
import com.example.teledonjfx.repository.VolunteerRepository;

public class VolunteerService {
    private final IVolunteerRepository volunteerRepository;

    public VolunteerService(VolunteerRepository volunteerRepository) {
        this.volunteerRepository = volunteerRepository;
    }

    public Volunteer getVolunteer(String username,String password){
        return volunteerRepository.getByUsernameAndPassword(username,password);
    }
}
