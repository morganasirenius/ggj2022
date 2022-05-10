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
    public LevelData[] Levels;
    public Dictionary<string, LevelData> LevelDictionary { get; set; }
    public Dictionary<string, Material> MaterialDictionary { get; set; }
    public Dictionary<string, GameObject> ParticleDictionary { get; set; }

    public Dictionary<string, AudioClip> MusicDictionary { get; set; }
    public Dictionary<string, AudioClip> SfxDictionary { get; set; }
    public Dictionary<string, AudioClip> RescueSfxDictionary { get; set; }
    public Dictionary<string, TutorialText> TutorialTextDictionary { get; set; }
    public Dictionary<string, AnimalData[]> AnimalDataDictionary { get; set; }
    public Dictionary<string, AnimalData> AnimalToDataDictionary { get; set; }
    public Dictionary<string, List<Sprite>> SpaceObjectsDictionary { get; set; }

    public Dictionary<string, Sprite> RarityCardsDictionary { get; set; }

    public List<string> RescueSfxNames { get; set; }

    public Sprite[] AnimalSpriteArray { get; set; }



    public ObjectPooler ProjectilePooler;

    private void Awake()
    {
        LoadLevels();
        LoadProjectilePoolers();
        LoadMaterials();
        LoadParticles();
        LoadAudio();
        LoadAnimalSprites();
        LoadAnimalData();
        LoadTutorialText();
        LoadSpaceObjects();
        LoadRarityCards();
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

    void LoadAudio()
    {
        AudioClip[] music = Resources.LoadAll<AudioClip>("Audio/Music");
        AudioClip[] sfx = Resources.LoadAll<AudioClip>("Audio/SFX");
        AudioClip[] rescueSfx = Resources.LoadAll<AudioClip>("Audio/SFX/Rescues");

        MusicDictionary = new Dictionary<string, AudioClip>();
        SfxDictionary = new Dictionary<string, AudioClip>();
        RescueSfxDictionary = new Dictionary<string, AudioClip>();

        RescueSfxNames = new List<string>();

        foreach (AudioClip clip in music)
        {
            MusicDictionary[clip.name] = clip;
        }

        foreach (AudioClip clip in sfx)
        {
            SfxDictionary[clip.name] = clip;
        }

        foreach (AudioClip clip in rescueSfx)
        {
            RescueSfxDictionary[clip.name] = clip;
            RescueSfxNames.Add(clip.name);
        }
    }

    void LoadAnimalSprites()
    {
        AnimalSpriteArray = Resources.LoadAll<Sprite>("AnimalSprites/Default");
    }

    void LoadAnimalData()
    {
        AnimalDataDictionary = new Dictionary<string, AnimalData[]>();
        AnimalToDataDictionary = new Dictionary<string, AnimalData>();

        foreach (Globals.GachaponRarities rarity in System.Enum.GetValues(typeof(Globals.GachaponRarities)))
        {
            AnimalData[] dataArray = Resources.LoadAll<AnimalData>(string.Format("AnimalData/{0}", rarity.ToString()));
            foreach (AnimalData data in dataArray)
            {
                AnimalToDataDictionary[data.animalName] = data;
            }
            AnimalDataDictionary[rarity.ToString()] = dataArray;
        }
    }

    void LoadRarityCards()
    {
        RarityCardsDictionary = new Dictionary<string, Sprite>();
        Sprite[] RarirtyCards = Resources.LoadAll<Sprite>("CollectionView/RarityCards/");
        foreach (Sprite sprite in RarirtyCards)
        {
            RarityCardsDictionary[sprite.name] = sprite;
        }
    }
    void LoadSpaceObjects()
    {
        SpaceObjectsDictionary = new Dictionary<string, List<Sprite>>();
        string[] spriteNames = { "Asteroids", "Planets", "Clouds", "Stars" };
        foreach (string name in spriteNames)
        {
            List<Sprite> SpriteArray = new List<Sprite>();
            Sprite[] sprites = Resources.LoadAll<Sprite>("SpaceObjects/" + name);
            foreach (Sprite sprite in sprites)
            {
                SpriteArray.Add(sprite);
            }
            SpaceObjectsDictionary[name] = SpriteArray;
        }
    }

    public List<Sprite> ReturnSpaceObjects(string name)
    {
        return SpaceObjectsDictionary[name];
    }

    void LoadTutorialText()
    {
        TutorialText[] tutorialTexts = Resources.LoadAll<TutorialText>("TutorialTexts");
        TutorialTextDictionary = new Dictionary<string, TutorialText>();

        foreach (TutorialText text in tutorialTexts)
        {
            TutorialTextDictionary[text.name] = text;
        }
    }
}