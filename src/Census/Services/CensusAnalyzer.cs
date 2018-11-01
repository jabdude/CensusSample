using System.Linq;

namespace Census.Services
{
    public interface ICensusAnalyzer
    {
        void Start();
    }
    public class CensusAnalyzer : ICensusAnalyzer
    {
        private readonly ICensusReaderService _reader;
        private readonly ICensusParserService _parser;
        private readonly ICreateXLSpreadsheetService _spreadsheetService;
        private readonly ICensusByStateService _censusByStateService;
        private readonly ICensusByCityStateService _censusByCityStateService;

        public CensusAnalyzer(ICensusReaderService reader,
            ICensusParserService parser, 
            ICreateXLSpreadsheetService spreadsheetService,
            ICensusByStateService censusByStateService,
            ICensusByCityStateService censusByCityStateService)
        {
            _reader = reader;
            _parser = parser;
            _spreadsheetService = spreadsheetService;
            _censusByStateService = censusByStateService;
            _censusByCityStateService = censusByCityStateService;
        }

        public void Start()
        {
            var rawData = _reader.Read("us-500.csv").ToList();

            var censusModels = _parser.Parse(rawData).ToList();

            var workbook = _spreadsheetService.Create();

            _censusByStateService.Insert(workbook, "Census By State", censusModels);

            _censusByCityStateService.Insert(workbook, "Census By City-State", censusModels);
        }
    }
}
