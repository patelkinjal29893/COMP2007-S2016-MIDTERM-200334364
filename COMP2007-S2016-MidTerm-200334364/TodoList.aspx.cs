using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements that are required to connect to EF DB
using COMP2007_S2016_MidTerm_200334364.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

/*
 * @Author's Name : Kinjal Patel
 * @Date modified : 23 June, 2016
 * @Version : 2.2.4
 * @Description : This Page is for Display All Todo List
 * */

namespace COMP2007_S2016_MidTerm_200334364
{
    public partial class TodoList : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the Todo grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoID";
                Session["SortDirection"] = "ASC";
                // Get the Todo data
                this.GetTodos();
            }
        }

        /**
         * <summary>
         * This method gets the Todos data from the DB
         * </summary>
         * 
         * @method GetTodos
         * @returns {void}
         */
        protected void GetTodos()
        {
            string sortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

            // connect to EF
            using (TodoConnection db = new TodoConnection())
            {
                // query the Todos Table using EF and LINQ
                var Todos = (from allTodos in db.Todos
                             select allTodos);

                // bind the result to the GridView
                TodoGridView.DataSource = Todos.AsQueryable().OrderBy(sortString).ToList();
                TodoGridView.DataBind();
            }
        }

        protected void TodoGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

            // get the selected TodoID using the Grid's DataKey Collection
            int TodoID = Convert.ToInt32(TodoGridView.DataKeys[selectedRow].Values["TodoID"]);

            // use EF to find the selected Todo from DB and remove it
            using (TodoConnection db = new TodoConnection())
            {
                Todo deleteTodo = (from todoRecords in db.Todos
                                   where todoRecords.TodoID == TodoID
                                   select todoRecords).FirstOrDefault();

                // perform the removal in the DB
                db.Todos.Remove(deleteTodo);
                db.SaveChanges();

                // refresh the grid
                this.GetTodos();
            }
        }

        protected void TodoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            TodoGridView.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetTodos();
        }

        protected void TodoGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sort by
            Session["SortColumn"] = e.SortExpression;

            // refresh the grid
            this.GetTodos();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void TodoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // check to see if the click is on the header row
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < TodoGridView.Columns.Count; index++)
                    {
                        if (TodoGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }

        protected void CheckBoxCompleted_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
            int index = row.RowIndex;
            CheckBox cb1 = (CheckBox)TodoGridView.Rows[index].FindControl("CheckBoxCompleted");
            string checkboxstatus;
            if (cb1.Checked == true)
                checkboxstatus = "YES";
            else if (cb1.Checked == false)
                checkboxstatus = "NO";

            cb1.Checked = ((CheckBox)sender).Checked;
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set new page size
            TodoGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetTodos();
        }
    }
}