using SQLite4Unity3d;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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
        _connection.CreateTable<Monster>();
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

    public void CreateMonster(int id, string monsterType, string name, string movementStatus, float attackRange, float movementSpeed, float attackPower, float hp, float attackSpeed,string faction, string description, string iterationVersion, string reference)
    {
        var monster = new Monster
        {
            ID = id,
            MonsterType = monsterType,
            Name = name,
            MovementStatus = movementStatus,
            AttackRange = attackRange,
            MovementSpeed = movementSpeed,
            AttackPower = attackPower,
            HP = hp,
            Faction = faction,
            Description = description,
            IterationVersion = iterationVersion,
            Reference = reference,
            AttackSpeed = attackSpeed
        };

        _connection.Insert(monster);
    }

    public IEnumerable<Monster> GetAllMonsters()
    {
        return _connection.Table<Monster>();
    }

    public Monster GetMonster(int id)
    {
        return _connection.Table<Monster>().Where(x => x.ID == id).FirstOrDefault();
    }

    public void UpdateMonster(Monster monster)
    {
        _connection.Update(monster);
    }

    public void DeleteMonster(int id)
    {
        _connection.Delete<Monster>(id);
    }

    public void ClearDB()
    {
        // Drop the table if it exists
        _connection.DropTable<PlayerSave>();

        // Recreate the table
        _connection.CreateTable<PlayerSave>();
    }
    public void ClearMonsterDB()
    {
        // Drop the table if it exists
        _connection.DropTable<Monster>();

        // Recreate the table
        _connection.CreateTable<Monster>();
    }
    public void ImportMonstersFromCSV(string csvFilePath)
    {
        if (File.Exists(csvFilePath))
        {
            string csvContent = File.ReadAllText(csvFilePath);
            List<string[]> parsedData = CSVParser.Parse(csvContent);

            if (parsedData.Count > 0 && parsedData[0].Length < 12) // Assuming your CSV should have 12 columns
            {
                Debug.LogError("CSV format doesn't match the expected format.");
                return;
            }

            // Skip the header row
            for (int i = 1; i < parsedData.Count; i++)
            {
                string[] row = parsedData[i];

                // Check if row has sufficient data
                if (row.Length < 13)
                {
                    Debug.LogError($"Incomplete data on line {i + 1}. Skipping this line.");
                    continue;
                }

                Monster monster = new Monster();

                if (int.TryParse(row[0], out int id))
                {
                    monster.ID = id;
                }
                else
                {
                    Debug.LogError($"Cannot parse {row[0]} as integer for ID on line {i + 1}.");
                    continue;
                }

                monster.MonsterType = row[1] ?? string.Empty;
                monster.Name = row[2] ?? string.Empty;
                monster.MovementStatus = row[3] ?? string.Empty;

                if (float.TryParse(row[4], out float attackRange))
                {
                    monster.AttackRange = attackRange;
                }

                if (float.TryParse(row[5], out float movementSpeed))
                {
                    monster.MovementSpeed = movementSpeed;
                }

                if (float.TryParse(row[6], out float attackPower))
                {
                    monster.AttackPower = attackPower;
                }

                if (float.TryParse(row[7], out float hp))
                {
                    monster.HP = hp;
                }
                if (float.TryParse(row[8], out float attackSpeed))
                {
                    monster.AttackSpeed = attackSpeed;
                }

                monster.Faction = row[9] ?? string.Empty;
                monster.Description = row[10] ?? string.Empty;
                monster.IterationVersion = row[11] ?? string.Empty;
                monster.Reference = row[12] ?? string.Empty;

                _connection.Insert(monster);
            }

            Debug.Log("Monsters imported successfully!");
        }
        else
        {
            Debug.LogError("CSV file does not exist: " + csvFilePath);
        }
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

public class Monster
{
    public int ID { get; set; }
    public string MonsterType { get; set; }
    public string Name { get; set; }
    public string MovementStatus { get; set; }
    public float AttackRange { get; set; }
    public float MovementSpeed { get; set; }
    public float AttackPower { get; set; }
    public float HP { get; set; }
    public float AttackSpeed { get; set; }
    public string Faction { get; set; }
    public string Description { get; set; }
    public string IterationVersion { get; set; }
    public string Reference { get; set; }

    public override string ToString()
    {
        return string.Format("[Monster: ID={0}, MonsterType={1}, Name={2}, MovementStatus={3}, AttackRange={4}, MovementSpeed={5}, AttackPower={6}, HP={7},AttackSpeed={8}, Faction={9}, Description={10}, IterationVersion={11}, Reference={12}]",
            ID, MonsterType, Name, MovementStatus, AttackRange, MovementSpeed, AttackPower, HP, AttackSpeed, Faction, Description, IterationVersion, Reference);
    }

}

