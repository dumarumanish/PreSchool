using System;
using System.Collections.Generic;
using System.Text;

namespace PreSchool.Application.Services.PaymentPartners.Models.Dtos
{
    public class CyberSourceTransactionResponse
    {
        public string auth_cv_result { get; set; }
        public string req_card_number { get; set; }
        public string req_locale { get; set; }
        public string signature { get; set; }
        public string auth_trans_ref_no { get; set; }
        public string req_card_type_selection_indicator { get; set; }
        public string payer_authentication_enroll_veres_enrolled { get; set; }
        public string req_bill_to_surname { get; set; }
        public string req_bill_to_address_city { get; set; }
        public string payer_authentication_proof_xml { get; set; }
        public string req_card_expiry_date { get; set; }
        public string auth_cavv_result { get; set; }
        public string req_bill_to_address_postal_code { get; set; }
        public string req_bill_to_phone { get; set; }
        public string card_type_name { get; set; }
        public string reason_code { get; set; }
        public string auth_amount { get; set; }
        public string auth_response { get; set; }
        public string bill_trans_ref_no { get; set; }
        public string req_bill_to_forename { get; set; }
        public string req_payment_method { get; set; }
        public string request_token { get; set; }
        public string auth_cavv_result_raw { get; set; }
        public string auth_time { get; set; }
        public string req_amount { get; set; }
        public string req_bill_to_email { get; set; }
        public string payer_authentication_reason_code { get; set; }
        public string auth_avs_code_raw { get; set; }
        public string payer_authentication_enroll_e_commerce_indicator { get; set; }
        public string transaction_id { get; set; }
        public string req_currency { get; set; }
        public string req_card_type { get; set; }
        public string decision { get; set; }
        public string message { get; set; }
        public string signed_field_names { get; set; }
        public string req_transaction_uuid { get; set; }
        public string auth_avs_code { get; set; }
        public string auth_code { get; set; }
        public string req_bill_to_address_country { get; set; }
        public string req_transaction_type { get; set; }
        public string req_access_key { get; set; }
        public string auth_cv_result_raw { get; set; }
        public string req_profile_id { get; set; }
        public string req_reference_number { get; set; }
        public string req_bill_to_address_state { get; set; }
        public string signed_date_time { get; set; }
        public string req_bill_to_address_line1 { get; set; }
    }
}
