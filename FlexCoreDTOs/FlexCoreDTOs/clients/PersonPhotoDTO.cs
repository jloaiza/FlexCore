using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class PersonPhotoDTO
    {
        private byte[] _photoBytes;
        private int  _personID;

        public PersonPhotoDTO(int pPersonID)
        {
            _personID = pPersonID;
            _photoBytes = null;
        }

        public PersonPhotoDTO(int pPersonID, byte[] pFile)
        {
            _photoBytes = pFile;
            _personID = pPersonID;
        }

        //setters
        public void setHexBytes(byte[] pFile) { _photoBytes = pFile; }
        public void setPersonID(int pPersonID) { _personID = pPersonID; }

        //getters
        public byte[] getHexBytes() { return _photoBytes; }
        public int getPersonID() { return _personID; }
    }
}
