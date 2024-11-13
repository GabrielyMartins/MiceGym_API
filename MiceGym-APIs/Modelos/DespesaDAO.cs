using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MiceGym_APIs.DAO
{
    public class DespesaDAO
    {
        private readonly ConnectionMysql _conn;

        public DespesaDAO()
        {
            _conn = new ConnectionMysql();
        }

        public List<Despesa> List()
        {
            List<Despesa> despesas = new List<Despesa>();
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM despesa";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    despesas.Add(new Despesa
                    {
                        Id = reader.GetInt32("id_des"),
                        Valor = reader.GetDecimal("valor_des"),
                        Data = reader.GetDateTime("data_des"),
                        Descricao = reader.GetString("descricao_des")
                    });
                }
                return despesas;
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

        public Despesa? GetById(int id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "select * from despesa where id_des = @id";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Despesa
                    {
                        Id = reader.GetInt32("id_des"),
                        Valor = reader.GetDecimal("valor_des"),
                        Data = reader.GetDateTime("data_des"),
                        Descricao = reader.GetString("descricao_des")
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

        public void Insert(Despesa despesa)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "insert into despesa (valor_des, data_des, descricao_des) " +
                                    "VALUES (@valor, @data, @descricao)";
                query.Parameters.AddWithValue("@valor", despesa.Valor);
                query.Parameters.AddWithValue("@data", despesa.Data);
                query.Parameters.AddWithValue("@descricao", despesa.Descricao);
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

        public void Update(Despesa despesa)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "update despesa set valor_des = @valor, data_des = @data, descricao_des = @descricao " +
                                    "WHERE id_des = @id";
                query.Parameters.AddWithValue("@valor", despesa.Valor);
                query.Parameters.AddWithValue("@data", despesa.Data);
                query.Parameters.AddWithValue("@descricao", despesa.Descricao);
                query.Parameters.AddWithValue("@id", despesa.Id);
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
                query.CommandText = "delete from despesa where id_des = @id";
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
