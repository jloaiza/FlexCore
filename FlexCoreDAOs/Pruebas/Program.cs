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
            PersonDAO personDAO = PersonDAO.getInstance();
            PersonDTO person = new PersonDTO(DTOConstants.DEFAULT_INT_ID, "Joseph", "567", "Fisica");
            personDAO.insert(person);
            Console.WriteLine(person==null);

            //ClientDAO cd = new ClientDAO();
            //ClientDTO c = new ClientDTO(1, "cif1", true);
            //cd.getAll();

            //JuridicalClientVDAO jcv = JuridicalClientVDAO.getInstance();
            //jcv.getAll(0, 10, JuridicalClientVDAO.NAME);

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
