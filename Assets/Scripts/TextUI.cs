using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    public TMP_Text counter;
    void Update()
    {
        if (GameManager.Instance.currentScene == "Hub")
        {
            if (GameManager.Instance.activeSpot == 1)
            {
                counter.text = "Press F to enter camera mode, then press left mouse to capture a photo";
            }
            else if (GameManager.Instance.activeSpot == 2)
            {
                counter.text = "Jump and hold E to glide over to the other end of the room";
            }
            else if (GameManager.Instance.activeSpot == 3)
            {
                counter.text = "Press left shift to grapple while aiming at a green tile, then go to the middle of the platform";
            }
            else 
            {
                counter.text = "Enter the hallway to begin the tutorial, or enter a portal to start your adventure";
            }
        }
        if (GameManager.Instance.currentScene == "Cave")
        {
            if (GameManager.Instance.activeSpot == 1)
            {
                counter.text = "Capture a photo of the Mineshaft";
            }
            else if (GameManager.Instance.activeSpot == 2)
            {
                counter.text = "Capture a photo of the Giant Mushrooms";
            }
            else if (GameManager.Instance.activeSpot == 3)
            {
                counter.text = "Capture a photo of the Giant Crystal";
            }
            else
            {
                counter.text = "Take a photo at each of the three yellow rings";
            }
        }
        else if (GameManager.Instance.currentScene == "Redwood")
        {
            if (GameManager.Instance.activeSpot == 1)
            {
                counter.text = "Capture a photo of the Wild Birds";
            }
            else if (GameManager.Instance.activeSpot == 2)
            {
                counter.text = "Capture a photo of the Sunset";
            }
            else if (GameManager.Instance.activeSpot == 3)
            {
                counter.text = "Capture a photo of the Firewatch Tower";
            }
            else
            {
                counter.text = "Take a photo at each of the three yellow rings";
            }
        }
        if (GameManager.Instance.currentScene == "City")
        {
            if (GameManager.Instance.activeSpot == 1)
            {
                counter.text = "Capture a photo of the Fountain and Town Hall";
            }
            else if (GameManager.Instance.activeSpot == 2)
            {
                counter.text = "Capture a photo of the Crane and City Skyline";
            }
            else if (GameManager.Instance.activeSpot == 3)
            {
                counter.text = "Capture a photo of the Taxi Cab";
            }
            else
            {
                counter.text = "Take a photo at each of the three yellow rings";
            }
        }
    }
}
