using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    /**
     * Singleton class that stores the "state" (which is mostly unusused
     * for the game! (Basically GameData)
     */
    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects = new List<IDataPersistence>();

    private static DataPersistenceManager instance;
    public static DataPersistenceManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DataPersistenceManager>();
            }
            return instance;
        }
    }

    private void Start()
    {
        this.dataPersistenceObjects = FindAllDataPersistanceObjects();
        LoadGame();
    }

    /**
     * Once you've set this, you also access all the menu items namely 
     * 1) NewGame()
     * 2) LoadGame()
     * 3) SaveGame()
     * from here
     */
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    //1) Load saved data from a file using the data handler 
    public void LoadGame()
    {
        // If not present, initialize to a new game!
        if (this.gameData == null)
        {
            NewGame();
        }

        //Push loaded data to all other scripts that need it!

        foreach(IDataPersistence dpObj in dataPersistenceObjects)
        {
            dpObj.LoadData(gameData);

        }


    }


    
    //2) Save  data to file using data handler 
    public void SaveGame()
    {
        //1) Pass data to other scripts to update it 
        foreach (IDataPersistence dpObj in dataPersistenceObjects)
        {
            dpObj.LoadData(gameData);

        }

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    //Loads all Savable objects (that implement IDataPersistence) within the scnee!

    private List<IDataPersistence> FindAllDataPersistanceObjects()
    {
        //Sadly, they need to be monobehavs in this way
        IEnumerable<IDataPersistence> dpObjects = FindObjectsOfType<MonoBehaviour>().
        OfType<IDataPersistence>();
        return new List<IDataPersistence>(dpObjects);



    }



}
