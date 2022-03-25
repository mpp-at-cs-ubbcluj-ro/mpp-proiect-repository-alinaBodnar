package com.example.teledonjfx;

import com.example.teledonjfx.model.CharitableCase;
import com.example.teledonjfx.model.Donor;
import com.example.teledonjfx.service.CharitableCasesService;
import com.example.teledonjfx.service.DonationService;
import com.example.teledonjfx.service.DonorService;
import com.example.teledonjfx.utils.Observable;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.stage.Stage;

import java.io.IOException;
import java.sql.SQLOutput;
import java.util.List;

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
    private Label messageLabel;

    @FXML
    private TableView<Donor> donorsTable;

    @FXML
    private TableColumn<Donor, String> firstName;

    @FXML
    private TableColumn<Donor, String> lastName;

    @FXML
    private TableColumn<Donor, String> address;

    @FXML
    private TableColumn<Donor, String> phoneNumber;

    @FXML
    private TextField nameFinderTextField;

    @FXML
    private Button searchDonorButton;

    ObservableList<Donor> donorObservable = FXCollections.observableArrayList();

    @FXML
    private void initialize(){
        firstName.setCellValueFactory(new PropertyValueFactory<Donor,String>("firstName"));
        lastName.setCellValueFactory(new PropertyValueFactory<Donor,String>("lastName"));
        address.setCellValueFactory(new PropertyValueFactory<Donor,String>("address"));
        phoneNumber.setCellValueFactory(new PropertyValueFactory<Donor,String>("phoneNumber"));
        donorsTable.setItems(donorObservable);
    }

    @FXML
    void SaveDonationButtonOnAction(ActionEvent event) {
       String firstName = firstNameTextField.getText();
        String lastName = lastNameTextField.getText();
        String address = addressTextField.getText();
        String phoneNr = phoneNrTextField.getText();
        Integer amountDonated = Integer.parseInt(amountTextField.getText());

        Donor found = donorService.getDonorByFullName(firstName,lastName);
        if(found == null){
            donorService.saveDonor(firstName,lastName,address,phoneNr);
            Integer idDonor = donorService.getDonor(firstName,lastName);
            Donor donor = donorService.getDonorById(idDonor);
            Integer charitableCaseId = charitableCasesService.getCharitableCaseByName(charitableCase.getName());
            charitableCase.setId(charitableCaseId);


            donationService.saveDonation(charitableCase,donor,amountDonated);

            firstNameTextField.clear();
            lastNameTextField.clear();
            addressTextField.clear();
            phoneNrTextField.clear();
            amountTextField.clear();

            messageLabel.setText("Successfully added donation");
        }
        else{
            Integer charitableCaseId = charitableCasesService.getCharitableCaseByName(charitableCase.getName());
            charitableCase.setId(charitableCaseId);


            donationService.saveDonation(charitableCase,found,amountDonated);

            firstNameTextField.clear();
            lastNameTextField.clear();
            addressTextField.clear();
            phoneNrTextField.clear();
            amountTextField.clear();

            messageLabel.setText("Successfully added donation");
        }




    }

    public void setServices(CharitableCasesService charitableCasesService, DonorService donorService, DonationService donationService){
        this.charitableCasesService =charitableCasesService;
        this.donationService = donationService;
        this.donorService = donorService;
    }

    public void setCharitableCase(CharitableCase charitableCaseSelected) {
        this.charitableCase = charitableCaseSelected;
    }

    public void serchDonorButtonOnAction(ActionEvent actionEvent) {
        String name = nameFinderTextField.getText();
        List<Donor> donors = donorService.getAllDonorsByName(name);
        donorObservable.setAll(donors);


    }

    public void SelectDonorButtonOnAction(ActionEvent actionEvent) {
        Donor donor = donorsTable.getSelectionModel().getSelectedItem();
        System.out.println(donor);
        firstNameTextField.setText(donor.getFirstName());
        lastNameTextField.setText(donor.getLastName());
        addressTextField.setText(donor.getAddress());
        phoneNrTextField.setText(donor.getPhoneNumber());
    }
}
