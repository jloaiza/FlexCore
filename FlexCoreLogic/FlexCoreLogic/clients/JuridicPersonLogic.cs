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
using ConexionMySQLServer.ConexionMySql;

namespace FlexCoreLogic.clients
{

    class JuridicPersonLogic:AbstractPersonLogic<PersonDTO>
    {

        private static JuridicPersonLogic _instance = null;
        private static object _syncLock = new object();

        public JuridicPersonLogic  getInstance(){
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new JuridicPersonLogic();
                    }
                }
            }
            return _instance;
        }

        private JuridicPersonLogic() { }

        public override void insert(PersonDTO pPerson, MySqlCommand pCommand)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                dao.insert(pPerson, pCommand);
            }
            catch (MySqlException e)
            {
                throw new InsertException();
            }
        }

        public override void delete(PersonDTO pPerson, MySqlCommand pCommand)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                dao.delete(pPerson, pCommand);
            }
            catch (MySqlException e)
            {
                throw new DeleteException();
            }
                
        }

        public override void update(PersonDTO pNewPerson, PersonDTO pPastPerson, MySqlCommand pCommand)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                dao.update(pNewPerson, pPastPerson, pCommand);
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
            
        }

        public override List<PersonDTO> search(PersonDTO pPerson, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                return dao.searchJuridical(pPerson, pCommand, pPageNumber, pShowCount, pOrderBy);
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
        }

        public override List<PersonDTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                PersonDAO dao = PersonDAO.getInstance();
                return dao.getAllJuridical(pPageNumber, pShowCount, pOrderBy);
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
        }
    }
}
