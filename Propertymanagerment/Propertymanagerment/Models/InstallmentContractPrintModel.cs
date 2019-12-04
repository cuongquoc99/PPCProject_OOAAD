using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Propertymanagerment.Models
{
    public class InstallmentContractPrintModel
    {
        public string Installment_Contract_Code { get; set; }
        public string Customer_Name { get; set; }
        public string Year_Of_Birth { get; set; }
        public string SSN { get; set; }
        public string Customer_Address { get; set; }
        public string Mobile { get; set; }
        public Nullable<System.DateTime> Date_Of_Contract{ get; set; }
        public string Installment_Payment_Method { get; set; }
        public Nullable<int> Payment_Period { get; set; }
        public Nullable <decimal> Price { get; set; }
        public Nullable<decimal> Deposit { get; set; }
        public Nullable<decimal> Loan_Amount { get; set; }
        public Nullable<decimal> Taken { get; set; }
        public string Address { get; set; }
        public Nullable<decimal> Remain { get; set; }
        public string Property_Code { get; set; }
    }
}