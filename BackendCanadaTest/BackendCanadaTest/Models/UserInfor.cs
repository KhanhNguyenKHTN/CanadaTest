using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCanadaTest.Models
{
    public class UserInfor
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Authen { get; set; }
        public string Token { get; set; }
        public bool IsSetUpCode { get; set; }
        public List<string> Roles { get; set; }
    }
}
