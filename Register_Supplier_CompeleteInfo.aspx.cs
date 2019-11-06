using DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Account_Register_Supplier_CompeleteInfo : InfraStructure.LocalizedPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            // Determine the sections to render
            BindGridData();
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection cn = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SELECT Username,SupplierName,Avatar FROM Suppliers WHERE UserName=@UserName", cn);
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = Session["SupplierUserName"];


            DataTable dt = new DataTable();
            DataRow dr = dt.NewRow();

            SqlDataAdapter sda = new SqlDataAdapter();
            DataSet ds = new DataSet();
            sda.SelectCommand = cmd;
            try
            {
                cn.Open();
                //sda.UpdateCommand = cmd;
                sda.Fill(ds, "Suppliers");
                FullName.Text = ds.Tables["Suppliers"].Rows[0]["SupplierName"].ToString();
                UserName.Text = ds.Tables["Suppliers"].Rows[0]["UserName"].ToString();
                byte[] bytes = (byte[])ds.Tables["Supplier"].Rows[0]["Avatar"];

                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                imgAvatar.ImageUrl = "data:image/png;base64," + base64String;
                //int rowsAffected = ds.Tables[0].Rows.Count;
                cn.Close();
            }
            catch (Exception ex)
            {
                //litError.Text = ex.Message;
            }
        }
    }

    protected void CreateUser_Click(object sender, EventArgs e)
    {

        //Supplier sp=new Supplier();
        try
        {


            using (SafarFoodEntity entities = new SafarFoodEntity())
            {
                var result = entities.Suppliers.SingleOrDefault(s => s.CustomerID == UserName.Text);
                if (result != null)
                {

            //        result.CustomerPayment = EcologeName.Text;
            //        result.Slogan = Slogan.Text;

            //        result.KilometerTo = System.Convert.ToInt32(Kilometer.Text);
            //        result.Road = Road.Text;
            //        if (chkActive.Checked == true)
            //        {
            //            result.Active = true;

            //        }
            //        else
            //        {
            //            result.Active = false;
            //        }
            //        if (DeliverEnabled.Checked == true)
            //        {
            //            result.DeliveryEnabled = true;
            //        }
            //        else
            //        {
            //            result.DeliveryEnabled = false;
            //        }
            //        entities.Entry(result).State = System.Data.Entity.EntityState.Modified;
            //        // entities.update(sp);


               }

            //    entities.SaveChanges();
            //    Response.Redirect("~/Account/ConfirmationSupplier.aspx");
            //    Session["SupplierUserName"] = UserName.Text;

            }
        }
        catch (DbEntityValidationException db)
        {
            foreach (var eve in db.EntityValidationErrors)
            {
                Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                        ve.PropertyName, ve.ErrorMessage);
                }
            }
            throw;
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {

        System.Web.HttpPostedFile oHttpPostedFile = myFileUpload.PostedFile;
        if (oHttpPostedFile == null)
        {
            string strErrorMessage = Resources.Messages.PicNotUpload;
            DisplayErrorMessage(strErrorMessage);
            return;
        }
        if (oHttpPostedFile.FileName.Trim() == string.Empty)
        {
            string strErrorMessage = Resources.Messages.PicFileName;
            DisplayErrorMessage(strErrorMessage);
            return;
        }


        string strFileName = System.IO.Path.GetFileName(oHttpPostedFile.FileName);
        string strFileExtention = System.IO.Path.GetExtension(strFileName).ToUpper();
        if ((strFileExtention != ".JPG") && (strFileExtention != ".JPEG") && (strFileExtention != ".JPE"))
        {
            string strErrorMessage = Resources.Messages.PicJPEGFile;
            DisplayErrorMessage(strErrorMessage);
            return;
        }

        string strContentType = oHttpPostedFile.ContentType.ToUpper();
        if ((strContentType != "IMAGE/JPEG") && (strContentType != "IMAGE/PJPEG"))
        {
            string strErrorMessage = Resources.Messages.PicJPEGType;
            DisplayErrorMessage(strErrorMessage);
            return;
        }

        if (oHttpPostedFile.ContentLength == 0)
        {
            string strErrorMessage = Resources.Messages.PicNotUpload;
            DisplayErrorMessage(strErrorMessage);
            return;
        }

        if (oHttpPostedFile.ContentLength > 1000 * 100)
        {
            string strErrorMEssage = Resources.Messages.PicLength;
            DisplayErrorMessage(strErrorMEssage);
            return;
        }

        System.Drawing.Image oImage = System.Drawing.Image.FromStream(oHttpPostedFile.InputStream);

        if ((oImage.Width > 620) || (oImage.Height > 440))
        {
            string strErrorMessage = Resources.Messages.PicPixel;
            DisplayErrorMessage(strErrorMessage);
            return;
        }


        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        SqlConnection cn = new SqlConnection(cs);
        SqlCommand cmd = new SqlCommand("Update Suppliers SET Avatar=@Avatar WHERE UserName=@UserName", cn);
        cmd.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = UserName.Text; ;
        cmd.Parameters.Add("@Avatar", SqlDbType.VarBinary).Value = imageToByteArray(oImage);


        imgAvatar.ImageAlign = ImageAlign.AbsMiddle;
        byte[] bytes = imageToByteArray(oImage);

        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
        imgAvatar.ImageUrl = "data:image/png;base64," + base64String;
        try
        {

            cn.Open();
            //sda.UpdateCommand = cmd;
            cmd.ExecuteNonQuery();
            // FullName.Text = ds.Tables["Supplier"].Rows[0]["FullName"].ToString();
            // TelNumber.Text = ds.Tables["Supplier"].Rows[0]["MobileNumber"].ToString();
            //int rowsAffected = ds.Tables[0].Rows.Count;
            cn.Close();



            string strInformationMessage = Resources.Messages.PicSuccessfullyUploded;
            DisplayErrorMessage(strInformationMessage);
            //}
        }
        catch (Exception ex)
        {
            //ErrorMessage.Show(ex.Message);
        }

    }
    public byte[] imageToByteArray(System.Drawing.Image imageIn)
    {
        MemoryStream ms = new MemoryStream();
        imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        return ms.ToArray();
    }

    //Byte array to photo



    private void DisplayErrorMessage(string strErrorMessage)
    {
        divPageMessage.Visible = true;
        litPage.Text = strErrorMessage;
    }


    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(cs);

        //create command object
        SqlCommand cmd = new SqlCommand();

        //pass connection and query to your command object
        cmd.Connection = con;
        cmd.CommandText = String.Format("{0}{1}", "Select * from SupplierGallery INNERJOIN Suppliers ON Suppliers.UserName=SupplierGallery.Username where UserName=", UserName.Text.ToString());

        //create adaptor to fill data from database
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        //create datatable which holds the data
        DataTable dt = new DataTable();
        da.Fill(dt);

        //bind your data to gridview
        grdSupplierGallery.DataSource = dt;

        grdSupplierGallery.DataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Condition to check if the file uploaded or not   
        if (fileuploadEmpImage.HasFile)
        {
            //getting length of uploaded file  
            int length = fileuploadEmpImage.PostedFile.ContentLength;
            //create a byte array to store the binary image data  
            byte[] imgbyte = new byte[length];
            //store the currently selected file in memeory  
            HttpPostedFile img = fileuploadEmpImage.PostedFile;
            //set the binary data  
            img.InputStream.Read(imgbyte, 0, length);
            //int id = Convert.ToInt32(txtID.Text);
            // string imgName = txtName.Text;
            string Subject = txtSubject.Text;
            string Description = txtDescription.Text;

            //Connection String  
            string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(cs);
            //Open The Connection  
            connection.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO SupplierGallery (SupUserName,imgSubject,imgDescription,image) VALUES (@SupUserName,@imgSubject,@imgDescription,@imagedata)", connection);

            cmd.Parameters.Add("@SupUserName", SqlDbType.NVarChar, 50).Value = UserName.Text;
            cmd.Parameters.Add("@imgSubject", SqlDbType.NVarChar, 100).Value = Subject;
            cmd.Parameters.Add("@imgDescription", SqlDbType.NVarChar).Value = Description;
            cmd.Parameters.Add("@imagedata", SqlDbType.VarBinary).Value = imgbyte;

            int count = cmd.ExecuteNonQuery();
            //Close The Connection  
            connection.Close();
            if (count == 1)
            {
                //BindGridData();  
                txtID.Text = string.Empty;
                txtName.Text = string.Empty;
                txtSubject.Text = string.Empty;
                txtDescription.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert('با موفقیت ثبت شد')", true);
                //call the method to bind the grid  
                BindGridData();
            }

        }
    }

    private void BindGridData()
    {
        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlConnection connection = new SqlConnection(cs);
        SqlCommand command = new SqlCommand("SELECT Id,imgSubject,imgDescription,Image from [SupplierGallery] Where SupUserName=@SupUserName", connection);
        command.Parameters.Add("@SupUserName", SqlDbType.NVarChar, 50).Value = UserName.Text;
        SqlDataAdapter daimages = new SqlDataAdapter(command);
        DataTable dt = new DataTable();
        daimages.Fill(dt);
        grdSupplierGallery.DataSource = dt;
        grdSupplierGallery.DataBind();
    }



    //gvrow.FindControl("Label1") as Label
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //foreach (GridViewRow gvrow in GridView1.Rows)
        //{

        //    CheckBox chck = gvrow.FindControl("CheckBox1") as CheckBox;
        //    if (chck.Checked)
        //    {
             
      
        //        SqlCommand cmd = new SqlCommand("delete from tbl_data where id=@id", con);
        //        cmd.Parameters.AddWithValue("id", int.Parse(Label.Text));
        //        con.Open();
        //        int id = cmd.ExecuteNonQuery();
        //        con.Close();

        foreach (GridViewRow row in grdSupplierGallery.Rows)
        {
            if ((row.FindControl("cbImage") as CheckBox).Checked)
            {
                string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection connection = new SqlConnection(cs);
                //Open The Connection  
                connection.Open();
                SqlCommand cmd = new SqlCommand("Delete  from SupplierGallery Where Id=@Id", connection);

                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = ((Label)row.Cells[3].FindControl("lblId")).Text; 




                int count = cmd.ExecuteNonQuery();
                //Close The Connection  
                connection.Close();
                if (count == 1)
                {
                }
            }
            BindGridData();
        }
    }
    protected void grdSupplierGallery_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        
    }
    protected void grdSupplierGallery_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
}






