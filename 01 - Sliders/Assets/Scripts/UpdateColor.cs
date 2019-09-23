using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UpdateColor : MonoBehaviour
{
    public Slider red, green, blue;
    public AudioSource source;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void UpdateImageColor()
    {
        image.color = new Color(red.value, green.value, blue.value);
        if (red.value == 1f && green.value == 1f && blue.value == 1f)
            source.enabled = true;
    }
}
