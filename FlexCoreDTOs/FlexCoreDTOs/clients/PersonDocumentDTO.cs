using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    class PersonDocumentDTO
    {
        private string _hexBytes;
        private string _name;
        private string _description;
        private int    _clientID;

        public PersonDocumentDTO(string pDocHexBytes, string pName, string pDescription, int pClientID)
        {
               _hexBytes = pDocHexBytes;
                   _name = pName;
            _description = pDescription;
               _clientID = pClientID;
        }

        //getters
        public string getDocHexBytes() { return _hexBytes; }

        public string getName() { return _name; }

        public string getDescription() { return _description; }

        public int getClientID() { return _clientID; }

        //setters
        public void setDocHexBytes(string pBytes) { _hexBytes = pBytes; }

        public void setName(string pName) { _name = pName; }

        public void setDescription(string pDescription) { _description = pDescription; }

        public void setClientID(int pClientID) { _clientID = pClientID; }

    }
}
