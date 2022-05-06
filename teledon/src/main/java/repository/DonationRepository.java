package repository;

import model.CharitableCase;
import model.Donation;
import model.Donor;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class DonationRepository implements IDonationRepository<Donation,Integer>{
    private JdbcUtils dbUtils;

    private static final Logger logger = LogManager.getLogger();
    public DonationRepository(Properties properties){
        logger.info("Initializing DonationRepository with properties: {}",properties);
        dbUtils = new JdbcUtils(properties);
    }

    @Override
    public void save(Donation elem) {
        logger.traceEntry("saving donation {}", elem);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("insert into Donations(donorID,charitableCaseID,amountDonated) values (?,?,?)")){
            preparedStatement.setInt(1,elem.getDonor().getId());
            preparedStatement.setInt(2,elem.getCharitableCase().getId());
            preparedStatement.setInt(3,elem.getAmountDonated());
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
    }

    @Override
    public void update(Donation elem, Integer id) {
        logger.traceEntry("updating donation {}", id);
        Connection connection = dbUtils.getConnection();
        String sql = String.format("update Donations set amountDonated = ? where donorID = %d and charitableCaseID = %d",elem.getDonor().getId(),elem.getCharitableCase().getId());
        try(PreparedStatement preparedStatement = connection.prepareStatement(sql)) {
            preparedStatement.setInt(3,elem.getAmountDonated());
            int result = preparedStatement.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit();
    }

    @Override
    public Donation findById(Integer id) {
        return null;
    }

    @Override
    public Iterable<Donation> findAll() {
        logger.traceEntry("finding all donations {}");
        Connection connection = dbUtils.getConnection();
        List<Donation> donations = new ArrayList<>();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from Donations")) {
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                while(resultSet.next()){
                    int idDonor = resultSet.getInt("donorID");
                    Donor donor = findByDonor(idDonor);

                    int idCharitableCase = resultSet.getInt("charitableCaseID");
                    CharitableCase charitableCase = findByCharitableCase(idCharitableCase);

                    int amountdonated = resultSet.getInt("amountDonated");
                    Donation donation = new Donation(donor,charitableCase,amountdonated);
                    donations.add(donation);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit(donations);
        return donations;
    }

    @Override
    public int size() {
        logger.traceEntry();
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select count(*) as [SIZE] from Donations")){
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
    public void deleteDonation(Integer donor, Integer charitableCase) {
        logger.traceEntry("deleting donation {}", donor,charitableCase);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("delete from CharitableCases where donorID = ? and charitableCaseID = ?")){
            preparedStatement.setInt(1,donor);
            preparedStatement.setInt(2,charitableCase);
            int result = preparedStatement.executeUpdate();
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit();
    }

    @Override
    public Donor findByDonor(Integer donor) {
        logger.traceEntry("finding donor with id {}", donor);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from Donors where id = ?")){
            preparedStatement.setInt(0,donor);
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                if(resultSet.next()){
                    int idDonor = resultSet.getInt("id");
                    String firstNameDonor = resultSet.getString("firstName");
                    String lastNameDonor = resultSet.getString("lastName");
                    String addressDonor = resultSet.getString("address");
                    String phoneNumberDonor = resultSet.getString("phoneNumber");
                    Donor donorr = new Donor(idDonor,firstNameDonor,lastNameDonor,addressDonor,phoneNumberDonor);
                    logger.traceExit(donor);
                    return donorr;
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("No donor found with id {}", donor);
        return null;
    }

    @Override
    public CharitableCase findByCharitableCase(Integer charitableC) {
        logger.traceEntry("finding charitable case with id {}",charitableC);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from CharitableCases where id = ?")){
            preparedStatement.setInt(0,charitableC);
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                if(resultSet.next()){
                    int idCharitableCase = resultSet.getInt("id");
                    String nameCharitableCase = resultSet.getString("name");
                    int amountCharitableCase = resultSet.getInt("amountRaised");
                    CharitableCase charitableCase = new CharitableCase(idCharitableCase,nameCharitableCase,amountCharitableCase);
                    logger.traceExit(charitableCase);
                    return charitableCase;
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("No charitable case found with id {}", charitableC);
        return null;
    }
}
