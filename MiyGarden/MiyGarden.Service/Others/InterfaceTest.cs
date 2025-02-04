using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MiyGarden.Service.Others
{
    public interface IMiyAble<out T> : IMiyAble, IQAQAble<T>, IQAQAble, IEnumerable<T>, IEnumerable
    {
        new IMiyAble<T> GetMiytor();
    }

    public interface IMiyAble : IQAQAble, IEnumerable
    {
        IMiyAble GetMiytor();
    }

    public interface IQAQAble<out T> : IQAQAble
    {
        new IQAQAble<T> GetQAQtor();
    }

    public interface IQAQAble
    {
        IQAQAble GetQAQtor();
    }

    public class MiyableQuery<T> : IMiyAble<T>
    {
        private readonly Expression _expression;
        private IEnumerable<T> _enumerable;
        private IQAQAble<T> _qaqable;
        private IMiyAble<T> _miyable;
        public MiyableQuery(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
            _expression = Expression.Constant(this);
            _qaqable = null;
            _miyable = null;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IQAQAble IQAQAble.GetQAQtor() => _qaqable;

        IQAQAble<T> IQAQAble<T>.GetQAQtor() => _qaqable;

        IMiyAble IMiyAble.GetMiytor() => _miyable;

        IMiyAble<T> IMiyAble<T>.GetMiytor() => _miyable;

        private IEnumerator<T> GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }
    }

    public static class MiyAble
    {
        public static IMiyAble<TSource> AsMiyAble<TSource>(this IEnumerable<TSource> source)
        {
            if (source is IMiyAble<TSource>)
                return (IMiyAble<TSource>)source;
            return new MiyableQuery<TSource>(source);
        }

        public static IMiyAble<TSource> AsMiyAble<TSource>(this IQueryable<TSource> source)
        {
            return (IMiyAble<TSource>)source;
        }

        public static void SayHello<TSource>(this IMiyAble<TSource> source)
        {
            Console.Write(source.GetType() + "：Hello Miy.");
        }
    }


    /**-------------------------------------**/
    public interface IAnimalParameter
    {
        string 種族 { get; }
        string Name { set; get; }
    }
    public interface IAnimalActive
    {
        void 叫();
    }
    public interface ICat : IAnimalActive, IAnimalParameter
    {
        string 品種 { get; }
        void 炸毛();
    }
    public interface ISheep : IAnimalActive, IAnimalParameter
    {
        string 品種 { get; }
        void 抖毛();
    }
    public abstract class Cat : IAnimalActive, IAnimalParameter
    {
        public abstract string Name { set; get; }

        public string 種族 => "貓";

        public void 叫() => Console.WriteLine(this.Name + "喵喵喵喵喵啊啊啊啊啊");
    }
    public class 鄙視你的貓 : Cat, ICat
    {
        public string 品種 => "大漠鄙視種";
        public override string Name { set; get; }

        public void 炸毛()
        {
            Console.WriteLine(this.Name + "高速炸毛OOOOOOOOAOOOOOOOO");
        }

        public 鄙視你的貓(string name)
        {
            this.Name = name;
        }
    }
}