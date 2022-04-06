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
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.net.Socket;
import java.util.List;

public class TeledonServicesRpcReflectionWorker implements Runnable, IDonationObserver {
    private IDonationServices server;
    private Socket connection;

    private ObjectInputStream input;
    private ObjectOutputStream output;
    private volatile boolean connected;

    public TeledonServicesRpcReflectionWorker(IDonationServices server, Socket connection) {
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
        System.out.println("sending response "+response);
        output.writeObject(response);
        output.flush();
    }


    private static Response okResponse = new Response.Builder().type(ResponseType.OK).build();
    private Response handleRequest(Request request) {
        Response response = null;
        String handlerName = "handle" + (request).type();
        System.out.println("HandlerName" + handlerName);
        try{
            Method method = this.getClass().getDeclaredMethod(handlerName,Request.class);
            response = (Response) method.invoke(this,request);
        } catch (NoSuchMethodException e) {
            e.printStackTrace();
        } catch (InvocationTargetException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
        return response;
    }

    private Response handleSAVE_DONATION(Request request){
        System.out.println("SaveDonation...");
        Donation donation = (Donation) request.data();
        try{
            server.saveDonation(donation);
            return okResponse;
        } catch (DonationExpection e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }
    private Response handleLOGIN(Request request){
        System.out.println("Login request...");
        Volunteer volunteer = (Volunteer) request.data();
        try{
            server.login(volunteer.getUsername(),volunteer.getPassword(),this);
            return okResponse;
        } catch (DonationExpection e) {
            connected = false;
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }

    private Response handleLOGOUT(Request request){
        System.out.println("Logout request..");
        Volunteer volunteer = (Volunteer) request.data();
        try{
            server.logout(volunteer,this);
            connected = false;
            return okResponse;
        } catch (DonationExpection e) {
            connected = false;
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }
    }
    private Response handleGET_CHARITABLE_CASES(Request request) throws DonationExpection {
        System.out.println("Get charitable cases request....");
        List<CharitableCase> cases = server.getAllCharitableCase();
        return new Response.Builder().type(ResponseType.GET_CHARITABLE_CASES).data(cases).build();
    }

    private Response handleGET_DONORS_BY_NAME(Request request)throws DonationExpection{
        System.out.println("Get donors by name request...");
        String name = (String) request.data();
        List<Donor> donors = server.getAllDonorsByName(name);
        return new Response.Builder().type(ResponseType.GET_DONORS_BY_NAME).data(donors).build();
    }
    private Response handleGET_DONOR_BY_FULL_NAME(Request request) throws DonationExpection {
        System.out.println("get donor by full name request...");
        String fullName = (String) request.data();
        String[] names = fullName.split(" ");
        String firstName = names[0];
        String lastName = names[1];
        Donor donor = server.getDonorByFullName(firstName,lastName);
        return new Response.Builder().type(ResponseType.GET_DONOR_BY_FULL_NAME).data(donor).build();
    }
    private Response handleGET_DONOR_ID(Request request) throws DonationExpection {
        System.out.println("get id donor request...");
        String fullName = (String) request.data();
        String[] names = fullName.split(" ");
        String firstName = names[0];
        String lastName = names[1];
        Integer donorId = server.getDonor(firstName,lastName);
        return new Response.Builder().type(ResponseType.GET_DONOR_BY_FULL_NAME).data(donorId).build();

    }
    private Response handleGET_DONOR(Request request) throws DonationExpection {
        System.out.println("get donor by id request...");
        Integer id = (Integer) request.data();
        Donor donor = server.getDonorById(id);
        return new Response.Builder().type(ResponseType.GET_DONOR).data(donor).build();
    }

    private Response handleGET_CHARITABLE_CASE(Request request) throws DonationExpection {
        System.out.println("get charitable case id request....");
        String name = (String) request.data();
        Integer charitableCaseId = server.getCharitableCaseByName(name);
        return new Response.Builder().type(ResponseType.GET_CHARITABLE_CASE).data(charitableCaseId).build();
    }
    private Response handleSAVE_DONOR(Request request) throws DonationExpection {
        System.out.println("save donor request...");
        String info = (String) request.data();
        String[] info1 = info.split(" ");
        String firstName = info1[0];
        String lastName = info1[1];
        String address = info1[2];
        String phoneNr = info1[3];
        try{
            server.saveDonor(firstName,lastName,address,phoneNr);
            return okResponse;
        }catch (DonationExpection e) {
            return new Response.Builder().type(ResponseType.ERROR).data(e.getMessage()).build();
        }

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
