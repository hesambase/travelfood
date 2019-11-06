using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangeCulture : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strCulture = Request.Params["Culture"];
        string strReferer = Request.ServerVariables["HTTP_REFERER"];
        Session["Culture"] = strCulture;
       Response.Redirect(strReferer, false);
       // Response.Redirect("~/Default.aspx", false);
    }
}