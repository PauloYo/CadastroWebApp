# 📊 Analýtics - FastRequest

Este projeto é um **dashboard interativo em Streamlit** para análise de
métricas de clientes, pedidos, produtos e receita de um sistema de
pedidos.

------------------------------------------------------------------------

## 🚀 Funcionalidades

-   **Clientes**
    -   Idade média dos clientes
    -   Distribuição de idade
    -   Cadastros por mês
    -   Clientes ativos vs inativos
-   **Pedidos**
    -   Evolução de pedidos por mês
    -   Média de pedidos por cliente
    -   Taxa de cancelamento e entrega
    -   Distribuição dos cancelamentos por tipo de entrega (%)
-   **Produtos**
    -   Top 5 produtos mais vendidos
    -   Público consumidor por gênero para os Top 5 produtos
-   **Receita**
    -   Receita mensal: Entregues vs Cancelados
    -   Diferença mensal de receita (Entregue - Cancelado)
    -   Receita anual consolidada
-   **Análises Avançadas**
    -   Identificação de outliers nos valores totais de pedidos

------------------------------------------------------------------------

## 🛠️ Tecnologias Utilizadas

-   [Python](https://www.python.org/)
-   [Streamlit](https://streamlit.io/)
-   [Plotly](https://plotly.com/python/)
-   [SQLAlchemy](https://www.sqlalchemy.org/)
-   [PostgreSQL](https://www.postgresql.org/)
-   Pandas

------------------------------------------------------------------------

## 📂 Estrutura do Projeto

    ├── app.ipynb           # código com jupyter notebook    
    ├── dashboard.py        # Código principal do dashboard
    ├── README.md           # Documentação do projeto

------------------------------------------------------------------------

## ⚙️ Como Rodar o Projeto

1.  **Clone o repositório**

    ``` bash
    git clone https://github.com/seu-usuario/seu-repositorio.git
    cd seu-repositorio
    ```

2.  **Crie um ambiente virtual**

    ``` bash
    python -m venv venv
    source venv/bin/activate   # Linux/Mac
    venv\Scripts\activate    # Windows
    ```

3.  **Instale as dependências**

    ``` bash
    pip install -r requirements.txt
    ```

4.  **Configure o acesso ao banco de dados**

    -   Edite a variável `DATABASE_URL` em `dashboard.py` com as credenciais
        do seu banco PostgreSQL.

    Exemplo:

    ``` python
    DATABASE_URL = "postgresql://usuario:senha@host:porta/nome_banco"
    ```

5.  **Execute o Streamlit**

    ``` bash
    streamlit run dashboard.py
    ```

6.  **Acesse no navegador**

    -   O Streamlit abrirá automaticamente em: <http://localhost:8501>

------------------------------------------------------------------------

## 📊 Exemplo de Visualização

O dashboard apresenta gráficos interativos de **linha, barra, pizza e
boxplot**, permitindo insights rápidos sobre os dados.

------------------------------------------------------------------------

## 📜 Licença

Este projeto está sob a licença MIT. Sinta-se à vontade para usar,
modificar e compartilhar.
