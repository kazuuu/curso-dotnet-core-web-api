
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

Fontes para consultas:
- Jose Carlos Macoratti: https://www.youtube.com/watch?v=L1bJUKZV0b0
- Domain Driven Design: https://www.devmedia.com.br/introducao-ao-ddd-em-net/32724