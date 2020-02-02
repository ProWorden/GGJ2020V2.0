using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScrolling : MonoBehaviour
{
    Transform target;
    GameObject[] players;
    int player_count;

     void FixedUpdate()
    {
        findTarget();

        if (target)
        {
            Vector3 targetPos = new Vector3(transform.position.x, target.position.y + 5, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.2f);
        }   
    }

    void findTarget()
    {
        int highest_player = 1;

        for (int i=1; i<player_count+1; i++)
        {
            float current_height = players[i].transform.position.y;
            float best_height = players[highest_player].transform.position.y;

            if (current_height > best_height)
            {
                highest_player = i;
            }
        }

        target = players[highest_player].transform;
    }

    public void setupList(int count)
    {
        players = new GameObject[count];
    }

    public void addPlayerToList(GameObject player, int player_no)
    {
        players[player_no] = player;
    }

}
