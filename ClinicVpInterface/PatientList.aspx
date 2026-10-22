<%@ Page Title="Pacientes" Language="C#" Async="true" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientList.aspx.cs" Inherits="ClinicVpInterface.PatientList" %>

<%@ Register Src="~/Components/PatientCard.ascx" TagName="Card" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-general">
        <h3>Pacientes Cadastrados</h3>

        <asp:Label Id="teste" Text="" runat="server" />
        <div class="container-header">
            <%-- Search Container Patient by CPF or Name --%>
            <div class="search-container">
                <button type="submit" class="btn-search">
                    <i class="fa fa-search"></i>
                </button>
                <input type="text" placeholder="Busque um paciente por nome ou CPF" />
            </div>
            <%-- Add Patient Button --%>
            <div class="add-patient-container">
                <a href="/PatientForm.aspx">Adicionar Paciente</a>
                <i class="fa-solid fa-user"></i>
                <span class="add-patient-text">+</span>
            </div>

        </div>

        <div class="container-body">
            <div class="patient-list-container">
                <asp:Repeater ID="rptPatients" runat="server">
                    <ItemTemplate>
                        <uc1:Card runat="server" PatientId='<%# Eval("Id") %>' Name='<%# Eval("Name") %>' Cpf='<%# Eval("Cpf") %>' Phone='<%# Eval("Phone") %>' Photo='<%# Eval("Photo") %>'/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>

</asp:Content>
