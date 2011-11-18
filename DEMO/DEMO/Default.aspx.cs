using System;
using DataAnnotation;

namespace DEMO
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Model.ContactInfo oContactInfo = new DEMO.Model.ContactInfo
            {
                FirstName = "Harish",
                LastName = "Bansal",
                Email = "harish@gmail.com",
                Mobile = 4515733426,
                URL = "http://www.google.com",
                Birthday = Convert.ToDateTime("12/14/2001")
            };

            DataAnnotation.ModelValidate modelState = new ModelValidate();

            if (modelState.Validate<Model.ContactInfo>(oContactInfo))
            {
                labelMessage.Text = "Form has been validated";
            }
            else
                labelMessage.Text = modelState.GetError(",");
        }
    }
}