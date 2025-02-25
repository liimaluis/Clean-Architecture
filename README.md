CleanArchMvc - Aplicação ASP.NET Core com Clean Architecture

📌 Sobre o Projeto

O CleanArchMvc é uma aplicação desenvolvida em C# utilizando ASP.NET Core que implementa os princípios da Clean Architecture e do SOLID com a arquitetura MVC (Model-View-Controller). O projeto contém uma versão visual do site, permitindo gerenciar categorias e produtos, além de uma API REST segura para operações externas.

🔥 Principais Funcionalidades

🎨 Interface Web (MVC)

Cadastro, edição e exclusão de Categorias e Produtos.

Autenticação de usuários utilizando Identity.

Controle de acesso com perfis de usuário (níveis de acesso).

🌐 API REST

O projeto também fornece uma API para integração externa, incluindo:

Autenticação e Autorização via JWT Bearer Token.

Endpoints para Produtos e Categorias, acessíveis apenas com autenticação.

Documentação interativa com Swagger.

🏗️ Arquitetura do Projeto

Este projeto segue os princípios da Clean Architecture, organizando o código em camadas bem definidas:

📁 Application → Contém a lógica de aplicação, interfaces de serviço e DTOs.
📁 Domain → Contém as entidades e interfaces de repositórios.
📁 Infra.Data → Contém a implementação do banco de dados (Entity Framework Core) e os repositórios.
📁 Infra.IoC → Contém a injeção de dependências e configurações do projeto.
📁 Presentation (MVC) → Contém a interface visual do projeto e controllers da Web.
📁 API → Contém os controllers da API REST.

🛠️ Tecnologias Utilizadas

.NET 7 / .NET Core

ASP.NET Core MVC

Entity Framework Core

SQL Server

Identity Framework (Autenticação e Autorização)

JWT Bearer Token

MediatR (Padrão CQRS)

Swagger (Swashbuckle)

AutoMapper

🚀 Como Executar o Projeto

🔧 Pré-requisitos

.NET SDK 7.0+ instalado

SQL Server instalado e configurado

📥 Clone o Repositório

git clone https://github.com/seu-usuario/CleanArchMvc.git
cd CleanArchMvc

📌 Configuração do Banco de Dados

Atualize o appsettings.json com a sua string de conexão do SQL Server

Aplique as migrations:

dotnet ef database update

🏃 Executando o Projeto

dotnet run

Acesse:

Aplicação Web: http://localhost:7249

Swagger da API: http://localhost:7099/swagger

🔑 Autenticação e Testes na API

Crie um usuário na aplicação

Realize o login para obter um token JWT

Adicione o token no Swagger (Authorize → Bearer <seu-token>)

Acesse os endpoints protegidos de produtos e categorias

🏗️ Estrutura dos Endpoints

🆔 Autenticação

POST /api/Token/Login → Realiza login e retorna um token JWT.

POST /api/Token/CreateUser → Cria um novo usuário.

📦 Produtos

GET /api/Products → Lista todos os produtos (Requer autenticação).

POST /api/Products → Cria um novo produto (Requer autenticação).

📂 Categorias

GET /api/Categories → Lista todas as categorias (Requer autenticação).

POST /api/Categories → Cria uma nova categoria (Requer autenticação).
