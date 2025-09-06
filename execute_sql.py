import psycopg2
import os
from urllib.parse import urlparse

def execute_sql_script():
    # Connection string do appsettings.Development.json
    conn_string = "Host=trolley.proxy.rlwy.net;Port=12027;Database=railway;Username=postgres;Password=IWztuihqsKmzsKMsxNsCjxbZbThQNrFg"
    
    try:
        # Parse dos parâmetros de conexão
        params = {}
        for param in conn_string.split(';'):
            key, value = param.split('=', 1)
            params[key.lower()] = value
        
        # Conectar ao banco
        conn = psycopg2.connect(
            host=params['host'],
            port=int(params['port']),
            database=params['database'],
            user=params['username'],
            password=params['password']
        )
        
        cursor = conn.cursor()
        
        # Ler o arquivo SQL
        with open('cantina_update.sql', 'r', encoding='utf-8') as file:
            sql_script = file.read()
        
        # Executar o script
        cursor.execute(sql_script)
        conn.commit()
        
        print("✅ Script SQL executado com sucesso!")
        print("✅ Tabelas Produtos e ItensPedido criadas")
        print("✅ Dados de exemplo inseridos")
        
        # Verificar se as tabelas foram criadas
        cursor.execute("""
            SELECT table_name 
            FROM information_schema.tables 
            WHERE table_schema = 'public' 
            AND table_name IN ('Produtos', 'ItensPedido')
        """)
        
        tables = cursor.fetchall()
        print(f"\n📊 Tabelas encontradas: {[table[0] for table in tables]}")
        
        # Contar produtos inseridos
        cursor.execute("SELECT COUNT(*) FROM \"Produtos\"")
        produto_count = cursor.fetchone()[0]
        print(f"📦 Produtos cadastrados: {produto_count}")
        
        cursor.close()
        conn.close()
        
    except psycopg2.Error as e:
        print(f"❌ Erro no banco de dados: {e}")
    except FileNotFoundError:
        print("❌ Arquivo cantina_update.sql não encontrado")
    except Exception as e:
        print(f"❌ Erro inesperado: {e}")

if __name__ == "__main__":
    execute_sql_script()
