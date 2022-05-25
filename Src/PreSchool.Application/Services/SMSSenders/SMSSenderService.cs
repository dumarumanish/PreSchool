using PreSchool.Application.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PreSchool.Application.Services.SMSSenders
{
    public class SMSSenderService : ISMSSenderService
    {
 
        private readonly AppSettings _appSettings;
     

        public SMSSenderService(
          
            IOptions<AppSettings> appSettings
       
            )
        {
         
            _appSettings = appSettings.Value;
          
        }

        public string SendSMS(string from, string token, string to, string text)
        {

            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["from"] = from;
                values["auth_token"] = token;
                values["to"] = to;
                values["text"] = text;
                var response = client.UploadValues("https://sms.aakashsms.com/sms/v3/send/", "Post", values);
                var responseString = Encoding.Default.GetString(response);
                return responseString;
            }
        }

        public async Task<bool> SendSMS(string to, string text)
        {
            using (var client = new WebClient())
            {
                var values = new NameValueCollection();
                values["from"] = _appSettings.SendSMSFrom;
                values["auth_token"] = _appSettings.AccessSMSToken;
                values["to"] = to;
                values["text"] = text;
                var response = await client.UploadValuesTaskAsync(_appSettings.SMSSendUrl, "Post", values);
                var responseString = Encoding.Default.GetString(response);
                return responseString == "success";
            }
        }

    }

}
