using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using BankManagement.BLL;
using BankManagement.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace BankWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly IHostingEnvironment _ihostingEnvironment;
        private readonly IBank _bank;
        BankDetail bankDetail = new BankDetail();
        public BankController(IBank bank, IHostingEnvironment ihostingEnvironment)
        {
            _bank = bank;
            _ihostingEnvironment = ihostingEnvironment;
        }
     
        //To Show All Bank Detail.
        [HttpGet]
        public ActionResult<List<string>> ShowAllBankDetail(string order)
        {
            List<BankDetail> bankDetail = _bank.ShowAllBankDetail(order);
            List<string> all = new List<string>();
            foreach (BankDetail bankDetails in bankDetail)
            {
                System.IO.StringWriter writer = new System.IO.StringWriter();
                new XmlSerializer(bankDetail.GetType()).Serialize(writer, bankDetail);
                string xml = writer.ToString();
                all.Add(xml);
            }
            return all;

        }
        // To Show Single Bank Detail.
        [HttpGet("{accountNumber}")]
        public string GetSingleBankDetail(int accountNumber)
        {
            BankDetail bankDetail = _bank.GetSingleBankAccountDetail(accountNumber);
            System.IO.StringWriter writer = new System.IO.StringWriter();
            new XmlSerializer(bankDetail.GetType()).Serialize(writer, bankDetail);
            string xml = writer.ToString();
            return xml;

        }
        //// To Show Single Bank Detail.
        //[HttpGet("{accountNumber}")]
        //public ActionResult<BankDetail> GetSingleBankDetail(int accountNumber)
        //{
        //    BankDetail bankDetail = _bank.GetSingleBankAccountDetail(accountNumber);
        //    return bankDetail;

        //}



        [HttpPost]
        public BankDetail Add([FromBody]List<BankDetail> bankDetailList)
        {
            if (bankDetail != null)
            {
                _bank.AddBankDetail(bankDetailList);
            }
            return bankDetail;
        }
        //To Update The Data In Bank Data Base.
        [HttpPut("{accountNumber}")]
        public string  UpdateBankDetail(int accountNumber, [FromBody]BankDetail bankDetail)
        {
            _bank.UpdatebankDetail(accountNumber,bankDetail.CustomerEmail,bankDetail.CustomerPhoneNumber);
            return "update successfully";
        }
        //To Delete The Data In BanK Data Base.
        [HttpDelete("{accountNumber}")]
        public string DeleteBankDetail(int accountNumber)
        {
            _bank.DeleteBankDetail(accountNumber);

            return "Deleted successfully";
        }
        [HttpPost]
        public string PostFile(IFormFile file)
        {
            string uniquename = null;
            if (file != null)
            {
                string uploadfolder = Path.Combine(_ihostingEnvironment.WebRootPath, "Files");
              //  string uploadfolder = Path.Combine(_ihostingEnvironment.WebRootPath, "Files");
                uniquename = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filepath = Path.Combine(uploadfolder, uniquename);
                file.CopyTo(new FileStream(filepath, FileMode.Create));
                return "File uploadded Succesfully";
            }
            else
            {
                return "File  not uploadded  Succesfully";
            }
        }

    }
}