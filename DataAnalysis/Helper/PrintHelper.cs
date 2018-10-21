using DataAnalysis.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAnalysis.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class PrintHelper
    {
        public string Error { get; set; }

        public PrintHelper()
        {
            Error = string.Empty;
        }

        /// <summary>
        /// Print Personal Information With Header
        /// </summary>
        /// <param name="PersonInformationList"></param>
        public void PrintPersonInformation(List<PersonInformation> PersonInformationList)
        {
            try
            {
                Error = string.Empty;
                if (PersonInformationList.Count == 0)
                {
                    Console.WriteLine(string.Format("{0}", "No Records Found"));
                }
                else
                {
                    foreach (var objPersonInformationa in PersonInformationList)
                    {
                        Console.WriteLine(string.Format("{0} {1} :::Suburb {2}", objPersonInformationa.LastName, objPersonInformationa.FirstName, objPersonInformationa.Suburb.ToString()));
                    }
                }
            }
            catch(Exception ex)
            {
                Error = string.Format("An unknown error occurred while printing. Exception Details:::{0}.", ex.InnerException);
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="printText"></param>
        public void PrintText(string printText)
        {
            Console.WriteLine(string.Format("{0}", printText));
        }
    }
}
