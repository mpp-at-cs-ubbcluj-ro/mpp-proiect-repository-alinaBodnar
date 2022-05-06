package repository;

import model.Donor;

public interface IDonorRepository extends ICrudRepository<Donor,Integer>{
    Integer getDonorByName(String firstName,String lastName);
    Donor getDonorByFullName(String first,String last);
}
