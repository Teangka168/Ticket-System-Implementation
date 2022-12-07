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
    public partial class pgTask : System.Web.UI.Page
    {

        private SqlConnection _sqlConn = new SqlConnection();
        private int _intTickeTypeID=0;
        private String _strTickeTypeName;
        private String _currentUserName;
        private int userGroupID = 0;

        private WorkingInfo _objWorkingEdit = new WorkingInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["st"].ToString() != "")
                {
                    string[] strValues = Request.QueryString["st"].ToString().Split(',');
                    _intTickeTypeID = int.Parse(strValues[0]);
                    _strTickeTypeName = strValues[1];
                }

                if (Request.QueryString["us"].ToString() != "")
                {
                    _currentUserName = Request.QueryString["us"].ToString();
                }
                if (Request.QueryString["ugid"].ToString() != "")
                {
                    userGroupID = int.Parse(Request.QueryString["ugid"].ToString());
                }

                new DBHelper().InitiateWebConnection(out _sqlConn);
                WebCommon.CheckSession(this, (int)WebCommon.WebPageEnum.FromCurrentPage, _sqlConn);

                if (!IsPostBack)
                {
                    loadSeverity();
                    loadPriority();


                    #region PermisionUser
                    if (userGroupID == 1)
                    {                        
                    }
                    else if (userGroupID == 2)
                    {
                        btnAddTask.Enabled = false;
                    }
                    else if (userGroupID == 3)
                    {
                        btnAddTask.Enabled = false;
                    }
                    else if (userGroupID == 4)
                    {
                        btnAddTask.Enabled = false;
                    }
                    else
                    {
                        btnAddTask.Enabled = false;
                    }
                    #endregion
                }

                loadAllTask();
            }
            catch { }
            finally
            {
                new DBHelper().CloseWebConnection(_sqlConn);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                new DBHelper().InitiateWebConnection(out _sqlConn);

                WorkingInfo objWorking = new WorkingInfo();

                objWorking.title = txtTitle.Text;
                objWorking.description = txtDescrition.Text;
                objWorking.severity_id = int.Parse(ddlSeverity.SelectedValue.ToString());
                objWorking.priority_id = int.Parse(ddlPriority.SelectedValue.ToString());
                objWorking.status_id = 1;                                
                objWorking.create_by = _currentUserName;
                objWorking.create_date = Common.GetCurrentDate(_sqlConn, null);
                objWorking.is_active = true;
                if (Working.Insert(objWorking, null, _sqlConn))
                {
                    txtTitle.Text = "";
                    txtDescrition.Text = "";

                    loadAllTask();
                }
                
            }
            catch { }
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

                _objWorkingEdit.edit_by = "";
                _objWorkingEdit.edit_description = "";
                _objWorkingEdit.is_edit = true;
                _objWorkingEdit.title = txtViewTitle.Text;
                _objWorkingEdit.description = txtViewDescription.Text;
                _objWorkingEdit.severity_id = int.Parse(ddlViewSeverity.SelectedValue.ToString());
                _objWorkingEdit.priority_id = int.Parse(ddlViewPriority.SelectedValue.ToString());
                _objWorkingEdit.status_id = 1;
                _objWorkingEdit.edit_by = "";
                _objWorkingEdit.edit_date = Common.GetCurrentDate(_sqlConn, null);
                _objWorkingEdit.edit_time =0;
              
                if (Working.Update(_objWorkingEdit, null, _sqlConn))
                {
                    loadAllTask();
                }
            }
            catch { }
            finally
            {
                new DBHelper().CloseWebConnection(_sqlConn);
            }

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                new DBHelper().InitiateWebConnection(out _sqlConn);
                _objWorkingEdit = Working.getWorkingInfo(int.Parse(lblWorkingID.Text.ToString()), null, _sqlConn);

                _objWorkingEdit.is_active = false;

                if (Working.Delete(_objWorkingEdit, null, _sqlConn))
                {
                    loadAllTask();
                }
            }
            catch { }
            finally
            {
                new DBHelper().CloseWebConnection(_sqlConn);
            }
        }

        protected void dtgTask_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                #region PermisionUser
                if (userGroupID == 1)
                {

                    if (e.Row.RowIndex != -1)
                    {
                        var btnView = new Button();
                        btnView.ID = e.Row.Cells[0].Text.ToString();
                        btnView.Text = "Edit Bug";
                        btnView.CssClass = "btnNomal";
                        btnView.Click += new EventHandler(btnView_Click);
                        e.Row.Cells[1].Controls.Add(btnView);
                    }

                    e.Row.Cells[0].Visible = false;
                }

                #endregion

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


//////////////////////////////////////////////////////////////////////////////////////

        private void loadAllTask()
        {
            try
            {
                DataTable dtData = Working.LoadWorkingList(_strTickeTypeName, _sqlConn);

                dtgTask.DataSource = dtData;
                dtgTask.DataBind();
               
            }
            catch { }
        }

        private void loadSeverity()
        {
            DataTable dtData = new DataTable();
            try
            {
                string strSQL =
"select id as Severity_id\r\n" +
",name as Severity_name\r\n" +
"from tblSeverity\r\n" +
"where is_active=1\r\n" +
"order by sort_order\r\n";

                SqlCommand sqlcom = _sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.CommandTimeout = 0;
                dtData = Common.ExecuteTable(sqlcom);

                ddlSeverity.DataSource = dtData;
                ddlSeverity.DataValueField = "Severity_id";
                ddlSeverity.DataTextField = "Severity_name";
                ddlSeverity.DataBind();

                ddlViewSeverity.DataSource = dtData;
                ddlViewSeverity.DataValueField = "Severity_id";
                ddlViewSeverity.DataTextField = "Severity_name";
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
"select id as status_id\r\n" +
",name as status_name\r\n" +
"from tblPriority\r\n" +
"where is_active=1\r\n" +
"order by sort_order\r\n";

                SqlCommand sqlcom = _sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.CommandTimeout = 0;
                dtData = Common.ExecuteTable(sqlcom);

                ddlPriority.DataSource = dtData;
                ddlPriority.DataValueField = "Priority_id";
                ddlPriority.DataTextField = "Priority_name";
                ddlPriority.DataBind();

                ddlViewPriority.DataSource = dtData;
                ddlViewPriority.DataValueField = "Priority_id";
                ddlViewPriority.DataTextField = "Priority_name";
                ddlViewPriority.DataBind();

            }
            catch (Exception exp)
            {
                CallingErrorForm.GetErrorForm("Can not load Priority.", exp);
            }
        }


    }
}