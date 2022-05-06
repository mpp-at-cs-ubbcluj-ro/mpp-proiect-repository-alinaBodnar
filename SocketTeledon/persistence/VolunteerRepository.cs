using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using log4net;
using teledonCS.model;

namespace teledonCS.repository
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private static readonly ILog log = LogManager.GetLogger("VolunteerRepository");
        IDictionary<String, string> props;

        public VolunteerRepository(IDictionary<string, string> props)
        {
            log.Info("Creating VolunteerRepository");
            this.props = props;
        }

        public void delete(int key)
        {
            log.InfoFormat("Deleting volunteer");
            IDbConnection con = DBUtils.getConnection();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from Volunteers where id = @key";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@key";
                paramId.Value = key;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                {
                    log.Error("No volunteer deleted");
                    throw new Exception("No volunteer deleted!");
                }
                else
                {
                    log.InfoFormat("Volunteer deleted");
                }
            }
            log.InfoFormat("Exiting delete");
        }

        public List<Volunteer> findAll()
        {
            log.InfoFormat("Finding all Volunteers");
            IDbConnection con = DBUtils.getConnection();
            IList<Volunteer> volunteers = new List<Volunteer>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,username,password from Volunteers";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idV = dataR.GetInt32(0);
                        String usernameV = dataR.GetString(1);
                        String passwordV = dataR.GetString(2);
                        Volunteer volunteer = new Volunteer(idV, usernameV, passwordV);
                        volunteers.Add(volunteer);
                    }
                }
            }
            log.InfoFormat("Exiting findAll");

            return (List<Volunteer>) volunteers;
        }

        public Volunteer getByUsernameAndPassword(string username, string password)
        {
            log.InfoFormat("Entering find volunteer by username{0} and password{1}",username,password);
            IDbConnection con = DBUtils.getConnection() ?? throw new ArgumentNullException("DBUtils.getConnection()");

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,username,password from Volunteers where username = @user and password = @pass";
                IDbDataParameter paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@user";
                paramUsername.Value = username;
                comm.Parameters.Add(paramUsername);

                IDbDataParameter paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@pass";
                paramPassword.Value = password;
                comm.Parameters.Add(paramPassword);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idV = dataR.GetInt32(0);
                        String usernameV = dataR.GetString(1);
                        String passwordV = dataR.GetString(2);
                        Volunteer volunteer = new Volunteer(idV, usernameV, passwordV);
                        log.InfoFormat("Exiting findOne with value{0}", volunteer);
                        return volunteer;
                    }
                }
            }
            log.InfoFormat("Exiting findOne with value{0}", null);
            return null;
        }

        public Volunteer findOne(int key)
        {
            log.InfoFormat("Entering findOne with value{0}",key);
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,username,password from Volunteers where id = @key";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@key";
                paramId.Value = key;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idV = dataR.GetInt32(0);
                        String usernameV = dataR.GetString(1);
                        String passwordV = dataR.GetString(2);
                        Volunteer volunteer = new Volunteer(idV, usernameV, passwordV);
                        log.InfoFormat("Exiting findOne with value{0}", volunteer);
                        return volunteer;
                    }
                }
            }
            log.InfoFormat("Exiting findOne with value{0}", null);
            return null;
        }

        public void save(Volunteer el)
        {
            log.InfoFormat("Saving volunteer");
            var con = DBUtils.getConnection();
            using(var comm=con.CreateCommand())
            {
                comm.CommandText = "insert into Volunteers values(@username,@password)";
                var paramUserName = comm.CreateParameter();
                paramUserName.ParameterName = "@username";
                paramUserName.Value = el.username;
                comm.Parameters.Add(paramUserName);
                
                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@password";
                paramPassword.Value = el.password;
                comm.Parameters.Add(paramPassword);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No volunteer saved");
                    throw new Exception("No volunteer added!!");
                }
                else
                {
                    log.InfoFormat("Volunteer saved");
                }
            }
            log.InfoFormat("Exiting save");
        }

        public void update(Volunteer el)
        {
            log.InfoFormat("Updating volunteer");
            var con = DBUtils.getConnection();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText =
                    "update Volunteers set username = @usernameNew, password = @passwordNew where id = @id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = el.id;
                comm.Parameters.Add(paramId);

                var paramUsername = comm.CreateParameter();
                paramUsername.ParameterName = "@usernameNew";
                paramUsername.Value = el.username;
                comm.Parameters.Add(paramUsername);

                var paramPassword = comm.CreateParameter();
                paramPassword.ParameterName = "@passwordNew";
                paramPassword.Value = el.password;
                comm.Parameters.Add(paramPassword);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No volunteer updated");
                    throw new Exception("No volunteer updated!!");
                }
                else
                {
                    log.InfoFormat("Volunteer updated");
                }
            }

            log.InfoFormat("Exiting update");
        }
    }
}
