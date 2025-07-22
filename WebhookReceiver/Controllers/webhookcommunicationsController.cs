using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using WebhookReceiver.Models;

namespace WebhookReceiver.Controllers
{
    public class webhookcommunicationsController : ApiController
    {

        string CS = ConfigurationManager.ConnectionStrings["LocalSqlServerDB"].ConnectionString;

        public object Post([FromBody] webhookCommunicationModel value)
        {
            SqlConnection con = new SqlConnection(CS);
            var PacketCommunicationGuid = value.GUID;
            var PacketGuid = value.PacketGUID;

            var login = new WebhookController();
            string apikey = login.loginToEnate();
            string data = login.getPacketCommunication(PacketCommunicationGuid, apikey);
            getcommunication getobject = JsonConvert.DeserializeObject<getcommunication>(data);


            SqlCommand cmd = new SqlCommand("insert into newWebhookCommunication (GUID,ToAddress,FromAddress," +
               "AttachmentCount,Body,PacketGUID,pullstate) Values('" + getobject.GUID + "'" +
               ",'" + getobject.To + "','" + getobject.From + "', '" + getobject.AttachmentCount + "','" + getobject.Body + "'" +
               ",'" + PacketGuid +"',0)", con);
            
            
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


        public string Get()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM requests inner JOIN newWebhookCommunication ON Requests.PacketGUID = newWebhookCommunication.PacketGUID where newWebhookCommunication.pullState = '0'", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SqlDataAdapter da1 = new SqlDataAdapter("UPDATE newWebhookCommunication SET pullstate = 1 WHERE pullstate=0", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
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
            SqlCommand cmd = new SqlCommand("delete from newWebhookCommunication where GUID = '" + id + "'", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return id;
        }

    }
}