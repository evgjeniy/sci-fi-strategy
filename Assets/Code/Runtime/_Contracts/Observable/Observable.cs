namespace SustainTheStrain._Contracts
{
    public interface IObservable<out TValue>
    {
        event System.Action<TValue> Changed;
    }
    
    public class Observable<TValue> : IObservable<TValue>
    {
        private TValue _value;
        private event System.Action<TValue> OnChangedEvent = _ => {};

        public TValue Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChangedEvent(_value);
            }
        }
    
        public event System.Action<TValue> Changed
        {
            add
            {
                OnChangedEvent += value;
                value(_value);
            }
            remove => OnChangedEvent -= value;
        }
    }
}