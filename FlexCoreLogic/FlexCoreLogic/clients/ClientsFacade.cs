using ConexionMySQLServer.ConexionMySql;
using FlexCoreDTOs.clients;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreLogic.clients
{
    public class ClientsFacade
    {
        public void newClient(PersonDTO pPerson, List<PersonAddressDTO> pAddresses=null, List<PersonPhoneDTO> pPhones=null, List<PersonDocumentDTO> pDocuments=null, PersonPhotoDTO pPhoto=null)
        {
            ClientLogic.getInstance().newClient(pPerson, pAddresses, pPhones, pDocuments, pPhoto);
        }

        public void deleteClient(ClientDTO pClient)
        {
            ClientLogic.getInstance().delete(pClient);
        }

        public List<ClientDTO> searchClient(ClientDTO pClient, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            return ClientLogic.getInstance().search(pClient, pPageNumber, pShowCount, pOrderBy);
        }

        public List<ClientDTO> getAllClient(int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            return ClientLogic.getInstance().getAll(pPageNumber, pShowCount, pOrderBy);
        }

        public bool isClientActive(ClientDTO pClient)
        {
            throw new Exception("Not implemented yet.");
        }

        public void setClientActiveStatus(ClientDTO pClient)
        {
            throw new Exception("Not implemented yet.");
        }

        //address
        public void addAddress(List<PersonAddressDTO> pAddresses)
        {
            PersonLogic.getInstance().addAddress(pAddresses);
        }

        public void deleteAddress(List<PersonAddressDTO> pAddresses)
        {
            PersonLogic.getInstance().deleteAddress(pAddresses);
        }

        public List<PersonAddressDTO> getAddress(PersonDTO pPerson)
        {
            return PersonLogic.getInstance().getAddress(pPerson);
        }

        //Photo

        public void updatePhoto(PersonPhotoDTO pPhoto)
        {
            PersonLogic.getInstance().updatePhoto(pPhoto);
        }

        public PersonPhotoDTO getPhoto(PersonDTO pPerson)
        {
            return PersonLogic.getInstance().getPhoto(pPerson);
        }

        //Phone

        public void addPhone(List<PersonPhoneDTO> pPhones)
        {
            PersonLogic.getInstance().addPhone(pPhones);
        }

        public void deletePhone(List<PersonPhoneDTO> pPhones)
        {
            PersonLogic.getInstance().deletePhone(pPhones);
        }

        public List<PersonPhoneDTO> getPhones(PersonDTO pPerson)
        {
            return PersonLogic.getInstance().getPhones(pPerson);
        }

        //Document

        public void addDoc(List<PersonDocumentDTO> pDocuments)
        {
            PersonLogic.getInstance().addDoc(pDocuments);
        }

        public void deleteDoc(List<PersonDocumentDTO> pDocuments)
        {
            PersonLogic.getInstance().deleteDoc(pDocuments);
        }

        public PersonDocumentDTO getCompleteDoc(PersonDocumentDTO pDocumment)
        {
            return PersonLogic.getInstance().getCompleteDoc(pDocumment);
        }

        public List<PersonDocumentDTO> getPartialDoc(PersonDocumentDTO pDocumment)
        {
            throw new Exception("Not implemented yet.");
        }

        //juridical person
        public void newJuridicalPerson(PersonDTO pPerson, MySqlCommand pCommand)
        {
            throw new Exception("Not implemented yet.");
        }

        public void deleteJuridicalPerson(PersonDTO pPerson, MySqlCommand pCommand)
        {
            throw new Exception("Not implemented yet.");
        }

        public void updateJuridicalPerson(PersonDTO pNewPerson, PersonDTO pPastPerson, MySqlCommand pCommand)
        {
            throw new Exception("Not implemented yet.");
        }

        public List<PersonDTO> searchJuridicalPerson(PersonDTO pPerson, MySqlCommand pCommand, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            throw new Exception("Not implemented yet.");
        }

        public List<PersonDTO> getAllJuridicalPerson(int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            throw new Exception("Not implemented yet.");
        }

        //physical person
        public void insertPhysicalPerson(PhysicalPersonDTO pPerson)
        {
            throw new Exception("Not implemented yet.");
        }

        public void deletePhysicalPerson(PhysicalPersonDTO pPerson, MySqlCommand pCommand)
        {
            throw new Exception("Not implemented yet.");
        }

        public void updatePhysicalPerson(PhysicalPersonDTO pNewPerson, PhysicalPersonDTO pPastPerson)
        {
            throw new Exception("Not implemented yet.");
        }

        public List<PhysicalPersonDTO> searchPhysicalPerson(PhysicalPersonDTO pPerson, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            throw new Exception("Not implemented yet.");
        }

        public List<PhysicalPersonDTO> getAllPhysicalPerson(int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            throw new Exception("Not implemented yet.");
        }
    }
}
