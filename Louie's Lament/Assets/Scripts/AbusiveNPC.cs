using System;
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class AbusiveNPC : MonoBehaviour
{
    [SerializeField] private GameObject floorTrap;
    [SerializeField] private GameObject floorTrapPlug;
    private GameObject abuseCanvas;

    // CITATION:
    // Getting sprites to change
    // https://forum.unity.com/threads/need-to-get-a-sprite-using-its-name-from-assets-folder.377401/

    // Start is called before the first frame update
    void Start()
    {
        //System.Random random = new System.Random();
        //int randomSprite = random.Next(3);
        int randomSprite = 0;

        switch (randomSprite)
        {
            case 0:
                deployFloorTrap();
                break;
            case 1:
                deployNapAbuse();
                break;
            case 2:
                deployRedShirtAbuse();
                break;
        }
    }

    private void deployFloorTrap()
    {
        removeFloorTrapPlug();
        enableFloorTrap();
        updateCanvas(0);
    }

    private void deployNapAbuse()
    {
        disableFloorTrap();
        deployFloorTrapPlug();
        updateCanvas(1);
    }

    private void deployRedShirtAbuse()
    {
        disableFloorTrap();
        deployFloorTrapPlug();
        updateCanvas(2);
    }

    private void updateCanvas(int itemToDeploy)
    {
        abuseCanvas = GameObject.FindGameObjectWithTag("Abuse Canvas");
        SpriteRenderer abuseCanvasSpriteRenderer = abuseCanvas.GetComponent<Renderer>() as SpriteRenderer;

        switch (itemToDeploy)
        {
            case 0:
                abuseCanvasSpriteRenderer.sprite = Resources.Load<Sprite>("Block_Interact");
                break;
            case 1:
                abuseCanvasSpriteRenderer.sprite = Resources.Load<Sprite>("Block_RedShirt");
                break;
            case 2:
                abuseCanvasSpriteRenderer.sprite = Resources.Load<Sprite>("Block_AfterNap");
                break;
        }
    }

    private void enableFloorTrap()
    {        
        floorTrap.SetActive(true);
    }

    private void disableFloorTrap()
    {     
        floorTrap.SetActive(false);
    }

    private void deployFloorTrapPlug()
    {
        // uhhh...instantiate it?
    }

    private void removeFloorTrapPlug()
    {     
        Destroy(floorTrapPlug);
    }
}
