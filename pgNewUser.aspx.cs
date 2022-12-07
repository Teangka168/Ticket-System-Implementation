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
using System.Security.Cryptography;

namespace WebApp.Interface
{
    public partial class pgNewUser : System.Web.UI.Page
    {

        private SqlConnection _sqlConn = new SqlConnection();
        private String _currentUserName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["us"].ToString() != "")
            {
                _currentUserName = Request.QueryString["us"].ToString();
            }

            new DBHelper().InitiateWebConnection(out _sqlConn);
            WebCommon.CheckSession(this, (int)WebCommon.WebPageEnum.FromCurrentPage, _sqlConn);

            if (!IsPostBack)
            {
                loadUserType();                  
            }

            loadAllUser();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                new DBHelper().InitiateWebConnection(out _sqlConn);

                NewUserInfo objNewUserInfo = new NewUserInfo();

                objNewUserInfo.first_name = txtFirstName.Text.Trim();
                objNewUserInfo.last_name = txtLastName.Text.Trim();
                objNewUserInfo.sex = ddlSex.SelectedValue.ToString();
                objNewUserInfo.user_name = txtUserName.Text.Trim();
                objNewUserInfo.is_active = true;
                objNewUserInfo.group_id = int.Parse(ddlSaveUserType.SelectedValue.ToString());

                string strSalt = "", strHashPwd = "", strPasswordOut = "";
                if (txtPassword.Text.Length > 0)
                {
                    GenerateSaltNHash(txtPassword.Text, out strSalt, out strHashPwd);
                    HashPassword(txtPassword.Text, strSalt, out strPasswordOut);
                }

                objNewUserInfo.password1 = strPasswordOut;
                objNewUserInfo.password2 = strSalt;

                if (NewUser.Insert(objNewUserInfo, null, _sqlConn))
                {
                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtUserName.Text = "";
                    txtDescrition.Text = "";

                    loadAllUser();
                }
            }
            catch { }
            finally
            {
                new DBHelper().CloseWebConnection(_sqlConn);
            }

        }

        private void loadAllUser()
        {
            try
            {
                DataTable dtData = NewUser.LoadUserList(int.Parse(ddlUserType.SelectedValue.ToString()), _sqlConn);

                dtgUserList.DataSource = dtData;
                dtgUserList.DataBind();

            }
            catch { }
        }

        private void loadUserType()
        {
            DataTable dtData = new DataTable();
            try
            {
                string strSQL =
"select 0 as id,'All' as [name]\r\n" +
"union all\r\n" +
"select group_id as id\r\n" +
",group_name as [name]\r\n" +
"from tblUserGroup\r\n" +
"order by id\r\n";

                string strSQL2 =
"select group_id as id\r\n" +
",group_name as [name]\r\n" +
"from tlkpUserGroup\r\n" +
"order by id\r\n";



                SqlCommand sqlcom = _sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.CommandTimeout = 0;
                dtData = Common.ExecuteTable(sqlcom);

                DataTable dtData2 = new DataTable();
                sqlcom.CommandText = strSQL2;
                sqlcom.CommandTimeout = 0;
                dtData2 = Common.ExecuteTable(sqlcom);

                ddlUserType.DataSource = dtData;
                ddlUserType.DataValueField = "id";
                ddlUserType.DataTextField = "name";
                ddlUserType.DataBind();

                ddlSaveUserType.DataSource = dtData2;
                ddlSaveUserType.DataValueField = "id";
                ddlSaveUserType.DataTextField = "name";
                ddlSaveUserType.DataBind();

            }
            catch (Exception exp)
            {
                CallingErrorForm.GetErrorForm("Can not load Data.", exp);
            }
        }

        private static void HashPassword(string strPassword, string strSalt, out string strOutPwd)
        {
            Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(strPassword, System.Convert.FromBase64String(strSalt), 1000);
            strOutPwd = System.Convert.ToBase64String(hash.GetBytes(32));
        }

        private static void GenerateSaltNHash(string strPassword, out string strSalt, out string strHashPwd)
        {
            Rfc2898DeriveBytes hash = new Rfc2898DeriveBytes(strPassword, 32, 1000);
            strSalt = System.Convert.ToBase64String(hash.Salt); // add more salt to it
            strHashPwd = System.Convert.ToBase64String(hash.GetBytes(32));
        }


    }
}