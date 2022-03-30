using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using teledonCS.model;
using teledonCS.repository;
using log4net;
using log4net.Config;
namespace teledonCS
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4j.xml"));
            Console.WriteLine("Configuration Settings for teledonTV{0}", GetConnectionStringByName("teledonTV"));
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString",GetConnectionStringByName("teledonTV"));
            
            Console.WriteLine("Charitable Cases Repository DB");
            CharitableCaseRepository charitableCaseRepository = new CharitableCaseRepository(props);
            // foreach (CharitableCase variableCharitableCase in charitableCaseRepository.findAll())
            // {
            //     Console.WriteLine(variableCharitableCase);
            // }
            //
            // CharitableCase charitableCase = new CharitableCase(4, "Ucraina", 60000);
            // charitableCaseRepository.save(charitableCase);
            CharitableCase charitableCase1 = new CharitableCase(5, "Strangere fonduri", 1000);
            charitableCaseRepository.update(charitableCase1);
            foreach (CharitableCase variableCharitableCase in charitableCaseRepository.findAll())
            {
                Console.WriteLine(variableCharitableCase);
            }
            // DonorRepository donorRepository = new DonorRepository(props);
            // foreach (Donor donor in donorRepository.findAll())
            // {
            //     Console.WriteLine(donor);
            // }
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
