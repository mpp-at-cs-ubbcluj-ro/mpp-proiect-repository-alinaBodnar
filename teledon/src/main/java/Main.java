import model.Donor;

public class Main {
    public static void main(String[] args) {
        System.out.println("Hello!");
        Donor donor = new Donor(1,"Vasile","Popescu","Cluj 190J","071234455");
        System.out.println(donor.toString());
    }
}
