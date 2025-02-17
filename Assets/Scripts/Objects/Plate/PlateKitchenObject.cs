using System.Collections.Generic;
using UnityEngine;
using System;

public class PlateKitchenObject : KitchenObject {

    public event EventHandler<IngredientAddedEventArgs> OnIngredientAdded;
    public class IngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectSO> vaildkitchenObjectSOList;
    private List<KitchenObjectSO> kitchenObjectSOList;

    public void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        // if the ingredient is not valid or already added
        if (!vaildkitchenObjectSOList.Contains(kitchenObjectSO)) return false;

        // if the ingredient is already added
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) return false;

        kitchenObjectSOList.Add(kitchenObjectSO);

        OnIngredientAdded?.Invoke(this, new IngredientAddedEventArgs { kitchenObjectSO = kitchenObjectSO });

        return true;
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }
}
