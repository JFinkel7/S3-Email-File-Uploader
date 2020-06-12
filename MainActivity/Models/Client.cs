using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MainActivity.Models {
    public class Client {

        // Email  
        [Required(ErrorMessage = "Invalid")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        // File
        [DataType(DataType.Upload)]
        public IFormFile File { get; set; }



    }// Class Ends 
}
