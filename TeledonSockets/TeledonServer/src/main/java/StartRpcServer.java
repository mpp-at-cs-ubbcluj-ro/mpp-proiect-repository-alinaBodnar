import repository.CharitableCaseRepository;
import repository.DonationRepository;
import repository.DonorRepository;
import repository.VolunteerRepository;
import server.TeledonServicesImpl;
import services.IDonationServices;
import utils.AbstractServer;
import utils.ServerExpection;
import utils.TeledonRpcConcurrentServer;

import java.io.IOException;
import java.rmi.ServerException;
import java.util.Properties;

public class StartRpcServer {
    private static int defaultPort=55555;
    public static void main(String[] args) {
        // UserRepository userRepo=new UserRepositoryMock();
        Properties serverProps=new Properties();
        try {
            serverProps.load(StartRpcServer.class.getResourceAsStream("/teledonServer.properties"));
            System.out.println("Server properties set. ");
            serverProps.list(System.out);
        } catch (IOException e) {
            System.err.println("Cannot find chatserver.properties "+e);
            return;
        }

        VolunteerRepository volunteerRepository = new VolunteerRepository(serverProps);
        CharitableCaseRepository charitableCaseRepository = new CharitableCaseRepository(serverProps);
        DonorRepository donorRepository = new DonorRepository(serverProps);
        DonationRepository donationRepository = new DonationRepository(serverProps);
        TeledonServicesImpl donationServer = new TeledonServicesImpl(volunteerRepository,charitableCaseRepository,donorRepository,donationRepository);

        int chatServerPort=defaultPort;
        try {
            chatServerPort = Integer.parseInt(serverProps.getProperty("teledon.server.port"));
        }catch (NumberFormatException nef){
            System.err.println("Wrong  Port Number"+nef.getMessage());
            System.err.println("Using default port "+defaultPort);
        }
        System.out.println("Starting server on port: "+chatServerPort);
        AbstractServer server = new TeledonRpcConcurrentServer(chatServerPort, donationServer);
        try {
            server.start();
        } catch (ServerExpection e) {
            e.printStackTrace();
        } finally {
            try {
                server.stop();
            } catch (ServerExpection e) {
                e.printStackTrace();
            }
        }
    }
}
