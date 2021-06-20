using RestSharp;
using System.Configuration;
using System.Web.Http;
using WebhookReceiver.Models;

namespace WebhookReceiver.Controllers
{
    public class HelpersController : ApiController
    {
        // GET: Helpers
        public string Post([FromBody] addPacketCommunicatioModel value, string id)
        {
            var login = new WebhookController();
            string apikey = login.loginToEnate();
            string url = ConfigurationManager.AppSettings["URL"]+"/PacketCommunication/AddPacketCommunication?packetGUID=" + id + "&authToken=" + apikey;
            var client = new RestClient(url);
            var request = new RestRequest();

            var body = new addPacketCommunicatioModel
            {
                Body = value.Body,
                CommunicationType = 4,
                IsResolutionCommunication = false
            };
            request.AddJsonBody(body);
            client.Post(request);
            return "Added";
        }
    }
}