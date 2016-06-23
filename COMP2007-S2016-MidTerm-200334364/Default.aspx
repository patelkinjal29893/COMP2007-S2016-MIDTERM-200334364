<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="COMP2007_S2016_MidTerm_200334364.Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>To Do List </h1>                

                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover" 
                    ID="TodoGridView" AutoGenerateColumns="False" DataKeyNames="TodoID">
                    <Columns>
                        <asp:BoundField DataField="TodoID" HeaderText="Todo ID" Visible="true" SortExpression="TodoID" />
                        <asp:BoundField DataField="TodoName" HeaderText="Todo Name" Visible="true" SortExpression="TodoName"/>                                                 
                        <asp:BoundField DataField="TodoNotes" HeaderText="Todo Notes" Visible="true" SortExpression="TodoNotes"/>                           
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
