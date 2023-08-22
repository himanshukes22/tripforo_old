using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PG
{
    public class CCAvenue
    {
    }

    #region  Confirm Order and Cancel order boths are used  FailedList class
    public class FailedList
    {
        public string reference_no { get; set; }
        public string reason { get; set; }
        public string error_code { get; set; }
    }
    #endregion   

    #region Confirm Order
    public class Confirm
    {
        public List<FailedList> failed_List { get; set; }
        public string error_desc { get; set; }
        public int success_count { get; set; }
        public string error_code { get; set; }
    }
    #endregion

    #region Cancel Order
    public class Cancel
    {
        public List<FailedList> failed_List { get; set; }
        public string error_desc { get; set; }
        public int success_count { get; set; }
        public string error_code { get; set; }
    }
    #endregion

    #region Status
    public class Status
    {
        public string reference_no { get; set; }
        public string order_no { get; set; }
        public string order_currncy { get; set; }
        public double order_amt { get; set; }
        public string order_date_time { get; set; }
        public string order_bill_name { get; set; }
        public string order_bill_address { get; set; }
        public string order_bill_zip { get; set; }
        public string order_bill_tel { get; set; }
        public string order_bill_email { get; set; }
        public string order_bill_country { get; set; }
        public string order_ship_name { get; set; }
        public string order_ship_address { get; set; }
        public string order_ship_country { get; set; }
        public string order_ship_tel { get; set; }
        public string order_bill_city { get; set; }
        public string order_bill_state { get; set; }
        public string order_ship_city { get; set; }
        public string order_ship_state { get; set; }
        public string order_ship_zip { get; set; }
        public string order_ship_email { get; set; }
        public string order_notes { get; set; }
        public string order_ip { get; set; }
        public string order_status { get; set; }
        public string order_fraud_status { get; set; }
        public string order_status_date_time { get; set; }
        public double order_capt_amt { get; set; }
        public string order_card_name { get; set; }
        public string order_delivery_details { get; set; }
        public double order_fee_perc { get; set; }
        public double order_fee_perc_value { get; set; }
        public double order_fee_flat { get; set; }
        public double order_gross_amt { get; set; }
        public double order_discount { get; set; }
        public double order_tax { get; set; }
        public string order_bank_ref_no { get; set; }
        public string order_gtw_id { get; set; }
        public string order_bank_response { get; set; }
        public string order_option_type { get; set; }
        public double order_TDS { get; set; }
        public string order_device_type { get; set; }
        public string error_desc { get; set; }
        public int status { get; set; }
        public string error_code { get; set; }
    }
    #endregion
    #region Order Lookup
    public class OrderStatusList
    {
        public object reference_no { get; set; }
        public string order_no { get; set; }
        public string order_currncy { get; set; }
        public double order_amt { get; set; }
        public string order_date_time { get; set; }
        public string order_bill_name { get; set; }
        public string order_bill_address { get; set; }
        public string order_bill_zip { get; set; }
        public string order_bill_tel { get; set; }
        public string order_bill_email { get; set; }
        public string order_bill_country { get; set; }
        public string order_ship_name { get; set; }
        public string order_ship_address { get; set; }
        public string order_ship_country { get; set; }
        public string order_ship_tel { get; set; }
        public string order_bill_city { get; set; }
        public string order_bill_state { get; set; }
        public string order_ship_city { get; set; }
        public string order_ship_state { get; set; }
        public string order_ship_zip { get; set; }
        public string order_ship_email { get; set; }
        public string order_notes { get; set; }
        public string order_ip { get; set; }
        public string order_status { get; set; }
        public string order_fraud_status { get; set; }
        public string order_status_date_time { get; set; }
        public double order_capt_amt { get; set; }
        public string order_card_name { get; set; }
        public string order_delivery_details { get; set; }
        public double order_fee_perc_value { get; set; }
        public double order_fee_perc { get; set; }
        public double order_fee_flat { get; set; }
        public double order_gross_amt { get; set; }
        public double order_discount { get; set; }
        public double order_tax { get; set; }
        public string order_bank_ref_no { get; set; }
        public string order_gtw_id { get; set; }
        public string order_bank_response { get; set; }
        public string order_option_type { get; set; }
        public string order_TDS { get; set; }
        public string order_device_type { get; set; }
    }

    public class OrderLookup
    {
        public List<OrderStatusList> order_Status_List { get; set; }
        public int page_count { get; set; }
        public int total_records { get; set; }
        public string error_desc { get; set; }
        public string error_code { get; set; }
    }
    #endregion

#region Refund  Date 16Nov2016

    public class Refund
    {
        public string reason { get; set; }
        public int refund_status { get; set; }
        public string error_code { get; set; }
    }

#endregion

}
