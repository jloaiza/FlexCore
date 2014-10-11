using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.clients;
using FlexCoreDAOs.clients;
using MySql.Data;
using MySql.Data.MySqlClient;
using FlexCoreLogic.exceptions;

namespace FlexCoreLogic.clients
{

    class PersonLogic
    {

        private static PersonLogic _instance = null;
        private static object _syncLock = new object();

        public PersonLogic  getInstance(){
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PersonLogic();
                    }
                }
            }
            return _instance;
        }

        private PersonLogic() { }

        public void insert(PersonDTO pPerson)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                dao.insert(pPerson);
            }
            catch (MySqlException e)
            {
                throw new InsertException();
            }
        }

        public void delete(PersonDTO pPerson)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                dao.delete(pPerson);
            }
            catch (MySqlException e)
            {
                throw new DeleteException();
            }
                
        }

        public void update(PersonDTO pNewPerson, PersonDTO pPastPerson)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                dao.update(pNewPerson, pPastPerson);
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
            
        }

        public List<PersonDTO> search(PersonDTO pPerson, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                return dao.search(pPerson, pPageNumber, pShowCount, pOrderBy);
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
        }

        public List<PersonDTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                return dao.getAll(pPageNumber, pShowCount, pOrderBy);
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
        }
    }
}
