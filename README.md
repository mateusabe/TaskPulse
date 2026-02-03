# Task Pulse

Sistema de gerenciamento de tarefas com controle de SLA, upload de arquivos e notificações, desenvolvido como desafio técnico utilizando **.NET 8**, seguindo princípios de **Clean Architecture**, **DDD light** e boas práticas de engenharia de software.

---

## 🧱 Arquitetura Utilizada

O projeto foi estruturado seguindo **Clean Architecture**, separando claramente responsabilidades e garantindo baixo acoplamento entre camadas:

```
src/
 ├── TaskPulse.Api            → Camada de apresentação (Controllers, DTOs)
 ├── TaskPulse.Application    → Casos de uso, Commands, Queries, Handlers
 ├── TaskPulse.Domain         → Entidades, regras de negócio, Value Objects
 ├── TaskPulse.Infrastructure → EF Core, PostgreSQL, File Storage, Background Services
 └── TaskPulse.Tests          → Testes unitários e de integração
```

### Por que Clean Architecture?

* Facilita manutenção e evolução
* Permite testar regras de negócio sem dependências externas
* Infraestrutura pode ser trocada (DB, Storage, Notificações) sem impacto no domínio

---

## 🧠 Design Patterns Utilizados

### 1️⃣ **Mediator (MediatR)**

Utilizado para desacoplar Controllers da lógica de negócio.

* Controllers apenas enviam comandos/queries
* Handlers concentram a regra de cada caso de uso

**Benefícios:**

* Código mais limpo
* Facilita testes
* Evita controllers inchados

---

### 2️⃣ **Command / Query (CQRS light)**

Separação clara entre:

* **Commands** → ações que alteram estado (CreateTask, CompleteTask)
* **Queries** → apenas leitura (GetTasks, GetTaskById)

**Benefícios:**

* Clareza de intenção
* Menos efeitos colaterais
* Queries mais performáticas

---

### 3️⃣ **Observer**

Utilizado no monitoramento de SLA expirado.

* O `SlaMonitorService` observa tarefas não concluídas
* Quando o SLA expira, dispara notificações

Hoje a notificação é simulada via polling na api, mas o padrão permite facilmente:

* E-mail
* Push notification
* WebSocket / SignalR

---

### 4️⃣ **Repository Pattern**

Abstração de acesso a dados definida na Application e implementada na Infrastructure.

**Benefícios:**

* Domínio e Application não conhecem EF Core
* Facilita troca de banco
* Facilita testes unitários

---

### 5️⃣ **Value Object**

Utilizado para representar o **SLA**.

* Encapsula validação
* Evita valores inválidos espalhados pelo código
* Reforça linguagem do domínio

---

## 📦 Bibliotecas e Pacotes Utilizados

### 🔹 **ASP.NET Core (.NET 8)**

Framework principal para construção da API REST.

### 🔹 **Entity Framework Core + Npgsql**

* ORM para persistência
* PostgreSQL como banco relacional
* Índices criados para queries performáticas

### 🔹 **MediatR**

* Implementação do padrão Mediator
* Comunicação desacoplada entre camadas

### 🔹 **Swagger (Swashbuckle)**

* Documentação automática da API
* Facilita testes e validação dos endpoints

### 🔹 **NUnit**

* Framework de testes
* Escolhido por aderência ao padrão utilizado pela empresa

---

## 📁 Upload e Download de Arquivos

O upload de arquivos é realizado através de uma abstração (`IFileStorage`).

* Implementação atual: **LocalFileStorage** (salva arquivos em pasta local)
* O caminho do arquivo é persistido no banco

Essa abordagem permite facilmente trocar a implementação para:

* AWS S3
* Azure Blob Storage
* Google Cloud Storage

Sem impacto nas camadas superiores.

---

## ⏰ SLA e Conclusão de Tarefas Expiradas

Uma decisão importante de negócio foi **permitir a conclusão da tarefa mesmo após o SLA expirar**.

### Por quê?

* SLA expirado indica atraso, não invalida a tarefa
* Evita bloquear o fluxo do usuário
* Reflete cenários reais de negócio

O sistema:

* Marca SLA como expirado
* Gera notificação
* Permite conclusão normalmente

Essa decisão mantém o sistema flexível e mais realista.

---

## 🧪 Testes

### 🔹 Testes Unitários

* Entidades de domínio (ex: verificação de SLA)
* Handlers de Commands e Queries

### 🔹 Testes de Integração

* API completa via `WebApplicationFactory`
* Banco em memória
* Validação real de endpoints

---

## 🚧 Maiores Desafios do Teste

* Configuração correta de testes de integração
* Isolamento do banco PostgreSQL para InMemory
* Remoção de HostedServices durante testes
* Garantir que Application não dependesse da Web
* Manter arquitetura limpa sem overengineering

Esses desafios reforçaram decisões arquiteturais importantes e boas práticas de desacoplamento.

---

## ✅ Conclusão

O **Task Pulse** foi desenvolvido priorizando:

* Qualidade de código
* Arquitetura sustentável
* Testabilidade
* Clareza de regras de negócio

O projeto está preparado para crescer, receber novas integrações e evoluir sem grandes refatorações.
