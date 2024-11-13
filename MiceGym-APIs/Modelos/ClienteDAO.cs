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
                query.CommandText = "insert into cliente (nome_cli, datanascimento_cli, rg_cli, cpf_cli, sexo_cli, email_cli, telefone_cli, uf_cli, cidade_cli, bairro_cli, numero_cli, cep_cli) " +
                                    "VALUES (@nome, @data_nascimento, @rg, @cpf, @sexo, @email, @telefone, @uf, @cidade, @bairro, @numero, @cep)";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@cpf", cliente.CPF);
                query.Parameters.AddWithValue("@rg", cliente.RG);
                query.Parameters.AddWithValue("@data_nascimento", cliente.DataNascimento);
                query.Parameters.AddWithValue("@sexo", cliente.Sexo);
                query.Parameters.AddWithValue("@telefone", cliente.Telefone);
                query.Parameters.AddWithValue("@cidade", cliente.Cidade);
                query.Parameters.AddWithValue("@estado", cliente.UF);
                query.Parameters.AddWithValue("@estado", cliente.Bairro);
                query.Parameters.AddWithValue("@estado", cliente.CEP);
                query.Parameters.AddWithValue("@estado", cliente.Numero);
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
                query.CommandText = "select * from cliente";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    clientes.Add(new Cliente
                    {
                        Nome = reader.GetString("nome_cli"),
                        DataNascimento = reader.GetDateTime("datanascimento_cli"),
                        CPF = reader.GetString("cpf_cli"),
                        RG = reader.GetString("rg_cli"),
                        Sexo = reader.GetString("sexo_cli"),
                        Email = reader.GetString("email_cli"),
                        Telefone = reader.GetString("telefone_cli"),
                        UF = reader.GetString("uf_cli"),
                        Cidade = reader.GetString("cidade_cli"),
                        Bairro = reader.GetString("bairro_cli"),
                        Numero = reader.GetString("numero_cli"),
                        CEP = reader.GetString("cep_cli"),

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
                query.CommandText = "select * from cliente where cpf_cli = @cpf";
                query.Parameters.AddWithValue("@cpf", cpf);

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Cliente
                    {
                        Nome = reader.GetString("nome_cli"),
                        DataNascimento = reader.GetDateTime("datanascimento_cli"),
                        CPF = reader.GetString("cpf_cli"),
                        RG = reader.GetString("rg_cli"),
                        Sexo = reader.GetString("sexo_cli"),
                        Email = reader.GetString("email_cli"),
                        Telefone = reader.GetString("telefone_cli"),
                        UF = reader.GetString("uf_cli"),
                        Cidade = reader.GetString("cidade_cli"),
                        Bairro = reader.GetString("bairro_cli"),
                        Numero = reader.GetString("numero_cli"),
                        CEP = reader.GetString("cep_cli"),
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
                query.CommandText = "UPDATE cliente SET nome_cli = @nome, datanascimento_cli = @data_nascimento, rg_cli = @rg, sexo_cli = @sexo, " +
                                    "email = @email, telefone_cli = @telefone, uf_cli = @uf, cidade_cli = @cidade, bairro_cli = @bairro, numero_cli = @numero, cep_cli = @cep " +
                                    "WHERE cpf = @cpf";

                query.Parameters.AddWithValue("@nome", cliente.Nome);
                query.Parameters.AddWithValue("@data_nascimento", cliente.DataNascimento);
                query.Parameters.AddWithValue("@rg", cliente.RG);
                query.Parameters.AddWithValue("@sexo", cliente.Sexo);
                query.Parameters.AddWithValue("@email", cliente.Email);
                query.Parameters.AddWithValue("@telefone", cliente.Telefone);
                query.Parameters.AddWithValue("@uf", cliente.UF);
                query.Parameters.AddWithValue("@cidade", cliente.Cidade);
                query.Parameters.AddWithValue("@bairro", cliente.Bairro);
                query.Parameters.AddWithValue("@numero", cliente.Numero);
                query.Parameters.AddWithValue("@cep", cliente.CEP);
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
                query.CommandText = "delete from cliente where cpf_cli = @cpf";
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
