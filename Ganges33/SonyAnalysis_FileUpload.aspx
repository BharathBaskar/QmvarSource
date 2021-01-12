<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/SonyAnalysis.Master" CodeBehind="SonyAnalysis_FileUpload.aspx.vb" Inherits="Ganges33.SonyAnalysis_FileUpload" %>
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
  <meta http-equiv="X-UA-Compatible" name="msapplication-tap-highlight" content="IE=edge,chrome=1" />
  
  <meta content='width=device-width, initial-scale=1.0, shrink-to-fit=no' name='viewport' />
  <!--     Fonts and icons     -->
  <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css">
  <!-- CSS Files -->
   <link href="assets/css/material-dashboard.css" rel="stylesheet" /> 
  <!-- CSS Just for demo purpose, don't include it in your project -->
  <link href="assets/demo/demo.css" rel="stylesheet" />
    <style type="text/css">

        .container{
    -ms-overflow-style: none;
    overflow: auto;
}
       
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    

   
   <div class="wrapper  col-sm-12 sidebar-wrapper position-fixed scrolbar contain" id="style-10">
   
    <div class="content" >
        <div class="container-fluid">
          <div class="row">
            <div class="col-md-12">
              <div class="card">
                <div class="card-header card-header-primary">
                  <h3 class="card-title ">Analysis File Upload</h3>
                  <p class="card-category"></p>
                </div>
                <div class="card-body " >
<br /><br />
  <div class="row col-sm-12">

           <div class="row col-sm-6"> 
              
                        <div class="col-sm-4">
                            
                             <label id="" class="fontFamily bmd-label-floating">Upload Branch</label>
                       </div>
                         <div class="col-sm-8">
                             
                            <asp:DropDownList class="form-control  " ID="DropListLocation" runat="server" style="width: 100%;height:33px; ">

                      </asp:DropDownList>
                        </div>
               </div>
               
                        
          <div class="col-sm-6 row">
              <div class="col-sm-12">
              <div  class="col-sm-7">
                   <div  class="col-sm-12">
                           <label id="" class="fontFamily bmd-label-floating">Parts Invoice No</label>
                                </div>
                                </div>
                            <div class="col-sm-5">
                                 <asp:TextBox ID="TextPartsInvoiceNo" runat="server" value="" Height="33px" Width="100%" class="form-file-upload  serverlbl" />
                    </div>
      </div>
      </div>
