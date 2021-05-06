using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedEntities;
using System.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.IO;


namespace SharedServices
{
    public class MeasureMyPlataformServices 
    {
        public string Message { get; set; }

        public TokenResponse GetUrlRedirectMeasureMyPlataform(string username) {

            TokenResponse tokenResponse = new TokenResponse();

            try
            {
                TokenRequest requestData = new TokenRequest()
                {
                    ApplicationKey = ConfigurationManager.AppSettings["SAMLiveApplicationKey"],
                    ClientSecret = ConfigurationManager.AppSettings["SAMLiveClientSecret"],
                    Username = username
                };

                var jsonRequest = JsonConvert.SerializeObject(requestData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["SAMLiveGetToken"]);
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = jsonRequest.Length;
                request.Timeout = 60000;
                request.ReadWriteTimeout = 60000;

                //se envian los datos
                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonRequest);
                    writer.Flush();
                    writer.Close();
                    writer.Dispose();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                string jsonString = null;

                using (StreamReader reader = new StreamReader(responseStream))
                {

                    jsonString = reader.ReadToEnd();

                    reader.Close();
                    reader.Dispose();
                    response.Close();

                }

                tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonString);

                if (tokenResponse.error)
                {
                    Message = tokenResponse.message;
                }
                else {

                    tokenResponse.Url = String.Format(ConfigurationManager.AppSettings["SAMLiveUrlTokenAuthentication"], username, tokenResponse.data.token);
                }
            }
            catch (Exception ex) {
                Message = ex.Message;
            }

            return tokenResponse;
        }


        public bool UpdateUser(UserSAMLive user)
        {

            TokenResponse tokenResponse = new TokenResponse();

            try
            {
                string UserJson = "{\"Name\":\"" + user.username +
                                    "\",\"FirstName\": \"" + user.FirstName +
                                    "\",\"LastName\":\"" + user.LastName +
                                    "\",\"Company\":\"" + user.Company +
                                    "\",\"E-Mail\":\"" + user.Email +
                                    "\",\"Telephone\":\"" + user.Telephone +
                                    "\",\"Mobil\":\"" + user.Mobil +
                                    "\",\"RefUserGroupID\":\"" + user.RefUserGroupID +
                                    "\",\"DefaultDashboard\":\"" + user.DefaultDashboard + "\"}";

                UserModifyRequest requestData = new UserModifyRequest()
                {
                    UserJson = UserJson,
                    Token = user.Token
                };

                var jsonRequest = JsonConvert.SerializeObject(requestData);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ConfigurationManager.AppSettings["SAMLiveUpdateUser"]);
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = jsonRequest.Length;
                request.Timeout = 60000;
                request.ReadWriteTimeout = 60000;

                //se envian los datos
                using (var writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(jsonRequest);
                    writer.Flush();
                    writer.Close();
                    writer.Dispose();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();

                string jsonString = null;

                using (StreamReader reader = new StreamReader(responseStream))
                {

                    jsonString = reader.ReadToEnd();

                    reader.Close();
                    reader.Dispose();
                    response.Close();

                }

                tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(jsonString);

                if (tokenResponse.error)
                {
                    Message = tokenResponse.message;
                }
                
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return !tokenResponse.error;
        }
    }
}
