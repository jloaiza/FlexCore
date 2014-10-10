using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;

namespace FlexCoreLogic.clients
{
    class PersonAddressLogic
    {
        public void insert(PersonAddressDTO pPerson)
        {
            PersonAddressDAO dao = PersonAddressDAO.getInstance();
            dao.insert(pPerson);
        }

        public void delete(PersonAddressDTO pPerson)
        {
            PersonAddressDAO dao = PersonAddressDAO.getInstance();
            dao.delete(pPerson);
        }

        public void update(PersonAddressDTO pNewPerson, PersonAddressDTO pPastPerson)
        {
            PersonAddressDAO dao = PersonAddressDAO.getInstance();
            dao.update(pNewPerson, pPastPerson);
        }

        public List<PersonAddressDTO> search(PersonAddressDTO pPerson, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            PersonAddressDAO dao = PersonAddressDAO.getInstance();
            return dao.search(pPerson, pPageNumber, pShowCount, pOrderBy);
        }

        public List<PersonAddressDTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            PersonAddressDAO dao = PersonAddressDAO.getInstance();
            return dao.getAll(pPageNumber, pShowCount, pOrderBy);
        }
    }
}
