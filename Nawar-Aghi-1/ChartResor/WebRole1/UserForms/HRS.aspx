<%@ Page Title="Hotel Reservation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HRS.aspx.cs" Inherits="WebRole1.UserForms.HRS" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

        <div id="booking" class="section">
        <div class="section-center">
            <div class="container pt-3">
                <div class="row ">
                    <div class="booking-form">
                        <div class="form-header">
                            <h1>Hotel Reservation</h1>
                        </div>
                        <form>
                            <div class="row">
                                <div class="form-group">
                                    <asp:RadioButtonList ID="databaseOption" runat="server">
                                        <asp:ListItem Value="1" Selected="True">SQL</asp:ListItem>
                                        <asp:ListItem Value="0">NoSQL</asp:ListItem>

                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <h6 class="pl-3 text-white">Hotel</h6>
                                </div>
                                
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="HotelList" runat="server" CssClass="form-control">
                                            
                                        </asp:DropDownList>
                                        <span class="select-arrow"></span>
                                    </div>
                                </div>
                                
                            </div>
                           
                            <div class="row">
                                
                                <div class="col-md-6">
                                    <h6 class="pl-3 text-white">No.Nights</h6>
                                </div>

                                <div class="col-md-6">
                                    <h6 class="pl-3 text-white">Arrival Date</h6>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="NightBox" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="DateBox" TextMode="Date" Columns="2" MaxLength="3" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                </div>

                            <div class="row">

                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    
                                </div>

                                <div class="col-md-6">
                                    <h6 class="pl-3 text-white">Price</h6>
                                </div>
                                                                
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-btn">
                                        <asp:Button ID="PriceBtn" runat="server" Text="Get Price" class="submit-btn" OnClick="GetPrice"/>

                                </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:Label ID="PriceLabel" runat="server" Enabled="false" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                               
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-btn">
                                        <asp:Button ID="cancelBtn" runat="server" Text="Cancel" class="submit-btn" OnClick="CancelButton"/>

                                </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-btn">
                                        <asp:Button ID="backBtn" runat="server" Text="Back" class="submit-btn" OnClick="BackButton"/>

                                </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-btn">
                                        <asp:Button ID="paymentBtn" runat="server" Text="Payment" class="submit-btn" OnClick="PaymentButton"/>

                                </div>
                                </div>
                            </div>


                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>        
    </asp:Content>
