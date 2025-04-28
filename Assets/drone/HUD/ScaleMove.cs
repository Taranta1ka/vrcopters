using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleMove : MonoBehaviour
{
     public Transform drone;          // Объект дрона
    public RectTransform scaleRect; // RectTransform движущейся шкалы
    public float scaleSpeed = 50f;  // Пикселей на метр высоты
    public float maxVisibleHeight = 10000f; // Макс. отображаемая высота

    private Vector2 _initialPosition;

    void Start()
    {
        _initialPosition = scaleRect.anchoredPosition;
    }

    void Update()
    {
        // Вычисляем смещение шкалы (чем выше дрон, тем ниже опускается шкала)
        float heightOffset = -(drone.position.y-0.15f) * scaleSpeed;
        
        // Ограничиваем диапазон
        heightOffset = Mathf.Clamp(heightOffset, -maxVisibleHeight, maxVisibleHeight);
        
        // Применяем позицию
        scaleRect.anchoredPosition = _initialPosition + new Vector2(heightOffset, 0);
        
        // Для бесконечной шкалы (опционально):
        // if (scaleRect.anchoredPosition.y < -maxVisibleHeight) 
        //     scaleRect.anchoredPosition += Vector2.up * maxVisibleHeight * 2;
    }
}
