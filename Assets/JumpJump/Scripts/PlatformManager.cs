using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformDirection
{
    Left, 
    Right
}

[Serializable]
public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _platformPrefabs; // List of platform prefabs that can be instantiated

    [SerializeField]
    List<GameObject> _activePlatforms; // Platforms currently visible in the game

    [SerializeField]
    private int _maxPlatformCount = 6; // Maximum number of platforms to exist at a time.

    [SerializeField] float minPlatformSize = 1f;
    [SerializeField] float maxPlatformSize = 2f; 

    private GameObject _currentPlatform; // The platform player is currently on
    private GameObject _nextPlatform;  // The platform that the player will jump to
    private PlatformDirection _nextPlatformDirection; // Either left or right 

    private void Awake()
    {
        _currentPlatform = GeneratePlatform();
        _nextPlatform = GeneratePlatform();
    }

    private void Update()
    {
#if UNITY_EDITOR
        // For debugging, comment out when player is intergrated
        //if (Input.GetKeyDown("space"))
        //{
        //    JumpLanded();
        //}
#endif
    }

    /// <summary>
    /// Called by after the player ends the jump animation
    /// </summary>
    public void JumpLanded()
    {
        _currentPlatform = _nextPlatform;
        _nextPlatform = GeneratePlatform();
        DeinstantiateOldPlatform();
    }

    /// <summary>
    /// Called by after player ends the jump animation, checks if the player landed perfectly 
    /// </summary>
    /// <param name="playerPos"></param>
    /// <returns></returns>
    public bool CheckPerfectLanding(Vector3 playerPos)
    {
        return (Math.Abs(playerPos.x - _nextPlatform.transform.position.x) < 0.15f) &&
            (Math.Abs(playerPos.z - _nextPlatform.transform.position.z) < 0.15f);
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns> The world position of the next platform to jump to </returns>
    public Vector3 GetNextPlatformPosition()
    {
        if (_nextPlatform == null)
        {
            Debug.LogError("Next platform is null. Check if the next platform has been instantiated by calling GeneratePlatform and set");
        }
        return _nextPlatform.transform.position;
    }

    public float GetNextPlatformRadius()
    {
        if (_nextPlatform == null)
        {
            Debug.LogError("Next platform is null. Check if the next platform has been instantiated by calling GeneratePlatform and set");
        }


        return _nextPlatform.transform.localScale.x / 2; 
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns> The direction of the next platform (left or right) </returns>
    public PlatformDirection GetNextPlatformDirection()
    {
        return _nextPlatformDirection; 
    }

    /// <summary>
    /// Creates a platform in either left or right of the platform
    /// </summary>
    private GameObject GeneratePlatform()
    {
        bool isGeneratingOnLeft = (UnityEngine.Random.value < 0.5);
        // Set the direction of the direction of the next platform 
        // Player uses this info for aligning to the platform and changing thd direction it is facing 
        _nextPlatformDirection = (isGeneratingOnLeft) ? PlatformDirection.Left : PlatformDirection.Right;
        
        

        Vector3 newPlatformPosition;
        if (_currentPlatform is null) // No platform has been generated so far
        {
            newPlatformPosition = transform.position;
        } else // Generating new platform based on current platform position
        {
            newPlatformPosition = _currentPlatform.transform.position;
            float distance = 3;
            if (isGeneratingOnLeft)
            {
                newPlatformPosition += _currentPlatform.transform.forward * distance;
            }
            else
            {
                newPlatformPosition += _currentPlatform.transform.right * distance;
            }
        }

        GameObject newPlatform = GameObject.Instantiate(_platformPrefabs[0], newPlatformPosition, Quaternion.identity);

        newPlatform.GetComponent<MeshRenderer>().material.color =
             new Color(UnityEngine.Random.Range(0F, 1F), UnityEngine.Random.Range(0, 1F), UnityEngine.Random.Range(0, 1F));
        // Randomize the size of the platform by adjusting its radius.
        // The platform's default scale assumes a radius of 1, which is then scaled to a random value between minPlatformRadius and maxPlatformRadius.
        float platformSize = UnityEngine.Random.Range(minPlatformSize, maxPlatformSize);
        newPlatform.transform.localScale = new Vector3(platformSize, 1, platformSize);

#if UNITY_EDITOR
        PlatformDebug platformDebug = newPlatform.GetComponent<PlatformDebug>();
        platformDebug.SetPlatformRadius(platformSize / 2);
#endif

        _activePlatforms.Add(newPlatform);

        return newPlatform;
    }

    /// <summary>s
    /// Deinstantiates the platform that is out of view
    /// </summary>
    private void DeinstantiateOldPlatform()
    {
        while (_activePlatforms.Count > _maxPlatformCount)
        {
            GameObject platformToRemove = _activePlatforms[0];
            _activePlatforms.RemoveAt(0);
            Destroy(platformToRemove);
        }
    }
}
