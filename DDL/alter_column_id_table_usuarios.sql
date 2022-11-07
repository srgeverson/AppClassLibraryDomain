/************************************************************
 * Code formatted by SoftTree SQL Assistant � v11.0.35
 * Time: 19/06/2022 15:02:35
 ************************************************************/

CREATE TABLE permissoes
(
	id        BIGINT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	nome      VARCHAR(220) NOT NULL,
	descricao     VARCHAR(220) NOT NULL,
	ativo     TINYINT NOT NULL,
	data_cadastro DATETIME NOT NULL,
	data_operacao DATETIME NOT NULL,
);