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

namespace COMP2007_S2016_MidTerm_200334364
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the Todo grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "TodoID";
                Session["SortDirection"] = "ASC";
                // Get the Todo data
                this.GetTodo();
            }
        }
        protected void GetTodo()
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
    }
}