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
    public class RecipientsController : ControllerBase
    {
        private readonly ILogger<RecipientsController> _logger;

        public RecipientsController(ILogger<RecipientsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{recipient}")]
        public IEnumerable<Recipient> Get(string myRecipient)
        {
            List<Recipient> recipients = new List<Recipient>();

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

                command.Parameters.AddWithValue("@Recipient", "%" + myRecipient + "%");

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Recipient recipient = new Recipient();

                    if(!reader.IsDBNull(reader.GetOrdinal("FEC_Election_Year")))
                    {
                        recipient.FEC_Election_Year = reader.GetInt16(reader.GetOrdinal("FEC_Election_Year"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_ZIP")))
                    {
                        recipient.Contributor_ZIP = reader.GetInt32(reader.GetOrdinal("Contributor_ZIP"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_City")))
                    {
                        recipient.Contributor_City = reader.GetString(reader.GetOrdinal("Contributor_City"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Name")))
                    {
                        recipient.Contributor_Name = reader.GetString(reader.GetOrdinal("Contributor_Name"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Street_1")))
                    {
                        recipient.Contributor_Street_1 = reader.GetString(reader.GetOrdinal("Contributor_Street_1"));
                    }   
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Employer")))
                    {
                        recipient.Contributor_Employer = reader.GetString(reader.GetOrdinal("Contributor_Employer"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contributor_Occupation")))
                    {
                        recipient.Contributor_Occupation = reader.GetString(reader.GetOrdinal("Contributor_Occupation"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contribution_Receipt_Amount")))
                    {
                        recipient.Contribution_Receipt_Amount = reader.GetInt32(reader.GetOrdinal("Contribution_Receipt_Amount"));
                    }
                    if(!reader.IsDBNull(reader.GetOrdinal("Contribution_Receipt_Date")))
                    {
                        recipient.Contribution_Receipt_Date = reader.GetInt32(reader.GetOrdinal("Contribution_Receipt_Date"));
                    }
                    recipients.Add(recipient);
                }
            }

            return recipients;
        }
    }
}
