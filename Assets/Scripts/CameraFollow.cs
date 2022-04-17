using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
    public Vector3 cameraViewOffset = new Vector3(-5f, 10f, -4f);
    public float panDelay = 1f;
    public float panDuration = 1f;
    private Vector3 exit;
	private Transform player;
    private enum State
    {
        PAN_FROM_EXIT,
        TRACK_PLAYER,

        COUNT, // always keep me last
    };
    private State state;
    private float timeElapsed = 0f;
    private bool acquiredFollowTargets = false;

    void Start()
    {
        state = State.PAN_FROM_EXIT;
    }

    void Update()
    {
        if (!acquiredFollowTargets)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            GameObject map = GameObject.FindGameObjectWithTag("Map");
            if (map != null)
            {
                exit = map.GetComponent<GeneratorBehaviour>().physicalMap.GetEndPosition();
            }
            acquiredFollowTargets = true;
        }
    }

	void FixedUpdate()
	{
        timeElapsed += Time.deltaTime;

        switch (state)
        {
            case State.PAN_FROM_EXIT:
                if (acquiredFollowTargets)
                {
                    float t = Mathf.Clamp((timeElapsed - panDelay)/panDuration, 0, 1);
                    transform.position = Vector3.Lerp(exit + cameraViewOffset, player.position + cameraViewOffset, -t * (t-2));
                    if (timeElapsed > panDelay + panDuration)
                    {
                        state = State.TRACK_PLAYER;
                    }
                }
                break;
            case State.TRACK_PLAYER:
                if (player != null)
                {
                    transform.position = player.position + cameraViewOffset;
                }
                break;
        }
	}
}
