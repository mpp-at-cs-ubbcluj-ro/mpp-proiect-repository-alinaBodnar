using teledonCS.model;

namespace services
{
    public interface ITeledonObserver
    {
        void donationSaved(Donation donation);
    }
}