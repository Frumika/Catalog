package com.example.catalog.core;

import com.example.catalog.register.RegFragment;

public enum FieldType {
    EMAIL(1),
    LOGIN(2),
    PASSWORD(4),
    CONFIRM_PASSWORD(8);

    private final int value;

    FieldType(int value) {
        this.value = value;
    }

    public int getValue() {
        return value;
    }

    public static int combine(FieldType... types) {
        int result = 0;
        for (FieldType type : types) {
            result |= type.value;
        }
        return result;
    }

    public static boolean hasFlag(int flags, FieldType type) {
        return (flags & type.value) == type.value;
    }
}