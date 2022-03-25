package com.example.teledonjfx.utils;

import com.example.teledonjfx.utils.events.Event;

public interface Observer<E extends Event> {
    void update(E e);
}
