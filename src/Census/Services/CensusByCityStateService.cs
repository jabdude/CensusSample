using Census.Models;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Census.Services
{
    public interface ICensusByCityStateService
    {
        void Insert(XSSFWorkbook workbook, string sheetName, IList<CensusModel> models);
    }

    public class CensusByCityStateService : ICensusByCityStateService
    {
        public void Insert(XSSFWorkbook workbook, string sheetName, IList<CensusModel> models)
        {

            var censusByCityState = models.GroupBy(x =>  new { x.City, x.State })
                .Select(x => new
                {
                    x.Key.City,
                    x.Key.State,
                    Count = x.Count()
                })
                .ToList();

            using (var fs = new FileStream("CensusAnalysis.xls", FileMode.Open))
            {
                workbook = new XSSFWorkbook();

                var excelSheet = workbook.CreateSheet(sheetName);
                var row = excelSheet.CreateRow(0);

                foreach (var model in censusByCityState)
                {
                    row = excelSheet.CreateRow(censusByCityState.IndexOf(model));
                    row.CreateCell(0).SetCellValue(model.City);
                    row.CreateCell(1).SetCellValue(model.State);
                    row.CreateCell(2).SetCellValue(model.Count);
                }

                workbook.Write(fs);
            }
        }
    }
}
