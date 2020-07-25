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
    public class DonorOccupationController : ControllerBase
    {
        private readonly ILogger<DonorOccupationController> _logger;

        public DonorOccupationController(ILogger<DonorOccupationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{donorOccupation}")]
        public IEnumerable<DonorOccupation> Get(string myOccupation)
        {
            List<DonorOccupation> occupations = new List<DonorOccupation>();

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
                                        where Contributor_Occupation LIKE @Occupation"; 

                command.Parameters.AddWithValue("@Occupation", "%" + myOccupation + "%");

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DonorOccupation occupation = new DonorOccupation();

                    if(!reader.IsDBNull(reader.GetOrdinal("FEC_Election_Year")))
                    {
                        occupation.FEC_Election_Year = reader.GetInt16(reader.GetOrdinal("FEC_Election_Year"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_ZIP")))
                    {
                        occupation.Contributor_ZIP = reader.GetInt32(reader.GetOrdinal("Contributor_ZIP"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_City")))
                    {
                        occupation.Contributor_City = reader.GetString(reader.GetOrdinal("Contributor_City"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Name")))
                    {
                        occupation.Contributor_Name = reader.GetString(reader.GetOrdinal("Contributor_Name"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Street_1")))
                    {
                        occupation.Contributor_Street_1 = reader.GetString(reader.GetOrdinal("Contributor_Street_1"));
                    }   
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Employer")))
                    {
                        occupation.Contributor_Employer = reader.GetString(reader.GetOrdinal("Contributor_Employer"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Occupation")))
                    {
                        occupation.Contributor_Occupation = reader.GetString(reader.GetOrdinal("Contributor_Occupation"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contribution_Receipt_Amount")))
                    {
                        occupation.Contribution_Receipt_Amount = reader.GetInt32(reader.GetOrdinal("Contribution_Receipt_Amount"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contribution_Receipt_Date")))
                    {
                        occupation.Contribution_Receipt_Date = reader.GetInt32(reader.GetOrdinal("Contribution_Receipt_Date"));
                    }
                    occupations.Add(occupation);
                }
            }

            return occupations;
        }
    }
}
