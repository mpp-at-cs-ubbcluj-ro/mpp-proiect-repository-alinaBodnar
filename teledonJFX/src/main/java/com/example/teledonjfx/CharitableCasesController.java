package com.example.teledonjfx;

import com.example.teledonjfx.model.CharitableCase;
import com.example.teledonjfx.model.Donor;
import com.example.teledonjfx.repository.DonorRepository;
import com.example.teledonjfx.service.CharitableCasesService;
import com.example.teledonjfx.service.DonationService;
import com.example.teledonjfx.service.DonorService;
import com.example.teledonjfx.utils.Observer;
import com.example.teledonjfx.utils.events.CharitableCaseEvent;
import com.example.teledonjfx.utils.events.DonationEvent;
import javafx.beans.Observable;
import javafx.beans.property.IntegerProperty;
import javafx.beans.property.StringProperty;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.fxml.Initializable;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;

import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;
import java.util.List;
import java.util.ResourceBundle;

public class CharitableCasesController implements Observer<DonationEvent> {
    private CharitableCasesService charitableCasesService;
    private DonationService donationService;
    private DonorService donorService;

    public CharitableCasesController() {
    }

    public void setCharitableService(CharitableCasesService charitableCasesService1,DonationService donationService,DonorService donorService){
        this.charitableCasesService = charitableCasesService1;
        this.donationService = donationService;
        this.donorService = donorService;
        donationService.addObserver(this);
        init();

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
    private Button logOutButton;

    ObservableList<CharitableCase> list = FXCollections.observableArrayList();

    private void init(){
        Iterable<CharitableCase> cases = charitableCasesService.getAll();
        List<CharitableCase> all = new ArrayList<>();
        cases.forEach(all::add);
        list.setAll(all);
    }
    @FXML
    private void initialize() {
        charitableCase.setCellValueFactory(new PropertyValueFactory<CharitableCase,String>("name"));
        amountRaised.setCellValueFactory(new PropertyValueFactory<CharitableCase, Integer>("amountRaised"));
        charitableTable.setItems(list);

    }

    public void AddDonationButtonOnAction(ActionEvent actionEvent) {
        try{
            FXMLLoader fxmlLoader = new FXMLLoader(getClass().getResource("add-donation-view.fxml"));
            Parent root = (Parent) fxmlLoader.load();
            Stage stage = new Stage();
            Scene scene = new Scene(root,869,426);

            AddDonationController addDonationController = fxmlLoader.getController();
            addDonationController.setServices(charitableCasesService,donorService,donationService);
            stage.setScene(scene);
            CharitableCase charitableCaseSelected = charitableTable.getSelectionModel().getSelectedItem();
            if(charitableCaseSelected != null){
                addDonationController.setServices(charitableCasesService,donorService,donationService);
                Integer charitableCaseId = charitableCasesService.getCharitableCaseByName(charitableCaseSelected.getName());
                charitableCaseSelected.setId(charitableCaseId);
                addDonationController.setCharitableCase(charitableCaseSelected);
                labelInfo.setText("");

                stage.show();
            }
            else{
                labelInfo.setText("You must to select a charitable case!");
            }

        } catch (IOException e) {
            e.printStackTrace();
        }

    }




    private List<CharitableCase> getAllCases(){
        Iterable<CharitableCase> cases = charitableCasesService.getAll();
        List<CharitableCase> charitableCaseList = new ArrayList<>();
        cases.forEach(x->charitableCaseList.add(x));
        return charitableCaseList;
    }


    @Override
    public void update(DonationEvent donationEvent) {
        list.setAll(getAllCases());
        init();
    }

    public void logOutButtonOnAction(ActionEvent actionEvent) throws IOException {
        Stage actualStage = (Stage) logOutButton.getScene().getWindow();
        actualStage.close();
    }
}
