using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;
using ConexionMySQLServer.ConexionMySql;
using MySql.Data.MySqlClient;

namespace FlexCoreLogic.clients
{
    class ClientLogic
    {
        public void insert(PersonDTO pPerson)
        {
            ClientDAO clientDAO = ClientDAO.getInstance();
            ClientDTO client = new ClientDTO();
            client.setClientID(pPerson.getPersonID());
            client.setCIF(generarCIF());
            client.setActive(true);
            clientDAO.insert(client);
        }

        public void delete(ClientDTO pClient)
        {
            ClientDAO dao = ClientDAO.getInstance();
            dao.delete(pClient);
        }

        public void update(ClientDTO pNewClient, ClientDTO pPastClient)
        {
            ClientDAO dao = ClientDAO.getInstance();
            dao.update(pNewClient, pPastClient);
        }

        public List<ClientDTO> search(ClientDTO pClient, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            ClientDAO dao = ClientDAO.getInstance();
            return dao.search(pClient, pPageNumber, pShowCount, pOrderBy);
        }

        public List<ClientDTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            ClientDAO dao = ClientDAO.getInstance();
            return dao.getAll(pPageNumber, pShowCount, pOrderBy);
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
