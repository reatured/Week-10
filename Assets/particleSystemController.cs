using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSystemController : MonoBehaviour
{

    public ParticleSystem[] parSyss;


    public void playAllOnce()
    {
        parSyss[0].Play();


    }

    public ParticleSystem getPS(int index)
    {
        
        return parSyss[index];
    }
}
