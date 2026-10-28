<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ClinicVpInterface.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-login">
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
                        <asp:TextBox runat="server" />
                    </div>
                </div>
                <div class="container-input-login">
                    <asp:Label Text="Digite seu email" runat="server" CssClass="label-login" />
                    <div class="input-login">
                        <div class="container-icon">
                            <i class="fa-solid fa-lock"></i>
                        </div>
                        <asp:TextBox runat="server" />
                    </div>
                </div>
                <div class="container-forget-pass">
                    <a href="#">Esqueci minha senha</a>
                </div>
            </div>
            <div class="container-button-login">
                <asp:Label Text="Email e/ou senha incorretos!" Style="color: red;" Visible="true" runat="server" />
                <asp:Button Text="Login" runat="server" CssClass="button-login" />
            </div>
        </section>
    </div>

</asp:Content>
