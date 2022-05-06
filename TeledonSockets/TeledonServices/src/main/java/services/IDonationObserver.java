package services;

import model.Donation;

public interface IDonationObserver {
    void donationSaved(Donation donation) throws DonationExpection;
}
