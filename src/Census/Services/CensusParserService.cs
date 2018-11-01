using Census.Models;
using System.Collections.Generic;
using System.Linq;

namespace Census.Services
{
    public interface ICensusParserService
    {
        IEnumerable<CensusModel> Parse(IList<string> rawData);
    }

    public class CensusParserService : ICensusParserService
    {
        public IEnumerable<CensusModel> Parse(IList<string> rawData)
        {
            rawData.RemoveAt(0);

            return rawData.Select(x =>
            {
                var data = x.Split(",");

                return new CensusModel
                {
                    FirstName = data[0].Trim(),
                    LastName = data[1].Trim(),
                    CompanyName = data[2].Trim(),
                    Address = data[3].Trim(),
                    City = data[4].Trim(),
                    County = data[5].Trim(),
                    State = data[6].Trim(),
                    Zip = data[7].Trim(),
                    Phone1 = data[8].Trim(),
                    Phone2 = data[9].Trim(),
                    Email = data[10].Trim(),
                    WebPage = data[2].Trim(),
                };
            });
        }
    }
}
