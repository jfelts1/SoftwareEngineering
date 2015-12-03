//     James Felts 2015
//put searching behaviour for the database in this file
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace FinalProjMediaPlayer
{
    public partial class DatabaseHandler
    {

        public IEnumerable<string> SearchByTitle(string searchArtist, string searchGenre)
        {
            SQLiteCommand searchCom = new SQLiteCommand(_dbConnection);
            if (searchArtist != null && searchGenre == null)//search Artist
            {
                searchCom.CommandText = "SELECT Title FROM Music, Video WHERE Artist OR Publisher LIKE '%@searchArtist%';";
                searchCom.Parameters.Add(new SQLiteParameter("@searchArtist", DbType.String).Value = searchArtist);
            }
            else if (searchArtist == null && searchGenre != null)//search Genre
            {
                searchCom.CommandText = "SELECT Title FROM Music, Video WHERE Genre LIKE '%@searchGenre%';";
                searchCom.Parameters.Add(new SQLiteParameter("@searchGenre", DbType.String).Value = searchGenre);
            }
            else //search Both
            {
                searchCom.CommandText = "SELECT Title FROM Music, Video WHERE Artist OR Publisher LIKE '%@searchArtist%' AND Genre LIKE '%@searchGenre%';";
                if (searchArtist != null)
                {
                    searchCom.Parameters.Add(new SQLiteParameter("@searchArtist", DbType.String).Value = searchArtist);
                    searchCom.Parameters.Add(new SQLiteParameter("@searchGenre", DbType.String).Value = searchGenre);
                }  
            }
            SQLiteDataReader reader = searchCom.ExecuteReader();
            List<string> listFound = new List<string>();
            for(int x = 0; reader.NextResult(); x++)
            {
                listFound.Add(reader.GetString(x));
            }
            return listFound;
        }
    }
}