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
                query.CommandText = "select * from equipamentos";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    equipamentos.Add(new Equipamento
                    {
                        Nome = reader.GetString("nome_equi"),
                        Descricao = reader.GetString("descricao_equi"),
                        Codigo = reader.GetString("codigo_equi"),
                        Quantidade = reader.GetInt32("quantidade_equi"),
                        Valor = reader.GetDouble("valor_equi"),
                        Fornecedor = reader.GetString("fornecedor_equi)
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
                query.CommandText = "select * from equipamentos where codigo_equi = @codigo";
                query.Parameters.AddWithValue("@codigo", codigo);
                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Equipamento
                    {
                        Nome = reader.GetString("nome_equi"),
                        Descricao = reader.GetString("descricao_equi"),
                        Codigo = reader.GetString("codigo_equi"),
                        Quantidade = reader.GetInt32("quantidade_equi"),
                        Valor = reader.GetDouble("valor_equi"),
                        Fornecedor = reader.GetString("fornecedor_equi")
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
                query.CommandText = "insert into equipamentos (nome_equi, descricao_equi, codigo_equi, quantidade_equi, valor_equi, fornecedor_equi) " +
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
                query.CommandText = "update equipamentos set nome_equi = @nome, descricao_equi = @descricao, quantidade_equi = @quantidade, " +
                                    "valor_equi = @valor, fornecedor_equi = @fornecedor where codigo_equi = @codigo";
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
                query.CommandText = "delete from equipamentos where codigo_equi = @codigo";
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
