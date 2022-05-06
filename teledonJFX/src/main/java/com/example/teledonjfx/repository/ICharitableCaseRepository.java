package com.example.teledonjfx.repository;

import com.example.teledonjfx.model.CharitableCase;

public interface ICharitableCaseRepository extends ICrudRepository<CharitableCase,Integer>{
    Integer getCharitableCaseByName(String name);
}
