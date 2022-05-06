package gui;

import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Node;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.BorderPane;
import javafx.stage.Stage;
import javafx.stage.WindowEvent;
import model.Volunteer;
import services.DonationExpection;
import services.IDonationServices;

import java.io.IOException;
import java.net.URL;
import java.util.ResourceBundle;

public class LoginController implements Initializable {


    private IDonationServices server;
    private CharitableCasesController charitableCasesController;
    private Volunteer currentVolunteer;
    private Parent mainParent;

    public void setServer(IDonationServices server){
        this.server = server;
    }

    @FXML
    private Label loginMessageLabel;

    @FXML
    private PasswordField passwordTextField;

    @FXML
    private TextField volunteerTextField;

    public LoginController(){
    }
    @FXML
    void loginButtonOnAction(ActionEvent event) {
        if(volunteerTextField.getText().isBlank() == false && passwordTextField.getText().isBlank() == false){
            validateLogin(event);

        }
        else{
            loginMessageLabel.setText("Please enter username and password");
        }

    }
    private void validateLogin(ActionEvent actionEvent) {
        String username = volunteerTextField.getText();
        String password = passwordTextField.getText();
        Volunteer volunteer = new Volunteer(username,password);
        try{
            server.login(volunteer.getUsername(),volunteer.getPassword(),charitableCases
                    Controller);
            loadCharitableCases(actionEvent);

        } catch (DonationExpection e) {
            System.out.println(e.getMessage());
        }
    }
    private void loadCharitableCases(ActionEvent actionEvent) throws DonationExpection {
        Stage stage = new Stage();
        stage.setScene(new Scene(mainParent));
        stage.setOnCloseRequest(new EventHandler<WindowEvent>() {
            @Override
            public void handle(WindowEvent event) {
                charitableCasesController.logout();
                System.exit(0);
            }
        });
        String username = volunteerTextField.getText();
        String password = passwordTextField.getText();
        Volunteer volunteer = new Volunteer(username,password);
        charitableCasesController.setVolunteer(volunteer);
        charitableCasesController.setServer(server);
        charitableCasesController.setCases();
        stage.show();
        ((Node)(actionEvent.getSource())).getScene().getWindow().hide();

    }

    @FXML
    void signUpButtonOnAction(ActionEvent event) {

    }

    @Override
    public void initialize(URL location, ResourceBundle resources) {

    }

    public void setCharitableCaseController(CharitableCasesController casesCtrl) {
        this.charitableCasesController = casesCtrl;
    }

    public void setParent(Parent croot) {
        this.mainParent = croot;
    }
}
