using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class PersonDocumentDTO
    {
        private byte[] _docBytes;
        private string _name;
        private string _description;
        private int    _clientID;

        public PersonDocumentDTO()
        {
            _docBytes = null;
            _name = DTOConstants.DEFAULT_STRING;
            _description = DTOConstants.DEFAULT_STRING;
            _clientID = DTOConstants.DEFAULT_INT_ID;
        }

        public PersonDocumentDTO(string pName)
        {
            _docBytes = null;
            _name = pName;
            _description = DTOConstants.DEFAULT_STRING;
            _clientID = DTOConstants.DEFAULT_INT_ID;
        }

        public PersonDocumentDTO(int pClientID)
        {
            _docBytes = null;
            _name = DTOConstants.DEFAULT_STRING;
            _description = DTOConstants.DEFAULT_STRING;
            _clientID = pClientID;
        }

        public PersonDocumentDTO(int pClientID, byte[] pFile, string pName, string pDescription)
        {
                   _docBytes = pFile;
                   _name = pName;
            _description = pDescription;
               _clientID = pClientID;
        }

        //getters
        public byte[] getFile() { return _docBytes; }

        public string getName() { return _name; }

        public string getDescription() { return _description; }

        public int getPersonID() { return _clientID; }

        //setters
        public void setDocHexBytes(byte[] pFile) { _docBytes = pFile; }

        public void setName(string pName) { _name = pName; }

        public void setDescription(string pDescription) { _description = pDescription; }

        public void setPersonID(int pClientID) { _clientID = pClientID; }

    }
}
