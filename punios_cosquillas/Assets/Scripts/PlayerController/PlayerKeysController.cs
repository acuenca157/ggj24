using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKeysController : MonoBehaviour
{
    public bool isLeftPlayer;
    private PlayerFightController playerFightController;
    // Start is called before the first frame update
    void Start()
    {
        playerFightController = this.gameObject.GetComponent<PlayerFightController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeftPlayer)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerFightController.changePosition(true);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                playerFightController.changePosition(false);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerFightController.attack();
            }
        }
        else {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                playerFightController.changePosition(true);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                playerFightController.changePosition(false);
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                playerFightController.attack();
            }
        }
    }
}
