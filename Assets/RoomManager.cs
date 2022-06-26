using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager Instance;

     void Awake()
    {
        if(Instance)// checks if other room manager exsists 
        {
            Destroy(gameObject);// there can only be one 
                return;
        }

        DontDestroyOnLoad(gameObject);//I am the only one
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;


    }

    void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex==1) // here player in the game scene

        {
            PhotonNetwork.Instantiate(Path.Combine("photonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity); 
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
