using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Dtos
{
    public class KhaltiVerificationResponse
    {
        public string idx { get; set; }
        public Type type { get; set; }
        public State state { get; set; }
        public int amount { get; set; }
        public int fee_amount { get; set; }
        public bool refunded { get; set; }
        public DateTime created_on { get; set; }
        public object ebanker { get; set; }
        public User user { get; set; }
        public Merchant merchant { get; set; }
        public string token { get; set; }
    }

    public class Type
    {
        public string idx { get; set; }
        public string name { get; set; }
    }

    public class State
    {
        public string idx { get; set; }
        public string name { get; set; }
        public string template { get; set; }
    }

    public class User
    {
        public string idx { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
    }

    public class Merchant
    {
        public string idx { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
    }
}