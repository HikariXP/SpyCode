/*
 * Author: CharSui
 * Created On: 2024.02.15
 * Description: 输入数字的按钮
 */
using UnityEngine;
using UnityEngine.UI;

public class BtnDecodeNumber : MonoBehaviour
{
    //传入的数字
    [SerializeField]
    private int _inputNumber;

    //上部索引
    [SerializeField]
    private PnlDecode _pnlDecode;
    
    //按钮本身缓存
    private Button _button;

    private void Awake()
    {
        _button = gameObject.GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void OnClick()
    {
        _pnlDecode.OnNumberBtnClick(_inputNumber);
    }
}
