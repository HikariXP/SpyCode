/*
 * Author: CharSui
 * Created On: 2024.02.15
 * Description: �������ֵİ�ť
 */
using UnityEngine;
using UnityEngine.UI;

public class BtnDecodeNumber : MonoBehaviour
{
    //���������
    [SerializeField]
    private int _inputNumber;

    //�ϲ�����
    [SerializeField]
    private PnlDecode _pnlDecode;
    
    //��ť������
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
