using UnityEngine;

public class DeliveryMangerUI : MonoBehaviour {
    [SerializeField] private Transform container;
    [SerializeField] private Transform recipeTemplate;

    private void Awake() {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        DeliveryManger.Instance.OnRecipeSpwaned += DeliveryManger_OnRecipeSpwaned;
        DeliveryManger.Instance.OnRecipeCompleted += DeliveryManger_OnRecipeCompleted;

        UpdateVisual();
    }

    private void DeliveryManger_OnRecipeSpwaned(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void DeliveryManger_OnRecipeCompleted(object sender, System.EventArgs e) {
        UpdateVisual();
    }

    private void UpdateVisual() {

        foreach (Transform child in container) {
            if (child == recipeTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSO in DeliveryManger.Instance.GetWaitingRecipeSOList()) {

            Transform recipeTransfrom = Instantiate(recipeTemplate, container);
            recipeTransfrom.gameObject.SetActive(true);
            recipeTransfrom.GetComponent<DeliveryMangerSingleUI>().SetRecipeSO(recipeSO);
        }


    }
}
