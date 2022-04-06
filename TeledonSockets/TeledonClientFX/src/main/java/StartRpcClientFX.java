import gui.CharitableCasesController;
import gui.LoginController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.stage.Stage;
import rpcprotocol.TeledonServicesRpcProxy;
import services.IDonationServices;

import java.io.IOException;
import java.util.Properties;

public class StartRpcClientFX extends Application {

    private static int defaultTeledonPort = 55555;
    private static String defaultServer = "localhost";
    @Override
    public void start(Stage primaryStage) throws Exception {
        System.out.println("In start");
        Properties clientProps = new Properties();
        try{
            clientProps.load(StartRpcClientFX.class.getResourceAsStream("/teledonClient.properties"));
            System.err.println("Client props set.");
            clientProps.list(System.out);
        }catch (IOException e){
            System.out.println("Cannot find teledonClient.properties " + e);
            return;
        }
        String serverIP = clientProps.getProperty("teledon.server.host",defaultServer);
        int serverPort = defaultTeledonPort;
        try{
            serverPort = Integer.parseInt(clientProps.getProperty("teledon.server.port"));
        }catch (NumberFormatException ex){
            System.err.println("Wrong port number " + ex.getMessage());
            System.out.println("Using default port: " + defaultTeledonPort);
        }
        System.out.println("Using server IP " + serverIP);
        System.out.println("Using server port " + serverPort);

        IDonationServices server = new TeledonServicesRpcProxy(serverIP,serverPort);


        FXMLLoader loader = new FXMLLoader(
                getClass().getClassLoader().getResource("login.fxml"));
        Parent root=loader.load();
        LoginController ctrl =
                loader.<LoginController>getController();
        ctrl.setServer(server);


        FXMLLoader cloader = new FXMLLoader(
                getClass().getClassLoader().getResource("charitableCases.fxml"));
        Parent croot=cloader.load();


        CharitableCasesController casesCtrl =
                cloader.<CharitableCasesController>getController();
        casesCtrl.setServer(server);

        ctrl.setCharitableCaseController(casesCtrl);
        ctrl.setParent(croot);

        Scene scene = new Scene(root,520,400);
        primaryStage.setScene(scene);
        primaryStage.show();


    }
}
