using SQLite4Unity3d;
using System.Collections.Generic;
using UnityEngine;
#if !UNITY_EDITOR
using System.IO;
#endif

public class DataService
{
    private SQLiteConnection _connection;

    public DataService(string databaseName)
    {
        string dbPath;

#if UNITY_EDITOR
        dbPath = string.Format(@"Assets/StreamingAssets/{0}", databaseName);
#else
        // Check if file exists in Application.persistentDataPath
        string filepath = string.Format("{0}/{1}", Application.persistentDataPath, databaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            
            // If it doesn't ->
            // Open StreamingAssets directory and load the db ->
#if UNITY_ANDROID
            WWW loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + databaseName);
            while (!loadDb.isDone) { }
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
            string loadDb = Application.dataPath + "/Raw/" + DatabaseName;
            File.Copy(loadDb, filepath);
#else
            string loadDb = Application.dataPath + "/StreamingAssets/" + databaseName;
            File.Copy(loadDb, filepath);
#endif
            Debug.Log("Database written");
        }

        dbPath = filepath;
#endif
        _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);

        // Create PlayerSave table
        _connection.CreateTable<PlayerSave>();
    }

    public void CreatePlayerSave(string playerName, int level, int score)
    {
        var playerSave = new PlayerSave
        {
            PlayerName = playerName,
            Level = level,
            Score = score
        };

        _connection.Insert(playerSave);
    }

    public IEnumerable<PlayerSave> GetAllPlayerSaves()
    {
        return _connection.Table<PlayerSave>();
    }

    public PlayerSave GetPlayerSave(int id)
    {
        return _connection.Table<PlayerSave>().Where(x => x.ID == id).FirstOrDefault();
    }

    public void UpdatePlayerSave(PlayerSave playerSave)
    {
        _connection.Update(playerSave);
    }

    public void DeletePlayerSave(int id)
    {
        _connection.Delete<PlayerSave>(id);
    }
    public void ClearDB()
    {
        // Drop the table if it exists
        _connection.DropTable<PlayerSave>();

        // Recreate the table
        _connection.CreateTable<PlayerSave>();
    }

}

public class PlayerSave
{
    [PrimaryKey, AutoIncrement]
    public int ID { get; set; }
    public string PlayerName { get; set; }
    public int Level { get; set; }
    public int Score { get; set; }

    public override string ToString()
    {
        return string.Format("[PlayerSave: ID={0}, PlayerName={1}, Level={2}, Score={3}]", ID, PlayerName, Level, Score);
    }
}
