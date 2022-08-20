/************************************************************
 * Code formatted by SoftTree SQL Assistant © v11.0.35
 * Time: 15/08/2022 00:10:49
 ************************************************************/

SELECT *
       --DELETE
FROM   [SequelizeMeta]
WHERE  [name] = '202207241456-CreateUsuariosPermissoes.js'
  
 -- DROP TABLE [SequelizeMeta]
 --DROP TABLE usuarios;
 --DROP TABLE permissoes;
 --DROP TABLE usuarios_permissoes;
 --DROP TABLE logs;

SELECT *
FROM   usuarios AS u
ORDER BY
       u.data_ultimo_acesso DESC;
       --DELETE FROM usuarios WHERE email='paulistensetecnologia@zohomail.com'
       
SELECT *
FROM   permissoes;

       
SELECT *
FROM   usuarios_permissoes;

SELECT SYSDATETIME()  
    ,SYSDATETIMEOFFSET()  
    ,SYSUTCDATETIME()  
    ,CURRENT_TIMESTAMP  
    ,GETDATE()  
    ,GETUTCDATE(); 