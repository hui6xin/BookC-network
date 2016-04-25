using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SPersonalArrayList
{
    public class MyArrayList
    {

        object[] data;
        int currentIndex;

        public MyArrayList(int length)
        {
            this.data = new object[length];
            currentIndex = 0;
        }

        public void Add(object s)
        {
            data[currentIndex++] = s;
        }

        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(data);
        }
    }

    public class MyEnumerator : IEnumerator
    {

        private object[] _data;
        private int position = -1;

        public MyEnumerator(object[] data)
        {
            _data = data;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _data.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public object Current
        {
            get
            {
                return _data[position];
            }
        }
    }



    public class MyArrayList<T>: IEnumerable<T>
    {
    
        T[] data;
        int currentIndex;
    
        public MyArrayList(int length)
        {
            this.data = new T[length];
            currentIndex = 0;
        }
    
        public void Add(T s)
        {
            data[currentIndex++] = s;
        }
    
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    
        public IEnumerator<T> GetEnumerator()
        {
            return new MyEnumerator<T>(data);
        }
        //public IEnumerator<T> GetEnumerator()
        //{
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        yield return data[i];
        //    }
        //}
    }

    public class MyEnumerator<T>:IEnumerator<T>
    {
    
        private T[] _data;
        private int position = -1;
    
        public MyEnumerator(T[] data)
        {
            _data = data;
        }
    
        public bool MoveNext()
        {
            position++;
            return (position < _data.Length);
        }
    
        public void Reset()
        {
            position = -1;
        }
    
        public void Dispose()
        {
            //Dispose the resource
        }
    
        object IEnumerator.Current
        {
            get
            {
                return Current;    
            }
        }
    
        public T Current
        {
            get
            {
                return _data[position];
            }
        }
    }
    public class PowersOf2
    {

        public static System.Collections.Generic.IEnumerable<int> Power(int number, int exponent)
        {
            int result = 1;

            for (int i = 0; i < exponent; i++)
            {
                result = result * number;
                yield return result;
            }
        }

        // Output: 2 4 8 16 32 64 128 256
    }
}
