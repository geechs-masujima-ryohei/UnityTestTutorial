using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlayer : Player
{
    private Player player;

    private IEnumerator Start()
    {
        player = FindObjectOfType<Player>();

        while (true)
        {
            base.Update();
            yield return new WaitForSeconds(Random.Range(0f, 0.05f));
        }
    }

    protected new void Update()
    {
    }
}