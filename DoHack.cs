using UnityEngine;
using System.Collections;

public class DoHack : MonoBehaviour {

    RoundManager roundManager;

    void OnTriggerEnter(Collider other)
    {
        TargetZone zone = other.GetComponent<TargetZone>();
        if (zone != null)
        {
            if (!zone.BeingHacked)
            {
                // Start a new hack!
                zone.BeingHacked = true;
                zone.playerHacking = NetworkManager.ownPlayer;
                zone.HackFinished += zone_HackFinished;
            }
        }
    }

    void zone_HackFinished()
    {
        // This is where we should call a method on RoundManager, so that it can alert all other players
        Debug.Log("HACK COMPLETE!");
    }

    void OnTriggerExit(Collider other)
    {
        TargetZone zone = other.GetComponent<TargetZone>();
        if (zone != null)
        {
            if (zone.BeingHacked)
            {
                // Left hacking area, cancel the hack. Send messages to RoundManager as well!
                zone.BeingHacked = false;
                zone.HackFinished -= zone_HackFinished;
            }
        }
    }
}
