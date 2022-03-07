package model;

import java.io.Serializable;

public class CharitableCase implements Identifiable<Integer>, Serializable {
    private int id;
    private String name;
    private double amountRaised;

    public CharitableCase(int id, String name, double amountRaised) {
        this.id = id;
        this.name = name;
        this.amountRaised = amountRaised;
    }


    @Override
    public Integer getId() {
        return id;
    }

    @Override
    public void setId(Integer id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public double getAmountRaised() {
        return amountRaised;
    }

    public void setAmountRaised(double amountRaised) {
        this.amountRaised = amountRaised;
    }

    @Override
    public String toString() {
        return "CharitableCase{" +
                "id=" + id +
                ", name='" + name + '\'' +
                ", amountRaised=" + amountRaised +
                '}';
    }
}
