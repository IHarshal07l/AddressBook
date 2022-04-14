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

public partial class AddminPanel_City_CityAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //  FillDropDownListForState();

            FillStateDropDownList();
          
            if (Request.QueryString["CityID"] != null)
            {

                

                FillControls(Convert.ToInt32(Request.QueryString["CityID"]));
                lblMassge.Text = "Edit Mode";
                lblMassge.ForeColor = Color.Green;
                lblCountry.Attributes.Add("Style", "display:None");
                ddlCountry.Attributes.Add("Style", "display:None");
            }
            else
            {
                if (Request.QueryString["CityID"] == null)
                {
                    FillCountryDropDownList(); 
                    lblMassge.Text = "Add Mode";
                    lblMassge.ForeColor = Color.Green;
                    ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
                  
                }
            }
            
        }
        
        
    }


    #endregion Load Event

    #region Button | save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local variable
        SqlInt32 strStateID = SqlInt32.Null;
        SqlString strCityName = SqlString.Null;
        SqlString strSTDCode = SqlString.Null;
        SqlString strPINCode = SqlString.Null;

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local variable
        try
        {
            #region Server Side validation
            if (ddlState.SelectedIndex > 0)
            {
                strStateID = Convert.ToInt32(ddlState.SelectedValue);
            }
            if (txtCityName.Text.Trim() != "")
            {
                strCityName = txtCityName.Text.Trim();
            }
            if (txtPINcode.Text.Trim() != "")
            {
                strPINCode = txtPINcode.Text.Trim();
            }
            if (txtSTDCode.Text.Trim() != "")
            {
                strSTDCode = txtSTDCode.Text.Trim();
            }
            #endregion Server Side validation

            #region Read The Value And Set The Controals
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
              

                objCmd.Parameters.AddWithValue("@StateID", strStateID);
                objCmd.Parameters.AddWithValue("@CityName", strCityName);
                objCmd.Parameters.AddWithValue("@STDCode", strSTDCode);
                objCmd.Parameters.AddWithValue("@PinCode", strPINCode);
                #endregion Read The Value And Set The Controals

                #region update Data

                if (Request.QueryString["CityID"] != null)
                {


                    objCmd.Parameters.AddWithValue("@CityID", Request.QueryString["CityID"].ToString().Trim());
                    objCmd.CommandText = "PR_City_Table_UpdateByPK";
                    objCmd.ExecuteNonQuery();
                    Response.Redirect("~/AddminPanel/City/CityList.aspx");


                }
                #endregion update Data

                #region Insert data
                else
                {
                    objCmd.CommandText = "PR_City_Table_Insert";
                    objCmd.ExecuteNonQuery();
                    txtCityName.Text = "";
                    txtPINcode.Text = "";
                    txtSTDCode.Text = "";
                    ddlState.SelectedIndex = -1;
                    ddlCountry.SelectedIndex = -1;
                    lblMassge.Text = "Insert Data SucessFully";

                }
                #endregion Insert data



                objConn.Close();




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

    #endregion Button | save

    #region Fill CountryDropDown List
    private void FillCountryDropDownList()
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

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_Country_SelectForDropDownList";
                SqlDataReader objSDR = objCmd.ExecuteReader();
                #endregion Set Connection & Command Object

                #region Read The Value And Set The Controals
                if (objSDR.HasRows == true)
                {
                    ddlCountry.DataSource = objSDR;
                    ddlCountry.DataValueField = "CountryID";
                    ddlCountry.DataTextField = "CountryName";
                    ddlCountry.DataBind();
                }
                #endregion Read The Value And Set The Controals
                ddlCountry.Items.Insert(0, new ListItem("Select Country", "-1"));


                objConn.Close();

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
    #endregion Fill CountryDropDown List

    #region Fill StateDropDown List
    private void FillStateDropDownList()
    {
        #region Local Variable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)

                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_SelectFromState";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            #endregion Set Connection & Command Object

            #region Read The Value And Set The Controals
            if (objSDR.HasRows == true)
            {
                ddlState.DataSource = objSDR;
                ddlState.DataValueField = "StateID";
                ddlState.DataTextField = "StateName";
                ddlState.DataBind();
            }

            ddlCountry.Items.Insert(0, new ListItem("Select State", "-1"));
            #endregion Read The Value And Set The Controals

            objConn.Close();
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


    #endregion Fill StateDropDown List

    #region ddlCountry | SelectedIndexChanged
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region Local Variable
        SqlInt32 strCountryID = SqlInt32.Null;

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable


        if (Request.QueryString["CityID"] == null)
       {
           try
           {
               if (ddlCountry.SelectedIndex > 0)
               {
                   strCountryID = Convert.ToInt32(ddlCountry.SelectedValue);
               }
               if (objConn.State != ConnectionState.Open)
               {
                   #region Set Connection & Command Object
                   objConn.Open();

                   SqlCommand objCmd = objConn.CreateCommand();
                   objCmd.CommandType = CommandType.StoredProcedure;
                   objCmd.CommandText = "PR_State_SelectFromCountry";
                   objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
                   #endregion Set Connection & Command Object
                   SqlDataReader objSDR = objCmd.ExecuteReader();
                   #region Read The Value And Set The Controals
                   ddlState.DataSource = objSDR;
                   ddlState.DataValueField = "StateID";
                   ddlState.DataTextField = "StateName";
                   ddlState.DataBind();
                   ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
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
    }

    #endregion  ddlCountry_SelectedIndexChanged

    #region  FillControls
    private void FillControls(SqlInt32 CityID)
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
                objCmd.CommandText = "PR_City_Table_SelectByPK";
                objCmd.Parameters.AddWithValue("@CityID", CityID.ToString().Trim());
                SqlDataReader objSDR = objCmd.ExecuteReader();
                lblMassge.Text = "Edit Mode";
                lblMassge.ForeColor = Color.Green;
            #endregion Set Connection & Command Object

                #region Read The Value And Set The Controals
                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                       
                        if (!objSDR["StateID"].Equals(DBNull.Value))
                        {
                          ddlState.SelectedValue = objSDR["StateID"].ToString().Trim();
                        }
                        if (!objSDR["CityName"].Equals(DBNull.Value))
                        {
                            txtCityName.Text = objSDR["CityName"].ToString().Trim();
                        }
                       
                        {
                            txtSTDCode.Text = objSDR["STDCode"].ToString().Trim();
                        }
                       
                        {
                            txtPINcode.Text = objSDR["PinCode"].ToString().Trim();
                        }
                        break;
                    }
                    #endregion Read The Value And Set The Controals
                }
                else
                {
                    lblMassge.Text = "Data Is Not Available";
                }

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
    #endregion  FillControls
}




