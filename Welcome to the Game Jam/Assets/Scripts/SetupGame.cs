using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
    public int player_count = 2;
    public GameObject player;
    public Sprite player_sprite;
    public Camera main_camera;
    public GameObject barrier;

    float distanceZ;
    float leftConstraint;
    float rightConstraint;
    float bottomConstraint;

    float screen_size;

    private void Awake()
    {
        player_count %= 5;
        player_count++;

        main_camera.GetComponent<VerticalScrolling>().setupList(player_count);
        setupBoundries();

        float player_pos = -21;
        Debug.Log(player_count);

        for (int i=1; i<player_count; i++)
        {
            GameObject new_player = Instantiate(player);
            new_player.GetComponent<CharacterController2D>().setup(i);
            new_player.GetComponent<place_block>().setup(i);

            //player_pos = rightConstraint;

            new_player.transform.position = new Vector3(player_pos, -7, 0);
            player_pos += 14;

            main_camera.GetComponent<VerticalScrolling>().addPlayerToList(new_player, i);

            Debug.Log("Creating Player [" + i + "]");
        }

        setup_barriers();

    }

    void setupBoundries()
    {
        distanceZ = Mathf.Abs(main_camera.transform.position.z - transform.position.z);
        leftConstraint = main_camera.ScreenToWorldPoint(new Vector3(Screen.width * 0.0f, 0.0f, distanceZ)).x;
        rightConstraint = main_camera.ScreenToWorldPoint(new Vector3(Screen.width * 1.0f, 0.0f, distanceZ)).x;

        screen_size = rightConstraint - leftConstraint;

        this.GetComponent<LoopScreen>().setup(distanceZ, leftConstraint, rightConstraint);
    }

    void setup_barriers()
    {
        for (int i=0; i<player_count+2; i++)
        {

            float pos = leftConstraint + i * (screen_size / (player_count + 1));

            GameObject new_barrier = Instantiate(barrier);
            new_barrier.transform.position = new Vector3(pos, 7, 1);
        }
    }
}
