using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;
using teledonCS.model;
using teledonCS.repository;
using teledonCS.utils.observer;

namespace teledonDesktop.services
{
    public class CharitableCaseService
    {
        public ICharitableChaseRepository _charitableChaseRepository;


        public CharitableCaseService(ICharitableChaseRepository charitableChaseRepository)
        {
            _charitableChaseRepository = charitableChaseRepository;
        }
        public List<CharitableCase> getAll()
        {
            return _charitableChaseRepository.findAll();
        }

        public int getCharitableCaseByName(String name)
        {
            return (int) _charitableChaseRepository.getCharitableCaseByName(name);
        }
    }
    
   
}