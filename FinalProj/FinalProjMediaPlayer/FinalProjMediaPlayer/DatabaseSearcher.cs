﻿//     Team Ctrl-Alt-Delete
//put searching behaviour for the database in this file

using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace FinalProjMediaPlayer
{
    public partial class DatabaseHandler
    {

        public IEnumerable<string> searchByArtistAndGenre(string searchArtist, string searchGenre)
        {
            SQLiteCommand searchCom = new SQLiteCommand(_dbConnection);
            if (!string.IsNullOrEmpty(searchArtist)&& string.IsNullOrEmpty(searchGenre))//search Artist
            {
                return searchByArtist(searchArtist);
            }
            else if (string.IsNullOrEmpty(searchArtist) && !string.IsNullOrEmpty(searchGenre))//search Genre
            {
                return searchByGenre(searchGenre);
            }
            else if (!string.IsNullOrEmpty(searchArtist))//search Both
            {
                searchCom.CommandText =
                    @"SELECT Title FROM Music WHERE Artist LIKE @searchArtist AND Genre Like @searchGenre Union all SELECT Title FROM Video WHERE Publisher LIKE @searchArtist AND Genre Like @searchGenre";
                searchCom.CommandType = CommandType.Text;

                searchCom.Parameters.AddWithValue("@searchArtist", $"%{searchArtist}%");
                searchCom.Parameters.AddWithValue("@searchGenre", $"%{searchGenre}%");
                searchCom.Prepare();

            }
            return getList(searchCom);
        }

        public IEnumerable<string> searchByArtist(string artist)
        {
            SQLiteCommand searchCom = new SQLiteCommand(_dbConnection);
            if (artist == null)
            {
                return getList(searchCom);
            }
            searchCom.CommandText =
                @"SELECT Title FROM Music WHERE Artist LIKE @searchArtist UNION ALL SELECT Title FROM Video WHERE Publisher LIKE @searchArtist";
            searchCom.CommandType = CommandType.Text;
            searchCom.Parameters.AddWithValue("@searchArtist", $"%{artist}%");
            searchCom.Prepare();
            
            return getList(searchCom);
        }

        public IEnumerable<string> searchByGenre(string genre)
        {
            SQLiteCommand searchCom = new SQLiteCommand(_dbConnection);
            if (genre == null)
            {
                return getList(searchCom);
            }
            searchCom.CommandText =
                @"SELECT Title FROM Music WHERE Genre LIKE @searchGenre Union ALL SELECT Title FROM Video WHERE Genre LIKE @searchGenre;";
            searchCom.Parameters.AddWithValue("@searchGenre", $"%{genre}%");
            searchCom.Prepare();

            return getList(searchCom);
        } 

        
        private static IEnumerable<string> getList(SQLiteCommand searchCom)
        {
            if (string.IsNullOrEmpty(searchCom.CommandText))
            {
                throw new NoNullAllowedException("The CommandText must initalized");
            }
            
            SQLiteDataReader reader = searchCom.ExecuteReader();
            
            List<string> listFound = new List<string>();

            while(reader.Read())
            {
                listFound.Add(reader["Title"].ToString());
            }
            return listFound;
        }
    }
}