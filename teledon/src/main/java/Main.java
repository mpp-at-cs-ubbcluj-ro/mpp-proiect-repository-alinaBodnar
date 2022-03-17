import model.CharitableCase;
import model.Donor;
import model.Volunteer;
import repository.CharitableCaseRepository;
import repository.DonorRepository;
import repository.VolunteerRepository;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class Main {
    public static void main(String[] args) {
        System.out.println("Hello!");
        Donor donor = new Donor(1,"Vasile","Popescu","Cluj 190J","071234455");
        System.out.println(donor.toString());
        System.out.println(donor.getAddress());
        Properties properties = new Properties();
        try{
            properties.load(new FileReader("bd.config"));
        } catch (IOException e) {
            System.out.println("Cannot find bd.config" + e);
        }
        CharitableCaseRepository charitableCaseRepository = new CharitableCaseRepository(properties);
        CharitableCase charitable = new CharitableCase(2,"Poluare",2000);
        charitableCaseRepository.save(charitable);
        CharitableCase charitableCase1 = new CharitableCase(3,"Foamete",50000);
        charitableCaseRepository.update(charitableCase1,2);
        charitableCaseRepository.delete(3);
        for(CharitableCase charitableCase:charitableCaseRepository.findAll()){
            System.out.println(charitableCase);
        }
//        DonorRepository donorRepository = new DonorRepository(properties);
//        donorRepository.save(donor);
//        Donor dono1 = new Donor(2,"Maria","Hasna","Clujjjjj","0788888888");
//        donorRepository.update(dono1,1);
//        for (Donor donor1: donorRepository.findAll()){
//            System.out.println(donor1);
//        }
//        VolunteerRepository volunteerRepository = new VolunteerRepository(properties);
//        Volunteer volunteer = new Volunteer(1,"mari12","123");
//        volunteerRepository.save(volunteer);
//        Volunteer volunteer1 = new Volunteer(2,"marinel","bicicleta");
//        volunteerRepository.update(volunteer1,2);
//        for(Volunteer volunteer2: volunteerRepository.findAll()){
//            System.out.println(volunteer2);
//        }
//        System.out.println(volunteerRepository.size());
//        volunteerRepository.delete(3);
//        System.out.println(volunteerRepository.size());
    }
}
