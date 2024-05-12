using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextMovement : MonoBehaviour
{

    private RectTransform m_RectTransform;

    [SerializeField]
    private float _upSpeed;

    [SerializeField]
    private bool _isLastCredit;

    public UnityEvent OnCreditEnd;

    void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        TextUpMovement();
        if (m_RectTransform.anchoredPosition.y > 200f)
        {
            if (_isLastCredit)
            {
                OnCreditEnd?.Invoke();
            }
            gameObject.SetActive(false);
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
