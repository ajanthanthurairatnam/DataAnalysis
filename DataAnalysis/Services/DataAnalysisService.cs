using DataAnalysis.Entities;
using DataAnalysis.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Services
{
    public class DataAnalysisService
    {
        public string Error { get; set; }

        public DataAnalysisService()
        {
            Error = string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvLine"></param>
        /// <returns></returns>
        public Person ReadFromCsv(string csvLine, Person person)
        {
            Error = string.Empty;
            try
            {
                string[] values = csvLine.Split(',');
                person.LastName = values[0];
                person.FirstName = values[1];
                person.Attribute = Convert.ToInt32(values[2]);
                person.Suburb = values[3];
                person.PostCode = values[4];
                person.Lat = Convert.ToDouble(values[5]);
                person.Lon = Convert.ToDouble(values[6]);
                return person;
            }
            catch (Exception ex)
            {
                Error = string.Format("An unknown error occurred while reading CSV. Exception Details:::{0}", ex.InnerException);
                return new Person();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDataAnalysisHelper"></param>
        /// <returns></returns>
        public List<Person> GetPersonListFromFile(DataAnalysisHelper dataAnalysisHelper)
        {
            Error = string.Empty;
            List<Person> persons = new List<Person>();
            try
            {
                string currentDirectory = dataAnalysisHelper.GetCurrentDirectory();
                if (dataAnalysisHelper.Error == string.Empty)
                {
                    string filePath = System.IO.Path.Combine(currentDirectory, "CSVFiles", "dataOct-17-2018.csv");
                    persons = File.ReadAllLines(filePath)
                                                   .Skip(1)
                                                   .Select(v => this.ReadFromCsv(v, new Person()))
                                                   .ToList();
                }
                else
                {
                    Error = dataAnalysisHelper.Error;
                }
            }
            catch (Exception ex)
            {
                Error = string.Format("An unknown error occurred while reading Person List From File. Exception Details:::{0}", ex.InnerException);

            }
            return persons;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persons"></param>
        /// <param name="dataAnalysisHelper"></param>
        /// <returns></returns>
        public List<PersonInformation> GetPersonInformationList(List<Person> persons, DataAnalysisHelper dataAnalysisHelper)
        {
            Error = string.Empty;
            List<PersonInformation> personInformationList = new List<PersonInformation>();
            try
            {
                 personInformationList= persons.Select(e =>
                                         new PersonInformation
                                         {
                                             LastName = e.LastName,
                                             FirstName = e.FirstName,
                                             Attribute = e.Attribute,
                                             Suburb = e.Suburb,
                                             PostCode = e.PostCode,
                                             Lat = e.Lat,
                                             Lon = e.Lon,
                                             RelativeDistance
                                                                 =
                                                                     dataAnalysisHelper.GetDistanceBetweenCoordinates(e.Lat, e.Lon, RelativePoint.LATTITUDE, RelativePoint.LONGITUDE)
                                         }
                ).ToList();
            }
            catch (Exception ex)
            {
                Error = string.Format("An unknown error occurred while generating Person Information List. Exception Details:::{0}", ex.InnerException);

            }
            return personInformationList;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personInformationList"></param>
        /// <param name="dataAnalysisHelper"></param>
        /// <returns></returns>
        public List<PersonInformation> GetPersonInfomationListRelativeDistanceLessThanThousandMeters(List<PersonInformation> personInformationList, DataAnalysisHelper dataAnalysisHelper)
        {
            Error = string.Empty;
            return personInformationList.Where(e => e.RelativeDistance <= 1).ToList();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="personInformationList"></param>
        /// <returns></returns>
        public List<PersonInformation> GettopTenPersonInfomationToRelativeDistance(List<PersonInformation> personInformationList)
        {
            Error = string.Empty;
            return personInformationList.OrderBy(e => e.RelativeDistance).Take(10).ToList();
        }
        
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personInformationList"></param>
        /// <returns></returns>
        public List<PersonInformation> GetPersonInfomationWithAttributeOne(List<PersonInformation> personInformationList)
        {
            Error = string.Empty;
            return personInformationList.Where(e => e.Attribute == 1).ToList();
        }
           

    }
}
