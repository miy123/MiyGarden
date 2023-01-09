using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiyGarden.Service.Pattrens
{
    public class Topic : IObservable<Dto>
    {
        class Unsubscriber : IDisposable
        {
            private List<IObserver<Dto>> _observers;
            private IObserver<Dto> _observer;

            public Unsubscriber(List<IObserver<Dto>> observers, IObserver<Dto> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null) _observers.Remove(_observer);
            }
        }
        private List<IObserver<Dto>> _observers;
        private readonly int _testNumber;

        public Topic(int testNumber)
        {
            _observers = new List<IObserver<Dto>>();
            _testNumber = testNumber;
        }

        public IDisposable Subscribe(IObserver<Dto> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);

            return new Unsubscriber(_observers, observer);
        }

        public void GetData()
        {
            foreach (var ob in this._observers)
            {
                ob.OnNext(new Dto
                {
                    Content = "i am test" + this._testNumber,
                    TransTime = DateTime.Now
                });
            }
        }
    }

    public class PersonObserver : IObserver<Dto>
    {
        private readonly string _name;

        public PersonObserver(string name)
        {
            this._name = name;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Dto value)
        {
            Console.WriteLine(this._name);
            Console.WriteLine(value.TransTime.ToShortTimeString() + ":" + value.Content);
        }
    }

    public struct Dto
    {
        public object Content { set; get; }

        public DateTime TransTime { set; get; }

        public int TrackingId { set; get; }
    }

    public class ObserverPatternTest
    {
        public void Start()
        {
            var topic1 = new Topic(1);
            var topic2 = new Topic(2);

            var p1 = new PersonObserver("小名");
            var p2 = new PersonObserver("小王");
            var p3 = new PersonObserver("小華");

            topic1.Subscribe(p1);
            topic1.Subscribe(p2);

            topic2.Subscribe(p2);
            topic2.Subscribe(p3);

            topic1.GetData();
            topic2.GetData();
        }
    }
}
