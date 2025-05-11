using API_Dealer_Mobil_Acc.Helper;
using API_Dealer_Mobil_Acc.Models;
using Npgsql;
using System;
using System.Collections.Generic;

namespace API_Dealer_Mobil_Acc.Context
{
    public class MobilContext
    {
        private readonly string _constr;

        public MobilContext(string constr)
        {
            _constr = constr;
        }

        public List<Mobil> GetAll()
        {
            var list = new List<Mobil>();
            string query = "SELECT * FROM mobil";
            var db = new sqlDBHelper(_constr);

            try
            {
                using var cmd = db.GetNpgsqlCommand(query);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Mobil
                    {
                        Id = reader.GetInt32(0),
                        Merek = reader.GetString(1),
                        Model = reader.GetString(2),
                        Tahun = reader.GetInt32(3),
                        Harga = reader.GetInt64(4),
                        Warna = reader.GetString(5)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetAll Mobil: {ex.Message}");
            }
            finally
            {
                db.closeConnection();
            }

            return list;
        }

        public Mobil GetById(int id)
        {
            Mobil mobil = null;
            string query = "SELECT * FROM mobil WHERE id = @id";
            var db = new sqlDBHelper(_constr);

            try
            {
                using var cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    mobil = new Mobil
                    {
                        Id = reader.GetInt32(0),
                        Merek = reader.GetString(1),
                        Model = reader.GetString(2),
                        Tahun = reader.GetInt32(3),
                        Harga = reader.GetInt64(4),
                        Warna = reader.GetString(5)
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] GetById Mobil: {ex.Message}");
            }
            finally
            {
                db.closeConnection();
            }

            return mobil;
        }

        public bool Add(Mobil m)
        {
            try
            {
                string query = "INSERT INTO mobil (merek, model, tahun, harga, warna) VALUES (@merek, @model, @tahun, @harga, @warna)";
                using var cmd = new sqlDBHelper(_constr).GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@merek", m.Merek);
                cmd.Parameters.AddWithValue("@model", m.Model);
                cmd.Parameters.AddWithValue("@tahun", m.Tahun);
                cmd.Parameters.AddWithValue("@harga", m.Harga);
                cmd.Parameters.AddWithValue("@warna", m.Warna);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Add Mobil: {ex.Message}");
                return false;
            }
            finally
            {
                new sqlDBHelper(_constr).closeConnection();
            }
        }

        public bool Update(int id, Mobil m)
        {
            try
            {
                string query = "UPDATE mobil SET merek=@merek, model=@model, tahun=@tahun, harga=@harga, warna=@warna WHERE id=@id";
                using var cmd = new sqlDBHelper(_constr).GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@merek", m.Merek);
                cmd.Parameters.AddWithValue("@model", m.Model);
                cmd.Parameters.AddWithValue("@tahun", m.Tahun);
                cmd.Parameters.AddWithValue("@harga", m.Harga);
                cmd.Parameters.AddWithValue("@warna", m.Warna);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Update Mobil: {ex.Message}");
                return false;
            }
            finally
            {
                new sqlDBHelper(_constr).closeConnection();
            }
        }

        public bool Delete(int id)
        {
            try
            {
                string query = "DELETE FROM mobil WHERE id=@id";
                using var cmd = new sqlDBHelper(_constr).GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Delete Mobil: {ex.Message}");
                return false;
            }
            finally
            {
                new sqlDBHelper(_constr).closeConnection();
            }
        }
    }
}
