//     James Felts 2015

using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using FinalProjMediaPlayer.Interfaces;

namespace FinalProjMediaPlayer
{
    public partial class DatabaseHandler
    {
        public DatabaseHandler(IList<IMediaEntry> mediaEntries)
        {
            _mediaEntries = mediaEntries;
            _dbConnection = new SQLiteConnection("Data source = MediaData.sqlite");
            _dbConnection.Open();
            insertFromListItoDatabase();
        }

        private void insertFromListItoDatabase()
        {
            SQLiteCommand insertCom = new SQLiteCommand(_dbConnection);
            createTableIfNotFound();
            foreach (IMediaEntry entry in _mediaEntries)
            {
                if (entry is MusicEntry)
                {
                    insertCom.CommandText = "insert or ignore into Music (Title, Artist, Genre, Length, FilePath) " +
                                            "values (?,?,?,?,?)";
                    insertCom.Parameters.Add("@Title", DbType.String).Value = entry.Title;
                    insertCom.Parameters.Add("@Artist", DbType.String).Value = entry.Creator;
                    insertCom.Parameters.Add("@Genre", DbType.String).Value = entry.Genre;
                    insertCom.Parameters.Add("@Length", DbType.Int64).Value = entry.Length;
                    insertCom.Parameters.Add("@FilePath", DbType.String).Value = entry.FilePath;
                }
                else if(entry is VideoEntry)
                {
                    insertCom.CommandText = "insert or ignore into Video (Title, Publisher, Genre, Length, FilePath) " +
                                            "values (?,?,?,?,?)";
                    insertCom.Parameters.Add("@Title", DbType.String).Value = entry.Title;
                    insertCom.Parameters.Add("@Publisher", DbType.String).Value = entry.Creator;
                    insertCom.Parameters.Add("@Genre", DbType.String).Value = entry.Genre;
                    insertCom.Parameters.Add("@Length", DbType.Int64).Value = entry.Length;
                    insertCom.Parameters.Add("@FilePath", DbType.String).Value = entry.FilePath;
                }
                else
                {
                    throw new DataException("not a Music or Video entry");
                }
                insertCom.ExecuteNonQuery();
            }
        }

        private void createTableIfNotFound()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            SQLiteCommand checkCom = new SQLiteCommand(_dbConnection);
            checkCom.CommandText =
                "create table if not exists " +
                "Music (Title varchar(300), Artist varchar(300), Genre varchar(20), Length int, FilePath varchar(300) PRIMARY KEY)";
            checkCom.ExecuteNonQuery();
            checkCom.CommandText =
                "create table if not exists " +
                "Video (Title varchar(300), Publisher varchar(300), Genre varchar(20), Length int, FilePath varchar(300) PRIMARY KEY)";
            checkCom.ExecuteNonQuery();
        }

        public void shutdownDatabaseConnection()
        {
            _dbConnection.Close();
        }

        private readonly IList<IMediaEntry> _mediaEntries;
        private readonly SQLiteConnection _dbConnection;
    }
}