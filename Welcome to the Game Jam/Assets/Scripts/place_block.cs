using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class place_block : MonoBehaviour
{
    // Update is called once per frame

    public GameObject[] block_list;
    public GameObject arrow_obj;
    public GameObject hit_check;

    int block_count = 0;
    int selection = 0;
    GameObject current_block;
    GameObject[] menu_blocks;
    GameObject arrow;
    bool canPlaceBlock = true;

    Vector2 spawn_pos;

    enum state
    {
        MOVING = 0,
        MENU = 1,
        CARRYING = 2,
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

                current_block.AddComponent<hit_detection>();
                current_block.GetComponent<hit_detection>().setTargetTag("Block");

                closeMenu();
                spawn_pos = block_pos;
                current_state = state.CARRYING;
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                closeMenu();
                current_state = state.MOVING;
            }

        }
        else if (Input.GetButtonDown("Open") && current_state == state.MOVING && isGrouded())
        {
            openMenu();
        }
        

        else if (current_state == state.CARRYING)
        {
          //if (this.gameObject.GetComponentInChildren<SpriteRenderer>().flipX)
          //{
          //    pos.x += 1.2f;
          //}
          //else
          //{
          //    pos.x -= 1.2f;
          //}

            
            if (testCollision())
            {
                /*
                if (Input.GetButton("Drop"))
                {
                    rotateBlock();
                }
                */

                if (Input.GetButtonDown("Throw"))
                {
                    throwBlock();
                    current_state = state.MOVING;
                }
                else if (Input.GetButtonUp("Drop"))
                {
                    dropBlock();
                    current_state = state.MOVING;
                }
                else if (Input.GetButtonDown("RotRight"))
                {
                    rotateBlock(-1);
                }
                else if (Input.GetButtonDown("RotLeft"))
                {
                    rotateBlock(1);
                }
            }       
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

        current_block.GetComponent<Rigidbody2D>().AddForce(force*250);
    }


    void dropBlock()
    {
        current_block.transform.parent = null;
        current_block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        current_block.transform.position = spawn_pos;
        current_block.GetComponent<PolygonCollider2D>().enabled = true;
        
    }


    /*
    void dropBlock()
    {
        current_block.transform.parent = null;

        current_block.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

        Vector2 force = Vector2.zero;

        if (this.gameObject.GetComponentInChildren<SpriteRenderer>().flipX)
        {
            force.x = 1;
        }
        else
        {
            force.x = -1;
        }

        current_block.GetComponent<PolygonCollider2D>().isTrigger = false;
        current_block.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        current_block.GetComponent<Rigidbody2D>().AddForce(force * 200);
    }
    */


    bool isGrouded()
    {
        if(this.GetComponent<CharacterController2D>().velocity.y > -0.2
            && this.GetComponent<CharacterController2D>().velocity.y < 0.2)
        {
            return true;
        }

        return false;
    }

    bool testCollision()
    {
        
        spawn_pos.x = this.transform.position.x - 1.2f;

        if (this.gameObject.GetComponentInChildren<SpriteRenderer>().flipX)
        {
            spawn_pos.x = this.transform.position.x + 1.2f;
        }

        Collider2D collision = Physics2D.OverlapBox(spawn_pos, new Vector2(1, 1), 0);

        Vector2 pos = current_block.transform.position;
        if (collision)
        {
            spawn_pos.y += 0.5f;
            Debug.Log(spawn_pos);
            Debug.Log("COL");
            return false;
        }
        else
        {
            return true;
        }
        
        

      //for (int i=0; i<collisions.Length; i++)
      //{
      //    Debug.Log("Yo Collisions dough");
      //    pos.y += 3;
      //    current_block.transform.position = pos;
      //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block" &&
            collision.gameObject.GetInstanceID() != current_block.GetInstanceID())
        {
            canPlaceBlock = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Block" && 
            collision.gameObject.GetInstanceID() != current_block.GetInstanceID())
        {
            canPlaceBlock = true;
        }
    }

    void rotateBlock()
    {
        current_block.transform.Rotate(new Vector3(0, 0, Time.deltaTime * 300));
    }

    void rotateBlock(int dir)
    {
        current_block.transform.Rotate(new Vector3(0, 0, dir * 90));
    }
}
