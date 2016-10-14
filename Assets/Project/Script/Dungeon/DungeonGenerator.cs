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

    void Start()
    {
        Module firstModule = (Module)Instantiate(startModule, transform.position, transform.rotation);
        List<ModuleConnector> pendingConnections = new List<ModuleConnector>(startModule.GetExits());

        for (int iteration = 0; iteration < iterations; iteration++)
        {
            List<ModuleConnector> newConnection = new List<ModuleConnector>();

            foreach (ModuleConnector pendingConnection in pendingConnections)
            {
                string newTag = GetRandom(pendingConnection.Tags);
                Module newModulePrefab = GetRandomWithTag(modules, newTag);
                Module newModule = Instantiate(newModulePrefab);
                print(newModule);
                ModuleConnector[] newModuleConnection = newModule.GetExits();
                ModuleConnector connectionToMatch = newModuleConnection.FirstOrDefault(x => x.IsDefault) ?? GetRandom(newModuleConnection);
                MatchConnection(pendingConnection, connectionToMatch);
                newConnection.AddRange(newModuleConnection.Where(c => c != connectionToMatch));
            }

            pendingConnections = newConnection;
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

    private static T GetRandom<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }

    private static Module GetRandomWithTag(IEnumerable<Module> modules, string tagToMatch)
    {
        Module[] matchingModules = modules.Where(m => m.Tags.Contains(tagToMatch)).ToArray();
        return GetRandom(matchingModules);
    }

    private static float Azimuth(Vector3 vector)
    {
        return Vector3.Angle(Vector3.forward, vector) * Mathf.Sign(vector.x);
    }
}
