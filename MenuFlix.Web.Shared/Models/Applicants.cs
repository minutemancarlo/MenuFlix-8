using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuFlix.Web.Shared.Models
{
    public class Applicants : UserInfo
    {
        public string UserId { get; set; }
        public string ApplicationType { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string ApplicationId { get; set; }
    }
}
