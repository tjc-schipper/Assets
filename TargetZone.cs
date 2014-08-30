using UnityEngine;
using System.Collections;

public class TargetZone : MonoBehaviour
{

    public event StealthEventTypes.VoidEvent HackFinished;
    public event StealthEventTypes.VoidEvent HackStopped;
    public event StealthEventTypes.VoidEvent HackStarted;

    private bool hackCompleted = false;
    private bool beingHacked = false;
    public bool BeingHacked
    {
        get { return beingHacked; }
        set
        {
            if (beingHacked != value)
            {
                if (value)
                {
                    beingHacked = true;
                    if (HackStarted != null)
                        HackStarted();
                }
                else
                {
                    beingHacked = false;
                    if (!hackCompleted)
                        if (HackStopped != null)
                            HackStopped();
                }

                tickTimer = 0f;
            }
        }
    }
    public Player playerHacking;
    public float hackProgress = 0f;

    public float timeBetweenHackTicks = 1000f;
    public float timeBetweenHackLoss = 2000f;
    private float tickTimer = 0f;

    void Update()
    {
        // Actively being hacked
        if (beingHacked)
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= timeBetweenHackTicks)
            {
                hackProgress += 0.1f;
                tickTimer = 0f;
            }

            if (hackProgress >= 1f) // Check if hack complete
            {
                hackCompleted = true;
                if (HackFinished != null)
                {
                    HackFinished();
                }
            }
        }
        // Just idly decreasing
        else
        {
            if (hackProgress > 0f)
            {
                tickTimer += Time.deltaTime;
                if (tickTimer >= timeBetweenHackLoss)
                {
                    hackProgress -= 0.1f;
                    tickTimer = 0f;
                }
            }
        }
    }
}
