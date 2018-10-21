using DataAnalysis.Entities;
using DataAnalysis.Helper;
using DataAnalysis.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAnalysis
{
    public class Program
    {
        static void Main(string[] args)
        {
            PrintHelper printHelper = new PrintHelper();
            printHelper.PrintText("Start Data Analysis.");
            printHelper.PrintText("::::::::::::::::::::");
            printHelper.PrintText("");
            try
            {   
                DataAnalysisService dataAnalysisService = new DataAnalysisService();
                DataAnalysisHelper dataAnalysisHelper = new DataAnalysisHelper();

                /*
                 Reading a text file and Binding to Person List, 
                        This functionality will be changed if retrieved from database 
                        Entity Framework/ADO.NET Can be used to retrive data from DB and Bind As a PersonList
                 */
                List<Person> personList = dataAnalysisService.GetPersonListFromFile(dataAnalysisHelper);

                if (dataAnalysisService.Error != string.Empty)
                        printHelper.PrintText(dataAnalysisService.Error);
                    else
                {
                    //1) Distance of the people relating to -37.954, 144.615
                    List<PersonInformation> personInformationList = dataAnalysisService.GetPersonInformationList(personList, dataAnalysisHelper);

                    if (dataAnalysisService.Error == string.Empty)
                    {
                        //2) People within 1000 m of reference point
                        List<PersonInformation> personInfomationRelativeDistanceLessThanThousandMeters 
                                                    = dataAnalysisService.GetPersonInfomationListRelativeDistanceLessThanThousandMeters
                                                                (personInformationList, dataAnalysisHelper);

                        printHelper.PrintText(string.Format("People within 1000m of the reference point Lat {0} Long {1}.", 
                                                                RelativePoint.LATTITUDE.ToString(), 
                                                                    RelativePoint.LONGITUDE.ToString()));

                        if (dataAnalysisService.Error!="")
                        {
                            printHelper.PrintText(dataAnalysisService.Error);
                        }
                        else
                        {
                            printHelper.PrintText(string.Format("No of People {0}", personInfomationRelativeDistanceLessThanThousandMeters.Count()));
                        }

                        //Break Point
                        printHelper.PrintText("");
                      
                        //3) Top 10 people closest to reference point.
                        printHelper.PrintText(string.Format("Top 10 people closest to reference point Lat {0} Long {1}.", RelativePoint.LATTITUDE.ToString(), RelativePoint.LONGITUDE.ToString()));

                        List <PersonInformation> topTenPersonInfomationToRelativeDistance = dataAnalysisService.GettopTenPersonInfomationToRelativeDistance(personInformationList);
                        if (dataAnalysisService.Error != "")
                        {
                            printHelper.PrintText(dataAnalysisService.Error);
                        }
                        else
                        {
                            printHelper.PrintPersonInformation(topTenPersonInfomationToRelativeDistance);
                        }

                        //Break Point
                        printHelper.PrintText("");                      

                        //4) People with Attribute = 1
                        printHelper.PrintText(string.Format("People with Attribute 1.", RelativePoint.LATTITUDE.ToString(), RelativePoint.LONGITUDE.ToString()));
                        
                        List <PersonInformation> personInfomationWithAttributeOne = dataAnalysisService.GetPersonInfomationWithAttributeOne(personInformationList);
                        if (dataAnalysisService.Error != "")
                        {
                            printHelper.PrintText(dataAnalysisService.Error);
                        }
                        else
                        {
                            printHelper.PrintPersonInformation(personInfomationWithAttributeOne);
                        }
                    }
                    else
                    {
                        printHelper.PrintText(dataAnalysisService.Error);
                    }

                }

                
            }
            catch (Exception ex)
            {
                printHelper.PrintText(string.Format("Data Analysis Unexcepted Error Occured{0}",ex.StackTrace));
            }
            printHelper.PrintText("");
            printHelper.PrintText("::::::::::::::::::::");           
            printHelper.PrintText("Stop Data Analysis");
        }
       
    }  
    
}
