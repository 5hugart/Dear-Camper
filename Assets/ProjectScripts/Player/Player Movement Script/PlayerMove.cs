using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerMove : MonoBehaviour
{
    public FixedJoystick joystick;
    public float SpeedMove = 5f;
    public float SprintSpeed = 8f;
    public FixedButton sprintButton;
    private CharacterController controller;

    private float Gravity = -9.81f;
    public float GroundDistance = 0.3f;
    public Transform Ground;
    public LayerMask layerMask;
    Vector3 velocity;
    public float jumpheight = 3f;
    public bool isGround;
    public bool Pressed;

    private AudioSource audioSource;
    [SerializeField] private AudioClip GrassFootstep;
    [SerializeField] private AudioClip GravelFootstep;
    [SerializeField] private AudioClip DirtFootstep;

    [SerializeField] private float walkTimerSound = 1f;
    [SerializeField] private float runTimerSound = 0.4f;
    private float footstepTimer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        isGround = Physics.CheckSphere(Ground.position, GroundDistance, layerMask);
        if (isGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        bool isSprinting = (sprintButton != null && sprintButton.Pressed) || Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isSprinting ? SprintSpeed : SpeedMove;

        Vector3 Move = transform.right * joystick.Horizontal + transform.forward * joystick.Vertical;
        controller.Move(Move * currentSpeed * Time.deltaTime);

        HandleFootsteps(Move.magnitude > 0.1f, isSprinting);

        if (isGround && Pressed)
        {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * Gravity);
            isGround = false;
        }

        velocity.y += Gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
void HandleFootsteps(bool isMoving, bool isSprinting)
{
    if (!isGround || !isMoving)
    {
        footstepTimer = 0f;
        if (audioSource.isPlaying) audioSource.Stop();
        return;
    }

    footstepTimer += Time.deltaTime;
    float interval = isSprinting ? runTimerSound : walkTimerSound;

    if (footstepTimer >= interval)
    {
        footstepTimer = 0f;
        AudioClip clip = GetSurfaceClip();
        if (clip != null) audioSource.PlayOneShot(clip, PlayerPrefs.GetFloat("SfxVolume", 1f));
    }
}

    AudioClip GetSurfaceClip()
    {
        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null) return DirtFootstep;

        TerrainData td = terrain.terrainData;
        Vector3 p = transform.position - terrain.transform.position;

        if (p.x < 0 || p.z < 0 || p.x > td.size.x || p.z > td.size.z) return DirtFootstep;

        int x = Mathf.Clamp((int)(p.x / td.size.x * td.alphamapWidth), 0, td.alphamapWidth - 1);
        int z = Mathf.Clamp((int)(p.z / td.size.z * td.alphamapHeight), 0, td.alphamapHeight - 1);

        float[,,] map = td.GetAlphamaps(x, z, 1, 1);
        int best = 0;
        float max = 0f;
        for (int i = 0; i < map.GetLength(2); i++)
        {
            if (map[0, 0, i] > max) { max = map[0, 0, i]; best = i; }
        }

        switch (best)
        {
            case 0:
            case 1:  return GrassFootstep;
            case 3:
            case 4:  return GravelFootstep;
            default: return DirtFootstep;
        }
    }
}