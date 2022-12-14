<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="pgResolveBug.aspx.cs" Inherits="WebApp.Interface.pgResolveBug" %>


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
            height:500px;
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
                    <p>
                        <%--<asp:Label ID="lbOutletName" runat="server" Text="Outlet Name: " Font-Size="X-Large" ForeColor="Black" BackColor="#B3D38D"></asp:Label>--%>
                    </p>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <%--<asp:Button ID="btnAddTask" runat="server" Text="Add Bug" CssClass="btnNomal" />--%>
                </td>
            </tr>
        </table>
    </div>


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
                    <asp:Panel ID="pTable" runat="server" GroupingText="Bug" >
                    
                    <asp:GridView ID="dtgBugList" runat="server" AutoGenerateSelectButton="false" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" Font-Names="Microsoft Sans Serif" Font-Size="16px" OnRowDataBound="dtgBugList_RowDataBound"  >             
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




    <div >
        <asp:Panel ID="pnViewTask" runat="server" CssClass="modalpopup">
            <table>

                <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Title: "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtViewTitle" runat="server" ReadOnly="true" Width="250px" Font-Names="Khmer OS System" ></asp:TextBox>
                            </td>
                        </tr>

                <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Description: " ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtViewDescription" runat="server" ReadOnly="true" TextMode="MultiLine"  Height="115px" Width="250px" Font-Names="Khmer OS System"></asp:TextBox>
                        </td>
                    </tr>

                <tr>
                            <td>
                                <asp:Label ID="Label8" runat="server" Text="Severity: " ></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlViewSeverity" runat="server" Width="258px" Height="28px" AutoPostBack="false" Enabled="false" >  </asp:DropDownList>
                            </td>
                        </tr>

                <tr>
                            <td>
                                <asp:Label ID="Label9" runat="server" Text="Priority: " ></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlViewPriority" runat="server" Width="258px" Height="28px" AutoPostBack="false" Enabled="false"  >  </asp:DropDownList>
                            </td>
                        </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Rosolved By: " ></asp:Label>
                    </td>                    
                    <td>                        
                        <asp:TextBox ID="txtRosolvedBy" runat="server" ReadOnly="false" Width="250px" Font-Names="Khmer OS System" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblStartDate" Text="Rosolved Date:" Font-Size="Large" />
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtRosolvedDate" Font-Size="Large" Width="250" CssClass="textbox-std" AutoPostBack="false" EnableViewState="False" />
                        <ajaxToolkit:CalendarExtender runat="server" ID="ceStartDate" Format="dd-MMM-yyyy" TargetControlID="txtRosolvedDate" />
                    </td>
                </tr>
                <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Rosolved Description: " ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRosolvedDescription" runat="server" ReadOnly="false" TextMode="MultiLine"  Height="115px" Width="250px" Font-Names="Khmer OS System"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td></td>                    
                    <td></td>
                </tr>

                <tr>
                    <td colspan="2">
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnEdit" runat="server" Text="Save"  CssClass="btnPopupPax" OnClick="btnEdit_Click"  />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                        <asp:Button ID="btnClosViewTask" runat="server" Text="Close" CssClass="btnPopupPax" />
                        <asp:Label ID="lblWorkingID" runat="server" Text="" style="display:none" ></asp:Label>
                    </td>
                </tr>

        </table>

        </asp:Panel>
    </div>

<asp:Button ID="btnViewTask" runat="server" Text="Resolved" style="display:none" />

<ajaxtoolkit:ModalPopupExtender ID="mpeViewTask" runat="server" CancelControlID="btnClosViewTask"
    PopupControlID="pnViewTask" TargetControlID="btnViewTask"  BackgroundCssClass="modalBackgroud">
</ajaxtoolkit:ModalPopupExtender>



</asp:Content>
