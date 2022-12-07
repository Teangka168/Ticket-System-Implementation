using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using BRC.CNN;
using BRC.Utilities;
using WebApp.BLL;
using WebApp.Utilities;

namespace WebApp
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {

        public DateTime _dt = DateTime.Now;
        public SqlConnection _sqlConn = new SqlConnection();
        private int userGroupID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string strUserID = "";

            try
            {
                new DBHelper().InitiateWebConnection(out _sqlConn);
                strUserID = LoadUserName(_sqlConn);
                userGroupID = loadGroupUserID(_sqlConn);
            }
            catch (Exception ex)
            {
                Response.Write("");
            }
            finally
            {
                new DBHelper().CloseWebConnection(_sqlConn);
            }

            if (!IsPostBack)
            {
                lblUserID.Text = "| [ " + (strUserID ?? "") + "]";
                getMenu(userGroupID);

                #region PermisionUser
                if (userGroupID == 1)
                {
                    dmMENUTicket.Items.Remove(dmMENUTicket.FindItem("NewTicketType"));
                    dmMENUTicket.Items.Remove(dmMENUTicket.FindItem("CreateUser"));                   
                }
                else if (userGroupID == 2)
                {
                    dmMENUTicket.Items.Remove(dmMENUTicket.FindItem("NewTicketType"));
                    dmMENUTicket.Items.Remove(dmMENUTicket.FindItem("CreateUser"));                    
                }
                else if (userGroupID == 3)
                {
                    dmMENUTicket.Items.Remove(dmMENUTicket.FindItem("CreateUser"));
                }
                else if (userGroupID == 4)
                {
                    dmMENUTicket.Items.Remove(dmMENUTicket.FindItem("NewTicketType"));                   
                }
                else
                {

                }
                #endregion

            }
        }

        protected void btnLogOut_Click(object sender, EventArgs e)
        {
            if (this.Session != null)
            {                
                this.Session.Abandon();
            }

            Response.Redirect("~/pgLogin.aspx");
        }

        protected void dmMENUTicket_MenuItemClick(object sender, System.Web.UI.WebControls.MenuEventArgs e)
        {
            try
            {
                if (this.dmMENUTicket.SelectedItem.Value == "NewTicketType")
                {
                    Response.Redirect("~/Interface/pgTicketType.aspx?PageID=2" + "&us=" + LoadUserName(_sqlConn) + "&ugid=" + userGroupID);
                }

                else if (this.dmMENUTicket.SelectedItem.Value == "CreateUser")
                {
                    Response.Redirect("~/Interface/pgNewUser.aspx?PageID=3" + "&us=" + LoadUserName(_sqlConn) + "&ugid=" + userGroupID);
                }

                else if (this.dmMENUTicket.SelectedItem.Parent.Value.ToString() == "Ticket Tracking |")
                {
                    Response.Redirect("~/Interface/pgTask.aspx?pt=" + dmMENUTicket.SelectedItem.Parent.Target.ToString() + "&st=" + dmMENUTicket.SelectedItem.Target.ToString() + "&us=" + LoadUserName(_sqlConn) + "&ugid=" + userGroupID);
                }
                else if (this.dmMENUTicket.SelectedItem.Parent.Value.ToString() == "Resolve Bug |")
                {
                    Response.Redirect("~/Interface/pgResolveBug.aspx?pt=" + dmMENUTicket.SelectedItem.Parent.Target.ToString() + "&st=" + dmMENUTicket.SelectedItem.Target.ToString() + "&us=" + LoadUserName(_sqlConn) + "&ugid=" + userGroupID);
                }

            }
            catch { }
        }

        private void getMenu(int intGroupID)
        {
            try
            {
                DataSet dsParentMenu = getPARENTMENU(intGroupID);
                DataRowCollection drcParentMenu = dsParentMenu.Tables[0].Rows;

                DataSet dsChildMenuAll = getCHILDMENU(intGroupID);
                DataTable drcChildMenuAll = dsChildMenuAll.Tables[0];               

                MenuItem mainMENUITEM;
                MenuItem childMENUITEM;
                foreach (DataRow drParentMenu in drcParentMenu)
                {
                    mainMENUITEM = new MenuItem(drParentMenu["short_name"].ToString());
                    mainMENUITEM.Target = drParentMenu["id"].ToString() + "," + drParentMenu["short_name"].ToString();
                    mainMENUITEM.Selectable = false;
                    dmMENUTicket.Items.Add(mainMENUITEM);
                    DataRow[] drcChildMenu = drcChildMenuAll.Select("hotelproperty_id=" + drParentMenu["id"].ToString());
                    foreach (DataRow drSUBMENUITEM in drcChildMenu)
                    {
                        childMENUITEM = new MenuItem(drSUBMENUITEM["name"].ToString());
                        childMENUITEM.Target = drSUBMENUITEM["id"].ToString() + "," + drSUBMENUITEM["name"].ToString();
                        mainMENUITEM.ChildItems.Add(childMENUITEM);                        
                    }
                }
            }
            catch { }
        }

        private DataSet getPARENTMENU(int intGroupID)
        {
            DataSet dsTEMP = new DataSet();
            try
            {
                string strSQL =
"--declare @group_id int\r\n" +
"--set @group_id=1\r\n" +
"\r\n" +
"select hp.id\r\n" +
",hp.short_name + ' |' as short_name\r\n" +
",hp.full_name \r\n" +
",hp.is_active\r\n" +
",hp.sort_order \r\n" +
"from tblhotelproperty hp\r\n" +
"left join tblPermission p on hp.id=p.menu_parent_id\r\n" +
"where is_active=1\r\n" +
"and p.group_id=@group_id\r\n" +
"group by \r\n" +
"hp.id\r\n" +
",hp.short_name\r\n" +
",hp.full_name\r\n" +
",hp.is_active\r\n" +
",hp.sort_order \r\n" +
"order by sort_order\r\n";

                SqlCommand sqlcom = _sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                sqlcom.Parameters.Add("@group_id", SqlDbType.Int).Value = intGroupID;
                da.Fill(dsTEMP, "tablePARENT");
            }
            catch { }
            return dsTEMP;
        }

        private DataSet getCHILDMENU(int intGroupID)
        {
            DataSet dsTEMP = new DataSet();
            try
            {
                string strSQL =
"--declare @group_id int\r\n" +
"--set @group_id=1\r\n" +
"\r\n" +
"select st.id\r\n" +
",st.[name]\r\n" +
",st.description\r\n" +
",st.is_active\r\n" +
",st.sort_order\r\n" +
",st.hotelproperty_id\r\n" +
",st.create_by\r\n" +
",st.create_date\r\n" +
"from tblsection st\r\n" +
"left join tblhotelproperty hp on st.hotelproperty_id=hp.id\r\n" +
"left join tblPermission p on st.id=p.menu_child_id \r\n" +
"and  hp.id=p.menu_parent_id\r\n" +
"where st.is_active=1\r\n" +
"and p.group_id=@group_id\r\n" +
"group by \r\n" +
"st.id\r\n" +
",st.[name]\r\n" +
",st.description\r\n" +
",st.is_active\r\n" +
",st.sort_order\r\n" +
",st.hotelproperty_id\r\n" +
",st.create_by\r\n" +
",st.create_date \r\n" +
"order by sort_order\r\n";

                SqlCommand sqlcom = _sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(sqlcom);
                sqlcom.Parameters.Add("@group_id", SqlDbType.Int).Value = intGroupID;
                da.Fill(dsTEMP, "tableCHILD");
            }
            catch { }
            return dsTEMP;

        }

        private int loadGroupUserID(SqlConnection sqlConn)
        {
            int intGroupID = 0;

            int intEmp_id = int.Parse(this.Session["User_id"].ToString());

            DataTable dtEmployeeData = new EmployeePersonalInformation().LoadEmployeePersonalInformation(intEmp_id, sqlConn);
            foreach (DataRow dr in dtEmployeeData.Rows)
            {
                intGroupID = int.Parse(dr["group_id"].ToString());
            }
            return intGroupID;
        }

        private string LoadUserName(SqlConnection sqlConn)
        {
            string strUserName = "";
            int intUS_id = int.Parse(this.Session["us_id"].ToString());

            DataTable dtEmployeeData = LoadUserName(intUS_id, sqlConn);
            foreach (DataRow dr in dtEmployeeData.Rows)
            {
                strUserName = dr["full_name"].ToString();
            }
            return strUserName;
        }
        private DataTable LoadUserName(int intUserID, SqlConnection sqlConn)
        {
            DataTable dtResult = new DataTable();
            try
            {
                string strSQL =
                "select [user_id]\r\n" +
                ",[user_name] as full_name\r\n" +
                ",group_id\r\n" +
                "from tbluser\r\n" +
                "where [user_id]=@us_id\r\n";

                SqlCommand sqlCom = sqlConn.CreateCommand();
                sqlCom.CommandText = strSQL;
                sqlCom.Parameters.Add("@us_id", SqlDbType.Int).Value = intUserID;
                dtResult = Common.ExecuteTable(sqlCom);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            dtResult.TableName = "Employee";
            return dtResult;
        }



    }
}
