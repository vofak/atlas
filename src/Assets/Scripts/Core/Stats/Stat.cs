using UnityEngine;

namespace Atlas.Core
{
	/**
		This class holds all necessary information about a generic stat.
	*/
    [System.Serializable]
    public abstract class Stat
    {

		// Callback that is invoked on stat's depletion.
        public delegate void OnChanged(int curr, int max);
        public event OnChanged OnChangedCallback;

        [SerializeField] protected int currentValue;
        [SerializeField] protected int maxValue;

        public Stat(int maxValue)
        {
            this.maxValue = maxValue;
            this.currentValue = maxValue;
        }

        public Stat(int maxValue, int currentValue)
        {
            this.maxValue = maxValue;
            this.currentValue = currentValue;
        }

        public void Increase(int value)
        {
            int targetValue = currentValue + value;
            currentValue = targetValue > maxValue ? maxValue : targetValue;

            if (OnChangedCallback != null)
            {
                OnChangedCallback.Invoke(currentValue, maxValue);
;           }
        }

        public void ChangeMaxValue(int value)
        {
            maxValue = maxValue + value;

            if (maxValue < 1)
            {
                maxValue = 1;
            }

            if (OnChangedCallback != null)
            {
                OnChangedCallback.Invoke(currentValue, maxValue);
;           }
        }

        public int GetCurrentValue()
        {
            return currentValue;
        }

        public int GetMaxValue()
        {
            return maxValue;
        }

        public bool Decrease(int value)
        {
            bool ret = DecreasePrivate(value);

            if (OnChangedCallback != null)
            {
                OnChangedCallback.Invoke(currentValue, maxValue);
            }

            return ret;

        }

		/**
			This method allows for each individual stat (derived from this class) to
			implement its own behavior when it is supposed to decrease.
		*/
        public abstract bool DecreasePrivate(int value);

    }
}