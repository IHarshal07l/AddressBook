using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddminPanel_City_CityList : System.Web.UI.Page
{
    #region load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            FillGridview();
        }
    }
    #endregion load Event

    #region Fill Grid view
    private void FillGridview()
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable

        try
        {
         
            if (objConn.State != ConnectionState.Open)
            {
                #region Set Connection & Command Object

                objConn.Open();

                SqlCommand objComm = objConn.CreateCommand();

                objComm.CommandType = CommandType.StoredProcedure;
                objComm.CommandText = "PR_City_Table_SelectAll";

                SqlDataReader objSDR = objComm.ExecuteReader();
                gvCity.DataSource = objSDR;
                gvCity.DataBind();
                objConn.Close();
                #endregion Set Connection & Command Object
            }
        }
        catch (Exception ex)
        {
            lblMassge.Text = ex.Message;
        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
    }
    #endregion Fill Grid view

    #region gvCity | RowCommand
    protected void gvCity_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {


            if (e.CommandArgument != "")
            {

                DeleteCity(Convert.ToInt32(e.CommandArgument.ToString().Trim()));

            }


        }

    }
    #endregion gvCity | RowCommand


    #region Delete City
    private void DeleteCity(SqlInt32 CityID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable
        {
            try
            {
                if (objConn.State != ConnectionState.Open)
                {
                    #region Set Connection & Command Object

                    objConn.Open();

                    SqlCommand ObjCmd = new SqlCommand();

                    ObjCmd.Connection = objConn;
                    ObjCmd.CommandType = CommandType.StoredProcedure;
                    ObjCmd.CommandText = "PR_City_Table_DeletByPK";
                    ObjCmd.Parameters.AddWithValue("CityID", CityID);
                    ObjCmd.ExecuteNonQuery();

                    objConn.Close();
                    FillGridview();
                    #endregion Set Connection & Command Object
                }
            }
            catch (Exception ex)
            {
                lblMassge.Text = ex.Message;
            }
            finally
            {
                objConn.Close();
            }
        
        }
    }
    #endregion Delete City

}