﻿using System;
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

public partial class AddminPanel_ContactCategory_ContactCategoryList : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            FillGridView();
            if(Request.QueryString["ContactCategoryID"]!= null)
            {
                FillControls(Convert.ToInt32(Request.QueryString["ContactCategoryID"])); 
            }
        }
    }

    #endregion Load Event

    #region Fill Grid View
    private void FillGridView()
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
                SqlCommand objCmd = new SqlCommand();
                objCmd.Connection = objConn;
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_ContactCaegory_Table_SelectAll";

                SqlDataReader objSDR = objCmd.ExecuteReader();

                gvCountry.DataSource = objSDR;
                gvCountry.DataBind();





             objConn.Close();
            }
            #endregion Set Connection & Command Object
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
    #endregion Fill Grid View

    #region Button|Save
    protected void btnSave_Click(object sender, EventArgs e)
    {
        #region Local Variable
        SqlString strContactCategory = SqlString.Null;
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
                        strContactCategory = txtContactCategory.Text.Trim();
                        objCmd.Parameters.AddWithValue("@ContactCategoryName", strContactCategory);
                       

                    #region Update Data
                        if (Request.QueryString["ContactCategoryID"]!=null)
                        {
                            objCmd.Parameters.AddWithValue("@ContactCategoryID", Request.QueryString["ContactCategoryID"].ToString().Trim());
                            objCmd.CommandText="PR_ContactCategory_Table_UpdateByPK" ;
                            objCmd.ExecuteNonQuery();

                            Response.Redirect("~/AddminPanel/ContactCategory/ContactCategoryList.aspx");

                        }
                        #endregion Update Data

                    #region Insert Data
                        else{
                            objCmd.CommandText = "PR_ContactCategory_Table_Insert";
                             objCmd.ExecuteNonQuery();

                            txtContactCategory.Text = "";
                            lblMassge.Text = "ADD Data SuccessFully";
                        }
                      #endregion Insert Data

                        objConn.Close();
                   #endregion Set Connection & Command Object
                        FillGridView();
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
    #endregion Button|Save

    #region gvCountry | RowCommand
    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
           if(e.CommandArgument != null)
           {
               DeleteContactCategory(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
           }
        }
    }
    #endregion gvCountry | RowCommand


    #region Delete Contact Category
    private void DeleteContactCategory(SqlInt32 ContactCategoryID)
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
                objCmd.CommandText = "PR_ContactCategory_Table_DeleteByPK";
                objCmd.Parameters.AddWithValue("@ContactCategoryId", ContactCategoryID.ToString().Trim());
                objCmd.ExecuteNonQuery();


                objConn.Close();
                FillGridView();

            }
            #endregion Set Connection & Command Object


        }
        catch (Exception ex)
        {
            lblMassge.Text = ex.Message;
            lblMassge.ForeColor = Color.Red;

        }
        finally
        {
            if (objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }
    }
    #endregion Delete Contact Category


    #region Fill Controls
    private void FillControls(SqlInt32 ContactCategoryID)
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
                objCmd.CommandText = "PR_ContactCategory_SelectByPK";
                objCmd.Parameters.AddWithValue("@ContactCategoryID", ContactCategoryID.ToString().Trim());
                SqlDataReader objSDR = objCmd.ExecuteReader();
                lblMassge.Text = "Edit Mode";
                lblMassge.ForeColor = Color.Green;
             #endregion Set Connection & Command Object

                #region Read The Value And Set The Controals
                if (objSDR.HasRows)
                {
                    while (objSDR.Read())
                    {
                        if (!objSDR["ContactCategoryID"].Equals(DBNull.Value))
                        {
                            txtContactCategory.Text = objSDR["ContactCategoryName"].ToString().Trim();
                        }
                       
                        break;
                    }
                }
                else
                {
                    lblMassge.Text = "Data Is Not Available";
                }

            }
            #endregion Read The Value And Set The Controals
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