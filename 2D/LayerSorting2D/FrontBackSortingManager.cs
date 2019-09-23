using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontBackSortingManager : MonoBehaviour
{
    [SerializeField]
    List<IFrontBackSorting> sortingObjects = new List<IFrontBackSorting>();
    [SerializeField]
    Transform character = null;
    // Start is called before the first frame update
    void Start()
    {
        var sortingList = GameObject.FindObjectsOfType<IFrontBackSorting>();
        foreach (var sortingTarget in sortingList)
        {
            sortingObjects.Add(sortingTarget);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var target in sortingObjects)
        {
            var pos = target.HostPosition();

            if (target.IsAboveCharacter(character.position, pos))
            {
                pos.z = character.position.z - 1;
            }
            else if (target.IsBelowCharacter(character.position, pos))
            {
                pos.z = character.position.z + 1;
            }
            else
            {
                pos.z = character.position.z;
            }
            target.SetHostPosition(pos);
        }
    }

}
