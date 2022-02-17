using LougisSP.BO.Exceptions;
using LouigisSP.BO;
using LouigisSP.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LouigisSP.SL
{
    public class PersonOperations
    {

        

        public Person GetPersonByEmailAndPassword(Tuple<string, string> personCredentials)
        {

            if (personCredentials is null || personCredentials.Item1.Length == 0 || personCredentials.Item2.Length == 0)
                throw new InvalidCredentialsException("given credential are null or empty");


            Person obj_person = null;
            IDbDataParameter[] parameters = GetParametersForPerson(personCredentials);
            DataTable dataTable = DBManager.GetDataTable("select * from persons where email=@email and password=@password", CommandType.Text, parameters);
            if (dataTable.Rows.Count <= 0)
                throw new ElementNotFoundException("No user with those credentials were found");

            else
            {

                Console.WriteLine("it was retrieved succesfully");
                DataRow row = dataTable.Rows[0];
                obj_person = new Person();
                ProcssPersonInformation(row, (Person)obj_person);
                return obj_person;
            }




        }


        private IDbDataParameter[] GetParametersForPerson(Tuple<string, string> personCredentials) 
        {
            var emailP = DBManager.CreateParameter("@email", personCredentials.Item1, DbType.String);
            var passP = DBManager.CreateParameter("@password", personCredentials.Item2, DbType.String);
            IDbDataParameter[] parameters = new IDbDataParameter[2];
            parameters[0] = emailP;
            parameters[1] = passP;

            return parameters;
        }

        private void  ProcssPersonInformation(DataRow row,  Person person)
        {
            
            person.Id = (int)row["id"];
            person.FirstName = (string)row["first_name"];
            person.LastName = (string)row["last_name"];
            person.PhoneNumber = (string)row["phone_number"];
            person.Email = (string)row["email"];
            person.Pass = (string)row["password"];
            person.DateOfBirth = (DateTime)row["date_of_birth"];
            person.Role = (int)row["role"];


        }

       


    


        public void InsertPerson(Person person)
        {
            try
            {
                if (person != null)
                {

                    IDbDataParameter[] parameters = new IDbDataParameter[7];
                    parameters[0] = DBManager.CreateParameter("@first_name", person.FirstName, DbType.String);
                    parameters[1] = DBManager.CreateParameter("@last_name", person.LastName, DbType.String);
                    parameters[2] = DBManager.CreateParameter("@phone_number", person.PhoneNumber, DbType.String);
                    parameters[3] = DBManager.CreateParameter("@email", person.Email, DbType.String);
                    parameters[4] = DBManager.CreateParameter("@password", person.Pass, DbType.String);
                    parameters[5] = DBManager.CreateParameter("@date_of_birth", person.DateOfBirth, DbType.DateTime);
                    parameters[6] = DBManager.CreateParameter("@role", person.Role, DbType.Int32);
                    DBManager.Insert("insert into persons(first_name, last_name, phone_number, email, password, date_of_birth, role) values (@first_name, @last_name, @phone_number, @email, @password, @date_of_birth, @role)", commandType: CommandType.Text, parameters);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error while trying to insert to the database");
                throw new DatabaseInsertionException();
            }
        }


        public Person GetPersonById(int id) {
            Person obj_person = null;
            var id_person = DBManager.CreateParameter("@id", id, DbType.Int32);
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            parameters[0] = id_person;
            DataTable dataTable = DBManager.GetDataTable("select * from persons where id=@id", CommandType.Text, parameters);
            if (dataTable.Rows.Count <= 0)
                throw new ElementNotFoundException("User could not be retrieved");

            else
            {

                Console.WriteLine("it was retrieved succesfully");
                DataRow row = dataTable.Rows[0];
                obj_person = new Person();
                ProcssPersonInformation(row, (Person)obj_person);
                return obj_person;
            }
        }

        public Person GetPersonByEmail(string email)
        {
            Person obj_person = null;
            var emailPerson = DBManager.CreateParameter("@email", email, DbType.String);
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            parameters[0] = emailPerson;
            DataTable dataTable = DBManager.GetDataTable("select * from persons where email=@email", CommandType.Text, parameters);
            if (dataTable.Rows.Count <= 0)
                throw new ElementNotFoundException("No user with that id could be retrieved");

            else
            {

                Console.WriteLine("it was retrieved succesfully");
                DataRow row = dataTable.Rows[0];
                obj_person = new Person();
                ProcssPersonInformation(row, (Person)obj_person);
                return obj_person;
            }
        }


        public Person EditPersonUsingId(int id, Person person) {
            if (person == null)
                throw new ArgumentNullException("Two parameters needed");
            if (person.Id!= id) {
                throw new InvalidDataException("Data not valid or does not match with the model");
            }
            try {
                IDbDataParameter[] parameters = new IDbDataParameter[6];
                parameters[0] = DBManager.CreateParameter("@first_name", person.FirstName, DbType.String);
                parameters[1] = DBManager.CreateParameter("@last_name", person.LastName, DbType.String);
                parameters[2] = DBManager.CreateParameter("@phone_number", person.PhoneNumber, DbType.String);
                parameters[3] = DBManager.CreateParameter("@email", person.Email, DbType.String);
                parameters[4] = DBManager.CreateParameter("@password", person.Pass, DbType.String);
                parameters[5] = DBManager.CreateParameter("@date_of_birth", person.DateOfBirth, DbType.DateTime);
                DBManager.Update("update persons set first_name=@first_name, last_name=@last_name, phone_number=@phone_number, email=@email, password=@password, date_of_birth=@date_of_birth", commandType: CommandType.Text, parameters);

                return person;

            } catch (Exception ex) {
                throw ex;
               }



        }


        
        public Address getUseraddress(int idPerson)
        {
            Address obj_address = null;
            var idPersonParameter = DBManager.CreateParameter("@id_person", idPerson, DbType.Int32);
            IDbDataParameter[] parameters = new IDbDataParameter[1];
            parameters[0] = idPersonParameter;
           DataTable addresses= DBManager.GetDataTable("select * from addresses where id_person=@id_person ", CommandType.Text, parameters);
            if (addresses.Rows.Count < 1) throw new ElementNotFoundException ("No address found for person with id "+idPerson);
           
                DataRow row = addresses.Rows[0];
                obj_address = new Address();
                ProcessAddressInformation(row, obj_address);
                return obj_address;
            
        }

        private void ProcessAddressInformation(DataRow row, Address address)
        {
            address.Id = Convert.ToInt32(row["id"]);
            address.Street = Convert.ToString(row["street"]);
            address.Number= Convert.ToInt32(row["number"]);
            address.ZipCode = Convert.ToInt32(row["zip_code"]);
            address.State= Convert.ToString(row["state"]);
            address.City = Convert.ToString(row["city"]);
            address.Country = Convert.ToString(row["country"]);
            address.IdPerson= Convert.ToInt32(row["id_person"]);
          
        }


    }
}
