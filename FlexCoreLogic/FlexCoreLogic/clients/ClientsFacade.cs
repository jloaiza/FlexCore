using ConexionMySQLServer.ConexionMySql;
using FlexCoreDAOs.clients;
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

        private static ClientsFacade _instance = null;
        private static object _syncLock = new object();

        public static ClientsFacade getInstance()
        {
            if (_instance == null)
            {
                lock (_syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new ClientsFacade();
                    }
                }
            }
            return _instance;
        }

        private ClientsFacade();

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
            return ClientLogic.getInstance().isActive(pClient);
        }

        public void setClientActiveStatus(ClientDTO pClient)
        {
            ClientLogic.getInstance().setActive(pClient);
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

        public List<PersonDocumentDTO> getPartialDoc(PersonDocumentDTO pDocumment, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            return PersonLogic.getInstance().getPartialDoc(pDocumment, pPageNumber, pShowCount, pOrderBy);
        }

        //juridical person
        public void newJuridicalPerson(PersonDTO pPerson, MySqlCommand pCommand)
        {
            JuridicPersonLogic.getInstance().insert(pPerson);
        }

        public void deleteJuridicalPerson(PersonDTO pPerson, MySqlCommand pCommand)
        {
            JuridicPersonLogic.getInstance().delete(pPerson);
        }

        public void updateJuridicalPerson(PersonDTO pNewPerson, PersonDTO pPastPerson)
        {
            JuridicPersonLogic.getInstance().update(pNewPerson, pPastPerson);
        }

        public List<PersonDTO> searchJuridicalPerson(PersonDTO pPerson, int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            return JuridicPersonLogic.getInstance().search(pPerson, pPageNumber, pShowCount, pOrderBy);
        }

        public List<PersonDTO> getAllJuridicalPerson(int pPageNumber = 0, int pShowCount = 0, params string[] pOrderBy)
        {
            return JuridicPersonLogic.getInstance().getAll(pPageNumber, pShowCount, pOrderBy);
        }

        //physical person
        public void insertPhysicalPerson(PhysicalPersonDTO pPerson)
        {
            PhysicalPersonLogic.getInstance().insert(pPerson);
        }

        public void deletePhysicalPerson(PhysicalPersonDTO pPerson)
        {
            PhysicalPersonLogic.getInstance().delete(pPerson);
        }

        public void updatePhysicalPerson(PhysicalPersonDTO pNewPerson, PhysicalPersonDTO pPastPerson)
        {
            PhysicalPersonLogic.getInstance().update(pNewPerson, pPastPerson);
        }

        public List<PhysicalPersonDTO> searchPhysicalPerson(PhysicalPersonDTO pPerson, MySqlCommand pCommand, int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            return PhysicalPersonLogic.getInstance().search(pPerson, pPageNumber, pShowCount, pOrderBy);
        }

        public List<PhysicalPersonDTO> getAllPhysicalPerson(int pPageNumber=0, int pShowCount=0, params string[] pOrderBy)
        {
            return PhysicalPersonLogic.getInstance().getAll(pPageNumber, pShowCount, pOrderBy);
        }
    }
}
