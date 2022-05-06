using System;
using teledonCS.model;

namespace teledonCS.repository
{
    public interface IDonorRepository:ICrudRepository<int,Donor>
    {
        int? getDonorByName(string firstName, string lastName);
        Donor getDonorByFullName(String first, String last);
    }
}