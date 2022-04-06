package repository;

import model.CharitableCase;
import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class CharitableCaseRepository implements ICharitableCaseRepository {
    private JdbcUtils dbUtils;

    private static final Logger logger = LogManager.getLogger();

    public CharitableCaseRepository(Properties props) {
        logger.info("Initializing CharitableCaseRepository with properties: {}",props);
        dbUtils = new JdbcUtils(props);
    }

    @Override
    public void save(CharitableCase elem) {
        logger.traceEntry("saving charitable case {}", elem);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("insert into CharitableCases(name,amountRaised) values (?,?)")){
            preparedStatement.setString(1,elem.getName());
            preparedStatement.setInt(2,elem.getAmountRaised());
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
        logger.traceEntry("deleting charitable case {}", id);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("delete from CharitableCases where id = ?")){
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
    public void update(CharitableCase elem, Integer id) {
        logger.traceEntry("updating charitable case {}", id);
        Connection connection = dbUtils.getConnection();
        String sql = String.format("update CharitableCases set amountRaised = ? where id = %d",id);
        try(PreparedStatement preparedStatement = connection.prepareStatement(sql)) {
            preparedStatement.setInt(1,elem.getAmountRaised());
            int result = preparedStatement.executeUpdate();
            logger.trace("Updated {} instances", result);
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit();


    }

    @Override
    public CharitableCase findById(Integer id) {
        logger.traceEntry("finding charitable case with id {}", id);
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from CharitableCases where id = ?")){
            preparedStatement.setInt(0,id);
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                if(resultSet.next()){
                    int idCharitableCase = resultSet.getInt("id");
                    String nameCharitableCase = resultSet.getString("name");
                    int amountCharitableCase = resultSet.getInt("amountRaised");
                    CharitableCase charitableCase = new CharitableCase(nameCharitableCase,amountCharitableCase);
                    logger.traceExit(charitableCase);
                    return charitableCase;
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("No charitable case found with id {}", id);
        return null;

    }

    @Override
    public Iterable<CharitableCase> findAll() {
        logger.traceEntry("finding all charitable cases {}");
        Connection connection = dbUtils.getConnection();
        List<CharitableCase> charitableCaseList = new ArrayList<>();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from CharitableCases")) {
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                while(resultSet.next()){
                    int idCharitableCase = resultSet.getInt("id");
                    String nameCharitableCase = resultSet.getString("name");
                    int amountCharitableCase = resultSet.getInt("amountRaised");
                    CharitableCase charitableCase = new CharitableCase(nameCharitableCase,amountCharitableCase);
                    charitableCaseList.add(charitableCase);
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit(charitableCaseList);
        return charitableCaseList;
    }

    @Override
    public int size() {
       logger.traceEntry();
       Connection connection = dbUtils.getConnection();
       try(PreparedStatement preparedStatement = connection.prepareStatement("select count(*) as [SIZE] from CharitableCases")){
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
    public Integer getCharitableCaseByName(String name) {
        logger.traceEntry("finding charitable case with id {}");
        Connection connection = dbUtils.getConnection();
        try(PreparedStatement preparedStatement = connection.prepareStatement("select * from CharitableCases where name = ?")){
            preparedStatement.setString(1,name);
            try(ResultSet resultSet = preparedStatement.executeQuery()){
                if(resultSet.next()){
                    int idCharitableCase = resultSet.getInt("id");
                    return idCharitableCase;
                }
            }
        } catch (SQLException e) {
            logger.error(e);
            System.out.println("Error DB " + e);
        }
        logger.traceExit("No charitable case found with id {}");
        return null;

    }
}
