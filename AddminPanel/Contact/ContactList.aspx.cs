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

public partial class AddminPanel_Contact_ContactList : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            FillGridview();
        }

    }

    #endregion Load Event

    #region Fill Grid view
    private void FillGridview()
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
            {


                objConn.Open();

                SqlCommand objComm = objConn.CreateCommand();

                objComm.CommandType = CommandType.StoredProcedure;
                objComm.CommandText = "PR_Contact_SelecteALL";

                SqlDataReader objSDR = objComm.ExecuteReader();
                gvContact.DataSource = objSDR;
                gvContact.DataBind();
            }
            #endregion Set Connection & Command Object
        }
        catch (Exception ex)
        {
            lblmassge.Text = ex.Message;
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


    #region gvContact | RowCommand
    protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if(e.CommandName=="DeleteRecord")
        {
            if(e.CommandArgument!=null)
            {
                DeleteContact(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
                
            }
        }
    }

    #endregion gvContact | RowCommand

    #region Delete Contact
    private void DeleteContact(SqlInt32 ContactID)
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable
        {
            try
            {
                #region Set Connection & Command Object
                objConn.Open();

                SqlCommand ObjCmd = new SqlCommand();

                ObjCmd.Connection = objConn;
                ObjCmd.CommandType = CommandType.StoredProcedure;
                ObjCmd.CommandText = "PR_Contact_DeleteByPK";
                ObjCmd.Parameters.AddWithValue("ContactID", ContactID);
                ObjCmd.ExecuteNonQuery();

                objConn.Close();
                FillGridview();
                #endregion Set Connection & Command Object
            }
            catch (Exception ex)
            {
                lblmassge.Text = ex.Message;
            }
            finally
            {
                objConn.Close();
            }

        }
    }
    #endregion Delete Contact
}