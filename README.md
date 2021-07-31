
# curso-dotnet-core-web-api
Projeto desenvolvido no curso para desenvolver uma Rest API com DotNet Core 5, EntityFramework e Identity da Microsoft

## Passos para concluir a etapa 00-WebAPI-Base
1) Instalacao do Ambiente:
- Instalar o Git
+ Fazer o clone deste repositório
- Instalar Visual Studio Code
- Visual Studio Community
- Instalar o Dotnet core 5
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

Fontes para consultas:
- Jose Carlos Macoratti: https://www.youtube.com/watch?v=L1bJUKZV0b0
