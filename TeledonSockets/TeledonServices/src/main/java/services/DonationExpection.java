package services;

import model.Donation;

public class DonationExpection extends Exception{
    public DonationExpection(){
        super();
    }
    public DonationExpection(String message){
        super(message);
    }
    public DonationExpection(String message,Throwable cause){
        super(message,cause);
    }
}
