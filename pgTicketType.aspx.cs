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
    public partial class pgTicketType : System.Web.UI.Page
    {


        private SqlConnection _sqlConn = new SqlConnection();
        private String _currentUserName;
             
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Request.QueryString["us"].ToString() != "")
                {
                    _currentUserName = Request.QueryString["us"].ToString();
                }

                new DBHelper().InitiateWebConnection(out _sqlConn);
                WebCommon.CheckSession(this, (int)WebCommon.WebPageEnum.FromCurrentPage, _sqlConn);

                if (!IsPostBack)
                {                    
                }

                loadAllTicketType();
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

                TicketTypeInfo objTicketType = new TicketTypeInfo();

                objTicketType.name = txtName.Text.Trim();
                objTicketType.description = txtDescrition.Text;
                objTicketType.is_active = true;
                objTicketType.create_by=_currentUserName;
                objTicketType.create_date=Common.GetCurrentDate(_sqlConn, null);

                if (TicketType.Insert(objTicketType, null, _sqlConn))
                {
                    if (TicketType.InsertPermisionMenu(objTicketType.id ,null, _sqlConn))
                    {
                        txtName.Text = "";
                        txtDescrition.Text = "";
                        loadAllTicketType();
                    }
                }

            }
            catch { }
            finally
            {
                new DBHelper().CloseWebConnection(_sqlConn);
            }
        }

        private void loadAllTicketType()
        {
            try
            {
                DataTable dtData = TicketType.LoadTicketTypeList(_sqlConn);

                dtgTicketType.DataSource = dtData;
                dtgTicketType.DataBind();

            }
            catch { }
        }



    }
}