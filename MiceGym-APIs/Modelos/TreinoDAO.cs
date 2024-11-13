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

       
        public string Insert(Treino treino)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "insert into treino (data_tre, frequencia_tre, exercicios_tre, seriesreps_tre, status_tre, tempodesc_tre, observacoes_tre, objetivo_tre, carga_tre) " +
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

        public List<Treino> List()
        {
            var treinos = new List<Treino>();

            try
            {
                var query = _conn.Query();
                query.CommandText = "select * from treino";
                MySqlDataReader reader = query.ExecuteReader();

                while (reader.Read())
                {
                    treinos.Add(new Treino
                    {
                        Id = reader.GetInt32("id_tre"),
                        Data = reader.GetString("data_tre"),
                        Frequencia = reader.GetString("frequencia_tre"),
                        Exercicios = reader.GetString("exercicios_tre"),
                        SeriesReps = reader.GetString("series_reps_tre"),
                        Status = reader.GetString("status_tre"),
                        Tempodesc = reader.GetString("tempodesc_tre"),
                        Observacoes = reader.GetString("observacoes_tre"),
                        Objetivo = reader.GetString("objetivo_tre"),
                        Carga = reader.GetString("carga_tre")
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

        
        public Treino GetById(int id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "select * from treino where id_tre = @id";
                query.Parameters.AddWithValue("@id", id);

                MySqlDataReader reader = query.ExecuteReader();

                if (reader.Read())
                {
                    return new Treino
                    {
                        Id = reader.GetInt32("id_tre"),
                        Data = reader.GetString("data_tre"),
                        Frequencia = reader.GetString("frequencia_tre"),
                        Exercicios = reader.GetString("exercicios_tre"),
                        SeriesReps = reader.GetString("seriesreps_tre"),
                        Status = reader.GetString("status_tre"),
                        Tempodesc = reader.GetString("tempodesc_tre"),
                        Observacoes = reader.GetString("observacoes_tre"),
                        Objetivo = reader.GetString("objetivo_tre"),
                        Carga = reader.GetString("carga_tre")
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

       
        public void Update(Treino treino)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "update treino set data_tre = @data, frequencia_tre = @frequencia, exercicios_tre = @exercicios, " +
                                    "seriesreps_tre = @series_reps, status_tre = @status, tempodesc_tre = @tempodesc, observacoes_tre = @observacoes, " +
                                    "objetivo_tre = @objetivo, carga_tre = @carga where id_tre = @id";

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

        
        public void Delete(int id)
        {
            try
            {
                var query = _conn.Query();
                query.CommandText = "delete from treino WHERE id_tre = @id";
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
