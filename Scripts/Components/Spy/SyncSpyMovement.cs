using UnityEngine;
using System.Collections;

public class SyncSpyMovement : Movement
{

    // Used for lerping
    private Vector3 startPos;
    private Vector3 endPos;
    private float startAngle;
    private float endAngle;

    void Start()
    {
        state.OnSyncPositionReceived += SyncPositionHandler;
        state.OnSyncAngleReceived += SyncAngleHandler;
        state.inited = true;

        // Reset values to prevent weird start lerps
        startPos = Position;
        endPos = Position;
        startAngle = Angle;
        endAngle = Angle;
    }
    void OnDestroy()
    {
        state.OnSyncPositionReceived -= SyncPositionHandler;
        state.OnSyncAngleReceived -= SyncAngleHandler;
    }

    void Update()
    {
        Vector3 newPos = Vector3.Lerp(
            startPos,
            endPos,
            state.SyncLerpValue
            );
        Position = newPos;

        float newAngle = Mathf.Lerp(
            startAngle,
            endAngle,
            state.SyncLerpValue
            );
        Angle = newAngle;
    }

    private void SyncPositionHandler(Vector3 p)
    {
        endPos = p;
        startPos = Position;
    }

    private void SyncAngleHandler(float a)
    {
        endAngle = a;
        startAngle = Angle;
    }

    void Awake()
    {
        state = GetComponent<SpyState>();
    }
}
