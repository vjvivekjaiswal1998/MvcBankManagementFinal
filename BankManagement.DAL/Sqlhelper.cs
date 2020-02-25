using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;
using System.Configuration;
using BankManagement.DTO;

namespace BankManagement.DAL
{
    public class Sqlhelper : ISqlhelper
    {
        ////   SqlConnection bankdataconn = new SqlConnection(@"Data Source=CS68-PC\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");

        //public void OpenDataBaseConnection()
        //{
        //    bankdataconn.Open();
        //}
        //public void CloseDataBaseConnection()
        //{
        //    bankdataconn.Close();
        //}
        public static SqlDataAdapter dataadapter;
        public static SqlConnection bankdataconn;
        public static DataTable datatable;
        public static SqlCommandBuilder sqlcommandbuilder;

        public void EstablishConnection()
        {
             bankdataconn = new SqlConnection(@"Data Source=CS68-PC\SQLEXPRESS;Initial Catalog=Bank;Integrated Security=True");
            //string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
           // bankdataconn = new SqlConnection(connect);
        }

        public void AddConstraint()
        {
            Sqlhelper.datatable.Constraints.Add("id", Sqlhelper.datatable.Columns[1], true);
        }

        public DataTable FillDetail()
        {
            //  bankdataconn = new SqlConnection(connect);
            string query = StringUtilityDAL.sqlSelectQuery;
            dataadapter = new SqlDataAdapter(query, bankdataconn);
            sqlcommandbuilder = new SqlCommandBuilder(dataadapter);
            datatable = new DataTable();
            dataadapter.Fill(datatable);
            return datatable;
        }


        public void SaveBankDetail(BankDetail bankDetail)
        {
            try
            {
                // code for disconnected architecture

                AddConstraint();
                DataRow dataRow = Sqlhelper.datatable.NewRow();

                dataRow[1] = bankDetail.AccountNumber;
                dataRow[2] = bankDetail.AccountType;
                dataRow[3] = bankDetail.CustomerName;
                dataRow[4] = bankDetail.CustomerAddress;
                dataRow[5] = bankDetail.CustomerEmail;
                dataRow[6] = bankDetail.CustomerPhoneNumber;
                dataRow[7] = bankDetail.NomieeName;

                Sqlhelper.datatable.Rows.Add(dataRow);
                Sqlhelper.dataadapter.Update(Sqlhelper.datatable);
                Console.WriteLine(StringUtilityDAL.accountDetailsAddedSuccessfully);
            }
            catch (Exception e)
            {
                Console.WriteLine(StringUtilityDAL.exceptionCaught + e);
            }
            finally
            {
                //   bankdataconn.Close();
            }

        }

        public void UpdateBankAccount(int accountid)
        {
            AddConstraint();
            if (Sqlhelper.datatable.Rows.Contains(accountid))
            {
                DataRow dataRow = Sqlhelper.datatable.Rows.Find(accountid);
                Console.WriteLine("record found for:");
                dataRow.BeginEdit();
                Console.Write("Enter the updated email of the customer: ");
                dataRow["CustomerEmail"] = Console.ReadLine();
                Console.Write("Enter the updated PHONE of the customer: ");
                dataRow["customerPhoneNumber"] = Console.ReadLine();

                Console.WriteLine("mark record as updated");
                dataRow.EndEdit();
                Sqlhelper.dataadapter.Update(Sqlhelper.datatable);
                Console.WriteLine("Record has been updated Succesfully");
            }
        }

        public void DeleteBankAccount(int accountId)
        {
            try
            {
                Console.WriteLine("Find And Delete");
                AddConstraint();

                if (!Sqlhelper.datatable.Rows.Contains(accountId))
                {
                    Console.WriteLine("NO records found");
                }
                else
                {
                    DataRow dataRow = Sqlhelper.datatable.Rows.Find(accountId);
                    Console.WriteLine("record found for:" + dataRow[1] + dataRow[2]);
                    dataRow.Delete();
                    Console.WriteLine("mark record as deleted");
                    Sqlhelper.dataadapter.Update(Sqlhelper.datatable);
                    Console.WriteLine("Record deleted");
                }
                //boolvalue = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("execption caught" + e);
            }
        }
        public void SaveBankDetailFromFile(BankDetail bankDetail)
        {
            try
            {

                AddConstraint();
                DataRow dataRow = Sqlhelper.datatable.NewRow();

                dataRow[1] = bankDetail.AccountNumber;
                dataRow[2] = bankDetail.AccountType;
                dataRow[3] = bankDetail.CustomerName;
                dataRow[4] = bankDetail.CustomerAddress;
                dataRow[5] = bankDetail.CustomerEmail;
                dataRow[6] = bankDetail.CustomerPhoneNumber;
                dataRow[7] = bankDetail.NomieeName;

                Sqlhelper.datatable.Rows.Add(dataRow);
                Sqlhelper.dataadapter.Update(Sqlhelper.datatable);
                Console.WriteLine(StringUtilityDAL.accountDetailsAddedSuccessfully);

         
            }
            catch (Exception e)
            {
                Console.WriteLine(StringUtilityDAL.exceptionCaught + e);
            }
            finally
            {
                //   bankdataconn.Close();
            }

        }
    }
}
