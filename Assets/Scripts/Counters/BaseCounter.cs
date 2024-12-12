using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {

    public static event EventHandler OnAnyObjectPlacedHere;

    public static void ResetStaticData() {
        OnAnyObjectPlacedHere = null;
    }

    [SerializeField] protected Transform counterTopPoint;

    protected KitchenObject kitchenObject;

    public virtual void Interact(Player player) {
        Debug.LogError("BaseCounter.Interact();");
    }

    public virtual void InteractAlternate(Player player) {
        //Debug.LogError("BaseCounter.InteractAlternate();");
    }

    public virtual Transform GetKitchenObjectFollowTranform() {
        return counterTopPoint;
    }

    public virtual void SetKitchenObject(KitchenObject kitchenObject) {
        this.kitchenObject = kitchenObject;

        if (kitchenObject != null) {
            OnAnyObjectPlacedHere?.Invoke(this, EventArgs.Empty);
        }
    }

    public virtual KitchenObject GetKitchenObject() {
        return kitchenObject;
    }

    public virtual void ClearKitchenObject() {
        kitchenObject = null;
    }

    public virtual bool HasKitchenObject() {
        return kitchenObject != null;
    }
}