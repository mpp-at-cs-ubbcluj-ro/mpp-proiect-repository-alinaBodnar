package com.example.teledonjfx;

import com.example.teledonjfx.model.Volunteer;
import com.example.teledonjfx.service.CharitableCasesService;
import com.example.teledonjfx.service.DonationService;
import com.example.teledonjfx.service.DonorService;
import com.example.teledonjfx.service.VolunteerService;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.BorderPane;
import javafx.stage.Stage;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

public class LoginController implements Initializable {

    private VolunteerService volunteerService;
    private CharitableCasesService charitableCasesService;
    private DonationService donationService;
    private DonorService donorService;

    @FXML
    private AnchorPane anchorPane;

    @FXML
    private BorderPane borderPane;

    @FXML
    private Button loginButton;

    @FXML
    private Label loginMessageLabel;

    @FXML
    private PasswordField passwordTextField;

    @FXML
    private Button signUpButton;

    @FXML
    private TextField volunteerTextField;

    public LoginController(){
    }
    public void setVolunteerService(VolunteerService volunteerService){
        this.volunteerService = volunteerService;
    }
    public void setCharitableCasesService(CharitableCasesService charitableCasesService){
        this.charitableCasesService = charitableCasesService;
    }

    public void setDonationService(DonationService donationService){
        this.donationService = donationService;
    }

    public void setDonorService(DonorService donorService){
        this.donorService = donorService;
    }

    @FXML
    void loginButtonOnAction(ActionEvent event) {
        if(volunteerTextField.getText().isBlank() == false && passwordTextField.getText().isBlank() == false){
            validateLogin();
        }
        else{
            loginMessageLabel.setText("Please enter username or password");
        }

    }
    private void validateLogin() {
        String username = volunteerTextField.getText();
        String password = passwordTextField.getText();
        Volunteer found = volunteerService.getVolunteer(username,password);
        if(found != null){
            loadCharitableCases();
        }
        else{
            loginMessageLabel.setText("Invalid username or password! Try again!");
        }
    }
    private void loadCharitableCases() {
        try{

            FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("charitableCases.fxml"));
            Parent root = (Parent) fxmlLoader.load();
            Stage stage = new Stage();
            Scene scene = new Scene(root,584,310);

            CharitableCasesController charitableCasesController = fxmlLoader.getController();
            charitableCasesController.setCharitableService(charitableCasesService,donationService,donorService);
            stage.setScene(scene);
            stage.show();

//            Stage firstStage = (Stage) loginButton.getScene().getWindow();
//            firstStage.close();

        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @FXML
    void signUpButtonOnAction(ActionEvent event) {

    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {

    }
}
