using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlexCoreDTOs.clients
{
    public class ClientDTO
    {

        private string _CIF;
        private bool   _active;
        private int    _clientID;

        public ClientDTO(int pIDClient = -1, string pCIF = "", bool pActive = false)
        {
                _clientID = pIDClient;
                     _CIF = pCIF;
                  _active = pActive;
        }

        //Setters
        public void setClientID(int pClientID) { _clientID = pClientID; }

        public void setCIF(string pCIF) { _CIF = pCIF; }

        public void setActive(bool pActive) { _active = pActive; }

        //Getters
        public int getClientID() { return _clientID; }        

        public string getCIF() { return _CIF; }

        public bool isActive() { return _active; }

    }
}
