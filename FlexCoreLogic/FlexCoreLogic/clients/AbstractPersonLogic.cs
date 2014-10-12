using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.clients;
using FlexCoreDAOs.clients;
using MySql.Data;
using MySql.Data.MySqlClient;
using FlexCoreLogic.exceptions;
using ConexionMySQLServer.ConexionMySql;

namespace FlexCoreLogic.clients
{

    abstract class AbstractPersonLogic<DTO>
    {

        public virtual void insert(DTO pPerson)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                insert(pPerson, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public virtual void delete(DTO pPerson)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                delete(pPerson, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public virtual void update(DTO pNewPerson, DTO pPastPerson)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                update(pNewPerson, pPastPerson, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public virtual List<DTO> search(DTO pPerson, int pPageNumber, int pShowCount, params string[] pOrderBy)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                return search(pPerson, command, pPageNumber, pShowCount, pOrderBy);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }


        public abstract void insert(DTO pPerson, MySqlCommand pCommand);

        public abstract void delete(DTO pPerson, MySqlCommand pCommand);

        public abstract void update(DTO pNewPerson, DTO pPastPerson, MySqlCommand pCommand);

        public abstract List<DTO> search(DTO pPerson, MySqlCommand pCommand, int pPageNumber, int pShowCount, params string[] pOrderBy);

        public abstract List<DTO> getAll(int pPageNumber, int pShowCount, params string[] pOrderBy);


        //Address

        public void addAddress(List<PersonAddressDTO> pAddresses)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                addAddress(pAddresses, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

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
                throw new InsertException();
            }
        }

        public void deleteAddress(List<PersonAddressDTO> pAddresses)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                deleteAddress(pAddresses, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void deleteAddress(List<PersonAddressDTO> pAddresses, MySqlCommand pCommand)
        {
            try
            {
                PersonAddressDAO dao = PersonAddressDAO.getInstance();
                foreach (PersonAddressDTO address in pAddresses)
                {
                    dao.delete(address, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new DeleteException();
            }
        }

        //Photo

        public void updatePhoto(PersonPhotoDTO pPhoto)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                updatePhoto(pPhoto, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void updatePhoto(PersonPhotoDTO pPhoto, MySqlCommand pCommand)
        {
            try
            {
                PersonPhotoDAO dao = PersonPhotoDAO.getInstance();
                PersonPhotoDTO result = dao.search(pPhoto, pCommand)[0];
                if (result != null)
                {
                    dao.update(pPhoto, pPhoto, pCommand);
                }
                else
                {
                    dao.insert(pPhoto, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }

        //Phone

        public void addPhone(List<PersonPhoneDTO> pPhones)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                addPhone(pPhones, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void addPhone(List<PersonPhoneDTO> pPhones, MySqlCommand pCommand)
        {
            try
            {
                PersonPhoneDAO dao = PersonPhoneDAO.getInstance();
                foreach (PersonPhoneDTO phone in pPhones)
                {
                    dao.insert(phone, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }

        public void deletePhone(List<PersonPhoneDTO> pPhones)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                deletePhone(pPhones, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void deletePhone(List<PersonPhoneDTO> pPhones, MySqlCommand pCommand)
        {
            try
            {
                PersonPhoneDAO dao = PersonPhoneDAO.getInstance();
                foreach (PersonPhoneDTO phone in pPhones)
                {
                    dao.delete(phone, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }

        //Document

        public void addDoc(PersonDocumentDTO pDocument)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                addDoc(pDocument, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void addDoc(PersonDocumentDTO pDocument, MySqlCommand pCommand)
        {
            try
            {
                PersonDocumentDAO dao = PersonDocumentDAO.getInstance();
                PersonDocumentDTO result = dao.search(pDocument)[0];
                if (result != null)
                {
                    dao.update(pDocument, pDocument, pCommand);
                }
                else
                {
                    dao.insert(pDocument, pCommand);
                }
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }

        public void deleteDoc(PersonDocumentDTO pDocument)
        {
            MySqlConnection con = MySQLManager.nuevaConexion();
            MySqlCommand command = new MySqlCommand();
            command.Connection = con;
            try
            {
                deleteDoc(pDocument, command);
            }
            finally
            {
                MySQLManager.cerrarConexion(con);
            }
        }

        public void deleteDoc(PersonDocumentDTO pDocument, MySqlCommand pCommand)
        {
            try
            {
                PersonDocumentDAO dao = PersonDocumentDAO.getInstance();
                dao.delete(pDocument, pCommand);
            }
            catch (MySqlException e)
            {
                throw new UpdateException();
            }
        }

    }
}
