package rpcprotocol;

import model.CharitableCase;
import model.Donation;
import model.Volunteer;
import services.DonationExpection;
import services.IDonationObserver;
import services.IDonationServices;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.sql.SQLOutput;
import java.util.List;

public class TeledonClientRpcWorker implements Runnable, IDonationObserver {

    private IDonationServices server;
    private Socket connection;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;

    public TeledonClientRpcWorker(IDonationServices server, Socket connection) {
        this.server = server;
        this.connection = connection;
        try{
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();

            input = new ObjectInputStream(connection.getInputStream());
            connected = true;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    @Override
    public void run() {
        while(connected){
            try{
                Object request = input.readObject();
                Response response = handleRequest((Request)request);
                if(response != null){
                    sendResponse(response);
                }
            } catch (IOException e) {
                e.printStackTrace();
            } catch (ClassNotFoundException e) {
                e.printStackTrace();
            }

            try{
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
        try{
            input.close();
            output.close();
            connection.close();
        } catch (IOException e) {
            System.out.println("Error" + e);
        }
    }

    private void sendResponse(Response response) throws IOException{
        System.out.println("sending response " + response);
        output.writeObject(response);
        output.flush();
    }

    private static Response okResponse = new Response.Builder().type(ResponseType.OK).build();
    private Response handleRequest(Request request) {
        Response response = null;
        if(request.type() == RequestType.LOGIN){
            System.out.println("Login request... " + request.type());
            Volunteer volunteer = (Volunteer) request.data();
            try{
                server.login(volunteer.getUsername(),volunteer.getPassword(),this);
                return okResponse;

            } catch (DonationExpection e) {
                connected = false;
                return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
            }
        }
        if(request.type() == RequestType.LOGOUT){
            System.out.println("Logout request..");
            Volunteer volunteer = (Volunteer) request.data();
            try{
                server.logout(volunteer,this);
                return okResponse;
            } catch (DonationExpection e) {
                return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
            }
        }
        if (request.type() == RequestType.SAVE_DONATION) {
            System.out.println("Save donation....");
            Donation donation = (Donation) request.data();
            try {
                server.saveDonation(donation);
                return okResponse;
            } catch (DonationExpection e) {
                return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
            }
        }
        if(request.type() == RequestType.GET_CHARITABLE_CASES){
            System.out.println("GetCharitableCases Request...");
            try{
                List<CharitableCase> cases = server.getAllCharitableCase();
                return new Response.Builder().type(ResponseType.GET_CHARITABLE_CASES).data(cases).build();
            } catch (DonationExpection e) {
                return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
            }
        }
        return response;
    }

    @Override
    public void donationSaved(Donation donation) throws DonationExpection {
        Response resp = new Response.Builder().type(ResponseType.NEW_DONATION).data(donation).build();
        System.out.println("Donation saved " + donation);
        try{
            sendResponse(resp);
        }catch (IOException e){
            throw new DonationExpection("Sending error: " + e);
        }
    }
}
