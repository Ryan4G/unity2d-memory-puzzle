using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButton : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private string targetMessage;

    public Color highlightColor = Color.cyan;

    public void OnMouseEnter()
    {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = highlightColor;
        }
    }

    public void OnMouseExit()
    {
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = Color.white;
        }
    }

    public void OnMouseDown()
    {
        transform.localScale = Vector3.one * 1.5f;
    }

    public void OnMouseUp()
    {
        transform.localScale = Vector3.one * 1.3f;
        if (targetObject != null)
        {
            targetObject.SendMessage(targetMessage);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
