package repository;

import model.CharitableCase;
import model.Donation;
import model.Donor;

public interface IDonationRepository extends ICrudRepository<Donation,Integer> {
    public void deleteDonation(Integer donor,Integer charitableCase);
    public Donor findByDonor(Integer donor);
    public CharitableCase findByCharitableCase(Integer charitableC);

}
