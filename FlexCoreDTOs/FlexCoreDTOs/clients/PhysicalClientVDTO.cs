using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class PhysicalClientVDTO
    {
        private PhysicalPersonDTO _person;
        private ClientDTO _client;

        public PhysicalClientVDTO()
        {
            _client = new ClientDTO();
            _person = new PhysicalPersonDTO();
        }

        public PhysicalClientVDTO(int pClientID)
        {
            _client = new ClientDTO(pClientID);
            _person = new PhysicalPersonDTO(pClientID);
        }

        public PhysicalClientVDTO(int pIDClient, string pName, string pFirstLastName, string pSecondLastName, string pIDCard, string pCIF, bool pActive = false)
        {
            _client = new ClientDTO(pIDClient, pCIF, pActive);
            _person = new PhysicalPersonDTO(pIDClient, pName, pFirstLastName, pSecondLastName, pIDCard);
        }

        //Setters
        public void setClientID(int pClientID) { 
            _client.setClientID(pClientID);
            _person.setPersonID(pClientID);
        }

        public void setCIF(string pCIF) { _client.setCIF(pCIF); }

        public void setActive(bool pActive) { _client.setActive(pActive); }

        public void setName(string pName) { _person.setName(pName); }

        public void setIDCard(string pIDCard) { _person.setIDCard(pIDCard); }

        public void setPersonType(string pType) { _person.setPersonType(pType); }

        public void setFirstLastName(string pLastName) { _person.setFirstLastName(pLastName); }

        public void setSecondLastName(string pLastName) { _person.setSecondLastName(pLastName); }

        //Getters
        public int getClientID() { return _client.getClientID(); }

        public string getCIF() { return _client.getCIF(); }

        public bool isActive() { return _client.isActive(); }

        public int getPersonID() { return _person.getPersonID(); }

        public string getName() { return _person.getName(); }

        public string getIDCard() { return _person.getIDCard(); }

        public string getPersonType() { return _person.getPersonType(); }

        public string getFirstLastName() { return _person.getFirstLastName(); }

        public string getSecondLastName() { return _person.getSecondLastName(); }
    }
}
