using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;
using RestSharp;
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


    public class FirstCallController : ApiController
    {

      
        // DB connection string 
        string CS = ConfigurationManager.ConnectionStrings["LocalSqlServerDB"].ConnectionString;


        /*   
         * this web api will Get all the data from DB Requests table where pull state =0 ;
         * get called by browser loads first time.
         */
        public string Get()
        {
            SqlConnection con = new SqlConnection(CS);
            SqlDataAdapter da = new SqlDataAdapter("select * from Requests where pullstate=0", con);
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
    }
}