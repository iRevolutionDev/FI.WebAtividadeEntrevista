﻿CREATE
PROC FI_SP_VerifyExistenceBenefic
    @CPF VARCHAR(14)
AS
BEGIN
SELECT ID,
       IDCLIENTE,
       NOME,
       CPF
FROM BENEFICIARIOS WITH (NOLOCK)
WHERE CPF = @CPF
END