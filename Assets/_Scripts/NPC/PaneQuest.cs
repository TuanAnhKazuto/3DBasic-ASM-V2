using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaneQuest : MonoBehaviour
{


    private bool isShown = false;
    private Vector3 initialPosition;
    private Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ShowHideQuestsPanel()
    {
        isShown = !isShown;
        if (isShown)
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(MovingPanel(true));
        }
        else
        {
            if (coroutine != null) StopCoroutine(coroutine);
            coroutine = StartCoroutine(MovingPanel(false));
        }
    }

    IEnumerator MovingPanel(bool show)
    {
        while (true)
        {
            var currentX = transform.localPosition.x;
            var currentY = transform.localPosition.y;
            var currentZ = transform.localPosition.z;
            var targetX = show ? initialPosition.x + 40 : initialPosition.x - 230 ;
            var newX = Mathf.Lerp(currentX, targetX, Time.deltaTime * 2);
            transform.localPosition = new Vector3 (newX, currentY, currentZ);

            if (Mathf.Abs(currentX - targetX) < 1)
            {
                break;
            }
            yield return null;

        }

    }



}
