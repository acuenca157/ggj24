using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private EventReference musica;
    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(musica);
    }
}
