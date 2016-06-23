using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

// using statements required for EF DB access
using COMP2007_S2016_MidTerm_200334364.Models;
using System.Web.ModelBinding;

/*
 * @Author's Name : Kinjal Patel
 * @Date modified : 23 June, 2016
 * @Version : 2.2.4
 * @Description : This Page is for Edit and Delete Todo List
 * */

namespace COMP2007_S2016_MidTerm_200334364
{
    public partial class TodoDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
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
            // populate the form with existing Todo data from the db
            int TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

            // connect to the EF DB
            using (TodoConnection db = new TodoConnection())
            {
                // populate a Todo instance with the TodoID from the URL parameter
                Todo updateTodo = (from todo in db.Todos
                                   where todo.TodoID == TodoID
                                   select todo).FirstOrDefault();

                // map the Todo properties to the form controls
                if (updateTodo != null)
                {
                    TodoNameTextBox.Text = updateTodo.TodoName;
                    NotesTextBox.Text = updateTodo.TodoNotes;
                    //CompletedCheckBox.Checked = updateTodo.Completed;
                }
            }
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to Todo List page
            Response.Redirect("~/TodoList.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            // Use EF to connect to the server
            using (TodoConnection db = new TodoConnection())
            {
                // use the Todo model to create a new Todo object and
                // save a new record
                Todo newTodo = new Todo();

                int TodoID = 0;

                if (Request.QueryString.Count > 0)
                {
                    // get the id from url
                    TodoID = Convert.ToInt32(Request.QueryString["TodoID"]);

                    // get the current Todo from EF DB
                    newTodo = (from todo in db.Todos
                               where todo.TodoID == TodoID
                               select todo).FirstOrDefault();
                }

                // add form data to the new Todo record
                newTodo.TodoName = TodoNameTextBox.Text;
                newTodo.TodoNotes = NotesTextBox.Text;
                newTodo.Completed = CompletedCheckBox.Checked;
                
                // use LINQ to ADO.NET to add / insert new Todo into the database

                // check to see if a new Todo is being added
                if (TodoID == 0)
                {
                    db.Todos.Add(newTodo);
                }

                // save our changes - run an update
                db.SaveChanges();

                // Redirect back to the updated Todos page
                Response.Redirect("~/TodoList.aspx");
            }
        }
    }
}