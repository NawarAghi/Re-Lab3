<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="booking" class="section">
        <div class="section-center">
            <div class="container pt-3">
                <div class="row ">
                    <div class="booking-form">
                        <div class="form-header">
                            <h1>Chart Resor</h1>
                        </div>
                        <form>
                          
                            <div class="row">
                                <div class="col-md-6">
                                   <div class="form-btn">
                                        <asp:Button ID="userBtn" runat="server" Text="User" class="submit-btn" OnClick="toUser"/>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-btn">
                                        <asp:Button ID="adminBtn" runat="server" Text="Admin Report" class="submit-btn" OnClick="toAdmin"/>

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
