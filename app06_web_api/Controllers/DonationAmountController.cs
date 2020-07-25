using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;


namespace app06_web_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DonationAmountController : ControllerBase
    {
        private readonly ILogger<DonationAmountController> _logger;

        public DonationAmountController(ILogger<DonationAmountController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{amount}")]
        public IEnumerable<DonationAmount> Get(string amount)
        {
            List<DonationAmount> donationAmounts = new List<DonationAmount>();

            string connectionString = @"Server=LAPTOP-ALPTJ9MA\SQLEXPRESS;Database=AR FEC Donation Data;Trusted_Connection=True;";
                                                
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"select TOP 10 FEC_Election_Year,
                                               Contributor_Name,
                                               Contributor_Street_1,
                                               Contributor_City,
                                               Contributor_ZIP,
                                               Contributor_Employer,
                                               Contributor_Occupation,
                                               Contribution_Receipt_Amount,
                                               Contribution_Receipt_Date
                                        from Donations 
                                        where Contribution_Receipt_Amount LIKE @DonationAmount"; 

                command.Parameters.AddWithValue("@DonationAmount", "%" + amount + "%");

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DonationAmount donationAmount = new DonationAmount();
                    if(!reader.IsDBNull(reader.GetOrdinal("FEC_Election_Year")))
                    {
                        donationAmount.FEC_Election_Year = reader.GetInt16(reader.GetOrdinal("FEC_Election_Year"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_ZIP")))
                    {
                        donationAmount.Contributor_ZIP = reader.GetInt32(reader.GetOrdinal("Contributor_ZIP"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_City")))
                    {
                        donationAmount.Contributor_City = reader.GetString(reader.GetOrdinal("Contributor_City"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Name")))
                    {
                        donationAmount.Contributor_Name = reader.GetString(reader.GetOrdinal("Contributor_Name"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Street_1")))
                    {
                        donationAmount.Contributor_Street_1 = reader.GetString(reader.GetOrdinal("Contributor_Street_1"));
                    }   
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Employer")))
                    {
                        donationAmount.Contributor_Employer = reader.GetString(reader.GetOrdinal("Contributor_Employer"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Occupation")))
                    {
                        donationAmount.Contributor_Occupation = reader.GetString(reader.GetOrdinal("Contributor_Occupation"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contribution_Receipt_Amount")))
                    {
                        donationAmount.Contribution_Receipt_Amount = reader.GetInt32(reader.GetOrdinal("Contribution_Receipt_Amount"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contribution_Receipt_Date")))
                    {
                        donationAmount.Contribution_Receipt_Date = reader.GetInt32(reader.GetOrdinal("Contribution_Receipt_Date"));
                    }
                    donationAmounts.Add(donationAmount);
                }
            }

            return donationAmounts;
        }
    }
}
