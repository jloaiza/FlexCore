using FlexCoreDTOs.clients;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreLogic.clients
{
    class PersonLogic:AbstractPersonLogic<PersonDTO>
    {
        private static PersonLogic _instance = null;
        private static object _syncLock = new object();

        public static PersonLogic getInstance(){
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

        public override void insert(PersonDTO pPerson, MySqlCommand pCommand)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }

        public override void delete(PersonDTO pPerson, MySqlCommand pCommand)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }

        public override void update(PersonDTO pNewPerson, PersonDTO pPastPerson, MySqlCommand pCommand)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }

        public override List<PersonDTO> search(PersonDTO pPerson, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }

        public override List<PersonDTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            throw new Exception("For this operation use specialized person type child classes of overridePersonLogic, this method is not implemented");
        }
    }
}
