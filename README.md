
# curso-dotnet-core-web-api
Projeto desenvolvido no curso para desenvolver uma Rest API com DotNet Core 5, EntityFramework e Identity da Microsoft

## Passos para concluir a etapa 00-WebAPI-Básica
1) Instalacao do Ambiente:
- Instalar o Git
  * Fazer o clone deste repositório
  * Criar o diretório \Coding dentro do diretório do seu usuário
  * Configurar este diretório para ficar no Acesso Rápido do file explorer
- Instalar Visual Studio Code
- Instalar Visual Studio Community
- Instalar o .Net Core 5
  * .Net versão 5.0.7 com a SDK 5.0.301
- Instalar MySQL Community Server
- Instalar MySQL Client - WorkBench
- Instalar o Postman

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

13)	Começaremos a trabalhar com os modelos de dados que fazem parte do Domínio, ou seja, da regra de negócio. Vamos mover a classe Post.cs para o seguinte diretório:
- \Domain\Models\Post.cs

14) Em seguida, vamos criar a classe PostRepository.cs que tratará a persistencia de dados que faz parte da camada de Infrastructure. Esta classe ficará no seguinte diretório:
- \Infrastructure\Data\Repositories\PostRepository.cs

15) Agora, com os modelos e as interações de dados feitos, vamos aplicar as regras de negócio em uma classe chamada PostService.cs que ficará em:
- \Domain\Services\PostService.cs

16) Vamos colocar as controllers na camada Application, pois ela serve como uma ponte entre o usuário (as requisições) e as funções na service.
- \Application\Controllers\PostController.cs

17) E para finalizar, vou criar um diretorio para colocarmos nossos contextos dentro da camada de Dados que fina na Infrastructure
- \Infrastructure\Data\Contexts\MySQLContext.cs

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

24) Adicione as informações do JWT no JSON do arquivo appsettings.json

25) Colocaremos todas as classes customizadas do Identity na camada Config dentro de Infrastructure. Então vamos criar o seguinte diretorio:
- \Infrastructure\Config\Identity\

26) Dentro da pasta Identity colocaremos as classes que eu deixei preparado para vocês no repositório do git.
- "CustomClaimTypes.cs"
- "JwtSecurityKey.cs" 
- "TokenJWT.cs" 
- "TokenJWTBuilder"

27) Configure o Identity para ser iniciado no Startup.cs 
- Authentication 
- JwtBearer 
- app.UseAuthentication(); (Atencao, este precisa estar antes do "app.UseAuthorization();")

28) Vamos testar se a configuração do Identity está ok. Para isso, decore a PostController.cs com o decorador [Authorize] para proteger todas rotas da controller. Rode a aplicação e faça uma requisição em qualquer rota do Controller para testar.

29) Agora vamos criar a classe UserRepository.cs que irá interagir com os dados do usuário no banco de dados.

30) Para recebermos e retornarmos as informações do Login e Cadastra utilizaremos classes do tipo DTO (Data Transfer Object), que como o próprio nome diz, são classes apenas para transferência de informações. É como se fosse um modelo porém ele não é persistido em Banco de Dados. Apenas uma classe simples. 

Estes arquivos DTOs ficarão dentro do seguinte diretorio:

Dentro do diretório Models Domain insira uma nova pasta chamada "DTOs" e crie as 3 classes a seguir: i. "LoginDto.cs" ii. "RegisterDto.cs" iii. "SsoDto.cs" b. Faça os passos do 15 até o 17 novamente, trocando o nome de Área para User. c. 
- \Domain\Models\DTOs\SignInDTO.cs: Através desta classe receberemos o Username e Password para autenticação
- \Domain\Models\DTOs\SignUpDTO.cs: Através desta classe receberemos todos os dados necessário para um novo cadastro
- \Domain\Models\DTOs\SsoDTO.cs: Através desta classe retornaremos o Access Token e os dados do usuário que acabou de fazer o processo de Sign In

31) Apesar de estarmos trabalhando com o modelo de usuário, em nosso domínio utilizarei a palabra Auth para tudo referente a autenticação de usuário. Então Criaremos a classe AuthService.cs que adicionará as funcionalidades do usuário como Cadastrar e Login.

32) Crie a classe AuthController.cs para rotearmos as requisições http referentes ao usuário para as respectivas funções da Service.

33) Adicione o decorador Authorize nas classes de Controllers, e em cada função que quisermos deixar o acesso liberado sem autenticação colocaremos o decorador AllowAnonymous.

34) Para finalizar basta testar todas as funções sem autenticação e com autentição.

# Fontes para consultas:
- Jose Carlos Macoratti: https://www.youtube.com/watch?v=L1bJUKZV0b0
- Domain Driven Design: https://www.devmedia.com.br/introducao-ao-ddd-em-net/32724
