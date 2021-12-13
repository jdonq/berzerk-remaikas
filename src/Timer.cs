
namespace berzerk
{
    class Timer
    {
        private float framesCounter = 0f;
        private int frames = 0;

        public Timer(int seconds)
        {
            frames = seconds * 60;
        }
        
        public bool UpdateTimer()
        {
            framesCounter++;

            if (((framesCounter/frames)%2) == 1)
            {
                framesCounter = 0;
                return true;
            }

            return false;
        }

        public void ResetTimer()
        {
            framesCounter = 0f;
        }
    }
}
