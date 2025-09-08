import streamlit as st
import pandas as pd
import plotly.express as px
from sqlalchemy import create_engine

DATABASE_URL = "postgresql://postgres:IWztuihqsKmzsKMsxNsCjxbZbThQNrFg@trolley.proxy.rlwy.net:12027/railway"
engine = create_engine(DATABASE_URL)

tabelas = ['"Clientes"', '"Pedidos"','"Produtos"', '"ItensPedido"']
dfs = {}
for tabela in tabelas:
    query = f"SELECT * FROM {tabela};"
    dfs[tabela] = pd.read_sql(query, engine)



# Dados simulados 
clientes = dfs['"Clientes"'].copy()
pedidos = dfs['"Pedidos"'].copy()
itens = dfs['"ItensPedido"'].copy()
produtos = dfs['"Produtos"'].copy()


# Layout
st.set_page_config(page_title="📊 Dashboard Métricas", layout="wide")
st.title("📊 Analytics - FastRequest")
st.markdown("---")

menu = st.sidebar.radio("📌 Selecione a Categoria", [
    "Clientes",
    "Pedidos",
    "Produtos",
    "Receita",
    "Análises Avançadas"
])


# CLIENTES
if menu == "Clientes":
    st.header("👥 Métricas de Clientes")

    # Idade média
    clientes['Idade'] = clientes['DataNascimento'].apply(
        lambda x: (pd.Timestamp('today') - pd.to_datetime(x)).days // 365
    )
    idade_media = clientes['Idade'].mean()
    st.metric("Idade Média dos Clientes", f"{idade_media:.1f} anos")

    st.plotly_chart(
        px.histogram(clientes, x='Idade', nbins=20, title='Distribuição de Idade dos Clientes'),
        use_container_width=True
    )

    # Cadastros por mês
    cadastros_por_mes = clientes.groupby(clientes['DataCadastro'].dt.to_period('M')).size()
    st.plotly_chart(
        px.line(x=cadastros_por_mes.index.astype(str), y=cadastros_por_mes.values,
                title="Cadastros de Clientes por Mês",
                labels={"x": "Mês", "y": "Qtd. Cadastros"}),
        use_container_width=True
    )

    # Clientes inativos
    data_limite = pedidos['DataPedido'].max() - pd.Timedelta(days=90)
    clientes_ativos = pedidos[pedidos['DataPedido'] >= data_limite]['ClienteId'].unique()
    clientes_inativos = clientes[~clientes['Id'].isin(clientes_ativos)]
    st.metric("Clientes Inativos (90 dias)", len(clientes_inativos))
    st.plotly_chart(
        px.bar(x=['Ativos', 'Inativos'],
               y=[len(clientes) - len(clientes_inativos), len(clientes_inativos)],
               title='Clientes Ativos vs Inativos (90 dias)'),
        use_container_width=True
    )


# PEDIDOS
elif menu == "Pedidos":
    st.header("📦 Métricas de Pedidos")

    # Pedidos por mês
    pedidos_por_mes = pedidos.groupby(pedidos['DataPedido'].dt.to_period('M')).size()
    st.plotly_chart(
        px.line(x=pedidos_por_mes.index.astype(str), y=pedidos_por_mes.values,
                title="Pedidos por Mês", labels={"x": "Mês", "y": "Qtd. Pedidos"}),
        use_container_width=True
    )

    # Número médio de pedidos por cliente
    media_pedidos = pedidos.shape[0] / clientes.shape[0]
    st.metric("Média de Pedidos por Cliente", f"{media_pedidos:.2f}")

    # Taxa de cancelamento vs entrega (geral)
    taxa_cancelamento = (pedidos['Status'] == 'Cancelado').mean()
    taxa_entregue = (pedidos['Status'] == 'Entregue').mean()
    st.metric("Taxa de Cancelamento", f"{taxa_cancelamento:.2%}")
    st.metric("Taxa de Entrega", f"{taxa_entregue:.2%}")

    # Cancelamento por tipo de entrega (base = total de cancelamentos)
    cancelados = pedidos[pedidos['Status'] == 'Cancelado']
    cancel_por_tipo = cancelados['TipoEntrega'].value_counts(normalize=True) * 100
    fig = px.bar(cancel_por_tipo, x=cancel_por_tipo.index, y=cancel_por_tipo.values,
                 text=cancel_por_tipo.values,
                 title="Distribuição dos Cancelamentos por Tipo de Entrega (%)")
    fig.update_traces(texttemplate='%{text:.1f}%', textposition='outside')
    st.plotly_chart(fig, use_container_width=True)


