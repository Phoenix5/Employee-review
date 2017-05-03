using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleDelegateTest
{
    class Program
    {
        public delegate int BinaryOp(int x, int y);
        static void Main(string[] args)
        {
            Console.WriteLine("------ Async Delegate Invocation -------");
            //Print out the ID of the executing thread
            Console.WriteLine("Main() invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);
            //Invoke Add() on a secondary thread 
            var i = 0;

            while (i < 10)
            {
                Program p = new Program();
                BinaryOp b = new BinaryOp(new Program().Add);
                IAsyncResult iftAr = b.BeginInvoke(i, i, new AsyncCallback(p.AddComplete), null);
                
            }
            Console.ReadLine();
        }

        public int Add(int a, int b)
        {
            //Print out the ID of the executing thread
            Console.WriteLine("Add() invoked on thread {0}", Thread.CurrentThread.ManagedThreadId);
            //wait some time
            Thread.Sleep(5000);
            //perform the task
            return (a + b);
        }


        //Target of AsyncCallback delegate should match the following pattern
        public void AddComplete(IAsyncResult iftAr)
        {
            Console.WriteLine("AddComplete() running on thread {0}", Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("Operation completed.");

            //Getting result
            AsyncResult ar = (AsyncResult)iftAr;
            BinaryOp bp = (BinaryOp)ar.AsyncDelegate;
            int result = bp.EndInvoke(iftAr);
            

            Console.WriteLine("5 + 5 ={0}{1}", result);
            Console.WriteLine("Message recieved on thread {0}", Thread.CurrentThread.ManagedThreadId);
        }

    }
}
