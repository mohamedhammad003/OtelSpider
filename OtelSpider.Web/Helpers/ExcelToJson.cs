using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace OtelSpider.Web.Helpers
{
    public static class ExcelToJson
    {
        public static List<JObject> ConvertExcelToJson(string filePath)
        {
            var data = new List<JObject>();
            var fileStream = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(fileStream))
            {
                foreach (var worksheet in package.Workbook.Worksheets)
                {

                    var rowCount = worksheet.Dimension.Rows;
                    var columnCount = worksheet.Dimension.Columns;

                    var headers = new List<string>();

                    for (int col = 1; col <= columnCount; col++)
                    {
                        headers.Add(worksheet.Cells[1, col].Value.ToString().Trim());
                    }

                    for (int row = 2; row <= rowCount; row++)
                    {
                        dynamic obj = new JObject();

                        for (int col = 1; col <= columnCount; col++)
                        {
                            obj[headers[col - 1]] = worksheet.Cells[row, col].Value?.ToString().Trim();
                        }
                        data.Add(obj);
                    }
                }
            }
            return data;
        }
    }
}