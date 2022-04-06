package utils;

import rpcprotocol.TeledonServicesRpcReflectionWorker;
import services.IDonationServices;

import java.net.Socket;

public class TeledonRpcConcurrentServer extends AbsConcurrentServer{
    private IDonationServices donationServer;

    public TeledonRpcConcurrentServer(int port,IDonationServices donationServer) {
        super(port);
        this.donationServer = donationServer;
        System.out.println("Teledon - TeledonRpcConcurrentServer");
    }

    @Override
    protected Thread createWorker(Socket client) {
        TeledonServicesRpcReflectionWorker worker = new TeledonServicesRpcReflectionWorker(donationServer,client);

        Thread tw = new Thread(worker);
        return tw;
    }

    @Override
    public void stop(){
        System.out.println("Stopping services...");
    }
}
