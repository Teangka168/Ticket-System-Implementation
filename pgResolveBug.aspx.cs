using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using BRC.CNN;
using WebApp.Structure;
using WebApp.BLL;
using BRC.Utilities;

namespace WebApp.Interface
{
    public partial class pgResolveBug : System.Web.UI.Page
    {

        private SqlConnection _sqlConn = new SqlConnection();
        private int _intTicketTypeID = 0;
        private String _strTicketTypeName;
        private String _currentUserName;

        private WorkingInfo _objWorkingEdit = new WorkingInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["st"].ToString() != "")
                {
                    string[] strValues = Request.QueryString["st"].ToString().Split(',');
                    _intTicketTypeID = int.Parse(strValues[0]);
                    _strTicketTypeName = strValues[1];
                }

                if (Request.QueryString["us"].ToString() != "")
                {
                    _currentUserName = Request.QueryString["us"].ToString();
                }

                new DBHelper().InitiateWebConnection(out _sqlConn);
                WebCommon.CheckSession(this, (int)WebCommon.WebPageEnum.FromCurrentPage, _sqlConn);

                if (!IsPostBack)
                {
                    loadSeverity();
                    loadPriority();   
                }

                loadAllTask();
            }
            catch { }
            finally
            {
                new DBHelper().CloseWebConnection(_sqlConn);
            }
        }

        protected void dtgBugList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex != -1)
                {

                    var btnView = new Button();
                    btnView.ID = e.Row.Cells[0].Text.ToString();
                    btnView.Text = "Resolve";
                    btnView.CssClass = "btnNomal";
                    btnView.Click += new EventHandler(btnView_Click);
                    e.Row.Cells[1].Controls.Add(btnView);

                }

                e.Row.Cells[0].Visible = false;
            }
            catch { }
            finally
            {
                new DBHelper().CloseWebConnection(_sqlConn);
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                var btView = sender as Button;
                lblWorkingID.Text = btView.ID.ToString();
                _objWorkingEdit = Working.getWorkingInfo(int.Parse(lblWorkingID.Text.ToString()), null, _sqlConn);

                txtViewTitle.Text = _objWorkingEdit.title;
                txtViewDescription.Text = _objWorkingEdit.description;

                ddlViewSeverity.SelectedIndex = _objWorkingEdit.severity_id - 1;
                ddlViewPriority.SelectedIndex = _objWorkingEdit.priority_id - 1;

                mpeViewTask.Show();
            }
            catch
            {
            }
            finally
            {
                new DBHelper().CloseWebConnection(_sqlConn);
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                new DBHelper().InitiateWebConnection(out _sqlConn);
                _objWorkingEdit = Working.getWorkingInfo(int.Parse(lblWorkingID.Text.ToString()), null, _sqlConn);

                if (_objWorkingEdit.edit_by != null && _objWorkingEdit.edit_by != "")
                {

                    _objWorkingEdit.edit_by += ", " + (_objWorkingEdit.edit_time + 1) + ": " + txtRosolvedBy.Text.Trim();
                    _objWorkingEdit.edit_description = _objWorkingEdit.edit_date.ToString("dd-MMM-yyyy") + ": " + _objWorkingEdit.edit_description + ", " + txtRosolvedDate.Text.Trim().ToString() + ": " + txtRosolvedDescription.Text.Trim();
                    
                }
                else
                {

                    _objWorkingEdit.edit_by = txtRosolvedBy.Text.Trim();
                    _objWorkingEdit.edit_description = txtRosolvedDescription.Text;
                   
                }

                _objWorkingEdit.is_edit = true;
                _objWorkingEdit.edit_time += 1;
                _objWorkingEdit.status_id = 2;
                _objWorkingEdit.edit_date = DateTime.Parse(txtRosolvedDate.Text.Trim().ToString());

                if (Working.UpdateResolved(_objWorkingEdit, null, _sqlConn))
                {

                    txtRosolvedBy.Text = "";
                    txtRosolvedDate.Text = "";
                    txtRosolvedDescription.Text = "";
                    loadAllTask();
                }
            }
            catch { }
            finally
            {
                new DBHelper().CloseWebConnection(_sqlConn);
            }
        }

        private void loadAllTask()
        {
            try
            {
                DataTable dtData = Working.LoadWorkingListSolve(_strTicketTypeName, _sqlConn);

                dtgBugList.DataSource = dtData;
                dtgBugList.DataBind();
            }
            catch { }
        }

        private void loadSeverity()
        {
            DataTable dtData = new DataTable();
            try
            {
                string strSQL =
"select id as severity_id\r\n" +
",name as severity_name\r\n" +
"from tblSeverity\r\n" +
"where is_active=1\r\n" +
"order by sort_order\r\n";

                SqlCommand sqlcom = _sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.CommandTimeout = 0;
                dtData = Common.ExecuteTable(sqlcom);

                ddlViewSeverity.DataSource = dtData;
                ddlViewSeverity.DataValueField = "severity_id";
                ddlViewSeverity.DataTextField = "severity_name";
                ddlViewSeverity.DataBind();
            }
            catch (Exception exp)
            {
                CallingErrorForm.GetErrorForm("Can not load Severity.", exp);
            }
        }

        private void loadPriority()
        {
            DataTable dtData = new DataTable();
            try
            {
                string strSQL =
"select id as priority_id\r\n" +
",name as priority_name\r\n" +
"from tblPriority\r\n" +
"where is_active=1\r\n" +
"order by sort_order\r\n";

                SqlCommand sqlcom = _sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.CommandTimeout = 0;
                dtData = Common.ExecuteTable(sqlcom);

                ddlViewPriority.DataSource = dtData;
                ddlViewPriority.DataValueField = "priority_id";
                ddlViewPriority.DataTextField = "priority_name";
                ddlViewPriority.DataBind();

            }
            catch (Exception exp)
            {
                CallingErrorForm.GetErrorForm("Can not load priority.", exp);
            }
        }


    }
}