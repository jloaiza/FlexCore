using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    class PersonPhoneDTO
    {
        private string _phone;
        private int _personID;

        public PersonPhoneDTO(string pPhone, int pPersonID)
        {
            _phone = pPhone;
            _personID = pPersonID;
        }

        //setters
        public void setPhone(string pPhone) { _phone = pPhone; }
        public void setPersonID(int pPersonID) { _personID = pPersonID; }

        //getters
        public string getPhone() { return _phone; }
        public int getPersonID() { return _personID; }
    }
}
