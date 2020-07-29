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
    public class DonorZipCodeController : ControllerBase
    {
        private readonly ILogger<DonorZipCodeController> _logger;

        public DonorZipCodeController(ILogger<DonorZipCodeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{ZIP}")]
        public IEnumerable<Donor> Get(string ZIP)
        {
            List<Donor> donors = new List<Donor>();

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
                                               Committee_Name,
                                               Contribution_Receipt_Date
                                        from Donations 
                                        where Contributor_ZIP LIKE @ZIP"; 

                command.Parameters.AddWithValue("@ZIP", "%" + ZIP + "%");

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Donor donor = new Donor();

                    if(!reader.IsDBNull(reader.GetOrdinal("FEC_Election_Year")))
                    {
                        donor.FEC_Election_Year = reader.GetInt16(reader.GetOrdinal("FEC_Election_Year"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_ZIP")))
                    {
                        donor.Contributor_ZIP = reader.GetInt32(reader.GetOrdinal("Contributor_ZIP"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_City")))
                    {
                        donor.Contributor_City = reader.GetString(reader.GetOrdinal("Contributor_City"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Name")))
                    {
                        donor.Contributor_Name = reader.GetString(reader.GetOrdinal("Contributor_Name"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Street_1")))
                    {
                        donor.Contributor_Street_1 = reader.GetString(reader.GetOrdinal("Contributor_Street_1"));
                    }   
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Employer")))
                    {
                        donor.Contributor_Employer = reader.GetString(reader.GetOrdinal("Contributor_Employer"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Occupation")))
                    {
                        donor.Contributor_Occupation = reader.GetString(reader.GetOrdinal("Contributor_Occupation"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contribution_Receipt_Amount")))
                    {
                        donor.Contribution_Receipt_Amount = reader.GetDouble(reader.GetOrdinal("Contribution_Receipt_Amount"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Committee_Name")))
                    {
                        donor.committee_name = reader.GetString(reader.GetOrdinal("committee_name"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contribution_Receipt_Date")))
                    {
                        donor.Contribution_Receipt_Date = reader.GetDateTime(reader.GetOrdinal("Contribution_Receipt_Date"));
                    }
                    donors.Add(donor);
                }
            }

            return donors;
        }
    }
}
