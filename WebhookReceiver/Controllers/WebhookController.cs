using Newtonsoft.Json;
using RestSharp;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using WebhookReceiver.Models;
namespace WebhookReceiver.Controllers
{
    public class WebhookController : ApiController
    {

        string CS = ConfigurationManager.ConnectionStrings["LocalSqlServerDB"].ConnectionString;






        /* this api will add new webhook 
         * in DB table newwebhook*/

        public string Post(string id)
        {

            SqlConnection con = new SqlConnection(CS);
            var helpers = new ValuesController();
            var apikey = loginToEnate();
            var webhookGUID = helpers.getWebhookGUID(id, apikey);



            SqlCommand cmd = new SqlCommand("insert into Newcommunicationwebhook (PacketGUID,WebhookGUID) Values('" + id + "','" + webhookGUID + "')" +
                "UPDATE Requests SET NewCommunication =  1 WHERE PacketGUID = '" + id + "'" +
                "UPDATE Requests SET webhookGUID = '" + webhookGUID + "' WHERE PacketGUID = '" + id + "'", con);


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();


            return webhookGUID.ToString();

        }



        /* this api will Delete new webhook 
         * in DB table newwebhook*/
        public string Delete(string id)
        {
            var login = new WebhookController();
            var apikey = login.loginToEnate();
            id = id.ToUpper();


            SqlConnection con = new SqlConnection(CS);
            var WebhookGUID = "";


            SqlCommand cmd1 = new SqlCommand("select WebhookGUID  from NewCommunicationWebhook where packetGUID  = '" + id + "'", con);
            con.Open();
            WebhookGUID = (string)cmd1.ExecuteScalar();


            string removed = removeWebhookGUID(WebhookGUID, apikey);

            if (removed == "Done")
            {
                SqlCommand cmd = new SqlCommand("delete from Newcommunicationwebhook where PacketGUID = '" + id + "'" +
                    "UPDATE Requests SET NewCommunication = 0 WHERE PacketGUID = '" + id + "'", con);

                cmd.ExecuteNonQuery();
                con.Close();
                return id;
            }
            else
            {
                return "Not Found";
            }
        }

        // first time communication loads
        public string Get()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM requests inner JOIN newWebhookCommunication ON Requests.PacketGUID = newWebhookCommunication.PacketGUID where newWebhookCommunication.pullState = '1' ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "Empty";
            }
        }

        // Helpers
        public string loginToEnate()
        {
            string url = ConfigurationManager.AppSettings["MySetting"] + "/webapi/Authentication/Login?useCookie=false";
            var client = new RestClient(url);
            var request = new RestRequest();
            string apikey;


            var body = new loginData
            {
                username = ConfigurationManager.AppSettings["UN"],
                password = ConfigurationManager.AppSettings["PW"]
            };

            request.AddJsonBody(body);

            var response = client.Post(request);
            apikey = response.Content;
            apikey = apikey.Replace("+", "%2B").Replace("/", "%2F").Replace("=", "%3D");
            apikey = apikey.Substring(1, apikey.Length - 2);
            return apikey;
        }

        private string removeWebhookGUID(string id, string apikey)
        {
            var url = "https://enate.community/Innovation/webapi/WebHookSubscription/Unsubscribe?webHookSubscriptionGUID=" + id + "&" + "authToken=" + apikey;
            var client1 = new RestClient(url);
            var request1 = new RestRequest();
            client1.Post(request1);
            return "Done";
        }

        public string getPacketCommunication(string id, string apikey)
        {
            var url = "https://enate.community/Innovation/webapi/PacketCommunication/GetCommunication?communicationGUID=" + id + "&" + "&sanitizeHTMLBody=false&authToken=" + apikey;
            var client1 = new RestClient(url);
            var request1 = new RestRequest();
            var response = client1.Get(request1);
            string content = response.Content.ToString();








            return content;
        }
    }
}