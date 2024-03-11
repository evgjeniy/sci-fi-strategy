namespace SustainTheStrain.Utils
{
    public class Observable<TValue> : IObservable<TValue>, System.IEquatable<Observable<TValue>>
    {
        private TValue _value;
        private readonly (ObservableCondition condition, IfFalseCondition ifNot)[] _conditions;
        
        public event System.Action<TValue> Changed;
        
        public delegate bool ObservableCondition(TValue value);
        public delegate void IfFalseCondition(ref TValue value);

        public Observable() => _conditions = System.Array.Empty<(ObservableCondition condition, IfFalseCondition ifNot)>();
        public Observable(params (ObservableCondition condition, IfFalseCondition ifNot)[] conditions) => _conditions = conditions;

        public TValue Value
        {
            get => _value;
            set
            {
                if (_value.Equals(value)) return;

                foreach (var condition in _conditions)
                {
                    if (!condition.condition(value) || condition.ifNot is null) continue;

                    condition.ifNot.Invoke(ref _value);
                    Changed?.Invoke(_value);

                    return;
                }

                _value = value;
                Changed?.Invoke(_value);
            }
        }
        
        public static implicit operator Observable<TValue>(TValue value) => new() { _value = value };
        public static explicit operator TValue(Observable<TValue> observable) => observable._value;
        public static bool operator ==(Observable<TValue> arg1, Observable<TValue> arg2) => arg1 != null && arg1.Equals(arg2);
        public static bool operator !=(Observable<TValue> arg1, Observable<TValue> arg2) => arg1 != null && !arg1.Equals(arg2);

        public bool Equals(Observable<TValue> other) => other != null && other._value.Equals(_value);
        public override bool Equals(object other) => other is Observable<TValue> observable && observable._value.Equals(_value);
        public override int GetHashCode() => _value.GetHashCode();
        public override string ToString() => _value.ToString();
    }
}