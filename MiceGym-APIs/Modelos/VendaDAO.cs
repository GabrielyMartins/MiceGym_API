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
                query.CommandText = "select * from venda";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    vendas.Add(new Venda
                    {
                        Id = reader.GetInt32("id_ven"),
                        Data = reader.GetDateTime("data_ven"),
                        Valor = reader.GetDecimal("valor_ven")
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
                query.CommandText = "select * from venda where id_ven = @id";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Venda
                    {
                        Id = reader.GetInt32("id_ven"),
                        Data = reader.GetDateTime("data_ven"),
                        Valor = reader.GetDecimal("valor_ven")
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
                query.CommandText = "insert into venda (data_ven, valor_ven) VALUES (@data, @valor)";
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
                query.CommandText = "update venda set data_ven = @data, valor_ven = @valor where id_ven = @id";
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
                query.CommandText = "delete from venda where id_ven = @id";
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