# PRODUTOS
elif menu == "Produtos":
    st.header("🛒 Métricas de Produtos")

    # Top 5 produtos (geral)
    produtos_vazao = itens.groupby('ProdutoId')['Quantidade'].sum().reset_index()
    produtos_vazao = produtos_vazao.merge(produtos[['Id', 'Nome']], left_on='ProdutoId', right_on='Id')
    top5 = produtos_vazao.sort_values('Quantidade', ascending=False).head(5)

    st.subheader("Top 5 Produtos")
    st.plotly_chart(
        px.bar(top5, x='Nome', y='Quantidade', title='Top 5 Produtos Mais Vendidos'),
        use_container_width=True
    )

    # Público consumidor por gênero para cada top produto
    st.subheader("Público Consumidor (por Gênero) - Top 5 Produtos")
    itens_pedidos = itens.merge(pedidos[['Id', 'ClienteId']], left_on='PedidoId', right_on='Id')
    itens_pedidos = itens_pedidos.merge(clientes[['Id', 'Genero']], left_on='ClienteId', right_on='Id')

    for _, row in top5.iterrows():
        prod_id = row['ProdutoId']
        nome_prod = row['Nome']
        consumidores = itens_pedidos[itens_pedidos['ProdutoId'] == prod_id]['Genero'].value_counts(normalize=True) * 100
        fig = px.pie(names=consumidores.index, values=consumidores.values,
                     title=f"Público Consumidor por Gênero - {nome_prod}")
        st.plotly_chart(fig, use_container_width=True)


# RECEITA
elif menu == "Receita":
    st.header("💰 Métricas de Receita")

    pedidos['Mes'] = pedidos['DataPedido'].dt.to_period('M')
    pedidos['Ano'] = pedidos['DataPedido'].dt.year

    # Receita mensal entregue e cancelado
    receita_entregue = pedidos[pedidos['Status'] == 'Entregue'].groupby('Mes')['ValorTotal'].sum()
    receita_cancelado = pedidos[pedidos['Status'] == 'Cancelado'].groupby('Mes')['ValorTotal'].sum()

    # --- NOVA ANÁLISE: Receita Entregues vs Cancelados ---
    fig_line = px.line(title='Receita Mensal: Entregues vs Cancelados')
    fig_line.add_scatter(x=receita_entregue.index.astype(str), y=receita_entregue.values, name='Entregues')
    fig_line.add_scatter(x=receita_cancelado.index.astype(str), y=receita_cancelado.values, name='Cancelados')
    st.plotly_chart(fig_line, use_container_width=True)

    # Diferença mensal
    receita_diff = (receita_entregue - receita_cancelado).fillna(0)
    fig_bar = px.bar(x=receita_diff.index.astype(str), y=receita_diff.values,
                     title="Diferença Mensal de Receita (Entregue - Cancelado)",
                     labels={"x": "Mês", "y": "Diferença Receita (R$)"})
    st.plotly_chart(fig_bar, use_container_width=True)

    # Receita anual
    receita_anual = pedidos[pedidos['Status'] == 'Entregue'].groupby('Ano')['ValorTotal'].sum()
    st.subheader("📅 Receita Anual")
    for ano, valor in receita_anual.items():
        st.metric(f"Receita {ano}", f"R$ {valor:,.2f}")



# ANÁLISES AVANÇADAS
elif menu == "Análises Avançadas":
    st.header("🔎 Análises Avançadas")

    # Outliers
    Q1 = pedidos['ValorTotal'].quantile(0.25)
    Q3 = pedidos['ValorTotal'].quantile(0.75)
    IQR = Q3 - Q1
    limite_inferior = Q1 - 1.5 * IQR
    limite_superior = Q3 + 1.5 * IQR
    outliers = pedidos[(pedidos['ValorTotal'] < limite_inferior) | (pedidos['ValorTotal'] > limite_superior)]
    st.metric("Qtd. de Outliers", len(outliers))
    st.plotly_chart(
        px.box(pedidos, y='ValorTotal', points='all', title='Boxplot de Valor Total por Pedido (Outliers)'),
        use_container_width=True
    )
