using UnityEngine;

namespace Script
{
    public class Background : MonoBehaviour
    {
        public GameObject o;

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(transform.position.x, o.transform.position.y + 2);
        }
    }
}
