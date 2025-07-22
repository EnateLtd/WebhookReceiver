using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using WebhookReceiver.Models;

namespace WebhookReceiver.Helpers
{
    public class Helpers
    {

        SqlConnection con = new SqlConnection(@"Data Source= ");


        public string loginToEnate()
        {
            string url = "https://enate_InstanceUrl";
            var client = new RestClient(url);
            var request = new RestRequest();
            string apikey;


            var body = new loginData
            {
                username = "", //Username of instance
                password = "" //password of instance
            };

            request.AddJsonBody(body);


            var response = client.Post(request);

            apikey = response.Content;
            apikey = apikey.Replace("+", "%2B").Replace("/", "%2F").Replace("=", "%3D");
            apikey = apikey.Substring(1, apikey.Length - 2);
            return apikey;
        }

        private void updateDatabase()
        {
            SqlCommand cmd = new SqlCommand("UPDATE Requests SET PullState = 0 WHERE PullState = 1;", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private object getWebhookGUID(string id, string apikey)
        {
            var url ="https://enate_InstanceUrl"; + apikey;
            var client1 = new RestClient(url);
            var request1 = new RestRequest();



            // POST Request to Enate API

            id = id.ToUpper();
            var body1 = new MyWebhookSubsObj
            {
                Webhook = "NewCommunication",
                FilterObjectGUID = id,
                FilterObjectType = "Packet",
                SubscriberURL = "https://webhook.site/fff2f57f-4b9e-4d54-ad99-0f3f11eb9636"
            };

            request1.AddJsonBody(body1);
            var response = client1.Post(request1);
            string content = response.Content.ToString();


            // Desrialization of Response content and returning webhook GUID

            JavaScriptSerializer deSerializedResponse = new JavaScriptSerializer();
            Root root = (Root)deSerializedResponse.Deserialize(content, typeof(Root));
            return root.Result.GUID;
        }
    }
}
