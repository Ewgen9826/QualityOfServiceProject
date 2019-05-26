using OfficeOpenXml;
using System.IO;

namespace QualityOfServiceApp.Helpers
{
    public static class ExcelExportHelper
    {
        public static void Export(string path, SaveToExcelModel model)
        {
            using (var p = new ExcelPackage())
            {
                var ws = p.Workbook.Worksheets.Add("MySheet");
                
                ws.Cells["A1"].Value = "Ожидание";
                ws.Cells["A1"].Style.Font.Bold = true;
                ws.Cells["A2"].Value = "Восприятие";
                ws.Cells["A2"].Style.Font.Bold = true;
                ws.Cells["A3"].Value = "Коэффициент значимости";
                ws.Cells["A3"].Style.Font.Bold = true;
                ws.Cells["A4"].Value = "Вывод";
                ws.Cells["A4"].Style.Font.Bold = true;

                ws.Cells["B1"].Value = model.OverallExpectation;
                ws.Cells["B2"].Value = model.OverallPerception;
                ws.Cells["B3"].Value = model.QualityFactor;
                ws.Cells["B4"].Value = model.ResultMessage;

                ws.Cells["E1"].Value = "Значимость";
                ws.Cells["E1"].Style.Font.Bold = true;
                ws.Cells["E2"].Value = "Вывод";
                ws.Cells["E2"].Style.Font.Bold = true;

                ws.Cells["F1"].Value = model.OverallSignificance;
                ws.Cells["F2"].Value = model.ResultSignificanceMessage;

                ws.Cells.AutoFitColumns();

                p.SaveAs(new FileInfo(path+ ".xlsx"));
            }
        }
    }
}
