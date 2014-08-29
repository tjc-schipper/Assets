﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoundManager : Photon.MonoBehaviour
{
    public GameRound currentRound;

    [RPC]
    public void RespawnCharacter()
    {
        CharacterFactory fact = GameObject.FindObjectsOfType<CharacterFactory>()[0];
        SpawnPoint spawn = GetSpawnPoints(NetworkManager.ownPlayer.PlayerTeam)[0];

        // Create character
        if (NetworkManager.ownPlayer.PlayerTeam == Player.PlayerTeams.GUARD)
            NetworkManager.ownPlayer.Character = fact.GetGuard();
        else
            NetworkManager.ownPlayer.Character = fact.GetSpy();

        // Move to spawn point
        SpyMovement move = NetworkManager.ownPlayer.Character.GetComponent<SpyMovement>();
        move.Position = spawn.transform.position;
        spawn.Available = false;
    }

    [RPC]
    public void InitRound()
    {
        ResetSpawnPoints();
        RespawnCharacter();
        currentRound = new GameRound();
        currentRound.started = true;
    }

    private void ResetSpawnPoints()
    {
        SpawnPoint[] spawns = GameObject.FindObjectsOfType<SpawnPoint>();
        foreach (SpawnPoint spawn in spawns)
            spawn.Available = true;
    }
    private List<SpawnPoint> GetSpawnPoints(Player.PlayerTeams team)
    {
        SpawnPoint[] allSpawns = GameObject.FindObjectsOfType<SpawnPoint>();
        List<SpawnPoint> spawns = new List<SpawnPoint>();
        for (int i = 0; i < allSpawns.Length; i++)
        {
            if (allSpawns[i].Team == team)
            {
                if (allSpawns[i].Available)
                {
                    spawns.Add(allSpawns[i]);
                }
            }
        }
        return new List<SpawnPoint>(spawns);
    }

    public void StartNewRound(List<Player> players)
    {
        photonView.RPC("InitRound", PhotonTargets.AllBuffered);
    }
}
