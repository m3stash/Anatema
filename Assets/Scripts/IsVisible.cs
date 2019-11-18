using UnityEngine;
public class IsVisible : MonoBehaviour {
    private Renderer m_Renderer;
    public Chunk chunk;
    public Camera cam;
    void Start() {
        m_Renderer = GetComponent<Renderer>();
    }

    void Update() {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        if(GeometryUtility.TestPlanesAABB(planes, m_Renderer.bounds)) {
            chunk.ChunckVisible(true);
        } else {
            chunk.ChunckVisible(false);
        }
    }

}