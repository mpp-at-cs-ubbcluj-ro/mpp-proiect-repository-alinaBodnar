package model;

import java.io.Serializable;

public class Donation implements Identifiable<Integer>, Serializable {
    private int id;
    private int donorId;
    private int charitableCaseId;
    private double amountDonated;

    public Donation(int id, int donorId, int charitableCaseId, double amountDonated) {
        this.id = id;
        this.donorId = donorId;
        this.charitableCaseId = charitableCaseId;
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

    public int getDonorId() {
        return donorId;
    }

    public void setDonorId(int donorId) {
        this.donorId = donorId;
    }

    public int getCharitableCaseId() {
        return charitableCaseId;
    }

    public void setCharitableCaseId(int charitableCaseId) {
        this.charitableCaseId = charitableCaseId;
    }

    public double getAmountDonated() {
        return amountDonated;
    }

    public void setAmountDonated(double amountDonated) {
        this.amountDonated = amountDonated;
    }

    @Override
    public String toString() {
        return "Donation{" +
                "id=" + id +
                ", donorId=" + donorId +
                ", charitableCaseId=" + charitableCaseId +
                ", amountDonated=" + amountDonated +
                '}';
    }
}
