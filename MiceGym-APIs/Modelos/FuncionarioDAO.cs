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

        public string Insert(Funcionario funcionario)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "INSERT INTO funcionarios (nome, cpf, ctps, rg, funcao, setor, sala, telefone, uf, cidade, bairro, numero, cep) " +
                                    "VALUES (@nome, @cpf, @ctps, @rg, @funcao, @setor, @sala, @telefone, @uf, @cidade, @bairro, @numero, @cep)";
                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@cpf", funcionario.CPF);
                query.Parameters.AddWithValue("@ctps", funcionario.CTPS);
                query.Parameters.AddWithValue("@rg", funcionario.RG);
                query.Parameters.AddWithValue("@funcao", funcionario.Funcao);
                query.Parameters.AddWithValue("@setor", funcionario.Setor);
                query.Parameters.AddWithValue("@sala", funcionario.Sala);
                query.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                query.Parameters.AddWithValue("@uf", funcionario.Endereco.UF);
                query.Parameters.AddWithValue("@cidade", funcionario.Endereco.Cidade);
                query.Parameters.AddWithValue("@bairro", funcionario.Endereco.Bairro);
                query.Parameters.AddWithValue("@numero", funcionario.Endereco.Numero);
                query.Parameters.AddWithValue("@cep", funcionario.Endereco.CEP);
                query.ExecuteNonQuery();

                return funcionario.CPF;
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
                query.CommandText = "SELECT * FROM funcionarios";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Funcionario
                    {
                        Nome = reader.GetString("nome"),
                        CPF = reader.GetString("cpf"),
                        CTPS = reader.GetString("ctps"),
                        RG = reader.GetString("rg"),
                        Funcao = reader.GetString("funcao"),
                        Setor = reader.GetString("setor"),
                        Sala = reader.GetString("sala"),
                        Telefone = reader.GetString("telefone"),
                        Endereco = new Endereco
                        {
                            UF = reader.GetString("uf"),
                            Cidade = reader.GetString("cidade"),
                            Bairro = reader.GetString("bairro"),
                            Numero = reader.GetString("numero"),
                            CEP = reader.GetString("cep")
                        }
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

        public Funcionario? GetByCPF(string cpf)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM funcionarios WHERE cpf = @cpf";
                query.Parameters.AddWithValue("@cpf", cpf);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Funcionario
                    {
                        Nome = reader.GetString("nome"),
                        CPF = reader.GetString("cpf"),
                        CTPS = reader.GetString("ctps"),
                        RG = reader.GetString("rg"),
                        Funcao = reader.GetString("funcao"),
                        Setor = reader.GetString("setor"),
                        Sala = reader.GetString("sala"),
                        Telefone = reader.GetString("telefone"),
                        Endereco = new Endereco
                        {
                            UF = reader.GetString("uf"),
                            Cidade = reader.GetString("cidade"),
                            Bairro = reader.GetString("bairro"),
                            Numero = reader.GetString("numero"),
                            CEP = reader.GetString("cep")
                        }
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
                var query = _conn.Query();
                query.CommandText = "UPDATE funcionarios SET nome = @nome, ctps = @ctps, rg = @rg, funcao = @funcao, setor = @setor, sala = @sala, " +
                                    "telefone = @telefone, uf = @uf, cidade = @cidade, bairro = @bairro, numero = @numero, cep = @cep WHERE cpf = @cpf";
                query.Parameters.AddWithValue("@nome", funcionario.Nome);
                query.Parameters.AddWithValue("@ctps", funcionario.CTPS);
                query.Parameters.AddWithValue("@rg", funcionario.RG);
                query.Parameters.AddWithValue("@funcao", funcionario.Funcao);
                query.Parameters.AddWithValue("@setor", funcionario.Setor);
                query.Parameters.AddWithValue("@sala", funcionario.Sala);
                query.Parameters.AddWithValue("@telefone", funcionario.Telefone);
                query.Parameters.AddWithValue("@uf", funcionario.Endereco.UF);
                query.Parameters.AddWithValue("@cidade", funcionario.Endereco.Cidade);
                query.Parameters.AddWithValue("@bairro", funcionario.Endereco.Bairro);
                query.Parameters.AddWithValue("@numero", funcionario.Endereco.Numero);
                query.Parameters.AddWithValue("@cep", funcionario.Endereco.CEP);
                query.Parameters.AddWithValue("@cpf", funcionario.CPF);
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
                query.CommandText = "DELETE FROM funcionarios WHERE cpf = @cpf";
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
