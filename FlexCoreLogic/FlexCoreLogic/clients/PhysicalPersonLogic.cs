using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;
using MySql.Data.MySqlClient;
using FlexCoreLogic.exceptions;
using ConexionMySQLServer.ConexionMySql;

namespace FlexCoreLogic.clients
{
    class PhysicalPersonLogic:AbstractPersonLogic<PhysicalPersonDTO>
    {
        private static PhysicalPersonLogic _instance = null;
        private static object _syncLock = new object();

        public static PhysicalPersonLogic  getInstance(){
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new PhysicalPersonLogic();
                    }
                }
            }
            return _instance;
        }

        private PhysicalPersonLogic() { }

        public override void insert(PhysicalPersonDTO pPerson)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            MySqlTransaction tran = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = tran;
            try
            {
                insert(pPerson, command);
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw e;
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public override void insert(PhysicalPersonDTO pPerson, MySqlCommand pCommand)
        {
            try
            {
                PersonDAO perDao = PersonDAO.getInstance();
                PhysicalPersonDAO phyDao = PhysicalPersonDAO.getInstance();
                PersonDTO result = perDao.search(pPerson, pCommand)[0];
                if (result == null)
                {
                    perDao.insert(pPerson, pCommand);
                }
                phyDao.insert(pPerson, pCommand);
            }
            catch (MySqlException e)
            {
                throw new InsertException();
            }
        }

        public override void delete(PhysicalPersonDTO  pPerson, MySqlCommand pCommand)
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

        public override void update(PhysicalPersonDTO pNewPerson, PhysicalPersonDTO pPastPerson)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            MySqlTransaction tran = con.BeginTransaction();
            command.Connection = con;
            command.Transaction = tran;
            try
            {
                update(pNewPerson, pPastPerson, command);
                tran.Commit();
            }
            catch (Exception e)
            {
                tran.Rollback();
                throw e;
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public override void update(PhysicalPersonDTO  pNewPerson, PhysicalPersonDTO  pPastPerson, MySqlCommand pCommand)
        {
            try
            {
                PersonDAO perDao = PersonDAO.getInstance();
                perDao.update(pNewPerson, pPastPerson, pCommand);
                PhysicalPersonDAO phyDao = PhysicalPersonDAO.getInstance();
                phyDao.update(pNewPerson, pPastPerson, pCommand);
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
            
        }

        public override List<PhysicalPersonDTO> search(PhysicalPersonDTO  pPerson, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                PhysicalPersonDAO dao = PhysicalPersonDAO.getInstance();
                return dao.search(pPerson, pCommand, pPageNumber, pShowCount, pOrderBy);
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
        
        }

        public override List<PhysicalPersonDTO > getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                PhysicalPersonDAO dao = PhysicalPersonDAO.getInstance();
                return dao.getAll(pPageNumber, pShowCount, pOrderBy);
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
            
        }
    }
}
