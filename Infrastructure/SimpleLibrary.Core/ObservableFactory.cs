using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace SimpleLibrary.Core
{
    public static class ObservableFactory
    {
        public static IObservable<T> Create<T>(Func<T> func)
        {
            return Observable.Create<T>(observer =>
            {
                try
                {
                    observer.OnNext(func.Invoke());
                }
                catch (Exception ex)
                {
                    observer.OnError(ex);
                }
                finally
                {
                    observer.OnCompleted();
                }

                return Disposable.Empty;
            });
        }
    }
}