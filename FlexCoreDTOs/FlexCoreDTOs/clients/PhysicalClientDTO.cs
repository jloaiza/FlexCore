using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class PhysicalClientDTO:GenericClientDTO
    {
        private string _firstLastName;
        private string _secondLastName;

        public PhysicalClientDTO(int pIDClient, string pIDDoc, string pName, string pFirstLastName, string pSecondLastName, string pCIF, int pClientTypeID, bool pActive = false, bool pLock = true)
            :base (pIDClient, pIDDoc, pName, pCIF, pClientTypeID, pActive, pLock)
        {
             _firstLastName = pFirstLastName;
            _secondLastName = pSecondLastName;
        }

        //Setters
        public void setFirstLastName(string pFirstLastName) { _firstLastName = pFirstLastName; }

        public void setSecondLastName(string pSecondLastName) { _secondLastName = pSecondLastName; }

        //Getters
        public string getFirstLastName() { return _firstLastName;  }

        public string getSecondLastName() { return _secondLastName; }

    }
}
