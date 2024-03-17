using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuFlix.Web.Shared.Models
{
    public class UserInfo
    {
        public string UserId { get; set; }
        List<UserDetails> UserDetails { get; set; }
    }

    public partial class UserDetails
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
    }

}
