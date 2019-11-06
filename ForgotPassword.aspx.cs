using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ForgotPassword : InfraStructure.LocalizedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {


            Initialize();
        }




    }

    private void Initialize()
    {

       
    }

    protected void btnForgot_Click(object sender, EventArgs e)
    {

        string username;
        string password;
        try
        {
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection cn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SELECT UserName,PSW FROM AspNetUsers WHERE Email=@Email", cn);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text;

            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();

            SqlDataAdapter sda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            sda.SelectCommand = cmd;
            try
            {
                cn.Open();
                //sda.UpdateCommand = cmd;
                sda.Fill(ds, "AspNetUsers");
                if (ds.Tables["AspNetUsers"].Rows.Count > 0)
                {
                    username = ds.Tables["AspNetUsers"].Rows[0]["UserName"].ToString();
                    password = ds.Tables["AspNetUsers"].Rows[0]["PSW"].ToString();
                    //Create the msg object to be sent
                    MailMessage msg = new MailMessage();
                    //Add your email address to the recipients
                    msg.To.Add(txtEmail.Text);
                    //Configure the address we are sending the mail from
                    MailAddress address = new MailAddress("mail@ipdmsoft.com");
                    msg.From = address;
                    msg.IsBodyHtml = false;
                    //Append their name in the beginning of the subject
                    msg.Subject = "Ipdmsoft.com Account information Recovery";
                    msg.Body = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}", "Your http://safarfood.com Account information is:\n", "Username: ", username.ToString(), "\n", "Password: ", password.ToString(), "\n", "Please do not reply this message");

                    //Configure an SmtpClient to send the mail.
                    SmtpClient client = new SmtpClient();
                    client.Host = "38.99.139.136";
                    // client.EnableSsl = true; //only enable this if your provider requires it
                    //Setup credentials to login to our sender email address ("UserName", "Password")
                    NetworkCredential credentials = new NetworkCredential("mail@ipdmsoft.com", "hesam2d3d4d");
                    client.Credentials = credentials;

                    //Send the msg
                    client.Send(msg);

                    //Display some feedback to the user to let them know it was sent
                    divMenuTop.Visible = true;
                    divMenuTop.Attributes["class"] = "alert alert-success";
                    LitMenuTop.Text = string.Format("{0}{1}", Resources.Text.SendInfoSuccseed, txtEmail.Text);

                    //Clear the form
                    txtEmail.Text = "";
                }

                cn.Close();
            }
            catch(Exception ex)
            {
                //If the message failed at some point, let the user know
                divMenuTop.Visible = true;
                divMenuTop.Attributes["class"] = "alert alert-warning";

                LitMenuTop.Text = Resources.Text.SendInfoFail;
                
            }

        }
        catch(Exception ex)
        {
            divMenuTop.Visible = true;
            divMenuTop.Attributes["class"] = "alert alert-danger";
            LitMenuTop.Text = Resources.Messages.EmailIsNotValid;
        }
       


    }

    protected void divMenuTop_Load(object sender, EventArgs e)
    {
        divMenuTop.Visible = false;
    }
}



