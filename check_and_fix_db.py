import psycopg2
import os

def check_and_fix_database():
    # Connection string from appsettings.Development.json
    conn_string = "Host=trolley.proxy.rlwy.net;Port=12027;Database=railway;Username=postgres;Password=IWztuihqsKmzsKMsxNsCjxbZbThQNrFg"
    
    try:
        # Parse connection parameters
        params = {}
        for param in conn_string.split(';'):
            key, value = param.split('=', 1)
            params[key.lower()] = value
        
        # Connect to database
        conn = psycopg2.connect(
            host=params['host'],
            port=int(params['port']),
            database=params['database'],
            user=params['username'],
            password=params['password']
        )
        
        cursor = conn.cursor()
        
        # Check if ImagemUrl column exists
        cursor.execute("""
            SELECT column_name 
            FROM information_schema.columns 
            WHERE table_name = 'Produtos' AND column_name = 'ImagemUrl'
        """)
        
        result = cursor.fetchone()
        
        if result:
            print("ImagemUrl column found. Removing it...")
            
            # Remove the ImagemUrl column
            cursor.execute('ALTER TABLE "Produtos" DROP COLUMN IF EXISTS "ImagemUrl"')
            conn.commit()
            print("✅ ImagemUrl column removed successfully!")
            
            # Add the migration record to prevent EF from trying to apply it again
            cursor.execute("""
                INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion") 
                VALUES ('20250906152619_RemoveImagemUrlColumn', '9.0.0')
                ON CONFLICT ("MigrationId") DO NOTHING
            """)
            conn.commit()
            print("✅ Migration record added to history!")
            
        else:
            print("✅ ImagemUrl column does not exist. Database is already up to date!")
            
            # Still add the migration record to prevent future issues
            cursor.execute("""
                INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion") 
                VALUES ('20250906152619_RemoveImagemUrlColumn', '9.0.0')
                ON CONFLICT ("MigrationId") DO NOTHING
            """)
            conn.commit()
            print("✅ Migration record added to history!")
        
        # Check current table structure
        cursor.execute("""
            SELECT column_name, data_type, is_nullable, column_default
            FROM information_schema.columns 
            WHERE table_name = 'Produtos'
            ORDER BY ordinal_position
        """)
        
        columns = cursor.fetchall()
        print("\n📋 Current Produtos table structure:")
        for col in columns:
            print(f"  - {col[0]} ({col[1]}, nullable: {col[2]})")
        
        cursor.close()
        conn.close()
        print("\n🎉 Database check and fix completed successfully!")
        
    except Exception as e:
        print(f"❌ Error: {e}")

if __name__ == "__main__":
    check_and_fix_database()
