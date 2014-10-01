using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    class PhysicalPersonDTO:PersonDTO
    {
        private string _firstLastName;
        private string _secondLastName;

        public PhysicalPersonDTO(int pPersonID, string pName, string pFirstLastName, string pSecondLastName, string pIDCard)
            : base (pPersonID, pName, pIDCard, PersonDTO.PHYSICAL_PERSON)
        {
             _firstLastName = pFirstLastName;
            _secondLastName = pSecondLastName;
        }

        //setters
        public void setFirstLastName(string pLastName) { _firstLastName = pLastName; }
        public void setSecondLastName(string pLastName) { _secondLastName = pLastName; }

        //getters
        public string getFirstLastName() { return _firstLastName; }
        public string getSecondLastName() { return _secondLastName; }

    }
}
