/************************************************************
 * Code formatted by SoftTree SQL Assistant � v11.0.35
 * Time: 19/06/2022 15:02:35
 ************************************************************/
  CREATE TABLE contatos(
    Id int NOT NULL identity(1,1) primary key,
    Nome varchar(50) NULL,
    Sobre_Nome varchar(50) NULL,
    Email varchar(50) NULL,
    Telefone varchar(20) NULL
); 