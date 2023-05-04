using Microsoft.VisualBasic;
using MilestoneWebApplication.Models;
using MySqlConnector;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RegisterAndLoginApp.Services
{
    public class SecurityDAO
    {

       // MySQL connection string using MAMP: (Caleb)
       String connectionString = "datasource=localhost;port=8889;username=root;password=root;database=milestoneWebApp";
        // MySql connection string using MAMP: (Logan)
        //String connectionString = "datasource=172.24.198.180;port=8889;username=root;password=root;database=minesweeper_login";


        public bool UpdateUser(User user)
        {

            for (int i = 0; i < user.savedGames.Count(); i++)
            {
                user.savedGames[i].Id = i;
            }
            // serialize savedGames
            string savedGames = JsonConvert.SerializeObject(user.savedGames);

            string sql = "UPDATE `USERS` SET `saved_games` = @savedGames WHERE `users`.`id` = @id";

            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                bool success = false;
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.Parameters.AddWithValue("@savedGames", savedGames);
                command.Parameters.AddWithValue("@id", user.Id);

                try
                {
                    connection.Open();
                    int rows = command.ExecuteNonQuery();
                    connection.Close();
                    if(rows == 1)
                    {
                        Console.WriteLine("Column edited");
                        success = true;
                    }
                    else
                    {
                        Console.WriteLine("Column not edited");

                        success = false;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return success;
            }

        }

        public bool AddNewUser(User user)
        {
            bool success = false;
            string sqlStatement = "INSERT INTO `users` (`Id`, `firstName`, `lastName`, `sex`, `age`, `state`, `email`, `username`, `password`) VALUES (NULL, @firstName, @lastName, @sex, @age, @state, @email, @username, @password)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@firstName", user.firstName);
                command.Parameters.AddWithValue("@lastName", user.lastName);
                command.Parameters.AddWithValue("@sex", user.sex);
                command.Parameters.AddWithValue("@age", user.age);
                command.Parameters.AddWithValue("@state", user.state);
                command.Parameters.AddWithValue("@email", user.email);
                command.Parameters.AddWithValue("@username", user.username);
                command.Parameters.AddWithValue("@password", user.password);


                try
                {
                    connection.Open();
                    int rows = command.ExecuteNonQuery();
                    connection.Close();

                    if(rows == 1)
                    {
                        success = true;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return success;

            }

        }

        public bool FindUserByNameAndPassword(User user)
        {
            bool success = false;

            string sqlStatement = "SELECT * FROM USERS WHERE USERNAME = @username and PASSWORD = @password";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@username", user.username);
                command.Parameters.AddWithValue("@password", user.password);

                try
                {
                    connection.Open();
                    MySqlDataReader reader= command.ExecuteReader();
                    if(reader.HasRows)
                    {
                        success = true;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

                return success;
        }

        public User FindUserByName(string username)
        {
            User user = new User();
            string sqlStatement = "SELECT * FROM USERS WHERE USERNAME = @username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sqlStatement, connection);

                command.Parameters.AddWithValue("@username", username);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        user.Id = reader.GetInt32(0);
                        user.username = reader.GetString(7);
                        user.password = reader.GetString(8);
                        // Deserialize json
                        if(reader.GetString(9) != null)
                        {
                            user.savedGames = JsonConvert.DeserializeObject<List<Board>>(reader.GetString(9));
                        }

                        
                        

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                };
            }

            return user;
        }



        public Board LoadGame(string user, int index)
        {

            User thisUser = FindUserByName(user);
            Board returnThis = thisUser.savedGames[index];
           // returnThis.Grid = thisUser.savedGames[index].Grid;

            
            thisUser.savedGames.RemoveAt(index);
            
            
            foreach(Board b  in thisUser.savedGames)
            {
                Console.WriteLine("Board with ID " + b.Id);
            }
            UpdateUser(thisUser);
            return returnThis;
        }
        
    }
}
