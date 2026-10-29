<%@ Page Title="Lista de Pacientes" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientList.aspx.cs" Inherits="ClinicVpInterface.PatientList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server" Style="padding: 8px 32px;">
    
    <%-- Referência para o arquivo JavaScript que você criou --%>
    <script src="<%= ResolveUrl("~/Scripts/patientFeedback.js") %>" type="text/javascript"></script>

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
                <div style="border: solid; border-width: 2px; border-color: var(--blue-primary); border-radius: 100%;" class="imgProfileDatasContainer">
                    <asp:Image ID="imgPatientPhoto" runat="server" ImageUrl="/Photo_patient.png" AlternateText="Foto Paciente" CssClass="imgProfileDatas" />
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
                <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn-search" OnClick="BtnSearch_Click" CausesValidation="false">
                            <i class="fa fa-search"></i>
                </asp:LinkButton>
                <asp:TextBox AutoPostBack="true" OnTextChange="BtnSearch_Click" id="search" runat="server" placeholder="Busque um paciente por nome ou CPF" />
            </div>

            <a href="/PatientForm.aspx" class="add-patient-container" style="align-content: center;">
                <p style="color: #ffffff; font-weight: bold; font-size: 16px; margin-top: 14px;">Adicionar Paciente</p>
                <i class="fa-solid fa-user"></i>
                <span class="add-patient-text">+</span>
            </a>
        </div>

        <asp:UpdatePanel ID="updMain" runat="server">
            <ContentTemplate>
                <div class="container-body">
                    <div class="patient-list-container">
                        <asp:Repeater ID="rptPatients" runat="server" OnItemCommand="RptPatients_ItemCommand">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelectCard" runat="server" CommandName="SelectPatient" CommandArgument='<%# Eval("Id") %>' CssClass="card-link-wrapper">
                                    <article class="patient-card">
                                        <div class="container-patient-photo">
                                            <img runat="server" id="photo" class="photo-profile" src='<%# string.IsNullOrEmpty(Eval("Photo") as string) ? "/Photo_patient.png" : Eval("Photo") %>' alt="Foto de perfil do paciente." />
                                        </div>
                                        <div class="datas-container">
                                            <p id="name" runat="server" class="name"><%# Eval("Name") %></p>
                                            <p id="cpf" runat="server" class="cpf">CPF: <%# FormatCpf(Eval("Cpf")) %></p>
                                            <p id="phone" runat="server" class="phone">Telefone: <%# FormatPhone(Eval("Phone")) %></p>
                                        </div>
                                    </article>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <%-- Card de Feedback / Toast de Sucesso com Barra de Progresso --%>
    <div id="successToast" class="toast-overlay" style="display: none;">
        <div class="toast-card">
            <button type="button" class="toast-close-btn" onclick="hideSuccessToast()">×</button>
        
            <div class="toast-content">
                <p class="toast-message">Paciente inativado com sucesso!</p>
                <div class="toast-spinner"></div>
            </div>

            <div class="toast-progress-bar-container">
                <div id="toastProgressBar" class="toast-progress-bar"></div>
            </div>
        </div>
    </div>

        <%-- Card de Feedback / Toast de Erro com Barra de Progresso --%>
    <div id="errorToast" class="toast-overlay" style="display: none;">
        <div class="toast-card toast-card-error">
            <button type="button" class="toast-close-btn" onclick="hideErrorToast()">×</button>
    
            <div class="toast-content">
                <p id="errorToastMessage" class="toast-message">Erro ao processar a solicitação.</p>
                <div class="toast-spinner toast-spinner-error"></div>
            </div>

            <div class="toast-progress-bar-container">
                <div id="errorToastProgressBar" class="toast-progress-bar toast-progress-bar-error"></div>
            </div>
        </div>
    </div>
</asp:Content>
