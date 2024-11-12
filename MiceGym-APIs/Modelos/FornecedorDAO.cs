using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MiceGym_APIs.DAO
{
    public class FornecedorDAO
    {
        private readonly ConnectionMysql _conn;

        public FornecedorDAO()
        {
            _conn = new ConnectionMysql();
        }

        public List<Fornecedores> List()
        {
            List<Fornecedores> fornecedores = new List<Fornecedores>();
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM fornecedores";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    fornecedores.Add(new Fornecedores
                    {
                        NomeFantasia = reader.GetString("nome_fantasia"),
                        RazaoSocial = reader.GetString("razao_social"),
                        CNPJ = reader.GetString("cnpj"),
                        Endereco = reader.GetString("endereco"),
                        Cidade = reader.GetString("cidade"),
                        Estado = reader.GetString("estado"),
                        Telefone = reader.GetString("telefone"),
                        Email = reader.GetString("email"),
                        Responsavel = reader.GetString("responsavel")
                    });
                }
                return fornecedores;
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

        public Fornecedores? GetByCNPJ(string cnpj)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM fornecedores WHERE cnpj = @cnpj";
                query.Parameters.AddWithValue("@cnpj", cnpj);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Fornecedores
                    {
                        NomeFantasia = reader.GetString("nome_fantasia"),
                        RazaoSocial = reader.GetString("razao_social"),
                        CNPJ = reader.GetString("cnpj"),
                        Endereco = reader.GetString("endereco"),
                        Cidade = reader.GetString("cidade"),
                        Estado = reader.GetString("estado"),
                        Telefone = reader.GetString("telefone"),
                        Email = reader.GetString("email"),
                        Responsavel = reader.GetString("responsavel")
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

        public void Insert(Fornecedores fornecedor)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "INSERT INTO fornecedores (nome_fantasia, razao_social, cnpj, endereco, cidade, estado, telefone, email, responsavel) " +
                                    "VALUES (@nomeFantasia, @razaoSocial, @cnpj, @endereco, @cidade, @estado, @telefone, @email, @responsavel)";
                query.Parameters.AddWithValue("@nomeFantasia", fornecedor.NomeFantasia);
                query.Parameters.AddWithValue("@razaoSocial", fornecedor.RazaoSocial);
                query.Parameters.AddWithValue("@cnpj", fornecedor.CNPJ);
                query.Parameters.AddWithValue("@endereco", fornecedor.Endereco);
                query.Parameters.AddWithValue("@cidade", fornecedor.Cidade);
                query.Parameters.AddWithValue("@estado", fornecedor.Estado);
                query.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                query.Parameters.AddWithValue("@email", fornecedor.Email);
                query.Parameters.AddWithValue("@responsavel", fornecedor.Responsavel);
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

        public void Update(Fornecedores fornecedor)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "UPDATE fornecedores SET nome_fantasia = @nomeFantasia, razao_social = @razaoSocial, endereco = @endereco, " +
                                    "cidade = @cidade, estado = @estado, telefone = @telefone, email = @email, responsavel = @responsavel " +
                                    "WHERE cnpj = @cnpj";
                query.Parameters.AddWithValue("@nomeFantasia", fornecedor.NomeFantasia);
                query.Parameters.AddWithValue("@razaoSocial", fornecedor.RazaoSocial);
                query.Parameters.AddWithValue("@endereco", fornecedor.Endereco);
                query.Parameters.AddWithValue("@cidade", fornecedor.Cidade);
                query.Parameters.AddWithValue("@estado", fornecedor.Estado);
                query.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                query.Parameters.AddWithValue("@email", fornecedor.Email);
                query.Parameters.AddWithValue("@responsavel", fornecedor.Responsavel);
                query.Parameters.AddWithValue("@cnpj", fornecedor.CNPJ);
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

        public void Delete(string cnpj)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "DELETE FROM fornecedores WHERE cnpj = @cnpj";
                query.Parameters.AddWithValue("@cnpj", cnpj);
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
