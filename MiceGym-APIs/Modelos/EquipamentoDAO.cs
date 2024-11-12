using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace MiceGym_APIs.DAO
{
    public class EquipamentoDAO
    {
        private readonly ConnectionMysql _conn;

        public EquipamentoDAO()
        {
            _conn = new ConnectionMysql();
        }

        public List<Equipamento> List()
        {
            List<Equipamento> equipamentos = new List<Equipamento>();
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM equipamentos";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    equipamentos.Add(new Equipamento
                    {
                        Nome = reader.GetString("nome"),
                        Descricao = reader.GetString("descricao"),
                        Codigo = reader.GetString("codigo"),
                        Quantidade = reader.GetInt32("quantidade"),
                        Valor = reader.GetDouble("valor"),
                        Fornecedor = reader.GetString("fornecedor")
                    });
                }
                return equipamentos;
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

        public Equipamento? GetByCodigo(string codigo)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM equipamentos WHERE codigo = @codigo";
                query.Parameters.AddWithValue("@codigo", codigo);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Equipamento
                    {
                        Nome = reader.GetString("nome"),
                        Descricao = reader.GetString("descricao"),
                        Codigo = reader.GetString("codigo"),
                        Quantidade = reader.GetInt32("quantidade"),
                        Valor = reader.GetDouble("valor"),
                        Fornecedor = reader.GetString("fornecedor")
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

        public void Insert(Equipamento equipamento)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "INSERT INTO equipamentos (nome, descricao, codigo, quantidade, valor, fornecedor) " +
                                    "VALUES (@nome, @descricao, @codigo, @quantidade, @valor, @fornecedor)";
                query.Parameters.AddWithValue("@nome", equipamento.Nome);
                query.Parameters.AddWithValue("@descricao", equipamento.Descricao);
                query.Parameters.AddWithValue("@codigo", equipamento.Codigo);
                query.Parameters.AddWithValue("@quantidade", equipamento.Quantidade);
                query.Parameters.AddWithValue("@valor", equipamento.Valor);
                query.Parameters.AddWithValue("@fornecedor", equipamento.Fornecedor);
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

        public void Update(Equipamento equipamento)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "UPDATE equipamentos SET nome = @nome, descricao = @descricao, quantidade = @quantidade, " +
                                    "valor = @valor, fornecedor = @fornecedor WHERE codigo = @codigo";
                query.Parameters.AddWithValue("@nome", equipamento.Nome);
                query.Parameters.AddWithValue("@descricao", equipamento.Descricao);
                query.Parameters.AddWithValue("@quantidade", equipamento.Quantidade);
                query.Parameters.AddWithValue("@valor", equipamento.Valor);
                query.Parameters.AddWithValue("@fornecedor", equipamento.Fornecedor);
                query.Parameters.AddWithValue("@codigo", equipamento.Codigo);
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

        public void Delete(string codigo)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "DELETE FROM equipamentos WHERE codigo = @codigo";
                query.Parameters.AddWithValue("@codigo", codigo);
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
