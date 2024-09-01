using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextMovement : MonoBehaviour
{

    private RectTransform m_RectTransform;

    [SerializeField]
    private float _finalPoz;

    [SerializeField]
    private float _upSpeed;

    [SerializeField]
    private bool _isLastCredit;

    public UnityEvent OnCreditEnd;

    void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        if (m_RectTransform.anchoredPosition.y < _finalPoz)
        {
            TextUpMovement();
            if (_isLastCredit && m_RectTransform.anchoredPosition.y >= _finalPoz)
            {
                OnCreditEnd?.Invoke();
            }
        }

    }

    private void TextUpMovement()
    {
        if (m_RectTransform != null)
        {
            m_RectTransform.Translate(0f, _upSpeed, 0);
        }
    }
}
