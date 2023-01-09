using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Net;

namespace MiyGarden.Service.LDAP
{
    public class LDAPSvc
    {
        public string AuthAD_PrincipalSearcher()
        {
            var domain = "ict888.net";
            var password = "";
            var userName = "07xxx.xxx.xxx@ict888.net";
            // Get the Domain Pricipal
            PrincipalContext insPrincipalContext = new PrincipalContext(ContextType.Domain, domain, userName, password);
            // Get the User Principal and filter it by SAM / username
            UserPrincipal insUserPrincipal = new UserPrincipal(insPrincipalContext);
            insUserPrincipal.UserPrincipalName = userName;
            // Execute search.
            PrincipalSearcher insPrincipalSearcher = new PrincipalSearcher();
            insPrincipalSearcher.QueryFilter = insUserPrincipal;
            PrincipalSearchResult<Principal> results = insPrincipalSearcher.FindAll();
            foreach (Principal p in results)
            {
                // Return the first record.
                return p.DistinguishedName;
            }
            return null;
        }

        public bool AuthAD_DirectorySearcher()
        {
            try
            {
                var password = "";
                var UserPrincipalName = "07xxx.xxx.xxx@ict888.net";
                //DirectoryEntry deRootDSE = new DirectoryEntry("LDAP://RootDSE", UserPrincipalName, password, AuthenticationTypes.Secure);
                //DirectoryEntry deDomain = new DirectoryEntry("LDAP://" + deRootDSE.Properties["defaultNamingContext"].Value.ToString(), UserPrincipalName, password, AuthenticationTypes.Secure);
                var deDomain = new DirectoryEntry("LDAP://ict888.net", UserPrincipalName, password, AuthenticationTypes.Secure);
                var dsSearcher = new DirectorySearcher();
                dsSearcher.SearchRoot = deDomain;
                dsSearcher.SearchScope = System.DirectoryServices.SearchScope.Subtree;
                dsSearcher.Filter = "(userPrincipalName=" + UserPrincipalName + ")";
                var srResult = dsSearcher.FindOne();
                if (srResult != null)
                {
                    var deUser = new DirectoryEntry(srResult.GetDirectoryEntry().Path, UserPrincipalName, password, AuthenticationTypes.Secure);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;// ex.Message;
            }
        }

        public void TestAD_DirectorySearcher()
        {
            var dcs = new string[] {  }; //3268 389
            var u = "07xxx.xxx.xxx";
            foreach (var dc in dcs)
            {
                var directorySearcher = new DirectorySearcher(new DirectoryEntry(dc));
                directorySearcher.Filter = "(SAMAccountName=" + u + ")" /*"(objectCategory=organizationalUnit)"*/;

                try
                {
                    //var srResult = directorySearcher.FindAll();
                    //foreach (SearchResult item in srResult)
                    //{
                    //    string name = item.Properties["name"][0].ToString();
                    //    Console.WriteLine(name);
                    //}
                    var srResult = directorySearcher.FindOne();
                    if (srResult != null)
                    {
                        var deUser = new DirectoryEntry(srResult.GetDirectoryEntry().Path);
                        Console.WriteLine(deUser.Path);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(dc + " failed:" + ex.Message);
                }
            }
            Console.ReadKey();
        }

        public bool AuthAD_LdapConnection(string user = "07xxx.xxx.xxx", string pass = "")
        {
            using (var ldap = new LdapConnection(
                new LdapDirectoryIdentifier(new string[] {  }, 389, false, false),
                new NetworkCredential("", ""),
                AuthType.Basic))
            {
                ldap.SessionOptions.ProtocolVersion = 3;
                ldap.SessionOptions.ReferralChasing = ReferralChasingOptions.None;
                try
                {
                    ldap.Bind();
                    var searchRequest = new SearchRequest
                        ("DC=ICT888,DC=NET",
                        "(&(objectClass=user)(SAMAccountName=" + user + "))",
                        System.DirectoryServices.Protocols.SearchScope.Subtree);
                    var response = (SearchResponse)ldap.SendRequest(searchRequest);
                    string userDN = response.Entries[0].Attributes["DistinguishedName"][0].ToString();
                    ldap.Bind(new NetworkCredential(userDN, pass));
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
