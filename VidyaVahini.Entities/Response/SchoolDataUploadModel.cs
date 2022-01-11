using System.Collections.Generic;

namespace VidyaVahini.Entities.Response
{
    public class SchoolDataUploadModel
    {
        public int RecordsUploaded { get; set; }
        public int DuplicateRecords { get; set; }
        public List<ErrorExcelRow> RowsNotUploaded { get; set; }
    }

    public class ErrorExcelRow
    {
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public string ErrorMessage { get; set; }
    }
}
