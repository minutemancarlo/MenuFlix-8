using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuFlix.Web.Shared.Models
{
    public class Store
    {
        [Required]
        public int StoreId { get; set; }
        [Required]
        public string StoreName { get; set; }
        [Required]
        public string StoreAddress { get; set; }
        [Required]
        public string StoreContact { get; set; }
        [Required]
        public string StoreEmail { get; set; }
        public string StoreLogo { get; set; }
        public int ApplicationId { get; set; }
    }
}
