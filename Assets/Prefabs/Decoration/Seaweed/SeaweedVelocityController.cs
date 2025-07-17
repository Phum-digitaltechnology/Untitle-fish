using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeaweedVelocityController : MonoBehaviour 
{
    [Range(0f,5f)] public float FishInfluenceStrength = 0.25f;
    public float EaseInTime = 0.15f;
    public float EaseOutTime = 0.15f;
    public float VelocityThreshold = 5f;

    private int _fishInfluence = Shader.PropertyToID("_FishInfluence");

    public void InfluenceSeaweed(Material mat, float XVelocity)
    {
        Debug.Log(XVelocity);
        mat.SetFloat(_fishInfluence, XVelocity);
    }

}
