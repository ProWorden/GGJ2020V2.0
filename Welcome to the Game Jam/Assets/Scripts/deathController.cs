using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathController : MonoBehaviour
{
    GameObject[] players;
    int[] death_order;
    int next_death = 0;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");      
        death_order = new int[players.Length];
        next_death = 0;
    }

    private void Update()
    {
        GameObject deathPoint = GameObject.FindGameObjectWithTag("Death");

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].transform.position.y < deathPoint.transform.position.y)
            {
                death_order[next_death] = players[i].GetComponent<CharacterController2D>().getPlayerNo();
                next_death++;
                Destroy(players[i]);
            }
        }
    }
}
