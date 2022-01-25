using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRunner : MonoBehaviour
{


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
    private const int RequiredSpawnParams = 6;
    private const int RequiredDelayParams = 2;

    // Start is called before the first frame update
    void Start()
    {
        // Setup Object Poolers based on level- cache them
        // TODO: Make objectpoolers into prefabs and load them this way
        LevelData testLevel = ResourceManager.Instance.LevelDictionary["LevelOne"];
        List<Globals.EntityType> entities = testLevel.levelEntities;
        List<string> commands = testLevel.levelCommands;
        SetupObjectPoolers(entities);
        StartCoroutine(RunLevel(commands));
    }

    void SetupObjectPoolers(List<Globals.EntityType> entityTypes)
    {
        entityObjectPoolers = new Dictionary<Globals.EntityType, ObjectPooler>();
        foreach (Globals.EntityType type in entityTypes)
        {
            string path = string.Format("Prefabs/ObjectPoolers/{0}ObjectPooler", type.ToString());
            Debug.Log(path);
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
        int count = int.Parse(parameters[4]);
        float delay = float.Parse(parameters[5]);

        Spawner spawner = SetSpawner(spawnerDirection);
        ObjectPooler pooler = entityObjectPoolers[entityType];

        yield return StartCoroutine(SpawnEntity(pooler, spawner, spawnLocation, count, delay));
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

    IEnumerator SpawnEntity(ObjectPooler entityPooler, Spawner spawner, Globals.SpawnPoints spawnLocation, int entitiesToSpawn, float delay)
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
            spawner.SpawnAt(entityPooler.GetPooledObject(), spawnPoint);
            spawnedEntities++;
        }
    }

    IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
