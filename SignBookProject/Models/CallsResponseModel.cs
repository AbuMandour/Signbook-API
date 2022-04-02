using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignBookProject.Models
{
    public class CallsResponseModel
    {
        //user id user name access token

        public string UserID { get; set; }
        public string UserName { get; set; }
        public string AccessToken { get; set; }
    }
}
