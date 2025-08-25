package com.example.catalog.main;

public enum ProductType {
    ALL("Все"),
    MOBILE("Телефоны"),
    LAPTOP("Ноутбуки"),
    COMPUTER_EQUIPMENTS("Компьютерная техника"),
    APPLIANCES_EQUIPMENTS("Бытовая техника"),
    PHOTO_AND_VIDEO_EQUIPMENTS("Фото и видеотехника");

    private final String displayName;

    ProductType(String displayName) {
        this.displayName = displayName;
    }

    public String getDisplayName() {
        return displayName;
    }

    public static String[] getProductTypes() {
        ProductType[] values = ProductType.values();
        String[] result = new String[values.length];
        for (int i = 0; i < values.length; i++) {
            result[i] = values[i].getDisplayName();
        }
        return result;
    }

    public static ProductType fromDisplayName(String name) {
        for (ProductType type : values()) {
            if (type.getDisplayName().equals(name)) {
                return type;
            }
        }
        throw new IllegalArgumentException("Unknown category: " + name);
    }
}
