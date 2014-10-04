using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class PersonDocumentDTO
    {
        private string _hexBytes;
        private string _name;
        private string _description;
        private int    _clientID;

        public PersonDocumentDTO()
        {
            _hexBytes = DTOConstants.DEFAULT_STRING;
            _name = DTOConstants.DEFAULT_STRING;
            _description = DTOConstants.DEFAULT_STRING;
            _clientID = DTOConstants.DEFAULT_INT_ID;
        }

        public PersonDocumentDTO(string pName)
        {
            _hexBytes = DTOConstants.DEFAULT_STRING;
            _name = pName;
            _description = DTOConstants.DEFAULT_STRING;
            _clientID = DTOConstants.DEFAULT_INT_ID;
        }

        public PersonDocumentDTO(int pClientID)
        {
            _hexBytes = DTOConstants.DEFAULT_STRING;
            _name = DTOConstants.DEFAULT_STRING;
            _description = DTOConstants.DEFAULT_STRING;
            _clientID = pClientID;
        }

        public PersonDocumentDTO(int pClientID, string pDocHexBytes, string pName, string pDescription)
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

        public int getPersonID() { return _clientID; }

        //setters
        public void setDocHexBytes(string pBytes) { _hexBytes = pBytes; }

        public void setName(string pName) { _name = pName; }

        public void setDescription(string pDescription) { _description = pDescription; }

        public void setPersonID(int pClientID) { _clientID = pClientID; }

    }
}
