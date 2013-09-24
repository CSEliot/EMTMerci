using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchManager : MonoBehaviour
{

    protected GameObject touchedObject;

    private Dictionary<int, GestureObject> gestures;
    private Dictionary<int, GameObject> boundObjects;
    private List<GestureObject> remove;

    // Use this for initialization
    void Start()
    {
        gestures = new Dictionary<int, GestureObject>();
        boundObjects = new Dictionary<int, GameObject>();
        remove = new List<GestureObject>();
    }

    // Update is called once per frame
    void Update()
    {
        remove.Clear();
        foreach (Touch touch in Input.touches)
        {
            ConvertToGestureObject(touch);
        }

        ConvertMouseToGestureObject();

        foreach (GestureObject g in gestures.Values)
        {
            boundObjects.TryGetValue(g.Id, out touchedObject);

	//Pauls shitty code	prob needs to be deleted	

				if(g.GameGesture == GestureObject.GameGestureType.Drag){
					
					TouchObject camSwing = Camera.main.GetComponent<TouchObject>();
					
					camSwing.OnScreenDrag(g);
			}
				if(g.GameGesture != GestureObject.GameGestureType.Drag){
				
					TouchObject camSwing = Camera.main.GetComponent<TouchObject>();
					
					camSwing.OnScreenEnd(g);
				}
			
				

	//End of paul's shitty code delete it prob
			
			if (touchedObject != null)
            {
                TouchObject touchObj = touchedObject.GetComponent<TouchObject>();
                CommonFlags touchObjFlags = touchedObject.GetComponent<CommonFlags>();
                
				if (touchObj != null && touchObjFlags.IsEnabled)
                {
                    if (g.GameGesture == GestureObject.GameGestureType.Tap)
                    {
                        touchObj.OnTap(g);
                    }
                    else if (g.GameGesture == GestureObject.GameGestureType.Pull)
                    {
                        touchObj.OnPull(g);
                    }
                    else if (g.GameGesture == GestureObject.GameGestureType.Drag)
                    {
                        touchObj.OnDrag(g);
                    }
                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(g.EndPosition);
                var hits = Physics.RaycastAll(ray);
                foreach (RaycastHit hit in hits)
                {
                    touchedObject = hit.transform.gameObject;
                    TouchObject touchObj = touchedObject.GetComponent<TouchObject>();
                    CommonFlags touchObjFlags = touchedObject.GetComponent<CommonFlags>();
                    if (touchObj != null && touchObjFlags.IsEnabled)
                    {
                        boundObjects.Add(g.Id, touchedObject);
                        if (g.GameGesture == GestureObject.GameGestureType.Tap)
                        {
                            touchObj.OnTap(g);
                        }
                        else if (g.GameGesture == GestureObject.GameGestureType.Pull)
                        {
                            touchObj.OnPull(g);
                        }
                        else if (g.GameGesture == GestureObject.GameGestureType.Drag)
                        {
                            touchObj.OnDrag(g);
                        }
                        break;
                    }
                }
            }
            if (g.GameGesture == GestureObject.GameGestureType.Tap)
            {
                remove.Add(g);
            }
            else if (g.GameGesture == GestureObject.GameGestureType.Pull)
            {
                remove.Add(g);
            }
        }


        foreach (GestureObject g in remove)
        {
            gestures.Remove(g.Id);
            boundObjects.Remove(g.Id);
        }
    }

    protected GestureObject ConvertToGestureObject(Touch t)
    {
        if (t.phase == TouchPhase.Began)
        {
            GestureObject g = new GestureObject();
            g.Initialize(GestureObject.GameGestureType.Hold, t.position, t.position, Time.time, t.fingerId);
            gestures.Add(g.Id, g);
            return g;
        }
        else
        {
            GestureObject g;
            gestures.TryGetValue(t.fingerId, out g);

            if (g != null)
            {
                if (t.phase == TouchPhase.Ended)
                {
					
                    if (g.GameGesture == GestureObject.GameGestureType.Hold || g.GameGesture == GestureObject.GameGestureType.None)
                    {
                        g.GameGesture = GestureObject.GameGestureType.Tap;
						
                    }
                    else if (g.GameGesture == GestureObject.GameGestureType.Drag)
                    {
                        g.GameGesture = GestureObject.GameGestureType.Pull;
                        g.EndPosition = t.position;
                   		
					} 

                }
                else if (t.phase == TouchPhase.Moved)
                {
                    if (g.GameGesture == GestureObject.GameGestureType.Hold ||
                        g.GameGesture == GestureObject.GameGestureType.Drag ||
                        g.GameGesture == GestureObject.GameGestureType.None)
                    {
                        g.GameGesture = GestureObject.GameGestureType.Drag;
                        g.EndPosition = t.position;
                    }
                }
                else if (t.phase == TouchPhase.Stationary)
                {
                    if (g.GameGesture == GestureObject.GameGestureType.Hold ||
                        g.GameGesture == GestureObject.GameGestureType.Drag ||
                        g.GameGesture == GestureObject.GameGestureType.None)
                    {
                        g.GameGesture = GestureObject.GameGestureType.Hold;
                        g.EndPosition = t.position;
                    }
                }
            }
            else
            {
                g = new GestureObject();
                g.Initialize(GestureObject.GameGestureType.None, t.position, t.position, Time.time, t.fingerId);
                gestures.Add(g.Id, g);
            }

            return g;
        }

    }

    private void ConvertMouseToGestureObject()
    {
        GestureObject mouseGesture;
        gestures.TryGetValue(-1, out mouseGesture);

        //print(mouseGesture);
        //print("ID: " + mouseGesture.Id + " Gesture: " + mouseGesture.GameGesture);

        if (Input.GetMouseButton(0))
        {
            if (mouseGesture == null)
            {
                mouseGesture = new GestureObject();
                mouseGesture.Initialize(GestureObject.GameGestureType.Hold, Input.mousePosition, Input.mousePosition, Time.time, -1);
                gestures.Add(mouseGesture.Id, mouseGesture);
            }
            else
            {
                if (!Input.mousePosition.Equals(mouseGesture.StartPosition))
                {
                    mouseGesture.GameGesture = GestureObject.GameGestureType.Drag;
                }
                else
                {
                    mouseGesture.GameGesture = GestureObject.GameGestureType.Hold;
                }
                mouseGesture.EndPosition = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (mouseGesture.GameGesture == GestureObject.GameGestureType.Drag)
            {
                mouseGesture.GameGesture = GestureObject.GameGestureType.Pull;
                mouseGesture.EndPosition = Input.mousePosition;
            }
            else if (mouseGesture.GameGesture == GestureObject.GameGestureType.Hold)
            {
                mouseGesture.GameGesture = GestureObject.GameGestureType.Tap;
                mouseGesture.EndPosition = Input.mousePosition;
            }
        }
        //else
        //{
        //    gestures.Remove(-1);
        //}



    }
}
