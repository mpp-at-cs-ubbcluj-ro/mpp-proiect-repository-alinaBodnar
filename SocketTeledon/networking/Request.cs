using System;
using System.Runtime.CompilerServices;
using teledonCS.model;

namespace networking
{
    public interface Request
    {
        
    }

    [Serializable]
    public class LoginRequest : Request
    {
        private Volunteer _volunteer;

        public LoginRequest(Volunteer volunteer)
        {
            this._volunteer = volunteer;
        }

        public virtual Volunteer Volunteer
        {
            get
            {
                return _volunteer;
            }
        }
    }

    [Serializable]
    public class LogoutRequest : Request
    {
        private Volunteer _volunteer;

        public LogoutRequest(Volunteer volunteer)
        {
            this._volunteer = volunteer;
        }

        public virtual Volunteer Volunteer
        {
            get
            {
                return _volunteer;
            }
        }
    }

    [Serializable]
    public class GetCharitableCasesRquest:Request
    {
        public GetCharitableCasesRquest(){}
        
    }

    [Serializable]
    public class SaveDonationRequest : Request
    {
        private Donation _donation;

        public SaveDonationRequest(Donation donation)
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

    [Serializable]
    public class GetDonorsByNameRequest : Request
    {
        private String _name;

        public GetDonorsByNameRequest(String name)
        {
            this._name = name;
        }

        public virtual String Name
        {
            get
            {
                return _name;
            }
        }
    }

    [Serializable]
    public class GetDonorByFullNameRequest : Request
    {
        private String _fullName;

        public GetDonorByFullNameRequest(String fullName)
        {
            this._fullName = fullName;
        }

        public virtual String GetFullName
        {
            get
            {
                return _fullName;
            }
        }
        
    }

    [Serializable]
    public class SaveDonorRequest : Request
    {
        private String info;

        public SaveDonorRequest(String info)
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
    public class GetDonorIdRequest : Request
    {
        private String fullName;

        public GetDonorIdRequest(String fullName)
        {
            this.fullName = fullName;
        }

        public virtual String FullName
        {
            get
            {
                return fullName;
            }   
        }
    }

    [Serializable]
    public class GetDonorRequest : Request
    {
        private int id;

        public GetDonorRequest(int id)
        {
            this.id = id;
        }

        public virtual int GetId
        {
            get
            {
                return id;
            }
        }
    }

    [Serializable]
    public class GetCharitableCaseRequest : Request
    {
        private String name;

        public GetCharitableCaseRequest(String name)
        {
            this.name = name;
        }

        public virtual String getName
        {
            get
            {
                return name;
            }
        }
    }
}