using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFightController : MonoBehaviour
{
    public int totalLife = 50;
    private int actualLife = 50;

    [SerializeField]
    private PlayerFightController otherPlayer;
    private List<string> positions = new List<string> {"U","M","D"};
    private List<int> danios = new List<int> {10,10,10};
    private string actualPosition = "M";
    private bool isInDefense = true;

    // Start is called before the first frame update
    void Start()
    {
        actualLife = totalLife;
    }

    public void reciveAttack (string attackPosition) {
        int posIndex = positions.FindIndex(pos => pos == attackPosition);
        int damage = danios[posIndex];
        if ( (attackPosition != actualPosition) || (attackPosition == actualPosition && !isInDefense))
        {
            if (actualLife <= damage) {
                actualLife = 0;
                die();
            } else {
                actualLife -= damage;
            }
            Debug.Log("He recibido en " + attackPosition + " y me he hecho pupa en " + damage + " puntos. Me queda " + actualLife + " de vida");
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

    private void die() {
        Debug.Log("He muerto");
    }
}
