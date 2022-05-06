package repository;

import model.Volunteer;

public interface IVolunteerRepository extends ICrudRepository<Volunteer,Integer>{
    Volunteer getByUsernameAndPassword(String username,String password);
}
