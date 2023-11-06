$(document).ready(function () {
    const cpfInput = $('#formCadastro #CPF');

    //auto mask format on write cpf input
    cpfInput.keyup(function () {
            const cpf = cpfInput.val();
            cpfInput.val(cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, "$1.$2.$3-$4"));
        }
    );
});