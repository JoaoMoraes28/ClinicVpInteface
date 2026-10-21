<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PatientList.aspx.cs" Inherits="ClinicVpInterface.PatientList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-general">
        <h3>Pacientes Cadastrados</h3>


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

                <!-- Item do Paciente (Card) -->
                <div class="patient-card">
                    <!-- Lado Esquerdo: Ícone -->
                    <div class="patient-icon">
                        <i class="fa-solid fa-user">
                            <circle cx="12" cy="7" r="4"></circle>
                        </i>
                    </div>

                    <!-- Divisor Vertical -->
                    <div class="divider-vertical"></div>

                    <!-- Lado Direito: Detalhes/Informações -->
                    <div class="patient-info">
                        <span class="patient-detail"><strong>João Silva</strong> </span>
                        <span class="patient-detail">CPF: 123.456.789-00</span>
                        <span class="patient-detail">Tel: (11) 91234-5678</span>
                    </div>
                </div>

                <!-- Exemplo de um segundo paciente para teste -->
                <div class="patient-card">
                    <div class="patient-icon">
                        <i class="fa-solid fa-user">
                            <circle cx="12" cy="7" r="4"></circle>
                        </i>
                    </div>

                    <!-- Divisor Vertical -->
                    <div class="divider-vertical"></div>

                    <div class="patient-info">
                        <span class="patient-detail"><strong>Maria Santos</strong></span>
                        <span class="patient-detail">CPF: 987.654.321-11</span>
                        <span class="patient-detail">Tel: (11) 98765-4321</span>
                    </div>
                </div>
                
            </div>
     </div>
    
</asp:Content>
