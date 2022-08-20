/************************************************************
 * Code formatted by SoftTree SQL Assistant � v11.0.35
 * Time: 19/06/2022 15:02:35
 ************************************************************/

CREATE TABLE usuarios
(
	id        INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	nome      VARCHAR(80),
	email      VARCHAR(255) NOT NULL UNIQUE,
	senha     VARCHAR(255) NOT NULL,
	ativo     TINYINT NOT NULL,
	codigo_acesso      VARCHAR(80),
	data_cadastro DATETIME NOT NULL,
	data_operacao DATETIME NOT NULL,
	data_ultimo_acesso DATETIME,
);