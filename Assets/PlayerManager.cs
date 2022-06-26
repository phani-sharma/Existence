using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;


    void Awake()
    {
        PV = GetComponent<PhotonView>();    
    }


    void CreateController()
    {
        // Instantiate player Controller
        Debug.Log("Instantiate Player Controller");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }


    // Start is called before the first frame update
    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
