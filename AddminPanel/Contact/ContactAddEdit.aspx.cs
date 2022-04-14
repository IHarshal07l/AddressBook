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

public partial class AddminPanel_Contact_ContactAddEdit : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        

        if (!Page.IsPostBack)
        {

            FillCountryDropDownList();       
            FillContactCategoryDropdownList();
            FillStateDropDownList();
            FillCityDropDownList();
           

                
                if (Request.QueryString["ContactID"] != null)
                {

                   

                    FillControls(Convert.ToInt32(Request.QueryString["ContactID"]));
                    lblMassge.Text = "Edit Mode";
                    lblMassge.ForeColor = Color.Green;
                }
                
           
            else
            {
                if (Request.QueryString["ContactID"] == null)
                {
                    

                    lblMassge.Text = "Add Mode";
                    lblMassge.ForeColor = Color.Green;
                   
                }
            }
        }
    }
    #endregion Load Event

    #region Fill Country DropDownList
    private void FillCountryDropDownList()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)
            {


                objConn.Open();

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_Country_SelectForDropDownList";
                SqlDataReader objSDR = objCmd.ExecuteReader();

                if (objSDR.HasRows == true)
                {
                    ddlCountry.DataSource = objSDR;
                    ddlCountry.DataValueField = "CountryID";
                    ddlCountry.DataTextField = "CountryName";
                    ddlCountry.DataBind();
                }

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
            objConn.Close();
        }




    }


    #endregion Fill Country DropDownList

    #region Fill State DropDownList
    private void FillStateDropDownList()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        try
        {
            if (objConn.State != ConnectionState.Open)

                objConn.Open();

            SqlCommand objCmd = objConn.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "PR_State_SelectFromState";
            SqlDataReader objSDR = objCmd.ExecuteReader();

            if (objSDR.HasRows == true)
            {
                ddlState.DataSource = objSDR;
                ddlState.DataValueField = "StateID";
                ddlState.DataTextField = "StateName";
                ddlState.DataBind();
            }

            ddlState.Items.Insert(0, new ListItem("Select State", "-1"));


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


    #endregion Fill State DropDownList


    #region ddlCountry | SelectedIndexChanged
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        SqlInt32 strCountryID = SqlInt32.Null;

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);


        if (Request.QueryString["ContactID"] == null)
       {
           try
           {
               if (ddlCountry.SelectedIndex > 0)
               {
                   strCountryID = Convert.ToInt32(ddlCountry.SelectedValue);
               }
               if (objConn.State != ConnectionState.Open)
               {
                   objConn.Open();

                   SqlCommand objCmd = objConn.CreateCommand();
                   objCmd.CommandType = CommandType.StoredProcedure;
                   objCmd.CommandText = "PR_State_SelectFromCountry";
                   objCmd.Parameters.AddWithValue("@CountryID", strCountryID);

                   SqlDataReader objSDR = objCmd.ExecuteReader();
                   ddlState.DataSource = objSDR;
                   ddlState.DataValueField = "StateID";
                   ddlState.DataTextField = "StateName";
                   ddlState.DataBind();
                   ddlState.Items.Insert(0, new ListItem("Select State", "-1"));
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

    #endregion ddlCountry | SelectedIndexChanged


    #region  ddlState | SelectedIndexChanged
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlInt32 strStateID = SqlInt32.Null;

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);

        if (Request.QueryString["ContactID"] == null)
        {



            try
            {
                if (ddlState.SelectedIndex > 0)
                {
                    strStateID = Convert.ToInt32(ddlState.SelectedValue);
                }
                if (objConn.State != ConnectionState.Open)
                {
                    objConn.Open();

                    SqlCommand objCmd = objConn.CreateCommand();
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_City_SelecteFormState";
                    objCmd.Parameters.AddWithValue("@StateID", strStateID);

                    SqlDataReader objSDR = objCmd.ExecuteReader();
                    ddlCity.DataSource = objSDR;
                    ddlCity.DataValueField = "CityID";
                    ddlCity.DataTextField = "CityName";
                    ddlCity.DataBind();
                    ddlCity.Items.Insert(0, new ListItem("Select Country", "-1"));

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
    }

    #endregion  ddlState | SelectedIndexChanged


    #region Fill ContactCategory DropdownList

    private void FillContactCategoryDropdownList()
    {
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
       
        try
        {
            if (objConn.State != ConnectionState.Open)
            {


                objConn.Open();

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_ContactCategory_SelectFormDropdownList";
                SqlDataReader objSDR = objCmd.ExecuteReader();
                ddlContactCategory.DataSource = objSDR;
                ddlContactCategory.DataValueField = "ContactCategoryID";
                ddlContactCategory.DataTextField = "ContactCategoryName";
                ddlContactCategory.DataBind();
                objConn.Close();
                ddlContactCategory.Items.Insert(0, new ListItem("Select Contact Category", "-1"));
            }
        }
        catch(Exception ex)
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
    #endregion Fill ContactCategory DropdownList

    #region Fill City DropDownList

    private void FillCityDropDownList()
    {
        
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
       
        try
        {
            if (objConn.State != ConnectionState.Open)
            {


                objConn.Open();

                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_City_SelectFromDropDownList";
                SqlDataReader objSDR = objCmd.ExecuteReader();
                ddlCity.DataSource = objSDR;
                ddlCity.DataValueField = "CityID";
                ddlCity.DataTextField = "CityName";
                ddlCity.DataBind();
                objConn.Close();
                ddlCity.Items.Insert(0, new ListItem("Select City", "-1"));
            }
        }
        catch(Exception ex)
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
    #endregion Fill City DropDownList

    #region Button | Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlInt32 strCountryID = SqlInt32.Null;
        SqlInt32 strStateID = SqlInt32.Null;
        SqlInt32 strCityID = SqlInt32.Null;
        SqlInt32 strContactCategoryID = SqlInt32.Null;
        SqlString strContactName = SqlString.Null;
        SqlString strContactNO = SqlString.Null;
        SqlString strWhatsAppNo = SqlString.Null;
        SqlString strBirthDate = SqlString.Null;
        SqlInt32 strAge = SqlInt32.Null;
        SqlString strBloodGroup = SqlString.Null;
        SqlString strAddress = SqlString.Null;
        SqlString strEmailID = SqlString.Null;
        SqlString strFaceBookID = SqlString.Null;
        SqlString strLinkInID = SqlString.Null;

        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variable

        try
        {


            #region Server Side Validation
            if (txtContactName.Text.Trim() == "" || txtContactNumber.Text.Trim() == "" || txtEmail.Text.Trim() == "" || txtAddress.Text.Trim() == "")
                {
                    lblMassge.Text = "Plasse Enter All Fild";
                    lblMassge.ForeColor = Color.Red;
                    return;
                }


                if (ddlCountry.SelectedIndex > 0)
                {
                    strCountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                }
                if (ddlState.SelectedIndex > 0)
                {
                    strStateID = Convert.ToInt32(ddlState.SelectedValue);
                }
                if (ddlCity.SelectedIndex > 0)
                {
                    strCityID = Convert.ToInt32(ddlCity.SelectedValue);
                }
                if (ddlContactCategory.SelectedIndex > 0)
                {
                    strContactCategoryID = Convert.ToInt32(ddlContactCategory.SelectedValue);
                }
                if (txtContactName.Text.Trim() != "")
                {
                    strContactName = txtContactName.Text.Trim();
                }
                if (txtContactNumber.Text.Trim() != "")
                {
                    strContactNO = txtContactNumber.Text.Trim();
                }
                if (txtWhatsappNo.Text.Trim() != "")
                {
                    strWhatsAppNo = txtWhatsappNo.Text.Trim();
                }
                if (txtBirthDate.Text.Trim() != "")
                {
                    strBirthDate = txtBirthDate.Text.Trim();
                }
                if (txtAge.Text.Trim() != "")
                {
                    strAge = Convert.ToInt32(txtAge.Text.Trim());
                }
                if (txtBloodGroup.Text.Trim() != "")
                {
                    strBloodGroup = txtBloodGroup.Text.Trim();
                }
                if (txtAddress.Text.Trim() != "")
                {
                    strAddress = txtAddress.Text.Trim();
                }
                if (txtEmail.Text.Trim() != "")
                {
                    strEmailID = txtEmail.Text.Trim();
                }
                if (txtFaceBookID.Text.Trim() != "")
                {
                    strFaceBookID = txtFaceBookID.Text.Trim();
                }
                if (txtLinkInId.Text.Trim() != "")
                {
                    strLinkInID = txtLinkInId.Text.Trim();
                }
                #endregion Server Side Validation
                #region Set Connection & Command Object
                if (objConn.State != ConnectionState.Open)
                {
                objConn.Open();
                SqlCommand objCmd = objConn.CreateCommand();


                objCmd.CommandType = CommandType.StoredProcedure;
                
                objCmd.Parameters.AddWithValue("@CountryID", strCountryID);
                objCmd.Parameters.AddWithValue("@StateID ", strStateID);
                objCmd.Parameters.AddWithValue("@CityID  ", strCityID);
                objCmd.Parameters.AddWithValue("@ContactCategoryID  ", strContactCategoryID);
                objCmd.Parameters.AddWithValue("@ContactName ", strContactName);
                objCmd.Parameters.AddWithValue("@ContactNo ", strContactNO);
                objCmd.Parameters.AddWithValue("@WhatsAppNo ", strWhatsAppNo);
                objCmd.Parameters.AddWithValue("@BirthDate", strBirthDate);
                objCmd.Parameters.AddWithValue("@Email", strEmailID);
                objCmd.Parameters.AddWithValue("@Age", strAge);
                objCmd.Parameters.AddWithValue("@Address", strAddress);
                objCmd.Parameters.AddWithValue("@BloodGroup ", strBloodGroup);
                objCmd.Parameters.AddWithValue("@FacebookID ", strFaceBookID);
                objCmd.Parameters.AddWithValue("@LinkedINID  ", strLinkInID);
                #endregion Set Connection & Command Object

                #region Update Data
                if (Request.QueryString["ContactID"]!=null)
                {
                    objCmd.Parameters.AddWithValue("ContactID", Request.QueryString["ContactID"].ToString().Trim());
                    objCmd.CommandText = "PR_Contact_UpdateByPk";
                    objCmd.ExecuteNonQuery();
                    Response.Redirect("~/AddminPanel/Contact/ContactList.aspx");
                }
                #endregion Update Data
                #region Insert Data
                else
                {
                    objCmd.CommandText = "PR_Contact_Insert";
                    objCmd.ExecuteNonQuery();

                    ddlCountry.SelectedIndex = -1;
                    ddlState.SelectedIndex = -1;
                    ddlCity.SelectedIndex = -1;
                    ddlContactCategory.SelectedIndex = -1;
                    txtContactName.Text = "";
                    txtContactNumber.Text = "";
                    txtWhatsappNo.Text = "";
                    txtBirthDate.Text = "";
                    txtBloodGroup.Text = "";
                    txtAge.Text = "";
                    txtAddress.Text = "";
                    txtEmail.Text = "";
                    txtFaceBookID.Text = "";
                    txtLinkInId.Text = "";
                    lblMassge.Text = "Save Data SuccsessFlly";
                }

                #endregion Insert Data


                objConn.Close();


                    
            }
        }
        catch(Exception ex)
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

    #endregion Button | Save

    #region Fill Controls
    private void FillControls(SqlInt32 ContactID)
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
                objCmd.CommandText = "PR_Contact_SelectByPK";
                objCmd.Parameters.AddWithValue("@ContactID", ContactID.ToString().Trim());
                SqlDataReader objSDR = objCmd.ExecuteReader();
                lblMassge.Text = "Edit Mode";
                lblMassge.ForeColor = Color.Green;
                #endregion Set Connection & Command Object
                #region Read The Value And Set The Controals
                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {

                        if (!objSDR["CountryID"].Equals(DBNull.Value))
                        {
                            ddlCountry.SelectedValue = objSDR["CountryID"].ToString().Trim();
                        }
                        if (!objSDR["StateID"].Equals(DBNull.Value))
                        {
                            ddlState.SelectedValue = objSDR["StateID"].ToString().Trim();
                        }
                        if (!objSDR["CityID"].Equals(DBNull.Value))
                        {
                            ddlCity.SelectedValue = objSDR["CityID"].ToString().Trim();
                        }
                        if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                        {
                            ddlContactCategory.SelectedValue = objSDR["ContactCategoryID"].ToString().Trim();
                        }
                        if (!objSDR["ContactName"].Equals(DBNull.Value))
                        {
                            txtContactName.Text = objSDR["ContactName"].ToString().Trim();
                        }
                        if (!objSDR["ContactNo"].Equals(DBNull.Value))
                        {
                            txtContactNumber.Text = objSDR["ContactNo"].ToString().Trim();
                        }
                        if (!objSDR["WhatsAppNo"].Equals(DBNull.Value))
                        {
                            txtWhatsappNo.Text = objSDR["WhatsAppNo"].ToString().Trim();
                        }
                        if (!objSDR["BirthDate"].Equals(DBNull.Value))
                        {
                            txtBirthDate.Text = objSDR["BirthDate"].ToString().Trim();
                        }
                        if (!objSDR["Age"].Equals(DBNull.Value))
                        {
                            txtAge.Text = objSDR["Age"].ToString().Trim();
                        }
                        if (!objSDR["BloodGroup"].Equals(DBNull.Value))
                        {
                            txtBloodGroup.Text = objSDR["BloodGroup"].ToString().Trim();
                        }
                        if (!objSDR["Address"].Equals(DBNull.Value))
                        {
                            txtAddress.Text = objSDR["Address"].ToString().Trim();
                        }
                        if (!objSDR["Email"].Equals(DBNull.Value))
                        {
                            txtEmail.Text = objSDR["Email"].ToString().Trim();
                        }
                        if (!objSDR["FacebookID"].Equals(DBNull.Value))
                        {
                            txtFaceBookID.Text = objSDR["FacebookID"].ToString().Trim();
                        }
                        if (!objSDR["LinkedINID"].Equals(DBNull.Value))
                        {
                            txtLinkInId.Text = objSDR["LinkedINID"].ToString().Trim();
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

    #endregion Fill Controls
}