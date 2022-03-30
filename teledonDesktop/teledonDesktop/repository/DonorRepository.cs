using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using log4net;
using teledonCS.model;

namespace teledonCS.repository
{
    class DonorRepository : IDonorRepository
    {
        private static readonly ILog log = LogManager.GetLogger("DonorRepository");
        IDictionary<String, string> props;

        public DonorRepository(IDictionary<string, string> props)
        {
            log.Info("Creating DonorRepository");
            this.props = props;
        }

        public void delete(int key)
        {
            log.InfoFormat("Deleting donor");
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from Donors where id = @key";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@key";
                paramId.Value = key;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                if (dataR == 0)
                {
                    log.Error("No donor removed");
                    throw new Exception("No donor deleted!");
                }
                else
                {
                    log.InfoFormat("Donor removed");
                }
            }
            log.InfoFormat("Exiting delete");
        }

        public List<Donor> findAll()
        {
            log.InfoFormat("Finding all donors");
            IDbConnection con = DBUtils.getConnection(props);
            IList<Donor> donors = new List<Donor>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,firstName,lastName,address,phoneNumber from Donors";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idD = dataR.GetInt32(0);
                        String firstNameD = dataR.GetString(1);
                        String lastNameD = dataR.GetString(2);
                        String addressD = dataR.GetString(3);
                        String phoneNumberD = dataR.GetString(4);
                        Donor donor = new Donor(firstNameD, lastNameD, addressD, phoneNumberD);
                        donor.id = idD;
                        donors.Add(donor);
                    }
                }
            }
            log.InfoFormat("Exiting find all");
            return (List<Donor>) donors;
        }

        public int? getDonorByName(string firstName, string lastName)
        {
            log.InfoFormat("Entering findOne with first name {0} and last name {1}",firstName,lastName);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,firstName,lastName,address,phoneNumber from Donors where firstName= @first and lastName = @last";
                IDbDataParameter paramFirstName = comm.CreateParameter();
                paramFirstName.ParameterName = "@first";
                paramFirstName.Value = firstName;
                comm.Parameters.Add(paramFirstName);
                
                IDbDataParameter paramLastName = comm.CreateParameter();
                paramLastName.ParameterName = "@last";
                paramLastName.Value = lastName;
                comm.Parameters.Add(paramLastName);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idD = dataR.GetInt32(0);
                        log.InfoFormat("Exiting find by name with value {0}",idD);
                        return idD;
                    }
                }
            }

            log.InfoFormat("Exiting findOne with value{0}", null);
            return null;
        }

        public Donor getDonorByFullName(string first, string last)
        {
            log.InfoFormat("Entering findOne with first name {0} and last name {1}",first,last);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,firstName,lastName,address,phoneNumber from Donors where firstName= @first and lastName = @last";
                IDbDataParameter paramFirstName = comm.CreateParameter();
                paramFirstName.ParameterName = "@first";
                paramFirstName.Value = first;
                comm.Parameters.Add(paramFirstName);
                
                IDbDataParameter paramLastName = comm.CreateParameter();
                paramLastName.ParameterName = "@last";
                paramLastName.Value = last;
                comm.Parameters.Add(paramLastName);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idD = dataR.GetInt32(0);
                        String firstNameD = dataR.GetString(1);
                        String lastNameD = dataR.GetString(2);
                        String addressD = dataR.GetString(3);
                        String phoneNumberD = dataR.GetString(4);
                        Donor donor = new Donor(firstNameD, lastNameD, addressD, phoneNumberD);
                        donor.id = idD;
                        log.InfoFormat("Exiting find by full name with value {0}",donor);
                        return donor;
                    }
                }
            }

            log.InfoFormat("Exiting findOne with value{0}", null);
            return null;
        }


        public Donor findOne(int key)
        {
            log.InfoFormat("Entering findOne with value{0}",key);
            IDbConnection con = DBUtils.getConnection(props);

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,firstName,lastName,address,phoneNumber from Donors where id = @key";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@key";
                paramId.Value = key;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idD = dataR.GetInt32(0);
                        String firstNameD = dataR.GetString(1);
                        String lastNameD = dataR.GetString(2);
                        String addressD = dataR.GetString(3);
                        String phoneNumberD = dataR.GetString(4);
                        Donor donor = new Donor(firstNameD, lastNameD, addressD, phoneNumberD);
                        donor.id = idD;
                        log.InfoFormat("Exiting findOne with value{0}",donor);
                        return donor;
                    }
                }
            }

            log.InfoFormat("Exiting findOne with value{0}", null);
            return null;
        }

        public void save(Donor el)
        {
            log.InfoFormat("Saving donor");
            var con = DBUtils.getConnection(props);
            using(var comm=con.CreateCommand())
            {
                comm.CommandText = "insert into Donors(firstName,lastName,address,phoneNumber) values(@firstName,@lastName,@address,@phoneNumber)";
                var paramFirstName = comm.CreateParameter();
                paramFirstName.ParameterName = "@firstName";
                paramFirstName.Value = el.firstName;
                comm.Parameters.Add(paramFirstName);
                
                var paramLastName = comm.CreateParameter();
                paramLastName.ParameterName = "@lastName";
                paramLastName.Value = el.lastName;
                comm.Parameters.Add(paramLastName);
                
                var paramAddress = comm.CreateParameter();
                paramAddress.ParameterName = "@address";
                paramAddress.Value = el.address;
                comm.Parameters.Add(paramAddress);
                
                var paramPhoneNumber = comm.CreateParameter();
                paramPhoneNumber.ParameterName = "@phoneNumber";
                paramPhoneNumber.Value = el.phoneNumber;
                comm.Parameters.Add(paramPhoneNumber);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No donor added");
                    throw new Exception("No donor added!!");
                }
            }
            log.InfoFormat("Exiting save");
        }

        public void update(Donor el)
        {
            log.InfoFormat("Updating donor");
            var con = DBUtils.getConnection(props);
            using(var comm=con.CreateCommand())
            {
                comm.CommandText = "update Donors set firstName = @firstNameNew, lastName = @lastNameNew, address = @addressNew,phoneNumber = @phoneNumberNew where id = @id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = el.id;
                comm.Parameters.Add(paramId);
                
                var paramFirstName = comm.CreateParameter();
                paramFirstName.ParameterName = "@firstNameNew";
                paramFirstName.Value = el.firstName;
                comm.Parameters.Add(paramFirstName);
                
                var paramLastName = comm.CreateParameter();
                paramLastName.ParameterName = "@lastNameNew";
                paramLastName.Value = el.lastName;
                comm.Parameters.Add(paramLastName);
                
                var paramAddress = comm.CreateParameter();
                paramAddress.ParameterName = "@addressNew";
                paramAddress.Value = el.address;
                comm.Parameters.Add(paramAddress);
                
                var paramPhoneNumber = comm.CreateParameter();
                paramPhoneNumber.ParameterName = "@phoneNumberNew";
                paramPhoneNumber.Value = el.phoneNumber;
                comm.Parameters.Add(paramPhoneNumber);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No donor updated");
                    throw new Exception("No donor updated!!");
                }
                else
                {
                    log.InfoFormat("Updating donor");
                }
            }
            log.InfoFormat("Exiting update");
        }
    }
}
