package com.example.teledonjfx.repository;

public interface ICrudRepository<T,Tid>{
    void save(T elem);
    void delete(Tid id);
    void update(T elem,Tid id);
    T findById(Tid id);
    Iterable<T> findAll();
    int size();
}
