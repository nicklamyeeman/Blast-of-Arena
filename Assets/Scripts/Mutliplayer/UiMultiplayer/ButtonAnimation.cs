using UnityEngine;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour
{
    private Text _text;
    [SerializeField]
    private Animation _anim;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseOver()
    {
        if (_anim)
        {
            print("lol");
            _anim.Play();
            _anim.gameObject.SetActive(true);
        }
    }

    public void OnMouseExit()
    {
        _anim.gameObject.SetActive(false);
    }
}
