using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;

namespace MiceGym_APIs.Modelos
{
    public class ProdutoDAO
    {
        private static ConnectionMysql _conn;

        public ProdutoDAO()
        {
            _conn = new ConnectionMysql();
        }

        public int Insert(Produto item)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "insert into produto (nome_pro, descricao_pro, codigo_pro, precocompra_pro, precovenda_pro, quantidade_pro) " +
                                    "VALUES (@nome, @descricao, @codigo, @preco_compra, @preco_venda, @quantidade)";

                query.Parameters.AddWithValue("@nome", item.Nome);
                query.Parameters.AddWithValue("@descricao", item.Descricao);
                query.Parameters.AddWithValue("@codigo", item.Codigo);
                query.Parameters.AddWithValue("@preco_compra", item.PrecoCompra);
                query.Parameters.AddWithValue("@preco_venda", item.PrecoVenda);
                query.Parameters.AddWithValue("@quantidade", item.Quantidade);
                
                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O registro não foi inserido. Verifique e tente novamente");
                }

                return (int)query.LastInsertedId;
            }
            
            finally
            {
                _conn.Close();
            }
        }

        public List<Produto> List()
        {
            try
            {
                List<Produto> list = new List<Produto>();

                var query = _conn.Query();
                query.CommandText = "select * from produto";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Produto()
                    {
                        Id = reader.GetInt32("id_pro"),
                        Nome = reader.GetString("nome_pro"),
                        Descricao = reader.GetString("descricao_pro"),
                        Codigo = reader.GetString("codigo_pro"),
                        PrecoCompra = reader.GetDouble("precocompra_pro"),
                        PrecoVenda = reader.GetDouble("precovenda_pro"),
                        Quantidade = reader.GetDouble("quantidade_pro"),
                        
                    });
                }

                return list;
            }
            
            finally
            {
                _conn.Close();
            }
        }

        public Produto? GetById(int id)
        {
            try
            {
                Produto produto = new Produto();

                var query = _conn.Query();
                query.CommandText = "select * from produto where id_pro = @id";
                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                while (reader.Read())
                {
                    produto.Id = reader.GetInt32("id_pro");
                    produto.Nome = reader.GetString("nome_pro");
                    produto.Descricao = reader.GetString("descricao_pro");
                    produto.Codigo = reader.GetString("codigo_pro");
                    produto.PrecoCompra = reader.GetDouble("precocompra_pro");
                    produto.PrecoVenda = reader.GetDouble("precovenda_pro");
                    produto.Quantidade = reader.GetDouble("quantidade_pro");
                    
                }

                return produto;
            }
            
            finally
            {
                _conn.Close();
            }
        }

        public void Update(Produto item)
        {
            try
            {
                _conn.Open();

                var query = _conn.Query();
                query.CommandText = "update produto set nome_pro = @nome, descricao_pro = @descricao, codigo_pro = @codigo, " +
                                    "precocompra_pro = @preco_compra, precovenda_pro = @preco_venda, quantidade_pro = @quantidade" +
                                    "WHERE id = @id";

                query.Parameters.AddWithValue("@nome", item.Nome);
                query.Parameters.AddWithValue("@descricao", item.Descricao);
                query.Parameters.AddWithValue("@codigo", item.Codigo);
                query.Parameters.AddWithValue("@preco_compra", item.PrecoCompra);
                query.Parameters.AddWithValue("@preco_venda", item.PrecoVenda);
                query.Parameters.AddWithValue("@quantidade", item.Quantidade);
                query.Parameters.AddWithValue("@id", item.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O registro não foi atualizado. Verifique e tente novamente");
                }
            }
            
            finally
            {
                _conn.Close();
            }
        }

        public void Delete(int id)
        {
            try
            {
                _conn.Open();

                var query = _conn.Query();
                query.CommandText = "delete from produtos where id_pro = @id";
                query.Parameters.AddWithValue("@id", id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O registro não foi excluído. Verifique e tente novamente");
                }
            }
            
            finally
            {
                _conn.Close();
            }
        }
    }
}
