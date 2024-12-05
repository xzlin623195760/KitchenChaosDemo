using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
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