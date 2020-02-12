using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellContactPoint : MonoBehaviour {

    [Header("Don't touch it")]
    [SerializeField] private ItemColliderCell associatedCell;
    [SerializeField] private ContactType contactType;
    [SerializeField] private new SpriteRenderer renderer;

    private void Awake() {
        this.associatedCell = GetComponentInParent<ItemColliderCell>();
        this.renderer = GetComponentInParent<SpriteRenderer>();
    }

    private void OnEnable() {
        this.contactType = ContactType.NONE;
        this.RefreshUI();
    }

    public void Select() {
        switch(this.contactType) {
            case ContactType.NONE:
                this.contactType = ContactType.SIMPLE;
                break;

            case ContactType.SIMPLE:
                this.contactType = ContactType.MANDATORY;
                break;

            case ContactType.MANDATORY:
                this.contactType = ContactType.NONE;
                break;
        }

        this.RefreshUI();
    }

    public ContactType GetContactType() {
        return this.contactType;
    }

    private void RefreshUI() {
        switch (this.contactType) {
            case ContactType.NONE:
                this.renderer.color = new Color(1, 1, 1);
                break;

            case ContactType.SIMPLE:
                this.renderer.color = new Color(0, 1, 0);
                break;

            case ContactType.MANDATORY:
                this.renderer.color = new Color(1, 0, 0);
                break;
        }
    }
}

[System.Serializable]
public enum ContactType
{
    NONE,
    SIMPLE,
    MANDATORY
}
