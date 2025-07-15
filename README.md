# Chat LLaMA API

Este projeto está dividido em microserviços, facilitando escalabilidade, manutenção e integração entre diferentes tecnologias. Toda a arquitetura é orquestrada por **Docker**, permitindo que cada serviço rode de forma isolada e simplificada, sem necessidade de configurações manuais complexas.

A arquitetura principal é composta por:

- **Frontend Web:** Aplicação ASP.NET MVC (C#) que fornece a interface de chat para o usuário.
- **API Python:** Serviço FastAPI responsável por receber perguntas do usuário, encaminhar para o modelo de IA e retornar respostas.
- **Serviço de IA (Ollama) com modelo (LLaMA):** Utiliza o Ollama para executar o modelo LLaMA localmente, processando as perguntas e gerando respostas.
- **Docker:** Gerencia e executa todos os microserviços, garantindo que funcionem juntos de forma integrada e portátil.

## Sobre o modelo LLaMA e Ollama

O modelo utilizado para geração de respostas é o **LLaMA** (Large Language Model Meta AI), um dos modelos de linguagem mais avançados disponíveis atualmente. Para facilitar a execução local e integração com Python, utilizamos o **Ollama**, que permite rodar modelos LLM de forma simples e eficiente, sem necessidade de serviços externos.

O fluxo de funcionamento é:
1. O usuário envia uma pergunta pelo frontend.
2. O frontend faz uma requisição para a API Python.
3. A API Python utiliza o Ollama para consultar o modelo LLaMA e retorna a resposta ao usuário.

Esta abordagem garante flexibilidade, segurança e controle total sobre o processamento das perguntas e respostas.

## Como executar

### Executando com Docker (recomendado para iniciantes)

1. Certifique-se de ter o Docker instalado em seu computador:
   - [Download Docker Desktop](https://www.docker.com/products/docker-desktop/)

2. Abra o terminal na pasta raiz do projeto.

3. Execute o comando abaixo para iniciar todos os serviços (API Python, app .NET, etc):
   ```
   docker compose up --build
   ```
   > Este comando irá baixar as imagens necessárias, construir os containers e iniciar o sistema.

4. Acesse a aplicação web em seu navegador:
   - Frontend: [http://localhost:8080](http://localhost:8080)
   - API: [http://localhost:8081/docs](http://localhost:8081/docs) (documentação automática FastAPI)

---

## Rotas da API

### POST `/chat`
Recebe uma mensagem do usuário e retorna a resposta gerada pelo modelo LLaMA.

- **Request Body:**
  ```json
  {
    "message": "Sua pergunta aqui"
  }
  ```

- **Response Body:**
  ```json
  {
    "reply": "Resposta da IA"
  }
  ```

## CORS
A API está configurada para aceitar requisições de qualquer origem (`allow_origins=["*"]`), facilitando integração com frontends diversos.

---

Para dúvidas ou sugestões, abra uma issue neste repositório.
