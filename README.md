
# curso-dotnet-core-web-api
Projeto desenvolvido no curso para desenvolver uma Rest API com DotNet Core 5, EntityFramework e Identity da Microsoft

## Passos para concluir a etapa 00-WebAPI-Básica
1) Instalacao do Ambiente:
- Instalar Visual Studio
- Instalar o dotnet core 5
- Instalar MySQL Community Server
- Instalar MySQL Client - WorkBench
- Instalar o Postman
- Instalar o Git

2) No Visual Studio criar um projeto Asp.Net Core Web Application com o template: Asp.Net Core Web API chamado: MyWallWebAPI

3) Testar com o Postman
- Criar uma Collection com uma Requisicao basica de Get

4) Criar o modelo/entidade Post.

5) Agora vamos configurar o EntityFramework com MySQL para persistir os dados instalando os pacotes do Nugget:
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design
- Pomelo.EntityFrameworkCore.MySql

6) Criar um Contexto chamado MySQLContext.

7) Configurar no Startup.cs o Contexto com Entity.

8) Colocar a String de Conexao no appsettings.json.

9) Abrir o Package Manager Console para criar o Migration Inicial:
- Add-Migration Initial
- Update-Database

10) Criar a classe PostController com as rotas CRUD do modelo Post.
11) Testar com o Postman.

## Passos para concluir a etapa 01-Design-Pattern
Organizar o código utilizando um Design Pattern inspirado no DDD - Domain Driven Design

12) Vamos dividir nossa aplicação em Camadas principais:
- "Domain": Essa é camada de Domínio que concentra toda a regra de negócio da aplicação.
- "Infrastructure": É a camada de mais baixo nível, responsável por prover serviços como persistência de dados, envio de email, ou seja, dar o suporte tecnológico para as demais camadas;
- "Application": Coordena as atividades da aplicação, porém, não contém regras de negócio.

14)	Começaremos a trabalhar com os modelos de dados que fazem parte do Domínio, ou seja, da regra de negócio. Vamos mover a classe Post.cs para o seguinte diretório:
- \Domain\Models\Post.cs

15) Em seguida, vamos criar a classe PostRepository.cs que tratará a persistencia de dados que faz parte da camada de Infrastructure. Esta classe ficará no seguinte diretório:
- \Infrastructure\Data\Repositories\PostRepository.cs

16) Agora, com os modelos e as interações de dados feitos, vamos aplicar as regras de negócio em uma classe chamada PostService.cs que ficará em:
- \Domain\Services\PostService.cs

17) E para finalizar, montaremos a ponte da interface com o usuário que serão as Controllers na camada Application. Criaremos a classe PostController.cs
- \Application\Controllers\PostController.cs

18) E como sempre, vamos rodar e testar nossa aplicação para garantir que deu tudo certo nossa reestruturacao.

## Passos para concluir a etapa 02-Authentication
Agora vamos Implementar o sistema de usuarios e autenticação.
Para isso, utilizaremos o Identity do Asp.Net Core que já nos fornece diversas funcionalidades como Login do usuario, Password Recover, Two factors authentication, JWT entre outras. Porém, neste aprendizado implementaremos o básico que será a atutenticação do usuário com Login e Senha.

19) Adicionar os pacotes do Identity: 
- Microsoft.AspNetCore.Authentication
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.UI

20) Crie o modelo do usuario com o nome de "ApplicationUser.cs" e faça com que ele herde de IdentityUser 
21) Crie o modelo para a Role com o nome de "ApplicationRole.cs" e faça com que ele herde de IdentityRole
22) Para o Identity trabalhar com o nosso Banco de Dados vamos alterar a nossa Context:
- Agora esta classe irá herdar o IdentityDbContext e não mais DbContext.
- Incluir os modelos ApplicationUser e ApplicationRole na classe MySQLContext

23) Criar e aplicar os DB migrations do Identity.
- No Nugget Package Console digitar: Add-Migration Authentication 
- Em seguita, digite o seguinte para aplicar a nova migration no Banco de Dados: Update-Database 

24) Implementando o JWT 
- Adicione algumas informações do JWT no JSON do arquivo appsettings.json
- Chamaremos toda parte de autenticação como Security e colocaremos na camada Infrastructure. Então vamos criar o seguinte diretorio:
- \Infrastructure\Config\Security\
- Dentro da pasta Security, vamos colocar as classes de autenticação que eu deixei prepara para vocês no repositório do git.
-- "CustomClaimTypes.cs"
-- "JwtSecurityKey.cs" 
-- "TokenJWT.cs" 
-- "TokenJWTBuilder"

Vamos configurar o Security para ser iniciado no Startup.cs 
- Authentication 
- JwtBearer 
- app.UseAuthentication(); (Atencao, este precisa estar antes do "app.UseAuthorization();")

25) Criando Sign In e Sign Up
- Criar repositorio do usuario. UserRepository
- Criar a service do usuario. UserService
- Criar os DTOs, SignUpDTO para recebermos os dados de cadastro, e LoginDTO para login do usuario e para o retorno do access token vamos criar um outro DTO. SsoDTO c. Criar a controller do usuario, UserController

a. Dentro da Domain insira uma nova pasta chamada "DTOs" e crie as 3 classes a seguir: i. "LoginDto.cs" ii. "RegisterDto.cs" iii. "SsoDto.cs" b. Faça os passos do 15 até o 17 novamente, trocando o nome de Área para User. c. Configure o Identity na Startup.cs

Adicione a AuthController.

Adicione o Authorize na MensagemController

Adicionar um método na Service responsável por adicionar um usuário a uma área e implementa-lo na Controller

Testar com o Postman.

Fontes para consultas:
- Jose Carlos Macoratti: https://www.youtube.com/watch?v=L1bJUKZV0b0
- Domain Driven Design: https://www.devmedia.com.br/introducao-ao-ddd-em-net/32724