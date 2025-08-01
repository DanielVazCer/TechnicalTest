using UnityEngine;

public class SCR_SpriteAnimator : MonoBehaviour
{
  
    public Sprite[] frames;                   
    public float frameRate = 12f;             
    public bool loop = true;                  

    public SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;
    private bool isPlaying = true;

    private void Start()
    {
        

        if (frames.Length == 0)
        {
           
            isPlaying = false;
        }

        currentFrame = 0;
        timer = 0f;
        spriteRenderer.sprite = frames[currentFrame];
    }

    private void Update()
    {
        if (!isPlaying || frames.Length == 0)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f / frameRate)
        {
            timer -= 1f / frameRate;
            currentFrame++;

            if (currentFrame >= frames.Length)
            {
                if (loop)
                    currentFrame = 0;
                else
                {
                    currentFrame = frames.Length - 1;
                    isPlaying = false;
                }
            }

            spriteRenderer.sprite = frames[currentFrame];
        }
    }

    public void Play()
    {
        isPlaying = true;
        currentFrame = 0;
        timer = 0f;
    }

    public void Stop()
    {
        isPlaying = false;
    }
}
