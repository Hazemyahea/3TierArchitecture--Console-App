using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClsDataBaseLayer;

namespace clsBussnieesLayer
{
     public class ClsContact
    {
        public enum enMode
        {
            addNew=0,
            Update=1
        }
        public enMode Mode = enMode.addNew;
        public int ID
        {
            set;
            get;
        }
        public string FirstName
        {
            set;
            get;
        }
        public string LastName
        {
            set;
            get;
        }
        public string Email
        {
            set;
            get;
        }
        public string Phone
        {
            set;
            get;
        }
        public string Adrees
        {
            set;
            get;
        }
        public DateTime DateOfBirth
        {
            set;
            get;
        }
        public int Countryid
        {
            set;
            get;
        }
      public ClsContact()
        {
            this.FirstName = "";
            this.LastName = "";
            this.Email = "";
            this.Phone = "";
            this.Adrees = "";
            this.Countryid = 1;
            this.Mode = enMode.addNew;
        }
        private ClsContact(int ID, string FirstName, string LastName,
             string Email, string Phone, string Address, int CountryID)

        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Adrees = Address;
            this.Countryid = CountryID;
           this.Mode = enMode.Update;

        }
        static public ClsContact GetContactById(int ID)
        {
            string FirstName = "";
            string LastName = "";
            string Email = "";
            string Phone = "";
            string Adress = "";
            int CountryID = -1;
            if (ClsDataBaseAccsess.getContactById(ID,ref FirstName,ref LastName,ref Email,ref Phone, ref Adress, ref CountryID))
            {
                return new ClsContact(ID, FirstName, LastName, Email, Phone, Adress, CountryID);
            }
            else
            {
                return null;
            }

        }
         private bool _addNewContact()
        {
            this.ID = ClsDataBaseAccsess.addnewContact(this.FirstName, this.LastName, this.Adrees,this.Phone, this.Email, this.Countryid);
            if (this.ID == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool _UpdateContactWhereId()
        {
            if (ClsDataBaseAccsess.UpdateContactWhereID(this.ID,FirstName,LastName,Adrees,Phone,Email,Countryid))
            {
                return true;
            }
            return false;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.addNew:
                    if (_addNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                  return  _UpdateContactWhereId();
            
            }
            return false;
        }

        static public bool DeleteContact(int id)
        {
            if (ClsDataBaseAccsess.DeleteContactByid(id))
            {
                return true;
            }
            return false;
        }
        public static DataTable GetAllContact()
        {
            return ClsDataBaseAccsess.GetAllContacts();
        }
        public static bool isContactExisting(int id)
        {
            return ClsDataBaseAccsess.IsContactExisting(id);
        }
      

    }
    public class ClsCountires
    {
        public enum enMode
        {
            updatedMode = 1,
            AddNew = 2
        }
        public enMode Mode = enMode.AddNew;
        public int CountryID
        {
            get;
            set;
        }
        public string CountryName
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public string PhoneCode
        {
            get;
            set;
        }
        public ClsCountires()
        {
            CountryName = "";
            Code = "";
            PhoneCode = "";


        }
        private ClsCountires(int id,string CountryName,string Code,string PhoneCode)
        {
            this.CountryID = id;
            this.CountryName = CountryName;
            this.PhoneCode = PhoneCode;
            this.Code = Code;
            this.Mode = enMode.updatedMode;
        }
        public static DataTable GetAllCountries()
        {
            return ClsDataBaseAccsess.GetAllCountries();
        }
       public static ClsCountires GetCountryById(int id)
        {
            string CountryName = "";
            string PhoneCode = "";
            string Code = "";
            if (ClsDataBaseAccsess.GetCountryById(id,ref CountryName,ref Code, ref PhoneCode))
            {
                return new ClsCountires(id, CountryName,Code,PhoneCode);
            }
            return null;
        }

        public static ClsCountires GetCountryByCountryName(string CountryName)
        {
            int id =0;
            string PhoneCode = "";
            string Code = "";
            if (ClsDataBaseAccsess.GetCountryByCountryName(ref id, CountryName, ref Code, ref PhoneCode))
            {
                return new ClsCountires(id, CountryName,  Code,  PhoneCode);
            }
            return null;
        }
        public static bool isCountryExisiting(int countryID)
        {
            if (ClsDataBaseAccsess.isCountryExsisting(countryID))
            {
                return true;
            }
            return false;
        }
        private bool _addNewCountry()
        {
            this.CountryID = ClsDataBaseAccsess.addNewCountry(this.CountryName,this.Code,this.PhoneCode);
            if (this.CountryID == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private bool _UpdateCountry()
        {
            if (ClsDataBaseAccsess.UpdateCountryName(this.CountryID, this.CountryName, this.Code, this.PhoneCode)) 
            {
                return true;
            }
            return false;
        }

        static public bool DeleteCountryID(int id)
        {
            if (ClsDataBaseAccsess.DeletCountryByID(id))
            {
                return true;
            }

            return false;
        }
        public bool save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    Mode = enMode.updatedMode;
                    if (_addNewCountry())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.updatedMode:
                    return _UpdateCountry();


            }
            return false;
        }

    }
}
