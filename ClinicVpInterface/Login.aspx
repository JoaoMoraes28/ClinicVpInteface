<%@ Page Title="Login" Async="true" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ClinicVpInterface.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-login">
        <div class="container-login-img">
            <section class="login">
                <header class="header-login">
                    <asp:Image ImageUrl="/log-main.png" runat="server" CssClass="img-log" />
                    <h1>Seja bem vindo(a)</h1>
                    <p>Faça login para acessar sua conta:</p>
                </header>

                <div class="container-inputs-login">
                    <div class="container-input-login">
                        <asp:Label Text="Digite seu email" runat="server" CssClass="label-login" />
                        <div class="input-login">
                            <div class="container-icon">
                                <i class="fa-solid fa-user"></i>
                            </div>
                            <asp:TextBox ID="email" runat="server" TextMode="Email" Placeholder="example@email.com" />
                        </div>
                    </div>
                    <div class="container-input-login">
                        <asp:Label Text="Digite seu email" runat="server" CssClass="label-login" />
                        <div class="input-login">
                            <div class="container-icon">
                                <i class="fa-solid fa-lock"></i>
                            </div>
                            <asp:TextBox ID="password" runat="server" TextMode="Password" Placeholder="***********" />
                        </div>
                    </div>
                    <div class="container-forget-pass">
                        <a href="#">Esqueci minha senha</a>
                    </div>
                </div>

                <asp:UpdatePanel runat="server" Style="width: 100%; height: 100px;">
                    <ContentTemplate>
                        <div class="container-button-login">
                            <asp:Label ID="resultMessage" Text="" Style="color: red;" Visible="true" runat="server" />
                            <asp:Button OnClick="PostLogin" Text="Login" runat="server" CssClass="button-login" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </section>

            <div class="container-image-login">
                <asp:Image ImageUrl="/Image_login.png" runat="server" CssClass="image-login" />
            </div>

        </div>
    </div>

</asp:Content>
