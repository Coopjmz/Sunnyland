using UnityEngine;

namespace MainMenu
{
    class Camera: MonoBehaviour
    {
        //Variables (initialized from Unity)
        [SerializeField] float end = 187f;

        //Variables
        float start, unitPerSecond;

        //Methods
        void Start()
		{
            //Initialize variables
            start = transform.position.x;
            unitPerSecond = (end - start) / GetComponent<AudioSource>().clip.length;
        }

        void Update()
        {
            //Update camera speed
            float speed = Time.deltaTime * unitPerSecond;

            if(transform.position.x < end)
                //Move camera right
                transform.Translate(Vector3.right * speed);
            //Reset camera
            else transform.position = new Vector3(start, transform.position.y, -10f);
        }
    }
}