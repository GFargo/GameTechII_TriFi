using UnityEngine;
using System.Collections;

//extend MonoBehaviour so that we can attach this script to an object
public class LevelSelectObject: MonoBehaviour 
{
        //A public bool so that we can change in the editor whether or not the button will quit the application
    public bool isLevel1 = false;
	public bool isLevel2 = false;
	public bool isLevel3 = false;
	public bool isLevel4 = false;

	public Material brown_idle;
	public Material green_idle;
	public Material red_idle;
	public Material purp_idle;

	public Material brown_hover;
	public Material green_hover;
	public Material red_hover;
	public Material purp_hover;
    //For the next three functions, the collider is absolutely necessary
    //these functions will not fire off if there is no collider

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel("MainMenu");
	}
	
    //Fires off when the mouse hovers over the collider
    //When the mouse is over the item, change the colour of it to 
    //red so that the player knows that it is interacting with it
    void OnMouseEnter()
    {
		if(isLevel1)
        {
             renderer.material = brown_hover;
        }
		if(isLevel2)
		{
			renderer.material = green_hover;
		}
		if(isLevel3)
		{
			renderer.material = purp_hover;
		}
		if(isLevel4)
		{
			renderer.material = red_hover;
		}
		else{
		
		}
            
    }

    //Fires off when the mouse leaves the object
    //We want to change the colour of the object back to it's original when the mouse 
    //is no longer over it so that is exactly what we do here
    void OnMouseExit()
    {
        if(isLevel1)
        {
             renderer.material = brown_idle;
        }
		if(isLevel2)
		{
			renderer.material = green_idle;
		}
		if(isLevel3)
		{
			renderer.material = purp_idle;
		}
		if(isLevel4)
		{
			renderer.material = red_idle;
		}
		else{
		
		}
    }



    //Fires off when the mouse is clicked while hovering over the object
    //Here we check if the bool was set to true or not and we load the level id not
    //or quit the application if true
    void OnMouseDown()
    {
		 if(isLevel1)
        {
			if (2 < Application.levelCount)
				Application.LoadLevel(2);
		}
		if(isLevel2)
		{
			if (3 < Application.levelCount)
				Application.LoadLevel(3);
		}
		if(isLevel3)
		{
			if (4 < Application.levelCount)
				Application.LoadLevel(4);
		}
		if(isLevel4)
		{
			if (5 < Application.levelCount)
				Application.LoadLevel(5);
		}
	  	else{
		}
    }
}