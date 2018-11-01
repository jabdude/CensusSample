using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Census.Services
{
    public interface ICensusReaderService
    {
        IEnumerable<string> Read(string fileName);
    }

    public class CensusReaderService : ICensusReaderService
    {
        public IEnumerable<string> Read(string fileName)
        {
            var records = File.ReadAllLines(fileName).ToList();

            var cleanRecords = records.Select(x => x.Replace("\\", String.Empty));
            cleanRecords = records.Select(x => x.Replace("\"", String.Empty));

            return cleanRecords;
        }
    }
}
