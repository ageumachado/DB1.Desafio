# DB1 Projeto Desafio

Projeto desenvolvido do zero como desafio técnico

> :construction: Projeto concluído :construction:

# 🛠️ Tecnologias utilizadas
- `.Net 8`
- `Entity framework core 8`
- `Swagger`
- `Versionamento de endpoint`
- `Padrão Restfull`
- `DDD`
- `TDD (xUnit)`
- `Migration (CodeFirst)`
- `Mapping (EntityTypeConfiguration)`
- `Injeção de dependência`
- `AutoMapper`
- `UnitOkWork`
- `Repository`
- `FluentValidation`
- `EventDriven`
- `Github`
- `Banco de dados SqlServer`
- `Visual Studio 2022`
# Arquitetura
Solução desenvolvida utilizando Clean Architecture com as seguintes camadas:
- `DB1.Core`: Biblioteca de utilidades
- `DB1.WebApi.Core`: Biblioteca de utilidades para utilização na camada da API
- `DB1.Desafio.Domain`: Camada de domínio com entidades e contratos de repositórios
- `DB1.Desafio.Tests`: Camada de testes de unidade de domínio
- `DB1.Desafio.Infra`: Camada de implementação dos repositórios e conexões externas
- `DB1.Desafio.Application`: Camada de implementação dos casos de uso e recursos de classes para request e response, classes de comunicação
- `DB1.Desafio.Api`: Camada de implementação dos endpoins da api e documentação com swagger

# 📁 Acesso ao projeto
Você pode abrir o projeto acessando https://github.com/ageumachado/DB1.Desafio

# 🛠️ Abrir e rodar o projeto
**Após baixar o projeto, abra no visual studio 2022 e siga as intruções abaixo**
- Execute a limpeza da solução
- Compile a solução
- Insira a string de conexão do seu servidor de banco de dados no arquivo appsettings.Development
- Abra a guia do console do gerenciador de pacotes no visual studio e selecione o projeto padrão **DB1.Desafio.Infra**
- Execute os comandos para a realização da criação do banco de dados baseado nos arquivos de migração (comando: update-database)
- Execute o projeto (F5)
- Abrirá a página de gerenciamento de endpoins do swagger e a api estará pronta para ser utilizada
