<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SonyAnalysis.Master" CodeBehind="SonyAnalysis_Export.aspx.vb" Inherits="Ganges33.SonyAnalysis_Export" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>
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
    

  <<div class="wrapper  col-sm-12 sidebar-wrapper position-fixed scrolbar contain" >
   
    <div class="content" >
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-12">
              
              <div class="card">
                <div class="card-header card-header-primary">
                  <h3 class="card-title ">Analysis File Export</h3>
                  <p class="card-category"></p>
                </div>
                <div class="card-body scrollbar" id="style-10" >
                 
                   <div style="height:45px">

                </div>

                       
                     
                     
                      
               <div  class="col-sm-12">
					
                    <div class="row">
                   
                      <div class="col-md-6">
                              <div class="row">
                                  <div class="col-sm-3">
                         <label  class="fontFamily">Target Store</label>
                                      </div>
                                  <div class="col-sm-8">
                       <asp:DropDownList ID="DropListLocation" runat="server" CssClass="form-control dropdown-toggle" style="width: 100%; height:33px" >
                   
                          </asp:DropDownList>
                                      </div>
                    </div>
				  
      <div class="row  ">
                        <div class="form-group col-sm-3 ">
                                 <br />
                     <label id="" class="fontFamily">Month</label>
                            </div>
                            <div class="col-sm-4">
                                <br />

				  <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="form-control dropdown-toggle" style="width: 100%; height:33px" >
                        		     <asp:ListItem Text="Select..." Value="0"></asp:ListItem>
                             <asp:ListItem Text="January" Value="01"></asp:ListItem>
                              <asp:ListItem Text="February" Value="02"></asp:ListItem>
                              <asp:ListItem Text="March" Value="03"></asp:ListItem>
                             <asp:ListItem Text="April" Value="04"></asp:ListItem>
                              <asp:ListItem Text="May" Value="05"></asp:ListItem>
                               <asp:ListItem Text="June" Value="06"></asp:ListItem>
                             <asp:ListItem Text="July" Value="07"></asp:ListItem>
                              <asp:ListItem Text="August" Value="08"></asp:ListItem>
                              <asp:ListItem Text="September" Value="09"></asp:ListItem>
                              <asp:ListItem Text="October" Value="10"></asp:ListItem>
                              <asp:ListItem Text="November" Value="11"></asp:ListItem>
                              <asp:ListItem Text="December" Value="12"></asp:ListItem>
                      </asp:DropDownList>
                         </div> 
					 
                        
                          <div class="col-sm-2">
                                 <br />  
					<asp:DropDownList ID="DropDownYear" runat="server" CssClass="form-control dropdown-toggle"   style="width: 130%; height:33px" >
                            <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                              <asp:ListItem Text="2020" Value="2020" Selected="True"></asp:ListItem>
                              <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                             <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                              <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                               <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                             <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                         
                      </asp:DropDownList>

                        </div>
					  
					
          
           
                          <div class="col-sm-2">
                                   <br />
				 <asp:DropDownList ID="DropDownDaySub" runat="server" CssClass="form-control dropdown-toggle"  style="width: 100%; height:33px;"  Visible="false">
                              <asp:ListItem Text="Select Day..." Value="0"></asp:ListItem>
                            <asp:ListItem Text="01" Value="01"></asp:ListItem>
                              <asp:ListItem Text="11" Value="11"></asp:ListItem>
                              <asp:ListItem Text="21" Value="21"></asp:ListItem>
               </asp:DropDownList>
                                  
                     
                          <asp:DropDownList ID="DropDownGR" runat="server" CssClass="form-control dropdown-toggle"  style="width: 100%; height:33px;" Visible="false" >
                            <asp:ListItem Text="GR" Value="GR"></asp:ListItem>
                              <asp:ListItem Text="GD" Value="GD"></asp:ListItem>
                 </asp:DropDownList>
                              <br />
                               <asp:DropDownList ID="DropDownDTSub" runat="server" CssClass="form-control dropdown-toggle"  style="width:120%; height:33px;" Visible="false" >
                              <asp:ListItem Text="Select..." Value="0"></asp:ListItem>
                            <asp:ListItem Text="DT1" Value="DT1"></asp:ListItem>
                              <asp:ListItem Text="DT2" Value="DT2"></asp:ListItem>
                 </asp:DropDownList>
                         </div>
					  </div>
					
                     
                        </div>     
                     
                     
					<div class="col-sm-6 ">
                    <div class="row">
                        <div class="col-sm-3">
                      <label  class="bmd-label-floating ">Export File</label>
                            </div>
                        <div class="col-sm-9">
                             <asp:DropDownList ID="DropDownExportFile" runat="server" CssClass="form-control dropdown-toggle" Height="33px" Width="303px" Visible="false" >
                      </asp:DropDownList>
                           
                         <asp:DropDownList ID="drpTaskExport" CssClass="form-control dropdown-toggle" runat="server" style="width: 100%; height:33px;" AutoPostBack="true"  >
                             <asp:ListItem Text="Select..." Value="-1"></asp:ListItem>
                                <asp:ListItem Text="101.In Bound " Value="101"></asp:ListItem>
                                <asp:ListItem Text="102.Out bound " Value="102"></asp:ListItem>
                                <asp:ListItem Text="103.PartOrderListReport" Value="103"></asp:ListItem>
                                <asp:ListItem Text="104.RPSI Inquiry" Value="104"></asp:ListItem>
                                <asp:ListItem Text="105.Stock Report" Value="105"></asp:ListItem>
                                <asp:ListItem Text="106.Date Wise Sales Report" Value="106"></asp:ListItem>
                                <asp:ListItem Text="107.Stock Available" Value="107"></asp:ListItem>
                                <asp:ListItem Text="108.AscGstTaxReport" Value="108"></asp:ListItem>
                                <asp:ListItem Text="109.ASCTaxReport" Value="109"></asp:ListItem>
                                <asp:ListItem Text="110.ASC_Tax_Report" Value="110"></asp:ListItem>
                                <asp:ListItem Text="111.ClaimInvoiceDetail" Value="111"></asp:ListItem>
                                <asp:ListItem Text="112.ASC Invoice Data" Value="112"></asp:ListItem>
                                <asp:ListItem Text="113.Daily Abandon" Value="113"></asp:ListItem>
                                <asp:ListItem Text="114.Daily Accumulated KPI Report" Value="114"></asp:ListItem>
                                <asp:ListItem Text="115.Daily_ASCPartsUsed" Value="115"></asp:ListItem>
                                <asp:ListItem Text="116.Daily_Claim" Value="116"></asp:ListItem>
                                <asp:ListItem Text="117.Daily_Delivered" Value="117"></asp:ListItem>
                                <asp:ListItem Text="118.Daily_OS_Reservation" Value="118"></asp:ListItem>
                                <asp:ListItem Text="119.Daily_Receiveset" Value="119"></asp:ListItem>
                              

                              <asp:ListItem Text="120.Daily_OS_specialtreatment" Value="120"></asp:ListItem>
                              <asp:ListItem Text="121.Daily_Acct Stmt" Value="121"></asp:ListItem> 
                             <asp:ListItem Text="122.Daily_RepairsetSet_NP" Value="122"></asp:ListItem>
                             <asp:ListItem Text="123.Daily_UnDeliveredSet" Value="123"></asp:ListItem>
                              <asp:ListItem Text="124.Daily_UnRepaipairset_NP" Value="124"></asp:ListItem>
                              <asp:ListItem Text="125.Monthly Reservationvation" Value="125"></asp:ListItem>
                              <%--<asp:ListItem Text="135.Monthly Repairset" Value="135"></asp:ListItem>--%>
                             <asp:ListItem Text="126.Monthly Repairset" Value="126"></asp:ListItem>
                            <%--  <asp:ListItem Text="136.Monthly Abandon" Value="136"></asp:ListItem>--%>
                               <asp:ListItem Text="127.Monthly Abandon" Value="127"></asp:ListItem>
                               <%--<asp:ListItem Text="137.Monthly_SOMC_Claim" Value="137"></asp:ListItem>--%>
                             <asp:ListItem Text="128.Monthly_SOMC_Claim" Value="128"></asp:ListItem>

                             
                            
                      </asp:DropDownList>
                        </div>
                        </div>
                     
                 
				
		
					<div class="row">
                        <div class=" col-sm-5">
                        <div class="row" runat="server" ID="Label8">
                           <br />
                            <div class="col-sm-8">
                                <br />
              <label  class="bmd-label-floating "> Date:
              </label> 

                            </div> 
                          
                            <div > 
                                   <br />
                                <label class="bmd-label-floating ">
                                    From
                                </label>
                            </div>
                            </div>
              </div>
                       
                        <div class="col-sm-7">
                             <br />
                           
                 <asp:TextBox ID="TextDateFrom" runat="server" class="form-file-upload  date serverlbl" AutoCompleteType="Disabled" style="width: 40%;"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                      <ajaxToolkit:CalendarExtender ID="TextDateFrom_CalendarExtender" runat="server" BehaviorID="TextDateFrom_CalendarExtender" TargetControlID="TextDateFrom" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender>              
              <label runat="server" ID="Label7" class="bmd-label-floating fontFamily">To</label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="TextDateTo" runat="server" class="form-file-upload date serverlbl" AutoCompleteType="Disabled" style="width: 40%;"></asp:TextBox>
                     <%-- <ajaxToolkit:CalendarExtender ID="TextDateTo_CalendarExtender" runat="server" BehaviorID="TextDateTo_CalendarExtender" TargetControlID="TextDateTo" PopupPosition="right">
                      </ajaxToolkit:CalendarExtender>--%>
                            </div>
           </div>
                        </div>
                        </div>
                          
					   </div>
					   <div>
			</div>
					               <div class="col-sm-12">
                      <asp:Button runat="server" ID="btnExport" class="btn btn-primary pull-right" Text="Export" />
                        </div>
                      <div class="clearfix"></div>
               </div>
                    </div>
        
                 </div>
                </div>
              </div>
            </div>
            </div>
          
    <div id="dialog" title="message" style="display:none;"> >
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
    </div>
    <div style="visibility:hidden">
         <div class="form-group row">
                            <div class="col-sm-2">
                          <label class="bmd-label-floating">Current Location</label> 
                                </div>
                            <div>
                                <label>:</label>
                                <asp:Label ID="lblLoc" class="bmd-label-floating  serverlbl" runat="server"></asp:Label>
                       </div>
                        </div>
                 
                      
                      
                     
                        <div class="form-group row">
                            <div class="col-sm-2">
                           <label class="bmd-label-floating">Current Username</label>
                                </div>
                            <div>
                                <label>:</label>
                          <asp:label class="bmd-label-floating serverlbl" ID="lblName"  runat="server"> </asp:label>
                          </div>
                        </div>
    </div>
 

</asp:Content>
