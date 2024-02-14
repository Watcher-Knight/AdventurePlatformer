using System;

public class InputBuffer
{
    public Func<bool> Action;
    private float CurrentTime = 0f;

    public void Invoke(float waitTime)
    {
        CurrentTime = waitTime;
    }

    public void Update(float deltaTime)
    {
        if (CurrentTime > 0)
        {
            CurrentTime -= deltaTime;
            if (Action()) CurrentTime = 0;
        }
    }
}