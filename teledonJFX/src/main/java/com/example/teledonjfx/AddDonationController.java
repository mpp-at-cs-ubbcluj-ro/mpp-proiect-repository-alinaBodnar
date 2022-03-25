package com.example.teledonjfx;

import com.example.teledonjfx.model.CharitableCase;
import com.example.teledonjfx.model.Donor;
import com.example.teledonjfx.service.CharitableCasesService;
import com.example.teledonjfx.service.DonationService;
import com.example.teledonjfx.service.DonorService;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.TextField;
import javafx.stage.Stage;

import java.io.IOException;
import java.sql.SQLOutput;

public class AddDonationController {
    private CharitableCasesService charitableCasesService;
    private DonationService donationService;
    private DonorService donorService;
    private CharitableCase charitableCase;


    @FXML
    private Button SaveDonationButton;

    @FXML
    private TextField addressTextField;

    @FXML
    private TextField amountTextField;

    @FXML
    private TextField firstNameTextField;

    @FXML
    private TextField lastNameTextField;

    @FXML
    private TextField phoneNrTextField;

    @FXML
    void SaveDonationButtonOnAction(ActionEvent event) {
        String firstName = firstNameTextField.getText();
        String lastName = lastNameTextField.getText();
        String address = addressTextField.getText();
        String phoneNr = phoneNrTextField.getText();
        Integer amountDonated = Integer.parseInt(amountTextField.getText());

        donorService.saveDonor(firstName,lastName,address,phoneNr);
        Integer idDonor = donorService.getDonor(firstName,lastName);
        System.out.println(idDonor);
        Donor donor = donorService.getDonorById(idDonor);
        System.out.println(donor);


//        Integer newAmount = charitableCase.getAmountRaised() + amountDonated;
//        System.out.println(newAmount);
//        Integer charitableCaseId = charitableCasesService.getCharitableCaseByName(charitableCase.getName());
//        System.out.println(charitableCaseId);
//
//        CharitableCase charitableCaseUpdated = new CharitableCase(charitableCase.getName(), newAmount);
//        charitableCaseUpdated.setId(charitableCaseId);
//        charitableCasesService.updateAmount(charitableCaseUpdated,charitableCaseId);
//
//        donationService.saveDonation(charitableCase,donor,amountDonated);
    }

    public void setServices(CharitableCasesService charitableCasesService, DonorService donorService, DonationService donationService){
        this.charitableCasesService =charitableCasesService;
        this.donationService = donationService;
        this.donorService = donorService;
    }

    public void setCharitableCase(CharitableCase charitableCaseSelected) {
        this.charitableCase = charitableCaseSelected;
    }
}
