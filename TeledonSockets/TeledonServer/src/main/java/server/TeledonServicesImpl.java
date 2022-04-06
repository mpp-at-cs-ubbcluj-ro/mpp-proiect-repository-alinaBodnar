package server;

import model.CharitableCase;
import model.Donation;
import model.Donor;
import model.Volunteer;
import repository.CharitableCaseRepository;
import repository.DonationRepository;
import repository.DonorRepository;
import repository.VolunteerRepository;
import services.DonationExpection;
import services.IDonationObserver;
import services.IDonationServices;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.Executor;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

public class TeledonServicesImpl implements IDonationServices {

    private VolunteerRepository volunteerRepository;
    private CharitableCaseRepository charitableCaseRepository;
    private DonorRepository donorRepository;
    private DonationRepository donationRepository;
    private Map<String,IDonationObserver> loggedVolunteers;

    public TeledonServicesImpl(VolunteerRepository volunteerRepository, CharitableCaseRepository charitableCaseRepository, DonorRepository donorRepository, DonationRepository donationRepository) {
        this.volunteerRepository = volunteerRepository;
        this.charitableCaseRepository = charitableCaseRepository;
        this.donorRepository = donorRepository;
        this.donationRepository = donationRepository;
        loggedVolunteers = new ConcurrentHashMap<>();
    }


    @Override
    public synchronized void login(String username, String password, IDonationObserver client) throws DonationExpection {
        Volunteer volunteer = volunteerRepository.getByUsernameAndPassword(username,password);
        if(volunteer != null){
            if(loggedVolunteers.get(volunteer.getUsername()) != null) {
                throw new DonationExpection("Volunteer already logged in");
            }
            loggedVolunteers.put(volunteer.getUsername(),client);
        }
        else
            throw new DonationExpection("Authentification failed");


    }

    @Override
    public synchronized void logout(Volunteer volunteer, IDonationObserver client) throws DonationExpection {
        IDonationObserver localClient = loggedVolunteers.remove(volunteer.getUsername());
        if(localClient == null){
            throw new DonationExpection("Volunteer " + volunteer.getUsername() + " is not logged in");
        }
    }

    @Override
    public List<CharitableCase> getAllCharitableCase() throws DonationExpection {
        Iterable<CharitableCase> cases = charitableCaseRepository.findAll();
        List<CharitableCase> charitableCaseList = new ArrayList<>();
        cases.forEach(x -> charitableCaseList.add(x));
        return charitableCaseList;
    }

    @Override
    public void saveDonation(Donation donation) throws DonationExpection {
        Integer oldAmount = donation.getCharitableCase().getAmountRaised();
        donationRepository.save(donation);
        Integer newAmount = oldAmount + donation.getAmountDonated();
        donation.getCharitableCase().setAmountRaised(newAmount);
        charitableCaseRepository.update(donation.getCharitableCase(), donation.getCharitableCase().getId());
        notifyDonationSaved(donation);
    }

    @Override
    public void saveDonor(String firstName, String lastName, String address, String phoneNr) {
        Donor donor = new Donor(firstName,lastName,address,phoneNr);
        donorRepository.save(donor);
    }

    @Override
    public Integer getDonor(String firstName, String lastName) {
        return donorRepository.getDonorByName(firstName,lastName);
    }

    @Override
    public Donor getDonorById(Integer id) {
        return donorRepository.findById(id);
    }

    @Override
    public List<Donor> getAll() {
        Iterable<Donor> donors = donorRepository.findAll();
        List<Donor> allDonors = new ArrayList<>();
        donors.forEach(x -> allDonors.add(x));
        return allDonors;
    }

    @Override
    public List<Donor> getAllDonorsByName(String name) {
        List<Donor> possibleDonors = getAll();
        List<Donor> finaleDonors = new ArrayList<>();
        String nameLower = name.toLowerCase();
        for(Donor donor:possibleDonors){
            if(donor.getFirstName().toLowerCase().startsWith(nameLower) || donor.getLastName().toLowerCase().startsWith(nameLower))
                finaleDonors.add(donor);
        }
        return finaleDonors;
    }

    @Override
    public Donor getDonorByFullName(String firstName, String lastName) {
        return donorRepository.getDonorByFullName(firstName,lastName);
    }

    @Override
    public Integer getCharitableCaseByName(String name) throws DonationExpection {
        return charitableCaseRepository.getCharitableCaseByName(name);
    }

    private final int defaultThreadsNo = 5;
    private void notifyDonationSaved(Donation donation) {
        Iterable<Volunteer> all = volunteerRepository.findAll();
        ExecutorService executor = Executors.newFixedThreadPool(defaultThreadsNo);
        for(Volunteer vol: all){
            IDonationObserver client = loggedVolunteers.get(vol.getUsername());
            if(client != null){
                executor.execute(() ->{
                    try{
                        System.out.println("Notifying [ " + vol.getUsername() + "] volunteer");
                        client.donationSaved(donation);
                    } catch (DonationExpection e) {
                        System.out.println("Error notifying volunteer "+ e);
                    }
                });
            }
        }
    }

}
