using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AvailityAssessment
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool bReturn = ParenthesesProperlyCosed(TextBox1.Text);
            if (bReturn)
                this.Label2.Text = "Parantheses is properly closed!";
            else
                this.Label2.Text = "Parantheses is NOT properly closed!";
        }
        private bool ParenthesesProperlyCosed(string sValue)
        {
            Dictionary<char, char> paranPairs = new Dictionary<char, char>() {
            { '(', ')' }};

            //Stack<char> brackets = new Stack<char>();
            List<char> lParantheses = new List<char>();

            try
            {

                foreach (char c in sValue)
                {
                    if (paranPairs.Keys.Contains(c))
                    {
                        lParantheses.Add(c);
                    }
                    else
                        if (paranPairs.Values.Contains(c))
                    {
                        if (c == paranPairs[lParantheses.First()])
                        {
                            lParantheses.RemoveAt(lParantheses.Count - 1);
                        }
                        else
                            return false;
                    }
                    else
                        continue;
                }
            }
            catch
            {
                return false;
            }

            return lParantheses.Count() == 0 ? true : false;
        }
    }
}