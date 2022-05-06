package rpcprotocol;

import model.CharitableCase;
import model.Donation;
import model.Donor;
import model.Volunteer;
import services.DonationExpection;
import services.IDonationObserver;
import services.IDonationServices;

import java.io.IOException;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

public class TeledonServicesRpcProxy implements IDonationServices {

    private String host;
    private int port;

    private IDonationObserver client;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private Socket connection;

    private BlockingQueue<Response> qresponses;
    private volatile boolean finished;

    public TeledonServicesRpcProxy(String host, int port) {
        this.host = host;
        this.port = port;
        qresponses = new LinkedBlockingQueue<Response>();
    }

    private void sendRequest(Request req) throws DonationExpection{
        try{
            output.writeObject(req);
            output.flush();
        } catch (IOException e) {
            throw new DonationExpection("Error sending object " + e);
        }
    }

    @Override
    public void login(String username, String password, IDonationObserver client) throws DonationExpection{
        initializeConnection();
        Volunteer volunteer = new Volunteer(username,password);
        Request req = new Request.Builder().type(RequestType.LOGIN).data(volunteer).build();
        sendRequest(req);
        Response response = readResponse();
        if(response.type() == ResponseType.OK){
            this.client = client;
            return;
        }
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            closeConnection();
            throw new DonationExpection();
        }


    }

    @Override
    public void logout(Volunteer volunteer, IDonationObserver client)throws DonationExpection {
        Request req = new Request.Builder().type(RequestType.LOGOUT).data(volunteer).build();
        sendRequest(req);
        Response response = readResponse();
        closeConnection();
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new DonationExpection(err);
        }

    }

    @Override
    public List<CharitableCase> getAllCharitableCase()throws DonationExpection {
        Request req = new Request.Builder().type(RequestType.GET_CHARITABLE_CASES).build();
        sendRequest(req);
        Response response = readResponse();
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new DonationExpection();
        }
        List<CharitableCase> cases = (List<CharitableCase>) response.data();
        return cases;

    }

    @Override
    public void saveDonation(Donation donation) throws DonationExpection {
        Request req = new Request.Builder().type(RequestType.SAVE_DONATION).data(donation).build();
        sendRequest(req);
        Response response = readResponse();
//        closeConnection();
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new DonationExpection(err);
        }
    }

    @Override
    public void saveDonor(String firstName, String lastName, String address, String phoneNr) throws DonationExpection {
        String info = firstName + " " +  lastName + " " + address + " " + phoneNr;
        Request req = new Request.Builder().type(RequestType.SAVE_DONOR).data(info).build();
        sendRequest(req);
        Response response = readResponse();
//        closeConnection();
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new DonationExpection(err);
        }
    }

    @Override
    public Integer getDonor(String firstName, String lastName) throws DonationExpection {
        String fullName = firstName + " " +  lastName;
        Request req = new Request.Builder().type(RequestType.GET_DONOR_ID).data(fullName).build();
        sendRequest(req);
        Response response = readResponse();
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new DonationExpection(err);
        }
        Integer id = (Integer) response.data();
        return id;
    }

    @Override
    public Donor getDonorById(Integer id) throws DonationExpection {
        Request req = new Request.Builder().type(RequestType.GET_DONOR).data(id).build();
        sendRequest(req);
        Response response = readResponse();
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new DonationExpection(err);
        }
        Donor donor = (Donor) response.data();
        return donor;
    }

    @Override
    public List<Donor> getAll() {
        return null;
    }

    @Override
    public List<Donor> getAllDonorsByName(String name) throws DonationExpection {
        Request req = new Request.Builder().type(RequestType.GET_DONORS_BY_NAME).data(name).build();
        sendRequest(req);
        Response response = readResponse();
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new DonationExpection(err);
        }
        List<Donor> donors = (List<Donor>) response.data();
        return donors;
    }

    @Override
    public Donor getDonorByFullName(String firstName, String lastName) throws DonationExpection {
        String fullName = firstName + " " +  lastName;
        Request req = new Request.Builder().type(RequestType.GET_DONOR_BY_FULL_NAME).data(fullName).build();
        sendRequest(req);
        Response response = readResponse();
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new DonationExpection(err);
        }
        Donor donor = (Donor) response.data();
        return donor;
    }

    @Override
    public Integer getCharitableCaseByName(String name) throws DonationExpection {
        Request req = new Request.Builder().type(RequestType.GET_CHARITABLE_CASE).data(name).build();
        sendRequest(req);
        Response response = readResponse();
        if(response.type() == ResponseType.ERROR){
            String err = response.data().toString();
            throw new DonationExpection(err);
        }
        Integer id = (Integer) response.data();
        return id;
    }


    private void closeConnection() {
        finished = true;
        try{
            input.close();
            output.close();
            connection.close();
            client = null;
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private Response readResponse() {
        Response response = null;
        try{
            response  = qresponses.take();
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
        return response;
    }

    private void initializeConnection()throws DonationExpection{
        try{
            connection = new Socket(host,port);
            output = new ObjectOutputStream(connection.getOutputStream());
            output.flush();
            input = new ObjectInputStream(connection.getInputStream());
            finished = false;
            startReader();
        } catch (UnknownHostException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }

    private void startReader() {
        Thread tw = new Thread(new ReaderThread());
        tw.start();
    }

    private void handleUpdate(Response response){
        if(response.type() == ResponseType.NEW_DONATION){
            Donation donation = (Donation) response.data();
            try{
                client.donationSaved(donation);
            } catch (DonationExpection e) {
                e.printStackTrace();
            }
        }
    }

    private boolean isUpdate(Response response){
        return response.type() == ResponseType.NEW_DONATION;
    }

    private class ReaderThread implements Runnable {

        @Override
        public void run() {
            while (!finished){
                try{
                    Object response = input.readObject();
                    System.out.println("response received " + response);
                    if(isUpdate((Response) response)){
                        handleUpdate((Response) response);
                    }
                    else{
                        try{
                            qresponses.put((Response) response);
                        } catch (InterruptedException e) {
                            e.printStackTrace();
                        }
                    }
                } catch (IOException e) {
                    System.out.println("Reading error" + e);
                } catch (ClassNotFoundException e) {
                    System.out.println("Reading error" + e);
                }
            }
        }
    }
}
