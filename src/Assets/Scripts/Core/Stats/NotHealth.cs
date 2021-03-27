namespace Atlas.Core
{
	/**
		Generic "not health-like" stat whose depletion does not mean immediate death.
	*/
    [System.Serializable]
    public class NotHealth : Stat
    {

        public NotHealth(int maxValue) : base(maxValue) { }
        public NotHealth(int maxValue, int currentValue) : base(maxValue, currentValue) { }

        public override bool DecreasePrivate(int value)
        {
            bool ret = false;
            int targetValue = currentValue - value;

            if (targetValue >= 0)
            {
                currentValue = targetValue;
                ret = true;
            }

            return ret;
        }

    }
}