using MiceGym_APIs.Modelos;
using MiceGym_APIs.Database;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MiceGym_APIs.DAO
{
    public class TreinoDAO
    {
        private readonly ConnectionMysql _conn;

        public TreinoDAO()
        {
            _conn = new ConnectionMysql();
        }

        // Método para inserir um treino no banco de dados
        public string Insert(Treino treino)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "INSERT INTO treinos (data, frequencia, exercicios, series_reps, status, tempodesc, observacoes, objetivo, carga) " +
                                    "VALUES (@data, @frequencia, @exercicios, @series_reps, @status, @tempodesc, @observacoes, @objetivo, @carga)";

                query.Parameters.AddWithValue("@data", treino.Data);
                query.Parameters.AddWithValue("@frequencia", treino.Frequencia);
                query.Parameters.AddWithValue("@exercicios", treino.Exercicios);
                query.Parameters.AddWithValue("@series_reps", treino.SeriesReps);
                query.Parameters.AddWithValue("@status", treino.Status);
                query.Parameters.AddWithValue("@tempodesc", treino.Tempodesc);
                query.Parameters.AddWithValue("@observacoes", treino.Observacoes);
                query.Parameters.AddWithValue("@objetivo", treino.Objetivo);
                query.Parameters.AddWithValue("@carga", treino.Carga);

                query.ExecuteNonQuery();
                return treino.Id.ToString();
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

        // Método para listar todos os treinos
        public List<Treino> List()
        {
            var treinos = new List<Treino>();

            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM treinos";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    treinos.Add(new Treino
                    {
                        Id = reader.GetInt32("id"),
                        Data = reader.GetString("data"),
                        Frequencia = reader.GetString("frequencia"),
                        Exercicios = reader.GetString("exercicios"),
                        SeriesReps = reader.GetString("series_reps"),
                        Status = reader.GetString("status"),
                        Tempodesc = reader.GetString("tempodesc"),
                        Observacoes = reader.GetString("observacoes"),
                        Objetivo = reader.GetString("objetivo"),
                        Carga = reader.GetString("carga")
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

            return treinos;
        }

        // Método para buscar um treino pelo ID
        public Treino GetById(int id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "SELECT * FROM treinos WHERE id = @id";
                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Treino
                    {
                        Id = reader.GetInt32("id"),
                        Data = reader.GetString("data"),
                        Frequencia = reader.GetString("frequencia"),
                        Exercicios = reader.GetString("exercicios"),
                        SeriesReps = reader.GetString("series_reps"),
                        Status = reader.GetString("status"),
                        Tempodesc = reader.GetString("tempodesc"),
                        Observacoes = reader.GetString("observacoes"),
                        Objetivo = reader.GetString("objetivo"),
                        Carga = reader.GetString("carga")
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

        // Método para atualizar um treino existente
        public void Update(Treino treino)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "UPDATE treinos SET data = @data, frequencia = @frequencia, exercicios = @exercicios, " +
                                    "series_reps = @series_reps, status = @status, tempodesc = @tempodesc, observacoes = @observacoes, " +
                                    "objetivo = @objetivo, carga = @carga WHERE id = @id";

                query.Parameters.AddWithValue("@data", treino.Data);
                query.Parameters.AddWithValue("@frequencia", treino.Frequencia);
                query.Parameters.AddWithValue("@exercicios", treino.Exercicios);
                query.Parameters.AddWithValue("@series_reps", treino.SeriesReps);
                query.Parameters.AddWithValue("@status", treino.Status);
                query.Parameters.AddWithValue("@tempodesc", treino.Tempodesc);
                query.Parameters.AddWithValue("@observacoes", treino.Observacoes);
                query.Parameters.AddWithValue("@objetivo", treino.Objetivo);
                query.Parameters.AddWithValue("@carga", treino.Carga);
                query.Parameters.AddWithValue("@id", treino.Id);

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

        // Método para deletar um treino
        public void Delete(int id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "DELETE FROM treinos WHERE id = @id";
                query.Parameters.AddWithValue("@id", id);

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
