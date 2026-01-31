🚀 Task Pulse

Task Pulse é uma API moderna para gerenciamento de tarefas com SLA, desenvolvida como desafio técnico, com foco em arquitetura limpa, boas práticas, testabilidade e clareza de decisões técnicas.

O sistema permite criar tarefas com SLA, anexos, listar tarefas, monitorar expiração de SLA em background e gerar notificações para o usuário.

🧱 Arquitetura

O projeto segue os princípios de Clean Architecture, com uma abordagem DDD Light, separando claramente responsabilidades e dependências.

src/
 ├── TaskPulse.Api
 ├── TaskPulse.Application
 ├── TaskPulse.Domain
 ├── TaskPulse.Infrastructure
 └── TaskPulse.Tests

📌 Princípios aplicados

SOLID

Clean Architecture

DDD Light

Dependency Inversion

Separation of Concerns

Testabilidade desde o início

📦 Projetos
🔹 TaskPulse.Domain

Camada central do sistema, contendo regras de negócio puras, sem dependência de frameworks.

Entidades principais:

TaskEntity

Notification

Value Objects:

Sla

Responsabilidades:

Garantir invariantes

Regras como:

Cálculo de DueAt

Verificação de SLA expirado

Conclusão de tarefas

Marcação de notificações como lidas

👉 Nenhuma dependência externa.

🔹 TaskPulse.Application

Orquestra os casos de uso do sistema.

Contém:

Commands e Queries (CQRS)

Handlers (MediatR)

Abstrações:

ITaskRepository

IFileStorage

INotificationPublisher

Exemplos:

CreateTaskCommand

CompleteTaskCommand

GetTasksQuery

👉 A Application não conhece banco, web, EF, nem filesystem.

🔹 TaskPulse.Infrastructure

Implementações concretas das abstrações da Application.

Inclui:

Entity Framework Core (PostgreSQL)

Repositórios

File Storage local

Background Service de SLA

Observers de notificação

Mapeamento EF Core

Banco de dados (PostgreSQL):

CREATE TABLE tasks (
  id UUID PRIMARY KEY,
  title VARCHAR(150) NOT NULL,
  created_at TIMESTAMP NOT NULL,
  sla_hours INT NOT NULL,
  due_at TIMESTAMP NOT NULL,
  is_completed BOOLEAN NOT NULL DEFAULT FALSE,
  completed_at TIMESTAMP NULL,
  attachment_path TEXT NOT NULL
);

CREATE INDEX idx_tasks_is_completed ON tasks(is_completed);
CREATE INDEX idx_tasks_due_at ON tasks(due_at);


📌 Índices criados intencionalmente para garantir queries performáticas, conforme solicitado no desafio.

🔹 TaskPulse.Api

Camada de entrada da aplicação.

Responsabilidades:

Controllers REST

Validação de entrada

Swagger

Upload multipart/form-data

Versionamento de API

Exemplo de endpoint:

POST /api/v1/tasks

multipart/form-data
- Title
- SlaHours
- File (opcional)

🔹 TaskPulse.Tests

Testes automatizados usando NUnit (padrão utilizado pela empresa).

Tipos de testes

✅ Unitários

Domain (ex: IsSlaExpired)

Application Handlers

✅ Integração

API completa via WebApplicationFactory

Banco em memória (InMemory)

Infra isolada por ambiente

🧠 Design Patterns Utilizados
🧩 Factory

Usado na criação de entidades para garantir invariantes e consistência.

Por quê?

Evita entidades inválidas

Centraliza regras de criação

🧩 Value Object

Exemplo: Sla

Por quê?

Evita tipos primitivos espalhados

Encapsula validação e comportamento

Código mais expressivo e seguro

🧩 Repository

Isola acesso a dados.

Por quê?

Domain e Application não conhecem EF Core

Facilita testes

Permite troca de persistência

🧩 Mediator (MediatR)

Usado para Commands e Queries.

Por quê?

Desacopla controllers da lógica

Facilita testes

Organiza casos de uso

🧩 Observer

Usado no monitoramento de SLA e notificações.

Fluxo:

SlaMonitorService detecta SLA expirado

Dispara evento

Observers geram Notification

Usuário pode consultar e marcar como lida

⏰ SLA + Monitoramento

O sistema possui um BackgroundService que:

Executa periodicamente

Busca tarefas não concluídas

Verifica SLA expirado

Publica notificações

📌 Em ambiente de testes, esse serviço é desativado para evitar interferência.

🧪 Estratégia de Testes de Integração (Importante)

Durante os testes:

PostgreSQL é removido

EF Core usa InMemoryDatabase

Background Services são desabilitados

FileStorage é substituído por FakeFileStorage

Isso evita:

Conflito de providers EF Core

IO real

Testes instáveis

🧪 Exemplo de FakeFileStorage
public class FakeFileStorage : IFileStorage
{
    public Task<string> SaveAsync(FileUpload file, CancellationToken cancellationToken)
        => Task.FromResult("fake/path/file.txt");
}

▶️ Como rodar o projeto
Requisitos

.NET 8

PostgreSQL

Docker (opcional)

Rodar API
dotnet restore
dotnet run --project src/TaskPulse.Api


Swagger disponível em:

https://localhost:xxxx/swagger

🧠 Principais desafios do teste

Isolamento correto da infraestrutura nos testes

Conflito de providers EF Core (Postgres vs InMemory)

Upload multipart/form-data

Background Services em testes

Design de SLA com monitoramento

Arquitetura limpa sem overengineering

🏁 Conclusão

O Task Pulse foi desenvolvido com foco em:

Clareza arquitetural

Boas práticas reais de mercado

Código legível e testável

Decisões técnicas conscientes

O projeto reflete um ambiente real de desenvolvimento backend moderno, priorizando qualidade, manutenção e evolução futura.