using System.Collections.Generic;
using System.IO;
using ExcelDataReader;
using VidyaVahini.Entities.Response;
using VidyaVahini.Entities.School;
using VidyaVahini.Repository.Contracts;
using VidyaVahini.Service.Contracts;

namespace VidyaVahini.Service
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolService(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        public SchoolDataUploadModel AddSchools(Stream fileStream)
        {
            List<SchoolData> schoolData = new List<SchoolData>();
            List<ErrorExcelRow> errorExcelRows = new List<ErrorExcelRow>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var reader = ExcelReaderFactory.CreateReader(fileStream))
            {
                for (int row = 1; reader.Read(); row++)
                {
                   //if (row == 1) continue;
                    var school = new SchoolData();

                    if (string.IsNullOrWhiteSpace(reader.GetString(1)))
                    {
                        errorExcelRows.Add(new ErrorExcelRow
                        {
                            RowNumber = row,
                            ColumnNumber = 1,
                            ErrorMessage = "required data is missing",
                        });
                        continue;
                    }
                    else
                    {
                        school.Code = reader.GetValue(0)?.ToString();
                    }
                    school.Name = reader.GetString(1);
                    school.ContactNumber = reader.GetValue(2)?.ToString();
                    school.Email = reader.GetValue(3)?.ToString();
                    if (string.IsNullOrWhiteSpace(reader.GetValue(4)?.ToString()))
                    {
                        errorExcelRows.Add(new ErrorExcelRow
                        {
                            RowNumber = row,
                            ColumnNumber = 5,
                            ErrorMessage = "required data is missing",
                        });
                        continue;
                    }
                    else
                    {
                        school.AddressLine1 = reader.GetValue(4)?.ToString();
                    }
                    school.AddressLine2 = reader.GetValue(5)?.ToString();
                    if (string.IsNullOrWhiteSpace(reader.GetValue(6)?.ToString()))
                    {
                        errorExcelRows.Add(new ErrorExcelRow
                        {
                            RowNumber = row,
                            ColumnNumber = 7,
                            ErrorMessage = "required data is missing",
                        });
                        continue;
                    }
                    else
                    {
                        school.City = reader.GetValue(6)?.ToString();
                    }
                    if (string.IsNullOrWhiteSpace(reader.GetValue(7)?.ToString()))
                    {
                        errorExcelRows.Add(new ErrorExcelRow
                        {
                            RowNumber = row,
                            ColumnNumber = 8,
                            ErrorMessage = "required data is missing",
                        });
                        continue;
                    }
                    else
                    {
                        school.Area = reader.GetValue(7)?.ToString();
                    }
                    if (string.IsNullOrWhiteSpace(reader.GetValue(8)?.ToString()))
                    {
                        errorExcelRows.Add(new ErrorExcelRow
                        {
                            RowNumber = row,
                            ColumnNumber = 9,
                            ErrorMessage = "required data is missing or not in correct format",
                        });
                        continue;
                    }
                    else
                    {
                        school.StateId =reader.GetValue(8)?.ToString();
                    }
                    if (string.IsNullOrWhiteSpace(reader.GetValue(9)?.ToString()))
                    {
                        errorExcelRows.Add(new ErrorExcelRow
                        {
                            RowNumber = row,
                            ColumnNumber = 10,
                            ErrorMessage = "required data is missing or not in correct format",
                        });
                        continue;
                    }
                    else
                    {
                        school.countryid = reader.GetValue(9)?.ToString();
                    }

                    schoolData.Add(school);
                }
            }

            var schoolsAdded = _schoolRepository.AddSchools(schoolData);
            return new SchoolDataUploadModel
            {
                RecordsUploaded = schoolsAdded,
                DuplicateRecords = schoolData.Count - schoolsAdded,
                RowsNotUploaded = errorExcelRows,
            };
        }
    }
}
