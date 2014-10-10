using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;

namespace FlexCoreLogic.clients
{
    class PersonPhotoLogic
    {
        public void insert(PersonPhotoDTO pPerson)
        {
            PersonPhotoDAO dao = PersonPhotoDAO.getInstance();
            dao.insert(pPerson);
        }

        public void delete(PersonPhotoDTO pPerson)
        {
            PersonPhotoDAO dao = PersonPhotoDAO.getInstance();
            dao.delete(pPerson);
        }

        public void update(PersonPhotoDTO pNewPerson, PersonPhotoDTO pPastPerson)
        {
            PersonPhotoDAO dao = PersonPhotoDAO.getInstance();
            dao.update(pNewPerson, pPastPerson);
        }

        public List<PersonPhotoDTO> search(PersonPhotoDTO pPerson, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            PersonPhotoDAO dao = PersonPhotoDAO.getInstance();
            return dao.search(pPerson, pPageNumber, pShowCount, pOrderBy);
        }

        public List<PersonPhotoDTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            PersonPhotoDAO dao = PersonPhotoDAO.getInstance();
            return dao.getAll(pPageNumber, pShowCount, pOrderBy);
        }
    }
}
