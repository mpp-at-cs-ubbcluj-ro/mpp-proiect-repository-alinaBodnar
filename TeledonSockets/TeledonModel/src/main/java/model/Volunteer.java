package model;


import java.io.Serializable;

public class Volunteer implements Identifiable<Integer>, Serializable {
    private int id;
    private String username;
    private String password;

    public Volunteer( String username, String password) {
        this.username = username;
        this.password = password;
    }

    @Override
    public Integer getId() {
        return id;
    }

    @Override
    public void setId(Integer id) {
        this.id = id;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    @Override
    public String toString() {
        return "Volunteer{" +
                "id=" + id +
                ", firstName='" + username + '\'' +
                ", lastName='" + password + '\'' +
                '}';
    }
}
