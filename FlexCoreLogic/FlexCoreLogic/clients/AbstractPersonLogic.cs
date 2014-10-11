using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.clients;
using FlexCoreDAOs.clients;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace FlexCoreLogic.clients
{

    abstract class AbstractPersonLogic<DTO>
    {

        public abstract void insert(DTO pPerson, MySqlCommand pCommand);

        public abstract void delete(DTO pPerson, MySqlCommand pCommand);

        public abstract void update(DTO pPerson, MySqlCommand pCommand);

        public abstract List<DTO> search(DTO pPerson, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy);

        public abstract List<DTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy);

        public void addAddress(List<PersonAddressDTO> pAddresses, MySqlCommand pCommand){
            try
            {
                PersonAddressDAO dao = PersonAddressDAO.getInstance();
                foreach (PersonAddressDTO address in pAddresses)
                {
                    dao.insert(address, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw 
            }
                
        }

    }
}
