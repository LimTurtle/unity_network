using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public struct CreateMMOCharacterMessage : NetworkMessage
{
    public Color hairColor;
}

public enum Race
{
    None,
    Elvish,
    Dwarvish,
    Human
}

public class Network_Manager : NetworkManager
{
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<CreateMMOCharacterMessage>(OnCreateCharacter);
    }

    public override void OnClientConnect()
    {
        base.OnClientConnect();

        // you can send the message here, or wherever else you want
        CreateMMOCharacterMessage characterMessage = new CreateMMOCharacterMessage
        {

            hairColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
        };

        NetworkClient.Send(characterMessage);
    }

    void OnCreateCharacter(NetworkConnectionToClient conn, CreateMMOCharacterMessage message)
    {
        // playerPrefab is the one assigned in the inspector in Network
        // Manager but you can use different prefabs per race for example
        int rand = Random.Range(0, 2);
        GameObject gameobject = Instantiate(spawnPrefabs[rand]);

        // Apply data from the message however appropriate for your game
        // Typically Player would be a component you write with syncvars or properties
        //PlayerController player = gameobject.GetComponent<PlayerController>();
        //player.my_color = message.hairColor;

        // call this to use this gameobject as the primary controller
        NetworkServer.AddPlayerForConnection(conn, gameobject);
    }
}
