using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsGenerator : MonoBehaviour {

    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private Sprite[] cloudsSprites;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    [SerializeField] private float height;
    [SerializeField] private int quantity;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    private Camera camera;
    private int halfCamera;
    private int cameraWidth;

    private List<GameObject> activeClouds;
    private List<GameObject> availableClouds;

    private float maxDistance; // represend the size of camera on X axis

    private void OnEnable() {
        WorldManager.GetPlayer += SetPlayer;
    }
    private void OnDisable() {
        WorldManager.GetPlayer -= SetPlayer;
    }
    void Start() {
        activeClouds = new List<GameObject>();
        availableClouds = new List<GameObject>();
        StartCoroutine(StartGeneration());
        StartCoroutine(CloudManager());
    }

    private IEnumerator CloudManager() {
        while (true) {
            int[] findIndex = new int[activeClouds.Count];
            var i = 0;
            activeClouds.ForEach(cloud => {
                findIndex[i] = -1;
                var cloudX = cloud.transform.localPosition.x;
                var widthCloud = GetWidthCloud(cloud);
                if (cloudX < -(halfCamera + widthCloud) || cloudX > halfCamera + widthCloud) {
                    cloud.SetActive(false);
                    availableClouds.Add(cloud);
                    findIndex[i] = i;
                }
                i++;
            });
            for (var j = 0; j < findIndex.Length; j++) {
                if (findIndex[j] > -1) {
                    activeClouds.RemoveAt(j);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }
    private void SetPlayer(GameObject player) {
        camera = player.GetComponentInChildren<Camera>();
        var rightCam = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
        var leftCam = camera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        cameraWidth = (int)(rightCam - leftCam);
        halfCamera = cameraWidth / 2;
    }

    private IEnumerator StartGeneration() {
        while (true) {
            if (activeClouds.Count < quantity) {
                GenerateCloud();
                yield return new WaitForSeconds(20);
            } else {
                yield return new WaitForSeconds(1);
            }
        }
    }

    private void GenerateCloud() {
        Sprite cloudSprite = cloudsSprites[Random.Range(0, cloudsSprites.Length)];
        Vector2 position = new Vector2(-offsetX, Random.Range(0f, height));
        GameObject cloud = null;
        if (availableClouds.Count > 0) {
            cloud = availableClouds[0];
            cloud.SetActive(true);
            availableClouds.RemoveAt(0);
        } else {
            cloud = Instantiate(cloudPrefab, position, Quaternion.identity, transform);
        }
        SpriteRenderer renderer = cloud.GetComponent<SpriteRenderer>();
        renderer.sprite = cloudSprite;
        renderer.flipX = Random.Range(0, 2) == 0;
        cloud.transform.localPosition = new Vector2(-(halfCamera + GetWidthCloud(cloud)), Random.Range(-7, 11));
        cloud.GetComponent<InfiniteMove>().SetSpeed(Random.Range(minSpeed, maxSpeed));
        activeClouds.Add(cloud);
    }

    private float GetWidthCloud(GameObject cloud) {
        return cloud.GetComponent<SpriteRenderer>().bounds.size.x;
    }
}
