# EventAPI
EventAPI é um projeto avaliativo para o módulo de Programação WEB, da Lets Code que aplica conceitos avançados de WebAPI.

## Banco de Dados
Estrutura de tabelas utilizadas (SQL Server):

> CREATE TABLE CityEvent
(
	IdEvent BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT pk_IdEvent PRIMARY KEY,
	Title VARCHAR(100) NOT NULL,
	Description VARCHAR(255) NULL,
	DateHourEvent DateTime NOT NULL,
	Local VARCHAR(150) NOT NULL,
	Address VARCHAR (255) NULL,
	Price DECIMAL NULL,
	Status BIT NOT NULL
);

> CREATE TABLE EventReservation
(
	IdReservation BIGINT IDENTITY(1,1) NOT NULL CONSTRAINT pk_IdReservation PRIMARY KEY,
	IdEvent BIGINT NOT NULL CONSTRAINT fk_IdEvent FOREIGN KEY REFERENCES CityEvent(IdEvent),
	PersonName VARCHAR(200) NOT NULL,
	Quantity BIGINT
);



## Configurando o appsettings 
Insira um ederenço válido e deamais credencias onde foi criado o Banco de Dados:
 
> { <br> 
  "ConnectionStrings": { <br>
    "DefaultConnection": "Server= enderecoDaBasedeDados.com.br; Database=BaseExemplo; User Id=NomeUsuario; Password=SenhaUsuario; Encrypt=False" <br>
  }, <br> 
  
  ## Autenticação e Autorização
  É necessário gerar um Token na API de Clientes, e inserir antes de executar qualquer método da API de Eventos. 
  
  ![image](https://user-images.githubusercontent.com/38474570/190838931-f920e14b-9c07-47a5-a611-d1fd2a11e236.png)

Preencha com o token e clique em Authorize

  ![image](https://user-images.githubusercontent.com/38474570/190838939-f63e3b99-f7c5-4b96-a75f-0f1ec5b5d3df.png)
  
  Pronto, agora dependendo de sua permissão com base no token gerado na APIClientes você poderá utilizar os métodos da EventAPI

# Descrição do Projeto

Desenvolvido como projeto avaliativo para a disciplina de Programação Web III (WebAPI), da LetsCode(ADA).

## Enunciado
Construa uma API que registre e manipule eventos que acontecem na cidade, como shows, peças de teatro, eventos especiais em restaurantes, entre outros.

Implemente a documentação completa da API, utilizando Swagger. Todos os métodos devem possuir validação dos campos obrigatórios, quais os formatos de dados que a API recebe e responde e quais os possíveis status de retorno.

Construa uma API bem estruturada, respeitando as diretrizes de REST, SOLID e os princípios base de arquitetura.

Trate as exceções que forem necessárias.

Esta API deverá ter um cadastro do evento e um cadastro de reservas. Siga a estrutura apresentada abaixo:

![image](https://user-images.githubusercontent.com/38474570/190839011-8c495379-2ce7-4fdd-9199-11428325194f.png)

<b> Para o CityEvent, construa os métodos:</b> 

* Inclusão de um novo evento; *Autenticação e Autorização admin
* Edição de um evento existente, filtrando por id; *Autenticação e Autorização admin
* Remoção de um evento, caso o mesmo não possua reservas em andamento, caso possua inative-o; *Autenticação e Autorização admin
* Consulta por título, utilizando similaridades, por exemplo, caso pesquise Show, traga todos os eventos que possuem a palavra Show no título;
* Consulta por local e data;
* Consulta por range de preço e a data;

<b> Para o EventReservation, construa os métodos:</b> 

* Inclusão de uma nova reserva; *Autenticação
* Edição da quantidade de uma reserva; *Autenticação e Autorização admin
* Remoção de uma reserva; *Autenticação e Autorização admin
* Consulta de reserva pelo PersonName e Title do evento, utilizando similaridade para o title; *Autenticação

<b> Utilize para autenticação os seguintes parametros: </b> 

![image](https://user-images.githubusercontent.com/38474570/190839041-f976f9a6-887b-428a-a61d-340b16aad750.png)
