<%@ Page Title="Route Departing Services" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="RouteService.aspx.cs" Inherits="WebRole1.AdminForms.RouteService" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="booking" class="section">
        <div class="section-center">
            <div class="container pt-3">
                <div class="row ">
                    <div class="booking-form">
                        <div class="form-header">
                            <h1>Route Departing Services</h1>
                        </div>

                        <form class="mb-1">
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
                                <div class="form-group">
                                    <asp:DropDownList ID="depList" class="text-center text-white bg-dark w-100 border-0 rounded align-self-end" runat="server">
                                         </asp:DropDownList>
                                </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-btn">
                                        <asp:Button ID="FetchBtn" runat="server" Text="Fetch Data" class="submit-btn" OnClick="FetchDat"/>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                            </div>

                            <div class="row mb-1">
                                <asp:GridView class="table table-dark table-bordered mt-3 w-75  mx-auto  rounded" ID="RouteDepartingTable" runat="server">
                </asp:GridView>
                                </div>
                            <div class="row mb-1">
                                 <div class="form-btn">
                                    <asp:Button ID="backBtn" runat="server" Text="Back" class="submit-btn" OnClick="Back"/>
                                </div>
                            </div>
                            </form>

                        </div>
                    </div>
                </div>
            </div>
        </div>

</asp:Content>
