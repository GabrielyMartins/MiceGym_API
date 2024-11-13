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
                query.CommandText = "select * from plano";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    planos.Add(new Plano
                    {
                        Id = reader.GetInt32("id_plano"),
                        NomePlano = reader.GetString("nome_plano"),
                        Preco = reader.GetDecimal("preco_plano"),
                        Duracao = reader.GetInt32("duracao_plano")
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
                query.CommandText = "select * from plano where id_plano = @id";
                query.Parameters.AddWithValue("@id", codPlano);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Plano
                    {
                        Id = reader.GetInt32("id_plano"),
                        NomePlano = reader.GetString("nome_plano"),
                        Preco = reader.GetDecimal("preco_plano"),
                        Duracao = reader.GetInt32("duracao_plano")
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
                query.CommandText = "insert into plano (nome_plano, preco_plano, duracao_plano) " +
                                    "VALUES (@nome_plano, @preco, @duracao)";
                query.Parameters.AddWithValue("@nome_plano", plano.NomePlano);
                query.Parameters.AddWithValue("@preco_plano", plano.Preco);
                query.Parameters.AddWithValue("@duracao_plano", plano.Duracao);
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
                query.CommandText = "update plano set nome_plano = @nome_plano, preco_plano = @preco, duracao_plano = @duracao " +
                                    "where id_plano = @cod_plano";
                query.Parameters.AddWithValue("@nome_plano", plano.NomePlano);
                query.Parameters.AddWithValue("@preco", plano.Preco);
                query.Parameters.AddWithValue("@duracao", plano.Duracao);
                query.Parameters.AddWithValue("@id", plano.Id);
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
                query.CommandText = "delete from plano where id_plano = @id";
                query.Parameters.AddWithValue("@id", codPlano);
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
