let toastTimer = null;

function showSuccessToast() {
    const toast = document.getElementById('successToast');
    const progressBar = document.getElementById('toastProgressBar');

    if (!toast) return;

    progressBar.style.animation = 'none';
    progressBar.offsetHeight;
    progressBar.style.animation = 'progressCountdown 6s linear forwards';

    toast.style.display = 'flex';

    toastTimer = setTimeout(() => {
        hideSuccessToast();
        //window.location.reload();
    }, 6000);
}

function hideSuccessToast() {
    const toast = document.getElementById('successToast');
    if (toast) {
        toast.style.display = 'none';
        if (toastTimer) {
            clearTimeout(toastTimer);
        }
    }
}
let errorToastTimer = null;

function showErrorToast(customMessage) {
    const toast = document.getElementById('errorToast');
    const progressBar = document.getElementById('errorToastProgressBar');
    const msgElement = document.getElementById('errorToastMessage');

    if (!toast) return;

    // Se houver uma mensagem customizada, altera o texto
    if (customMessage) {
        msgElement.innerText = customMessage;
    }

    // Reinicia a animação da barra de progresso
    progressBar.style.animation = 'none';
    progressBar.offsetHeight; // Força o reflow
    progressBar.style.animation = 'progressCountdown 6s linear forwards';

    toast.style.display = 'flex';

    errorToastTimer = setTimeout(() => {
        hideErrorToast();
    }, 6000);
}

function hideErrorToast() {
    const toast = document.getElementById('errorToast');
    if (toast) {
        toast.style.display = 'none';
        if (errorToastTimer) {
            clearTimeout(errorToastTimer);
        }
    }
}