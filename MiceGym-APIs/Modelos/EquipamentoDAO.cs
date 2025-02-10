using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MiceGym_APIs.DAO
{
    public class EquipamentoDAO
    {
        private readonly ConnectionMysql _conn;

        public EquipamentoDAO()
        {
            _conn = new ConnectionMysql();
        }

        public int Insert(Equipamento equipamento)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "INSERT INTO equipamentos (nome_equi, descricao_equi, codigo_equi, quantidade_equi, valor_equi) " +
                                    "VALUES (@nome, @descricao, @codigo, @quantidade, @valor)";

                query.Parameters.AddWithValue("@nome", equipamento.Nome);
                query.Parameters.AddWithValue("@descricao", equipamento.Descricao);
                query.Parameters.AddWithValue("@codigo", equipamento.Codigo);
                query.Parameters.AddWithValue("@quantidade", equipamento.Quantidade);
                query.Parameters.AddWithValue("@valor", equipamento.Valor);
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

        public List<Equipamento> List()
        {
            List<Equipamento> lista = new List<Equipamento>();
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM equipamentos";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    lista.Add(new Equipamento
                    {
                        Id = reader.GetInt32("id_equi"),
                        Nome = reader.GetString("nome_equi"),
                        Descricao = reader.GetString("descricao_equi"),
                        Codigo = reader.GetString("codigo_equi"),
                        Quantidade = reader.GetInt32("quantidade_equi"),
                        Valor = reader.GetDouble("valor_equi")
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

        public Equipamento GetById(int id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM equipamentos WHERE id_equi = @id";
                query.Parameters.AddWithValue("@id", id);
                using var reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Equipamento
                    {
                        Id = reader.GetInt32("id_equi"),
                        Nome = reader.GetString("nome_equi"),
                        Descricao = reader.GetString("descricao_equi"),
                        Codigo = reader.GetString("codigo_equi"),
                        Quantidade = reader.GetInt32("quantidade_equi"),
                        Valor = reader.GetDouble("valor_equi")
                    };
                }
                return null;
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
                _conn.Open();

                var query = _conn.Query();
                query.CommandText = "UPDATE equipamentos SET nome_equi = @nome, descricao_equi = @descricao, quantidade_equi = @quantidade, valor_equi = @valor WHERE id_equi = @id";
                query.Parameters.AddWithValue("@nome", equipamento.Nome);
                query.Parameters.AddWithValue("@descricao", equipamento.Descricao);
                query.Parameters.AddWithValue("@quantidade", equipamento.Quantidade);
                query.Parameters.AddWithValue("@valor", equipamento.Valor);
                query.Parameters.AddWithValue("@id", equipamento.Id);
                query.ExecuteNonQuery();
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
                query.CommandText = "DELETE FROM equipamentos WHERE id_equi = @id";
                query.Parameters.AddWithValue("@id", id);
                query.ExecuteNonQuery();
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}