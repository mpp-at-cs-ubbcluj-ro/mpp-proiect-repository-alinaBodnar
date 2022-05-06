using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using log4net;
using teledonCS.model;

namespace teledonCS.repository
{
    public class CharitableCaseRepository : ICharitableChaseRepository
    {
        private static readonly ILog log = LogManager.GetLogger("CharitableCaseRepository");
        IDictionary<String, string> props;

        public CharitableCaseRepository(IDictionary<String, string> props)
        {
            log.Info("Creating CharitableCaseRepository");
            this.props = props;
        }

        public void delete(int key)
        {
            log.InfoFormat("Deleting charitable case");
            IDbConnection con = DBUtils.getConnection();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "delete from CharitableCases where id = @key";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@key";
                paramId.Value = key;
                comm.Parameters.Add(paramId);
                var dataR = comm.ExecuteNonQuery();
                
                if (dataR == 0)
                {
                    log.Error("No charitable case removed");
                }
                else
                    log.InfoFormat("Charitable case removed");
                    
            }
            
        log.InfoFormat("Exiting delete");
        }

        public List<CharitableCase> findAll()
        {
            log.InfoFormat("Finding all charitable cases");
            IDbConnection con = DBUtils.getConnection();
            IList<CharitableCase> charitableCases = new List<CharitableCase>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,name,amountRaised from CharitableCases";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int idCC = dataR.GetInt32(0);
                        String nameCC = dataR.GetString(1);
                        int amountRaisedCC = dataR.GetInt32(2);
                        CharitableCase charitableCase = new CharitableCase(idCC, nameCC, amountRaisedCC);
                        charitableCases.Add(charitableCase);
                    }
                }
            }
            log.InfoFormat("Exiting findAll");
            return (List<CharitableCase>) charitableCases;
        }

        public int? getCharitableCaseByName(string name)
        {
            log.InfoFormat("Entering find charitable case by name");
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id from CharitableCases where name = @nameToSearch";
                IDbDataParameter paramName = comm.CreateParameter();
                paramName.ParameterName = "@nameToSearch";
                paramName.Value = name;
                comm.Parameters.Add(paramName);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idCC = dataR.GetInt32(0);

                        log.InfoFormat("Exiting find by name");
                        
                        return idCC;
                    }
                }
            }

            log.InfoFormat("Exiting findOne with value{0}", null);
            return null;
            
        }

        public CharitableCase findOne(int key)
        {
            log.InfoFormat("Entering findOne with value{0}",key);
            IDbConnection con = DBUtils.getConnection();

            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,name,amountRaised from CharitableCases where id = @key";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@key";
                paramId.Value = key;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int idCC = dataR.GetInt32(0);
                        String nameCC = dataR.GetString(1);
                        int amountCC = dataR.GetInt32(2);
                        CharitableCase charitableCase = new CharitableCase(nameCC, amountCC);
                        charitableCase.id = idCC;
                        log.InfoFormat("Exiting findOne with value{0}",charitableCase);
                        return charitableCase;
                    }
                }
            }

            log.InfoFormat("Exiting findOne with value{0}", null);
            return null;
        }

        public void save(CharitableCase el)
        {
            log.InfoFormat("Saving charitable case");
            var con = DBUtils.getConnection();
            using(var comm=con.CreateCommand())
            {
                comm.CommandText = "insert into CharitableCases(name,amountRaised) values (@name,@amount)";
                var paramName = comm.CreateParameter();
                paramName.ParameterName = "@name";
                paramName.Value = el.name;
                comm.Parameters.Add(paramName);
                
                var paramAmount = comm.CreateParameter();
                paramAmount.ParameterName = "@amount";
                paramAmount.Value = el.amountRaised;
                comm.Parameters.Add(paramAmount);
                
                var result = comm.ExecuteNonQuery();
                if (result == 0)
                {
                    log.Error("No charitable case added");
                    throw new Exception("No charitable case added!!");
                }
                else
                {
                    log.InfoFormat("Charitable Case saved");
                }
            }
            log.InfoFormat("Exiting save");
        }

        public void update(CharitableCase el)
        {
            log.InfoFormat("Updating charitable case");
            var con = DBUtils.getConnection();
            using(var comm=con.CreateCommand())
            {
                comm.CommandText = "update CharitableCases set amountRaised = @amountNew where id = @id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = el.id;
                comm.Parameters.Add(paramId);

                var paramAmount = comm.CreateParameter();
                paramAmount.ParameterName = "@amountNew";
                paramAmount.Value = el.amountRaised;
                comm.Parameters.Add(paramAmount);

                try
                {
                    var result = comm.ExecuteNonQuery();
                    if (result == 0)
                    {
                        log.Error("No charitable case updated");
                        throw new Exception("No charitable case updated!!");
                    }

                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e.Message);
                }
                
            }
           
            log.InfoFormat("Exiting update");
            }
        
    }
}
