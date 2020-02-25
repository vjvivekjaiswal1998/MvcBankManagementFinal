using BankManagement.DTO;
using System;
using System.Data;

namespace BankManagement.DAL
{
    public interface ISqlhelper
    {
        void EstablishConnection();
        DataTable FillDetail();
        void SaveBankDetail(BankDetail bankDetail);
        void UpdateBankAccount(int accountid);
        void DeleteBankAccount(int accountId);
        void SaveBankDetailFromFile(BankDetail bankDetail);
    }
}