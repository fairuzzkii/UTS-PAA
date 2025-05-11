using API_Dealer_Mobil_Acc.Helper;
using API_Dealer_Mobil_Acc.Models;
using Npgsql;
using System;
using System.Collections.Generic;

namespace API_Dealer_Mobil_Acc.Context
{
    public class AksesorisContext
    {
        private readonly string _constr;

        public AksesorisContext(string constr)
        {
            _constr = constr;
        }

        public List<Aksesoris> GetAll()
        {
            var list = new List<Aksesoris>();
            string query = "SELECT * FROM aksesoris";
            var db = new sqlDBHelper(_constr);

            try
            {
                using var cmd = db.GetNpgsqlCommand(query);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Aksesoris
                    {
                        Id = reader.GetInt32(0),
                        Nama = reader.GetString(1),
                        Merek = reader.GetString(2),
                        Kategori = reader.GetString(3),
                        Harga = reader.GetInt64(4),
                        Stok = reader.GetInt32(5)
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAll: {ex.Message}");
            }
            finally
            {
                db.closeConnection();
            }

            return list;
        }

        public Aksesoris GetById(int id)
        {
            Aksesoris aksesoris = null;
            string query = "SELECT * FROM aksesoris WHERE id = @id";
            var db = new sqlDBHelper(_constr);

            try
            {
                using var cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    aksesoris = new Aksesoris
                    {
                        Id = reader.GetInt32(0),
                        Nama = reader.GetString(1),
                        Merek = reader.GetString(2),
                        Kategori = reader.GetString(3),
                        Harga = reader.GetInt64(4),
                        Stok = reader.GetInt32(5)
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetById: {ex.Message}");
            }
            finally
            {
                db.closeConnection();
            }

            return aksesoris;
        }

        public void Add(Aksesoris a)
        {
            string query = "INSERT INTO aksesoris (nama, merek, kategori, harga, stok) VALUES (@nama, @merek, @kategori, @harga, @stok)";
            var db = new sqlDBHelper(_constr);

            try
            {
                using var cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", a.Nama);
                cmd.Parameters.AddWithValue("@merek", a.Merek);
                cmd.Parameters.AddWithValue("@kategori", a.Kategori);
                cmd.Parameters.AddWithValue("@harga", a.Harga);
                cmd.Parameters.AddWithValue("@stok", a.Stok);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Add: {ex.Message}");
            }
            finally
            {
                db.closeConnection();
            }
        }

        public void Update(int id, Aksesoris a)
        {
            string query = "UPDATE aksesoris SET nama=@nama, merek=@merek, kategori=@kategori, harga=@harga, stok=@stok WHERE id=@id";
            var db = new sqlDBHelper(_constr);

            try
            {
                using var cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@nama", a.Nama);
                cmd.Parameters.AddWithValue("@merek", a.Merek);
                cmd.Parameters.AddWithValue("@kategori", a.Kategori);
                cmd.Parameters.AddWithValue("@harga", a.Harga);
                cmd.Parameters.AddWithValue("@stok", a.Stok);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Update: {ex.Message}");
            }
            finally
            {
                db.closeConnection();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM aksesoris WHERE id=@id";
            var db = new sqlDBHelper(_constr);

            try
            {
                using var cmd = db.GetNpgsqlCommand(query);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Delete: {ex.Message}");
            }
            finally
            {
                db.closeConnection();
            }
        }
    }
}
