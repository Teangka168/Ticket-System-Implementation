<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="pgNewUser.aspx.cs" Inherits="WebApp.Interface.pgNewUser" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>


<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="server">

    <style>
        .modalBackgroud{
            background-color:black;
            filter:alpha(opacity=90) !important;
            opacity:0.6 ! important;
            z-index:20;
        }
        .modalpopup{
            padding:20px 0px 24px 10px;
            position:absolute;
            width:500px;
            height:400px;
            background-color:white;
            border:1px solid black;
            text-align:left;

        }

        .btnPopup{
           position:absolute; 
           margin-left:-100px;
           left:50%;
           width:200px;
           bottom:5px;
        }

        .btnPopup2{
           position:absolute; 
           margin-left:-100px;
           left:50%;
           bottom:10px;
           width:200px;
           height:100px;
           font-family:Microsoft Sans Serif;
           Font-Size:45px;
           background-color:#B3D38D;
        }

         .btnPopup3{
           margin-left:15px;
           width:200px;
           height:100px;
           white-space: normal;
           font-family:Microsoft Sans Serif;
           Font-Size:20px;
           background-color:#008c9d;
        }

         .btnPopupPax{           
           bottom:10px;
           width:110px;
           height:50px;
           font-family:Microsoft Sans Serif;
           Font-Size:35px;
           background-color:#B3D38D;
           text-align:center;
        }

       .btnNomal{
           /*margin-left:15px;*/     
           font-family:Microsoft Sans Serif;
           Font-Size:20px;
           background-color:#B3D38D;
        }

        .btnAvailable{
            margin-left:15px;
            width:200px;
            height:100px;
            margin-top:15px;
            white-space: normal;   
            font-family:Microsoft Sans Serif;
            Font-Size:30px;
            background-color:DarkGreen;
        }

        .btnOccupy{
            margin-left:15px;
            width:200px;
            height:100px;
            margin-top:15px;
            white-space: normal;                
            font-family:Microsoft Sans Serif;
            Font-Size:30px;
            background-color:SandyBrown;
        }

        .btnReserve{
            margin-left:15px;
            width:200px;
            height:100px;
            margin-top:15px;
            white-space: normal;               
            font-family:Microsoft Sans Serif;
            Font-Size:30px;
            background-color:DarkBlue;
        }

         /*.wrap { 
             white-space: normal; 
             width: 100px;
         }*/

    </style>

    <div>        
        &nbsp; &nbsp; &nbsp;
        
        <table>
            <tr>
                <td>
                    &nbsp;&nbsp;&nbsp;
                </td>
                <td>
                   
                    <asp:DropDownList ID="ddlUserType" runat="server" Width="150px" Height="28px" AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"  >  </asp:DropDownList>
                    
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:Button ID="btnAddTask" runat="server" Text="Add User" CssClass="btnNomal" />
                </td>
            </tr>
        </table>
    </div>


    <div >
        <asp:Panel ID="pnAddTask" runat="server" CssClass="modalpopup">
            <table>

                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="First Name: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtFirstName" runat="server" ReadOnly="false" Width="250px" Font-Names="Khmer OS System"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Last Name: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server" ReadOnly="false" Width="250px" Font-Names="Khmer OS System"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Sex: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSex" runat="server" Width="250px" Height="28px" AutoPostBack="false" >  
                            <asp:ListItem Text="Male" Value ="Male" >Male</asp:ListItem>
                            <asp:ListItem Text="Female" Value ="Female" >Female</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="User Type: "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSaveUserType" runat="server" Width="250px" Height="28px" AutoPostBack="false"  >  </asp:DropDownList>
                    </td>
                </tr>


                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="User Name: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUserName" runat="server" ReadOnly="false" Width="250px" Font-Names="Khmer OS System"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Password: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" ReadOnly="false" Width="250px" Font-Names="Khmer OS System" TextMode="Password" ></asp:TextBox>
                    </td>
                </tr>

                <tr>
                        <td>
                            <asp:Label ID="Label22" runat="server" Text="Description: " ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescrition" runat="server" ReadOnly="false" TextMode="MultiLine"  Height="115px" Width="250px" Font-Names="Khmer OS System"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="btnPopupPax" OnClick="btnSave_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnClosAddTask" runat="server" Text="Close" CssClass="btnPopupPax" />
                    </td>
                </tr>
        </table>

        </asp:Panel>
    </div>


<ajaxtoolkit:ModalPopupExtender ID="mpeNewTask" runat="server" CancelControlID="btnClosAddTask"
    PopupControlID="pnAddTask" TargetControlID="btnAddTask"  BackgroundCssClass="modalBackgroud">
</ajaxtoolkit:ModalPopupExtender>

</asp:Content>



<asp:Content ID="BodyContent" ContentPlaceHolderID="BodyContentPlaceHolder" runat="server">


        <div>        
        <table>
            <tr>
                <td >
                    <asp:ScriptManager ID="ScriptManager1" runat="server" ></asp:ScriptManager>
                </td>
            </tr>
            <tr>
                <td>                                            
                    <asp:Panel ID="pTable" runat="server" GroupingText="User List" >
                    
                    <asp:GridView ID="dtgUserList" runat="server" AutoGenerateSelectButton="false" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Font-Names="Microsoft Sans Serif" Font-Size="16px" OnRowDataBound="dtgUserList_RowDataBound">             
                        <AlternatingRowStyle BackColor="#CCCCCC" />
                        <FooterStyle BackColor="#CCCCCC" />
                        <HeaderStyle BackColor="#B3D38D" Font-Bold="True" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#808080" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#383838" />

                    </asp:GridView>
                
                    </asp:Panel>                                 
                </td>                
            </tr>
        </table>
    </div>

</asp:Content>
