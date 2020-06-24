<%@ Page Title="Flight Reservation" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FRS.aspx.cs" Inherits="WebRole1.UserForms.FRS" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="booking" class="section">
        <div class="section-center">
            <div class="container pt-3">
                <div class="row ">
                    <div class="booking-form">
                        <div class="form-header">
                            <h1>Flight Reservation</h1>
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
                                    <h6 class="pl-3 text-white">From</h6>
                                </div>
                                <div class="col-md-6">
                                    <h6 class="pl-3 text-white">To</h6>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="FromList" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="UpdateToList">
                                            
                                        </asp:DropDownList>
                                        <span class="select-arrow"></span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ToList" runat="server" CssClass="form-control">
                                           
                                        </asp:DropDownList>
                                        <span class="select-arrow"></span>
                                    </div>
                                </div>
                            </div>
                           
                            <div class="row">
                                
                                <div class="col-md-6">
                                    <h6 class="pl-3 text-white">Passenger Name</h6>
                                </div>

                                <div class="col-md-6">
                                    <h6 class="pl-3 text-white">Passport Number</h6>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="PassengerBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="PassportBox" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <h6 class="pl-3 text-white">Type</h6>
                                </div>
                                                                
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:DropDownList ID="TypeList" runat="server" CssClass="form-control">
                                            <asp:ListItem>Infant</asp:ListItem>
                                            <asp:ListItem>Child</asp:ListItem>
                                            <asp:ListItem>Adult</asp:ListItem>
                                            <asp:ListItem>Senior</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                        <span class="select-arrow"></span>
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
                                        <input ID="PriceLabel" type="number" runat="server" Enabled="false" class="form-control"></input>
                                    </div>
                                </div>
                               
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-btn">
                                        <asp:Button ID="cancelBtn" runat="server" Text="Cancel" class="submit-btn" OnClick="CancelBtn"/>

                                </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-btn">
                                        <asp:Button ID="hotelBtn" runat="server" Text="Hotel" class="submit-btn" OnClick="HotelBtn"/>

                                </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-btn">
                                        <asp:Button ID="paymentBtn" runat="server" Text="Payment" class="submit-btn" OnClick="PaymentBtn"/>

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
