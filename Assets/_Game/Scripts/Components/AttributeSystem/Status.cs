using UnityEngine.Events;

namespace Components.AttributeSystem
{
    public class Status
    {
        public Status(RawStatus rawStatusData)
        {
            Life = new Stat(rawStatusData.Life);
            Strength = new Stat(rawStatusData.Strength);
            AttackSpeed = new Stat(rawStatusData.AttackSpeed);
            Speed = new Stat(rawStatusData.Speed);

            onAnyStatChanged = new OnAnyStatChangedEvent();
            Life.OnStatChanged.AddListener(stat => onAnyStatChanged.Invoke(this));
            Strength.OnStatChanged.AddListener(stat => onAnyStatChanged.Invoke(this));
            AttackSpeed.OnStatChanged.AddListener(stat => onAnyStatChanged.Invoke(this));
            Speed.OnStatChanged.AddListener(stat => onAnyStatChanged.Invoke(this));
        }

        public Stat Life { get; }
        public Stat Strength { get; }
        public Stat AttackSpeed { get; }
        public Stat Speed { get; }
        public OnAnyStatChangedEvent onAnyStatChanged { get; }

        public void Add(RawStatus rawStatus)
        {
            Life.AddModifier(rawStatus.Life);
            Strength.AddModifier(rawStatus.Strength);
            AttackSpeed.AddModifier(rawStatus.AttackSpeed);
            Speed.AddModifier(rawStatus.Speed);
        }

        public void Remove(RawStatus rawStatus)
        {
            Life.RemoveModifier(rawStatus.Life);
            Strength.RemoveModifier(rawStatus.Strength);
            AttackSpeed.RemoveModifier(rawStatus.AttackSpeed);
            Speed.RemoveModifier(rawStatus.Speed);
        }

        public void Add(Status status)
        {
            Life.AddModifier(status.Life);
            Strength.AddModifier(status.Strength);
            AttackSpeed.AddModifier(status.AttackSpeed);
            Speed.AddModifier(status.Speed);
        }

        public void Remove(Status status)
        {
            Life.RemoveModifier(status.Life);
            Strength.RemoveModifier(status.Strength);
            AttackSpeed.RemoveModifier(status.AttackSpeed);
            Speed.RemoveModifier(status.Speed);
        }
    }

    public class OnAnyStatChangedEvent : UnityEvent<Status> { }
}