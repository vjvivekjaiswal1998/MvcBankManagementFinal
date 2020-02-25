using BankManagement.DTO;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace BankManagement.DAL
{
    public class BankDetailAccess : IBankDetailAccess
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ISqlhelper _sqlhelper;

        public BankDetailAccess(ISqlhelper sqlhelper)
        {
            _sqlhelper = sqlhelper;
            _sqlhelper.EstablishConnection();
            log.Info("connection has been established");
        }
     //  Sqlhelper qlhelper = new Sqlhelper();
        private Boolean boolvalue;
        //static string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        //SqlDataAdapter dataadapter;    
        //SqlConnection bankdataconn;
        //private static DataTable datatable;
        //SqlCommandBuilder sqlcommandbuilder;
    
       

        //public void FillDetail()
        //{
        //    bankdataconn = new SqlConnection(connect);
        //    string query = StringUtilityDAL.sqlSelectQuery;
        //    dataadapter = new SqlDataAdapter(query, bankdataconn);
        //    sqlcommandbuilder = new SqlCommandBuilder(dataadapter);
        //    datatable = new DataTable();
        //    dataadapter.Fill(datatable);
        //}
        public void AddConstraint()
        {
           Sqlhelper.datatable.Constraints.Add("id", Sqlhelper.datatable.Columns[1], true);
            log.Info("Constraint");
        }
        public DataTable ShowBankDetail()
        {
            _sqlhelper.FillDetail();
            try
            {
               
                log.Info("Fill the Details");
                //  FillDetail();
            }
            catch (Exception e)
            {
                Console.WriteLine(StringUtilityDAL.exceptionCaught + e);
            }
            finally
            {
             //   bankdataconn.Close();
            }
            log.Info("Filled data table is been returned");
            return Sqlhelper.datatable;
          
        }
        public DataTable GetSingleAccountDetail(int accountId)
        {
            _sqlhelper.FillDetail();
            log.Info("Fill the Details");
            //FillDetail();
            string query = StringUtilityDAL.sqlSelectSingleQuery + accountId;
            Sqlhelper.dataadapter = new SqlDataAdapter(query, Sqlhelper.bankdataconn);

            Sqlhelper.datatable = new DataTable();

            Sqlhelper.dataadapter.Fill(Sqlhelper.datatable);
            return Sqlhelper.datatable;
        }
        //To Insert The Detail Of Bank To DataBase
        public void SaveBankDetail(BankDetail bankDetail)
        {
            _sqlhelper.FillDetail();
            log.Info("Fill the Details");
            // FillDetail();
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
        public void UpdateBankAccount(int accountid , string CustomerEmail, string CustomerPhoneNumber)
        {
            _sqlhelper.FillDetail();
            log.Info("Fill the Details");
            // FillDetail();
            AddConstraint();
            if (Sqlhelper.datatable.Rows.Contains(accountid))
            {
                DataRow dataRow =Sqlhelper.datatable.Rows.Find(accountid);
             //   Console.WriteLine("record found for:");
                dataRow.BeginEdit();
                dataRow["CustomerEmail"] = CustomerEmail;
                dataRow["CustomerPhoneNumber"] = CustomerPhoneNumber;
                //   Console.Write("Enter the updated email of the customer: ");
                //  dataRow["CustomerEmail"] = Console.ReadLine();
                //  Console.WriteLine("mark record as updated");
                dataRow.EndEdit();
                Sqlhelper.dataadapter.Update(Sqlhelper.datatable);
           //     Console.WriteLine("Record has been updated Succesfully");
            }
        }

        public Boolean DeleteBankAccount(int accountId)
        {
            _sqlhelper.FillDetail();
            log.Info("Fill the Details");
            //FillDetail();
            boolvalue = false;
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
                boolvalue = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("execption caught" + e);
            }
        
            return boolvalue;
        }
        
             public void SaveBankDetailFromFile(BankDetail bankDetail)
        {
            _sqlhelper.FillDetail();
            log.Info("Fill the Details");
            _sqlhelper.SaveBankDetailFromFile(bankDetail);
        }
    }
}
