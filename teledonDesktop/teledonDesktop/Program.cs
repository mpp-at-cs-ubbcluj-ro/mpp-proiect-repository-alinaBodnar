using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net.Config;
using teledonCS.model;
using teledonCS.repository;
using teledonDesktop.services;

namespace teledonDesktop
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    // [STAThread]
    // static void Main()
    // {
    //   Application.EnableVisualStyles();
    //   Application.SetCompatibleTextRenderingDefault(false);
    //   Application.Run(new Form1());
    // }
     static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            IDictionary<String, string> props = new SortedList<string, string>();
            props.Add("ConnectionString",GetConnectionStringByName("teledonTV"));
            
            VolunteerRepository volunteerRepository = new VolunteerRepository(props);
            VolunteerService volunteerService = new VolunteerService(volunteerRepository);
            
            CharitableCaseRepository charitableCaseRepository = new CharitableCaseRepository(props);
            CharitableCaseService charitableCaseService = new CharitableCaseService(charitableCaseRepository);
            
            DonorRepository donorRepository = new DonorRepository(props);
            DonorService donorService = new DonorService(donorRepository);
            
            DonationRepository donationRepository = new DonationRepository(props);
            DonationService donationService =
                new DonationService(donationRepository, donorRepository, charitableCaseRepository);
            
            Form1 form1 = new Form1();
            form1.setServices(volunteerService,charitableCaseService,donationService,donorService);
            Application.Run(form1);

        }

        
        static  string GetConnectionStringByName(string name)
        {
            // Assume failure.
            string returnValue = null;

            // Look for the name in the connectionStrings section.
            ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];

            // If found, return the connection string.
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
      
  }
}
