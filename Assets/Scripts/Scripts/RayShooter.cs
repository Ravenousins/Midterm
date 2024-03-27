using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{

    private Camera cam;
    private Coroutine currentCoroutine;
    public PauseMenu pauseMenu;

    public AudioClip gunfireSound;
    private AudioSource gunfireAudioSource;



    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();

        GameObject gunfireObject = new GameObject("GunfireAudio");
        gunfireObject.transform.parent = transform;
        gunfireAudioSource = gunfireObject.AddComponent<AudioSource>();
        gunfireAudioSource.volume = 0.15f; 
        gunfireAudioSource.pitch = 1.0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
                gunfireAudioSource.PlayOneShot(gunfireSound);
                Ray ray = cam.ScreenPointToRay(point);

                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                    if (target != null)
                    {
                        target.ReactToHit();

                    }
                    else
                    {
                        StartCoroutine(SphereIndicator(hit.point));

                    }

                }
            }
        }
    }
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.GetComponent<Renderer>().material.color = Color.black; 

        sphere.transform.localScale = Vector3.one * 0.1f;
        sphere.transform.position = pos;

        yield return new WaitForSeconds(0.5f);

        Destroy(sphere);
    }

    private void OnGUI()
    {
        if (PauseMenu.isGameActive)
        {
            int size = 12;

            float posX = cam.pixelWidth / 2 - size / 4;
            float posY = cam.pixelHeight / 2 - size / 2;

            GUI.Label(new Rect(posX, posY, size, size), "*");
        }
    }
  

}
