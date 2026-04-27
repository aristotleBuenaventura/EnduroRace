using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPreviewManager : MonoBehaviour
{
    [Header("Preview Scene Objects")]
    public GameObject maleFootModel;     // Male PlayeronFoot at X=1000
    public GameObject maleBikeModel;     // Male PlayeronBike at X=1000
    public GameObject male2FootModel;     // Male PlayeronFoot at X=1000
    public GameObject male2BikeModel;     // Male PlayeronBike at X=1000
    public GameObject femaleFootModel;   // Female PlayeronFoot at X=1000
    public GameObject femaleBikeModel;   // Female PlayeronBike at X=1000

    [Header("UI")]
    public RawImage previewDisplay;
    public TextMeshProUGUI characterNameText;
    public TextMeshProUGUI toggleButtonText;

    [Header("Selection Cards")]
    public GameObject maleCard;
    public GameObject male2Card;
    public GameObject femaleCard;
    public GameObject maleSelectedBorder;
    public GameObject male2SelectedBorder;
    public GameObject femaleSelectedBorder;

    [Header("Auto-rotate")]
    public float rotateSpeed = 30f;

    private string currentSelection = "Male";
    private bool isOnFoot = true;
    private GameObject activeModel;

    // Called explicitly by PlayerProfileManager after SetActive(true)
    // Do NOT use OnEnable — refs are not guaranteed live when panel is freshly activated
    public void InitPreview()
    {
        string saved = PlayerPrefs.HasKey("SelectedModel")
            ? PlayerPrefs.GetString("SelectedModel") : "Male";

        isOnFoot = true;
        UpdateToggleButtonText();
        ShowModel(saved, animate: false);
    }

    private void Update()
    {
        if (activeModel != null)
            activeModel.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
    }

    public void OnSelectMale()   => ShowModel("Male",   animate: true);
    public void OnSelectMale2() => ShowModel("Male2", animate: true);
    public void OnSelectFemale() => ShowModel("Female", animate: true);

    public void ToggleFootBike()
    {
        isOnFoot = !isOnFoot;
        UpdateToggleButtonText();
        ShowModel(currentSelection, animate: false);
    }

    private void ShowModel(string modelName, bool animate)
    {
        if (maleFootModel == null || maleBikeModel == null ||
            male2FootModel == null || male2BikeModel == null ||
            femaleFootModel == null || femaleBikeModel == null)
        {
            Debug.LogError("[CharacterPreview] One or more model references are missing! Check the Inspector.");
            return;
        }

        currentSelection = modelName;

        maleFootModel.SetActive(false);
        maleBikeModel.SetActive(false);
        male2FootModel.SetActive(false);
        male2BikeModel.SetActive(false);
        femaleFootModel.SetActive(false);
        femaleBikeModel.SetActive(false);

        if (modelName == "Male")
            activeModel = isOnFoot ? maleFootModel : maleBikeModel;
        else if (modelName == "Male2")
            activeModel = isOnFoot ? male2FootModel : male2BikeModel;  // ← was using male2BikeModel twice (bug)
        else if (modelName == "Female")
            activeModel = isOnFoot ? femaleFootModel : femaleBikeModel;

        activeModel.SetActive(true);
        activeModel.transform.rotation = Quaternion.identity;

        string activity = isOnFoot ? "Runner" : "Cyclist";
        characterNameText.text = $"{modelName} {activity}";  // ← was hardcoding "Male"/"Female" (bug)

        maleSelectedBorder.SetActive(modelName == "Male");
        male2SelectedBorder.SetActive(modelName == "Male2");    // ← was missing (bug)
        femaleSelectedBorder.SetActive(modelName == "Female");  // ← was using !isMale (bug, would light up for Male2 too)

        if (animate)
        {
            GameObject selectedCard = modelName == "Male"   ? maleCard
                                    : modelName == "Male2"  ? male2Card   // ← was missing (bug)
                                    : femaleCard;
            StartCoroutine(PunchScale(selectedCard.transform));
        }

        PlayerPrefs.SetString("SelectedModel", modelName);
    }

    private void UpdateToggleButtonText()
    {
        if (toggleButtonText != null)
            toggleButtonText.text = isOnFoot ? "Show Cycling" : "Show Running";
    }

    public string GetCurrentSelection() => currentSelection;

    private System.Collections.IEnumerator PunchScale(Transform t)
    {
        Vector3 original = t.localScale;
        Vector3 punched  = original * 1.12f;
        float   duration = 0.1f;
        float   elapsed  = 0f;

        while (elapsed < duration)
        {
            t.localScale = Vector3.Lerp(original, punched, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0f;
        while (elapsed < duration)
        {
            t.localScale = Vector3.Lerp(punched, original, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        t.localScale = original;
    }
}