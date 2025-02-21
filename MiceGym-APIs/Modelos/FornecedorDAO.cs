﻿using MiceGym_APIs.Modelos;
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

        public List<Fornecedor> List()
        {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            try
            {
                var query = _conn.Query();  

                query.CommandText = "SELECT * FROM fornecedor";
                using (MySqlDataReader reader = query.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fornecedores.Add(new Fornecedor
                        {

                            Id = reader.GetInt32("id_forn"),
                            NomeFantasia = reader.GetString("nomefantasia_forn"),
                            RazaoSocial = reader.GetString("razaosocial_forn"),
                            CNPJ = reader.GetString("cnpj_forn"),
                            Endereco = reader.GetString("endereco_forn"),
                            Cidade = reader.GetString("cidade_forn"),
                            Estado = reader.GetString("estado_forn"),
                            Telefone = reader.GetString("telefone_forn"),
                            Email = reader.GetString("email_forn"),
                            Responsavel = reader.GetString("responsavel_forn")
                        });
                    }
                }
                return fornecedores;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro no banco de dados: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro desconhecido: {ex.Message}");
                throw;
            }
            finally
            {
                _conn.Close();  
            }
        }

        public Fornecedor? GetById(int Id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "select * from fornecedor where id_forn = @id";
                query.Parameters.AddWithValue("id", Id);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Fornecedor
                    {
                        Id = reader.GetInt32("id_forn"),
                        NomeFantasia = reader.GetString("nomefantasia_forn"),
                        RazaoSocial = reader.GetString("razaosocial_forn"),
                        CNPJ = reader.GetString("cnpj_forn"),
                        Endereco = reader.GetString("endereco_forn"),
                        Cidade = reader.GetString("cidade_forn"),
                        Estado = reader.GetString("estado_forn"),
                        Telefone = reader.GetString("telefone_forn"),
                        Email = reader.GetString("email_forn"),
                        Responsavel = reader.GetString("responsavel_forn")
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

        public void Insert(Fornecedor fornecedor)
        {
            try
            {
                

                var query = _conn.Query();  
                query.CommandText = @"INSERT INTO fornecedor 
            (nomefantasia_forn, razaosocial_forn, cnpj_forn, endereco_forn, cidade_forn, estado_forn, telefone_forn, email_forn, responsavel_forn)
            VALUES (@nomeFantasia, @razaoSocial, @cnpj, @endereco, @cidade, @estado, @telefone, @email, @responsavel)";

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
            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro no banco de dados: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro desconhecido: {ex.Message}");
                throw;
            }
            finally
            {
                _conn.Close();  
            }
        }

        public void Update(Fornecedor fornecedor)
        {
            try
            {
                _conn.Open();

                var query = _conn.Query();
                query.CommandText = "update fornecedor set nomefantasia_forn = @nomeFantasia, razaosocial_forn = @razaoSocial, endereco_forn = @endereco, " +
                                    "cidade_forn = @cidade, estado_forn = @estado, telefone_forn = @telefone, email_forn = @email, responsavel_forn = @responsavel " +
                                    "WHERE id_forn = @id"; 
                query.Parameters.AddWithValue("@nomeFantasia", fornecedor.NomeFantasia);
                query.Parameters.AddWithValue("@razaoSocial", fornecedor.RazaoSocial);
                query.Parameters.AddWithValue("@endereco", fornecedor.Endereco);
                query.Parameters.AddWithValue("@cidade", fornecedor.Cidade);
                query.Parameters.AddWithValue("@estado", fornecedor.Estado);
                query.Parameters.AddWithValue("@telefone", fornecedor.Telefone);
                query.Parameters.AddWithValue("@email", fornecedor.Email);
                query.Parameters.AddWithValue("@responsavel", fornecedor.Responsavel);
                query.Parameters.AddWithValue("@id", fornecedor.Id);
                query.ExecuteNonQuery();
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
                query.CommandText = "delete from fornecedor where id_forn = @id";
                query.Parameters.AddWithValue("@id", Id);
                query.ExecuteNonQuery();
            }
            
            finally
            {
                _conn.Close();
            }
        }
    }
}
