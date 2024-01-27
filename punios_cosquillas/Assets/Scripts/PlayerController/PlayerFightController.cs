using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFightController : MonoBehaviour
{
    [SerializeField]
    private PlayerFightController otherPlayer;
    private List<string> positions = new List<string> {"U","M","D"};
    private string actualPosition = "M";
    private bool isInDefense = true;

    public void reciveAttack (string attackPosition) {
        if ( (attackPosition != actualPosition) || (attackPosition == actualPosition && !isInDefense))
        {
            Debug.Log("He recibido en " + attackPosition + " y me he hecho pupa");
            // PUPA
        }
        else {
            Debug.Log("He recibido en " + attackPosition + " y me he come la polla porque me he defendido");
        }
    }

    public void attack() {
        otherPlayer.reciveAttack(actualPosition);
        StartCoroutine(attackCourrutine());
    }

    public void changePosition(bool changeUp) {
        if (changeUp)
        {
            if (actualPosition != "U")
            {
                int actualPosIndex = positions.FindIndex(pos => pos == actualPosition);
                actualPosition = positions[actualPosIndex - 1];
                isInDefense = true;
            }
        }
        else {
            if (actualPosition != "D")
            {
                int actualPosIndex = positions.FindIndex(pos => pos == actualPosition);
                actualPosition = positions[actualPosIndex + 1];
                isInDefense = true;
            }
        }


        Debug.Log("He cambiado a " + actualPosition);
    }

    private IEnumerator attackCourrutine() {
        isInDefense = false;
        yield return new WaitForSeconds(1.3f);
        isInDefense = true;
        yield return new WaitForEndOfFrame();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
