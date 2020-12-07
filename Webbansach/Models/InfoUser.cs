using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webbansach.Models
{
    public class InfoUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Adress { get; set; }
        public string SDT { get; set; }
        public string Score { get; set; }
        public OrderDetail OrderDetail { get; set; }
    }
}