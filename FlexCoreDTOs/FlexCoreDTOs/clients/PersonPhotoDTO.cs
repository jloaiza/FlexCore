using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class PersonPhotoDTO
    {
        private string _hexBytes;
        private int    _personID;

        public PersonPhotoDTO(string pHexBytes, int pPersonID)
        {
            _hexBytes = pHexBytes;
            _personID = pPersonID;
        }

        //setters
        public void setHexBytes(string pHexBytes) { _hexBytes = pHexBytes; }
        public void setPersonID(int pPersonID) { _personID = pPersonID; }

        //getters
        public string getHexBytes() { return _hexBytes; }
        public int getPersonID() { return _personID; }
    }
}
