using NPOI.XSSF.UserModel;
using System.IO;

namespace Census.Services
{
    public interface ICreateXLSpreadsheetService
    {
        XSSFWorkbook Create();
    }

    public class CreateXLSpreadsheetService : ICreateXLSpreadsheetService
    {
        public XSSFWorkbook Create()
        {
            XSSFWorkbook workbook;
            using (var fs = new FileStream("CensusAnalysis.xls", FileMode.Create))
            {
                workbook = new XSSFWorkbook();
              
                workbook.Write(fs);
            }

            return workbook;
        }
    }
}
