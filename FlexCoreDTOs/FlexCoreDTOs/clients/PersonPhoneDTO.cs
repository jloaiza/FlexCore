using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class PersonPhoneDTO
    {
        private string _phone;
        private int _personID;

        public PersonPhoneDTO(int pPersonID)
        {
            _personID = pPersonID;
            _phone = DTOConstants.DEFAULT_STRING;
        }

        public PersonPhoneDTO(int pPersonID, string pPhone)
        {
            _phone = pPhone;
            _personID = pPersonID;
        }

        public PersonPhoneDTO(string pPhone)
        {
            _phone = pPhone;
            _personID = DTOConstants.DEFAULT_INT_ID;
        }

        //setters
        public void setPhone(string pPhone) { _phone = pPhone; }
        public void setPersonID(int pPersonID) { _personID = pPersonID; }

        //getters
        public string getPhone() { return _phone; }
        public int getPersonID() { return _personID; }
    }
}
