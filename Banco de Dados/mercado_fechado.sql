/* Lucas de Azevedo Juvito - 190047101
Mateus da Silva Souza   - 190035072 
*/

DROP DATABASE IF EXISTS mercado_fechado;
CREATE DATABASE mercado_fechado;
USE mercado_fechado;

CREATE TABLE usuario_comum(
    id_user_comum INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    login VARCHAR(50) NOT NULL UNIQUE,
    senha TINYTEXT NOT NULL,
    saldo DOUBLE DEFAULT 10000.0 NOT NULL CHECK(saldo >= 0)
);

CREATE TABLE acesso_usuario (
    token CHAR(32) NOT NULL PRIMARY KEY,
    id_user_comum INTEGER,
    data_expiracao DATETIME DEFAULT (NOW() + INTERVAL 24 HOUR),
    FOREIGN KEY (id_user_comum) REFERENCES usuario_comum(id_user_comum)
);

CREATE TABLE estado (
    sigla CHAR(2) PRIMARY KEY,
    descricao TINYTEXT
);

CREATE TABLE endereco(
    id_endereco INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    cep VARCHAR(9) NOT NULL,
    sigla_estado CHAR(2) NOT NULL,
    cidade TINYTEXT NOT NULL,
    bairro TINYTEXT,
    quadra TINYTEXT NOT NULL,
    numero INT NOT NULL,
    complemento TINYTEXT,
    id_proprietario INT NOT NULL,
    FOREIGN KEY (id_proprietario) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE,
    FOREIGN KEY (sigla_estado) REFERENCES estado(sigla) ON UPDATE CASCADE
);

CREATE TABLE usuario_pes_juridica(
    cnpj VARCHAR(18) PRIMARY KEY,
    id_pes_juridica INT UNIQUE,
    nome_fantasia TINYTEXT NOT NULL,
    FOREIGN KEY (id_pes_juridica) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE
);

CREATE TABLE usuario_pes_fisica(
    cpf VARCHAR(14) PRIMARY KEY,
    id_pes_fisica INT UNIQUE,
    nome TINYTEXT NOT NULL,
    data_nascimento DATE NOT NULL,
    FOREIGN KEY (id_pes_fisica) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE
);

CREATE TABLE categoria(
    id_categoria INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome TINYTEXT NOT NULL
);

CREATE TABLE produto(
    id_produto INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    nome TINYTEXT NOT NULL,
    marca TINYTEXT NOT NULL,
    fabricante TINYTEXT NOT NULL,
    ano_fabricacao SMALLINT NOT NULL,
    id_categoria INT NOT NULL,
    foto LONGBLOB,
    descricao TINYTEXT,
    FOREIGN KEY (id_categoria) REFERENCES categoria(id_categoria) ON UPDATE CASCADE
);

CREATE TABLE anuncio(
    id_anuncio INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    titulo TINYTEXT NOT NULL,
    descricao TEXT NOT NULL,
    id_produto INT NOT NULL,
    id_vendedor INT NOT NULL,
    valor DOUBLE NOT NULL,
    FOREIGN KEY (id_produto) REFERENCES produto(id_produto) ON UPDATE CASCADE,
    FOREIGN KEY (id_vendedor) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE
);

