using UnityEngine;
public class IsVisible : MonoBehaviour {
    private Renderer m_Renderer;
    public Chunk chunk;
    public Camera cam;
    void Start() {
        m_Renderer = chunk.tilemapTile.GetComponent<Renderer>();
    }

    void Update() {
        /*Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        if(GeometryUtility.TestPlanesAABB(planes, m_Renderer.bounds)) {
            chunk.ChunckVisible(true);
        } else {
            chunk.ChunckVisible(false);
        }*/
        if (m_Renderer.isVisible) {
            chunk.ChunckVisible(true);
        } else {
            chunk.ChunckVisible(false);
        }
    }

}