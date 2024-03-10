using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuFlix.Web.Shared
{
    public static class UserDto
    {
        public class Index
        {
            public string Auth0Id { get; set; }
            public string Email { get; set; }
            public string FullName { get; set; }
            public string Provider { get; set; }
            public string UserId { get; set; }
            public bool Blocked { get; set; }
            public DateTime? LastLogin { get; set; }
        }


    }
}
