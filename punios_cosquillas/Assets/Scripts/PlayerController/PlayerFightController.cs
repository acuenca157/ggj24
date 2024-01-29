using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using UnityEngine.SceneManagement;

public class PlayerFightController : MonoBehaviour
{
    public int totalLife = 50;
    private int actualLife = 50;

    [SerializeField]
    private PlayerFightController otherPlayer;
    private List<string> positions = new List<string> {"U","M","D"};
    private List<int> danios = new List<int> {10,10,10};
    [SerializeField]
    private GameObject[] attackArms, defendArms, particlePositions;
    [SerializeField]
    private GameObject particleAttack;
    private string actualPosition = "M";
    private bool isInDefense = true;
    [SerializeField]
    private Slider vida;
    [SerializeField]
    private string sceneName;

    [SerializeField]
    private EventReference eventoDano, eventoDefensa, eventoMover;

    // Start is called before the first frame update
    void Start()
    {
        actualLife = totalLife;
        resetArms();
        actualPosition = "M";
        defendArms[1].SetActive(true);
        vida.value = 100 - actualLife;
    }

    private void resetArms() {
        foreach (GameObject arm in attackArms)
        {
            arm.SetActive(false);
        }
        foreach (GameObject arm in defendArms)
        {
            arm.SetActive(false);
        }
        defendArms[getActualPositionIndex()].SetActive(true);
    }

    private int getActualPositionIndex() {
        return positions.FindIndex(pos => pos == actualPosition);
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
            Instantiate(particleAttack, particlePositions[getActualPositionIndex()].transform.position, Quaternion.identity);

                FMODUnity.RuntimeManager.PlayOneShot(eventoDano);
            Debug.Log("He recibido en " + attackPosition + " y me he hecho pupa en " + damage + " puntos. Me queda " + actualLife + " de vida");
        }
        else {
                FMODUnity.RuntimeManager.PlayOneShot(eventoDefensa);
            Debug.Log("He recibido en " + attackPosition + " y me he come la polla porque me he defendido");
        }
        vida.value = 100 - actualLife;
    }

    public void attack() {
        if (isInDefense) {
            otherPlayer.reciveAttack(actualPosition);
            StartCoroutine(attackCourrutine());
        }
        
    }

    public void changePosition(bool changeUp) {
        if (isInDefense) {
            if (changeUp)
            {
                if (actualPosition != "U")
                {
                    int actualPosIndex = positions.FindIndex(pos => pos == actualPosition);
                    actualPosition = positions[actualPosIndex - 1];
                    isInDefense = true;
                    resetArms();
                    FMODUnity.RuntimeManager.PlayOneShot(eventoMover);
                }
            }
            else
            {
                if (actualPosition != "D")
                {
                    int actualPosIndex = positions.FindIndex(pos => pos == actualPosition);
                    actualPosition = positions[actualPosIndex + 1];
                    isInDefense = true;
                    resetArms();
                    FMODUnity.RuntimeManager.PlayOneShot(eventoMover);
                }
            }
            Debug.Log("He cambiado a " + actualPosition);
        }
        
    }

    private IEnumerator attackCourrutine() {
        isInDefense = false;
        defendArms[getActualPositionIndex()].SetActive(false);
        attackArms[getActualPositionIndex()].SetActive(true);
        yield return new WaitForSeconds(1.0f);
        isInDefense = true;
        resetArms();
        yield return new WaitForEndOfFrame();
    }

    private void die() {
        SceneManager.LoadScene(sceneName);
        Debug.Log("He muerto");
    }
}
