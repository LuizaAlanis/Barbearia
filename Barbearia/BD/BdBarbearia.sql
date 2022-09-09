drop database if exists bdBarbearia;

create database bdBarbearia
default character set utf8
default collate utf8_general_ci;

Use bdBarbearia;

create table tbLogin(
	usuario varchar(50) primary key,
	senha varchar(10),
	tipo int
);

create table tbCliente(
	codCli int primary key,
	nomeCli varchar(60),
	telefoneCli varchar(18),
    celularCli varchar(18),
    emailCli varchar(150)
);

create table tbBarbeiro(
	codBarbeiro int primary key,
	nomeBarbeiro varchar(60)
);

create table tbReserva(
	codReserva int primary key auto_increment,
	codCli int,
    codBarbeiro int,
    dataReserva date,
    horaReserva varchar(10)
);

ALTER TABLE tbReserva ADD CONSTRAINT fk_cli FOREIGN KEY(codCli) REFERENCES tbCliente(codCli);
ALTER TABLE tbReserva ADD CONSTRAINT fk_barbeiro FOREIGN KEY(codBarbeiro) REFERENCES tbBarbeiro(codBarbeiro);

-- Views

create view vwReserva
as select
	tbReserva.codReserva as 'Codigo',
    DATE_FORMAT(tbReserva.dataReserva, "%d/%m/%Y") as 'Data',
	tbReserva.horaReserva as 'Hora',
	tbBarbeiro.nomeBarbeiro as 'Barbeiro',
    tbCliente.nomeCli as 'Cliente'
from tbReserva
	inner join tbBarbeiro
	on tbBarbeiro.codBarbeiro = tbReserva.codBarbeiro
	inner join tbCliente 
	on tbCliente.codCli = tbReserva.codCli;
    
create view vwCliente
as select
	tbCliente.codCli as 'Código',
    tbCliente.nomeCli as 'Nome',
    tbCliente.telefoneCli as 'Telefone',
    tbCliente.celularCli as 'Celular',
    tbCliente.emailCli as 'Email'
from tbCliente;

create view vwBarbeiro
as select
	tbBarbeiro.codBarbeiro as 'Código',
    tbBarbeiro.nomeBarbeiro as 'Nome'
from tbBarbeiro;
   
-- Inserts para teste  
   
insert into tblogin values
('Sasha Braus','12345',2),
('Hange Zoe','12345',1); -- consegue cadastrar, atualizar e excluir
    
insert into tbCliente(codCli, nomeCli, telefoneCli, celularCli, emailCli)
values(1,'Zeke Yeager','5432-2332','11 5331-25422','zekeyeager@aot.com'),
	  (2,'Levi ackerman','4332-6378','11 4324-38532','leviackerman@aot.com');

insert into tbBarbeiro(codBarbeiro, nomeBarbeiro)
values(1,'Erwin smith'),
	  (2,'Floch Forster');
	
-- Selects

select * from tbLogin;
select * from vwCliente;
select * from vwBarbeiro;
select * from vwReserva;

-- Procedures

/* Inserir cliente */
delimiter $$
create procedure spInsCli(
    in vCodCli int, 
    in vNomeCli varchar(60), 
    in vTelefoneCli varchar(18),
    in vCelularCli varchar(18), 
    in vEmailCli varchar(150)
)
begin
	insert into tbCliente(codCli, nomeCli, telefoneCli, celularCli, emailCli)
	values(vCodCli, vNomeCli, vTelefoneCli, vCelularCli, vEmailCli);
end $$
delimiter ;

/* SP atualizar cliente */

delimiter $$
create procedure spAtCli(
    in vCodCli int, 
    in vNomeCli varchar(60), 
    in vTelefoneCli varchar(18),
    in vCelularCli varchar(18), 
    in vEmailCli varchar(150)
)
begin
	update tbCliente set 
    nomeCli = @vNomeCli,
    telefoneCli = @vTelefoneCli,
    celularCli = @vCelularCli,
    emailCli = @vEmailCli
    where codCli = @vCodCli;
end $$
delimiter ;

call spInsCli(3,'Eren Yeager', '4344-5324', '11 8342-45678', 'erenyeager@aot.com');

/* Inserir barbeiro */
delimiter $$
create procedure spInsBarbeiro(
    in vCodBarbeiro int, 
    in vNomeBarbeiro varchar(60)
)
begin
	insert into tbBarbeiro(codBarbeiro, nomeBarbeiro)
	values(vCodBarbeiro, vNomeBarbeiro);
end $$
delimiter ;

call spInsBarbeiro(3,'Armin Arlert');

/* Inserir reserva */

delimiter $$
create procedure spInsReserva(
	vCodCli int,
    vCodBarbeiro int,
    vDataReserva date,
    vHoraReserva varchar(10)
)
begin
	insert into tbReserva(codCli, codBarbeiro, dataReserva, horaReserva)
	values(vCodCli, vCodBarbeiro, vDataReserva, vHoraReserva);
end $$
delimiter ;

call spInsReserva(2,3,'2021-05-05','10:00');

-- Criando um usuário para acessar o banco e adicionando as respectivas permissões

create user 'userBarbearia'@'localhost' identified with mysql_native_password by '1234567';
grant all privileges on bdBarbearia.* to 'userBarbearia'@'localhost';

delimiter $$
create procedure spAtualizaReserva(
	vCodCli int,
    vCodBarbeiro int,
    vDataReserva date,
    vHoraReserva varchar(10)
)
begin
	insert into tbReserva(codCli, codBarbeiro, dataReserva, horaReserva)
	values(vCodCli, vCodBarbeiro, vDataReserva, vHoraReserva);
end $$
delimiter ;


call spAtCli(5,'Hange Zoe','4324 4325','11 4324-44325','hangezoe@aot.com');