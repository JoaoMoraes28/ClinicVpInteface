<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientCard.aspx.cs" Inherits="ClinicVpInterface.Components.PatientCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        
        <article class="patient-card">
            <div class="photo-container">
                <img src="/Patient_patient.png" alt="Foto de perfil do patiente." />
            </div>
            <div class="datas-container">
                <p>Angélica Santos da Silva</p>
                <p>CPF: 123.456.789-10</p>
                <p>Telefone: (11) 91234-5678</p>
            </div>
        </article>

    </form>
</body>
</html>
