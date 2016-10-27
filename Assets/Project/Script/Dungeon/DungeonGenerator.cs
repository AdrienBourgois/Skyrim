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

    private const float SpawnPointX = 3.5f;
    private const float SpawnPointY = 1f;
    private const float SpawnPointZ = 6f;

    public void GenerateDungeon()
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

        CheckEmptyConnection(pendingConnections, modules[2]);
        GameManager.Instance.ChangeGameStateTo(GameManager.GameState.PopulateDungeon);
    }

    private void ModuleCreation(Module _module, ModuleConnector _pendingConnection, List<ModuleConnector> _newConnections)
    {
        
            Module newModule = Instantiate(_module);
            ModuleConnector[] newModuleConnection = newModule.GetExits();
            ModuleConnector connectionToMatch = newModuleConnection.FirstOrDefault(_x => _x.IsDefault) ?? GetRandom(newModuleConnection);
            MatchConnection(_pendingConnection, connectionToMatch);
            _newConnections.AddRange(newModuleConnection.Where(_c => _c != connectionToMatch));
            connectionToMatch.IsConnected = true;
            newModule.transform.SetParent(transform);
        
    }

    private void AddSpawnPoint(Module _module)
    {
        Transform moduleTransform = _module.transform;
        Vector3 modulePosition = moduleTransform.position;
        Object spawPointPrefab = Resources.Load("Dungeon/Door");
        GameObject spawPoint = 
            (GameObject)Instantiate(spawPointPrefab, 
            new Vector3(modulePosition.x + SpawnPointX, modulePosition.y + SpawnPointY, modulePosition.z + SpawnPointZ), 
            moduleTransform.rotation);

        spawPoint.transform.SetParent(moduleTransform);
        
    }

    private void CheckEmptyConnection(List<ModuleConnector> _pendingConnections, Module _fillingModule)
    {
        foreach (ModuleConnector connection in _pendingConnections)
        {
            if (connection.IsConnected == false)
            {
                List<ModuleConnector> newConnections = new List<ModuleConnector>();
                ModuleCreation(_fillingModule, connection, newConnections);
            }
        }
    }

    private void MatchConnection(ModuleConnector _oldConnection, ModuleConnector _newConnection)
    {
        Transform newModule = _newConnection.transform.parent;
        Vector3 forwardVectorToMatch = -_oldConnection.transform.forward;
        float correctiveRotation = Azimuth(forwardVectorToMatch) - Azimuth(_newConnection.transform.forward);
        newModule.RotateAround(_newConnection.transform.position, Vector3.up, correctiveRotation);
        Vector3 correctiveTranslation = _oldConnection.transform.position - _newConnection.transform.position;
        newModule.transform.position += correctiveTranslation;
        
    }

    #region GetRandom

    private static Module GetRandom(Module[] _array)
    {
        if (_array.Length <= 0)
            return null;

        return _array[Random.Range(0, _array.Length)];
    }

    private static string GetRandom(string[] _array)
    {
        if (_array.Length <= 0)
            return null;

        return _array[Random.Range(0, _array.Length)];
    }

   

    private static ModuleConnector GetRandom(ModuleConnector[] _array)
    {
        if (_array.Length <= 0)
            return null;

        return _array[Random.Range(0, _array.Length)];
    }

    #endregion

    private static Module GetRandomWithTag(IEnumerable<Module> _modules, string _tagToMatch)
    {
        Module[] matchingModules = _modules.Where(_module => _module.Tags.Contains(_tagToMatch)).ToArray();
        return GetRandom(matchingModules);
    }

    private static float Azimuth(Vector3 _vector)
    {
        return Vector3.Angle(Vector3.forward, _vector) * Mathf.Sign(_vector.x);
    }
}
