namespace Atlas.Core
{

	/**
		Health stat whose depletion means immediate death.
	*/
    [System.Serializable]
    public class Health : Stat
    {

        public delegate void OnDepletion();
        public event OnDepletion OnDepletionCallback;

        public Health(int maxValue) : base(maxValue) { }
        public Health(int maxValue, int currentValue) : base(maxValue, currentValue) { }

        public override bool DecreasePrivate(int value)
        {
            currentValue -= value;

            if (currentValue <= 0 && OnDepletionCallback != null)
            {
                currentValue = 0;
                OnDepletionCallback.Invoke();
            }

            return true;
        }

    }
}