using UnityEngine;
using TMPro;
using System.Collections;

public class SegmentNPCGuide : MonoBehaviour
{
    [Header("NPC Settings")]
    public string npcMessage = "Welcome to the Bike Segment!\nHop on your bike and race to the finish!";
    public float displayDuration = 4f;
    public float typingSpeed = 0.03f; // typewriter effect speed

    [Header("UI")]
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI npcNameText;
    public string npcName = "Coach";

    [Header("NPC Animator")]
    public Animator npcAnimator;
    public string idleAnimationTrigger = "Idle";
    public string talkAnimationTrigger = "Talk";

    private bool hasTriggered = false;
    private Coroutine displayCoroutine;

    [Header("Audio")]

    public AudioSource audioSource;
    
    public AudioClip voiceLine;

    private void Start()
    {
        if (messagePanel != null)
            messagePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        ;
        
        if (hasTriggered) return;

        NetworkPlayer np = other.GetComponentInParent<NetworkPlayer>();
        ;
        
        if (np == null || !np.IsOwner) return;

        hasTriggered = true;
        displayCoroutine = StartCoroutine(ShowMessage());
    }

    private IEnumerator ShowMessage()
    {
        if (messagePanel != null)
            messagePanel.SetActive(true);

        if (npcNameText != null)
            npcNameText.text = npcName;

        if (npcAnimator != null)
        {
            npcAnimator.ResetTrigger(idleAnimationTrigger);
            npcAnimator.SetTrigger(talkAnimationTrigger);
        }

        // Play voice line at the START so it plays while text types out
        if (audioSource != null && voiceLine != null)
            audioSource.PlayOneShot(voiceLine);

        // Typewriter effect
        if (messageText != null)
        {
            messageText.text = "";
            foreach (char c in npcMessage)
            {
                messageText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        yield return new WaitForSeconds(displayDuration);

        if (messagePanel != null)
            messagePanel.SetActive(false);

        if (npcAnimator != null)
        {
            npcAnimator.ResetTrigger(talkAnimationTrigger);
            npcAnimator.SetTrigger(idleAnimationTrigger);
        }
    }
}