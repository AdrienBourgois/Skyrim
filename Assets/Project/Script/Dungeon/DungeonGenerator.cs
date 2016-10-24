using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour {

    #region SerializeField

    [SerializeField]
    private Module[] modules;

    [SerializeField]
    private Module startModule;

    [SerializeField]
    private int iterations = 5;



    #endregion

    private float spawnPointX = 2.5f;
    private float spawnPointY = 1f;
    private float spawnPointZ = 6f;

    void Start()
    {
        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        Module firstModule = (Module)Instantiate(startModule, transform.position, transform.rotation);
        firstModule.transform.SetParent(transform);
        AddSpawnPoint(firstModule);
        List<ModuleConnector> pendingConnections = new List<ModuleConnector>(startModule.GetExits());

        for (int iteration = 0; iteration < iterations; iteration++)
        {
            List<ModuleConnector> newConnections = new List<ModuleConnector>();

            foreach (ModuleConnector pendingConnection in pendingConnections)
            {
                if (pendingConnection.Tags.Length > 0)
                {
                    string newTag = GetRandom(pendingConnection.Tags);
                    Module newModulePrefab = GetRandomWithTag(modules, newTag);
                    ModuleCreation(newModulePrefab, pendingConnection, newConnections);
                }
            }
            pendingConnections = newConnections;
        }

        CheckEmptyConnection(pendingConnections, firstModule);
    }

    private void ModuleCreation(Module module, ModuleConnector pendingConnection, List<ModuleConnector> newConnections)
    {
        
            Module newModule = Instantiate(module);
            ModuleConnector[] newModuleConnection = newModule.GetExits();
            ModuleConnector connectionToMatch = newModuleConnection.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newModuleConnection);
            MatchConnection(pendingConnection, connectionToMatch);
            newConnections.AddRange(newModuleConnection.Where(c => c != connectionToMatch));
            connectionToMatch.IsConnected = true;
            newModule.transform.SetParent(transform);
        
    }

    private void AddSpawnPoint(Module module)
    {
        Transform moduleTransform = module.transform;
        Vector3 modulePosition = moduleTransform.position;
        Object spawPointPrefab = Resources.Load("Door");
        GameObject spawPoint = 
            (GameObject)Instantiate(spawPointPrefab, 
            new Vector3(modulePosition.x + spawnPointX, modulePosition.y + spawnPointY, modulePosition.z + spawnPointZ), 
            moduleTransform.rotation);

        spawPoint.transform.SetParent(moduleTransform);
        
    }

    private void CheckEmptyConnection(List<ModuleConnector> pendingConnections, Module fillingModule)
    {
        foreach (ModuleConnector connection in pendingConnections)
        {
            if (connection.IsConnected == false)
            {
                List<ModuleConnector> newConnections = new List<ModuleConnector>();
                ModuleCreation(fillingModule, connection, newConnections);
            }
        }
    }

    private void MatchConnection(ModuleConnector oldConnection, ModuleConnector newConnection)
    {
        Transform newModule = newConnection.transform.parent;
        Vector3 forwardVectorToMatch = -oldConnection.transform.forward;
        float correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(newConnection.transform.forward);
        newModule.RotateAround(newConnection.transform.position, Vector3.up, correctiveRotation);
        Vector3 correctiveTranslation = oldConnection.transform.position - newConnection.transform.position;
        newModule.transform.position += correctiveTranslation;
        
    }

    #region GetRandom

    private static Module GetRandom(Module[] array)
    {
        if (array.Length <= 0)
            return null;

        return array[Random.Range(0, array.Length)];
    }

    private static string GetRandom(string[] array)
    {
        if (array.Length <= 0)
            return null;

        return array[Random.Range(0, array.Length)];
    }

   

    private static ModuleConnector GetRandom(ModuleConnector[] array)
    {
        if (array.Length <= 0)
            return null;

        return array[Random.Range(0, array.Length)];
    }

    #endregion

    private static Module GetRandomWithTag(IEnumerable<Module> modules, string tagToMatch)
    {
        Module[] matchingModules = modules.Where(module => module.Tags.Contains(tagToMatch)).ToArray();
        return GetRandom(matchingModules);
    }

    private static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
}
