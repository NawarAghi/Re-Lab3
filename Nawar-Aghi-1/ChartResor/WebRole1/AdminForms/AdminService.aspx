<%@ Page Title="Admin opstions" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminService.aspx.cs" Inherits="WebRole1.AdminForms.AdminService" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="booking" class="section">
        <div class="section-center">
            <div class="container pt-3">
                <div class="row ">
                    <div class="booking-form">
                        <div class="form-header">
                            <h1>Admin Services</h1>
                        </div>
                        <form class="mb-1">
                            <div class="row mb-1">
                                <div class="col-md-6">
                                     <div class="form-btn">
                                    <asp:Button ID="airportBtn" runat="server" Text="Airports" class="submit-btn" OnClick="AirportPage"/>
                                </div>
                                    </div>
                                <div class="col-md-6">
                                     <div class="form-btn">
                                    <asp:Button ID="airlineBtn" runat="server" Text="Airlines" class="submit-btn" OnClick="AirlinePage" />
                                </div>
                                    </div>
                                
                            </div>
                            <div class="row mb-1">
                                <div class="col-md-6">
                                     <div class="form-btn">
                                    <asp:Button ID="routeDepBtn" runat="server" Text="Routes Departing" class="submit-btn" OnClick="RouteDepPage"/>
                                </div>
                                    </div>
                                <div class="col-md-6">
                                     <div class="form-btn">
                                    <asp:Button ID="routeArrivBtn" runat="server" Text="Routes Arriving" class="submit-btn" OnClick="RouteArrPage"/>
                                </div>
                                    </div>
                            </div>
                            <div class="row mb-1">
                                <div class="col-md-6">
                                     <div class="form-btn">
                                    <asp:Button ID="PassengerBtn" runat="server" Text="Revenue Flight" class="submit-btn" OnClick="RevenuePage"/>
                                </div>
                                    </div>
                                <div class="col-md-6">
                                     <div class="form-btn">
                                    <asp:Button ID="revFlightBtn" runat="server" Text="Card Holders" class="submit-btn" OnClick="CardHolderPage"/>
                                </div>
                                    </div>
                                

                            </div>
                            <div class="row mb-1">
                                <div class="col-md-6">
                                     <div class="form-btn">
                                    <asp:Button ID="revDeptBtn" runat="server" Text="Transactions" class="submit-btn" OnClick="TransactionsPage"/>
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
