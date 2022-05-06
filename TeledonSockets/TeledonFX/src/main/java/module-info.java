module teledonsockets.teledonfx {
    requires javafx.controls;
    requires javafx.fxml;


    opens teledonsockets.teledonfx to javafx.fxml;
    exports teledonsockets.teledonfx;
}