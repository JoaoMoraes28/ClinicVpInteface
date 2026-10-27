<%@ Page Title="Lista de Pacientes" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientList.aspx.cs" Inherits="ClinicVpInterface.PatientList" %>

<%@ Register Src="~/Components/PatientCard.ascx" TagName="Card" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="updMain" runat="server" Style="padding: 8px 32px;">
        <ContentTemplate>

            <%-- Modal Overlay --%>
            <div id="modalOverlay" runat="server" class="modal-overlay" visible="false">
                <div class="modal-card">

                    <div class="top-card">
                        <span></span>
                        <asp:LinkButton ID="btnClose" runat="server" OnClick="BtnClose_Click" CssClass="close-btn">
                            <h3>X</h3>
                        </asp:LinkButton>
                    </div>
                    <div class="title-card">
                        <div style="border: solid; border-width: 2px; border-color: var(--blue-primary); border-radius: 100%;">
                            <asp:Image ID="imgPatientPhoto" runat="server" ImageUrl="/Photo_patient.png" AlternateText="Foto Paciente" />
                        </div>

                        <div class="title-content">
                            <h2>
                                <asp:Literal ID="litName" runat="server" />
                            </h2>
                        </div>
                    </div>
                    <div class="row-data-container">
                        <span><strong>CPF: </strong>
                            <asp:Literal ID="litCpf" runat="server" /></span>
                        <span><strong>Estado Civil: </strong>
                            <asp:Literal ID="litCivilState" runat="server" /></span>
                    </div>

                    <div class="row-data-container">
                        <span><strong>Gênero: </strong>
                            <asp:Literal ID="litGender" runat="server" /></span>
                        <span><strong>Profissão: </strong>
                            <asp:Literal ID="litProfession" runat="server" /></span>
                    </div>

                    <div class="row-data-container">
                        <span><strong>Email: </strong>
                            <asp:Literal ID="litEmail" runat="server" /></span>
                    </div>

                    <div class="row-data-container">
                        <span><strong>Telefone: </strong>
                            <asp:Literal ID="litPhone" runat="server" /></span>
                        <span><strong>Emergência: </strong>
                            <asp:Literal ID="litEmergencyPhone" runat="server" /></span>
                    </div>

                    <div class="row-data-container">
                        <span><strong>Peso: </strong>
                            <asp:Literal ID="litWeight" runat="server" /></span>
                        <span><strong>Altura: </strong>
                            <asp:Literal ID="litHeight" runat="server" /></span>
                    </div>

                    <div class="row-data-container">
                        <span><strong>Data de Nascimento: </strong>
                            <asp:Literal ID="litBornDate" runat="server" /></span>
                        <span><strong>Idade: </strong>
                            <asp:Literal ID="litAge" runat="server" /></span>
                    </div>

                    <div class="row-data-container">
                        <span><strong>Tipo sanguíneo: </strong>
                            <asp:Literal ID="litBloodType" runat="server" /></span>
                        <span><strong>Cadastro: </strong>
                            <asp:Literal ID="litCreatedAt" runat="server" /></span>
                    </div>

                    <div class="row-data-container">
                        <span><strong>Endereço: </strong>
                            <asp:Literal ID="litAddress" runat="server" /></span>
                    </div>

                    <div class="row-data-container">
                        <span>
                            <asp:Literal ID="litObservations" runat="server" /></span>
                    </div>

                    <div class="row-data-container">
                        <asp:Button ID="btnUpdate" runat="server" Text="Atualizar dados" OnClick="BtnUpdate_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="btnDelete" runat="server" Text="Deletar" OnClick="BtnDelete_Click" CssClass="btn btn-danger" />
                    </div>

                </div>
            </div>

            <%-- Lista Principal --%>
            <div class="container-general">
                <h3>Pacientes Cadastrados</h3>

                <div class="container-header">
                    <div class="search-container">
                        <button type="button" class="btn-search">
                            <i class="fa fa-search"></i>
                        </button>
                        <input type="text" placeholder="Busque um paciente por nome ou CPF" />
                    </div>

                    <div class="add-patient-container">
                        <a href="/PatientForm.aspx">Adicionar Paciente</a>
                        <i class="fa-solid fa-user"></i>
                        <span class="add-patient-text">+</span>
                    </div>
                </div>
                

                <div class="container-body">
                    <div class="patient-list-container">
                        <asp:Repeater ID="rptPatients" runat="server" OnItemCommand="RptPatients_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelectCard" runat="server" CommandName="SelectPatient" CommandArgument='<%# Eval("Id") %>' CssClass="card-link-wrapper">
                                    <uc1:Card runat="server" PatientId='<%# Eval("Id") %>' Name='<%# Eval("Name") %>' Cpf='<%# Eval("Cpf") %>' Phone='<%# Eval("Phone") %>' Photo='<%# Eval("Photo") %>'/>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
