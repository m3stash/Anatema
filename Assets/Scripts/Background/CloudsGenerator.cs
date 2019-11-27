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

    private List<GameObject> activeClouds;
    private List<GameObject> availableClouds;

    private float maxDistance; // represend the size of camera on X axis

    // Start is called before the first frame update
    void Start() {
        this.activeClouds = new List<GameObject>();
        this.availableClouds = new List<GameObject>();

        StartCoroutine(this.StartGeneration());
    }

    private IEnumerator StartGeneration() {
        while (true) {
            if (this.activeClouds.Count < this.quantity) {
                this.GenerateCloud();
                yield return new WaitForSeconds(5);
            } else {
                yield return new WaitForSeconds(1);
            }
        }
    }

    private void GenerateCloud() {
        Sprite cloudSprite = this.cloudsSprites[Random.Range(0, this.cloudsSprites.Length)];
        Vector2 position = new Vector2(-this.offsetX, Random.Range(0f, this.height));
        float size = Random.Range(this.minSize, this.maxSize);
        GameObject cloud = null;

        if (this.availableClouds.Count > 0) {
            cloud = this.availableClouds[0];
            cloud.SetActive(true);
            this.availableClouds.RemoveAt(0);
        } else {
            cloud = Instantiate(this.cloudPrefab, position, Quaternion.identity, this.transform);
        }

        SpriteRenderer renderer = cloud.GetComponent<SpriteRenderer>();
        renderer.sprite = cloudSprite;
        renderer.flipX = Random.Range(0, 2) == 0;

        cloud.GetComponent<InfiniteMove>().SetSpeed(Random.Range(this.minSpeed, this.maxSpeed));

        cloud.transform.localScale = new Vector2(size, size);

        this.activeClouds.Add(cloud);
    }
}
