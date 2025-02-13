create database mice_gym_bd;
use mice_gym_bd;

create table usuario (
    id_user int primary key auto_increment,
    nome_user varchar(100),
    senha_user varchar(100),
    email_user varchar(100),
    cpf_user varchar(100),
    telefone_user varchar(100)
);

insert into usuario values (null, 'admin', 'admin', 'admin', null, null);

create table academia (
    id_aca int primary key auto_increment,
    nome_aca varchar(100),
    endereco_aca varchar(100),
    numero_aca varchar(100)
);

create table fornecedor (
    id_forn int primary key auto_increment,
    nomefantasia_forn varchar(100),
    razaosocial_forn varchar(100),
    cnpj_forn varchar(100),
    endereco_forn varchar(100),
    cidade_forn varchar(100),
    estado_forn varchar(100),
    email_forn varchar(100),
    telefone_forn varchar(100),
    responsavel_forn varchar(100)
);

create table funcionario (
    id_fun int primary key auto_increment,
    nome_fun varchar(100),
    cpf_fun varchar(100),
    rg_fun varchar(100),
    ctps_fun varchar(100),
    funcao_fun varchar(100),
    setor_fun varchar(100),
    sala_fun varchar(100),
    telefone_fun varchar(100),
    uf_fun varchar(100),
    cidade_fun varchar(100),
	bairro_fun varchar(100),
    numero_fun varchar(100),
    cep_fun varchar(100)
);

create table cliente (
    id_cli int primary key auto_increment,
    nome_cli varchar(100),
    datanascimento_cli date,
    rg_cli varchar(100),
    cpf_cli varchar(100),
    sexo_cli varchar(100),
	email_cli varchar(100),
    telefone_cli varchar(100),
    uf_cli varchar(100),
    cidade_cli varchar(100),
    bairro_cli varchar(100),
	numero_cli varchar(100),
	cep_cli varchar(100),
    fk_academia_cli int,
    foreign key (fk_academia_cli) references academia(id_aca)
);

create table equipamentos (
    id_equi int primary key auto_increment,
    nome_equi varchar(100),
    valor_equi double,
    quantidade_equi int,
    descricao_equi varchar(100),
    codigo_equi varchar(100),
    fk_academia_equi int,
    fk_fornecedor_equi int,
    foreign key (fk_academia_equi) references academia(id_aca),
    foreign key (fk_fornecedor_equi) references fornecedor(id_forn)
);

create table produto (
    id_pro int primary key auto_increment,
    nome_pro varchar(100),
    descricao_pro text,
    codigo_pro varchar(100),
    precocompra_pro decimal,
    precovenda_pro decimal,
    quantidade_pro decimal
    fk_fornecedor int,
    foreign key (fk_fornecedor) references fornecedor(id_forn)
);

create table venda (
    id_ven int primary key auto_increment,
    data_ven date,
    valor_ven float,
    fk_cliente int,
    fk_funcionario int,
    foreign key (fk_cliente) references cliente(id_cli),
    foreign key (fk_funcionario) references funcionario(id_fun)
);

create table itemvenda (
    id_item int primary key auto_increment,
    fk_ven int,
    fk_pro int,
    quantidade int,
    preco decimal(10, 2),
    foreign key (fk_ven) references venda(id_ven),
    foreign key (fk_pro) references produto(id_pro)
);

create table servico (
    id_ser int primary key auto_increment,
    nome_ser varchar(100),
    descricao_ser text,
    preco_ser decimal(10, 2)
);

create table despesa (
    id_des int primary key auto_increment,
    descricao_des text,
    valor_des decimal(10, 2),
    data_des date,
    fk_fun int,
    foreign key (fk_fun) references funcionario(id_fun)
);

create table caixa (
    id_cai int primary key auto_increment,
    saldoinicial_cai decimal,
	dataabertura_cai datetime,
    datafechamento_cai datetime,
    saldofinal_cai decimal,
    fk_fun int,
    foreign key (fk_fun) references funcionario(id_fun)
);

create table treino (
    id_tre int primary key auto_increment,
    frequencia_tre varchar(100),
    exercicios_tre varchar(100),
    seriereps_tre varchar(100),
	status_tre varchar(100),
	tempodesc_tre varchar(100),
	observacoes_tre varchar(100),
	objetivo_tre varchar(100),
	carga_tre varchar(100),
    fk_cai int,
    foreign key (fk_cai) references caixa(id_cai)
);

create table plano (
    id_plano int primary key auto_increment,
    nome_plano varchar(100),
    duracao_plano varchar(100),
    preco_plano double
);

alter table treino add fk_plano int, add foreign key (fk_plano) references plano(id_plano);

create table recebimento (
    id_recebimento int primary key auto_increment,
    descricao_recebimento text,
    valor_recebimento decimal(10, 2),
    data_recebimento date,
    fk_cliente int,
    fk_cai int,
    foreign key (fk_cliente) references cliente(id_cli),
    foreign key (fk_cai) references caixa(id_cai)
);

alter table caixa add fk_recebimento int, add fk_des int, add foreign key (fk_recebimento) references recebimento(id_recebimento), add foreign key (fk_des) references despesa(id_des);

create table entrada (
    id_entrada int primary key auto_increment,
    data_entrada date,
    quantidade int,
    fk_pro int,
    fk_fornecedor int,
    foreign key (fk_pro) references produto(id_pro),
    foreign key (fk_fornecedor) references fornecedor(id_forn)
);

create table servico_plano (
    id_servico_plano int primary key auto_increment,
    fk_ser int,
    fk_plano int,
    foreign key (fk_ser) references servico(id_ser),
    foreign key (fk_plano) references plano(id_plano)
);

create table venda_recebimento (
    id_venda_recebimento int primary key auto_increment,
    fk_ven int,
    fk_recebimento int,
    foreign key (fk_ven) references venda(id_ven),
    foreign key (fk_recebimento) references recebimento(id_recebimento)
);
