﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Content/AddressBook.master" AutoEventWireup="true" CodeFile="StateList.aspx.cs" Inherits="AddminPanel_State_StateList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



    <div>
       <div class="col-12 ">
         <div class="row bg-info">
             <div class="col-6">
                    <h1 class=" pt-2">State List</h1>
             </div>
             <div class="col-6 float-left pt-3">
                <asp:HyperLink ID="hlAddState" CssClass="btn btn-sm  btn-primary   float-right" NavigateUrl="~/AddminPanel/State/StateAddEdit.aspx" runat="server">Add Country</asp:HyperLink>   
            </div>
         </div>
           <div class="row">
               <div class="col-md-12">
                   <asp:Label ID="lblmassge" runat="server"/>
               </div>
           </div>
     <div class="mt-3">
        <asp:GridView ID="gvState" runat="server" CssClass=" w-100 table text-center  table-hover shadow" AutoGenerateColumns="false" OnRowCommand="gvState_RowCommand">
            <Columns>
                <asp:BoundField DataField ="StateID" HeaderText="ID"/>
                <asp:BoundField DataField ="CountryName" HeaderText="Country"/>
                <asp:BoundField DataField ="StateName" HeaderText="State"/>
                <asp:BoundField DataField ="StateCode" HeaderText="StateCode"/>
               <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        
                        <asp:HyperLink ID="hlEdit" runat="server" Text="Edit" CssClass="btn btn-success btn-sm btn-block " NavigateUrl='<%# "~/AddminPanel/State/StateAddEdit.aspx?StateID="+Eval("StateID").ToString().Trim() %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate >
                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-danger btn-sm btn-block "  CommandName="DeleteRecord" CommandArgument ='<%#Eval("StateID").ToString() %>'   />
                    </ItemTemplate>
                </asp:TemplateField>
               
            </Columns>
        </asp:GridView>
     </div>

           </div>
    </div>    










</asp:Content>

