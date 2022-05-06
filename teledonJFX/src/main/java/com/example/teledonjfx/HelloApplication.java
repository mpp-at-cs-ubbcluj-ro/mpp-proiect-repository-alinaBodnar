package com.example.teledonjfx;

import com.example.teledonjfx.model.CharitableCase;
import com.example.teledonjfx.repository.CharitableCaseRepository;
import com.example.teledonjfx.repository.DonationRepository;
import com.example.teledonjfx.repository.DonorRepository;
import com.example.teledonjfx.repository.VolunteerRepository;
import com.example.teledonjfx.service.CharitableCasesService;
import com.example.teledonjfx.service.DonationService;
import com.example.teledonjfx.service.DonorService;
import com.example.teledonjfx.service.VolunteerService;
import javafx.application.Application;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;

import java.io.FileReader;
import java.io.IOException;
import java.util.Properties;

public class HelloApplication extends Application {
    @Override
    public void start(Stage stage) throws IOException {
        FXMLLoader root = new FXMLLoader(getClass().getResource("login.fxml"));
        //FXMLLoader root1 = new FXMLLoader(getClass().getResource("charitableCases.fxml"));
        Pane myPane = (Pane) root.load();

        LoginController loginController = root.getController();
        loginController.setVolunteerService(getVolunteerService());
        loginController.setCharitableCasesService(getCharitableService());
        loginController.setDonationService(getDonationService());
        loginController.setDonorService(getDonorService());

        Scene scene = new Scene(myPane,520,400);
        stage.setScene(scene);
        stage.show();
    }

    public static void main(String[] args) {
        launch(args);
    }
    static VolunteerService getVolunteerService(){
        Properties serverProps=new Properties();
        try {
            serverProps.load(new FileReader("bd.config"));
            System.out.println("Properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.out.println("Cannot find bd.config "+e);
            return null;
        }
        VolunteerRepository volunteerRepository = new VolunteerRepository(serverProps);
        VolunteerService volunteerService = new VolunteerService(volunteerRepository);
        return volunteerService;
    }

    static CharitableCasesService getCharitableService(){
        Properties serverProps=new Properties();
        try {
            serverProps.load(new FileReader("bd.config"));
            System.out.println("Properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.out.println("Cannot find bd.config "+e);
            return null;
        }
        CharitableCaseRepository charitableCaseRepository = new CharitableCaseRepository(serverProps);
        CharitableCasesService charitableCasesService = new CharitableCasesService(charitableCaseRepository);
        return  charitableCasesService;
    }

    static DonorService getDonorService(){
        Properties serverProps=new Properties();
        try {
            serverProps.load(new FileReader("bd.config"));
            System.out.println("Properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.out.println("Cannot find bd.config "+e);
            return null;
        }
        DonorRepository donorRepository = new DonorRepository(serverProps);
        DonorService donorService = new DonorService(donorRepository);
        return donorService;
    }
    static DonationService getDonationService(){
        Properties serverProps=new Properties();
        try {
            serverProps.load(new FileReader("bd.config"));
            System.out.println("Properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.out.println("Cannot find bd.config "+e);
            return null;
        }
        DonorRepository donorRepository = new DonorRepository(serverProps);
        DonationRepository donationRepository = new DonationRepository(serverProps);
        CharitableCaseRepository charitableCaseRepository = new CharitableCaseRepository(serverProps);
        DonationService donationService = new DonationService(donationRepository,donorRepository,charitableCaseRepository);
        return donationService;
    }
}