</div>

          
                  <div class="col-sm-12 row">



                      <div class="col-sm-6 row">
                           <div  class="col-sm-4">
                                <br />
                        <label id="" class=" bmd-label-floating">Task Name</label>
                               </div>
                        <div class="col-sm-8">
                             <br />
                               <asp:DropDownList ID="drpTask" runat="server"  AutoPostBack="true"  OnSelectedIndexChanged="OnSelectedIndexChanged"  class="form-control" style="width: 100%;height:33px;">
                                           <asp:ListItem Text="Select..." Value="0"></asp:ListItem>

                                
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
                                <asp:ListItem Text="114.Daily Accumulated_KPI_Report" Value="114"></asp:ListItem>
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



                                <%--<asp:ListItem Text="137.Monthly_SOMC_Claim" Value="137"></asp:ListItem>--%>




                      </asp:DropDownList>
                            </div>
                      </div>
                      
                         <div class="col-sm-6 row">
                             <div class="col-sm-12">
                            <div  class="col-sm-7">
                                 <br />
                                 <div  class="col-sm-12">
                          <label id="" class="fontFamily bmd-label-floating">labor Invoice No</label>
                                </div>
                                </div>
                            <div class="col-sm-5">
                                 <br />
                                <asp:TextBox ID="TextLaborInvoiceNo" runat="server" Height="33px" Width="100%" value="" class="form-file-upload serverlbl" />  
              </div>
                          </div>
                          </div>
                   </div>
                    <div class="col-sm-12 row">
                        <div class="col-sm-6 row">
    <div  class="col-sm-4">
         <br />
                         <label  class="fontFamily bmd-label-floating">Invoice Date</label>
                  </div>
                         <div class="col-sm-8">
                              <br />
                      <span class=" fontFamily"><asp:RadioButton type="radio"  value="RadioBtnMM" disabled="disabled"  ID="RadioBtnMM" runat="server" />&nbsp;&nbsp;<label>MM/DD/YYYY</label></span>
                      <span class="fontFamily">&nbsp;&nbsp;</span>&nbsp;&nbsp; &nbsp;<span class=" fontFamily"><asp:RadioButton ID="RadioBtnDD" runat="server" AutoPostBack="True" type="radio" value="RadioBtnDD" disabled="disabled" />&nbsp;&nbsp;<label>DD/MM/YYYY</label></span>
                      
                 </div>
                    </div>
                        <div class="col-sm-6 row">
                            <div class="col-sm-12">
                          <div  class="col-sm-7">
                                <br />
                               <div  class="col-sm-12">
                    <label id="" class="fontFamily bmd-label-floating" >Invoice Date(DD/MM/YYYY)</label>
                    </div>
                    </div>
                            
                      <div class="col-sm-5">
                            <br />
                          <asp:TextBox value="" ID="TextInvoiceDate" runat="server" Height="33px" Width="100%" AutoCompleteType="Disabled" class="form-file-upload serverlbl date  " />
                   <%--   <ajaxToolkit:CalendarExtender ID="TextInvoiceDate_CalendarExtender" runat="server"  CssClass="date" BehaviorID="TextInvoiceDate_CalendarExtender" TargetControlID="TextInvoiceDate" PopupPosition="right">
                      </ajaxToolkit:CalendarExtender>--%>
                          </div>
                          </div>
                        </div>
                    </div>
                    
                
                    <div class="col-sm-12 row">
                        <div class="col-sm-6 row">
                           <div class="col-sm-12">
                                 <br />
                               <asp:FileUpload ID="FileUploadAnalysis" runat="server" Class="serverlbl" />
                      
                               </div>

                        </div>
                                
                        <div class="col-sm-6 row">
                            <div class="col-sm-12">
                              <div  class="col-sm-7">
                                   <br />
                                   <div  class="col-sm-12">
                    <label id="" class="fontFamily bmd-label-floating">Date</label>
                    </div>
                    </div>

                      <div class="col-sm-5">
                           <br />
                    <asp:TextBox value="" ID="txtDate" runat="server" Height="33px" Width="100%" AutoCompleteType="Disabled"  class="form-file-upload serverlbl date  calender"  />&nbsp;&nbsp;&nbsp;
               <%--       <ajaxToolkit:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="MM/dd/yyyy" BehaviorID="txtDate_CalendarExtender" TargetControlID="txtDate" PopupPosition="Left">
                      </ajaxToolkit:CalendarExtender> --%> 
                          </div>
                        </div>
                    </div>
                        </div>
                    
                  
                          <div class="col-sm-12 row">

                              <div class="col-sm-6 row">


                              </div>

                              <div class="col-sm-6 row">
                                  <div class="col-sm-12">
                                   <div  class="col-sm-7">
                                        <br />
                                        <div  class="col-sm-12">
                  <label id="" class="  bmd-label-floating">Amount</label>
               </div>
               </div>
                    <div class="col-sm-5">
                         <br />
                        <asp:TextBox ID="txtAmount" runat="server" value="" Height="33px" Width="100%" class="form-file-upload serverlbl" />
                   </div>
                              </div>
                              </div>


                          </div>
                   
                              <div class="col-sm-12 row">
                                  <div class="col-sm-6 row">
                                      </div>
                                  <div class="col-sm-6 row">
                                      <div class="col-sm-12">
                                        <div  class="col-sm-7">
                                             <br />
                                            <div class="col-sm-12">
                    <label class="bmd-label-floating">Advice Reference No</label>
                  </div></div>
                      <div class="col-sm-5">
                           <br />
                          <asp:TextBox ID="txtArNo" runat="server" value="" Height="33px" Width="100%" class="form-control serverlbl" />
               </div>
                        
                                  </div>
                                  </div>

                                  </div>
                     
                  <div class="col-sm-12">
                      <br />
                        <asp:Button ID="btnUpload" runat="server" Text="Import" class="btn btn-primary pull-right" />
                  </div>
                                         <div class="row col-sm-12">
                      <div class="col-sm-6">
                        
                            <div >
                            <label class="bmd-label-floating">Message</label>
                          
                                </div>
                          <div>
                      <select size="4" ID="ListMsg" runat="server" class="listbox form-control" style="Height: 85px;Width: 100%;overflow: auto;">
                      </select>      
                        </div>
                           
                      </div>
                      <div class="col-sm-6">
                    <div>
                            <label class="bmd-label-floating"> History</label>
                           </div>
                          <div>
                      <select size="4" ID="ListHistory" runat="server" class="listbox form-control" style="Height: 85px;Width: 100%;overflow: auto;">
                       </select>              
                        </div>
                            </div>
                      </div>
                 
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
                            <label class="bmd-label-floating">Current Location</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <label class="bmd-label-floating">:</label>
                            <asp:Label ID="lblLoc" class="bmd-label-floating serverlbl" runat="server"></asp:Label>
                      
    
                       <label class="bmd-label-floating">Current Username</label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                         <label class="bmd-label-floating">:</label>
                            <asp:label class="bmd-label-floating serverlbl" ID="lblName"  runat="server"> </asp:label>
                      
                          </div>
                       <asp:DropDownList ID="DropDownMonth" runat="server" CssClass="dropdownmonth" Visible ="false" >
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
                      <asp:DropDownList ID="DropDownYear" runat="server" CssClass="dropdownyear" Visible ="false" >
                            <asp:ListItem Text="2019" Value="2019"></asp:ListItem>
                              <asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                              <asp:ListItem Text="2021" Value="2021"></asp:ListItem>
                             <asp:ListItem Text="2022" Value="2022"></asp:ListItem>
                              <asp:ListItem Text="2023" Value="2023"></asp:ListItem>
                               <asp:ListItem Text="2024" Value="2024"></asp:ListItem>
                             <asp:ListItem Text="2025" Value="2025"></asp:ListItem>
                         
                      </asp:DropDownList>
  
   
     

</asp:Content>