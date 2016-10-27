﻿using UnityEngine;

public class LevelManager : MonoBehaviour {

    private Player player = null;
    public Player Player
    {
        get
        {
            if (!player)
                player = FindObjectOfType<Player>();

            return player;
        }
    }

    static private LevelManager instance;
    static public LevelManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject gao = GameObject.FindGameObjectWithTag("LevelManager");
                if (gao)
                    instance = gao.GetComponent<LevelManager>();
            }

            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        InstanceGame();
    }

    private void InstanceGame()
    {
        if (!player)
        {
            GameObject playerObject = Instantiate(ResourceManager.Instance.Load("Character/Player"));
            player = playerObject.GetComponent<Player>();
            
            if (player == null)
                Debug.LogError("LevelManager.Awake() - could not load Player from Prefab Character/Player.");
        }

        if (!FindObjectOfType<Cam>())
        {
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (mainCamera != null)
                Destroy(mainCamera);
            Instantiate(ResourceManager.Instance.Load("Character/Main Camera"));
        }

        if (!FindObjectOfType<Compass>())
            Instantiate(ResourceManager.Instance.Load("Gui/Compass"));

        if (!FindObjectOfType<IGGui>())
            Instantiate(ResourceManager.Instance.Load("Gui/inGameGui"));
    }
}
