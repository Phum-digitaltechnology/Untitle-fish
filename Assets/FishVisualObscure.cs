using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FishVisualObscure : MonoBehaviour
{
    [SerializeField] float hitCd = 0.1f;
    [SerializeField] int FishHealth;
    [SerializeField] UnityEvent onFishHit;
    [SerializeField] UnityEvent onDied;
    [SerializeField] RectTransform rect;
    [SerializeField] Transform Destination;
    [SerializeField] List<Color> fishColor = new List<Color>();

    public void SetUP()
    {
        Destination.transform.SetParent(null);
        this.GetComponent<UnityEngine.UI.Image>().color = fishColor[Random.Range(0, fishColor.Count)];
    }
    bool isDied = false;

    float hitTime = 0;
    bool canHit()
    {
        return Time.time > hitTime;
    }

    bool canDied = false;
    public void OnClick()
    {
        if (canHit() == false) return;
        if (isDied) return;
        hitTime = Time.time + hitCd;
        FishHealth--;
        if (FishHealth <= 0)
        {
            isDied = true;
            onDied?.Invoke();
            StartCoroutine(delayDied());
        }
        else
        {
            onFishHit?.Invoke();
        }
    }

    IEnumerator delayDied()
    {
        yield return new WaitForSeconds(0.5f);
        canDied = true;
    }

    private void Update()
    {
        if (IsUIOutOfScreen(rect) && canDied)
        {
            Destroy(this.gameObject);
        }
    }

    bool IsUIOutOfScreen(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        int outCount = 0;
        foreach (Vector3 corner in corners)
        {
            Vector3 screenPoint = RectTransformUtility.WorldToScreenPoint(null, corner);

            if (screenPoint.x < 0 || screenPoint.x > Screen.width ||
                screenPoint.y < 0 || screenPoint.y > Screen.height)
            {
                outCount++;
            }
        }

        return outCount == 4; // Only return true if all 4 corners are out of screen

    }

}
