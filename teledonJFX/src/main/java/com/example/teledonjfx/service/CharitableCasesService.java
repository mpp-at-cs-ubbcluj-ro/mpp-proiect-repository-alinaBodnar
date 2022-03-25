package com.example.teledonjfx.service;

import com.example.teledonjfx.model.CharitableCase;
import com.example.teledonjfx.repository.CharitableCaseRepository;
import com.example.teledonjfx.repository.ICharitableCaseRepository;

public class CharitableCasesService {
    private final ICharitableCaseRepository charitableCaseRepository;

    public CharitableCasesService(CharitableCaseRepository charitableCaseRepository) {
        this.charitableCaseRepository = charitableCaseRepository;
    }
    public Iterable<CharitableCase> getAll(){
        return charitableCaseRepository.findAll();
    }
    public void updateAmount(CharitableCase charitableCase,Integer id){
        charitableCaseRepository.update(charitableCase,id);
    }

    public Integer getCharitableCaseByName(String name){
        return charitableCaseRepository.getCharitableCaseByName(name);
    }
    public CharitableCase getCharitableCaseById(Integer id){
        return charitableCaseRepository.findById(id);
    }
}
