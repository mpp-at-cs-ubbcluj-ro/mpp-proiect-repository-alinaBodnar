package com.example.teledonjfx.utils.events;

import com.example.teledonjfx.model.CharitableCase;

public class DonationEvent implements Event{
    private ChangeEventType type;
    private CharitableCase newCharitableCase, oldCharitableCase;

    public DonationEvent(ChangeEventType type, CharitableCase newCharitableCase) {
        this.type = type;
        this.newCharitableCase = newCharitableCase;
    }

    public DonationEvent(ChangeEventType type, CharitableCase newCharitableCase, CharitableCase oldCharitableCase) {
        this.type = type;
        this.newCharitableCase = newCharitableCase;
        this.oldCharitableCase = oldCharitableCase;
    }

    public ChangeEventType getType() {
        return type;
    }

    public CharitableCase getNewCharitableCase() {
        return newCharitableCase;
    }

    public CharitableCase getOldCharitableCase() {
        return oldCharitableCase;
    }
}
