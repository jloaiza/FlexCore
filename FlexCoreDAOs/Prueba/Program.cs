using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;

namespace Prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            //PersonDTO person = new PersonDTO("Joseph", "201293834", PersonDTO.JURIDIC_PERSON);
            //PersonDAO.getInstance().insert(person);

            //ClientDTO client = new ClientDTO(1, "sddsfdsf", true);
            //ClientDAO.getInstance().insert(client);
            

            ClientDTO client = new ClientDTO(1);


            ClientDAO.getInstance().search(client);

            Console.ReadLine();
        }
    }
}
