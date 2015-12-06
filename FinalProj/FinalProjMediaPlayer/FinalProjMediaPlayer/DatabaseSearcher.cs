//     James Felts 2015
//put searching behaviour for the database in this file
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace FinalProjMediaPlayer
{
    public partial class DatabaseHandler
    {

        public IEnumerable<string> searchByTitle(string searchArtist, string searchGenre)
        {
            SQLiteCommand searchCom = new SQLiteCommand(_dbConnection);
            if (searchArtist != null && searchGenre == null)//search Artist
            {
                return searchByArtist(searchArtist);
            }
            else if (searchArtist == null && searchGenre != null)//search Genre
            {
                return searchByGenre(searchGenre);
            }
            else if (searchArtist != null)//search Both
            {
                searchCom.CommandText = "SELECT Title FROM Music, Video WHERE Artist OR Publisher LIKE '%@searchArtist%' AND Genre LIKE '%@searchGenre%';";

                searchCom.Parameters.Add(new SQLiteParameter("@searchArtist", DbType.String).Value = searchArtist);
                searchCom.Parameters.Add(new SQLiteParameter("@searchGenre", DbType.String).Value = searchGenre);
               
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
            searchCom.CommandText = "SELECT Title FROM Music, Video WHERE Artist OR Publisher LIKE @searchArtist;";
            searchCom.CommandType = CommandType.Text;
            searchCom.Parameters.AddWithValue("searchArtist", artist);

            return getList(searchCom);
        }

        public IEnumerable<string> searchByGenre(string genre)
        {
            SQLiteCommand searchCom = new SQLiteCommand(_dbConnection);
            if (genre == null)
            {
                return getList(searchCom);
            }
            searchCom.CommandText = "SELECT Title FROM Music, Video WHERE Genre LIKE '%@searchGenre%';";
            searchCom.Parameters.Add(new SQLiteParameter("@searchGenre", DbType.String).Value = genre);

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
            for (int x = 0; reader.NextResult(); x++)
            {
                listFound.Add(reader.GetString(x));
            }
            return listFound;
        }
    }
}