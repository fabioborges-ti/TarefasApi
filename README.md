
# Testes de Integração com ASP.NET Core e PostgreSQL via Testcontainers

Este projeto demonstra como implementar testes de integração utilizando ASP.NET Core 8, PostgreSQL e a biblioteca [Testcontainers](https://github.com/testcontainers/testcontainers-dotnet). Apesar de eu preferir outras abordagens de testes, como testes unitários e testes de carga (por sua objetividade e performance), muitos desenvolvedores têm interesse em testes de integração — por isso, apresentamos este exemplo.

---

## 🐳 Pré-requisitos: Docker

Antes de executar os testes de integração, é necessário ter o **Docker** instalado e em execução na sua máquina.

### O que é Docker?

O Docker é uma plataforma que permite criar, executar e gerenciar containers — ambientes isolados que empacotam uma aplicação e todas as suas dependências. Ele é muito útil para testes, pois permite criar rapidamente ambientes realistas (como um banco de dados PostgreSQL) sem a necessidade de instalação local.

### Como instalar o Docker?

- **Windows e Mac**: Acesse [https://www.docker.com/products/docker-desktop](https://www.docker.com/products/docker-desktop) e baixe o Docker Desktop.
- **Linux (Ubuntu)**:
```bash
sudo apt update
sudo apt install docker.io -y
sudo systemctl enable docker
sudo systemctl start docker
```

Após a instalação, verifique se o Docker está funcionando:

```bash
docker --version
```

---

## 📌 O que são Testes de Integração?

Testes de integração validam se diferentes partes do sistema (por exemplo, controladores, serviços e repositórios) interagem corretamente entre si, geralmente com uma infraestrutura real ou simulada (como banco de dados, API externa, etc.).

---

## ✅ Vantagens

- Detectam falhas de integração entre camadas (ex: controller → service → repository → DB).
- Simulam o ambiente de produção com maior fidelidade.
- Garantem que configurações como `DbContext`, migrations e dependências estão funcionando.

---

## ❌ Desvantagens

- Mais lentos que testes unitários.
- Exigem infraestrutura adicional (ex: containers Docker, base de dados real).
- Mais difíceis de manter e depurar.
- Podem ser instáveis se dependerem de tempo ou estado global.

---

## ⚙️ Pacotes NuGet Utilizados

Instale os pacotes abaixo no projeto de testes:

```bash
dotnet add package Testcontainers.PostgreSql --version 4.4.0
dotnet add package Microsoft.AspNetCore.Mvc.Testing
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package xunit
dotnet add package Microsoft.NET.Test.Sdk
```

---

## 🧪 Como funciona o teste

1. Um container PostgreSQL é iniciado automaticamente usando o pacote `Testcontainers.PostgreSql`.
2. A aplicação é inicializada com `WebApplicationFactory` usando a connection string do container.
3. As migrations são aplicadas automaticamente ao banco de testes.
4. São realizados testes como criação e consulta de tarefas.
5. O container é automaticamente descartado ao fim da execução.

---

## 🚀 Próximos Passos

- Adicionar testes de atualização (`PUT`) e exclusão (`DELETE`).
- Integrar cobertura de testes (`coverlet`) e relatórios.
- Automatizar execução em pipelines CI (ex: GitHub Actions, Azure DevOps).
- Avaliar o uso de base em memória (`InMemoryDb`) para testes mais rápidos quando possível.
- Implementar testes de carga (ex: com k6 ou JMeter) e testes unitários (usando mocks).

---

## 🧠 Considerações Pessoais

Embora os testes de integração ofereçam uma abordagem robusta para validar funcionalidades completas, prefiro manter foco em testes **unitários** (pela velocidade e facilidade de manutenção) e **de carga** (por medirem a resiliência). No entanto, para casos em que é necessário testar a aplicação de ponta a ponta, os testes de integração com Testcontainers são uma excelente ferramenta.

---

### 📄 Licença

Este projeto está licenciado sob os termos da licença MIT.

---

### 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

---
