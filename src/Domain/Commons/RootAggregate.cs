namespace NetCoreHexagonal.Domain.Commons
{
    public class RootAggregate : Entity
    {
        private readonly List<IEvent> _events = new List<IEvent>();
        public IReadOnlyList<IEvent> Events => _events;

        public void RaiseEvent(IEvent @event)
        {
            _events.Add(@event);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
    }
}
