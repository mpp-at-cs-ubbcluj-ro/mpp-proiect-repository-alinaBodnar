package model;

import java.io.Serializable;

public class CharitableCase implements Identifiable<Integer>, Serializable {
    private int id;
    private String name;
    private int amountRaised;

    public CharitableCase(String name, int amountRaised) {
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

    public int getAmountRaised() {
        return amountRaised;
    }

    public void setAmountRaised(int amountRaised) {
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
