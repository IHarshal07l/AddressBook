using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddminPanel_Country_CountryADDEdit : System.Web.UI.Page
{
    #region Lode Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if(Request.QueryString["CountryID"]!=null)
            {
                FillControls(Convert.ToInt32(Request.QueryString["CountryID"])); 
            }
            else
            {
                if (Request.QueryString["CountryID"] == null)
                {
                    lblMassge.Text = "Add Mode";
                    lblMassge.ForeColor = Color.Green;
                }
            }
        }



    }

    #endregion Lode Event

    #region Button | Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variables 
        SqlString strCountryName = SqlString.Null;
        SqlInt32 intCountryCode = SqlInt32.Null;
        

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variables


        try
        {

            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                
                #region Gather Information
                intCountryCode = Convert.ToInt32(txtCountryCode.Text.Trim());
                objCmd.Parameters.AddWithValue("@CountryCode", intCountryCode);

                strCountryName = txtCountryName.Text.Trim();
                objCmd.Parameters.AddWithValue("@CountryName", strCountryName);
                #endregion Gather Information


                #endregion Set Connection & Command Object


                #region Update Record
                if (Request.QueryString["CountryID"] != null)
                {

                    
                    objCmd.Parameters.AddWithValue("@CountryID", Request.QueryString["CountryID"].ToString().Trim());
                    objCmd.CommandText = "PR_Country_Table_Update_PK";
                    objCmd.ExecuteNonQuery();
                    Response.Redirect("~/AddminPanel/Country/CountryList.aspx");


                }
                #endregion Update Record
                else
                {

                    #region Insert Record

                    objCmd.CommandText = "PR_Country_Table_Insert";
                    objCmd.ExecuteNonQuery();

                    txtCountryName.Text = "";
                    txtCountryCode.Text = "";

                    txtCountryName.Focus();
                    lblMassge.Text = "Data ADD Successfuly";
                    lblMassge.ForeColor = Color.Green;

                    #endregion Insert Record
                }



                


                objConn.Close();
            }
        }
        catch(Exception ex)
        {
            lblMassge.Text = ex.Message;
        }
        finally
        {
             if(objConn.State == ConnectionState.Open)
             {
                 objConn.Close();
             }
        }
    }

    #endregion Button | Save

    #region Fill Controls
    private void FillControls(SqlInt32 CountryID)
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
                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_Country_Table_SelectByPK";
                objCmd.Parameters.AddWithValue("@CountryID", CountryID.ToString().Trim());
                SqlDataReader objSDR = objCmd.ExecuteReader();
                lblMassge.Text = "Edit Mode";
                lblMassge.ForeColor = Color.Green;

                #endregion Set Connection & Command Object


                #region Read The Value And Set The Controals
                if (objSDR.HasRows)
                {
                    while(objSDR.Read())
                    {
                        if (!objSDR["CountryName"].Equals(DBNull.Value))
                        {
                            txtCountryName.Text = objSDR["CountryName"].ToString().Trim();
                        }
                        if (!objSDR["CountryCode"].Equals(DBNull.Value))
                        {
                            txtCountryCode.Text = objSDR["CountryCode"].ToString().Trim();
                        }
                        break;
                    }
                }
                else
                {
                    lblMassge.Text = "Data Is Not Available";
                }
                #endregion Read The Value And Set The Controals
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
    #endregion Fill Controls
}