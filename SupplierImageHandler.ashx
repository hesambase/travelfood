<%@ WebHandler Language="C#" Class="SupplierImageHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;  
using System.Data.SqlClient;
using System.Configuration;


public class SupplierImageHandler : IHttpHandler {

    public void ProcessRequest (HttpContext context) {

        string cs = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlConnection connection = new SqlConnection(cs);
        string imageid = context.Request.QueryString["Id"];
        
        connection.Open();
        SqlCommand command = new SqlCommand("select image from SupplierGallery where Id=" + imageid, connection);

        SqlDataReader dr = command.ExecuteReader();
        dr.Read();
        context.Response.BinaryWrite((Byte[])dr[0]);
        connection.Close();
        context.Response.End();


    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}