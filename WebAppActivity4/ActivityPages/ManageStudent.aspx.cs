//InClass4 Assesment Start Kit
//You have been provided all the necessary Entities, Contexts, 
//and Controllers, do not change any of that code.
//You have been provided the Presentation Layer (ManageStudent.aspx file), do not change any of that code.
//You must complete this code behind file where necessary, 
//to complete the proper functionality of the Fetch and Update buttons only,
//(no Clear, Add, or Delete buttons needed).
//All method skeletons needed have been provided, 
//you just have to add code to them, do not create any other methods.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Activity4.BLL;
using Activity4.Entities;
using System.Text.RegularExpressions;

namespace WebAppActivity4.ActivityPages
{
    public partial class ManageStudent : System.Web.UI.Page
    {
        List<string> errormsgs = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //This method is complete, do not change.
            if (!Page.IsPostBack)
            {
                try
                {
                    BindStudentList();
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
        protected Exception GetInnerException(Exception ex)
        {
            //This method is complete, do not change
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
        protected void LoadMessageDisplay(List<string> errormsglist, string cssclass)
        {
            //This method is complete, do not change
            Message.CssClass = cssclass;
            Message.DataSource = errormsglist;
            Message.DataBind();
        }
        protected void BindStudentList()
        {
            try
            {
                //code here
                StudentController sysmgr = new StudentController();
                List<Student> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.StudentName.CompareTo(y.StudentName));
                StudentList.DataSource = info;
                StudentList.DataTextField = nameof(Student.StudentName);
                StudentList.DataValueField = nameof(Student.StudentID);
                StudentList.DataBind();
                StudentList.Items.Insert(0, "select...");

            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
        protected void BindProgramList()
        {
            try
            {
                //Add code here to get all the Program records and bind to the "ProgramList" dropdown
                ProgramController sysmgr = new ProgramController();
                List<Program> info = null;
                info = sysmgr.List();
                info.Sort((x, y) => x.ProgramName.CompareTo(y.ProgramName));
                ProgramList.DataSource = info;
                ProgramList.DataTextField = nameof(Program.ProgramName);
                ProgramList.DataValueField = nameof(Program.ProgramID);
                ProgramList.DataBind();
                ProgramList.Items.Insert(0, "select...");
            }
            catch (Exception ex)
            {
                errormsgs.Add(GetInnerException(ex).ToString());
                LoadMessageDisplay(errormsgs, "alert alert-danger");
            }
        }
        protected void Fetch_Click(object sender, EventArgs e)
        {
            Message.DataSource = null;
            Message.DataBind();
            if (StudentList.SelectedIndex == 0)
            {
                errormsgs.Add("Please select a student first.");
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    StudentController sysmgr01 = new StudentController();
                    Student info01 = null;
                    info01 = sysmgr01.FindByPKID(int.Parse(StudentList.SelectedValue));
                    StudentID.Text = info01.StudentID.ToString();
                    StudentName.Text = info01.StudentName;
                    ProgramList.Text = info01.ProgramID.ToString();
                    Credits.Text = info01.Credits.ToString();
                    EmergencyPhoneNumber.Text = info01.EmergencyPhoneNumber;
                    BindProgramList();
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
        protected void Validation(object sender, EventArgs e)
        {
            string input = EmergencyPhoneNumber.Text;
            Match match = Regex.Match(input, @"[(][1-9][0-9][0-9][)][ ][1-9][0-9][0-9][-][0-9][0-9][0-9][0-9]$");
            if (!match.Success)
            {
                errormsgs.Add("Emergency Phone Number must be like (123) 123-1234");
            }
            //Complete the code in this method to validate the following with codebehind
            //Student Name is required
            //Program is required
            //Credits is required and has to be a double between 0.00 and 100.00
            //Validation for the EmergencyPhoneNumber has been provided above, do not change
            if (string.IsNullOrEmpty(StudentName.Text))
            {
                errormsgs.Add("Student name is required");
            }
            if (ProgramList.SelectedIndex == 0)
            {
                errormsgs.Add("Program is required");
            }
            if (string.IsNullOrEmpty(Credits.Text))
            {
                errormsgs.Add("Credits are required");
            }
            double credits = 0;
            if (!string.IsNullOrEmpty(Credits.Text))
            {
                if (double.TryParse(Credits.Text, out credits))
                {
                    if (credits < 0.00 || credits > 100.00)
                    {
                        errormsgs.Add("Credits must be between 0.00 and 200.00");
                    }
                }
                else
                {
                    errormsgs.Add("Credits must be a real number");
                }
            }
        }
        protected void UpdateStudent_Click(object sender, EventArgs e)
        {
            Message.DataSource = null;
            Message.DataBind();
            if (string.IsNullOrEmpty(StudentID.Text))
            {
                errormsgs.Add("Please select a student and Fetch first.");
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                Validation(sender, e);
            }
            if (errormsgs.Count > 0)
            {
                LoadMessageDisplay(errormsgs, "alert alert-info");
            }
            else
            {
                try
                {
                    //Add code here to Update the ONE Student record, 
                    //do not change the code above, only add code in this "try" structure
                    StudentController sysmgr = new StudentController();
                    Student item = new Student();
                    item.StudentID = int.Parse(StudentID.Text);
                    item.StudentName = StudentName.Text.Trim();               
                    item.ProgramID = int.Parse(ProgramList.SelectedValue);
                    item.Credits = int.Parse(Credits.Text.Trim());
                    item.EmergencyPhoneNumber = EmergencyPhoneNumber.Text.Trim();
                    int rowsaffected = sysmgr.Update(item);
                    if (rowsaffected > 0)
                    {
                        errormsgs.Add("Student has been updated");
                        LoadMessageDisplay(errormsgs, "alert alert-success");
                    }
                    else
                    {
                        errormsgs.Add("Record was not found");
                        LoadMessageDisplay(errormsgs, "alert alert-warning");
                    }
                }
                catch (Exception ex)
                {
                    errormsgs.Add(GetInnerException(ex).ToString());
                    LoadMessageDisplay(errormsgs, "alert alert-danger");
                }
            }
        }
    }
}