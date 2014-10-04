using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.clients
{
    public class PersonDTO
    {
        public static readonly string PHYSICAL_PERSON = "Fisica";
        public static readonly string JURIDIC_PERSON  = "Juridica";

        private int    _personID;
        private string _name;
        private string _idCard;
        private string _type;

        public PersonDTO()
        {
            _personID = DTOConstants.DEFAULT_INT_ID;
            _name = DTOConstants.DEFAULT_STRING;
            _idCard = DTOConstants.DEFAULT_STRING;
            _type = DTOConstants.DEFAULT_STRING;
        }


        public PersonDTO(int pPersonID)
        {
            _personID = pPersonID;
            _name = DTOConstants.DEFAULT_STRING;
            _idCard = DTOConstants.DEFAULT_STRING;
            _type = DTOConstants.DEFAULT_STRING;
        }

        public PersonDTO(int pPersonID, string pName, string pIDCard, string pType)
        {
            _personID = pPersonID;
            _name = pName;
            _idCard = pIDCard;
            _type = pType;
            checkType();
        }

        private void checkType()
        {
            bool notValid = _type != PHYSICAL_PERSON && _type != JURIDIC_PERSON;
            if (notValid)
            {
                Console.WriteLine("Bad person type in attribute pType of new PersonDTO. Name: {0,0}, ID card: {1,0}, Type: {2,0}. Use class satic variables.", _name, _idCard, _type);
            }
        }

        //setters
        public void setPersonID(int pPersonID) { _personID = pPersonID; }
        public void setName(string pName) { _name = pName; }
        public void setIDCard(string pIDCard) { _idCard = pIDCard; }
        public void setPersonType(string pType) { _type = pType; checkType(); }

        //getters
        public int getPersonID() { return _personID; }
        public string getName() { return _name; }
        public string getIDCard() { return _idCard; }
        public string getPersonType() { return _type; }

    }
}
