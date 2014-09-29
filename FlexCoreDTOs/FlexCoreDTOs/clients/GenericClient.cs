using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlexCoreDTOs.clients
{
    public class GenericClientDTO
    {

        public static readonly string TIPO_JURIDICO = "Jurídico";
        public static readonly string TIPO_FISICO = "Físico";

        private int     _clientID;
        private string  _idDoc;
        private string  _name;
        private string  _CIF;
        private bool    _active;
        private int     _clientTypeID;
        private bool    _lock;

        public GenericClientDTO(int pIDClient, string pIDDoc, string pName, string pCIF, int pClientTypeID, bool pActive = false, bool pLock = true)
        {
                _clientID = pIDClient;
                   _idDoc = pIDDoc;
                    _name = pName;
                     _CIF = pCIF;
            _clientTypeID = pClientTypeID;
                  _active = pActive;
                    _lock = pLock;
        }

        //Setters
        public void setIDCliente(int pIDClient) { _clientID = pIDClient; }

        public void setCedula(string pIDDoc) { _idDoc = pIDDoc; }

        public void setNombre(string pName) { _name = pName; }

        public void setCIF(string pCIF) { _CIF = pCIF; }

        public void setIDTipo(int pClientTypeID) { _clientTypeID = pClientTypeID; }

        public void setActivo(bool pActive) { _active = pActive; }

        public void seteClientTypeID(int pClientTypeID) { _clientTypeID = pClientTypeID; }

        public void setLock(bool pLock) { _lock = pLock; }

        //Getters
        public int getClientID() { return _clientID; }

        public string getIDDoc() { return _idDoc; }

        public string getName() { return _name; }

        public string getCIF() { return _CIF; }

        public bool isActive() { return _active; }

        public int getClientTypeID() { return _clientTypeID; }

        public bool isLock() { return _lock;  }

    }
}
