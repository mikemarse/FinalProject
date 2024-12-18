using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] Texture2D cursorSprite;

    private Vector2 cursorCenter;
    // Start is called before the first frame update
    void Start()
    {
        cursorCenter = new Vector2(cursorSprite.width / 2, cursorSprite.height / 2);
        Cursor.SetCursor(cursorSprite, cursorCenter, CursorMode.Auto);
        
        /*if (SceneManager.GetActiveScene().name == "MainMenu") {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
        else {
            cursorCenter = new Vector2(cursorSprite.width / 2, cursorSprite.height / 2);
            Cursor.SetCursor(cursorSprite, cursorCenter, CursorMode.Auto);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
