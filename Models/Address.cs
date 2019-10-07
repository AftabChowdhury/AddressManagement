using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AddressManagement_2.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "*")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "*")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "*")]
        public string Street { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "*")]
        [DisplayName("Post Code")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Post Code must be numeric")]
        public string PostCode { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "*")]
        public string City { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "*")]
        public string State { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [Required(ErrorMessage = "*")]
        public string Country { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(250)")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone Number must be numeric")]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
