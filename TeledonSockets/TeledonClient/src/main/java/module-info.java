module teledonsockets.teledonclient {
    requires javafx.controls;
    requires javafx.fxml;


    opens teledonsockets.teledonclient to javafx.fxml;
    exports teledonsockets.teledonclient;
}