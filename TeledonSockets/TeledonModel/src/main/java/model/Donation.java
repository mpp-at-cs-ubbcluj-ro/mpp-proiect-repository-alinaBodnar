package model;


import java.io.Serializable;

public class Donation implements Identifiable<Integer>, Serializable {
    private int id;
    private Donor donor;
    private CharitableCase charitableCase;
    private int amountDonated;

    public Donation( Donor donorId, CharitableCase charitableCaseId, int amountDonated) {
        this.donor = donorId;
        this.charitableCase = charitableCaseId;
        this.amountDonated = amountDonated;
    }

    @Override
    public Integer getId() {
        return id;
    }
    @Override
    public void setId(Integer id) {
        this.id = id;
    }

    public Donor getDonor() {
        return donor;
    }

    public void setDonor(Donor donorId) {
        this.donor = donorId;
    }

    public CharitableCase getCharitableCase() {
        return charitableCase;
    }

    public void setCharitableCase(CharitableCase charitableCaseId) {
        this.charitableCase = charitableCaseId;
    }

    public int getAmountDonated() {
        return amountDonated;
    }

    public void setAmountDonated(int amountDonated) {
        this.amountDonated = amountDonated;
    }

    @Override
    public String toString() {
        return "Donation{" +
                "id=" + id +
                ", donorId=" + donor +
                ", charitableCaseId=" + charitableCase +
                ", amountDonated=" + amountDonated +
                '}';
    }
}
