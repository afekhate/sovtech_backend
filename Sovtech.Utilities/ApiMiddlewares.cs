using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http.Json;
using System.Threading;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using RestSharp;
using Newtonsoft.Json;
using Sovtech_HM.Utilities;

namespace VatPay.Utilities.Common
{
    public class ApiMiddleWare
    {



      

        public static async Task<IRestResponse> IRestGetLocal(string dataurl)
        {

            try
            {
                var client = new RestClient(dataurl);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "text/plain");
                IRestResponse responsetask = client.Execute(request);
                return responsetask;


            }


            catch (Exception ex)
            {
                return null;
            }

        }

        public static async Task<IRestResponse> IRestPostLocal(string Url, object payload)
        {

            try
            {
                string payloadtoJson = JsonConvert.SerializeObject(payload);

                var client = new RestClient(Url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");

                //request.AddHeader("hashKey", shagoHashkey);


                request.AddParameter("application/json", payloadtoJson, ParameterType.RequestBody);
                IRestResponse responsetask = client.Execute(request);

                // LogError(responsetask);

                return responsetask;


            }


            catch (Exception ex)
            {
                return null;
            }

        }

        public static async Task<IRestResponse> IRestGetAsync(string baseUrl, string dataurl)
        {
            try
            {

                var client = new RestClient(baseUrl + dataurl);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                //request.AddHeader("Authorization", /*"Bearer " +*/ accessDetails.access_token);
                IRestResponse responsetask = client.Execute(request);
                return responsetask;


            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static async Task<IRestResponse> IRestPostAsync(string Url, object payload)
        {

            try
            {
                string payloadtoJson = JsonConvert.SerializeObject(payload);

                var client = new RestClient(Url);
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");

                //request.AddHeader("hashKey", shagoHashkey);

                request.AddParameter("application/json", payloadtoJson, ParameterType.RequestBody);
                IRestResponse responsetask = client.Execute(request);

                // LogError(responsetask);

                return responsetask;

            }

            catch (Exception ex)
            {
                return null;
            }

        }
    }
}