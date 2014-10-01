using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlexCoreDTOs.clients
{
    public class ClientDTO
    {

        //
        private string _CIF;
        private bool   _active;
        private bool   _lock;
        private int    _clientID;

        public ClientDTO(int pIDClient, string pCIF, bool pActive = false, bool pLock = true)
        {
                _clientID = pIDClient;
                     _CIF = pCIF;
                  _active = pActive;
                    _lock = pLock;
        }

        //Setters
        public void setClientID(int pClientID) { _clientID = pClientID; }

        public void setCIF(string pCIF) { _CIF = pCIF; }

        public void setActive(bool pActive) { _active = pActive; }

        public void setLock(bool pLock) { _lock = pLock; }

        //Getters
        public int getClientID() { return _clientID; }        

        public string getCIF() { return _CIF; }

        public bool isActive() { return _active; }

        public bool isLock() { return _lock;  }

    }
}
