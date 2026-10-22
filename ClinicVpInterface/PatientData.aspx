<%@ Page Title="Dados do Paciente" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientData.aspx.cs" Inherits="ClinicVpInterface.PatientData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Modal Overlay (Fundo desfocado/escurecido) --%>
    <div id="modalOverlay" runat="server" class="modal-overlay" visible="true">
        <div class="modal-card">
            
            <div class="top-card">
                <span></span>
                <asp:LinkButton ID="btnClose" runat="server" OnClick="BtnClose_Click" CssClass="close-btn">
                    <h3>X</h3>
                </asp:LinkButton>
            </div>

            <asp:Image ID="imgPatientPhoto" runat="server" ImageUrl="/Photo_patient.png" AlternateText="Foto Paciente" />
            
            <h2><asp:Literal ID="litName" runat="server" /></h2>
            
            <div class="row-data-container">
                <span><strong>CPF: </strong><asp:Literal ID="litCpf" runat="server" /></span>
                <span><strong>Estado Civil: </strong><asp:Literal ID="litCivilState" runat="server" /></span>
            </div>

            <div class="row-data-container">
                <span><strong>Gênero: </strong><asp:Literal ID="litGender" runat="server" /></span>
                <span><strong>Profissão: </strong><asp:Literal ID="litProfession" runat="server" /></span>
            </div>
            
            <div class="row-data-container">
                <span><strong>Email: </strong><asp:Literal ID="litEmail" runat="server" /></span>
            </div>

            <div class="row-data-container">
                <span><strong>Telefone: </strong><asp:Literal ID="litPhone" runat="server" /></span>
                <span><strong>Emergência: </strong><asp:Literal ID="litEmergencyPhone" runat="server" /></span>
            </div>

            <div class="row-data-container">
                <span><strong>Peso: </strong><asp:Literal ID="litWeight" runat="server" /></span>
                <span><strong>Altura: </strong><asp:Literal ID="litHeight" runat="server" /></span>
            </div>

            <div class="row-data-container">
                <span><strong>Data de Nascimento: </strong><asp:Literal ID="litBornDate" runat="server" /></span>
                <span><strong>Idade: </strong><asp:Literal ID="litAge" runat="server" /></span>
            </div>

            <div class="row-data-container">
                <span><strong>Tipo sanguíneo: </strong><asp:Literal ID="litBloodType" runat="server" /></span>
                <span><strong>Cadastro: </strong><asp:Literal ID="litCreatedAt" runat="server" /></span>
            </div>

            <div class="row-data-container">
                <span><strong>Endereço: </strong><asp:Literal ID="litAddress" runat="server" /></span>
            </div>
            
            <div class="row-data-container">
                <span><asp:Literal ID="litObservations" runat="server" /></span>
            </div>

            <div class="row-data-container">
                <asp:Button ID="btnUpdate" runat="server" Text="Atualizar dados" OnClick="BtnUpdate_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnDelete" runat="server" Text="Deletar" OnClick="BtnDelete_Click" CssClass="btn btn-danger" />
            </div>

        </div>
    </div>
</asp:Content>