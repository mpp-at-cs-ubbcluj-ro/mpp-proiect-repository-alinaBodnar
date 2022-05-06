package gui;

import javafx.application.Platform;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.stage.Stage;
import model.CharitableCase;
import model.Donation;
import model.Donor;
import model.Volunteer;
import services.DonationExpection;
import services.IDonationObserver;
import services.IDonationServices;

import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.ResourceBundle;

public class CharitableCasesController implements IDonationObserver, Initializable {

    private IDonationServices server;
    private Volunteer volunteer;

    public CharitableCasesController() {
    }

    public CharitableCasesController(IDonationServices server) throws DonationExpection {
        this.server = server;
        init();
    }

    public void setServer(IDonationServices s) throws DonationExpection {
        this.server = s;
    }

    @FXML
    private TextField addressTextField;
    @FXML
    private TextField firstNameTextField;

    @FXML
    private Label labelInfo;

    @FXML
    private TextField lastNameTextField;

    @FXML
    private TextField phoneNrTextField;


    @FXML
    private TableColumn<CharitableCase, Integer> amountRaised;

    @FXML
    private TableColumn<CharitableCase, String> charitableCase;

    @FXML
    private TableView<CharitableCase> charitableTable;

    @FXML
    private TextField amountTextField;


    @FXML
    private TextField nameFinderTextField;

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
    private Label messageLabel;

    ObservableList<Donor> donorObservable = FXCollections.observableArrayList();

    ObservableList<CharitableCase> list = FXCollections.observableArrayList();

    private void init() throws DonationExpection {
        Platform.runLater(() ->{
            initialize();
            List<CharitableCase> all  = null;
            try {
                all = server.getAllCharitableCase();
                list.setAll(all);
            } catch (DonationExpection e) {
                e.printStackTrace();
            }

        });


    }
    @FXML
    private void initialize() {
        charitableCase.setCellValueFactory(new PropertyValueFactory<CharitableCase,String>("name"));
        amountRaised.setCellValueFactory(new PropertyValueFactory<CharitableCase, Integer>("amountRaised"));
        charitableTable.setItems(list);

    }



    public void logOutButtonOnAction(ActionEvent actionEvent) throws IOException {
        logout();
        ((Node)(actionEvent.getSource())).getScene().getWindow().hide();

    }

    @Override
    public void donationSaved(Donation donation) throws DonationExpection {
        Platform.runLater(() ->{
            initialize();
            List<CharitableCase> all  = null;
            try {
                all = server.getAllCharitableCase();
                list.setAll(all);
            } catch (DonationExpection e) {
                e.printStackTrace();
            }

        });
    }

    public void setVolunteer(Volunteer currentVolunteer) {
        this.volunteer = currentVolunteer;
    }

    public void setCases() {
        try{
            List<CharitableCase> cases = server.getAllCharitableCase();
            list.setAll(cases);
            init();
        } catch (DonationExpection e) {
            e.printStackTrace();
        }
    }

    public void SaveDonationButtonOnAction(ActionEvent actionEvent) throws DonationExpection {
        CharitableCase charitableCaseSelected = charitableTable.getSelectionModel().getSelectedItem();
        if(charitableCaseSelected != null){
            Integer charitableCaseId = server.getCharitableCaseByName(charitableCaseSelected.getName());
            charitableCaseSelected.setId(charitableCaseId);
            labelInfo.setText("");
            String firstName = firstNameTextField.getText();
            String lastName = lastNameTextField.getText();
            String address = addressTextField.getText();
            String phoneNr = phoneNrTextField.getText();
            Integer amountDonated = Integer.parseInt(amountTextField.getText());

            Donor found = server.getDonorByFullName(firstName,lastName);
            if(found == null){
                server.saveDonor(firstName,lastName,address,phoneNr);
                Integer idDonor = server.getDonor(firstName,lastName);
                Donor donor = server.getDonorById(idDonor);

                Donation donation = new Donation(donor,charitableCaseSelected,amountDonated);
                server.saveDonation(donation);

                firstNameTextField.clear();
                lastNameTextField.clear();
                addressTextField.clear();
                phoneNrTextField.clear();
                amountTextField.clear();

                messageLabel.setText("Successfully added donation");
                donationSaved(donation);
            }
            else{

                Donation donation = new Donation(found,charitableCaseSelected,amountDonated);
                server.saveDonation(donation);

                firstNameTextField.clear();
                lastNameTextField.clear();
                addressTextField.clear();
                phoneNrTextField.clear();
                amountTextField.clear();

                messageLabel.setText("Successfully added donation");
                donationSaved(donation);
            }
        }
        else{
            labelInfo.setText("You must to select a charitable case!");
            }


    }

    public void serchDonorButtonOnAction(ActionEvent actionEvent) throws DonationExpection {
        firstName.setCellValueFactory(new PropertyValueFactory<Donor,String>("firstName"));
        lastName.setCellValueFactory(new PropertyValueFactory<Donor,String>("lastName"));
        address.setCellValueFactory(new PropertyValueFactory<Donor,String>("address"));
        phoneNumber.setCellValueFactory(new PropertyValueFactory<Donor,String>("phoneNumber"));
        donorsTable.setItems(donorObservable);
        String name = nameFinderTextField.getText();
        List<Donor> donors = server.getAllDonorsByName(name);
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

    public void logout() {
        try{
            server.logout(volunteer,this);

        } catch (DonationExpection e) {
            System.out.println("Logout error " + e);
        }
    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {
    }

}
