using teledonCS.model;

namespace teledonCS.repository
{
    public interface IVolunteerRepository:ICrudRepository<int,Volunteer>
    {
        Volunteer getByUsernameAndPassword(string username, string password);
    }
}