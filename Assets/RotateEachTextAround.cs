using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class RotateEachTextAround : MonoBehaviour
{
    public float angleLimit = 30f;        // Max angle in either direction
    public float swingSpeed = 2f;         // Speed of swinging
    public float offsetPerChar = 0.2f;    // Delay offset per character
    public Vector3 rotationAxis = Vector3.up;

    private TMP_Text text;
    private TMP_TextInfo textInfo;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.ForceMeshUpdate();
        textInfo = text.textInfo;
    }

    void Update()
    {
        text.ForceMeshUpdate();
        textInfo = text.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            var charInfo = textInfo.characterInfo[i];
            if (!charInfo.isVisible) continue;

            int matIndex = charInfo.materialReferenceIndex;
            int vertIndex = charInfo.vertexIndex;

            Vector3[] verts = textInfo.meshInfo[matIndex].vertices;

            Vector3 center = (verts[vertIndex] + verts[vertIndex + 2]) / 2;

            // Calculate oscillating angle based on sine wave
            float angle = Mathf.Sin(Time.time * swingSpeed + i * offsetPerChar) * angleLimit;
            Quaternion rotation = Quaternion.AngleAxis(angle, rotationAxis);

            for (int j = 0; j < 4; j++)
            {
                verts[vertIndex + j] -= center;
                verts[vertIndex + j] = rotation * verts[vertIndex + j];
                verts[vertIndex + j] += center;
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
            text.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
