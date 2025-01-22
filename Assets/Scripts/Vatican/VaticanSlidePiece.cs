using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaticanSlidePiece : MonoBehaviour
{
    public VaticanSlidePuzzle puzzle;
    public Vector2Int correctPosition;
    public Vector2Int currentPosition;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    public void OnClick()
    {
        audioSource.Play();
        puzzle.SlidePiece(this);
    }
}
