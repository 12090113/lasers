using UnityEngine;

public class Mirror : MonoBehaviour
{
    void Start()
    {
        transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 6) * 60);
    }
    public void Rotate()
    {
        transform.eulerAngles = new Vector3(0,0,transform.eulerAngles.y+60);
    }
}
