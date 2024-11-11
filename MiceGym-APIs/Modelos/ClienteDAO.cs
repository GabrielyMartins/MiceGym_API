using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MiceGym_APIs.DAO
{
    public class ClienteDAO
    {
        private readonly ConnectionMysql _conn;

        public ClienteDAO()
        {
            _conn = new ConnectionMysql();
        }

        public string Insert(Cliente cliente)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "INSERT INTO cliente (nome, cpf, rg, data_nascimento, sexo, telefone, endereco, cidade, estado, email) " +
                                    "VALUES (@nome, @cpf, @rg, @data_nascimento, @sexo, @telefone, @endereco, @cidade, @estado, @email)";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@cpf", cliente.CPF);
                query.Parameters.AddWithValue("@rg", cliente.RG);
                query.Parameters.AddWithValue("@data_nascimento", cliente.DataNascimento);
                query.Parameters.AddWithValue("@sexo", cliente.Sexo);
                query.Parameters.AddWithValue("@telefone", cliente.Telefone);
                query.Parameters.AddWithValue("@endereco", cliente.Endereco);
                query.Parameters.AddWithValue("@cidade", cliente.Cidade);
                query.Parameters.AddWithValue("@estado", cliente.Estado);
                query.Parameters.AddWithValue("@email", cliente.Email);

                query.ExecuteNonQuery();
                return cliente.CPF;
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

        public List<Cliente> List()
        {
            var clientes = new List<Cliente>();

            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM cliente";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        Nome = reader.GetString("nome"),
                        CPF = reader.GetString("cpf"),
                        RG = reader.GetString("rg"),
                        DataNascimento = reader.GetDateTime("data_nascimento"),
                        Sexo = reader.GetString("sexo"),
                        Telefone = reader.GetString("telefone"),
                        Endereco = reader.GetString("endereco"),
                        Cidade = reader.GetString("cidade"),
                        Estado = reader.GetString("estado"),
                        Email = reader.GetString("email")
                    });
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

            return clientes;
        }

        public Cliente? GetByCPF(string cpf)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM cliente WHERE cpf_cli = @cpf";
                query.Parameters.AddWithValue("@cpf", cpf);

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Cliente
                    {
                        Nome = reader.GetString("nome"),
                        CPF = reader.GetString("cpf"),
                        RG = reader.GetString("rg"),
                        DataNascimento = reader.GetDateTime("data_nascimento"),
                        Sexo = reader.GetString("sexo"),
                        Telefone = reader.GetString("telefone"),
                        Endereco = reader.GetString("endereco"),
                        Cidade = reader.GetString("cidade"),
                        Estado = reader.GetString("estado"),
                        Email = reader.GetString("email")
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

        public void Update(Cliente cliente)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "UPDATE cliente SET nome = @nome, rg = @rg, data_nascimento = @data_nascimento, sexo = @sexo, " +
                                    "telefone = @telefone, endereco = @endereco, cidade = @cidade, estado = @estado, email = @email " +
                                    "WHERE cpf = @cpf";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@rg", cliente.RG);
                query.Parameters.AddWithValue("@data_nascimento", cliente.DataNascimento);
                query.Parameters.AddWithValue("@sexo", cliente.Sexo);
                query.Parameters.AddWithValue("@telefone", cliente.Telefone);
                query.Parameters.AddWithValue("@endereco", cliente.Endereco);
                query.Parameters.AddWithValue("@cidade", cliente.Cidade);
                query.Parameters.AddWithValue("@estado", cliente.Estado);
                query.Parameters.AddWithValue("@email", cliente.Email);
                query.Parameters.AddWithValue("@cpf", cliente.CPF);

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

        public void Delete(string cpf)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "DELETE FROM cliente WHERE cpf = @cpf";
                query.Parameters.AddWithValue("@cpf", cpf);

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
