using teledonCS.model;

namespace teledonCS.utils.observer.events
{
    public class DonationEvent: Event
    {
        private ChangeEventType type;
        private CharitableCase charitableCase, oldCharitableCase;

        public DonationEvent(ChangeEventType type, CharitableCase charitableCase, CharitableCase oldCharitableCase)
        {
            this.type = type;
            this.charitableCase = charitableCase;
            this.oldCharitableCase = oldCharitableCase;
        }

        public DonationEvent(ChangeEventType type, CharitableCase charitableCase)
        {
            this.type = type;
            this.charitableCase = charitableCase;
        }

        public ChangeEventType getType()
        {
            return type;
        }

        public CharitableCase getCharitableCase()
        {
            return charitableCase;
        }

        public CharitableCase GetOldCharitableCase()
        {
            return oldCharitableCase;
        }
    }
}