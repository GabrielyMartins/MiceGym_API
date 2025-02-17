using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MiceGym_APIs.Modelos
{
    public class FuncionarioDAO
    {
        private readonly ConnectionMysql _conn;

        public FuncionarioDAO()
        {
            _conn = new ConnectionMysql();
        }

        public int Insert(Funcionario funcionario)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "insert into funcionario (id_fun, nome_fun, cpf_fun, ctps_fun, rg_fun, funcao_fun, setor_fun, sala_fun, telefone_fun) " +
                                    "VALUES (@id, @nome, @cpf, @ctps, @rg, @funcao, @setor, @sala, @telefone)";
                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@cpf", funcionario.CPF);
                query.Parameters.AddWithValue("@ctps", funcionario.CTPS);
                query.Parameters.AddWithValue("@rg", funcionario.RG);
                query.Parameters.AddWithValue("@funcao", funcionario.Funcao);
                query.Parameters.AddWithValue("@setor", funcionario.Setor);
                query.Parameters.AddWithValue("@sala", funcionario.Sala);
                query.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                query.Parameters.AddWithValue("@id", funcionario.Id);

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

        public List<Funcionario> List()
        {
            List<Funcionario> lista = new List<Funcionario>();
            try
            {
                var query = _conn.Query();
                query.CommandText = "select * from funcionario";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Funcionario
                    {
                        Id = reader.GetInt32("id_fun"),
                        Nome = reader.GetString("nome_fun"),
                        CPF = reader.GetString("cpf_fun"),
                        RG = reader.GetString("rg_fun"),
                        CTPS = reader.GetString("ctps_fun"),
                        Funcao = reader.GetString("funcao_fun"),
                        Setor = reader.GetString("setor_fun"),
                        Sala = reader.GetString("sala_fun"),
                        Telefone = reader.GetString("telefone_fun"),
                      
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

        public Funcionario? GetById(int Id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "select * from funcionario where id_fun = @id";
                query.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Funcionario
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome_fun"),
                        CPF = reader.GetString("cpf_fun"),
                        RG = reader.GetString("rg_fun"),
                        CTPS = reader.GetString("ctps_fun"),
                        Funcao = reader.GetString("funcao_fun"),
                        Setor = reader.GetString("setor_fun"),
                        Sala = reader.GetString("sala_fun"),
                        Telefone = reader.GetString("telefone_fun"),
                       
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

        public void Update(Funcionario funcionario)
        {
            try
            {
                _conn.Open();
                var query = _conn.Query();
                query.CommandText = "update funcionario ser nome_fun = @nome, ctps_fun = @ctps, rg_fun = @rg, funcao_fun = @funcao, setor_fun = @setor, sala_fun = @sala, " +
                                    "telefone_fun = @telefone WHERE id_fun = @id";
                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@cpf", funcionario.CPF);
                query.Parameters.AddWithValue("@rg", funcionario.RG);
                query.Parameters.AddWithValue("@ctps", funcionario.CTPS);
                query.Parameters.AddWithValue("@funcao", funcionario.Funcao);
                query.Parameters.AddWithValue("@setor", funcionario.Setor);
                query.Parameters.AddWithValue("@sala", funcionario.Sala);
                query.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                query.Parameters.AddWithValue("@id", funcionario.Id);

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

        public void Delete(int Id)
        {
            try
            {
                _conn.Open();
                var query = _conn.Query();
                query.CommandText = "delete from funcionario where id_fun = @id";
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();

                var result = query.ExecuteNonQuery();
                if (result == 0)
                {
                    throw new Exception("O registro não foi excluido.Verifique e tente novamente");
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
