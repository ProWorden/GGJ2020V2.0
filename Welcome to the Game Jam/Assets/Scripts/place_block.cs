using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class place_block : MonoBehaviour
{
    // Update is called once per frame

    public GameObject[] block_list;
    public GameObject arrow_obj;

    int block_count = 0;
    int selection = 0;
    GameObject current_block;
    GameObject[] menu_blocks;
    GameObject arrow;
    public float forceMultiplyer;

    enum state
    {
        MOVING = 0,
        MENU = 1,
        CARRYING = 2
    }

    state current_state = state.MOVING;

    private void Start()
    {
        block_count = block_list.Length;
        menu_blocks = new GameObject[block_count];
    }


    void Update()
    {
        if (current_state == state.MENU)
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
                current_block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                current_block.GetComponent<PolygonCollider2D>().enabled = false;
                Vector2 block_pos = this.transform.position;
                block_pos.y += 1;

                current_block.transform.position = block_pos;
                current_block.transform.parent = this.transform;

                closeMenu();
                current_state = state.CARRYING;
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                closeMenu();
                current_state = state.MOVING;
            }

        }
        else if (Input.GetButtonDown("Open") && current_state == state.MOVING)
        {
            openMenu();
        }
        else if (Input.GetButtonDown("Throw") && current_state == state.CARRYING)
        {
            throwBlock();
            current_state = state.MOVING;
        }
        else if (Input.GetButtonDown("Drop") && current_state == state.CARRYING)
        {
            dropBlock();
            current_state = state.MOVING;
        }
    }

    void closeMenu()
    {
        for (int i = 0; i < block_count; i++)
        {
            GameObject.Destroy(menu_blocks[i]);

        }

        GameObject.Destroy(arrow);

        this.GetComponent<CharacterController2D>().enabled = true;
    }

    void openMenu()
    {
        current_state = state.MENU;
        Vector2 player_pos = this.transform.position;

        for (int i = 0; i < block_count; i++)
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

    void throwBlock()
    {
        current_block.transform.parent = null;

        current_block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        current_block.GetComponent<PolygonCollider2D>().enabled = true;
        Vector2 force;
        force.x = 1;
        force.y = 0.5f;

        if (!this.gameObject.GetComponentInChildren<SpriteRenderer>().flipX)
        {
            force.x = -1;
        }

        current_block.GetComponent<Rigidbody2D>().AddForce(force*forceMultiplyer);
    }

    void dropBlock()
    {
        current_block.transform.parent = null;

        current_block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        current_block.GetComponent<PolygonCollider2D>().enabled = true;

        Vector2 pos = current_block.transform.position;

        if (this.gameObject.GetComponentInChildren<SpriteRenderer>().flipX)
        {
            pos.x += 1.5f;
        }
        else
        {
            pos.x -= 1.5f;
        }

        current_block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        current_block.GetComponent<PolygonCollider2D>().enabled = true;
        current_block.transform.position = pos;
    }
}
