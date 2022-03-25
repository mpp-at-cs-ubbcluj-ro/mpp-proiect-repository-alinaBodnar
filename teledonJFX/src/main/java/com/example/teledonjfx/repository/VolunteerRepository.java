package com.example.teledonjfx.repository;

import com.example.teledonjfx.model.Volunteer;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class VolunteerRepository implements IVolunteerRepository{
    private JdbcUtils dbUtils;

    private static final Logger logger = LogManager.getLogger();
    public VolunteerRepository(Properties properties){
        logger.info("Initializing VolunteerRepository with properties: {}",properties);
        dbUtils = new JdbcUtils(properties);
    }

    @Override
    public void save(Volunteer elem) {
        logger.traceEntry("saving volunteer {}", elem);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("insert into Volunteers(username,password) values (?,?)")){
            preparedStatement.setString(1,elem.getUsername());
            preparedStatement.setString(2,elem.getPassword());
            int result = preparedStatement.executeUpdate();
            logger.traceExit("Saved {} instances",result);
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit();

    }

    @Override
    public void delete(Integer id) {
        logger.traceEntry("deleting volunteer {}", id);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("delete from Volunteers where id = ?")){
            preparedStatement.setInt(1,id);
            int result = preparedStatement.executeUpdate();
            logger.traceExit("Deleted {} instances",result);
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit();
    }

    @Override
    public void update(Volunteer elem, Integer id) {
        logger.traceEntry("updating volunteer {}", id);
        Connection connection = dbUtils.getConnection();
        String sql = String.format("update Volunteers set username = ?, password = ? where id = %d",id);
        try(PreparedStatement preparedStatement = connection.prepareStatement(sql)) {
            preparedStatement.setString(1,elem.getUsername());
            preparedStatement.setString(2,elem.getPassword());
            int result = preparedStatement.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit();
    }

    @Override
    public Volunteer findById(Integer id) {

        logger.traceEntry("finding volunteer with id {}", id);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from Volunteers where id = ?")){
            preparedStatement.setInt(0,id);
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                if(resultSet.next()){
                    int idVolunteer = resultSet.getInt("id");
                    String usernameVolunteer = resultSet.getString("username");
                    String passwordVolunteer = resultSet.getString("password");
                    Volunteer volunteer = new Volunteer(usernameVolunteer,passwordVolunteer);
                    volunteer.setId(idVolunteer);
                    logger.traceExit(volunteer);
                    return volunteer;
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("No volunteer found with id {}", id);
        return null;
    }

    @Override
    public Iterable<Volunteer> findAll() {

        logger.traceEntry("finding all volunteers {}");
        Connection connection = dbUtils.getConnection();
        List<Volunteer> volunteers = new ArrayList<>();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from Volunteers")) {
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                while(resultSet.next()){
                    int idVolunteer = resultSet.getInt("id");
                    String usernameVolunteer = resultSet.getString("username");
                    String passwordVolunteer = resultSet.getString("password");
                    Volunteer volunteer = new Volunteer(usernameVolunteer,passwordVolunteer);
                    volunteer.setId(idVolunteer);
                    volunteers.add(volunteer);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit(volunteers);
        return volunteers;
    }

    @Override
    public int size() {


        logger.traceEntry();
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select count(*) as [SIZE] from Volunteers")){
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                if(resultSet.next()){
                    logger.traceExit(resultSet.getInt("SIZE"));
                    return resultSet.getInt("SIZE");
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        return 0;
    }

    @Override
    public Volunteer getByUsernameAndPassword(String user, String pass) {
        logger.traceEntry("finding volunteer by username and password");
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from Volunteers where username = ? and password = ?")){
            preparedStatement.setString(1, user);
            preparedStatement.setString(2, pass);
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                if(resultSet.next()){
                    int idVolunteer = resultSet.getInt("id");
                    String usernameVolunteer = resultSet.getString("username");
                    String passwordVolunteer = resultSet.getString("password");
                    Volunteer volunteer = new Volunteer(usernameVolunteer,passwordVolunteer);
                    volunteer.setId(idVolunteer);
                    logger.traceExit(volunteer);
                    return volunteer;
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("No volunteer found");
        return null;
    }
}
