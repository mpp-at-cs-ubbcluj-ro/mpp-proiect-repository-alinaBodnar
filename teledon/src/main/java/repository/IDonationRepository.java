package repository;

import model.CharitableCase;
import model.Donor;

public interface IDonationRepository<Donation,Integer> extends ICrudRepository<Donation,Integer> {
    public void deleteDonation(Integer donor,Integer charitableCase);
    public Donor findByDonor(Integer donor);
    public CharitableCase findByCharitableCase(Integer charitableC);

}
