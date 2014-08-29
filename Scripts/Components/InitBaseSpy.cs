using UnityEngine;
using System.Collections;

/// <summary>
/// This is run once when the object awakens, and configures the spy depending on if it's local or not.
/// It deletes itself after its work is done.
/// </summary>
public class InitBaseSpy : Photon.MonoBehaviour {

    void Awake()
    {
        if (photonView.isMine)
        {
            gameObject.AddComponent<SpyMovement>();
            gameObject.AddComponent<SpyPostures>();
            gameObject.AddComponent<DirectionFollowMouse>();
        }
        else
        {
            gameObject.AddComponent<SyncSpyMovement>();
            gameObject.AddComponent<SpyPostures>();
        }
        Destroy(this);
    }
}
