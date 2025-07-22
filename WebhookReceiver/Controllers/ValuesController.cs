using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Web.Script.Serialization;
using WebhookReceiver.Models;

namespace WebhookReceiver.Controllers
{
    public class ValuesController : ApiController
    {

        string CS = ConfigurationManager.ConnectionStrings["LocalSqlServerDB"].ConnectionString;

        public string Get()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlDataAdapter da = new SqlDataAdapter("select * from Requests where pullstate = 1", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                updateDatabase();
                return JsonConvert.SerializeObject(dt);
            }
            else
            {
                return "Empty";
            }
        }


        public string Delete(string id)
        {
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("delete from requests where PacketGUID = '" + id + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return id;
        }



        public string Post([FromBody] MyObj value)
        {
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("insert into Requests (PacketGUID,CustomerName,SupplierName," +
                "ContractName,ServiceName,ServiceLineName,ProcessTypeName,Reference,Date,pullstate,RAGStatus,Title,DueDate,NewCommunication) Values('" + value.GUID + "'" +
                ",'" + value.CustomerName + "','" + value.SupplierName + "', '" + value.ContractName + "','" + value.ServiceName + "'" +
                ",' " + value.ServiceLineName + "','" + value.ProcessTypeName + "', '" +
                value.Reference + "','" + DateTime.Now + "', 1, '" + value.RAGStatus + "','" + value.Title + "','" + value.DueDate + "',0)", con);
            con.Open();

            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 1)
            {

                return "added to table" + value.GUID;
            }

            else
            {
                return "Error";
            }
        }


        // Helper methods

        private void updateDatabase()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("UPDATE Requests SET PullState = 0 WHERE PullState = 1;", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        public string getWebhookGUID(string id, string apikey)
        {

            var url = ConfigurationManager.AppSettings["URL"] + "/WebHookSubscription/SubscribeToWebhook?authToken=" + apikey;
            var client1 = new RestClient(url);
            var request1 = new RestRequest();



            // POST Request to Enate API

            id = id.ToUpper();
            var body1 = new MyWebhookSubsObj
            {
                Webhook = "NewCommunication",
                FilterObjectGUID = id,
                FilterObjectType = "Packet",
                SubscriberURL = ConfigurationManager.AppSettings["SubURL"]
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
