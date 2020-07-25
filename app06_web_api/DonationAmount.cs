using System;

namespace app06_web_api
{
    public class DonationAmount
    {
        public int? FEC_Election_Year { get; set; }
        public string Contributor_Name { get; set; }
        public string Contributor_Street_1 { get; set; }
        public string Contributor_City { get; set; }
        public int? Contributor_ZIP { get; set; }
        public string Contributor_Employer { get; set; }
        public string Contributor_Occupation { get; set; }
        public int? Contribution_Receipt_Amount { get; set; }
        public int? Contribution_Receipt_Date { get; set; }
        
    }
}
