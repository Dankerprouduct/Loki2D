namespace Loki2D.Core.Utilities
{
    public class Timer
    {

        public float Delay { get; private set; }
        public float TimeRemaining { get; private set; }
        private bool _runOnce;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delay"></param>
        public Timer(float delay, bool runOnce = false)
        {
            _runOnce = runOnce;
            TimeRemaining = delay;
            Delay = delay;
        }

        /// <summary>
        /// returns true when the time resets
        /// </summary>
        /// <param name="deltaTime"></param>
        /// <returns></returns>
        public bool Update(float deltaTime)
        {
            TimeRemaining -= deltaTime;

            if (TimeRemaining <= 0)
            {
                if (!_runOnce)
                {
                    TimeRemaining = Delay;
                }

                return true;
            }

            return false;
        }

    }
}
