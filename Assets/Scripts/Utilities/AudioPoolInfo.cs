
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPoolInfo : MonoBehaviour
{
    public int poolSize;
    public AudioClip[] clips;
    public string sourceTags;
    public Transform holderObj;
    public GameObject[] sourceObjs;
    public int iterator;
    public Dictionary<string, AudioClip> clipList;
}