using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class PersonAddressDTO
    {
        private string _address;
        private int _personID;

        public PersonAddressDTO(string pAddress, int pPersonID)
        {
            _address = pAddress;
            _personID = pPersonID;
        }

        //setters
        public void setAddress(string pAddress) { _address = pAddress; }
        public void setPersonID(int pPersonID) { _personID = pPersonID; }

        //getters
        public string getAddress() { return _address; }
        public int getPersonID() { return _personID; }

    }
}
