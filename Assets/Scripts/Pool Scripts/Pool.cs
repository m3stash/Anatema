using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T>: MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected List<T> availableObjects;
    [SerializeField] protected List<T> usedObjects;

    protected T prefab;

    private void Awake() {
        this.availableObjects = new List<T>();
        this.usedObjects = new List<T>();
    }

    public void Setup(T prefab, int size) {
        this.prefab = prefab;

        // Add our first object to available list
        this.availableObjects.Add(this.prefab);

        // Fill our pool in function of the size
        for(int i = 1; i < size; i++) {
            T obj = Instantiate(this.prefab, this.transform);
            obj.gameObject.SetActive(false);
            this.availableObjects.Add(obj);
        }
    }

    public virtual T GetOne() {
        T obj = null;

        if (this.availableObjects.Count > 0) {
            obj = this.availableObjects[this.availableObjects.Count - 1];
            this.availableObjects.RemoveAt(this.availableObjects.Count - 1);
        } else {
            obj = Instantiate(this.prefab, this.transform);
        }

        this.usedObjects.Add(obj);

        obj.gameObject.SetActive(true);

        return obj;
    }

    public void ReturnObject(T pooledObject) {
        this.usedObjects.Remove(pooledObject);
        this.availableObjects.Add(pooledObject);

        // Reparent the pooled object to us, and disable it.
        var pooledObjectTransform = pooledObject.transform;
        pooledObjectTransform.parent = transform;
        pooledObjectTransform.localPosition = Vector3.zero;
        pooledObject.gameObject.SetActive(false);
    }
}
