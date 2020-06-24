<%@ Page Title="Card Holders Service" MasterPageFile="~/Site.Master" Language="C#" AutoEventWireup="true" CodeBehind="CardHolderService.aspx.cs" Inherits="WebRole1.AdminForms.CardHolderService" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="booking" class="section">
        <div class="section-center">
            <div class="container pt-3">
                <div class="row ">
                    <div class="booking-form">
                        <div class="form-header">
                            <h1>Card Holders Services</h1>
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
                                    <div class="form-btn">
                                        <asp:Button ID="FetchBtn" runat="server" Text="Fetch Data" class="submit-btn" OnClick="FetchDat"/>
                                    </div>
                                </div>

                            </div>

                            <div class="row">

                            </div>

                            <div class="row mb-1">
                                <asp:GridView class="table table-dark table-bordered mt-3 w-75  mx-auto  rounded" ID="HolderTable" runat="server">
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
