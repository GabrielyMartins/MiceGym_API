using MiceGym_APIs.Database;
using MiceGym_APIs.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MiceGym_APIs.DAO
{
    public class ServicoDAO
    {
        private readonly ConnectionMysql _conn;

        public ServicoDAO()
        {
            _conn = new ConnectionMysql();
        }

        public int Insert(Servico servico)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "insert into servico (descricao_ser, nome_ser, preco_ser) VALUES (@Descricao, @Nome, @Preco)";
                query.Parameters.AddWithValue("@Descricao", servico.Descricao);
                query.Parameters.AddWithValue("@Nome", servico.Nome);
                query.Parameters.AddWithValue("@Preco", servico.Preco);

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

        public List<Servico> List()
        {
            List<Servico> lista = new List<Servico>();
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM servico";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Servico
                    {
                        Id = reader.GetInt32("id_ser"),
                        Descricao = reader.GetString("descricao_ser"),
                        Nome = reader.GetString("nome_ser"),
                        Preco = reader.GetDouble("preco_ser")
                    });
                    return lista;
                }
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


        public Servico? GetById(int id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM servico WHERE id_ser = @id";
                query.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Servico
                    {
                        Id = reader.GetInt32("id_ser"),
                        Descricao = reader.GetString("descricao_ser"),
                        Nome = reader.GetString("nome_ser"),
                        Preco = reader.GetDouble("preco_ser")
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


       public void Update(Servico servico)
        {
            try
            {
                _conn.Open();
                var query = _conn.Query();
                query.CommandText = "insert into servico (descricao_ser, nome_ser, preco_ser) VALUES (@Descricao, @Nome, @Preco)";
                query.Parameters.AddWithValue("@Descricao", servico.Descricao);
                query.Parameters.AddWithValue("@Nome", servico.Nome);
                query.Parameters.AddWithValue("@Preco", servico.Preco);
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
                _conn.Open();
                var query = _conn.Query();
                query.CommandText = "DELETE FROM serivo WHERE id_ser = @id";
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
                _conn.Close();
            }
        }



    }
}

