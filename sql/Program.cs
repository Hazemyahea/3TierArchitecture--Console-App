using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Linq.Expressions;
using clsBussnieesLayer;
using System.Data;

public class Program
{
  static void FindContactById(int id)
    {
        ClsContact contact1 = ClsContact.GetContactById(id);
        if (contact1 != null)
        {
            Console.WriteLine(contact1.FirstName + " " + contact1.LastName + " " + contact1.Email + contact1.Mode);
           
        }
        else
        {
            Console.WriteLine(id + "Not Found!");
        }
    }
    static void AddNewContact()
    {
        ClsContact NewConatct = new ClsContact();
        NewConatct.FirstName = "Zenab";
        NewConatct.LastName = "Mohamed Youssef";
        NewConatct.Phone = "0112313";
        NewConatct.Adrees = "Aswan";
        NewConatct.Email = "ZenabY@c.o";
        NewConatct.Countryid = 1;
        if (NewConatct.Save())
        {
            Console.WriteLine("Saved! id is {0}", NewConatct.ID);
        }
        else
        {
            Console.WriteLine("There is Some Thing Wrong!");

        }
    }
    static void UpdateContact(int id)
    {
        ClsContact NewConatct = ClsContact.GetContactById(id);
        if (NewConatct != null)
        {
            NewConatct.FirstName = "Dorar";
            NewConatct.LastName = "Mohamed Youssef";
            NewConatct.Phone = "0112313";
            NewConatct.Adrees = "JJ";
            NewConatct.Email = "Dorar@c.o";
            NewConatct.Countryid = 1;
            if (NewConatct.Save())
            {
                Console.WriteLine("Contact Updaeted Sucsfully!");
            }
            else
            {
                Console.WriteLine("There is Some Thing Wrong!");

            }
        }
    }
    static void DeleteContactByID(int id)
    {
        if (ClsContact.DeleteContact(id))
        {
            Console.WriteLine("Deleted Done");
        }
        else
        {
            Console.WriteLine("Deleted Not  Done");
        }
    }
    static void ListContact()
    {
        DataTable table = ClsContact.GetAllContact();
        Console.WriteLine("CONTACTS DATA: ");
        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine(row["ContactID"]+ " "+ row["FirstName"] + " " + row["LastName"]);
        }
    }
    static void isContactExisting(int id)
    {
        if (ClsContact.isContactExisting(id))
        {
            Console.WriteLine("Contact existing");
        }
        else
        {
            Console.WriteLine("Contact Is Not existing");

        }
    }
    static void ListCountries()
    {
        DataTable table = ClsCountires.GetAllCountries();
        Console.WriteLine("Coutries DATA: ");

        foreach (DataRow row in table.Rows)
        {
            Console.WriteLine(row["CountryID"] + " " + row["CountryName"] + " " + row["Code"] + " " + row["PhoneCode"]);
        }
        
    }
    static void GetCountrybyID(int id)
    {
        ClsCountires Country1 = ClsCountires.GetCountryById(id);
        if (Country1 !=null)
        {
            Console.WriteLine("id" + Country1.CountryID + "CountryName: " + Country1.CountryName + "Code: " + Country1.Code + " "+"PhoneCode: "+ Country1.PhoneCode);
        }
        else
        {
            Console.WriteLine("No");
        }
    }
    static void GetCountryByCountryName(string CountryName)
    {
        ClsCountires Country1 = ClsCountires.GetCountryByCountryName(CountryName);
        if (Country1 != null)
        {
            Console.WriteLine("id" + Country1.CountryID + "CountryName: " + Country1.CountryName + "Code: " + Country1.Code + " " + "PhoneCode: " + Country1.PhoneCode);

        }
        else
        {
            Console.WriteLine("No");
        }
    }
    static void isCountryExisitng(int id)
    {
        if (ClsCountires.isCountryExisiting(id))
        {
            Console.WriteLine("YES");
        }
    }
    static void AddNewCountry(string CountryName,string Code,string PhoneCode)
    {
        ClsCountires Country1 = new ClsCountires();
        Country1.CountryName = CountryName;
        Country1.Code = Code;
        Country1.PhoneCode = PhoneCode;
        
        if (Country1.save())
        {
            Console.WriteLine("Added Successfully!");
        }
        else
        {
            Console.WriteLine("Not Added!");
        }
    }
    static void UpdateCountry(int id,string CountryName, string Code, string PhoneCode)
    {
        if (ClsCountires.isCountryExisiting(id))
        {
            ClsCountires Country = ClsCountires.GetCountryById(id);
            Country.CountryName = CountryName;
            Country.Code = Code;
            Country.PhoneCode = PhoneCode;
            if (Country.save())
            {
                Console.WriteLine("Updated Successfully!");
            }
            else
            {
                Console.WriteLine("Updated NOT Successfully!");
            }
        }
        else
        {
            Console.WriteLine("ID Not Found!");
        }
    }

    public static void DeleteCountryByID(int id)
    {
        if (ClsCountires.DeleteCountryID(id))
        {
            Console.WriteLine("Deleted Successfully!");
        }
        else
        {
            Console.WriteLine("Not Delete !");
        }
    }
    public static void Main()
    {

        AddNewCountry("Ukrania", "00", "+00");
    
         
        Console.ReadKey();
    }
}