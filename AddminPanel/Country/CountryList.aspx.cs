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

public partial class AddminPanel_Country_CountryList : System.Web.UI.Page
{
    #region Load Event
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            FillCountryListGridView();
        }
       
    }

    #endregion Load Event


    #region Fill CountryList GridView
    private void FillCountryListGridView()
    {

        #region Local Variabls
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Local Variabls
        try
        {
            #region Set Connection & Command Object

            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();



                SqlCommand objCmd = objConn.CreateCommand();
                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_Country_Table_SelectAll";

                SqlDataReader objSDR = objCmd.ExecuteReader();

                gvCountry.DataSource = objSDR;
                gvCountry.DataBind();
                objConn.Close();


            }
            #endregion Set Connection & Command Object


        }
        catch(Exception ex)
        {
            lblmassge.Text = ex.Message;
            lblmassge.ForeColor = Color.Red;
            
        }
        finally
        {
            if(objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }






        

    }

    #endregion Fill CountryList GridView

    #region gvCountry | RowCommand
    protected void gvCountry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "DeleteRecord")
        {
            if (e.CommandArgument.ToString().Trim() != null)
            {
               DeleteCountre(Convert.ToInt32(e.CommandArgument.ToString().Trim()));
            }
        }

       
    }
    #endregion gvCountry | RowCommand


    #region Delete Country Recored
    private void DeleteCountre(SqlInt32 CountryID)
    {
        #region Localvariable
        SqlConnection objConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AddressBookConnectionString"].ConnectionString);
        #endregion Localvariable
        try
        {
            #region Set Connection & Command Object
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();



                SqlCommand objCmd = objConn.CreateCommand();

                objCmd.CommandType = CommandType.StoredProcedure;
                objCmd.CommandText = "PR_Country_Table_DeleteByPK";
                objCmd.Parameters.AddWithValue("@CountryId", CountryID.ToString().Trim());
                objCmd.ExecuteNonQuery();

               
                objConn.Close();
                FillCountryListGridView();

            }
            #endregion Set Connection & Command Object

        }
        catch(Exception ex)
        {
            lblmassge.Text = ex.Message;
             lblmassge.ForeColor = Color.Red;
            
        }
        finally
        {
            if(objConn.State == ConnectionState.Open)
            {
                objConn.Close();
            }
        }

        #endregion Delete Country Recored


    }
} 