using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MiceGym_APIs.DAO
{
    public class CaixaDAO
    {
        private readonly ConnectionMysql _conn;

        public CaixaDAO()
        {
            _conn = new ConnectionMysql();
        }

        public int Insert(Caixa caixa)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "INSERT INTO caixa (saldoinicial_cai, dataabertura_cai, datafechamento_cai, saldofinal_cai) " +
                                    "VALUES (@saldo_inicial, @data_abertura, @data_fechamento, @saldo_final)";

                query.Parameters.AddWithValue("@saldo_inicial", caixa.SaldoInicial);
                query.Parameters.AddWithValue("@data_abertura", caixa.DataAbertura);
                query.Parameters.AddWithValue("@data_fechamento", caixa.DataFechamento);
                query.Parameters.AddWithValue("@saldo_final", caixa.SaldoFinal);
                query.ExecuteNonQuery();

                return (int)query.LastInsertedId;
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

        public List<Caixa> List()
        {
            List<Caixa> lista = new List<Caixa>();
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM caixa";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Caixa
                    {
                        Id = reader.GetInt32("id_cai"),
                        SaldoInicial = reader.GetDecimal("saldoinicial_cai"),
                        DataAbertura = reader.GetDateTime("dataabertura_cai"),
                        DataFechamento = reader.IsDBNull(reader.GetOrdinal("datafechamento_cai")) ? (DateTime?)null : reader.GetDateTime("datafechamento_cai"),
                        SaldoFinal = reader.GetDecimal("saldofinal_cai")
                    });
                }
                return lista;
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

        public Caixa? GetById(int id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM caixa WHERE id_cai = @id";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Caixa
                    {
                        Id = reader.GetInt32("id_cai"),
                        SaldoInicial = reader.GetDecimal("saldoinicial_cai"),
                        DataAbertura = reader.GetDateTime("dataabertura_cai"),
                        DataFechamento = reader.IsDBNull(reader.GetOrdinal("datafechamento_cai")) ? (DateTime?)null : reader.GetDateTime("datafechamento_cai"),
                        SaldoFinal = reader.GetDecimal("saldofinal_cai")
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

        public void Update(Caixa caixa)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "UPDATE caixa SET saldoinicial_cai = @saldo_inicial, dataabertura_cai = @data_abertura, " +
                                    "datafechamento_cai = @data_fechamento, saldofinal_cai = @saldo_final WHERE id_cai = @id";

                query.Parameters.AddWithValue("@saldo_inicial", caixa.SaldoInicial);
                query.Parameters.AddWithValue("@data_abertura", caixa.DataAbertura);
                query.Parameters.AddWithValue("@data_fechamento", caixa.DataFechamento);
                query.Parameters.AddWithValue("@saldo_final", caixa.SaldoFinal);
                query.Parameters.AddWithValue("@id", caixa.Id);
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
                query.CommandText = "DELETE FROM caixa WHERE id_cai = @id";
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
