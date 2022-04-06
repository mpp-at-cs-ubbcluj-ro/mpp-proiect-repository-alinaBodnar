package utils;

public class ServerExpection extends Exception{
    public ServerExpection(){super();}
    public ServerExpection(String message){
        super(message);
    }
    public ServerExpection(String message,Throwable cause){
        super(message,cause);
    }
}
