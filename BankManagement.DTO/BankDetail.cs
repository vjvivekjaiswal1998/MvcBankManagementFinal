using System;
using System.ComponentModel.DataAnnotations;

namespace BankManagement.DTO
{
    public class BankDetail
    {


       //[Required]//(ErrorMessageResourceName = "AccountNuberValidation", ErrorMessageResourceType = typeof(ResourceDTO))]
       [Required(ErrorMessage = "*Please Enter The Account Number")]
        public int AccountNumber { get; set; }

        [Required]//(ErrorMessage = "*Please Enter The Account Type")]
        [StringLength(10 , ErrorMessage = " AccountType can't be more than 10." )]
        //    [StringLength(10,  ErrorMessage ="b"  , ErrorMessageResourceName = "ResourceDTO.b" )]
        public string AccountType { get; set; }
        
        [Required(ErrorMessage = "*Please Enter The Customer Name")]
        [StringLength(30)]
        [MinLength(3,ErrorMessage = "Customer Name should be minimum 3 character.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "*Please Enter The Nominee Name")]
        [StringLength(20)]
        [MinLength(3, ErrorMessage = "Nominee Name should be minimum 3 character.")]
        public string NomieeName { get; set; }

        [Required(ErrorMessage = "*Please Enter The Customer Address")]
        [StringLength(50)]
        [MinLength(3, ErrorMessage = "*Customer Address should be minimum 3 character.")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "*Please Enter The Customer Email")]
        [EmailAddress]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "*Please Enter The Customer Phone Number")]
        [Phone]
        [Range(1000000000, 9999999999, ErrorMessage = "*PhoneNumber must be of 10 digits")]
        public string CustomerPhoneNumber { get; set; }

    }
}
