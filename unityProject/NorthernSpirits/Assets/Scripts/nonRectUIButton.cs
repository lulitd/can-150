using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform), typeof(PolygonCollider2D))]
public class nonRectUIButton : MonoBehaviour, ICanvasRaycastFilter
{

    PolygonCollider2D mCollider;
    RectTransform rectTransform;
    Vector2[] polygonPoints;

    void Awake()
    {

        mCollider = GetComponent<PolygonCollider2D>();

        rectTransform = GetComponent<RectTransform>();

        polygonPoints = mCollider.GetPath(0);

    }

    public bool IsRaycastLocationValid(Vector2 screenPos, Camera eventCamera)
    {

        Vector2 worldLoc = Vector3.zero;

        // check if its inside the rect of the ui element
        bool isInside = RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            screenPos,
            eventCamera,
            out worldLoc);


        /*  if we are inside the rect then check
         *  if the point is inside the collider. This allows for custom button shapes.
         */
        if (isInside) isInside = PointInsidePolygon(worldLoc);

        return isInside;
    }


    private bool PointInsidePolygon(Vector2 point)
    {
        if (polygonPoints.Length < 3) return false; // invalid polygon

        int j = polygonPoints.Length - 1;
        bool inside = false;
        for (int i = 0; i < polygonPoints.Length; j = i++)
        {
            if (((polygonPoints[i].y <= point.y && point.y < polygonPoints[j].y) || (polygonPoints[j].y <= point.y && point.y < polygonPoints[i].y)) &&
               (point.x < (polygonPoints[j].x - polygonPoints[i].x) * (point.y - polygonPoints[i].y) / (polygonPoints[j].y - polygonPoints[i].y) + polygonPoints[i].x))
                inside = !inside;
        }

        return inside;

    }
}
