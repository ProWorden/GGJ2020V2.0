using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
    public int player_count = 2;
    public GameObject player;
    public Sprite player_sprite;
    public Camera main_camera;

    private void Awake()
    {
        player_count %= 5;
        player_count++;

        main_camera.GetComponent<VerticalScrolling>().setupList(player_count);

        float player_pos = -21;
        Debug.Log(player_count);

        for (int i=1; i<player_count; i++)
        {
            GameObject new_player = Instantiate(player);
            new_player.GetComponent<CharacterController2D>().setup(i);
            new_player.GetComponent<place_block>().setup(i);
            new_player.transform.position = new Vector3(player_pos, -7, 0);
            player_pos += 14;

            main_camera.GetComponent<VerticalScrolling>().addPlayerToList(new_player, i);

            Debug.Log("Creating Player [" + i + "]");
        }
    }
}
