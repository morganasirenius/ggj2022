using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A manager used to hold all necessary resources needed for the game.
/// This includes sprites, scriptable objects, prefabs, etc. 
/// Basically anything that we want to load from the Resources folder.
/// </summary> 
public class ResourceManager : Singleton<ResourceManager>
{
    // Contains all recipes in the game
    public LevelData[] Levels;
    public Dictionary<string, LevelData> LevelDictionary;
    public Dictionary<string, Material> MaterialDictionary;
    public Dictionary<string, GameObject> ParticleDictionary;

    public ObjectPooler ProjectilePooler;

    private void Awake()
    {
        LoadLevels();
        LoadProjectilePoolers();
        LoadMaterials();
        LoadParticles();
    }

    void LoadLevels()
    {
        Levels = Resources.LoadAll<LevelData>("Levels");
        LevelDictionary = new Dictionary<string, LevelData>();

        foreach (LevelData level in Levels)
        {
            LevelDictionary[level.levelName] = level;
        }
    }

    void LoadProjectilePoolers()
    {
        // TODO: There's only one projectile pooler right now
        // Add support for multiple projectiles
        GameObject[] objectPoolers = Resources.LoadAll<GameObject>("Prefabs/ObjectPoolers/Projectiles");
        foreach (GameObject obj in objectPoolers)
        {
            GameObject poolObj = Instantiate(obj) as GameObject;
            ProjectilePooler = poolObj.GetComponent<ObjectPooler>();
        }
    }

    void LoadMaterials()
    {
        Material[] materials = Resources.LoadAll<Material>("Materials");
        MaterialDictionary = new Dictionary<string, Material>();

        foreach (Material mat in materials)
        {
            MaterialDictionary[mat.name] = mat;
        }
    }

    void LoadParticles()
    {
        GameObject[] particles = Resources.LoadAll<GameObject>("ParticleSystems");
        ParticleDictionary = new Dictionary<string, GameObject>();

        foreach (GameObject particle in particles)
        {
            ParticleDictionary[particle.name] = particle;
        }
    }

}