﻿<%@ Page Title="Todo List" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TodoList.aspx.cs" Inherits="COMP2007_S2016_MidTerm_200334364.TodoList" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Todo List</h1>
                <a href="TodoDetails.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i> Add Todo</a>

                <label for="PageSizeDropDownList">Records per page: </label>
                <asp:DropDownList ID="PageSizeDropDownList" runat="server" AutoPostBack="true" 
                    CssClass="btn btn-default btn-sm dropdown-toggle" OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                    <asp:ListItem Text="3" Value="3" />
                    <asp:ListItem Text="5" Value="5" />
                    <asp:ListItem Text="All" Value="100" />
                </asp:DropDownList>

                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover" 
                    ID="TodoGridView" AutoGenerateColumns="False" DataKeyNames="TodoID" OnRowDeleting="TodoGridView_RowDeleting"
                    AllowPaging="True" PageSize="3" OnPageIndexChanging="TodoGridView_PageIndexChanging"
                    AllowSorting="True" OnSorting="TodoGridView_Sorting" OnRowDataBound="TodoGridView_RowDataBound"  PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="TodoID" HeaderText="Todo ID" Visible="true" SortExpression="TodoID" />
                        <asp:BoundField DataField="TodoName" HeaderText="Todo Name" Visible="true" SortExpression="TodoName"/>                                                                        
                        <asp:BoundField DataField="TodoNotes" HeaderText="Todo Notes" Visible="true" SortExpression="TodoNotes"/>                          
                        <asp:TemplateField HeaderText="Completed" ShowHeader="False" Visible="true" SortExpression="Completed">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxCompleted" runat="server" CausesValidation="false" AutoPostBack="true" OnCheckedChanged="CheckBoxCompleted_CheckedChanged" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                                            
                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" NavigateUrl="~/TodoDetails.aspx.cs"
                            DataNavigateUrlFields="TodoID" DataNavigateUrlFormatString="TodoDetails.aspx?TodoID={0}" 
                            ControlStyle-CssClass="btn btn-primary btn-sm" ControlStyle-ForeColor="White">
<ControlStyle CssClass="btn btn-primary btn-sm" ForeColor="White"></ControlStyle>
                        </asp:HyperLinkField>
                        <asp:CommandField HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete" ShowDeleteButton="true"
                            ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
