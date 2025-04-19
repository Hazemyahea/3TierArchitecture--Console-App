using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using System.Data.SqlClient;
using System.Security.Policy;
using System.Net;
using System.Data;
using System.Data.Common;
using System.Runtime.Remoting.Messaging;


namespace ClsDataBaseLayer
{
    static class ConnectionDB
    {
        static public string ConnectionString = "Server=.;Database=ContactsDB;User Id=sa;Password=12345";

    }
    public class ClsDataBaseAccsess
    {
        static public bool getContactById(int id,ref string firstname,ref string lastName,ref string Email,ref string Phone,ref string adrees,ref int CountryID)
        {
            bool isFind = false;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);
            string query = "select * from dbo.Contacts where ContactID = @id";
            SqlCommand command = new SqlCommand(query,connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                 SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFind = true;
                    firstname = (string)reader["FirstName"];
                    lastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    adrees = (string)reader["Address"];
                    CountryID = (int)reader["CountryID"];
                        
                }
                else
                {
                    isFind = false;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                isFind = false;
            }
            finally
            {
                connection.Close();
            }
            return isFind;
            
        }
        static public int addnewContact(string FirstName, string LastName, string Address, string Phone, string Email, int Countryid)
        {
            int contactId = -1;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);

            string query = @"
        INSERT INTO Contacts (FirstName, LastName, Address, Email, Phone, CountryID)
        VALUES (@FirstName, @LastName, @Address, @Email, @Phone, @Countryid);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Countryid", Countryid);

            try
            {
                connection.Open();
                contactId = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return contactId;
        }

        static public bool UpdateContactWhereID(int ContactID, string FirstName, string LastName, string Address, string Phone, string Email, int Countryid)
        {

            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);
            string query = @"
        UPDATE Contacts
        SET FirstName = @FirstName,
            LastName = @LastName,
            Address = @Address,
            Email = @Email,
            Phone = @Phone,
            CountryID = @Countryid
        WHERE ContactID = @ContactID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Countryid", Countryid);
            try
            {
                connection.Open();
                 rowAffected = command.ExecuteNonQuery();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return (rowAffected > 0);
        }

        static public bool DeleteContactByid(int ContactID)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);

            string query = "DELETE FROM Contacts WHERE ContactID = @ContactID;";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isDeleted = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return isDeleted;
        }
        public static DataTable GetAllContacts()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);
            string query = "SELECT * FROM dbo.Contacts";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                connection.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool IsContactExisting(int ContactID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);

            string query = "select x=1 from dbo.Contacts where ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
    static public DataTable GetAllCountries()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);
            string query = "Select * from dbo.Countries";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;
        }

        public static bool GetCountryById(int  id , ref string CountryName,ref string Code,ref string PhoneCode)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);
            string query = "SELECT * FROM dbo.Countries where CountryID = @id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                if (reader.Read())
                {
                    isFound = true;
                    CountryName = (string)reader["CountryName"];
                    Code = (string)reader["Code"];
                    PhoneCode = (string)reader["PhoneCode"];
                }
                reader.Close();

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                
                connection.Close();
            }
            return isFound;
        }

        public static bool GetCountryByCountryName(ref int id,  string CountryName,ref string Code,ref string PhoneCode)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);
            string query = "SELECT * FROM dbo.Countries where CountryName = @CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    id = (int)reader["CountryID"];
                    Code = (string)reader["Code"];
                    PhoneCode = (string)reader["PhoneCode"];
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {

                connection.Close();
            }
            return isFound;
        }
      public static  bool isCountryExsisting(int CountryID) {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);
            string query = "select x=1 from dbo.Countries  where CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("CountryID", CountryID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();

                
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return isFound;

        }

        public static int addNewCountry(string CountryName,string Code,string PhoneCode)
        {
            int CountryID = -1;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);
            string query = @"
        INSERT INTO dbo.Countries (CountryName, Code, PhoneCode) 
        VALUES (@CountryName, @Code, @PhoneCode);
        SELECT CAST(SCOPE_IDENTITY() AS INT);";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            if (Code != "")
            {
                command.Parameters.AddWithValue("@Code", Code);
            }
            else
            {
                command.Parameters.AddWithValue("@Code", System.DBNull.Value);

            }
            if (PhoneCode != "")
            {
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            }
            else
            {
                command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);

            }
            try
            {
                connection.Open();
                CountryID = (int)command.ExecuteScalar();

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return CountryID;
        }

        public static bool UpdateCountryName(int CountryID,string CountryName,string Code,string PhoneCode)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);
            string query = @"UPDATE dbo.Countries 
                     SET CountryName = @CountryName, 
                         Code = @Code, 
                         PhoneCode = @PhoneCode 
                     WHERE CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            if (Code != "")
            {
                command.Parameters.AddWithValue("@Code", Code);
            }
            else
            {
                command.Parameters.AddWithValue("@Code", System.DBNull.Value);

            }
            if (Code != "")
            {
                command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
            }
            else
            {
                command.Parameters.AddWithValue("@PhoneCode", System.DBNull.Value);

            }
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                int rowEffected = command.ExecuteNonQuery();
                if (rowEffected >0)
                {
                    IsUpdated = true;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return IsUpdated;
        }


        public static bool DeletCountryByID(int CountryID)
        {
            bool IsDeleted = false;
            SqlConnection connection = new SqlConnection(ConnectionDB.ConnectionString);
            string query = "delete from dbo.Countries where CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                int rowEffected = command.ExecuteNonQuery();
                if (rowEffected > 0)
                {
                    IsDeleted = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return IsDeleted;
        }

    }



}
