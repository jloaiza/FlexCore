using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;

namespace FlexCoreLogic.clients
{
    class PhysicalPersonLogic
    {
        public void insert(PhysicalPersonDTO pPerson)
        {
            PhysicalPersonDAO dao = PhysicalPersonDAO.getInstance();
            dao.insert(pPerson);
        }

        public void delete(PhysicalPersonDTO  pPerson)
        {
            PhysicalPersonDAO dao = PhysicalPersonDAO.getInstance();
            dao.delete(pPerson);
        }

        public void update(PhysicalPersonDTO  pNewPerson, PhysicalPersonDTO  pPastPerson)
        {
            PhysicalPersonDAO dao = PhysicalPersonDAO.getInstance();
            dao.update(pNewPerson, pPastPerson);
        }

        public List<PhysicalPersonDTO > search(PhysicalPersonDTO  pPerson, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            PhysicalPersonDAO dao = PhysicalPersonDAO.getInstance();
            return dao.search(pPerson, pPageNumber, pShowCount, pOrderBy);
        }

        public List<PhysicalPersonDTO > getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            PhysicalPersonDAO dao = PhysicalPersonDAO.getInstance();
            return dao.getAll(pPageNumber, pShowCount, pOrderBy);
        }
    }
}
