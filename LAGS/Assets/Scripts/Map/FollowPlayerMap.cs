using UnityEngine;

public class FollowPlayerMap : MonoBehaviour
{
    [SerializeField] Transform _player;


    private void Update()
    {
        transform.position = new Vector3(_player.position.x , transform.position.y,_player.position.z);
    }
}
