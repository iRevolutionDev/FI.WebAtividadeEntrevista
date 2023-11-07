var beneficiaries = [];

$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #CPF').val(obj.CPF);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
        beneficiaries = obj.Beneficiarios;
    }

    $('#formCadastro').submit(function (e) {
        e.preventDefault();

        console.log(beneficiaries);

        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                Nome: $('#formCadastro #Nome').val(),
                CEP: $('#formCadastro #CEP').val(),
                CPF: $('#formCadastro #CPF').val(),
                Email: $('#formCadastro #Email').val(),
                Sobrenome: $('#formCadastro #Sobrenome').val(),
                Nacionalidade: $('#formCadastro #Nacionalidade').val(),
                Estado: $('#formCadastro #Estado').val(),
                Cidade: $('#formCadastro #Cidade').val(),
                Logradouro: $('#formCadastro #Logradouro').val(),
                Telefone: $('#formCadastro #Telefone').val(),
                Beneficiarios: beneficiaries
            },
            error:
                function (r) {
                    switch (r.status) {
                        case 400:
                            ModalDialog("Ocorreu um erro", r.responseJSON);
                            break;
                        case 500:
                        default:
                            ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
                            break;
                    }
                },
            success:
                function (r) {
                    ModalDialog("Sucesso!", r)
                    $("#formCadastro")[0].reset();
                    window.location.href = urlRetorno;
                }
        });
    })

    const beneficiary = $('#formCadastro #beneficiary')[0];

    beneficiary.onclick = function (e) {
        e.preventDefault();

        ModalDialogWithContent("Beneficiário", `
            <form id="formBeneficiario">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="CPFBeneficiario">CPF</label>
                            <input type="text" class="form-control" id="CPFBeneficiario" placeholder="CPF">
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="form-group">
                            <label for="NomeBeneficiario">Nome</label>
                            <input type="text" class="form-control" id="NomeBeneficiario" placeholder="Nome">
                        </div>
                    </div>
                    <div class="col-md-4">
                        <button type="submit" id="addBeneficiary" class="btn btn-success btn-block" style="margin-top: 25px;">Incluir</button>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <table class="table">
                            <thead>
                                <tr>
                                    <th>CPF</th>
                                    <th>Nome</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody id="beneficiaries">
                            </tbody>
                        </table>
                    </div>
                </div>
            </form>
        `);

        const beneficiariesElementsList = $('#beneficiaries');
        const addBeneficiary = $('#addBeneficiary')[0];
        const form = $('#formBeneficiario')[0];

        let id = 0;

        const addRow = (cpf, nome, id) => {
            const row = `
                <tr data-cpf="${cpf}" data-nome="${nome}" data-id="${id}">
                    <td>${cpf}</td>
                    <td>${nome}</td>
                    <td>
                        <button class="btn btn-primary btn-sm" onclick="editBeneficiary(this)">Editar</button>
                        <button class="btn btn-danger btn-sm" onclick="removeBeneficiary(this)">Remover</button>
                    </td>
                    <input type="hidden" value="${nome}" />
                    <input type="hidden" value="${cpf}" />
                </tr>
            `;

            beneficiariesElementsList.append(row);
        }

        beneficiaries.forEach(b => {
            addRow(b.CPF, b.Nome, b.Id);
        });

        form.onsubmit = (e) => {
            e.preventDefault();
        }

        addBeneficiary.onclick = (e) => {
            e.stopPropagation();

            const cpf = $('#CPFBeneficiario')[0].value;
            const nome = $('#NomeBeneficiario')[0].value;

            if (cpf === "" || nome === "") {
                ModalDialog("Erro", "Preencha todos os campos.");
                return;
            }

            if (beneficiaries.find(b => b.CPF === cpf)) {
                ModalDialog("Erro", "CPF já cadastrado.");
                return;
            }

            beneficiaries.push({Id: id, CPF: cpf, Nome: nome});
            addRow(cpf, nome);

            form.reset();
        }
    }
});

function removeBeneficiary(element) {
    const root = $(element).closest('tr');

    const id = root.data('id');

    beneficiaries = beneficiaries.filter(b => b.Id !== id);
    console.log(beneficiaries);
    root.remove();
}


function editBeneficiary(element) {
    const root = $(element).closest('tr');

    const id = root.data('id');
    const cpf = root.data('cpf');
    const nome = root.data('nome');

    const CPFBeneficiario = $('#CPFBeneficiario')[0];
    const NomeBeneficiario = $('#NomeBeneficiario')[0];

    CPFBeneficiario.value = cpf;
    NomeBeneficiario.value = nome;

    beneficiaries = beneficiaries.filter(b => b.Id !== id);

    root.remove();
}
