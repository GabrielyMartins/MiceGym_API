using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MiceGym_APIs.DAO
{
    public class PlanoDAO
    {
        private readonly ConnectionMysql _conn;

        public PlanoDAO()
        {
            _conn = new ConnectionMysql();
        }

        public List<Plano> List()
        {
            List<Plano> planos = new List<Plano>();
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM Plano";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    planos.Add(new Plano
                    {
                        CodPlano = reader.GetInt32("cod_plano"),
                        NomePlano = reader.GetString("nome_plano"),
                        Preco = reader.GetDecimal("preco"),
                        Duracao = reader.GetInt32("duracao")
                    });
                }
                return planos;
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

        public Plano? GetById(int codPlano)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM Plano WHERE cod_plano = @cod_plano";
                query.Parameters.AddWithValue("@cod_plano", codPlano);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Plano
                    {
                        CodPlano = reader.GetInt32("cod_plano"),
                        NomePlano = reader.GetString("nome_plano"),
                        Preco = reader.GetDecimal("preco"),
                        Duracao = reader.GetInt32("duracao")
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

        public void Insert(Plano plano)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "INSERT INTO Plano (nome_plano, preco, duracao) " +
                                    "VALUES (@nome_plano, @preco, @duracao)";
                query.Parameters.AddWithValue("@nome_plano", plano.NomePlano);
                query.Parameters.AddWithValue("@preco", plano.Preco);
                query.Parameters.AddWithValue("@duracao", plano.Duracao);
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

        public void Update(Plano plano)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "UPDATE Plano SET nome_plano = @nome_plano, preco = @preco, duracao = @duracao " +
                                    "WHERE cod_plano = @cod_plano";
                query.Parameters.AddWithValue("@nome_plano", plano.NomePlano);
                query.Parameters.AddWithValue("@preco", plano.Preco);
                query.Parameters.AddWithValue("@duracao", plano.Duracao);
                query.Parameters.AddWithValue("@cod_plano", plano.CodPlano);
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

        public void Delete(int codPlano)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "DELETE FROM Plano WHERE cod_plano = @cod_plano";
                query.Parameters.AddWithValue("@cod_plano", codPlano);
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
