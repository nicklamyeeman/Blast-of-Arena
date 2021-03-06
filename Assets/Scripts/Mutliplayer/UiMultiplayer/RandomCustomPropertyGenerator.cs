using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;

public class RandomCustomPropertyGenerator : MonoBehaviour
{
    [SerializeField]
    private Text _text;
    private Hashtable ouaip = new Hashtable();

    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();

    private void setCustomNumber()
    {
        System.Random rnd = new System.Random();
        int result = rnd.Next(0, 99);

        _text.text = result.ToString();

        _myCustomProperties["RandomNumber"] = result;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
        //PhotonNetwork.LocalPlayer.CustomProperties = _myCustomProperties;
    }

    public void OnClick_Button()
    {
        setCustomNumber();
    }

}
