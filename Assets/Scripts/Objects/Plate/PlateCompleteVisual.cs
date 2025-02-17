using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour {


    [Serializable]
    public struct KitchenObjectSO_GameObject {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjects;

    public void Start() {
        plateKitchenObject.OnIngredientAdded += OnIngredientAdded;
    }

    private void OnIngredientAdded(object sender, PlateKitchenObject.IngredientAddedEventArgs e) {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjects) {
            if (kitchenObjectSOGameObject.kitchenObjectSO == e.kitchenObjectSO)
                kitchenObjectSOGameObject.gameObject.SetActive(true);
        }
    }
}
