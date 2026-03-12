using MauiApp1.Models;
using SQLite;

namespace MauiApp1.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;//Serve para conectar e executar comandos no banco SQLite

        public SQLiteDatabaseHelper(string path)//É o caminho do arquivo do banco SQLite
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();// Cria a tabela Produto se ela não existir
        }

        public Task <int> Insert(Produto p) {

            return _conn.InsertAsync(p);
        }//insere um produto no banco.

        public Task<List<Produto>> Update(Produto p ) {

            string sql = "UPDATE Produto SET Descricao=?, Quantidade=?,Preco=?, WHERE id=?";

            return _conn.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id  
                );
        }//Atualiza um produto existente.

        public Task<int> Delete(int id ) {

        return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);

        }

        public Task<List<Produto>> Getall() { 
        
           return _conn.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string q) {

            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%"+ q +"%'";

            return _conn.QueryAsync<Produto>(sql);

        }


    }
}
