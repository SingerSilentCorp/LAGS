using NUnit.Framework;
using UnityEngine;

public class SpecialDoor : MonoBehaviour
{
    [SerializeField] GameObject[] _walls;
   public void ShowPlace()
    {
        foreach (var wall in _walls)
        {
            wall.layer = 0;
        }
    }
}
