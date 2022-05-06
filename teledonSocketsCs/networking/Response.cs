using System;
using System.Collections.Generic;
using teledonCS.model;

namespace networking
{
    public interface Response
    {
        
    }

    [Serializable]
    public class OkResponse : Response
    {
        
    }

    [Serializable]
    public class ErrorResponse : Response
    {
        private String message;

        public ErrorResponse(String message)
        {
            this.message = message;
        }
        public virtual string Message
        {
            get
            {
                return message;
            }
        }
    }

    [Serializable]
    public class GetCharitableCasesResponse : Response
    {
        private List<CharitableCase> cases;

        public GetCharitableCasesResponse(List<CharitableCase> all)
        {
            this.cases = all;
        }

        public virtual List<CharitableCase> GetCases
        {
            get
            {
                return cases;
            }
        }
    }
     [Serializable]
    public class GetDonorsByNameResponse : Response
    {
        private List<Donor> donors;


        public GetDonorsByNameResponse(List<Donor> donors)
        {
            this.donors = donors;
        }

        public virtual List<Donor> Donors
        {
            get
            {
                return donors;
            }
        }
    }

    [Serializable]
    public class GetDonorByFullNameResponse : Response
    {
        private Donor _donor;

        public GetDonorByFullNameResponse(Donor donor)
        {
            this._donor = donor;
        }

        public virtual Donor GetDonor
        {
            get
            {
                return _donor;
            }
        }
        
    }

    [Serializable]
    public class SaveDonorResponse : Response
    {
        private String info;

        public SaveDonorResponse(String info)
        {
            this.info = info;
        }

        public virtual String Info
        {
            get
            {
                return info;
            }
        }
    }

    [Serializable]
    public class GetDonorIdResponse : Response
    {
        private int id;

        public GetDonorIdResponse(int id)
        {
            this.id= id;
        }

        public virtual int getId
        {
            get
            {
                return id;
            }   
        }
    }

    [Serializable]
    public class GetDonorResponse : Response
    {
        private Donor donor;

        public GetDonorResponse(Donor donor)
        {
            this.donor = donor;
        }

        public virtual Donor GetDonor
        {
            get
            {
                return donor;
            }
        }
    }

    [Serializable]
    public class GetCharitableCaseResponse : Response
    {
        private int _case;

        public GetCharitableCaseResponse(int cs)
        {
            this._case = cs;
        }

        public virtual int getCase
        {
            get
            {
                return _case;
            }
        }
    }

    public interface UpdateResponse : Response
    {
        
    }

    [Serializable]
    public class NewDonationResponse : UpdateResponse
    {
        private Donation _donation;

        public NewDonationResponse(Donation donation)
        {
            this._donation = donation;
        }

        public virtual Donation GetDonation
        {
            get
            {
                return _donation;
            }
        }

    }
    
}