using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace STest
{
    class Class1
    {
    }
    public delegate void SubEventHandler();
    public abstract class Subject
    {
        public event SubEventHandler SubEvent;
        protected void FireAway()
        {
            if (this.SubEvent != null)
                this.SubEvent();
        }
    }
    public  class Cat : Subject
    {
        public void Cry() 
        { 
          Console.WriteLine("cat cryed."); 
          this.FireAway(); 
        }
    }
    public abstract class Observer
    {
        public Observer(Subject sub)
        {
            sub.SubEvent += new SubEventHandler(Response);
        }
        public abstract void Response();
    }
    public class Mouse : Observer
    {
        private string name;
        public Mouse(string name, Subject sub)
            : base(sub)
        {
            this.name = name;
        }
        public override void Response() 
        { 
          Console.WriteLine(name + "attempt to escape!"); 
        }
    }
    public class Master : Observer
    {
        public Master(Subject sub) : base(sub) { }
        public override void Response() 
        { 
          Console.WriteLine("host waken"); 
        }
    }
    //class Class1
    //{
        //static void Main(string[] args)
        //{
            //Cat cat = new Cat();
            //Mouse mouse1 = new Mouse(mouse1, cat);
            //Mouse mouse2 = new Mouse(mouse2, cat);
            //Master master = new Master(cat);
            //cat.Cry();
        //}
    //}
}
