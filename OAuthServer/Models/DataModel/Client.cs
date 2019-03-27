using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthServer.Models.DataModel
{
    public class Client
    {
        public string ID { get; set; }
        public string Secret { get; set; }
        public string RedirectUrl { get; set; }
    }
}