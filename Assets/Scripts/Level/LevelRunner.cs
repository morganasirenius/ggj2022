using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunner : MonoBehaviour
{
    [SerializeField]
    private float minCommmandDelay, maxCommandDelay;

    [SerializeField]
    private float minSpawnDelay, maxSpawnDelay;

    [SerializeField]
    private Spawner TopSpawner;
    [SerializeField]
    private Spawner BottomSpawner;
    [SerializeField]
    private Spawner LeftSpawner;
    [SerializeField]
    private Spawner RightSpawner;

    private Dictionary<Globals.EntityType, ObjectPooler> entityObjectPoolers;

    private const string CommandDelimiter = ";";
    private const int RequiredSpawnParams = 7;
    private const int RequiredDelayParams = 2;

    // Start is called before the first frame update
    void Start()
    {
        LevelData level = ResourceManager.Instance.LevelDictionary["LevelOne"];
        List<Globals.EntityType> entities = level.levelEntities;
        List<string> commands = level.levelCommands;
        SetupObjectPoolers(entities);
        PlayLevelMusic(level.levelMusic);
        StartCoroutine(RunEndlessLevel());
        // StartCoroutine(RunLevel(commands));
    }

    void PlayLevelMusic(string trackName)
    {
        AudioManager.Instance.PlaySong(trackName);
    }

    // Setup Object Poolers based on level- cache them
    void SetupObjectPoolers(List<Globals.EntityType> entityTypes)
    {
        entityObjectPoolers = new Dictionary<Globals.EntityType, ObjectPooler>();
        foreach (Globals.EntityType type in entityTypes)
        {
            string path = string.Format("Prefabs/ObjectPoolers/{0}ObjectPooler", type.ToString());
            GameObject poolObj = Instantiate(Resources.Load(path, typeof(GameObject))) as GameObject;
            entityObjectPoolers[type] = poolObj.GetComponent<ObjectPooler>();
        }
    }

    IEnumerator RunLevel(List<string> commands)
    {
        foreach (string command in commands)
        {
            string[] parts = command.Split(CommandDelimiter);
            if (parts.Length > 0)
            {
                Globals.Action action = (Globals.Action)System.Enum.Parse(typeof(Globals.Action), parts[0]);
                switch (action)
                {
                    case Globals.Action.Spawn:
                        Debug.Log("Executing spawn command!");
                        yield return ExecuteSpawnCommand(parts);
                        break;
                    case Globals.Action.Delay:
                        Debug.Log("Executing delay command!");
                        yield return ExecuteDelayCommand(parts);
                        break;
                    default:
                        Debug.LogError(string.Format("Invalid action passed through", action.ToString()));
                        yield return null;
                        break;
                }
            }
        }
    }

    IEnumerator RunEndlessLevel()
    {
        while (!PlayerController.Instance.isDead)
        {
            // Delay first
            float randomDelay = Random.Range(minCommmandDelay, maxCommandDelay);
            Debug.Log(string.Format("Delay for {0} seconds!", randomDelay));
            yield return StartCoroutine(Delay(randomDelay));
            // Spawn next
            Globals.EntityType entityType = (Globals.EntityType)Random.Range(0, System.Enum.GetValues(typeof(Globals.EntityType)).Length);
            Globals.EntityProperties properties = Globals.entityMap[entityType];
            Globals.Direction spawnerDirection = properties.spawnDirection;
            Globals.SpawnPoints spawnPoint = properties.possibleSpawnPoints[Random.Range(0, properties.possibleSpawnPoints.Count)];
            Globals.SpawnStyle spawnStyle = properties.possibleSpawnStyles[Random.Range(0, properties.possibleSpawnStyles.Count)];
            int count = Random.Range(1, properties.maxSpawnCount + 1);
            float spawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
            Debug.Log(string.Format("Spawn delay per entity: {0}!", spawnDelay));
            Spawner spawner = SetSpawner(spawnerDirection);
            ObjectPooler pooler = entityObjectPoolers[entityType];
            Debug.Log(string.Format("Spawning {0} entity!", entityType));
            yield return StartCoroutine(SpawnEntity(pooler, spawner, spawnPoint, spawnStyle, count, spawnDelay));
        }
        yield return null;
    }

    IEnumerator ExecuteSpawnCommand(string[] parameters)
    {
        if (parameters.Length != RequiredSpawnParams)
        {
            Debug.LogError(string.Format("Not enough parameters to run command: {0}", parameters.Length));
            yield return null;
        }

        Globals.EntityType entityType = (Globals.EntityType)System.Enum.Parse(typeof(Globals.EntityType), parameters[1]);
        Globals.Direction spawnerDirection = (Globals.Direction)System.Enum.Parse(typeof(Globals.Direction), parameters[2]);
        Globals.SpawnPoints spawnLocation = (Globals.SpawnPoints)System.Enum.Parse(typeof(Globals.SpawnPoints), parameters[3]);
        Globals.SpawnStyle spawnStyle = (Globals.SpawnStyle)System.Enum.Parse(typeof(Globals.SpawnStyle), parameters[4]);
        int count = int.Parse(parameters[5]);
        float delay = float.Parse(parameters[6]);

        Spawner spawner = SetSpawner(spawnerDirection);
        ObjectPooler pooler = entityObjectPoolers[entityType];

        yield return StartCoroutine(SpawnEntity(pooler, spawner, spawnLocation, spawnStyle, count, delay));
    }

    IEnumerator ExecuteDelayCommand(string[] parameters)
    {
        if (parameters.Length != RequiredDelayParams)
        {
            Debug.LogError(string.Format("Not enough parameters to run command: {0}", parameters.Length));
            yield return null;
        }

        float delay = float.Parse(parameters[1]);

        yield return StartCoroutine(Delay(delay));
    }

    Spawner SetSpawner(Globals.Direction direction)
    {
        switch (direction)
        {
            case Globals.Direction.Up:
                return TopSpawner;
            case Globals.Direction.Down:
                return BottomSpawner;
            case Globals.Direction.Left:
                return LeftSpawner;
            case Globals.Direction.Right:
                return RightSpawner;
            default:
                Debug.LogError(string.Format("Invalid direction returned: {0}", direction.ToString()));
                return null;
        }
    }

    IEnumerator SpawnEntity(ObjectPooler entityPooler, Spawner spawner, Globals.SpawnPoints spawnLocation, Globals.SpawnStyle spawnStyle, int entitiesToSpawn, float delay)
    {
        Vector3 spawnPoint;
        switch (spawnLocation)
        {
            case Globals.SpawnPoints.Start:
                spawnPoint = spawner.StartPoint.position;
                break;
            case Globals.SpawnPoints.Middle:
                spawnPoint = spawner.GetMiddleSpawnPoint();
                break;
            case Globals.SpawnPoints.End:
                spawnPoint = spawner.EndPoint.position;
                break;
            case Globals.SpawnPoints.Random:
                spawnPoint = spawner.GetRandomSpawnPoint();
                break;
            default:
                Debug.LogError(string.Format("Invalid spawn location returned: {0}", spawnLocation.ToString()));
                spawnPoint = Vector3.one;
                break;
        }

        int spawnedEntities = 0;
        while (spawnedEntities < entitiesToSpawn)
        {
            yield return new WaitForSeconds(delay);
            // For random spawn styles, generate new spawnpoint for each entity
            if (spawnStyle == Globals.SpawnStyle.Random)
            {
                spawnPoint = spawner.GetRandomSpawnPoint();
            }
            spawner.SpawnAt(entityPooler.GetPooledObject(), spawnPoint);
            spawnedEntities++;
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
