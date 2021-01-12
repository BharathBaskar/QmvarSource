<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/SonyAnalysis.Master" CodeBehind="SonyAnalysis_Report2.aspx.vb" Inherits="Ganges33.SonyAnalysis_Report2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <link type="text/css" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1/themes/start/jquery-ui.css" rel="stylesheet">
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1/jquery-ui.min.js"></script>

   

    <link href="assets/jquery-ui_theme.css" rel="stylesheet" />
    <link href="assets/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript"  src="assets/jquery-ui.min_lips.js"></script>


    <link href="assets/css/material-dashboard.css" rel="stylesheet" />  
    <link href="assets/css/material-dashboard-rtl.css" rel="stylesheet" />
    <link href="assets/css/material-dashboard.min.css" rel="stylesheet" />
    <meta charset="utf-8" />
  <link rel="apple-touch-icon" sizes="76x76" href="../assets/img/apple-icon.png">
  <link rel="icon" type="image/png" href="../assets/img/favicon.png">
  <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
  
  <meta content='width=device-width, initial-scale=1.0, shrink-to-fit=no' name='viewport' />
  <!--     Fonts and icons     -->
  <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css">
  <!-- CSS Files -->
   <link href="assets/css/material-dashboard.css" rel="stylesheet" /> 
  <!-- CSS Just for demo purpose, don't include it in your project -->
  <link href="assets/demo/demo.css" rel="stylesheet" />
    <style type="text/css">
 
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    

  <<div class="wrapper  col-sm-12 sidebar-wrapper position-fixed scrolbar contain" id="style-10">
   
    <div class="content" >
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-12">
              
              <div class="card">
                <div class="card-header card-header-primary">
                  <h3 class="card-title ">Review Sony Activity Report</h3>
                  <p class="card-category"></p>
                </div>
                  <div class="card-body scrollbar" id="style-10">

                    <div class="row col-sm-12">
                        <div class="row col-sm-10">
                        <div class="col-sm-3">
                            <br />
                             <Label ID="Label6" runat="server" CssClass="bmd-label-floating serverlbl">Activity Month</Label>
                            <asp:Label ID="lblMonNow" runat="server" Font-Bold="true" CssClass="bmd-label-floating serverlbl"></asp:Label>
                            
                        </div>
                            <div class="col-sm-1">
                                <br />
                            <asp:Label ID="Label1"  CssClass="bmd-label-floating serverlbl" runat="server" GroupName="gender"  Text="All"></asp:Label>
                  <asp:RadioButton ID="RadioAll" runat="server" AutoPostBack="true" Checked="True" />
                            </div>
                            <div >
                                <br />
                     <asp:Label ID="Label2"  CssClass="bmd-label-floating serverlbl" runat="server" GroupName="gender"  Text="Service"></asp:Label>
                   <asp:RadioButton ID="RadioService" runat="server" AutoPostBack="true" />
                             </div>
                            <div class="col-sm-1">
                                <br />
                        <asp:Label ID="Label3"  CssClass="bmd-label-floating serverlbl" runat="server" GroupName="gender"  Text="D&I"></asp:Label>
                    <asp:RadioButton ID="RadioD_I" runat="server" AutoPostBack="true" />
                         </div>
                            <div >
                                <br />
                        <asp:Label ID="Label4"  CssClass="bmd-label-floating serverlbl" runat="server" GroupName="gender"  Text="Total"></asp:Label>
                   <asp:RadioButton ID="RadioTotal" runat="server" AutoPostBack="true" />
                           </div> 
                            
                        </div>
                        <div class="col-sm-12">
                            <div >
                              <asp:Button ID="btnDown" runat="server"  text="Download"  CssClass="btn btn-primary pull-right" />
                                </div>
                            <div>
                                <asp:Button ID="btnBack" runat="server" text="Back" CssClass="btn btn-primary pull-right" />
                              
                

                            </div>
                              <div >
                                  <asp:Button ID="btnSend" runat="server" text="Send" CssClass="btn btn-primary pull-right" />
                     
                              </div>
                        </div>
                          
                    </div>
                
                    <div class="col-sm-12">
                         <br />
                          <asp:GridView ID="GridInfo" runat="server" PageSize="31" class="col-sm-12" EmptyDataText="There was no relevant information">
                       <HeaderStyle BackColor="#8e24aa" ForeColor="white" />
                       <RowStyle Height="25px" Width ="150px" Wrap="False"/>
                      </asp:GridView>

                    </div>

                      <div style="height:10px">

                      </div>
                       



                    </div>
                  </div>
                </div>
              </div>
            </div>
        </div>
         </div>
    

        <asp:Button ID="BtnCancel" runat="server" Text="Button" style="display:none;"/>
        <asp:Button ID="BtnOK" runat="server" Text="Button" style="display:none;" />
   

    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>
