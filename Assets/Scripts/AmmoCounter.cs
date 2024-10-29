using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{

    [SerializeField] PlayerInputHandler playerInputHandler;
    [SerializeField] TextMeshProUGUI ammoText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInputHandler.GetMainCharacter().GetIsDead()) {
            return;
        }

        ammoText.text = playerInputHandler.GetMainCharacter().GetLaserPistol().GetCurrentAmmo().ToString();
    }
}
