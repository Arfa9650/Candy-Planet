using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : IntEventInvoker
{
    float totalTime = 0.31f;
    float elapsedTime = 0f;

    bool donezo = false;

    private void Start()
    {
        unityEvents.Add(EventNames.GameOver, new GameOverEvent());
        EventManager.AddInvoker(EventNames.GameOver, this);
    }

    private void Update()
    {
        if (donezo)
        {
            elapsedTime += Time.deltaTime;
        }
        else
            elapsedTime = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 3 && collision.gameObject.GetComponent<Candy>().canEndGame)
        {
            donezo = true;
            if (elapsedTime >= totalTime)
                unityEvents[EventNames.GameOver].Invoke(Vector2.zero, 0);

        }
        else
            donezo = false;
    }
}
