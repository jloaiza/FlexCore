using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class JuridicalClientVDTO:ClientVDTO
    {

        public JuridicalClientVDTO()
        {
            _client = new ClientDTO();
            _person = new PersonDTO();
            _person.setPersonType(PersonDTO.JURIDIC_PERSON);
        }

        public JuridicalClientVDTO(int pClientID)
        {
            _client = new ClientDTO(pClientID);
            _person = new PersonDTO(pClientID);
            _person.setPersonType(PersonDTO.JURIDIC_PERSON);
        }

        public JuridicalClientVDTO(int pIDClient, string pName, string pIDCard, string pType, string pCIF, bool pActive = false)
        {
            _client = new ClientDTO(pIDClient, pCIF, pActive);
            _person = new PersonDTO(pIDClient, pName, pIDCard, pType);
            _person.setPersonType(PersonDTO.JURIDIC_PERSON);
        }

        public JuridicalClientVDTO(string pName, string pIDCard, string pType, string pCIF, bool pActive = false)
        {
            _client = new ClientDTO(DTOConstants.DEFAULT_INT_ID, pCIF, pActive);
            _person = new PersonDTO(DTOConstants.DEFAULT_INT_ID, pName, pIDCard, pType);
            _person.setPersonType(PersonDTO.JURIDIC_PERSON);
        }
    }
}
