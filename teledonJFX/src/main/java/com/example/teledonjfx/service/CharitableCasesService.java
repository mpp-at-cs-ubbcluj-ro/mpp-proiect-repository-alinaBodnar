package com.example.teledonjfx.service;

import com.example.teledonjfx.model.CharitableCase;
import com.example.teledonjfx.repository.CharitableCaseRepository;
import com.example.teledonjfx.repository.ICharitableCaseRepository;
import com.example.teledonjfx.utils.Observable;
import com.example.teledonjfx.utils.Observer;
import com.example.teledonjfx.utils.events.CharitableCaseEvent;

import java.util.ArrayList;
import java.util.List;

public class CharitableCasesService implements Observable<CharitableCaseEvent> {
    private final ICharitableCaseRepository charitableCaseRepository;
    private List<Observer<CharitableCaseEvent>> observerList = new ArrayList<>();

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

    @Override
    public void addObserver(Observer<CharitableCaseEvent> e) {
        observerList.add(e);
    }

    @Override
    public void removeObserver(Observer<CharitableCaseEvent> e) {
        observerList.remove(e);

    }

    @Override
    public void notifyObservers(CharitableCaseEvent t) {
        observerList.forEach(x -> x.update(t));
    }
}
