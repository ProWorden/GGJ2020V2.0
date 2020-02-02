using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class place_block : MonoBehaviour
{
    // Update is called once per frame

    public GameObject[] block_list;
    public GameObject arrow_obj;

    int block_count = 0;

    bool in_menu = false;
    int selection = 0;
    GameObject current_block;
    GameObject[] menu_blocks;
    GameObject arrow;

    private void Start()
    {
        block_count = block_list.Length;
        menu_blocks = new GameObject[block_count];
    }


    void Update()
    {
        if (Input.GetButtonDown("Open") && !in_menu)
        {
            in_menu = true;
            Vector2 player_pos = this.transform.position;

            for (int i=0; i< block_count; i++)
            {
                Vector2 block_pos;
                block_pos.y = player_pos.y + 2;
                block_pos.x = player_pos.x - 5 + (i * 2);

                menu_blocks[i] = Instantiate(block_list[i]);
                menu_blocks[i].transform.position = block_pos;
                menu_blocks[i].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                menu_blocks[i].GetComponent<PolygonCollider2D>().enabled = false;

                this.GetComponent<CharacterController2D>().enabled = false;
            }

            arrow = Instantiate(arrow_obj);
        }

        if (in_menu)
        {
            Vector2 pos = arrow.transform.position;
            pos.x = menu_blocks[selection].transform.position.x;
            pos.y = menu_blocks[selection].transform.position.y + 2;
            arrow.transform.position = pos;

            if (Input.GetAxis("Horizontal") > 0 && Input.GetButtonDown("Horizontal"))
            {
                selection++;
                selection %= block_count;
            }
            else if (Input.GetAxis("Horizontal") < 0 && Input.GetButtonDown("Horizontal"))
            {
                selection--;                
                if (selection < 0)
                {
                    selection *= -1;
                    selection += 4;
                }
                selection %= block_count;
            }
            else if (Input.GetButtonDown("Select"))
            {
                current_block = Instantiate(block_list[selection]);
            }

            if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Select"))
            {
                for (int i = 0; i < block_count; i++)
                {
                    GameObject.Destroy(menu_blocks[i]);

                }

                GameObject.Destroy(arrow);

                in_menu = false;
                this.GetComponent<CharacterController2D>().enabled = true;
            }

        }
    }
}
