using teledonCS.utils.observer.events;

namespace teledonCS.utils.observer
{
    public interface Observable<E>:Event
    {
        void addObserver(Observer<E> e);
        void removeObserver(Observer<E> e);
        void notifyObserver(E t);

    }
}