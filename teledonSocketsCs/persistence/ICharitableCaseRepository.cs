using teledonCS.model;

namespace teledonCS.repository
{
    public interface ICharitableChaseRepository:ICrudRepository<int,CharitableCase>
    {
        int? getCharitableCaseByName(string name);
    }
}