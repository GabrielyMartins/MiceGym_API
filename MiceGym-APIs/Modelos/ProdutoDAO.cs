using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;

namespace MiceGym_APIs.Modelos
{
    public class ProdutoDAO
    {
        private static ConnectionMysql conn;

        public ProdutoDAO()
        {
            conn = new ConnectionMysql();
        }

        public int Insert(Produto item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "INSERT INTO produtos (nome, descricao, codigo, preco_compra, preco_venda, quantidade, fornecedor) " +
                                    "VALUES (@nome, @descricao, @codigo, @preco_compra, @preco_venda, @quantidade, @fornecedor)";

                query.Parameters.AddWithValue("@nome", item.Nome);
                query.Parameters.AddWithValue("@descricao", item.Descricao);
                query.Parameters.AddWithValue("@codigo", item.Codigo);
                query.Parameters.AddWithValue("@preco_compra", item.PrecoCompra);
                query.Parameters.AddWithValue("@preco_venda", item.PrecoVenda);
                query.Parameters.AddWithValue("@quantidade", item.Quantidade);
                query.Parameters.AddWithValue("@fornecedor", item.Fornecedor);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O registro não foi inserido. Verifique e tente novamente");
                }

                return (int)query.LastInsertedId;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Produto> List()
        {
            try
            {
                List<Produto> list = new List<Produto>();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM produtos";

                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    list.Add(new Produto()
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        Descricao = reader.GetString("descricao"),
                        Codigo = reader.GetString("codigo"),
                        PrecoCompra = reader.GetDouble("preco_compra"),
                        PrecoVenda = reader.GetDouble("preco_venda"),
                        Quantidade = reader.GetString("quantidade"),
                        Fornecedor = reader.GetString("fornecedor")
                    });
                }

                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public Produto? GetById(int id)
        {
            try
            {
                Produto produto = new Produto();

                var query = conn.Query();
                query.CommandText = "SELECT * FROM produtos WHERE id = @id";
                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (!reader.HasRows)
                {
                    return null;
                }

                while (reader.Read())
                {
                    produto.Id = reader.GetInt32("id");
                    produto.Nome = reader.GetString("nome");
                    produto.Descricao = reader.GetString("descricao");
                    produto.Codigo = reader.GetString("codigo");
                    produto.PrecoCompra = reader.GetDouble("preco_compra");
                    produto.PrecoVenda = reader.GetDouble("preco_venda");
                    produto.Quantidade = reader.GetString("quantidade");
                    produto.Fornecedor = reader.GetString("fornecedor");
                }

                return produto;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Produto item)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "UPDATE produtos SET nome = @nome, descricao = @descricao, codigo = @codigo, " +
                                    "preco_compra = @preco_compra, preco_venda = @preco_venda, quantidade = @quantidade, fornecedor = @fornecedor " +
                                    "WHERE id = @id";

                query.Parameters.AddWithValue("@nome", item.Nome);
                query.Parameters.AddWithValue("@descricao", item.Descricao);
                query.Parameters.AddWithValue("@codigo", item.Codigo);
                query.Parameters.AddWithValue("@preco_compra", item.PrecoCompra);
                query.Parameters.AddWithValue("@preco_venda", item.PrecoVenda);
                query.Parameters.AddWithValue("@quantidade", item.Quantidade);
                query.Parameters.AddWithValue("@fornecedor", item.Fornecedor);
                query.Parameters.AddWithValue("@id", item.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O registro não foi atualizado. Verifique e tente novamente");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Delete(int id)
        {
            try
            {
                var query = conn.Query();
                query.CommandText = "DELETE FROM produtos WHERE id = @id";
                query.Parameters.AddWithValue("@id", id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O registro não foi excluído. Verifique e tente novamente");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
