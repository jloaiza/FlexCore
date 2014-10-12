using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;
using FlexCoreLogic.exceptions;

namespace FlexCoreLogic.clients
{
    class ClientLogic
    {

        private static ClientLogic _instance = null;
        private static object _syncLock = new object();

        public static ClientLogic  getInstance(){
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ClientLogic();
                    }
                }
            }
            return _instance;
        }

        private ClientLogic() { }

        public void newClient(PersonDTO pPerson, List<PersonAddressDTO> pAddresses=null, List<PersonPhoneDTO> pPhones=null, List<PersonDocumentDTO> pDocuments=null, PersonPhotoDTO pPhoto=null)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            MySqlTransaction tran = con.BeginTransaction();
            try
            {
                this.insert(pPerson, command);
                PersonLogic perLogic = PersonLogic.getInstance();
                if (pAddresses != null)
                {
                    perLogic.addAddress(pAddresses, command);
                }
                if (pPhones != null){
                    perLogic.addPhone(pPhones, command);
                }
                if (pDocuments != null)
                {
                    perLogic.addDoc(pDocuments, command);
                }
                if (pPhoto != null)
                {
                    perLogic.updatePhoto(pPhoto, command);
                }
                tran.Commit();
            }
            catch (MySqlException e)
            {
                tran.Rollback();
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public bool isActive(ClientDTO pClient)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                return isActive(pClient, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public bool isActive(ClientDTO pClient, MySqlCommand pCommand)
        {
            try
            {
                ClientDAO dao = ClientDAO.getInstance();
                ClientDTO result = dao.search(pClient, pCommand)[0];
                return result.isActive();
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
            
        }

        public void setActive(ClientDTO pClient)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                setActive(pClient, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void setActive(ClientDTO pClient, MySqlCommand pCommand)
        {
            try
            {
                ClientDAO dao = ClientDAO.getInstance();
                dao.setActive(pClient, pCommand);
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }

        public void insert(PersonDTO pPerson)
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

        public void insert(PersonDTO pPerson, MySqlCommand pCommand)
        {
            try
            {
                if (!PersonLogic.getInstance().exists(pPerson, pCommand))
                {
                    if (pPerson.getPersonType() == PersonDTO.JURIDIC_PERSON)
                    {
                        JuridicPersonLogic.getInstance().insert(pPerson, pCommand);
                    }
                    else
                    {
                        PhysicalPersonLogic.getInstance().insert((PhysicalPersonDTO)pPerson, pCommand);
                    }
                }
                ClientDAO clientDAO = ClientDAO.getInstance();
                ClientDTO client = new ClientDTO();
                client.setClientID(pPerson.getPersonID());
                client.setCIF(generarCIF());
                client.setActive(true);
                clientDAO.insert(client, pCommand);
            }
            catch (MySqlException e)
            {
                throw new InsertException();
            }
        }

        public void delete(ClientDTO pClient)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                delete(pClient, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void delete(ClientDTO pClient, MySqlCommand pCommand)
        {
            try
            {
                ClientDAO dao = ClientDAO.getInstance();
                dao.delete(pClient, pCommand);
            }
            catch (MySqlException e)
            {
                throw new DeleteException();
            }
            
        }

        public List<ClientDTO> search(ClientDTO pClient, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                return search(pClient, command, pPageNumber, pShowCount, pOrderBy);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public List<ClientDTO> search(ClientDTO pClient, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                ClientDAO dao = ClientDAO.getInstance();
                return dao.search(pClient, pCommand, pPageNumber, pShowCount, pOrderBy);
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
            
        }

        public List<ClientDTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            try
            {
                ClientDAO dao = ClientDAO.getInstance();
                return dao.getAll(pPageNumber, pShowCount, pOrderBy);
            }
            catch (MySqlException e)
            {
                throw new SearchException();
            }
        }

        private static string generarCIFAux()
        {
            string _CIF = "";
            int _semilla = (int)DateTime.Now.Millisecond;
            Random _random = new Random(_semilla);
            for (int i = 0; i < 10; i++)
            {
                int _numero = _random.Next(0, 10);
                _CIF = _CIF + Convert.ToString(_numero);
                System.Threading.Thread.Sleep(1);
            }
            string _CIFAux = new string(_CIF.ToCharArray().OrderBy(s => (_random.Next(2) % 2) == 0).ToArray());
            return _CIFAux;
        }

        public static string generarCIF()
        {
            string CIF = "";
            bool generate = true;
            ClientDTO dummy = new ClientDTO();
            while (generate)
            {
                CIF = generarCIFAux();
                dummy.setCIF(CIF);
                ClientDTO result = ClientDAO.getInstance().search(dummy)[0];
                if (result == null) { generate = false; }
            }
            return CIF;
        }
    }
}
