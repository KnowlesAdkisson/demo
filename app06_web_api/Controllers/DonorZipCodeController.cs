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
        public IEnumerable<ZIP> Get(string myZIP)
        {
            List<ZIP> ZIPs = new List<ZIP>();

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
                                               Committee_Name,
                                               Contribution_Receipt_Amount,
                                               Contribution_Receipt_Date
                                        from Donations 
                                        where Committee_Name LIKE @Recipient"; 

                command.Parameters.AddWithValue("@ZIP", "%" + myZIP + "%");

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ZIP zip = new ZIP();
                    if(!reader.IsDBNull(reader.GetOrdinal("FEC_Election_Year")))
                    {
                        zip.FEC_Election_Year = reader.GetInt16(reader.GetOrdinal("FEC_Election_Year"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_ZIP")))
                    {
                        zip.Contributor_ZIP = reader.GetInt32(reader.GetOrdinal("Contributor_ZIP"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_City")))
                    {
                        zip.Contributor_City = reader.GetString(reader.GetOrdinal("Contributor_City"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Name")))
                    {
                        zip.Contributor_Name = reader.GetString(reader.GetOrdinal("Contributor_Name"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Street_1")))
                    {
                        zip.Contributor_Street_1 = reader.GetString(reader.GetOrdinal("Contributor_Street_1"));
                    }   
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Employer")))
                    {
                        zip.Contributor_Employer = reader.GetString(reader.GetOrdinal("Contributor_Employer"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Occupation")))
                    {
                        zip.Contributor_Occupation = reader.GetString(reader.GetOrdinal("Contributor_Occupation"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contribution_Receipt_Amount")))
                    {
                        zip.Contribution_Receipt_Amount = reader.GetInt32(reader.GetOrdinal("Contribution_Receipt_Amount"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contribution_Receipt_Date")))
                    {
                        zip.Contribution_Receipt_Date = reader.GetInt32(reader.GetOrdinal("Contribution_Receipt_Date"));
                    }
                    ZIPs.Add(zip);
                }
            }

            return ZIPs;
        }
    }
}
