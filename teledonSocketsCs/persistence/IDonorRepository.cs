using System;
using teledonCS.model;

namespace teledonCS.repository
{
    public interface IDonorRepository:ICrudRepository<int,Donor>
    {
        int? getDonorByName(string firstName, string lastName);
        Donor getDonorByFullNameDonor(String first, String last);
    }
}