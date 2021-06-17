using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows;

namespace BfgFleetRoster
{
    public class SqliteDataAccess
    {
        public static DataTable LoadRoster()
        {
            using (SqliteConnection conn = new SqliteConnection(LoadConnectionString()))
            {
                //string cs = @"Data Source=C:\Users\alucard\source\repos\BfgFleetRoster\BfgFleetRoster\BfgFleetRoster.db;";
                
                conn.Open();
                string selectSQL = "select * from tbl_ships";
                SqliteCommand selectCommand = new SqliteCommand(selectSQL, conn);
                SqliteDataReader results = selectCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(results);
                conn.Close();
                return dt;
            }
        }

        public static DataTable LoadWeapons(int ID)
        {
            using (SqliteConnection conn = new SqliteConnection(LoadConnectionString()))
            {
                conn.Open();
                string selectSQL = $"select weap_ID, weap_display_name, weap_range_speed, weap_firepower_strength, weap_fire_arc from tbl_ships inner join tbl_ships_weapons on tbl_ships.ship_ID = tbl_ships_weapons.ships_weapons_ship_ID inner join tbl_weapons on tbl_ships_weapons.ships_weapons_weap_ID = tbl_weapons.weap_ID where ship_ID = {ID}";
                SqliteCommand selectCommand = new SqliteCommand(selectSQL, conn);
                SqliteDataReader results = selectCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(results);
                conn.Close();
                return dt;
            }
        }

        public static void SaveRoster(List<string> l)
        {
            using (IDbConnection cnn = new SqliteConnection(LoadConnectionString()))
            {
                //cnn.Execute("insert into tbl_roster (ship_name, ship_stats)", l);
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
