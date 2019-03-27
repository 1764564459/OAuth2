using OAuthServer.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuthServer.Models.DataRepository
{
    public class ClientRepository
    {
        public static List<Client> GetClient()
        {
            return new List<Client>()
            {
                new Client ()
                {
                    ID="id",
                    Secret="LHZ5bUlOR2dzW1Yzd1dkbHdFbXNQSVBHSEs9dTZQKTE=",
                    RedirectUrl="http://localhost:60726",
                },
                new Client ()
                {
                    ID="second",
                    Secret="987654321",
                    RedirectUrl="http://localhost:60726"
                }
            };
        }
    }
}