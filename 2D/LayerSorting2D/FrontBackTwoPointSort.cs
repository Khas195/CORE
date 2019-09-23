using UnityEngine;

public class FrontBackTwoPointSort : IFrontBackSorting
{
    [SerializeField]
    Transform leftPoint = null;
    [SerializeField]
    Transform rightPoint = null;
    private void Start() {
        if (useSelfAsHost) {
            host = this.transform;
        }
    }
    public override bool IsAboveCharacter(Vector3 characterPos, Vector3 hostPos)
    {
        Debug.Log("Character position:" + characterPos);
        Debug.Log("Left Pivot: " + leftPoint.position);
        Debug.Log("Right Pivot: " + rightPoint.position);
        Debug.Log("Host Position: " + host.position);
        if (IsLeftOfPivot(characterPos))
        {
            return characterPos.y > leftPoint.position.y;
        }
        else
        {
            return characterPos.y > rightPoint.position.y;
        }
    }

    private bool IsLeftOfPivot(Vector3 characterPos)
    {
        return characterPos.x < host.position.x;
    }

    public override bool IsBelowCharacter(Vector3 characterPos, Vector3 hostPos)
    {
        if (IsLeftOfPivot(characterPos))
        {
            return characterPos.y < leftPoint.position.y;
        }
        else
        {
            return characterPos.y < rightPoint.position.y;
        }
    }
}
