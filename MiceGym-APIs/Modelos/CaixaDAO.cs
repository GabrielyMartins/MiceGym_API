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
                query.CommandText = "INSERT INTO caixas (saldo_inicial, data_abertura, data_fechamento, saldo_final) " +
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
                query.CommandText = "SELECT * FROM caixas";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Caixa
                    {
                        Id = reader.GetInt32("id"),
                        SaldoInicial = reader.GetDecimal("saldo_inicial"),
                        DataAbertura = reader.GetDateTime("data_abertura"),
                        DataFechamento = reader.IsDBNull(reader.GetOrdinal("data_fechamento")) ? (DateTime?)null : reader.GetDateTime("data_fechamento"),
                        SaldoFinal = reader.GetDecimal("saldo_final")
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
                query.CommandText = "SELECT * FROM caixas WHERE id = @id";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Caixa
                    {
                        Id = reader.GetInt32("id"),
                        SaldoInicial = reader.GetDecimal("saldo_inicial"),
                        DataAbertura = reader.GetDateTime("data_abertura"),
                        DataFechamento = reader.IsDBNull(reader.GetOrdinal("data_fechamento")) ? (DateTime?)null : reader.GetDateTime("data_fechamento"),
                        SaldoFinal = reader.GetDecimal("saldo_final")
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
                query.CommandText = "UPDATE caixas SET saldo_inicial = @saldo_inicial, data_abertura = @data_abertura, " +
                                    "data_fechamento = @data_fechamento, saldo_final = @saldo_final WHERE id = @id";
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
                query.CommandText = "DELETE FROM caixas WHERE id = @id";
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
