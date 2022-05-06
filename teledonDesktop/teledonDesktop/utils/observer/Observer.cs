using teledonCS.utils.observer.events;

namespace teledonCS.utils.observer
{
    public interface Observer<E>:Event
    {
        void update(E e);
    }
}