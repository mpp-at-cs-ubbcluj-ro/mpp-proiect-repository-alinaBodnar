using System;
using teledonCS.model;
using teledonCS.repository;

namespace teledonDesktop.services
{
    public class VolunteerService
    {
        public IVolunteerRepository VolunteerRepository;

        public VolunteerService(IVolunteerRepository volunteerRepository)
        {
            VolunteerRepository = volunteerRepository;
        }

        public Volunteer getVolunteer(String username, String password)
        {
            return VolunteerRepository.getByUsernameAndPassword(username, password);
        }
    }
}