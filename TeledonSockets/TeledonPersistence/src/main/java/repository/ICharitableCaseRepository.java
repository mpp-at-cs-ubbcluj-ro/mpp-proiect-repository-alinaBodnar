package repository;

import model.CharitableCase;

public interface ICharitableCaseRepository extends ICrudRepository<CharitableCase,Integer>{
    Integer getCharitableCaseByName(String name);
}
