using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace AvailityAssessment2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ProcessCSVFile("C:/Temp/Enrollments.csv");
            
        }
        private void ProcessCSVFile(string sFIle)
        {
            // path to the csv file
            //string path = "C:/Temp/Enrollments.csv";

            lblStatus1.Text = "Loading csv file....";

            string[] lines = System.IO.File.ReadAllLines(sFIle);

            List<EnrollmentClass> list = new List<EnrollmentClass>();

            foreach (string line in lines)
            {
                int i = 0;
                string[] columns = line.Split(',');

                EnrollmentClass eclass = new EnrollmentClass();
                if (columns[0].StartsWith("\""))
                {
                    eclass.UserId = columns[0].Substring(1, columns[0].Length - 1);
                }
                else
                    eclass.UserId = columns[0].ToString();

                eclass.Name = columns[1].ToString();
                eclass.Version = Convert.ToInt32(columns[2].ToString());

                if (columns[3].EndsWith("\""))
                {
                    eclass.Insurance = columns[3].Substring(0, columns[3].Length - 1);
                }
                else
                    eclass.Insurance = columns[3].ToString();

                list.Add(eclass);


            }
            //create list of insurers
            var groupByInsurers = list.GroupBy(x => x.Insurance);
            foreach (var group in groupByInsurers)
            {
                //the group.Key is the name of the grouped Insurance company. Find the insurer in the list and create group list.
                List<EnrollmentClass> grouplist = list.FindAll(e => e.Insurance == group.Key).OrderBy(e => e.Name).ToList();

                //find duplicate users in group and remove oldest version
                List<string> duplicateUser = grouplist.GroupBy(x => x.UserId).Where(g => g.Count() > 1).Select(x => x.Key).ToList();
                foreach(string user in duplicateUser)
                {
                    EnrollmentClass oldest = grouplist.FindAll(e => e.UserId == user).OrderBy(p => p.Version).First();
                    bool bReturn = grouplist.Remove(oldest);
                }
                lblStatus2.Text = "Creating insurance csv files....";
                //create the insurer csv file
                CreateCSVInsuranceFile("C:/Temp/" + group.Key, grouplist);

                lblStatus3.Text = "Files created in C:\\Temp";
            }
        }
        private void CreateCSVInsuranceFile(string sOutPutFile, List<EnrollmentClass> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var record in list)
            {
                WriteItem(sb, record.UserId, true, false);
                WriteItem(sb, record.Name, false, false);
                WriteItem(sb, record.Version.ToString(), false, false);
                WriteItem(sb, record.Insurance, false, true);
            }

            File.WriteAllText(sOutPutFile, sb.ToString());
        }
        private void WriteItem(StringBuilder sb, string value, bool first, bool last)
        {
            if (!value.Contains('\"'))
            {
                if (!first)
                    sb.Append(',');

                sb.Append(value);

                if (last)
                    sb.AppendLine();
            }
            else
            {
                if (!first)
                    sb.Append(",\"");
                else
                    sb.Append('\"');

                sb.Append(value.Replace("\"", "\"\""));

                if (last)
                    sb.AppendLine("\"");
                else
                    sb.Append('\"');
            }
        }
    }
    class EnrollmentClass
    {
        string m_UserId;
        string m_Name;
        int m_Version;
        string m_Insurance;

        public string UserId { get => m_UserId; set => m_UserId = value; }
        public string Name { get => m_Name; set => m_Name = value; }
        public int Version { get => m_Version; set => m_Version = value; }
        public string Insurance { get => m_Insurance; set => m_Insurance = value; }
    }
}