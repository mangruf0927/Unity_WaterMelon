using System.Collections.Generic;

public interface ISubject
{
    void AddObserver<T>(List<T> observerList, T observer) where T : IObserver;
    void RemoveObserver<T>(List<T> observerList, T observer) where T : IObserver;
    void NotifyObservers<T>(List<T> observerList) where T : IObserver;
}

