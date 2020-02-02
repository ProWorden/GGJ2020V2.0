using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hit_detection : MonoBehaviour
{
    bool detected = false;
    Collider2D col;
    string target_tag;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == target_tag)
        {
            detected = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == target_tag)
        {
            detected = false;
        }
    }

    public void setTargetTag(string tag_)
    {
        target_tag = tag_;
    }

    public bool isTriggered()
    {
        return detected;
    }

    public bool isTriggered(string tag)
    {
        return detected && col.gameObject.tag == tag;
    }

    public bool isTriggered(int id)
    {
        return detected && col.gameObject.GetInstanceID() == id;
    }

    public string getTriggerTag()
    {
        return col.tag;
    }
}
