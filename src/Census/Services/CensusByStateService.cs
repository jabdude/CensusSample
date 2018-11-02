using Census.Models;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Census.Services
{
    public interface ICensusByStateService
    {
        void Insert(XSSFWorkbook workbook, string sheetName, IList<CensusModel> models);
    }
    public class CensusByStateService : ICensusByStateService
    {
        public void Insert(XSSFWorkbook workbook, string sheetName, IList<CensusModel> models)
        {

            var censusByState = models
                .GroupBy(x => x.State)
                .Select(x => new 
                {
                    State = x.Key,
                    Count = x.Count()
                })
                .ToList();

            using (var fs = new FileStream("CensusAnalysis.xls", FileMode.Open))
            {
                workbook = new XSSFWorkbook();

                var excelSheet = workbook.CreateSheet(sheetName);

                foreach (var model in censusByState)
                {
                    var row = excelSheet.CreateRow(censusByState.IndexOf(model));
                    row.CreateCell(0).SetCellValue(model.State);
                    row.CreateCell(1).SetCellValue(model.Count);                    
                }

                workbook.Write(fs);
            }         
        }
    }
}
