using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryToolbar : InventoryUI
{

    private int currentSelectedIdx;

    public static InventoryToolbar instance;

    protected override void Awake()
    {
        base.Awake();

        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void OnEnable()
    {
        this.SelectCurrentSlot();

        InputManager.controls.Toolbar.Navigate.performed += ctx => this.ManageMouseScroll(ctx.ReadValue<float>());
    }

    public ItemConfig GetSelectedItem()
    {
        return this.cells[this.currentSelectedIdx].GetInventoryItem()?.GetItem()?.GetConfig();
    }

    private void ManageMouseScroll(float value)
    {
        if (value > 0f)
        {
            this.UnSelectCurrentSlot();
            currentSelectedIdx = (currentSelectedIdx < this.cells.Length - 1) ? currentSelectedIdx + 1 : 0;
            this.SelectCurrentSlot();
        }
        else if (value < 0f)
        {
            this.UnSelectCurrentSlot();
            currentSelectedIdx = (currentSelectedIdx > 0) ? currentSelectedIdx - 1 : this.cells.Length - 1;
            this.SelectCurrentSlot();
        }
    }

    private void SelectCurrentSlot()
    {
        this.cells[currentSelectedIdx].Select();
    }

    private void UnSelectCurrentSlot()
    {
        this.cells[currentSelectedIdx].UnSelect();
    }
}
