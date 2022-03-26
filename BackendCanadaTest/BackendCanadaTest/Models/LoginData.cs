using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCanadaTest.Models
{
    public class LoginData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class TwoFaData
    {
        public string UserName { get; set; }
        public string Code { get; set; }
    }
}
