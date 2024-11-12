using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MiceGym_APIs.DAO
{
    public class VendaDAO
    {
        private readonly ConnectionMysql _conn;

        public VendaDAO()
        {
            _conn = new ConnectionMysql();
        }

        public List<Venda> List()
        {
            List<Venda> vendas = new List<Venda>();
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM Venda";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    vendas.Add(new Venda
                    {
                        Id = reader.GetInt32("id"),
                        Data = reader.GetDateTime("data"),
                        Valor = reader.GetDecimal("valor")
                    });
                }
                return vendas;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public Venda? GetById(int id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM Venda WHERE id = @id";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Venda
                    {
                        Id = reader.GetInt32("id"),
                        Data = reader.GetDateTime("data"),
                        Valor = reader.GetDecimal("valor")
                    };
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void Insert(Venda venda)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "INSERT INTO Venda (data, valor) VALUES (@data, @valor)";
                query.Parameters.AddWithValue("@data", venda.Data);
                query.Parameters.AddWithValue("@valor", venda.Valor);
                query.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void Update(Venda venda)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "UPDATE Venda SET data = @data, valor = @valor WHERE id = @id";
                query.Parameters.AddWithValue("@data", venda.Data);
                query.Parameters.AddWithValue("@valor", venda.Valor);
                query.Parameters.AddWithValue("@id", venda.Id);
                query.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
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
                var query = _conn.Query();
                query.CommandText = "DELETE FROM Venda WHERE id = @id";
                query.Parameters.AddWithValue("@id", id);
                query.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
