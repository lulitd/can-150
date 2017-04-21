---
layout: post
title: "Radars as simple as 1,2,3"
categories:
  - Dev Log
  - Tutorial
tags:
  - Radar
  - Unity3d
  - Game Dev
  - Dev Log
---

Setting up a basic radar system in Unity is as simple as 1, 2, 3.

For Northern Spirits, we’ve set up a simple radar/mini map system which allows players to find the hidden gem pieces and other objects of interest. For this setup, we used a secondary camera, a new layer, and Unity’s UI system.  For the purposes of this tutorial, we have a simple scene setup with some spheres and a player.

{% capture fig_img %}
![SimpleScene]({{ site.url }}/images/posts/radar/1_simple_scene-min.png)
{% endcapture %}

<figure>
  {{ fig_img | markdownify | remove: "<p>" | remove: "</p>" }}
  <figcaption>A simple scene with a character and some spheres</figcaption>
</figure>

1. [Setting up the radar icons](#1-setting-up-the-radar-icons)
2. [The Radar Camera](#2-the-radar-camera)
3. [The Radar UI](#3-radar-ui)

----
## 1. Setting up the radar icons

On each object that you would like to display in your radar (the player, cave entrances, inns, etc.) create a child object(we've named them RadarIcon) and give it a sprite renderer component. Change the sprite to an icon representing your super special object!

![RadarSettings]({{ site.url }}/images/posts/radar/2_radar-icon-settings-min.png)

Make sure you rotate your sprites **90 degrees on the X-axis**. This will allow the sprites to be seen by the top-down camera that we will create for our radar.

![SceneView]({{ site.url }}/images/posts/radar/3_radar-icons-scene-view-min.png)

>Now you might be thinking, I can see the sprites in the game view, and it looks awful!

Here come layers to the rescue! Layers control which objects can interact with which objects.  You can use layers to control which objects can collide with each other, control lighting and control which objects are visible to which camera.

Create a new layer, we’ve called our's **Radar**. And you’ve guessed it, change all your radar icons to be on the **Radar layer**.

Now that all the radar icons are on the same layer, we can tell our main camera not to render them.  Select your main camera and in the inspector in the drop down menu for **culling mask uncheck the radar layer**. And like magic, the sprites have vanished.

![LayerMagic]({{ site.url}}/images/posts/radar/4_layers.gif)

![ResultsOfUsingLayers]({{ site.url}}/images/posts/radar/5_layer_result-min.png)

----

## 2. The Radar camera

Create a new camera object (Radar camera). Rotate the camera **90 degrees on the x-axis** so it is looking downwards and **move it upwards(y-axis)**.

![radarCameraSettings]({{ site.url}}/images/posts/radar/6_camera_settings-min.png){: .align-right}

**Camera settings:**
* __Clear flags:__  Solid color
* __Background color:__  Fully transparent Colour!(zero alpha!)
* __Culling Mask:__  Radar layer
* __Projection:__ Orthographic
* __Size:__ This will depend on the size of your game. Consider this the radius of your radar.

Ok, great but it's not much of a radar if it doesn’t move with the player. In order to get it to respond to the player’s movements you could  
1. Make the camera a child of the player game object or
2. Create a script that moves the camera according to the player’s position.

We went for option 2 as we didn’t want the player’s rotation to affect the camera’s viewpoint and it gave us finer control( like movement smoothness).

{% highlight csharp %}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower2D : MonoBehaviour
{
    public Transform target;// object the camera will follow

    public float dampTime = 0.1f; // movement damping
    public float cameraHeight; // height of camera
    public Vector3 offset; // offsetting the position if necessary

    Vector3 destinationPosition = Vector3.zero;
    float interpVel;
    new Transform transform;
    Camera cam;

    void Awake()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;

        transform = base.transform; // caching the camera's transform
        transform.position = target.transform.position; // setting our transform position
        cam = GetComponent<Camera>() ;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
           //we want to calculate the direction only in two axis(x and z).
            Vector3 posWithoutHeight = transform.position;
            posWithoutHeight.y = target.transform.position.y;
            Vector3 targetDirection = (target.transform.position - posWithoutHeight);

            // calculate the velocity
            interpVel = targetDirection.magnitude * dampTime;

            destinationPosition = target.transform.position + (targetDirection.normalized * interpVel * Time.deltaTime);

            Vector3 newPos = Vector3.Lerp(transform.position, destinationPosition + offset, 0.25f);
            newPos.y = cameraHeight;
            transform.position = newPos;

        }
    }
}

{% endhighlight %}

![followCameraSettings]({{ site.url}}/images/posts/radar/movement-min.png)

----
## 3. Radar UI
With the camera all setup we need to tell our camera to render to a texture in order to use it as part of the UI. **In your project window Create> Render texture.**  Assign this newly created render texture to your **radar camera’s target texture**.

![renderTexture]({{ site.url}}/images/posts/radar/7_render_texture-min.png)

To display the texture we will be using unity’s UI system. **In your hierarchy window>UI> Raw image.** This will automatically create a Canvas for you. **Change the raw Image texture to the newly created render texture.**

![displayTexture]({{ site.url}}/images/posts/radar/8_display-min.png)

You should now see your texture on screen. Position it to your liking.

![radarResult]({{ site.url}}/images/posts/radar/9_radar_result-min.png)

Now to make it look more like a radar, let's give it a background. While selecting your canvas in your hierarchy right click and select **ui>Image**. Change the source image of the image to your radar background and move it into position.

![radarBgSettings]({{ site.url}}/images/posts/radar/10_radar_bg-min.png)


![radarbackground]({{ site.url}}/images/posts/radar/11_radar_bg_result-min.png)

>Wait a minute, my radar background is covering my radar icons

Unity’s UI system renders items from top to bottom of the hierarchy, meaning items at the bottom of the hierarchy will be rendered above items at the top of your hierarchy.**So make your image texture a child of your background**.  


![fixedRadarBackground]({{ site.url}}/images/posts/radar/12_heirachy-order-min.png)

You’ll notice that some of the icons appear outside the radar circle. Select your radar background and **add a mask component**. This will mask out any of its children where it is not visible.


![maskedRadar]({{ site.url}}/images/posts/radar/13_mask-min.png)

### Voila! You now have a simple radar system.

![result]({{ site.url}}/images/posts/radar/result-min.jpg)
