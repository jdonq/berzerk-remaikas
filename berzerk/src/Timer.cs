//using NUnit.Framework;

namespace berzerk
{
    // [TestFixture(10)]
    // [TestFixture(5)]
    // [TestFixture(0)]
    // [TestFixture(-1)]
    class Timer
    {
        private float framesCounter = 0f;
        private int frames = 0;

        public Timer(int seconds)
        {
            frames = seconds * 60;
            // validateTime();
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

        // [Test]
        // public void validateTime(){
        //     if(frames <= 0)
        //     {
        //         frames = 60;
        //     }
        //     Assert.GreaterOrEqual(frames, 60);
        // }
    }
}
