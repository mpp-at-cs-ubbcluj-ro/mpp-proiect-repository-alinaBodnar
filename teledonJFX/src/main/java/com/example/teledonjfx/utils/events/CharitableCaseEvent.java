package com.example.teledonjfx.utils.events;

import com.example.teledonjfx.model.CharitableCase;

public class CharitableCaseEvent implements Event{
    private ChangeEventType type;
    private CharitableCase data, oldData;

    public CharitableCaseEvent(ChangeEventType type, CharitableCase data) {
        this.type = type;
        this.data = data;
    }

    public CharitableCaseEvent(ChangeEventType type, CharitableCase data, CharitableCase oldData) {
        this.type = type;
        this.data = data;
        this.oldData = oldData;
    }

    public ChangeEventType getType() {
        return type;
    }

    public CharitableCase getData() {
        return data;
    }

    public CharitableCase getOldData() {
        return oldData;
    }
}
