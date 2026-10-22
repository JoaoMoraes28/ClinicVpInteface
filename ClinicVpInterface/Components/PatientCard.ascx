<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PatientCard.ascx.cs" Inherits="ClinicVpInterface.Components.PatientCard" %>

<a href="/PatientData.aspx?id=<%# PatientId %>" style="text-decoration: none; color: inherit;">
    <article class="patient-card">
        <div class="containter-patient-photo">
            <img runat="server" id="photo" class="photo-profile" src="/Photo_patient.png" alt="Foto de perfil do paciente." />
        </div>
        <div class="datas-container">
            <p id="name" runat="server" class="name"></p>
            <p id="cpf" runat="server" class="cpf">CPF:</p>
            <p id="phone" runat="server" class="phone">Telefone</p>
        </div>
    </article>
</a>