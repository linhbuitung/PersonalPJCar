using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField]
    float destroyDelay;
    bool hasPackage;

    string colorCheck;
    private SpriteRenderer sprite;

    public Sprite Blue;
    public Sprite Yellow;
    public Sprite Purple;

    string packageColor;
    void Start()
    {
        sprite = transform.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        clearString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Package" && !hasPackage)
        {
            hasPackage = true;
            CheckColor(collision);
            Destroy(collision.gameObject, destroyDelay);
        }
        if (collision.tag == "Customer" && hasPackage)
        {
            checkCus(collision);
        }
    }


    private void CheckColor(Collider2D collision)
    {
        switch (collision.GetComponent<Package_Logic>().packageColor)
        {
            case "Blue":
                sprite.sprite = Blue;
                packageColor = "Blue";
                break;
            case "Yellow":
                sprite.sprite = Yellow;
                packageColor = "Yellow";
                break;
            case "Purple":
                sprite.sprite = Purple;
                packageColor = "Purple";
                break;
        }
    }

    private void checkCus(Collider2D collision)
    {
        if (collision.GetComponent<Customer_Logic>().customerColor == packageColor)
        {
            hasPackage = false;
        }
    }
    
    private void clearString()
    {
        if (!hasPackage)
        {
            packageColor = "";
        }
    }
}
