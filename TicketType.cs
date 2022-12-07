using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;
using WebApp.BLL;
using WebApp.Structure;
using BRC.Utilities;

namespace WebApp.BLL
{
    public class TicketType
    {

        public TicketTypeInfo getTicketTypeInfo(int intid, SqlTransaction sqlTran, SqlConnection sqlConn)
        {
            TicketTypeInfo obj = new TicketTypeInfo();
            String strSQL = "SELECT *\r\n" +
                            "FROM tblTicketType\r\n" +
                            "WHERE id=@id\r\n";
            SqlCommand sqlcom = sqlConn.CreateCommand();
            sqlcom.CommandText = strSQL;
            sqlcom.Transaction = sqlTran;
            sqlcom.Parameters.Add("@id", SqlDbType.Int).Value = intid;
            try
            {
                DataTable objData = Common.ExecuteTable(sqlcom);
                if (objData.Rows.Count > 0)
                {
                    DataRow objDr = objData.Rows[0];
                    obj = ReadData(objDr);
                }

            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    throw ex;
                }
                else
                {
                    CallingErrorForm.GetErrorForm("Can not getting record from tblSection.", ex);
                }
            }
            return obj;
        }

        public TicketTypeInfo ReadData(DataRow objDr)
        {
            TicketTypeInfo obj = new TicketTypeInfo();
            obj.id = (int)objDr["id"];
            obj.name = objDr["name"].ToString();
            obj.description = objDr["description"].ToString();
            obj.is_active = (bool)objDr["is_active"];
            obj.create_by = objDr["create_by"].ToString();
            obj.create_date = (DateTime)objDr["create_date"];
            return obj;
        }

        public static bool Insert(TicketTypeInfo objInfo, SqlTransaction sqlTran, SqlConnection sqlConn)
    {
        bool blnResult = false;
        String strSQL = 
        "INSERT INTO tblSection(\n\r" +
        "           name\r\n" +
        "           ,description\r\n" +
        "           ,is_active\r\n" +
        "           ,create_by\r\n" +
        "           ,create_date\r\n" +
        ")\r\n" +
        "VALUES(\r\n" +
        "           @name\r\n" +
        "           ,@description\r\n" +
        "           ,@is_active\r\n" +
        "           ,@create_by\r\n" +
        "           ,@create_date\r\n" +
        ") ; " +
"select @@Identity; \r\n";
                try
                {
                    SqlCommand sqlcom = sqlConn.CreateCommand();
                    sqlcom.CommandText = strSQL;
                    sqlcom.Transaction = sqlTran;
                    CommandHelper(sqlcom, objInfo);

                    object obj = sqlcom.ExecuteScalar();
                    if (obj != null)
                    {
                        objInfo.id = int.Parse(obj.ToString());
                    }
                    blnResult = true;
                }
            catch (Exception ex)
            {
                if(sqlTran != null) 
                { 
                       throw ex; 
                } 
                 else 
                {  
                       CallingErrorForm.GetErrorForm("Can not insert record to tblSection.", ex); 
                } 
            }
         return blnResult;
    }

        public static bool InsertPermisionMenu(int intChildID, SqlTransaction sqlTran, SqlConnection sqlConn)
        {
            bool blnResult = false;
            String strSQL = "INSERT INTO tblPermission(\n\r" +
            "           group_id\r\n" +
            "           ,menu_parent_id\r\n" +
            "           ,menu_child_id\r\n" +
            "           ,menu_name\r\n" +
            "           ,user_name\r\n" +
            ")\r\n" +
            "VALUES(\r\n" +
            "           1\r\n" +
            "           ,1\r\n" +
            "           ,@intChildID-1\r\n" +
            "           ,null\r\n" +
            "           ,null\r\n" +
            ") ; " +
"INSERT INTO tblPermission(\n\r" +
            "           group_id\r\n" +
            "           ,menu_parent_id\r\n" +
            "           ,menu_child_id\r\n" +
            "           ,menu_name\r\n" +
            "           ,user_name\r\n" +
            ")\r\n" +
            "VALUES(\r\n" +
            "           2\r\n" +
            "           ,2\r\n" +
            "           ,@intChildID\r\n" +
            "           ,null\r\n" +
            "           ,null\r\n" +
            ") ; " +
"update tblsection set sort_order=@intChildID-1  where id=@intChildID-1 ; update tblsection set sort_order=@intChildID where id=@intChildID";
            try
            {
                SqlCommand sqlcom = sqlConn.CreateCommand();
                sqlcom.CommandText = strSQL;
                sqlcom.Transaction = sqlTran;
                sqlcom.Parameters.Add("@intChildID", SqlDbType.Int).Value = intChildID;
                sqlcom.ExecuteNonQuery();

                blnResult = true;
            }
            catch (Exception ex)
            {
                if (sqlTran != null)
                {
                    throw ex;
                }
                else
                {
                    CallingErrorForm.GetErrorForm("Can not insert record to tblPermission.", ex);
                }
            }
            return blnResult;
        }

        private static void CommandHelper(SqlCommand command, TicketTypeInfo objInfo)
        {
            try
            {
                command.Parameters.Add("@id", SqlDbType.Int).Value = objInfo.id;
                command.Parameters.Add("@name", SqlDbType.NVarChar).Value = objInfo.name;
                command.Parameters.Add("@description", SqlDbType.NVarChar).Value = objInfo.description;
                command.Parameters.Add("@is_active", SqlDbType.Bit).Value = objInfo.is_active;
                command.Parameters.Add("@create_by", SqlDbType.NVarChar).Value = objInfo.create_by;
                command.Parameters.Add("@create_date", SqlDbType.DateTime).Value = objInfo.create_date;
            }
            catch { }
        }

        public static DataTable LoadTicketTypeList(SqlConnection sqlConn)
        {
            String strSQL =
"select [name] as [Ticket Type Name]\r\n" +
",description as Description\r\n" +
",create_by as [Create By]\r\n" +
",CONVERT(varchar,create_date,103) +' '+ LTRIM(RIGHT(CONVERT(VARCHAR(20), create_date, 100), 7))  as [Create Date]\r\n" +
"from tblTicketType\r\n" +
"where is_active=1\r\n";
            SqlCommand sqlcom = sqlConn.CreateCommand();
            sqlcom.CommandText = strSQL;
            DataTable objData = new DataTable();
            try
            {
                objData = Common.ExecuteTable(sqlcom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objData;
        }


    }
}