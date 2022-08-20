USE [db_teste]
GO

CREATE TABLE [dbo].[usuarios_foto_perfil](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[usuario_id] INT UNIQUE NOT NULL  CONSTRAINT [fk_usuario_id] FOREIGN KEY([usuario_id]) REFERENCES [dbo].[usuarios] ([id]),
	[nome] [nvarchar](50) NULL
)
GO


