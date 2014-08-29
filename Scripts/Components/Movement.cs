﻿using UnityEngine;
using System;
using System.Collections;

public class Movement : Photon.MonoBehaviour
{

    public SpyState state
    {
        get
        {
            if (_state == null)
                _state = GetComponent<SpyState>();
            return _state;
        }
        set
        {
            _state = value;
        }
    }
    private SpyState _state;
    //public SpyState state;

    public Vector3 Position
    {
        get
        {
            return rigidbody.position;
        }
        set
        {
            rigidbody.MovePosition(value);
        }
    }

    public Quaternion Rotation
    {
        get
        {
            return rigidbody.rotation;
        }
        set
        {
            rigidbody.MoveRotation(value);
        }
    }

    public float Angle
    {
        get
        {
            return RotationToAngle(Rotation);
        }
        set
        {
            Rotation = AngleToRotation(value);
        }
    }

    public Vector3 Direction
    {
        get
        {
            return transform.forward;
        }
        set
        {
            Vector3 n = value.normalized;
            Rotation = Quaternion.LookRotation(n);
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return rigidbody.velocity;
        }
        set
        {
            Vector3 deltaV = value - Velocity;
            rigidbody.AddForce(deltaV, ForceMode.VelocityChange);
        }
    }

    protected Quaternion AngleToRotation(float a)
    {
        // Convention: +x is 0, CCW. Unit circle
        Vector3 dir = AngleToDirection(a);
        return Quaternion.LookRotation(dir, transform.up);
    }
    protected Vector3 AngleToDirection(float a)
    {
        float x, z;
        return new Vector3(
            Mathf.Sin(a),
            0f,
            Mathf.Cos(a)
            );
    }

    protected float RotationToAngle(Quaternion q)
    {
        return q.eulerAngles.y;
    }
    protected Vector3 RotationToDirection(Quaternion q)
    {
        throw new NotImplementedException();
        return Vector3.zero;
    }
}