package com.example.teledonjfx.repository;

import com.example.teledonjfx.model.Donor;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class DonorRepository implements IDonorRepository {
    private JdbcUtils dbUtils;

    private static final Logger logger = LogManager.getLogger();
    public DonorRepository(Properties properties){
        logger.info("Initializing DonorRepository with properties: {}",properties);
        dbUtils = new JdbcUtils(properties);
    }

    @Override
    public void save(Donor elem) {
        logger.traceEntry("saving donor {}", elem);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("insert into Donors(firstName,lastName,address,phoneNumber) values (?,?,?,?)")){
            preparedStatement.setString(1,elem.getFirstName());
            preparedStatement.setString(2,elem.getLastName());
            preparedStatement.setString(3, elem.getAddress());
            preparedStatement.setString(4, elem.getPhoneNumber());
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
        logger.traceEntry("deleting donor {}", id);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("delete from Donors where id = ?")){
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
    public void update(Donor elem, Integer id) {
        logger.traceEntry("updating donor {}", id);
        Connection connection = dbUtils.getConnection();
        String sql = String.format("update Donors set firstName = ?, lastName = ?, address = ?, phoneNumber = ? where id = %d",id);
        try(PreparedStatement preparedStatement = connection.prepareStatement(sql)) {
            preparedStatement.setString(1,elem.getFirstName());
            preparedStatement.setString(2,elem.getLastName());
            preparedStatement.setString(3, elem.getAddress());
            preparedStatement.setString(4, elem.getPhoneNumber());
            int result = preparedStatement.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit();

    }

    @Override
    public Donor findById(Integer id) {
        logger.traceEntry("finding donor with id {}", id);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from Donors where id = ?")){
            preparedStatement.setInt(1,id);
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                if(resultSet.next()){
                    int idDonor = resultSet.getInt("id");
                    String firstNameDonor = resultSet.getString("firstName");
                    String lastNameDonor = resultSet.getString("lastName");
                    String addressDonor = resultSet.getString("address");
                    String phoneNumberDonor = resultSet.getString("phoneNumber");
                    Donor donor = new Donor(firstNameDonor,lastNameDonor,addressDonor,phoneNumberDonor);
                    donor.setId(idDonor);
                    logger.traceExit(donor);
                    return donor;
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("No donor found with id {}", id);
        return null;
    }

    @Override
    public Iterable<Donor> findAll() {

        logger.traceEntry("finding all donors {}");
        Connection connection = dbUtils.getConnection();
        List<Donor> donorList = new ArrayList<>();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from Donors")) {
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                while(resultSet.next()){
                    int idDonor = resultSet.getInt("id");
                    String firstNameDonor = resultSet.getString("firstName");
                    String lastNameDonor = resultSet.getString("lastName");
                    String addressDonor = resultSet.getString("address");
                    String phoneNumberDonor = resultSet.getString("phoneNumber");
                    Donor donor = new Donor(firstNameDonor,lastNameDonor,addressDonor,phoneNumberDonor);
                    donor.setId(idDonor);
                    donorList.add(donor);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit(donorList);
        return donorList;
    }

    @Override
    public int size() {

        logger.traceEntry();
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select count(*) as [SIZE] from Donors")){
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
    public Integer getDonorByName(String firstName, String lastName) {
        logger.traceEntry("finding donor with id {}");
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from Donors where firstName = ? and lastName = ?")){
            preparedStatement.setString(1,firstName);
            preparedStatement.setString(2,lastName);
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                if(resultSet.next()){
                    int idDonor = resultSet.getInt("id");
                    return idDonor;
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("No donor found with id {}");
        return null;
    }

    @Override
    public Donor getDonorByFullName(String first, String last) {
        logger.traceEntry("finding donor by name{}");
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from Donors where firstName = ? and lastName = ?")){
            preparedStatement.setString(1,first);
            preparedStatement.setString(2,last);
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                if(resultSet.next()){
                    int idDonor = resultSet.getInt("id");
                    String firstNameDonor = resultSet.getString("firstName");
                    String lastNameDonor = resultSet.getString("lastName");
                    String addressDonor = resultSet.getString("address");
                    String phoneNumberDonor = resultSet.getString("phoneNumber");
                    Donor donor = new Donor(firstNameDonor,lastNameDonor,addressDonor,phoneNumberDonor);
                    donor.setId(idDonor);
                    logger.traceExit(donor);
                    return donor;
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("No donor found{}");
        return null;
    }
}
