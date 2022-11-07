/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.0.35
 * Time: 24/08/2022 00:09:50
 ************************************************************/

CREATE TABLE [dbo].[usuarios_foto_perfil]
(
	[id]             [BIGINT] IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[usuario_id]     BIGINT UNIQUE NOT NULL CONSTRAINT [fk_usuario_id] FOREIGN KEY([usuario_id]) REFERENCES [dbo].[usuarios] ([id]) 
	ON DELETE CASCADE
	ON UPDATE CASCADE,
	[nome]           [varchar](50) NULL,
	[mime_type]      [varchar](50) NULL,
	[caminho] [varchar](255) NULL,
	[arquivo] [VARBINARY](MAX) NULL  
);
