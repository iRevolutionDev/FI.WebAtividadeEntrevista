﻿CREATE
PROC FI_SP_IncBeneficiario
        @IdCliente INT,
        @Nome VARCHAR(50),
        @CPF VARCHAR(11),
AS
BEGIN
INSERT INTO BENEFICIARIOS (IdCliente, Nome, CPF)
VALUES (@IdCliente, @Nome, @CPF)

SELECT SCOPE_IDENTITY()
END