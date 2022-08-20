/************************************************************
 * Code formatted by SoftTree SQL Assistant � v11.0.35
 * Time: 19/06/2022 15:02:35
 ************************************************************/

CREATE TABLE usuarios_permissoes
(
	id        INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,
	usuario_id INT NOT NULL FOREIGN KEY REFERENCES usuarios(id),
	permissao_id INT NOT NULL FOREIGN KEY REFERENCES permissoes(id),
	ativo     TINYINT NOT NULL,
	data_cadastro DATETIME NOT NULL,
	data_operacao DATETIME NOT NULL
);