CREATE TABLE comentarios(
    id_comentario INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    descricao VARCHAR(300) NOT NULL,
    id_user_comum INT NOT NULL,
    id_anuncio INT NOT NULL,
    FOREIGN KEY (id_user_comum) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE ON DELETE CASCADE,
    FOREIGN KEY (id_anuncio) REFERENCES anuncio(id_anuncio) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE venda(
    id_venda INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    venda_hora DATETIME NOT NULL, 
    vendedor INT NOT NULL,
    comprador INT NOT NULL,
    valor DOUBLE NOT NULL,
    endereco_entrega INT NOT NULL,
    id_anuncio INT,
    FOREIGN KEY (id_anuncio) REFERENCES anuncio(id_anuncio) ON UPDATE CASCADE ON DELETE SET NULL,
    FOREIGN KEY (vendedor) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE,
    FOREIGN KEY (comprador) REFERENCES usuario_comum(id_user_comum) ON UPDATE CASCADE,
    FOREIGN KEY (endereco_entrega) REFERENCES endereco(id_endereco)
);

CREATE TABLE avaliacao(
    id_avaliacao INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    pontuacao DOUBLE NOT NULL,
    comentario TINYTEXT,
    id_venda INT NOT NULL,
    FOREIGN KEY (id_venda) REFERENCES venda(id_venda) ON UPDATE CASCADE
);

CREATE VIEW dados_agrupados_venda AS 
    SELECT cnpj IS NOT NULL AS eh_empresa, 
       venda.id_venda,
       venda.id_anuncio, 
       id_produto,
       titulo, 
       venda_hora, 
       nome, 
       nome_fantasia, 
       venda.vendedor,
       venda.comprador,
       COALESCE(pontuacao,-1) AS pontuacao FROM venda 
    LEFT JOIN anuncio ON venda.id_anuncio = anuncio.id_anuncio 
    LEFT JOIN usuario_pes_fisica ON id_pes_fisica = vendedor 
    LEFT JOIN usuario_pes_juridica ON id_pes_juridica = vendedor 
    LEFT JOIN avaliacao ON avaliacao.id_venda = venda.id_venda;

/* Usuários */ 
INSERT INTO usuario_comum (login, senha) VALUES ('user1','123');
INSERT INTO usuario_comum (login, senha) VALUES ('user2','123');
INSERT INTO usuario_comum (login, senha) VALUES ('user3','123');
INSERT INTO usuario_comum (login, senha) VALUES ('user4','123');
INSERT INTO usuario_comum (login, senha) VALUES ('user5','123');
INSERT INTO usuario_comum (login, senha) VALUES ('user6','123');
INSERT INTO usuario_comum (login, senha) VALUES ('user7','123');
INSERT INTO usuario_comum (login, senha) VALUES ('user8','123');
INSERT INTO usuario_comum (login, senha) VALUES ('user9','123');
INSERT INTO usuario_comum (login, senha) VALUES ('user10','123');

/* Estados */
INSERT INTO estado (descricao, sigla) VALUES ("Acre", "AC");
INSERT INTO estado (descricao, sigla) VALUES ("Alagoas", "AL");
INSERT INTO estado (descricao, sigla) VALUES ("Amapá", "AP");
INSERT INTO estado (descricao, sigla) VALUES ("Amazonas", "AM");
INSERT INTO estado (descricao, sigla) VALUES ("Bahia", "BA");
INSERT INTO estado (descricao, sigla) VALUES ("Ceará", "CE");
INSERT INTO estado (descricao, sigla) VALUES ("Espírito Santo", "ES");
INSERT INTO estado (descricao, sigla) VALUES ("Goiás", "GO");
INSERT INTO estado (descricao, sigla) VALUES ("Maranhão", "MA");
INSERT INTO estado (descricao, sigla) VALUES ("Mato Grosso", "MT");
INSERT INTO estado (descricao, sigla) VALUES ("Mato Grosso do Sul", "MS");
INSERT INTO estado (descricao, sigla) VALUES ("Minas Gerais", "MG");
INSERT INTO estado (descricao, sigla) VALUES ("Pará", "PA");
INSERT INTO estado (descricao, sigla) VALUES ("Paraíba", "PB");
INSERT INTO estado (descricao, sigla) VALUES ("Paraná", "PR");
INSERT INTO estado (descricao, sigla) VALUES ("Pernambuco", "PE");
INSERT INTO estado (descricao, sigla) VALUES ("Piauí", "PI");
INSERT INTO estado (descricao, sigla) VALUES ("Rio de Janeiro", "RJ");
INSERT INTO estado (descricao, sigla) VALUES ("Rio Grande do Norte", "RN");
INSERT INTO estado (descricao, sigla) VALUES ("Rio Grande do Sul", "RS");
INSERT INTO estado (descricao, sigla) VALUES ("Rondônia", "RO");
INSERT INTO estado (descricao, sigla) VALUES ("Roraima", "RR");
INSERT INTO estado (descricao, sigla) VALUES ("Santa Catarina", "SC");
INSERT INTO estado (descricao, sigla) VALUES ("São Paulo", "SP");
INSERT INTO estado (descricao, sigla) VALUES ("Sergipe", "SE");
INSERT INTO estado (descricao, sigla) VALUES ("Tocantins", "TO");
INSERT INTO estado (descricao, sigla) VALUES ("Distrito Federal", "DF");

/* Endereços */
INSERT INTO endereco (cep, sigla_estado, cidade, bairro, quadra, numero, id_proprietario)
 VALUES ('72910-000', 'GO', 'Águas Lindas de Goiás', 'Perola 1', 'quadra 9', '900', 1);
INSERT INTO endereco (cep, sigla_estado, cidade, bairro, quadra, numero, id_proprietario)
 VALUES ('97573-136', 'RS', 'Santana do Livramento', 'Avenida Doutor Hector Acosta', '1', '9', 2);
INSERT INTO endereco (cep, sigla_estado, cidade, bairro, quadra, numero, id_proprietario)
 VALUES ('29055-913', 'ES', 'Vitória', 'Praia do Canto', 'Rua Doutor Eurico de Aguiar', '426', 3);
INSERT INTO endereco (cep, sigla_estado, cidade, bairro, quadra, numero, id_proprietario)
 VALUES ('69908-654', 'AC', 'Rio Branco', 'Loteamento Santa Helena', 'Travessa da Alegria', '2', 4);
INSERT INTO endereco (cep, sigla_estado, cidade, bairro, quadra, numero, id_proprietario)
 VALUES ('69317-302', 'RR', 'Boa Vista', 'Equatorial', 'Rua Antônio Batista de Miranda', '5', 5);

 /* Pessoas Jurídicas */
INSERT INTO usuario_pes_juridica (id_pes_juridica, cnpj, nome_fantasia)
 VALUES (1, '70.450.542/0001-41', 'Ameizon');
INSERT INTO usuario_pes_juridica (id_pes_juridica, cnpj, nome_fantasia)
 VALUES (2, '58.988.745/0001-90', 'Mercado Socialista');
INSERT INTO usuario_pes_juridica (id_pes_juridica, cnpj, nome_fantasia)
 VALUES (3, '34.859.857/0001-83', 'Kibum');
INSERT INTO usuario_pes_juridica (id_pes_juridica, cnpj, nome_fantasia)
 VALUES (4, '16.780.211/0001-24', 'Locadora MaxFilmes');
INSERT INTO usuario_pes_juridica (id_pes_juridica, cnpj, nome_fantasia)
 VALUES (5, '76.259.244/0001-55', 'Loja não suspeita');

 /* Pessoas Físicas */
INSERT INTO usuario_pes_fisica (id_pes_fisica, cpf, nome, data_nascimento)
 VALUES (6, '598.736.450-71', 'Peter Benjamin Parker', '1974-08-15');
INSERT INTO usuario_pes_fisica (id_pes_fisica, cpf, nome, data_nascimento)
 VALUES (7, '908.531.950-13', 'Bruce Wayne', '1939-04-17');
INSERT INTO usuario_pes_fisica (id_pes_fisica, cpf, nome, data_nascimento)
 VALUES (8, '115.717.090-02', 'Harleen Frances Quinzel', '1990-06-20');
INSERT INTO usuario_pes_fisica (id_pes_fisica, cpf, nome, data_nascimento)
 VALUES (9, '684.307.470-79', 'Steve Rogers', '1918-07-04');
INSERT INTO usuario_pes_fisica (id_pes_fisica, cpf, nome, data_nascimento)
 VALUES (10, '433.051.580-69', 'Clark Kent', '1938-05-31');

 /* Categorias */
INSERT INTO categoria (nome) VALUES ('CONSOLES');
INSERT INTO categoria (nome) VALUES ('COMPONENTES PARA PC');
INSERT INTO categoria (nome) VALUES ('LIVROS');
INSERT INTO categoria (nome) VALUES ('FILMES');
INSERT INTO categoria (nome) VALUES ('CELULARES');

/* Produtos */
INSERT INTO produto (nome, marca, fabricante, ano_fabricacao, id_categoria, foto, descricao)
 VALUES ('Playstation 5', 'SONY', 'SONY', 2020, 1, NULL, 'cor: branco');
INSERT INTO produto (nome, marca, fabricante, ano_fabricacao, id_categoria, foto, descricao)
 VALUES ('RTX 3090', 'NVIDIA', 'ZOTAC', 2020, 2, LOAD_FILE('../site_MercadoFechado/images/RTX_3090_Founders_Edition.jpg'), 'cor: preta, led: sim');
INSERT INTO produto (nome, marca, fabricante, ano_fabricacao, id_categoria, foto, descricao)
 VALUES ('Reinos de Runeterra', 'LEAGUE OF LEGENDS', 'RIOT', 2020, 3, LOAD_FILE('../site_MercadoFechado/images/league-of-legends-reinos-de-runeterra.jpg'), 'capa: dura');
INSERT INTO produto (nome, marca, fabricante, ano_fabricacao, id_categoria, foto, descricao)
 VALUES ('Homem Aranha 2', 'SONY', 'SONY', 2004, 4, LOAD_FILE('homem_aranha2.jpg'), 'Fita VHS');
INSERT INTO produto (nome, marca, fabricante, ano_fabricacao, id_categoria, foto, descricao)
 VALUES ('Iphone 13 PRO', 'APPLE', 'APPLE', 2021, 5, LOAD_FILE('../site_MercadoFechado/images/iphone-13-pro-blue-select.png'), 'cor: azul cierra, 128GB RAM');
 /* Anuncios */
INSERT INTO anuncio (titulo, valor, descricao, id_produto, id_vendedor)
 VALUES ('PS5 muito barato', 10000.0, 'O console mais potente da útima geração', 1, 1);
INSERT INTO anuncio (titulo, valor, descricao, id_produto, id_vendedor)
 VALUES ('Placa de vídeo para turbinar os seus video jogos', 32000.0, 'A melhor placa de vídeo já feita para os meros mortais \nPS: não me responsabilizo por problemas', 2, 2);
INSERT INTO anuncio (titulo, valor, descricao, id_produto, id_vendedor)
 VALUES ('Livro do LOL', 50.0, 'LOL LOL LOL', 3, 3);
INSERT INTO anuncio (titulo, valor, descricao, id_produto, id_vendedor)
 VALUES ('Fita antiga do Homem Aranha 2', 70.0, 'Edição de colecionador rabiscada pelo diretor do filme e talvez meus filhos \nPS: talvez não tenha sido rabiscada pelo diretor', 4, 4);
INSERT INTO anuncio (titulo, valor, descricao, id_produto, id_vendedor)
 VALUES ('IPHONE 13 PRO único no Brazil', 13000.0, 'Mais atual celular android que você terá em suas mãos', 5, 5);

/* Comentários */
INSERT INTO comentarios (descricao, id_user_comum, id_anuncio) VALUES ('Mas tá muito barato mesmo, vou comprar!!', 10, 1);
INSERT INTO comentarios (descricao, id_user_comum, id_anuncio) VALUES ('É seguro isso ai mesmo?', 9, 1);
INSERT INTO comentarios (descricao, id_user_comum, id_anuncio) VALUES ('Ainda estou inseguro, será que compro?', 8, 1);
INSERT INTO comentarios (descricao, id_user_comum, id_anuncio) VALUES ('Não gostei, fui tapeado', 7, 1);
INSERT INTO comentarios (descricao, id_user_comum, id_anuncio) VALUES ('Produto é falseta mermão', 6, 1);

/* Vendas */
INSERT INTO venda (venda_hora, vendedor, comprador, valor, endereco_entrega, id_anuncio)
 VALUES (NOW(), 1, 2, 10000.0, 3, 1);
INSERT INTO venda (venda_hora, vendedor, comprador, valor, endereco_entrega, id_anuncio)
 VALUES (NOW(), 2, 1, 32000.0, 3, 2);
INSERT INTO venda (venda_hora, vendedor, comprador, valor, endereco_entrega, id_anuncio)
 VALUES (NOW(), 3, 1, 50.0, 3, 3);
INSERT INTO venda (venda_hora, vendedor, comprador, valor, endereco_entrega, id_anuncio)
 VALUES (NOW(), 4, 3, 70.0, 3, 4);
INSERT INTO venda (venda_hora, vendedor, comprador, valor, endereco_entrega, id_anuncio)
 VALUES (NOW(), 5, 4, 13000.0, 3, 5);

/* Avaliações */
INSERT INTO avaliacao (pontuacao, comentario, id_venda)
 VALUES (3, 'Caro', 1);
INSERT INTO avaliacao (pontuacao, comentario, id_venda)
 VALUES (3.5, 'Deu bom', 2);
INSERT INTO avaliacao (pontuacao, comentario, id_venda)
 VALUES (4, 'É o esperado. LOL', 3);
INSERT INTO avaliacao (pontuacao, comentario, id_venda)
 VALUES (5, 'Produto incrível', 4);
INSERT INTO avaliacao (pontuacao, comentario, id_venda)
 VALUES (0.5, 'Não gostei', 5);

/* Tokens de Login */
INSERT INTO acesso_usuario (token, id_user_comum, data_expiracao) VALUES ('f52ab92457b1a97c1dbbc794de8dea8e', 1, NOW() + INTERVAL 2 HOUR);
INSERT INTO acesso_usuario (token, id_user_comum, data_expiracao) VALUES ('263674f3b69459e3f1d42bccb8996777', 2, NOW() + INTERVAL 2 HOUR);
INSERT INTO acesso_usuario (token, id_user_comum, data_expiracao) VALUES ('5daed7a0c4c24955efac805ae9a227ea', 3, NOW() + INTERVAL 2 HOUR);
INSERT INTO acesso_usuario (token, id_user_comum, data_expiracao) VALUES ('4217696c91eddd16d83bc5c51377f84b', 4, NOW() + INTERVAL 2 HOUR);
INSERT INTO acesso_usuario (token, id_user_comum, data_expiracao) VALUES ('cf9ea7177a0d5ada66b42af6b3b1136f', 5, NOW() + INTERVAL 2 HOUR);

/* Procedures */
DELIMITER $$
CREATE PROCEDURE cadastrar_atomico (IN ehempresa BOOLEAN, IN in_login TEXT, IN in_senha TEXT, IN identificador TEXT, IN nome TEXT, IN data_nascimento TIMESTAMP)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;
    
    START TRANSACTION;
    IF ehempresa THEN
        INSERT INTO usuario_comum (login, senha) VALUES (in_login, in_senha);
        SET @insertedID = LAST_INSERT_ID();
        INSERT INTO usuario_pes_juridica (id_pes_juridica, cnpj, nome_fantasia) VALUES (@insertedID, identificador, nome);
    ELSE
        INSERT INTO usuario_comum (login, senha) VALUES (in_login, in_senha);
        SET @insertedID = LAST_INSERT_ID();
        INSERT INTO usuario_pes_fisica (id_pes_fisica, cpf, data_nascimento, nome) VALUES (@insertedID, identificador, data_nascimento, nome);
    END IF;
    COMMIT;
END $$
DELIMITER ;

DELIMITER $$
CREATE PROCEDURE criar_venda (IN vendedor BIGINT, IN comprador BIGINT, IN valor DOUBLE, IN endereco_entrega INT, IN id_anuncio INT)
BEGIN
    DECLARE saldo_comprador INT DEFAULT 0;
    DECLARE resultado INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        ROLLBACK;
        RESIGNAL;
    END;
    
    START TRANSACTION;
    SELECT saldo INTO saldo_comprador FROM usuario_comum WHERE id_user_comum = comprador;
    IF saldo_comprador >= valor THEN
        UPDATE usuario_comum SET saldo = saldo - valor WHERE id_user_comum = comprador;
        UPDATE usuario_comum SET saldo = saldo + valor WHERE id_user_comum = vendedor;
        INSERT INTO venda (venda_hora, vendedor, comprador, valor, endereco_entrega, id_anuncio) VALUES (NOW(), vendedor, comprador, valor, endereco_entrega, id_anuncio);
        SET resultado = 1;
    ELSE
        SET resultado = -1;
    END IF;
    COMMIT;
    SELECT resultado;
END $$
DELIMITER ;