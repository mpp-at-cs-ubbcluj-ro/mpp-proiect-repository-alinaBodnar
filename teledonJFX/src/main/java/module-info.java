module com.example.teledonjfx {
    requires javafx.controls;
    requires javafx.fxml;
    requires org.apache.logging.log4j;
    requires java.sql;


    opens com.example.teledonjfx to javafx.fxml;
    exports com.example.teledonjfx;
    opens com.example.teledonjfx.model to javafx.fxml;
    exports com.example.teledonjfx.model;
}