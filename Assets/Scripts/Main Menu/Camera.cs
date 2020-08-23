using UnityEngine;

namespace MainMenu
{
    class Camera: MonoBehaviour
    {
        //Variables (initialized from Unity)
        [SerializeField] float end = 187f;

        //Variables
        float start, unitsPerSecond;

        //Methods
        void Start()
		{
            //Initialize variables
            start = transform.position.x;
            unitsPerSecond = (end - start) / GetComponent<AudioSource>().clip.length;
        }

        void Update()
        {
            if(transform.position.x < end)
                //Move camera right
                transform.Translate(Vector3.right * Time.deltaTime * unitsPerSecond);
            //Reset camera
            else transform.position = new Vector3(start, transform.position.y, -10f);
        }
    }
}