<%@ Page Title="Formulário de paciente" Language="C#" Async="true" MasterPageFile="~/Site.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="PatientForm.aspx.cs" Inherits="ClinicVpInterface.PatientForm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <%--Scripts para aplicar máscaras do CPF, CEP e telefones--%>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.16/jquery.mask.min.js"></script>

    <script>
$(document).ready(function () {
    $('#cpf').mask('000.000.000-00');
    $('#phone').mask('(00) 00000-0000');
    $('#emergencyPhone').mask('(00) 00000-0000');
    $('#cep').mask('00000-000');
})
</script>

    <script>
document.addEventListener("DOMContentLoaded", function () {
    const inputFile = document.getElementById("photoFile");

    inputFile.addEventListener("change", function (event) {
        const file = event.target.files[0];

        if (file) { 
            let url = URL.createObjectURL(file);

            document.getElementById("profilePhoto").src = url;

            document.getElementById("fileImg").value = file;
        }
    });
});
    </script>

    <main id="mainContainer" runat="server">

        <asp:HiddenField ClientIDMode="Static" ID="fileImg" runat="server" />

        <%--Modal de resultado de registro ou atualização de paciente--%>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div id="blackContainer" runat="server" visible="false" class="container-message-result">
                    <div class="message-result">
                        <asp:Label ID="messageResult" Style="font-size: 16px; font-weight: 500; text-align: center;" Text="Ocorreu um erro ao tentar cadastrar este paceinte!" runat="server" />
                        <asp:Button OnClick="CloseModalResponse" ID="finish" CssClass="btn btn-primary btn-finish" Text="Concluir" runat="server" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <header style="position: relative; width: 100%; display: flex; justify-content: center;">
            <div class="container-profile-photo">
                <asp:FileUpload ClientIDMode="Static" ID="photoFile" runat="server" Style="position: absolute; width: 100%; height: 100%; opacity: 0; border-radius: 100%;"></asp:FileUpload>
                <img ClientIDMode="Static" runat="server" id="profilePhoto" class="profile-img" src="/Photo_patient.png" alt="Alternate Text" />
            </div>
            <a href="PatientList.aspx"
                style="position: absolute; top: 0; left: 0; display: flex; justify-content: center; align-items: center; width: 50px; height: 50px; border-radius: 100%; background-color: var(--blue-primary)">
                <i aria-label="Volta a página de lista de pacientes."
                    style="color: var(--bg-primary);" class="fa-solid fa-arrow-left fa-lg"></i>
            </a>
        </header>

        <%--Dados do paciente--%>
        <section>
            <div class="container-input">
                <asp:TextBox ID="name" CssClass="input-patient" runat="server" placeHolder="Douglas Naconesky" />
                <div class="container-label">
                    <asp:Label Text="Nome" runat="server" />
                    <asp:Label CssClass="text-danger" Text="*" runat="server" />
                </div>
            </div>
            
            <div class="container-inputs">
                <div class="container-input">
                    <asp:TextBox ID="cpf" ClientIDMode="Static" CssClass="input-patient" runat="server" placeHolder="000.000.000-00" />
                    <div class="container-label">
                        <asp:Label Text="CPF" runat="server" />
                        <asp:Label CssClass="text-danger" Text="*" runat="server" />
                    </div>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="container-input side-input">
                            <asp:Button ID="showCivilState" OnClick="ChangeVisibilityFieldset" Style="z-index: 40; background-color: transparent; position: absolute; width: 100%; height: 100%;" runat="server" />
                            <asp:TextBox ID="civilState" CssClass="input-patient" runat="server" placeHolder="Clique para selecionar" ReadOnly="true" />
                            <div class="container-label">
                                <asp:Label Text="Estado civil" runat="server" />
                                <asp:Label CssClass="text-danger" Text="*" runat="server" />
                            </div>
                            <asp:Panel ID="panelCivilState" Visible="false" CssClass="option-choice" runat="server">
                                <asp:RadioButtonList ID="listCivilState" AutoPostBack="true" OnSelectedIndexChanged="ChangeValueIndexCivilState" runat="server">
                                    <asp:ListItem Text="Casado(a)" />
                                    <asp:ListItem Text="Solteiro(a)" />
                                    <asp:ListItem Text="Divorciado(a)" />
                                    <asp:ListItem Text="Separado(a)" />
                                    <asp:ListItem Text="Viúvo(a)" />
                                </asp:RadioButtonList>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
            <div class="container-inputs">
                <div class="container-input">
                    <asp:TextBox ID="profession" CssClass="input-patient" runat="server" placeHolder="Gerente, Diarista, Cabelereiro..." />
                    <div class="container-label">
                        <asp:Label Text="Profissão" runat="server" />
                    </div>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="container-input side-input">
                            <asp:Button ID="showGenders" OnClick="ChangeVisibilityFieldset" Style="z-index: 40; background-color: transparent; position: absolute; width: 100%; height: 100%;" runat="server" />
                            <asp:TextBox ID="gender" CssClass="input-patient" runat="server" placeHolder="Clique para selecionar" ReadOnly="true" />
                            <div class="container-label">
                                <asp:Label Text="Gênero" runat="server" />
                                <asp:Label CssClass="text-danger" Text="*" runat="server" />
                            </div>
                            <asp:Panel ID="panelGenders" Visible="false" CssClass="option-choice" runat="server">
                                <asp:RadioButtonList ID="listGenders" AutoPostBack="true" OnSelectedIndexChanged="ChangeValueIndexGender" runat="server">
                                    <asp:ListItem Text="Masculino" />
                                    <asp:ListItem Text="Feminino" />
                                </asp:RadioButtonList>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            
            <div class="container-input">
                <asp:TextBox ID="email" CssClass="input-patient" runat="server" placeHolder="example@email.com" TextMode="Email" />
                <div class="container-label">
                    <asp:Label Text="Email" runat="server" />
                    <asp:Label CssClass="text-danger" Text="*" runat="server" />
                </div>
            </div>
            
            <div class="container-inputs">
                <div class="container-input">
                    <asp:TextBox ID="phone" ClientIDMode="Static" CssClass="input-patient" runat="server" placeHolder="(11) 91234-5678" />
                    <div class="container-label">
                        <asp:Label Text="Telefone" runat="server" />
                        <asp:Label CssClass="text-danger" Text="*" runat="server" />
                    </div>
                </div>
                <div class="container-input">
                    <asp:TextBox ID="emergencyPhone" ClientIDMode="Static" CssClass="input-patient" runat="server" placeHolder="(11) 91234-5678" />
                    <div class="container-label">
                        <asp:Label Text="Telefone de emergência" runat="server" />
                    </div>
                </div>
            </div>
            
            <div class="container-inputs">
                <div class="container-input">
                    <asp:TextBox ID="weight" CssClass="input-patient" runat="server" placeHolder="91.5 (Kg)" TextMode="Number" />
                    <div class="container-label">
                        <asp:Label Text="Peso" runat="server" />
                    </div>
                </div>
                <div class="container-input">
                    <asp:TextBox ID="height" CssClass="input-patient" runat="server" placeHolder="185 (cm)" TextMode="Number" />
                    <div class="container-label">
                        <asp:Label Text="Altura" runat="server" />
                    </div>
                </div>
            </div>
            
            <div class="container-inputs">
                <div class="container-input">
                    <asp:TextBox ID="bornDate" CssClass="input-patient" runat="server" placeHolder="30/09/1998" TextMode="Date" />
                    <div class="container-label">
                        <asp:Label Text="Data de nascimento" runat="server" />
                        <asp:Label CssClass="text-danger" Text="*" runat="server" />
                    </div>
                </div>
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="container-input side-input">
                            <asp:Button ID="showBloodType" OnClick="ChangeVisibilityFieldset" Style="z-index: 40; background-color: transparent; position: absolute; width: 100%; height: 100%;" runat="server" />
                            <asp:TextBox ID="bloodType" CssClass="input-patient" runat="server" placeHolder="Clique para selecionar" ReadOnly="true" />
                            <div class="container-label">
                                <asp:Label Text="Tipo sanguíneo" runat="server" />
                            </div>
                            <asp:Panel ID="panelBloodType" Visible="false" CssClass="option-choice" runat="server">
                                <asp:RadioButtonList ID="listBloodType" AutoPostBack="true" OnSelectedIndexChanged="ChangeValueIndexBloodType" runat="server">
                                    <asp:ListItem Text="A+" />
                                    <asp:ListItem Text="A-" />
                                    <asp:ListItem Text="B+" />
                                    <asp:ListItem Text="B-" />
                                    <asp:ListItem Text="O+" />
                                    <asp:ListItem Text="O-" />
                                    <asp:ListItem Text="AB+" />
                                    <asp:ListItem Text="AB-" />
                                </asp:RadioButtonList>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </section>

        <%--Dados de endereço do paciente--%>
        <section>
            <h2>Endereço <span>(Digite o CEP primeiro para a busca de dados)</span></h2>

            <div class="container-input">
                <asp:TextBox AutoPostBack="true" ClientIDMode="Static" OnTextChanged="GetAddressByCep" ID="cep" CssClass="input-patient" runat="server" placeHolder="00000-000" />
                <div class="container-label">
                    <asp:Label Text="CEP" runat="server" />
                    <asp:Label CssClass="text-danger" Text="*" runat="server" />
                </div>
            </div>

            <div class="container-inputs">
                <div class="container-input">
                    <asp:TextBox ID="city" CssClass="input-patient" runat="server" placeHolder="Osasco" ReadOnly="true" />
                    <div class="container-label">
                        <asp:Label Text="Cidade" runat="server" />
                        <asp:Label CssClass="text-danger" Text="*" runat="server" />
                    </div>
                </div>
                <div class="container-input">
                    <asp:TextBox ID="uf" CssClass="input-patient" runat="server" placeHolder="SP" ReadOnly="true" />
                    <div class="container-label">
                        <asp:Label Text="UF" runat="server" />
                        <asp:Label CssClass="text-danger" Text="*" runat="server" />
                    </div>
                </div>
            </div>

            <div class="container-input">
                <asp:TextBox ID="district" CssClass="input-patient" runat="server" placeHolder="Bairro das Rosas" ReadOnly="true" />
                <div class="container-label">
                    <asp:Label Text="Bairro" runat="server" />
                    <asp:Label CssClass="text-danger" Text="*" runat="server" />
                </div>
            </div>

            <div class="container-inputs">
                <div class="container-input">
                    <asp:TextBox ID="street" CssClass="input-patient" runat="server" placeHolder="Av. dos Autonomistas" ReadOnly="true" />
                    <div class="container-label">
                        <asp:Label Text="Rua" runat="server" />
                        <asp:Label CssClass="text-danger" Text="*" runat="server" />
                    </div>
                </div>
                <div class="container-input">
                    <asp:TextBox ID="number" CssClass="input-patient" runat="server" placeHolder="167" />
                    <div class="container-label">
                        <asp:Label Text="N°" runat="server" />
                        <asp:Label CssClass="text-danger" Text="*" runat="server" />
                    </div>
                </div>
            </div> 
           
            <div class="container-input" style="height: 240px;">
                <asp:TextBox ID="observations" CssClass="text-patient" runat="server" TextMode="MultiLine" placeHolder="Descreva brevemente caso o paciente tenha alguma alergia, doença crônica ou faz uso de remédios..." />
                <div class="container-label">
                    <asp:Label ID="lblObs" Text="Observações" runat="server" />
                </div>
            </div>
        </section>

        <div style="width: 100%; display: flex; flex-direction: column; justify-content: space-between; align-content: center; height: 120px;">
            <asp:Label CssClass="text-danger text-center w-100" ID="errorMessage" Text="" runat="server" />
            <asp:Button ClientIDMode="Static" ID="submitButton" OnClick="ChoiceFunctionButtonSubmit" CssClass="btn btn-primary button-register" Text="Cadastrar" runat="server" />
        </div>
    
    </main>

</asp:Content>
