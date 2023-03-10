using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    static List<PlayerController> players = new List<PlayerController>();
    [SerializeField] private Color[] colors = new Color[4];

    public void OnJoin(PlayerInput playerInput)
    {
        PlayerController player = playerInput.GetComponent<PlayerController>();
        Debug.Log(colors[players.Count] + ", " + players.Count);
        player.SetColor(colors[players.Count]);
        players.Add(player);
    }

    public void OnLeave(PlayerInput playerInput)
    {
        players.Remove(playerInput.GetComponent<PlayerController>());
    }

    public List<PlayerController> GetPlayers()
    {
        return players;
    }
}
