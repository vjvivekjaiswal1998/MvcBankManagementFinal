using BankManagement.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Data;

namespace BankManagement.BLL
{
    public interface IBank
    {
        void DeleteBankDetail(int accountNumber);
        BankDetail  AddBankDetail(List<BankDetail> bankDetailList);
        BankDetail GetSingleBankAccountDetail(int AccountNumber);
        List<BankDetail> ShowAllBankDetail(string order);
        void UpdatebankDetail(int AccountNumber, string CustomerEmail, string CustomerPhoneNumber);
        bool AddBankDataFromFile(IFormFile BankDetails);
    }
}
