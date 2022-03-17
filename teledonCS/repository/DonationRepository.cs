using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using teledonCS.model;

namespace teledonCS.repository
{
    class DonationRepository : IDonationRepository<int, Donation>
    {
        private static readonly ILog log = LogManager.GetLogger("DonationRepository");
        IDictionary<String, string> props;

        public DonationRepository(IDictionary<string, string> props)
        {
            log.Info("Creating DonationRepository");
            this.props = props;
        }

        public void delete(int key)
        {
            throw new NotImplementedException();
        }

        public List<Donation> findAll()
        {
            throw new NotImplementedException();
        }

        public Donation findOne(int key)
        {
            throw new NotImplementedException();
        }

        public void save(Donation el)
        {
            var con = DBUtils.getConnection(props);
            using(var comm=con.CreateCommand())
            {
                comm.CommandText = "insert into Donations values(@donorId,@charitableCaseId,@amountDonated)";
                var paramDonorId = comm.CreateParameter();
                paramDonorId.ParameterName = "@donorId";
                paramDonorId.Value = el.donor.id;
                comm.Parameters.Add(paramDonorId);
                
                var paramCharitableCaseId = comm.CreateParameter();
                paramCharitableCaseId.ParameterName = "@charitableCaseId";
                paramCharitableCaseId.Value = el.charitableCase.id;
                comm.Parameters.Add(paramCharitableCaseId);
                
                var paramAmount = comm.CreateParameter();
                paramAmount.ParameterName = "@amountDonated";
                paramAmount.Value = el.amountDonated;
                comm.Parameters.Add(paramAmount);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new Exception("No donation added!!");
            }
        }

        public void update(Donation el)
        {
            var con = DBUtils.getConnection(props);
            using(var comm=con.CreateCommand())
            {
                comm.CommandText = "update Donations set amountDonated = @amountNew where id = @id";
                var paramId = comm.CreateParameter();
                paramId.ParameterName = "@id";
                paramId.Value = el.id;
                comm.Parameters.Add(paramId);

                var paramAmount = comm.CreateParameter();
                paramAmount.ParameterName = "@amountNew";
                paramAmount.Value = el.amountDonated;
                comm.Parameters.Add(paramAmount);

                var result = comm.ExecuteNonQuery();
                if (result == 0)
                    throw new Exception("No donation updated!!");
            }
        }
    }
}
