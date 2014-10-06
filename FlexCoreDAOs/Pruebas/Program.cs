using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDAOs.clients;
using FlexCoreDTOs.clients;

namespace Pruebas
{
    class Program
    {
        static void Main(string[] args)
        {
            //PersonDAO personDAO = new PersonDAO();
            //PersonDTO person = new PersonDTO(DTOConstants.DEFAULT_INT_ID, "%Joseph%", "", "");
            //personDAO.insert(person);

            //ClientDAO cd = new ClientDAO();
            //ClientDTO c = new ClientDTO(1, "cif1", true);
            //cd.getAll();

            JuridicalClientViewDAO jcv = new JuridicalClientViewDAO();
            jcv.getAll();

            //List<PersonDTO> results = personDAO.search(person);
            //foreach (PersonDTO p in results)
            //{
            //    Console.WriteLine(p.getPersonID());
            //    Console.WriteLine(p.getName());
            //    Console.WriteLine(p.getPersonType());
            //    Console.WriteLine("-----------------");
            //}
            Console.ReadLine();         
        }
    }
}
